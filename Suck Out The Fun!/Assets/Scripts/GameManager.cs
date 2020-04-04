using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return instance; } }             // Static getter of God Object
    private static GameManager instance;                                        // God Object

    [Header("Player")]
    public GameObject player; // prefab
    public GameObject currentPlayer; // active player clone of prefab
    public float score; // need 150 to win
    public int lives = 3; // player starts with 3 lives

    [Header("AI")]
    public GameObject aI; // enemy prefab
    public int activeEnemies = 0;
    public int allowedEnemies = 4;

    [Header("UI")]
    public UIManager UI;
    public SlotManager inventory;

    [Header("Spawn Locations")]
    public Vector3 playerSpawnPoint;
    public Transform[] enemySpawnPoints;

    [Header("Scene Progression")]
    public SceneLoader sceneLoader; // scene handler
    public Scene activeScene => SceneManager.GetActiveScene(); // current scene
    public bool isPaused; // used to pause things that aren't driven by Time. anything

    [Header("Resources")]
    public Rifle[] weapons;


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
    }

    void OnEnable() { SceneManager.sceneLoaded += OnSceneLoaded; }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && Time.timeScale == 1) { Pause(); } 
        else if (Input.GetKeyDown(KeyCode.P) && Time.timeScale == 0) { UnPause(); }
    }

    public void PlayerSpawn() // Spawns Player at the given spawn point if there is no active Player in the scene
    {
        if (!currentPlayer)
        {
            currentPlayer = Instantiate(player);
        }
        currentPlayer.transform.position = playerSpawnPoint;
        inventory = currentPlayer.GetComponentInChildren<SlotManager>();
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

        if (pawn.agent == null)
        {
            inventory.activeSlot = new Slot();
            inventory.activeSlot = inventory.slots[0];
            pawn.EquipWeapon(weapons[0]);         
        }
        else if (pawn.agent != null) { pawn.EquipWeapon(weapons[Random.Range(0, weapons.Length)]); }
    }
    
    public IEnumerator PlayerRespawn() // Spawn used when Player is killed
    {
        float countDown = 5f;
        CapsuleCollider playerHitBox = currentPlayer.GetComponent<CapsuleCollider>();
        GameObject newPlayer = Instantiate(player);
        currentPlayer = newPlayer;
        currentPlayer.transform.position = playerSpawnPoint;
        AssignWeapon(currentPlayer);

        while (countDown > 0) // tiny invincibility when Pla 
        {
            yield return new WaitForSeconds(1.0f);
            countDown--;
            if (countDown == 0) playerHitBox.isTrigger = true;
        }
        playerHitBox.isTrigger = false;
    }

    public void Pause() // Set time functions to a halt by multplying them by zero and using isPaused
    {
        isPaused = true;
        Time.timeScale = 0;
        instance.UI.resume.gameObject.SetActive(true);
        instance.UI.mainMenu.gameObject.SetActive(true);
    }

    public void UnPause() // Undo Pause
    {
        isPaused = false;
        Time.timeScale = 1.0f;
        instance.UI.resume.gameObject.SetActive(false);
        instance.UI.mainMenu.gameObject.SetActive(false);
    }

    public void Victory() { sceneLoader.RunWinScreen(); }

    public void Loss() { sceneLoader.RunMainMenu(); }
    
    void LoadReferences()
    {
        GameObject UImanager = GameObject.FindWithTag("UIManager");
        GameObject spawnParent = GameObject.FindWithTag("spawnList");
        enemySpawnPoints = spawnParent.GetComponentsInChildren<Transform>();
        UI = UImanager.GetComponent<UIManager>();
        UI.resume.gameObject.SetActive(false);
        UI.mainMenu.gameObject.SetActive(false);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode Mode)
    {
        if (scene.buildIndex == 1)
        {
            LoadReferences();
            PlayerSpawn();
            EnemySpawn();
        }
    }   
}