using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMove : MonoBehaviour
{
    private Transform _transform;
    [SerializeField]
    private float _speed;
    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    
    void FixedUpdate()
    {
        if (_transform.position.z <34)
        {
            var v = Mathf.Lerp(_transform.position.z, 35f, Time.deltaTime * _speed);
            _transform.position = new Vector3(_transform.position.x, _transform.position.y, v);
        }
     
    }
}
