using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Pawn))]

public class AIController : Controller
{
    public Transform target; // player

    private Coroutine traverseOffMeshLink;
    private GameManager instance;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] float closeEnoughSquared = .04f;

    public override void Start()
    {
        instance = GameManager.Instance;
        agent = pawn.GetComponent<NavMeshAgent>();      
    }

    public override void Update() 
    {
        Navigation();
        FireAtPlayer(); 
    }

    void Navigation()
    {
        target = instance.currentPlayer.transform;
        pawn.agent.SetDestination(target.position);  // Set the AI's destination as the player
        Vector3 desiredVelocity = agent.desiredVelocity;
        Vector3 desiredMovement = Vector3.MoveTowards(desiredVelocity, agent.desiredVelocity, agent.acceleration * Time.deltaTime);
        desiredMovement = pawn.transform.InverseTransformDirection(desiredMovement); // switching movement for the AI's animator
        pawn.Move(desiredMovement);
    }

    void FireAtPlayer()
    {
        PlayerController player = instance.currentPlayer.GetComponent<PlayerController>();
        Ray weaponAngle = new Ray(transform.position, transform.forward); // ai facing direction
        Vector3 pointToFireAt = player.playerFront.GetPoint(.02f); // player facing direction
        float distBetween = Vector3.Distance(weaponAngle.GetPoint(pawn.weapon.shotRange), pointToFireAt); 
        Debug.DrawLine(weaponAngle.GetPoint(pawn.weapon.shotRange), pointToFireAt, Color.blue);

        if (distBetween <= 2.5f) { pawn.weapon.OnUse(); } // if the distance between front of player and ai is close enoughthen fire
        else pawn.weapon.OnExit();
    }
}