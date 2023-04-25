using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private float _mixY = -27;
    [SerializeField] private float _maxY = 28;
    [SerializeField] private float _sensetivity = 5f;

    private float _newPositionY;
    private float _oldPositionY;

    private void Start()
    {
        _newPositionY = transform.position.y;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _oldPositionY = Input.mousePosition.y;
        }

        if (Input.GetMouseButton(0))
        {
            float delta = Input.mousePosition.y - _oldPositionY;
            _oldPositionY = Input.mousePosition.y;
            _newPositionY -= delta * _sensetivity / Screen.height;

            _newPositionY = Mathf.Clamp(_newPositionY, _mixY, _maxY);

            transform.position = new Vector3(transform.position.x, _newPositionY, transform.position.z);
        }
    }
}
