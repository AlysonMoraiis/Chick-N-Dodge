using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPoint : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public float _speed = 5f;
    public float _dashSpeed = 12f;
    private Vector3 _target;
    private Vector3 _targetDash;

    private float _timer;

    private float _dashLength = 0.24f;
    private float _dashCooldown = 0.2f;

    private Vector3 direction;

    void Start()
    {
        _target = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            SetTargetPosition();
        }

        if (Input.GetMouseButtonUp(0))
        {
            CheckCanDash();
        }

        Dash();

        DashCooldown();

        direction = _target - transform.position;
        direction.Normalize();

        // define as condições de animação
        _animator.SetFloat("moveX", direction.x);
        _animator.SetFloat("moveY", direction.y);
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

            transform.position = Vector3.MoveTowards(transform.position, _targetDash, _dashSpeed * Time.deltaTime);
        }

        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
        }
    }

    private void CheckCanDash()
    {
        _animator.SetFloat("lastX", direction.x);
        _animator.SetFloat("lastY", direction.y);
        _animator.SetBool("IsRunning", false);
        if (_timer < 0.2f && _dashCooldown <= 0)
        {
            StartCoroutine(DashLength());
            Debug.Log("Dash");
            _dashCooldown = 0.5f;
            _animator.SetBool("IsDashing", true);
        }
        _target = transform.position;

        _timer = 0f;
    }

    private void SetTargetPosition()
    {
        _target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _target.z = transform.position.z;
        _timer += Time.deltaTime;

        _animator.SetBool("IsRunning", true);
        if (!Mathf.Approximately(0, direction.x))
        {
            transform.rotation = direction.x < 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
        }
    }

    private IEnumerator DashLength()
    {
        _targetDash = _target;
        _dashLength = 0.24f;
        yield return new WaitForSeconds(_dashLength);
        _target = transform.position;
        _animator.SetBool("IsDashing", false);

    }
}
