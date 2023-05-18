using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private GameObject _menuScreen;
    [SerializeField] private GameData _gameData;

    void Start()
    {
        Time.timeScale = 0;
        _startButton.onClick.AddListener(HandleStartButton);
        _gameData.ChickenSpeed = 4;
    }

    private void Update()
    {
        IncreaseDifficulty();
    }

    private void HandleStartButton()
    {
        Time.timeScale = 1;
        _menuScreen.SetActive(false);
    }

    private void IncreaseDifficulty()
    {
        _gameData.ChickenSpeed += 0.02f * Time.deltaTime;
        _gameData.SpawnTime -= 0.006f * Time.deltaTime;
    }
}
