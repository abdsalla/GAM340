using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Pawn))]

public class AIController : Controller
{
    public Transform target; // player

    [SerializeField] private NavMeshAgent agent;

    public override void Start()
    {
        agent = pawn.GetComponent<NavMeshAgent>();
    }

    public override void Update() 
    {
        pawn.agent.SetDestination(target.position);  // Set the AI's destination as the player
        Vector3 desiredVelocity = agent.desiredVelocity;
        Vector3 desiredMovement = Vector3.MoveTowards(desiredVelocity, agent.desiredVelocity, agent.acceleration * Time.deltaTime);
        desiredMovement = pawn.transform.InverseTransformDirection(desiredMovement); // switching movement for the AI's animator
        pawn.Move(desiredMovement);
    }
}