using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]

public class CameraControls : MonoBehaviour
{
    public float targetRotateSpeed;  

    private GameManager instance;

    [SerializeField] private Vector3 offset; 
    [SerializeField] private GameObject target;  // Player's position
    [SerializeField] private Camera theCamera;  // Player's camera

    void OnEnable()
    {
        instance = GameManager.Instance;
    }

    void Start()
    {
        theCamera = GetComponent<Camera>();
        target = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        RotateToMousePosition(); // Rotate the PLayer to look at the direction the mouse is at
        FollowTarget(); // Follow the camera's target using MoveTowards
    }

    void RotateToMousePosition()
    {
        Plane groundPlane;
        groundPlane = new Plane(Vector3.up, target.transform.position);

        float distance;
        Ray theRay = theCamera.ScreenPointToRay(Input.mousePosition); // how far down theRay is the intersection at

        if (groundPlane.Raycast(theRay, out distance))
        {
            Vector3 intersectionPoint = theRay.GetPoint(distance); // Find world point of intersection
            Quaternion targetRotation;
            Vector3 lookVector = intersectionPoint - target.transform.position; // Goal minus start
            targetRotation = Quaternion.LookRotation(lookVector, Vector3.up);
            target.transform.rotation = Quaternion.RotateTowards(target.transform.rotation, targetRotation, targetRotateSpeed * Time.deltaTime);
        }
    }

    void FollowTarget()
    {
        Vector3 targetPosition = target.transform.position + offset; // we don't want to follow the target exactly since the camera
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, targetRotateSpeed * Time.deltaTime);
    }
}