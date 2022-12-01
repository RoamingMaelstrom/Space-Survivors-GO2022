using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using SOEvents;


public class BasePlayerController : MonoBehaviour
{
    [SerializeField] BoolSOEvent pauseEvent;
    [SerializeField] GameObject toggleActiveGameObjectOnPasue;
    [SerializeField] public float playerThrust = 250.0f;
    [SerializeField] public float playerTorque = 4.0f;
    [SerializeField] public float maxAngularVelocity = 180.0f;
    private Vector2 movement;
    private Vector2 mousePos;
    private Vector2 worldPos;
    private float isFiring;
    private bool isPausing;

    [SerializeField] Rigidbody2D playerBody;
    [SerializeField] [Range(0f, 1f)] float slowdownRate = 0.025f;

    void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
        if (movement.y < 0) movement.y *= 0.65f;
        movement.x *= 0.65f;
    }

    void OnLook(InputValue value)
    {
        mousePos = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        isFiring = value.Get<float>();
    }

    void OnPause(InputValue value)
    {
        if (value.Get<float>() == 0) return;
        if (isPausing) 
        {
            toggleActiveGameObjectOnPasue.SetActive(false);
            pauseEvent.Invoke(false);
        }
        else 
        {
            pauseEvent.Invoke(true);
            toggleActiveGameObjectOnPasue.SetActive(true);
        }

        isPausing = !isPausing;
    }

    public void FixedUpdate() 
    {
        if (!playerBody) return;
        PlayerMovement();
        PlayerRotation();

        playerBody.GetComponent<Rigidbody2D>().velocity = Vector2.Lerp(playerBody.GetComponent<Rigidbody2D>().velocity, Vector2.zero, slowdownRate);
    }

    void PlayerMovement(){
        Vector2 force = movement * playerThrust * Time.fixedDeltaTime;
        playerBody.AddRelativeForce(force);
    }

    public void PlayerRotation(){
        worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        float currentRotation = playerBody.transform.rotation.eulerAngles.z;

        float targetRotation = - Mathf.Atan2(worldPos.x - playerBody.transform.position.x,
         worldPos.y - playerBody.transform.position.y) * Mathf.Rad2Deg;
        
        if (targetRotation < 0) targetRotation += 360;

        // This is needed for making more precise movements. If not included, player can oscillate when currentRotation ~= targetRotation.
        float tempTorque = playerTorque;
        if (Mathf.Abs(targetRotation - currentRotation) < maxAngularVelocity * Time.fixedDeltaTime) tempTorque /= (maxAngularVelocity * Time.fixedDeltaTime);
        if (Mathf.Abs(targetRotation - currentRotation) < 2) tempTorque /= 2;
        if (Mathf.Abs(targetRotation - currentRotation) < 1) tempTorque /= 2;

        if (currentRotation < targetRotation && targetRotation - currentRotation > 180) playerBody.AddTorque(- tempTorque);
        else if (currentRotation > targetRotation && currentRotation - targetRotation > 180) playerBody.AddTorque(tempTorque);
        else if (currentRotation < targetRotation && targetRotation - currentRotation < 180) playerBody.AddTorque(tempTorque);
        else if (currentRotation > targetRotation && currentRotation - targetRotation < 180) playerBody.AddTorque(- tempTorque);
        

        playerBody.angularVelocity = Mathf.Max(-maxAngularVelocity, Mathf.Min(maxAngularVelocity, playerBody.angularVelocity));

        // This is needed to reduce the player wobbling when currentRotation ~= targetRotation.
        if (8 > Mathf.Abs(currentRotation - targetRotation)) playerBody.angularVelocity = Mathf.Lerp(playerBody.angularVelocity, 0, 0.2f);
        if (4 > Mathf.Abs(currentRotation - targetRotation)) playerBody.angularVelocity = Mathf.Lerp(playerBody.angularVelocity, 0, 0.4f);
        if (2 > Mathf.Abs(currentRotation - targetRotation)) playerBody.angularVelocity = Mathf.Lerp(playerBody.angularVelocity, 0, 0.6f);
        if (1 > Mathf.Abs(currentRotation - targetRotation)) playerBody.angularVelocity = Mathf.Lerp(playerBody.angularVelocity, 0, 0.8f);
    }

    
}
