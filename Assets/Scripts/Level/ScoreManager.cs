using System.Collections;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField] private Level _level;
    [SerializeField] private ScoreElement[] _scoreElementsPrefab;
    [SerializeField] private Camera _camera;
    [SerializeField] private ScoreElement[] _scoreElements;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _scoreElements = new ScoreElement[_level.LevelTasks.Length];

        for (int i = 0; i < _level.LevelTasks.Length; i++)
        {
            Task task = _level.LevelTasks[i];
            ItemType type = task.ItemType;

            for (int j = 0; j < _scoreElementsPrefab.Length; j++)
            {
                if (_scoreElementsPrefab[j].ItemType == type)
                {
                    ScoreElement newElement = Instantiate(_scoreElementsPrefab[j], transform);
                    newElement.Init(task);

                    _scoreElements[i] = newElement;
                }
            }
        }
    }

    public bool AddScore(ItemType itemType, Vector3 position, int level = 0)
    {
        for (int i = 0; i < _scoreElements.Length; i++)
        {
            if (itemType != _scoreElements[i].ItemType) continue;
            if (_scoreElements[i].CountOfElements == 0) continue;
            if (_scoreElements[i].Level != level) continue;

            StartCoroutine(AddScoreAnimation(_scoreElements[i], position));
            return true;
        }

        return false;
    }

    public void CheckWin()
    {
        for (int i = 0; i < _scoreElements.Length; i++)
        {
            if (_scoreElements[i].CountOfElements != 0)
            {
                return;
            }
        }

        GameManager.Instance.Win();
    }

    private IEnumerator AddScoreAnimation(ScoreElement element, Vector3 position)
    {
        Vector3 a = position;
        Vector3 b = position + Vector3.back * 5f + Vector3.down * 2f;
        Vector3 screenPosition = new Vector3(element.ImageTransform.position.x, element.ImageTransform.position.y, -_camera.transform.position.z);
        Vector3 d = _camera.ScreenToWorldPoint(screenPosition);
        Vector3 c = d + Vector3.back * 2f;

        GameObject icon = Instantiate(element.FlyindIconPrefab, position, Quaternion.identity);

        if (element.GetComponent<ScoreElementBall>() is ScoreElementBall scoreElementBall)
        {
            icon.GetComponent<Renderer>().material.color = scoreElementBall.RawImage.color;
        }

        for (float f = 0; f < 1; f += Time.deltaTime)
        {
            icon.transform.position = Bezier.GetPoint(a, b, c, d, f);
            yield return null;
        }

        element.AddOne();
        Destroy(icon);

        CheckWin();
    }
}