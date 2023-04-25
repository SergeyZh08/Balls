using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MergeController : MonoBehaviour
{
    public static MergeController Instance;
    public UnityEvent OnMerge;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Merge(ActiveItem itemA, ActiveItem itemB)
    {
        ActiveItem fromItem;
        ActiveItem toItem;

        if (Mathf.Abs(itemA.transform.position.y - itemB.transform.position.y) > 0.02f)
        {
            if (itemA.transform.position.y > itemB.transform.position.y)
            {
                fromItem = itemA;
                toItem = itemB;
            }
            else
            {
                fromItem = itemB;
                toItem = itemA;
            }
        }
        else
        {
            if (itemA.Rigidbody.velocity.magnitude > itemB.Rigidbody.velocity.magnitude)
            {
                fromItem = itemA;
                toItem = itemB;
            }
            else
            {
                fromItem = itemB;
                toItem = itemA;
            }
        }

        WakeUpNeighbors(fromItem);
        StartCoroutine(MergeProcess(fromItem, toItem));
    }

    private void WakeUpNeighbors(ActiveItem item)
    {
        Collider[] colliders = Physics.OverlapSphere(item.transform.position, item.Radius + 0.4f);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].attachedRigidbody)
            {
                ActiveItem activeItem = colliders[i].attachedRigidbody.GetComponent<ActiveItem>();
                if (activeItem)
                {
                    activeItem.Rigidbody.WakeUp();
                }
            }
        }
    }

    private IEnumerator MergeProcess(ActiveItem fromItem, ActiveItem toItem)
    {
        fromItem.Disable();

        if ((fromItem.ItemType == ItemType.Ball || toItem.ItemType == ItemType.Ball))
        {
            Vector3 spawn = fromItem.transform.position;
            for (float t = 0; t < 1f; t += Time.deltaTime / 0.08f)
            {
                fromItem.transform.position = Vector3.Lerp(spawn, toItem.transform.position, t);
                yield return null;
            }
        }
        
        if (fromItem.ItemType == ItemType.Ball && toItem.ItemType == ItemType.Ball)
        {
            fromItem.Die();
            toItem.DoEffect();
            ExplodeBall(toItem.transform.position, toItem.Radius + 0.45f);
        }
        else
        {
            if (fromItem.ItemType == ItemType.Ball)
            {
                fromItem.Die();
            }
            else
            {
                fromItem.Disable();
                fromItem.DoEffect();
            }

            if (toItem.ItemType == ItemType.Ball)
            {
                toItem.Die();
            }
            else
            {
                toItem.Disable();
                toItem.DoEffect();
            }
        }

        OnMerge.Invoke();
    }

    private void ExplodeBall(Vector3 position, float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(position, radius);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].attachedRigidbody)
            {
                PassiveItem passiveItem = colliders[i].attachedRigidbody.GetComponent<PassiveItem>();
                if (passiveItem)
                {
                    passiveItem.OnAffect();
                }
            }
        }
    }
}
