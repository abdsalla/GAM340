using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Pawn))]

public class AIController : Controller
{
    public Transform target;
    //public GameObject AIPawn;
    [SerializeField] private NavMeshAgent agent;

    public override void Start()
    {
        agent = pawn.GetComponent<NavMeshAgent>();
    }

    public override void Update()
    {
        pawn.agent.SetDestination(target.position);
        Vector3 desiredVelocity = agent.desiredVelocity;
        //actionTracker.unitEnergy = AIEnergy;
        //actionTracker.Set(gameObject);
        Vector3 desiredMovement = Vector3.MoveTowards(desiredVelocity, agent.desiredVelocity, agent.acceleration * Time.deltaTime);
        //desiredMovement = AIPawn.transform.InverseTransformDirection(desiredMovement);
        pawn.Move(desiredMovement);
    }
}