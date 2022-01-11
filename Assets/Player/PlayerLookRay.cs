using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookRay : MonoBehaviour
{
    [SerializeField] private Transform LookTarget;
    [SerializeField] private bool debug;
    PlayerStats playerStats;

    private void Awake()
    {
        playerStats = FindObjectOfType<PlayerStats>();
    }

    private void FixedUpdate()
    {
        Physics.Raycast(LookTarget.transform.position, gameObject.transform.forward, playerStats.interactionReach);
        if (debug)
            Debug.DrawLine(LookTarget.transform.position, LookTarget.transform.position + gameObject.transform.forward* playerStats.interactionReach, Color.cyan);
    }
}
