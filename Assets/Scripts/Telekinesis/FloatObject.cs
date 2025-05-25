using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Rendering;
public class FloatObject : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Collider col;
    [SerializeField] private ParticleSystem ps;
    [SerializeField] public bool isLevitating = false;
    [SerializeField] public bool isMoving = false;
    [SerializeField] public bool isMovingBackwards = false;
    [SerializeField] public bool isMovingForwards = false;
    float waveAmplitude => UnityEngine.Random.Range(0f, 1f);
    [SerializeField] float _speed;
    [SerializeField] float Move_speed;
    [SerializeField] private FolatObjectManager floatObjectManager;
    public Transform wandTransform; // The wand or the object to follow
    
    InputHandler inputHandler;
    private void Awake()
    {
        Assert.IsNotNull(rb);
        Assert.IsNotNull(col);
        Assert.IsNotNull(floatObjectManager);
        Assert.IsNotNull(ps);
        
        inputHandler = FindObjectOfType<InputHandler>();
        
    }

    void ParticleHandler(bool value)
    {
        ps.gameObject.SetActive(value);
        if (value)
        {
            ps.Play();
        }
        else
        {
            ps.Stop();
        }
    }

    private void FixedUpdate()
    {
        if (!inputHandler.BothHandsActivated)
        {
            if (isLevitating)
            {
                // Apply an upward force to simulate floating
                rb.useGravity = false;
                rb.velocity = new Vector3(rb.velocity.x, Mathf.Sin(Time.time * _speed) * waveAmplitude, rb.velocity.z);
                // this.gameObject.transform.SetParent(wandTransform, true);
                this.gameObject.transform.SetParent(null);
                ParticleHandler(true);
            }
            else
            {
                // Regular physics
                this.gameObject.transform.SetParent(null);
                rb.useGravity = true;
                ParticleHandler(false);
            }
            if (isMoving)
            {
                // Calculate the target position with the offset
                Vector3 targetPosition = wandTransform.position + floatObjectManager.offset;
            
                if (isMovingForwards)
                {
                    this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, 
                        this.gameObject.transform.position - floatObjectManager.offset * 2f, Time.deltaTime * Move_speed);
                    print("isMovingForwards");
                }
                else if (isMovingBackwards)
                {
                    this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, 
                        targetPosition, Time.deltaTime * Move_speed);
                    print("isMovingBackwards");
                }
            }
        }
        else
        {
            rb.useGravity = false;
            rb.velocity = new Vector3(rb.velocity.x, Mathf.Sin(Time.time * _speed) * waveAmplitude, rb.velocity.z);
            this.gameObject.transform.SetParent(wandTransform, true);
            ParticleHandler(true);
        }
    }
    

}
