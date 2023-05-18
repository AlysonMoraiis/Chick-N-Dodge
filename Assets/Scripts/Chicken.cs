using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    [HideInInspector] public Vector3 _targetPosition;
    [SerializeField] private GameData _gameData;

    public static event Action OnDisappear;

    void Start()
    {
        Vector3 direction = (_targetPosition - transform.position).normalized;
        GetComponent<Rigidbody2D>().velocity = direction * _gameData.ChickenSpeed;
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
        OnDisappear?.Invoke();
    }
}
