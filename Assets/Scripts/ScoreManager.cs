using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private GameData _gameData;
    [SerializeField] private TMP_Text _scoreText;

    private void Start()
    {
        RefreshHighscore();
        _scoreText.text = _gameData.Score.ToString();
    }

    private void OnEnable()
    {
        Chicken.OnDisappear += AddScore;
    }

    private void OnDisable()
    {
        Chicken.OnDisappear -= AddScore;
    }

    private void RefreshHighscore()
    {
        if (_gameData.Score > _gameData.Highscore)
        {
            _gameData.Highscore = _gameData.Score;
        }

        _gameData.Score = 0;
    }

    private void AddScore()
    {
        _gameData.Score += 1;

        _scoreText.text = _gameData.Score.ToString();

        Debug.Log("Score");
    }
}
