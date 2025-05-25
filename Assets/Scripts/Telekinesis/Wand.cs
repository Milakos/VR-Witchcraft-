using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.Interaction.Toolkit;
using CartoonFX;
public class Wand : MonoBehaviour
{
    [SerializeField] private ThrowableItem _throwableItem;
    [SerializeField] private XRGrabInteractable _grabInteractable;
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private FolatObjectManager floatObjects;
    [SerializeField] private ParticleSystem particles;
    public CFXR_Effect cfxr;
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
    void Update()
    {
        if (_throwableItem.isLevitating)
        {
            foreach (var floatObj in floatObjects._floatObjects)
            {
                floatObj.isFloating = true;
                floatObj.floatObjects.isLevitating = true;
            }

            if (_inputHandler.isMovingBackwards)
            {
                foreach (var floatObj in floatObjects._floatObjects)
                {
                    floatObj.floatObjects.isMoving = true;
                    floatObj.floatObjects.isMovingBackwards = true;
                    floatObj.floatObjects.isMovingForwards = false;
                    print("MovingToMe");
                }
            }
            // else if (_inputHandler.isMovingForwards)
            // {
            //     foreach (var floatObj in floatObjects._floatObjects)
            //     {
            //         floatObj.floatObjects.isMoving = true;
            //         floatObj.floatObjects.isMovingForwards = true;
            //         floatObj.floatObjects.isMovingBackwards = false;
            //     }
            // }
            // else
            // {
            //     foreach (var floatObj in floatObjects._floatObjects)
            //     {
            //         floatObj.floatObjects.isMoving = false;
            //         floatObj.floatObjects.isMovingForwards = false;
            //         floatObj.floatObjects.isMovingBackwards = false;
            //     }
            // }
        }
        else
        {
            foreach (var floatObj in floatObjects._floatObjects)
            {
                floatObj.isFloating = false;
                floatObj.floatObjects.isLevitating = false;
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
        if (_throwableItem.isHolding)
        {
            _throwableItem.TransitionToState(new LevitateState());
            particles.gameObject.SetActive(true);
            particles.Play();
            cfxr.cameraShake.enabled = true;
        }
    }
    private void OnDeactivated(DeactivateEventArgs arg0)
    {
        if (_throwableItem.isLevitating)
        {
            _throwableItem.TransitionToState(new HoldState());
            particles.Stop();
            particles.gameObject.SetActive(false);
        }
        
    }
    private void OnSelectEntered(SelectEnterEventArgs arg0)
    {
        if (_throwableItem.isIdle || _throwableItem.isLanded)
        {
            this.gameObject.transform.SetParent(arg0.interactor.attachTransform);
            _throwableItem.TransitionToState(new HoldState());
        }
    }
    private void OnSelectExited(SelectExitEventArgs arg0)
    {
        if (_throwableItem.isHolding )
        {
            _throwableItem.TransitionToState(new LandedState<Rigidbody>(this.GetComponent<Rigidbody>()));  
            this.gameObject.transform.SetParent(null);
        }
    }
}
