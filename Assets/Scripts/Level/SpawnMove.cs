using UnityEngine;

public class SpawnMove : MonoBehaviour
{
    [SerializeField] private float _maxXPosition;
    Plane _plane = new Plane(Vector3.forward, Vector3.zero);


    void Update()
    {
        if (Input.GetMouseButton(0) && !Pointer.IsPointerOverUIObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (_plane.Raycast(ray, out float distance))
            {
                float newXPosition = ray.GetPoint(distance).x;
                newXPosition = Mathf.Clamp(newXPosition, -_maxXPosition, _maxXPosition);
                transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);
            }
        }
    }
}
