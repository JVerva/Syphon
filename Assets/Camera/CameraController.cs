using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float maxDist;
    [SerializeField] private float minDist;
    [SerializeField] private Transform lookTarget;
    [SerializeField] private float scrollStepDist;
    [SerializeField] private float sensitivity;
    [SerializeField] private bool debug;

    public bool getInput;

    private float dist;
    private float mouseX;
    private float mouseY;
    private int layerMask;

    private void Start()
    {
        dist = -10;
        layerMask = 1<<3;
        layerMask = ~layerMask;
    }
    private void Update()
    {
        ChangeDist();
        //get mouse input and rotate camera appropriately
        if (getInput)
        {
            mouseX += Input.GetAxis("Mouse X") * sensitivity;
            mouseY += Input.GetAxis("Mouse Y") * sensitivity;
        }
        mouseY = Mathf.Clamp(mouseY, -80, 80);
        lookTarget.transform.rotation = Quaternion.Euler(-mouseY,mouseX , 0);
        if (debug)
            Debug.DrawLine(lookTarget.transform.position, -transform.forward*-dist+lookTarget.transform.position,Color.blue);
    }

    //change camera distance depending on scroll input
    private void ChangeDist()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        dist += scrollStepDist * scroll;
        dist = Mathf.Clamp(dist, -maxDist, -minDist);
        transform.localPosition = new Vector3(0, 0, CheckCollision());
    }

    //check if there are collisions between player and camera, puting camera at collision point 
    private float CheckCollision()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(lookTarget.transform.position, -transform.forward, out hitInfo, -dist, layerMask))
        {
            return -hitInfo.distance;
        }
        return dist;
    }
}
