using UnityEngine;

public class Box : PassiveItem
{
    [SerializeField] private Animator _hitBox;
    [SerializeField] private int _health = 2;
    [SerializeField] private GameObject[] _levels;
    public override void OnAffect()
    {
        _health--;
        AudioSource.PlayClipAtPoint(_diedClip, Camera.main.transform.position, 0.5f);
        Instantiate(_hitEffect, transform.position, Quaternion.Euler(-90f, 0f,0f));

        if (_health > 0)
        {
            _hitBox.SetTrigger("Hit");
            SetHealth();
        }
        else
        {
            Die();
        }
    }

    public void SetHealth()
    {
        for (int i = 0; i < _levels.Length; i++)
        {
            _levels[i].SetActive(i < _health);
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        ScoreManager.Instance.AddScore(ItemType, transform.position);
    }
}
