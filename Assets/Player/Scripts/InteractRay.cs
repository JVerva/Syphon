using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractRay : MonoBehaviour
{
    [SerializeField] private Transform LookTarget;
    [SerializeField] private bool debug;
    [SerializeField] private Text interactionText;
    private PlayerInputManager playerInputManager;
    private RaycastHit hitInfo;
    private Interactable interactable;
    private PlayerStats playerStats;

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
            if(interactable)
                interactionText.text = "[" + playerInputManager.Interact +"] " + interactable.interactionText;
            else
                interactionText.text = null;
        }
        else
        {
            //if we are not hitting anything set interactable to null
            interactable = null;
            interactionText.text = null;
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
