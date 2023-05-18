using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private GameData _gameData;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _highscoreText;

    private void Start()
    {
        RefreshHighscore();
        _scoreText.text = _gameData.Score.ToString();
    }

    private void OnEnable()
    {
        Chicken.OnDisappear += AddScore;
        PlayerCollisions.OnDeath += SaveHighscore;
    }

    private void OnDisable()
    {
        Chicken.OnDisappear -= AddScore;
        PlayerCollisions.OnDeath -= SaveHighscore;
    }

    private void SaveHighscore()
    {
        if (_gameData.Score > _gameData.Highscore)
        {
            _gameData.Highscore = _gameData.Score;
            PlayerPrefs.SetInt("highscore", _gameData.Highscore);
        }
        Debug.Log("Playerprefs highscore saved: " + PlayerPrefs.GetInt("highscore"));
    }

    private void RefreshHighscore()
    {
        _gameData.Highscore = PlayerPrefs.GetInt("highscore");
        _highscoreText.text = "Highscore\n" + _gameData.Highscore.ToString();
        _gameData.Score = 0;
    }

    private void AddScore()
    {
        _gameData.Score += 1;

        _scoreText.text = _gameData.Score.ToString();

        Debug.Log("Score");
    }
}
