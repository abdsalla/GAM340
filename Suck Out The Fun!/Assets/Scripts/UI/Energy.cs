using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class UnityFloatEvent : UnityEvent<float> { }

public class Energy : MonoBehaviour
{
    private GameManager instance;

    public WeightedDrops[] itemDrops;
    [SerializeField] private AudioClip grunt;
    [SerializeField] private float[] cdfArray;
    [SerializeField] private Vector3 itemDropOffset;

    [Header("Energy Stats")]
    public float regenRate;

    [Header("UI Values")]
    public float maxHealth;
    public float maxStamina;
    private float _currentHealth;
    private float _currentStamina;

    [Header("UI Visuals")]
    public Image health;
    public Image stamina;

    // UI Event for health, stamina and death
    public UnityFloatEvent OnHealthChange = new UnityFloatEvent();
    public UnityFloatEvent OnStaminaChange = new UnityFloatEvent();
    public UnityEvent OnDeath = new UnityEvent();

    void Awake()
    {
        instance = GameManager.Instance;
    }

    void Start()
    {
        _currentHealth = maxHealth;
        CalculateHealth();
        _currentStamina = maxStamina;
    }

    public float CurrentHealth
    {
        get { return _currentHealth; }
        set
        {
            _currentHealth = value;
            OnHealthChange.Invoke(_currentHealth);
            _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);
            CalculateHealth();
            if (_currentHealth <= 0)
            {
                OnDeath.Invoke();
            }
        }
    }

    public float CurrentStamina
    {
        get { return _currentStamina; }
        set
        {
            _currentStamina = value;
            OnStaminaChange.Invoke(_currentStamina);
            _currentStamina = Mathf.Clamp(_currentStamina, 0, maxStamina);
        }  
    }

    public void CalculateHealth() // Determines health color range off of unit's health percentage
    {
        float healthPercent = (100f / maxHealth) * _currentHealth;
        
        if (healthPercent <= 30f) { health.color = Color.red; }
        else if (healthPercent <= 70f && healthPercent > 30f) { health.color = new Color(255, 165, 0); }
        else if (healthPercent > 70f) { health.color = Color.green; }
    }

    Object DropChance()
    {
        List<int> CDFArray = new List<int>();

        int density = 0;

        for (int i = 0; i < itemDrops.Length; i++)
        {
            density += itemDrops[i].chance;
            CDFArray.Add(density);
        }

        int randonNumber = Random.Range(0, density);

        int selection = System.Array.BinarySearch(CDFArray.ToArray(), randonNumber);
        if (selection < 0) selection = ~selection;
        Debug.Log("selection: " + selection);
        return itemDrops[selection].value; 
    }

    void DropItem()
    {       
        Object itemDrop = Instantiate(DropChance(), transform.position + itemDropOffset, Quaternion.identity);
    }

    IEnumerator GruntEnemy()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.PlayOneShot(grunt);
        yield return new WaitForSeconds(grunt.length);
        Destroy(gameObject, 2.0f);
    }

    IEnumerator Grunt()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.PlayOneShot(grunt);
        yield return new WaitForSeconds(grunt.length);
    }

    public void DestroySelf() // Handles death and increments/decrements of player counters
    {
        Pawn unitCheck = GetComponent<Pawn>();

        if (unitCheck != null)
        {
            if (unitCheck.agent == null)
            {
                instance.lives -= 1;
                instance.score -= 5;
                StartCoroutine(Grunt());
                Destroy(gameObject);
                if (instance.lives <= 0) instance.Loss();
                StartCoroutine(instance.PlayerRespawn());
                StopCoroutine(instance.PlayerRespawn());
            }
            else if (unitCheck.agent != null)
            {
                instance.score += 10;
                StartCoroutine(GruntEnemy());
                DropItem();
                if (instance.score >= 150) { instance.Victory(); }

                for (int i = instance.activeEnemies; i >= instance.allowedEnemies; i--)
                {
                    Destroy(gameObject, 2.0f);
                    instance.activeEnemies -= 1;
                    if (instance.activeEnemies < instance.allowedEnemies)
                    {
                        instance.EnemyRespawn();
                        instance.activeEnemies += 1;
                    }
                }
                
            }
        }
    }
}