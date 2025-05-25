using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace Weapons
{
    public class Axe : MonoBehaviour
    {
        [Header("Refrences")]
        [SerializeField] private Rigidbody rb;
        [SerializeField] private XRGrabInteractable _grabInteractable;
        [SerializeField] private ThrowableItem _throwableItem;
        public Transform _transformHand;

        [Space(10)] [Header("Properties")]
        [SerializeField] private float acceleration;
        [SerializeField] private float returnSpeed;
        
        private void Awake()
        {
            Assert.IsNotNull(rb);
            Assert.IsNotNull(_grabInteractable);
            Assert.IsNotNull(_throwableItem);
        }
        private void Start()
        {
            _throwableItem.TransitionToState(new IdleState());
        }
        private void OnEnable()
        {
            _grabInteractable.selectEntered.AddListener(OnSelectEntered); 
            _grabInteractable.activated.AddListener(OnActivated);
            
            _grabInteractable.selectExited.AddListener(OnSelectExited);
        }
        private void OnDisable()
        {
            _grabInteractable.selectEntered.RemoveListener(OnSelectEntered); 
            _grabInteractable.activated.RemoveListener(OnActivated);
            _grabInteractable.selectExited.RemoveListener(OnSelectExited);
            _grabInteractable.deactivated.RemoveListener(OnDeactivated);
        }
        private void OnSelectEntered(SelectEnterEventArgs arg0)
        {
            if (_throwableItem.isIdle)
            {
                this.gameObject.transform.SetParent(arg0.interactor.attachTransform);
                _throwableItem.TransitionToState(new HoldState());
            }
            else if (_throwableItem.isLanded)
            {
                this.gameObject.transform.SetParent(arg0.interactor.attachTransform);
                _throwableItem.TransitionToState(new ReturnState<Transform>(this.gameObject, _transformHand, returnSpeed)); 
            }
        }
        private void OnSelectExited(SelectExitEventArgs arg0)
        {
            if (_throwableItem.isHolding || _throwableItem.isReturned)
            {
                // _throwableItem.TransitionToState(new IdleState());
                _throwableItem.TransitionToState(new ThrowState<Rigidbody>(rb, acceleration));               
            }
        }
        /// <summary>
        /// Get the first contact point.
        /// Attach object to the hit surface at the contact point
        /// Move to contact point
        /// Parent to the object you hit
        /// </summary>
        /// <param name="other"></param>
        private void OnCollisionEnter(Collision other)
        {
            // if (!_throwableItem.isThrown) return;
            if (!_throwableItem.isLanded && !_throwableItem.isReturned)
            {
                if (other.gameObject.layer == LayerMask.NameToLayer("Building") || other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    ContactPoint contact = other.contacts[0];
                    Transform hitTransform = other.transform;
                    transform.position = contact.point; 
                    transform.SetParent(hitTransform);  
                    _throwableItem.TransitionToState(new LandedState<Rigidbody>(rb));
                }
            }
        }
        private void OnActivated(ActivateEventArgs arg0)
        {
            if (_throwableItem.isLanded)
            {
                this.gameObject.transform.SetParent(arg0.interactor.attachTransform);
                _throwableItem.TransitionToState(new ReturnState<Transform>(this.gameObject, _transformHand, returnSpeed)); 
            }
            // if (!_throwableItem.isHolding) return;
            // arg0.interactor.attachTransform.DetachChildren();
            // _throwableItem.TransitionToState(new ThrowState<Rigidbody>(rb, acceleration));
        }
        private void OnDeactivated(DeactivateEventArgs arg0)
        {
            // if( _throwableItem.isReturned)
            // {
            //     // _throwableItem.TransitionToState(new IdleState());
            //     _throwableItem.TransitionToState(new ThrowState<Rigidbody>(rb, acceleration));               
            // }
        }
    } 
}

