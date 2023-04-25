using System.Collections;
using UnityEngine;

public class BallsCountAnim : MonoBehaviour
{
    [SerializeField] private AnimationCurve _animationCurve;
    private Coroutine _coroutine;

    public void StartAnim()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(Animation());
    }

    private IEnumerator Animation()
    {
        for (float f = 0; f < 1f; f += Time.deltaTime * 2f)
        {
            float scale = _animationCurve.Evaluate(f);
            transform.localScale = Vector3.one * scale;
            yield return null;
        }

        transform.localScale = Vector3.one;
    }
}
