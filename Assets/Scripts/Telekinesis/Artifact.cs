using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.Interaction.Toolkit;

public class Artifact : MonoBehaviour
{
    [SerializeField] private ThrowableItem _throwableItem;
    [SerializeField] private XRGrabInteractable _grabInteractable;
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private FolatObjectManager floatObjects;
    [SerializeField] private ParticleSystem particles;
    private void Awake()
    {
        Assert.IsNotNull(_grabInteractable);
        Assert.IsNotNull(_throwableItem);
        Assert.IsNotNull(_inputHandler);
        Assert.IsNotNull(floatObjects);
        Assert.IsNotNull(particles);
    }
    private void Start()
    {
        _throwableItem.TransitionToState(new IdleState());
    }

    private void OnEnable()
    {
        _grabInteractable.selectEntered.AddListener(OnSelectEntered); 
        _grabInteractable.selectExited.AddListener(OnSelectExited);
        _grabInteractable.activated.AddListener(OnActivated);
        _grabInteractable.deactivated.AddListener(OnDeactivated);
    }

    private void Update()
    {
        if (_throwableItem.isLevitating)
        {
            if (_inputHandler.isMovingForwards)
            {
                foreach (var floatObj in floatObjects._floatObjects)
                {
                    floatObj.floatObjects.isMoving = true;
                    floatObj.floatObjects.isMovingForwards = true;
                    floatObj.floatObjects.isMovingBackwards = false;
                }
            }
        }
    }

    private void OnDisable()
    {
        _grabInteractable.selectEntered.RemoveListener(OnSelectEntered); 
        _grabInteractable.selectExited.RemoveListener(OnSelectExited);
        _grabInteractable.activated.RemoveListener(OnActivated);
        _grabInteractable.deactivated.RemoveListener(OnDeactivated);
    }
    private void OnActivated(ActivateEventArgs arg0)
    {
        particles.gameObject.SetActive(true);
        particles.Play();
    }
    private void OnDeactivated(DeactivateEventArgs arg0)
    {
        particles.Stop();
        particles.gameObject.SetActive(false);
    }
    private void OnSelectEntered(SelectEnterEventArgs arg0)
    {
        this.gameObject.transform.SetParent(arg0.interactor.attachTransform);
        _throwableItem.TransitionToState(new HoldState());
    }
    private void OnSelectExited(SelectExitEventArgs arg0)
    {
        _throwableItem.TransitionToState(new LandedState<Rigidbody>(this.GetComponent<Rigidbody>()));  
        this.gameObject.transform.SetParent(null);
    }
}
