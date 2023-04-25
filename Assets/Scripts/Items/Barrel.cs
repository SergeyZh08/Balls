using UnityEngine;

public class Barrel : PassiveItem
{
    public override void OnAffect()
    {
        Die();
    }

    private void Die()
    {
        AudioSource.PlayClipAtPoint(_diedClip, Camera.main.transform.position, 0.5f);
        Instantiate(_hitEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
        Destroy(gameObject);
        ScoreManager.Instance.AddScore(ItemType, transform.position);
    }
}
