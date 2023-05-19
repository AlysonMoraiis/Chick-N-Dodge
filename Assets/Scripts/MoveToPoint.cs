using System.Collections;
using UnityEngine;

public class MoveToPoint : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _dashSpeed = 12f;
    [SerializeField] private FloatingJoystick _joystick;

    private float _timer;
    private const float DashLength = 0.24f;
    private float _dashCooldown = 0.2f;
    private Rigidbody2D _rigidbody;
    private float _moveH, _moveV;
    private float _lastMoveH, _lastMoveV;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Time.timeScale == 1)
        {
            HandleInput();
            Move();
            DashCooldown();
            UpdateAnimator();
        }
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonUp(0))
        {
            _animator.SetBool("IsRunning", false);
            SetIdleDirectionAnimator();
            _timer = 0f;
        }
    }

    private void Move()
    {
        _moveH = _joystick.Horizontal;
        _moveV = _joystick.Vertical;

        if (_moveH != 0 || _moveV != 0)
        {
            _timer += Time.deltaTime;
            _animator.SetBool("IsRunning", true);
            _lastMoveH = _moveH;
            _lastMoveV = _moveV;

            if (ShouldDash())
            {
                _animator.SetBool("IsRunning", false);
                CheckCanDash();
                Dash();
            }
        }

        UpdateRotation();

        if (!_animator.GetBool("IsDashing"))
        {
            _rigidbody.velocity = new Vector2(_moveH * _speed, _moveV * _speed);
        }
    }

    private bool ShouldDash()
    {
        return (_moveH >= 0.6f || _moveH <= -0.6f || _moveV >= 0.6f || _moveV <= -0.6f) && _timer <= 0.1f;
    }

    private void UpdateRotation()
    {
        if (!Mathf.Approximately(0, _joystick.Horizontal))
        {
            transform.rotation = _joystick.Horizontal < 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
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
        if (DashLength > 0)
        {
            Vector2 dashDirection = new Vector2(_moveH, _moveV).normalized;
            _rigidbody.velocity = dashDirection * _dashSpeed;
        }
    }

    private void CheckCanDash()
    {
        SetIdleDirectionAnimator();

        if (_timer < 0.2f && _dashCooldown <= 0)
        {
            StartCoroutine(DashCoroutine());
            _dashCooldown = 0.5f;
            _animator.SetBool("IsDashing", true);
        }
    }

    private void SetIdleDirectionAnimator()
    {
        _animator.SetFloat("lastX", _lastMoveH);
        _animator.SetFloat("lastY", _lastMoveV);
        _animator.SetBool("IsRunning", false);
    }

    private IEnumerator DashCoroutine()
    {
        yield return new WaitForSeconds(DashLength);
        _animator.SetBool("IsDashing", false);

        if (_moveH != 0 || _moveV != 0)
        {
            _animator.SetBool("IsRunning", true);
        }
        else
        {
            _animator.SetBool("IsRunning", false);
            SetIdleDirectionAnimator();
        }
    }

    private void UpdateAnimator()
    {
        _animator.SetFloat("moveX", _joystick.Horizontal);
        _animator.SetFloat("moveY", _joystick.Vertical);
    }
}
