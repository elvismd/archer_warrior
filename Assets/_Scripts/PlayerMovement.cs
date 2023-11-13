using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private CharacterController characterController;

    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private float speedRate = 10.0f;

    private float _speed;
    private float _animationBlend;

    public float AnimationBlend => _animationBlend;

    void Start()
    {

    }

    void FixedUpdate()
    {
        Vector3 move = Vector3.zero;
        move.x = inputManager.Move.x * -1;

        // a reference to the players current horizontal velocity
        float currentVelocity = new Vector3(characterController.velocity.x, 0.0f, characterController.velocity.z).magnitude;

        const float speedOffset = 0.001f;
        float inputMagnitude = move.magnitude;
        inputMagnitude = Mathf.Clamp(inputMagnitude, 0f, 1f);

        // accelerate or decelerate to target speed
        if (currentVelocity < movementSpeed - speedOffset || currentVelocity > movementSpeed + speedOffset)
        {
            _speed = Mathf.Lerp(currentVelocity, movementSpeed * inputMagnitude, Time.deltaTime * speedRate);
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
        {
            _speed = movementSpeed;
        }
        _animationBlend = Mathf.Lerp(_animationBlend, move.x, Time.deltaTime * speedRate);

        characterController.Move(move.normalized * _speed * Time.deltaTime + (Vector3.down * 3f));
        Debug.Log(move);

    }
}
