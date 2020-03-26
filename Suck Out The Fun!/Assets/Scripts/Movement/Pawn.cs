using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class Pawn : MonoBehaviour
{
    public UnityEvent OnUse;
    public UnityEvent OnExit;
    public Weapon weapon;
    public Animator anim;
    public NavMeshAgent agent;
    public float speed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Transform weaponSpawnPosition;


    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Move(Vector3 direction) // Receives controller input
    {
        direction = transform.InverseTransformDirection(direction); // World to local for animator
        direction = Vector3.ClampMagnitude(direction, 1);

        // passing animator the speed and direction
        anim.SetFloat("Horizontal", direction.x * speed);
        anim.SetFloat("Vertical", direction.z * speed);
    }

    public void EquipWeapon(Weapon newWeapon)
    {
        if (weapon != null) { UnequipWeapon(); } // if we already have a weapon equipped the remove it for the new one

        // Create the weapon object and set it's transform to the Player's weaponSpawnPosition 
        GameObject weaponObj = Instantiate(newWeapon.gameObject, weaponSpawnPosition.position, weaponSpawnPosition.rotation) as GameObject;
        weaponObj.transform.parent = weaponSpawnPosition;
        weapon = weaponObj.GetComponent<Weapon>();

        if (newWeapon.gameObject.tag == "AutoW") { weapon.gameObject.transform.Rotate(0, 90.0f, 0, Space.Self); }
            
        OnUse.AddListener(weapon.OnUse);
        OnExit.AddListener(weapon.OnExit);    
    }

    public void UnequipWeapon()
    {
        if (weapon != null)
        {
            Destroy(weapon.gameObject);
            weapon = null;

            OnUse.RemoveListener(weapon.OnUse);
            OnExit.RemoveListener(weapon.OnExit);
        }
    }

    void OnAnimatorIK()
    {
        if (weapon == null) // Release the Player from their imaginary weapon burden
        {
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.0f);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0.0f);
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 0.0f);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0.0f);
            return;
        }

        if (weapon.RightHandPoint != null) 
        {
            anim.SetIKPosition(AvatarIKGoal.RightHand, weapon.RightHandPoint.position);
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
            anim.SetIKRotation(AvatarIKGoal.RightHand, weapon.RightHandPoint.rotation);
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 0.0f);
        }
        else
        {
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.0f);
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 0.0f);
        }

        if (weapon.LeftHandPoint != null)
        {
            anim.SetIKPosition(AvatarIKGoal.LeftHand, weapon.LeftHandPoint.position);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
            anim.SetIKRotation(AvatarIKGoal.LeftHand, weapon.LeftHandPoint.rotation);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0.0f);
        }
        else
        {
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0.0f);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0.0f);
        }
    }

    /*void OnAnimatorMove()
    {
        if (agent != null) agent.velocity = anim.velocity;
        else if (agent == null) { }
    }*/
}