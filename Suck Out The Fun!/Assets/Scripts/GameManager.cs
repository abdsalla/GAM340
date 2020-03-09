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

    [Header("UI")]
    public UIManager UI;

    [Header("Spawn Locations")]
    public Vector3 playerSpawnPoint;

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
        UI.CheckNSet();
    }

    void PlayerSpawn() // Spawns Player at the given spawn point if there is no active Player in the scene
    {
        if (!currentPlayer) currentPlayer = Instantiate(player);
        currentPlayer.transform.position = playerSpawnPoint;
    }
}