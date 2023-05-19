using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _chickenPrefab;
    [SerializeField] private GameObject _playerGameObject;
    [SerializeField] private float _spawnTimer = 1f;
    [SerializeField] private GameData _gameData;

    private float _timer;

    private Vector3 _playerVector3;

    void Start()
    {
        _gameData.SpawnTime = _spawnTimer;
        _timer = _spawnTimer;
    }

    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            _playerVector3 = _playerGameObject.transform.position;
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
        chicken._targetPosition = _playerVector3;
        yield return StartCoroutine(NewPlayerPosition());
        _playerVector3 = _playerGameObject.transform.position;
        chicken._targetPosition = _playerVector3;
    }

    private IEnumerator NewPlayerPosition()
    {
        yield return new WaitForSeconds(0.6f);
    }

    private Vector3 GetRandomCornerPosition()
    {
        float randomNum = Random.Range(0f, 100f);

        Vector3 edgePosition = Vector3.zero;

        if (randomNum <= 34)
        {
            // Canto  esquerdo
            edgePosition = new Vector3(0f, Random.Range(0f, Screen.height), 0f);
        }
        else if (randomNum >= 35 && randomNum <= 69)
        {
            // Canto  direito
            edgePosition = new Vector3(Screen.width, Random.Range(0f, Screen.height), 0f);
        }
        else if (randomNum >= 70 && randomNum <= 84)
        {
            // Canto inferior 
            edgePosition = new Vector3(Random.Range(0f, Screen.width), 0f, 0f);
        }
        else
        {
            // Canto superior
            edgePosition = new Vector3(0f, Random.Range(0f, Screen.height), 0f);
        }

        return edgePosition;
    }
}
