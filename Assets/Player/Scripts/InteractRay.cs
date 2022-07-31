using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InteractRay : MonoBehaviour
{
    [SerializeField] private Transform _lookTarget;
    [SerializeField] private bool _debug;
    [SerializeField] private Text _interactionText;
    private PlayerInput _playerInput;
    private RaycastHit _hitInfo;
    private Interactable _interactable;

    private void Awake()
    {
        //set default values
        _playerInput = new PlayerInput();
        //get interact key event
        _playerInput.Misc.Interact.started += Interact;
    }

    private void FixedUpdate()
    {
        //check if we are looking at an interactable in reach
        if (Physics.Raycast(_lookTarget.transform.position, gameObject.transform.forward, out _hitInfo, PlayerStats.interactionReach))
        {
            //if so, get it
            _interactable = _hitInfo.collider.gameObject.GetComponent<Interactable>();
            if(_interactable)
                _interactionText.text = "[" + _playerInput.Misc.Interact.GetBindingDisplayString() +"] " + _interactable.interactionText;
            else
                _interactionText.text = null;
        }
        else
        {
            //if we are not hitting anything set interactable to null
            _interactable = null;
            _interactionText.text = null;
        }
        if (_debug)
            Debug.DrawLine(_lookTarget.transform.position, _lookTarget.transform.position + gameObject.transform.forward * PlayerStats.interactionReach, Color.cyan);
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if (_interactable!=null)
        {
            _interactable.Interact();
        }
    }

    private void OnEnable(){
        _playerInput.Enable();
    }

    private void OnDisable(){
        _playerInput.Disable();
    }
}
