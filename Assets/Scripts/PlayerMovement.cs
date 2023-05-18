using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;

    private Rigidbody2D _rigidbody;
    private Vector3 _moveVector;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // _moveVector = Vector3.zero;
        // _moveVector.x = _joystick.Horizontal * _moveSpeed * Time.deltaTime;
        // _moveVector.y = _joystick.Vertical * _moveSpeed * Time.deltaTime;
        float moveH = _joystick.Horizontal;
        float moveV = _joystick.Vertical;

        Vector2 dir = new Vector2(moveH, moveV);
        _rigidbody.velocity = new Vector2(moveH*_moveSpeed, moveV*_moveSpeed);
        // if(dir != Vector2.zero)
        // {
        //     transform.LookAt(transform.position + dir);
        // }
    }

}
