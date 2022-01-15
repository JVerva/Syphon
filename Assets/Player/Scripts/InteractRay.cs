using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractRay : MonoBehaviour
{
    [SerializeField] private Transform LookTarget;
    [SerializeField] private bool debug;
    PlayerInputManager playerInputManager;
    RaycastHit hitInfo;
    Interactable interactable;
    PlayerStats playerStats;

    private void Awake()
    {
        //set default values
        playerStats = FindObjectOfType<PlayerStats>();
        playerInputManager = FindObjectOfType<PlayerInputManager>();
        //get interact key event
        playerInputManager.interact += Interact;
    }

    private void FixedUpdate()
    {
        //check if we are looking at an interactable in reach
        if (Physics.Raycast(LookTarget.transform.position, gameObject.transform.forward, out hitInfo, playerStats.interactionReach))
        {
            //if so, get it
            interactable = hitInfo.collider.gameObject.GetComponent<Interactable>();
        }
        else
        {
            //if we are not hitting anything set interactable to null
            interactable = null;
        }
        if (debug)
            Debug.DrawLine(LookTarget.transform.position, LookTarget.transform.position + gameObject.transform.forward * playerStats.interactionReach, Color.cyan);
    }

    private void Interact()
    {
        if (interactable!=null)
        {
            interactable.Interact();
        }
    }
}
