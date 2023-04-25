using System.Collections;
using UnityEngine;

public class Dynamit : ActiveItem
{
    [SerializeField] private float _affectRadius = 1.5f;
    [SerializeField] private float _forceValue = 1000f;

    [SerializeField] private ParticleSystem _effectPrefab;
    [SerializeField] private GameObject _affectArea;
    [SerializeField] private LayerMask _layerMask;

    protected override void Start()
    {
        base.Start();
        _affectArea.SetActive(false);
    }

    public override void DoEffect()
    {
        base.DoEffect();
        StartCoroutine(AffectProcess());
    }

    private IEnumerator AffectProcess()
    {
        _affectArea.SetActive(true);
        _animator.enabled = true;
        yield return new WaitForSeconds(1f);

        Collider[] colliders = Physics.OverlapSphere(transform.position, _affectRadius, _layerMask, QueryTriggerInteraction.Ignore);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].attachedRigidbody)
            {
                ActiveItem activeItem = colliders[i].attachedRigidbody.GetComponent<ActiveItem>();
                if (activeItem && !activeItem.IsDead)
                {
                    Vector3 toItem = (activeItem.transform.position - transform.position).normalized;
                    activeItem.Rigidbody.velocity = toItem * _forceValue;
                }

                PassiveItem passiveItem = colliders[i].attachedRigidbody.GetComponent<PassiveItem>();
                if (passiveItem)
                {
                    passiveItem.OnAffect();
                }
            }
        }

        Die();
    }

    public override void Die()
    {
        AudioSource.PlayClipAtPoint(_diedClip, Camera.main.transform.position, 0.5f);
        Instantiate(_effectPrefab, transform.position, Quaternion.Euler(-90f, 0f, 0f));
        Destroy(gameObject);
    }

    private void OnValidate()
    {
        _affectArea.transform.localScale = Vector3.one * _affectRadius * 2f;
    }
}
