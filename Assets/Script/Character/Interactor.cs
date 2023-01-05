using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask _interactableMask;

    private readonly Collider[] _colliders = new Collider[3];
    [SerializeField] private int _numFound;
    [SerializeField] private Player player;

    private IInteractable interactable;

    private void Update()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders,
        (int)_interactableMask);

        if (_numFound > 0)
        {
            interactable = _colliders[0].GetComponent<IInteractable>();
            interactable.Hover();
            if (interactable != null && Keyboard.current.eKey.wasPressedThisFrame)
            {
                interactable.Interact(player);
                interactable.VisualInteraction(player);
                //Debug.Log(player.GetInventory().Count);
            }
        } else
        {
            if (interactable != null)
            {
                try
                {
                    interactable.Unhover();
                }
                catch (MissingReferenceException)
                {
                    Debug.Log("objet détruit donc peut pas acceder");
                }
                interactable = null;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
        //Gizmos.DrawCube(_interactionPoint.position, new Vector3(1,0.02f,0.02f));
        //Gizmos.DrawLine(_interactionPoint.position, _interactionPointEnd.position);
    }

    public int getNumFound()
    {
        return _numFound;
    }

    public IInteractable getInteractableObject()
    {
        return _colliders[0].GetComponent<IInteractable>();
    }
}