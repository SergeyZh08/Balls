using UnityEngine;
using TMPro;

public class ActiveItem : Item
{
    [field: SerializeField] public Projection Projection { get; private set; }
    [field: SerializeField] public int Level { get; private set; }
    [field: SerializeField] public Rigidbody Rigidbody { get; protected set; }
    public bool IsDead { get; private set; }
    public float Radius { get; protected set; }

    [SerializeField] protected TextMeshProUGUI _scoreText;
    [SerializeField] protected SphereCollider _collider;
    [SerializeField] protected SphereCollider _trigger;
    [SerializeField] protected Animator _animator;
    [SerializeField] protected AudioClip _diedClip;

    protected virtual void Start()
    {
        Projection.Hide();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsDead)
        {
            return;
        }

        if (other.attachedRigidbody)
        {
            ActiveItem otherItem = other.attachedRigidbody.GetComponent<ActiveItem>();
            if (otherItem)
            {
                if (!otherItem.IsDead && Level == otherItem.Level)
                {
                    MergeController.Instance.Merge(this, otherItem);
                }
            }
        }
    }

    public virtual void DoEffect() { }

    [ContextMenu("UpLevel")]
    public void IncreaseLevel()
    {
        Level++;
        SetItem(Level);
        _animator.SetTrigger("IncreaseLevel");
        _trigger.enabled = false;
        Invoke(nameof(SetEnabldeCollider), 0.1f);
    }

    public virtual void SetItem(int level)
    {
        Level = level;

        int score = (int)Mathf.Pow(2, Level + 1);
        _scoreText.text = score.ToString();
    }

    public void SetItemInTube()
    {
        _collider.enabled = false;
        _trigger.enabled = false;
        Rigidbody.isKinematic = true;
        Rigidbody.interpolation = RigidbodyInterpolation.None;
    }

    public void Drop()
    {
        _collider.enabled = true;
        _trigger.enabled = true;
        Rigidbody.isKinematic = false;
        Rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        transform.parent = null;
    }

    public void Disable()
    {
        _collider.enabled = false;
        _trigger.enabled = false;
        Rigidbody.isKinematic = true;
        IsDead = true;
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    private void SetEnabldeCollider()
    {
        _trigger.enabled = true;
    }
}
