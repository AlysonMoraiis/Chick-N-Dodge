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
        PlayerCollisions.OnDeath += DeathCount;
        _gameData.ChickenSpeed = 3.5f;
        CheckCanPlayAd();
    }

    private void OnDisable()
    {
        PlayerCollisions.OnDeath -= DeathCount;
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

    private void DeathCount()
    {
        _gameData.DeathCounter += 1;
    }

    private void CheckCanPlayAd()
    {
        if (_gameData.DeathCounter >= 3)
        {
            _gameData.DeathCounter = 0;
            AdmobManager.Instance.LoadInterstitialAd();
            //play ad
        }
    }

    private void IncreaseDifficulty()
    {
        _gameData.ChickenSpeed += 0.02f * Time.deltaTime;
        _gameData.SpawnTime -= 0.006f * Time.deltaTime;
    }
}
