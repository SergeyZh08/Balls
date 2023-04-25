using System.Collections;
using UnityEngine;

public class ButtonAnim : MonoBehaviour
{
    [SerializeField] private AnimationCurve _animationCurve;
    private Coroutine _coroutine;
    void Start()
    {
        _coroutine = StartCoroutine(StartButtonAnim());
    }

    private void OnDisable()
    {
        StopCoroutine(_coroutine);
    }

    private IEnumerator StartButtonAnim()
    {
        while (true)
        {
            for (float t = 0; t < 1; t += Time.deltaTime * 0.5f)
            {
                float scale = _animationCurve.Evaluate(t);
                transform.localScale = Vector3.one * scale;
                yield return null;
            }
        }
    }
}
