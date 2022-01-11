using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private float sizeRatio=1;

    private Collider interactCollider;
    protected bool playerLooking;
    protected GameObject Player;

    private void Start()
    {
        interactCollider = DuplicateCollider();
    }

    public virtual void Interact()
    {

    }

    //check wich collider the object has and create its interact collider by duplicating it and ratioing its size to intended
    private Collider DuplicateCollider()
    {
        Collider existingCoillider = gameObject.GetComponent<Collider>();
        if (existingCoillider is BoxCollider)
        {
            BoxCollider tempCollider = gameObject.AddComponent<BoxCollider>();
            tempCollider.size = ((BoxCollider)existingCoillider).size* sizeRatio;
            tempCollider.center = ((BoxCollider)existingCoillider).center;
            tempCollider.isTrigger = true;
            return tempCollider;
        }
        else if (existingCoillider is SphereCollider)
        {
            SphereCollider tempCollider = gameObject.AddComponent<SphereCollider>();
            tempCollider.radius = ((SphereCollider)existingCoillider).radius * sizeRatio;
            tempCollider.center = ((SphereCollider)existingCoillider).center;
            tempCollider.isTrigger = true;
            return tempCollider;
        }
        else if (existingCoillider is CapsuleCollider)
        {
            CapsuleCollider tempCollider = gameObject.AddComponent<CapsuleCollider>();
            tempCollider.radius = ((CapsuleCollider)existingCoillider).radius * sizeRatio;
            tempCollider.height = ((CapsuleCollider)existingCoillider).height * sizeRatio;
            tempCollider.center = ((CapsuleCollider)existingCoillider).center;
            tempCollider.isTrigger = true;
            return tempCollider;
        }
        else
            return null;
    }
}
