using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    public Vector3 _targetPosition;

    void Start()
    {
        Vector3 direction = (_targetPosition - transform.position).normalized;
        GetComponent<Rigidbody2D>().velocity = direction * _speed;
        RotateTowardsPlayer();
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
    }
}
