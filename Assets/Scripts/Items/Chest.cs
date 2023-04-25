using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Chest : ActiveItem
{
    public static event Action<int> OnPick;

    [SerializeField] private int _ballsCount = 5;
    [SerializeField] private ParticleSystem _effectPrefab;
    [SerializeField] private GameObject _flyindIconPrefab;
    [SerializeField] private Materials _materials;
    [SerializeField] private GameObject _main;

    [SerializeField] private AudioSource _openChest;
    [SerializeField] private AudioSource _spawnSound;

    private BallsCountAnim _ballsCountText;
    private List<GameObject> _icons = new List<GameObject>();

    private Vector3 _a;
    private Vector3 _b;
    private Vector3 _c;
    private Vector3 _d;

    public override void DoEffect()
    {
        base.DoEffect();
        StartCoroutine(AffectProcess());
        _openChest.Play();
    }

    public override void Die()
    {
        Destroy(gameObject);
    }

    public void SetEnabled()
    {
        AudioSource.PlayClipAtPoint(_diedClip, Camera.main.transform.position, 0.5f);
        Instantiate(_effectPrefab, transform.position, Quaternion.Euler(-90f, 0f, 0f));
        Rigidbody.isKinematic = true;
        _main.SetActive(false);
    }

    private IEnumerator AffectProcess()
    {
        _animator.enabled = true;
        yield return new WaitForSeconds(1f);

        StartCoroutine(CreateBalls());
    }

    private IEnumerator CreateBalls()
    {
        _ballsCountText = FindAnyObjectByType<BallsCountAnim>();

        _a = transform.position;
        _b = transform.position + Vector3.back * 2f + Vector3.down * 2f;
        Vector3 screenPosition = new Vector3(_ballsCountText.transform.position.x, _ballsCountText.transform.position.y, -Camera.main.transform.position.z);
        _d = Camera.main.ScreenToWorldPoint(screenPosition);
        _c = _d + Vector3.back * 2f;

        for (int i = 0; i < _ballsCount; i++)
        {
            GameObject icon = Instantiate(_flyindIconPrefab, transform.position, Quaternion.identity);
            icon.GetComponent<Renderer>().material.color = _materials.BallMaterials[UnityEngine.Random.Range(0, _materials.BallMaterials.Length)].color;
            _icons.Add(icon);
            _spawnSound.Play();
            yield return new WaitForSeconds(0.075f);
            StartCoroutine(AddAnimation(icon));
        }

        yield return new WaitForSeconds(0.2f);
        SetEnabled();
    }

    private IEnumerator AddAnimation(GameObject icon)
    {
        for (float f = 0; f < 1; f += Time.deltaTime)
        {
            icon.transform.position = Bezier.GetPoint(_a, _b, _c, _d, f);
            yield return null;
        }

        OnPick?.Invoke(1);
        _ballsCountText.StartAnim();

        _icons.Remove(icon);
        Destroy(icon);

        if (_icons.Count == 0)
        {
            Die();
        }
    }

}
