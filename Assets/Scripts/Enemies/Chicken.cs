using System;
using System.Collections;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    [SerializeField] private GameData _gameData;

    [HideInInspector] public Vector3 _targetPosition;
    public static event Action OnDisappear;

    private Rigidbody2D _rigidbody;
    private Vector3 _direction;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(InitialSpeed());
        RotateTowardsPlayer();
    }

    private void Update()
    {
        _direction = (_targetPosition - transform.position).normalized;
    }

    IEnumerator InitialSpeed()
    {
        _direction = (_targetPosition - transform.position).normalized;
        _rigidbody.velocity = _direction * 1.3f;
        yield return new WaitForSeconds(0.7f);
        _rigidbody.velocity = _direction * _gameData.ChickenSpeed;
    }

    private void RotateTowardsPlayer()
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > _targetPosition.x)
        {
            rotation.y = 0f;
        }
        else
        {
            rotation.y = 180f;
        }

        transform.eulerAngles = rotation;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
        OnDisappear?.Invoke();
    }
}
