using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerCollisions : MonoBehaviour
{
    private SpriteRenderer _sprite;
    public static event Action OnDeath;


    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            StartCoroutine(GameOver());
        }
    }

    private IEnumerator GameOver()
    {
        Time.timeScale = 0;

        for (int i = 0; i < 5; i++)
        {
            _sprite.color = Color.clear;
            yield return new WaitForSecondsRealtime(0.1f);
            _sprite.color = Color.white;
            yield return new WaitForSecondsRealtime(0.1f);
        }

        OnDeath?.Invoke();

        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
