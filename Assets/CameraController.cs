using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float maxDist;
    [SerializeField] private float minDist;
    [SerializeField] private GameObject lookTarget;
    [SerializeField] private float stepDist;
    [SerializeField] private float sensitivity;
    private float mouseX;
    private float mouseY;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        ChangeDist();
        mouseX += Input.GetAxis("Mouse X");
        mouseY += Input.GetAxis("Mouse Y");
        lookTarget.transform.rotation = Quaternion.Euler(-mouseY*sensitivity, mouseX*sensitivity, 0);
    }

    void ChangeDist()
    {
        float scroll=0;
        scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll >= 0 && transform.localPosition.z < -minDist|| scroll <= 0 && transform.localPosition.z > -maxDist)
            transform.localPosition += new Vector3(0, 0, stepDist * scroll);
        if(transform.localPosition.z > -minDist)
            transform.localPosition = new Vector3(0, 0, -minDist);
        if (transform.localPosition.z < -maxDist)
            transform.localPosition = new Vector3(0, 0, -maxDist);
    }
}
