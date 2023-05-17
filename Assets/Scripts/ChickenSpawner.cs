using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _chickenPrefab;
    [SerializeField] private GameObject _playerGameObject;
    [SerializeField] private float _spawnTimer = 1f;

    private float _timer;

    private Vector3 _playerVector3;

    void Start()
    {
        _timer = _spawnTimer;
    }

    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            _playerVector3 = _playerGameObject.transform.position;
            SpawnChicken();
            _timer = _spawnTimer;
        }
    }

    private void SpawnChicken()
    {
        Vector3 spawnPosition = GetRandomCornerPosition();
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(spawnPosition);
        worldPosition.z = 0f;
        GameObject chickenObject = Instantiate(_chickenPrefab, worldPosition, Quaternion.identity);
        Chicken chicken = chickenObject.GetComponent<Chicken>();
        chicken._targetPosition = _playerVector3;
    }

    private Vector3 GetRandomCornerPosition()
    {
        float randomX = Random.Range(0f, 1f);
        float randomY = Random.Range(0f, 1f);
        Vector3 edgePosition = Vector3.zero;

        if (randomX < 0.5f && randomY < 0.5f)
        {
            // Canto  esquerdo
            edgePosition = new Vector3(0f, Random.Range(0f, Screen.height), 0f);
        }
        else if (randomX < 0.5f && randomY >= 0.5f)
        {
            // Canto superior
            edgePosition = new Vector3(0f, Random.Range(0f, Screen.height), 0f);
        }
        else if (randomX >= 0.5f && randomY < 0.5f)
        {
            // Canto inferior 
            edgePosition = new Vector3(Random.Range(0f, Screen.width), 0f, 0f);
        }
        else
        {
            // Canto  direito
            edgePosition = new Vector3(Screen.width, Random.Range(0f, Screen.height), 0f);
        }

        return edgePosition;
    }
}
