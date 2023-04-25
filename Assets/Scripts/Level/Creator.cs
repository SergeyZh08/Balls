using System.Collections;
using UnityEngine;
using TMPro;

public class Creator : MonoBehaviour
{
    [SerializeField] private ActiveItem _ballPrefab;
    [SerializeField] private Transform _rayTransform;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Transform _tube;
    [SerializeField] private Transform _spawn;
    [SerializeField] private float _travelTime = 0.25f;

    [SerializeField] private TextMeshProUGUI _ballsCountText;

    private ActiveItem _itemInTube;
    private ActiveItem _itemInSpawner;

    private int _ballsCount;
    private int _maxBallCount;

    private float _currentTimeToLoose = 0f;
    private float _timeToLoose = 5f;

    private Coroutine _looseCoroutine;
    private void OnEnable()
    {
        Chest.OnPick += AddBalls;
    }

    private void OnDisable()
    {
        Chest.OnPick -= AddBalls;
    }

    private void Start()
    {
        _ballsCount = Level.Instance.NumberOfBalls;
        _maxBallCount = Level.Instance.MaxBallLevel;
        UpdateText();

        CreateNewItem();
        StartCoroutine(MoveToTube());

        GameManager.Instance.OnWin.AddListener(StopCoroutine);
    }

    private void LateUpdate()
    {
        if (_itemInSpawner)
        {
            Ray ray = new Ray(_rayTransform.transform.position, Vector3.down);
            if (Physics.SphereCast(ray, _itemInSpawner.Radius, out RaycastHit hit, 100f, _layerMask, QueryTriggerInteraction.Ignore))
            {
                _rayTransform.localScale = new Vector3(_itemInSpawner.Radius * 2, hit.distance, 1f);
                _itemInSpawner.Projection.SetPosition(_spawn.position + Vector3.down * hit.distance);
            }

            if (Input.GetMouseButtonUp(0) && !Pointer.IsPointerOverUIObject())
            {
                Drop();
            }
        }
    }

    public void AddBalls(int ballsCount)
    {
        _ballsCount += ballsCount;
        UpdateText();

        if (!_itemInTube)
        {
            CreateNewItem();
        }
    }

    private void CreateNewItem()
    {
        if (_ballsCount == 0)
        {
            return;
        }

        int level = Random.Range(0, _maxBallCount + 1);
        _itemInTube = Instantiate(_ballPrefab, _tube.position, Quaternion.identity);
        _itemInTube.SetItem(level);
        _itemInTube.SetItemInTube();

        _ballsCount--;
        UpdateText();
    }

    private IEnumerator MoveToTube()
    {
        _itemInTube.transform.parent = _spawn;
        for (float t = 0; t < _travelTime; t += Time.deltaTime)
        {
            _itemInTube.transform.position = Vector3.Lerp(_tube.position, _spawn.position, t * 2);

            yield return null;
        }

        _itemInTube.transform.localPosition = Vector3.zero;

        
        _itemInSpawner = _itemInTube;
        _itemInTube = null;
        _rayTransform.gameObject.SetActive(true);
        _itemInSpawner.Projection.Show();

        if (!_itemInTube)
        {
            CreateNewItem();
        }
    }

    private void Drop()
    {
        _itemInSpawner.Projection.Hide();
        _itemInSpawner.Drop();
        _itemInSpawner = null;
        _rayTransform.gameObject.SetActive(false);

        if (_itemInTube)
        {
            StartCoroutine(MoveToTube());
        }
        else
        {
            _looseCoroutine = StartCoroutine(TimeToLoose());
            MergeController.Instance.OnMerge.AddListener(ResetLooseTime);
        }
    }

    private void ResetLooseTime()
    {
        _currentTimeToLoose = 0f;
    }

    private void StopCoroutine()
    {
        if (_looseCoroutine != null)
        {
            StopCoroutine(_looseCoroutine);
        }
    }

    private IEnumerator TimeToLoose()
    {
        for (_currentTimeToLoose = 0; _currentTimeToLoose < _timeToLoose; _currentTimeToLoose += Time.deltaTime)
        {
            if (_ballsCount > 0)
            {
                _currentTimeToLoose = 0;

                if (!_itemInTube)
                {
                    CreateNewItem();
                }
                if (!_itemInSpawner)
                {
                    StartCoroutine(MoveToTube());
                }
                StopCoroutine();
            }
            
            yield return null;
        }

        GameManager.Instance.Loose();
    }

    private void UpdateText()
    {
        _ballsCountText.text = _ballsCount.ToString();
    }
}
