using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public bool isActivating = false;
    public bool LeftisActivating = false;
    public bool BothHandsActivated = false;
    
    public float pressValue;
    public float pressValueLeft;
    [SerializeField] private InputActionProperty[] inputActionProperty;

    public Vector2 moveRightThumb;
    public Vector2 moveLeftThumb;
    
    public bool isMovingBackwards = false;
    public bool isMovingForwards = false;
    void OnEnable() { }
    private void OnDisable() { }
    private void Update()
    {
        pressValue = inputActionProperty[0].action.ReadValue<float>();
        isActivating =  pressValue > 0 ? true : false;
        
        moveRightThumb = inputActionProperty[1].action.ReadValue<Vector2>();
        isMovingBackwards = moveRightThumb.y != 0f && moveRightThumb.y < 0.5f;
        
        moveLeftThumb = inputActionProperty[2].action.ReadValue<Vector2>();
        isMovingForwards = moveLeftThumb.y != 0f && moveLeftThumb.y > 0.5f;
        
        pressValueLeft = inputActionProperty[3].action.ReadValue<float>();
        LeftisActivating =  pressValueLeft > 0 ? true : false;

        if (isActivating == true && LeftisActivating == true)
        {
            BothHandsActivated = true;
        }
        else
        {
            BothHandsActivated = false;
        }
    }

}
