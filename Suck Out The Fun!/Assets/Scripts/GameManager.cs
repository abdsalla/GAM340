using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return instance; } }             // Static getter of God Object
    private static GameManager instance;                                        // God Object

    [Header("Player")]
    public GameObject player;
    public GameObject currentPlayer;
    public float score;
    public int lives = 3;

    [Header("AI")]
    public GameObject aiPawn;

    [Header("UI")]
    public UIManager UI;

    [Header("Spawn Locations")]
    public Vector3 playerSpawnPoint;
    public Transform[] enemySpawnPoints;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        PlayerSpawn();
        EnemySpawn();
    }

    public void PlayerSpawn() // Spawns Player at the given spawn point if there is no active Player in the scene
    {
        if (!currentPlayer) currentPlayer = Instantiate(player);
        currentPlayer.transform.position = playerSpawnPoint;
    }

    void EnemySpawn()
    {
        for (int i = 0; i < enemySpawnPoints.Length; i++)
        {
            Instantiate(aiPawn, enemySpawnPoints[i]);
        }   
    }

    public void EnemyRespawn() { Instantiate(aiPawn,enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)]); }
}