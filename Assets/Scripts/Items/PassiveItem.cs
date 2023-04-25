using UnityEngine;

public class PassiveItem : Item
{
    [SerializeField] protected ParticleSystem _hitEffect;
    [SerializeField] protected AudioClip _diedClip;

    public virtual void OnAffect() { }
}
