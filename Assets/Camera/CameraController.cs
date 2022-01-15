using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float maxDist;
    [SerializeField] private float minDist;
    public Vector2 offset;
    [SerializeField] private Transform lookTarget;
    [SerializeField] private Transform pivot;
    [SerializeField] private float scrollStepDist;
    [SerializeField] private float sensitivity;
    [SerializeField] private float moveAccelaration;
    [SerializeField] private bool debug;

    public bool getInput;

    public float dist;
    private float mouseX;
    private float mouseY;
    private int layerMask;

    private void OnValidate()
    {
        lookTarget.transform.localPosition = new Vector3(offset.x, offset.y, 0);
    }

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
        pivot.transform.rotation = Quaternion.Euler(0, mouseX, 0);
        lookTarget.transform.rotation = Quaternion.Euler(-mouseY,mouseX , 0);
        lookTarget.transform.localPosition = Vector3.Lerp(lookTarget.transform.localPosition, new Vector3(offset.x, offset.y, 0),moveAccelaration*Time.deltaTime);
        if (debug)
            Debug.DrawLine(lookTarget.transform.position, -transform.forward*-dist+lookTarget.transform.position,Color.blue);
    }

    //change camera distance depending on scroll input
    private void ChangeDist()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        dist += scrollStepDist * scroll;
        dist = Mathf.Clamp(dist, -maxDist, -minDist);
        transform.localPosition = Vector3.Lerp(transform.localPosition,new Vector3(0, 0, dist),moveAccelaration*Time.deltaTime);
    }

    //check if there are collisions between player and camera, puting camera at collision point 
    private void CheckCollision()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(lookTarget.transform.position, -transform.forward, out hitInfo, -dist, layerMask))
        {
            transform.localPosition = new Vector3(0, 0, -hitInfo.distance);
        }
    }
}
