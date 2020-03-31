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
    public GameObject aI;
    public int activeEnemies = 0;
    public int allowedEnemies = 4;

    [Header("UI")]
    public UIManager UI;

    [Header("Spawn Locations")]
    public Vector3 playerSpawnPoint;
    public Transform[] enemySpawnPoints;

    [Header("Resources")]
    public Weapon[] weapons;


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
        if (!currentPlayer)
        {
            currentPlayer = Instantiate(player);
        }
        currentPlayer.transform.position = playerSpawnPoint;
        AssignWeapon(currentPlayer);
    }

    void EnemySpawn() // Starting Enemy spawn only happens once
    {
        GameObject enemy;
        enemy = null;
        enemy = Instantiate(aI, enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)]);
        activeEnemies += 1;
        AssignWeapon(enemy);
        for (int i = activeEnemies; activeEnemies < allowedEnemies; i++)
        {
            enemy = Instantiate(aI, enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)]);
            activeEnemies += 1;
            AssignWeapon(enemy);
        }
    }

    public void EnemyRespawn() // Recurring Enemy spawn that gets exponentially higher the more you kill
    {
        GameObject enemy;
        enemy = null;

        if (activeEnemies <= allowedEnemies)
        {
            for (int i = activeEnemies; i < allowedEnemies; i++)
            {
                enemy = Instantiate(aI, enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)]);
                AssignWeapon(enemy);
                activeEnemies += 1;
            }
        }
        else if (activeEnemies > allowedEnemies) Debug.Log("Too Many active enemies");
    }

    void AssignWeapon(GameObject toAssign) // Assign weapon to the given GameObject's pawn
    {
        Pawn pawn = toAssign.GetComponent<Pawn>();
        pawn.EquipWeapon(weapons[Random.Range(0, weapons.Length)]);
    }
    
    public IEnumerator PlayerRespawn()
    {
        float countDown = 5f;
        CapsuleCollider playerHitBox = currentPlayer.GetComponent<CapsuleCollider>();
        GameObject newPlayer = Instantiate(player);
        currentPlayer = newPlayer;
        currentPlayer.transform.position = playerSpawnPoint;
        AssignWeapon(currentPlayer);
        Debug.Log("Player has respawned"); 

        while (countDown > 0)
        {
            yield return new WaitForSeconds(1.0f);
            countDown--;
            if (countDown == 0) playerHitBox.isTrigger = true;
        }
        playerHitBox.isTrigger = false;
    }
}