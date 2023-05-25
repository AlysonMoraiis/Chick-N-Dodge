using System.Collections;
using UnityEngine;

public class ChickenSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _chickenPrefab;
    [SerializeField] private GameObject _playerGameObject;
    [SerializeField] private float _spawnTimer = 1f;
    [SerializeField] private GameData _gameData;

    private float _timer;

    private void Start()
    {
        InitializeSpawnTimer();
    }

    private void Update()
    {
        HandleChickenSpawning();
    }

    private void InitializeSpawnTimer()
    {
        _gameData.SpawnTime = _spawnTimer;
        _timer = _spawnTimer;
    }

    private void HandleChickenSpawning()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            StartCoroutine(SpawnChicken());
            _timer = _gameData.SpawnTime;
        }
    }

    private IEnumerator SpawnChicken()
    {
        Vector3 spawnPosition = GetRandomCornerPosition();
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(spawnPosition);
        worldPosition.z = 0f;

        GameObject chickenObject = Instantiate(_chickenPrefab, worldPosition, Quaternion.identity);
        Chicken chicken = chickenObject.GetComponent<Chicken>();
        chicken._targetPosition = _playerGameObject.transform.position;

        yield return StartCoroutine(WaitForNewPlayerPosition());

        chicken._targetPosition = _playerGameObject.transform.position;
    }

    private IEnumerator WaitForNewPlayerPosition()
    {
        yield return new WaitForSeconds(0.6f);
    }

    private Vector3 GetRandomCornerPosition()
    {
        float randomNum = Random.Range(0f, 100f);
        Vector3 edgePosition = Vector3.zero;

        if (randomNum <= 34)
        {
            edgePosition = new Vector3(0f, Random.Range(0f, Screen.height), 0f);
        }
        else if (randomNum >= 35 && randomNum <= 69)
        {
            edgePosition = new Vector3(Screen.width, Random.Range(0f, Screen.height), 0f);
        }
        else if (randomNum >= 70 && randomNum <= 84)
        {
            edgePosition = new Vector3(Random.Range(0f, Screen.width), 0f, 0f);
        }
        else
        {
            edgePosition = new Vector3(0f, Random.Range(0f, Screen.height), 0f);
        }

        return edgePosition;
    }
}
