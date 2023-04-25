using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreElementBall : ScoreElement
{
    [field: SerializeField] public RawImage RawImage { get; private set; }
    [SerializeField] private Materials _materials;
    [SerializeField] private TextMeshProUGUI _levelText;
    public override void Init(Task task)
    {
        base.Init(task);
        Level = task.Level;
        RawImage.color = _materials.BallMaterials[Level].color;
        int level = (int)Mathf.Pow(2, Level + 1);
        _levelText.text = level.ToString();
    }
}
