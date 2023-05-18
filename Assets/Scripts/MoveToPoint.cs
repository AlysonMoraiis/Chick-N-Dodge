using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPoint : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _dashSpeed = 12f;
    [SerializeField] private FloatingJoystick _joystick;

    private float _timer;
    private float _dashLength = 0.24f;
    private float _dashCooldown = 0.2f;
    private Rigidbody2D _rigidbody;
    private float _moveH, _moveV;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Time.timeScale == 1)
        {
            if (Input.GetMouseButtonUp(0))
            {
                _animator.SetBool("IsRunning", false);
                CheckCanDash();
                Dash();
            }

            Move();
            DashCooldown();

            // define as condições de animação
            _animator.SetFloat("moveX", _joystick.Horizontal);
            _animator.SetFloat("moveY", _joystick.Vertical);
        }
    }

    private void Move()
    {
        _moveH = _joystick.Horizontal;
        _moveV = _joystick.Vertical;

        if (_moveH != 0 || _moveV != 0)
        {
            _animator.SetBool("IsRunning", true);
            _timer += Time.deltaTime;
        }

        if (!Mathf.Approximately(0, _joystick.Horizontal))
        {
            transform.rotation = _joystick.Horizontal < 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
        }

        if (!_animator.GetBool("IsDashing"))
        {

            _rigidbody.velocity = new Vector2(_moveH * _speed, _moveV * _speed);
        }
    }

    private void DashCooldown()
    {
        if (_dashCooldown > 0)
        {
            _dashCooldown -= Time.deltaTime;
        }
    }

    private void Dash()
    {
        if (_dashLength > 0)
        {
            _dashLength -= Time.deltaTime;
            Vector2 dashDirection = new Vector2(_moveH, _moveV).normalized;
            _rigidbody.velocity = dashDirection * _dashSpeed;
        }
    }

    private void CheckCanDash()
    {
        _animator.SetFloat("lastX", _moveH);
        _animator.SetFloat("lastY", _moveV);
        _animator.SetBool("IsRunning", false);

        if (_timer < 0.2f && _dashCooldown <= 0)
        {
            StartCoroutine(DashLength());
            Debug.Log("Dash");
            _dashCooldown = 0.5f;
            _animator.SetBool("IsDashing", true);
        }

        _timer = 0f;
    }

    private IEnumerator DashLength()
    {
        _dashLength = 0.24f;
        yield return new WaitForSeconds(_dashLength);
        _animator.SetBool("IsDashing", false);
    }
}
