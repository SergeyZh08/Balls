using System.Collections;
using UnityEngine;
using TMPro;

public class ScoreElement : MonoBehaviour
{
    [field: SerializeField] public ItemType ItemType { get; private set; }
    [field: SerializeField] public int Level { get; protected set; }
    [field: SerializeField] public int CountOfElements { get; private set; }
    [field: SerializeField] public Transform ImageTransform { get; private set; }
    [field: SerializeField] public GameObject FlyindIconPrefab { get; private set; }
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private TextMeshProUGUI _text;
    

    public virtual void Init(Task task)
    {
        CountOfElements = task.Number;
        UpdateText();
    }

    [ContextMenu("AddOne")]
    public void AddOne()
    {
        CountOfElements--;
        if (CountOfElements < 0)
        {
            CountOfElements = 0;
        }
        UpdateText();

        StartCoroutine(Animation());
    }

    private void UpdateText()
    {
        _text.text = CountOfElements.ToString();
    }

    private IEnumerator Animation()
    {
        for (float t = 0; t < 1; t += Time.deltaTime * 2f)
        {
            float scale = _animationCurve.Evaluate(t);
            ImageTransform.localScale = Vector3.one * scale;
            yield return null;
        }

        ImageTransform.localScale = Vector3.one;
    }
}
