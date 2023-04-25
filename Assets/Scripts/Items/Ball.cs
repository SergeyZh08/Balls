using UnityEngine;

public class Ball : ActiveItem
{
    [SerializeField] private Transform _visualTransform;
    [SerializeField] private Materials _materials;
    [SerializeField] private Renderer _renderer;

    public override void SetItem(int level)
    {
        base.SetItem(level);
        _renderer.material = _materials.BallMaterials[Level];

        Radius = Mathf.Lerp(0.4f, 0.55f, Level / 10f);
        _collider.radius = Radius;
        _trigger.radius = Radius + 0.2f;
        _visualTransform.localScale = Vector3.one * Radius * 2;

        Projection.Setup(_materials.BallProjectionMaterials[Level], _scoreText.text, Radius);

        if (ScoreManager.Instance.AddScore(ItemType, transform.position, Level))
        {
            Die();
        }
    }

    public override void Die()
    {
        AudioSource.PlayClipAtPoint(_diedClip, Camera.main.transform.position, 0.1f);
        Destroy(gameObject);
    }

    public override void DoEffect()
    {
        base.DoEffect();
        IncreaseLevel();
    }
}
