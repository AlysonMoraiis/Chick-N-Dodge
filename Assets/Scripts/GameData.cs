using UnityEngine;

[CreateAssetMenu(fileName = "GameData")]
public class GameData : ScriptableObject
{
    public int Score;
    public int Highscore;
    public int DeathCounter;

    public float ChickenSpeed;
    public float SpawnTime;
}
