using UnityEngine;
using TMPro;

public class Projection : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Transform _visualTransform;

    public void Setup(Material material, string scoreText, float radius)
    {
        _renderer.material = material;
        _scoreText.text = scoreText;
        _visualTransform.localScale = Vector3.one * radius * 2;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
}
