using UnityEngine;

public class Stone : PassiveItem
{
    [SerializeField] private Stone _prefabStone;
    [Range(0, 2)]
    [SerializeField] private int _level = 2;
    public override void OnAffect()
    {
        if (_level > 0)
        {
            for (int i = 0; i < 2; i++)
            {
                CreateNewStone(_level - 1);
            }
        }
        else
        {
            ScoreManager.Instance.AddScore(ItemType, transform.position);
        }

        Die();
    }

    public void Init(int level)
    {
        _level = level;
        float scale = 1;

        if (_level == 2)
        {
            scale = 1;
        }
        else if (_level == 1)
        {
            scale = 0.5f;
        }
        else if (_level == 0)
        {
            scale = 0.35f;
        }

        transform.localScale = Vector3.one * scale;
    }

    private void CreateNewStone(int level)
    {
        Stone newStone = Instantiate(_prefabStone, transform.position, Quaternion.identity);
        newStone.Init(level);
    }

    private void Die()
    {
        AudioSource.PlayClipAtPoint(_diedClip, Camera.main.transform.position, 0.5f);
        Instantiate(_hitEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
        Destroy(gameObject);
    }
}
