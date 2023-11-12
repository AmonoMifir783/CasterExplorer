using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour, IDataPersistence
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    public AudioClip[] jumpSounds;
    public AudioSource audioSource;
    private float lastJumpTime = 0f;
    private PlayerStamina playerStamina; // Reference to the PlayerStamina script
    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    [HideInInspector]
    public bool canMove = true;
    private bool isRunning = false;
    private float lastRunTime = 0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerStamina = GetComponent<PlayerStamina>(); // Get the PlayerStamina component
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Press Left Shift to run
        bool isRunningInput = Input.GetKey(KeyCode.LeftShift);

        if (isRunningInput && !isRunning && playerStamina.currentStamina > 0)
        {
            playerStamina.TakeFatigue(1);
            isRunning = true;
            lastRunTime = Time.time;
        }
        else if (!isRunningInput || playerStamina.currentStamina <= 0)
        {
            isRunning = false;
        }

        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded && Time.time - lastJumpTime >= 0.5f)
        {
            // Check if there is enough stamina for jumping
            if (playerStamina.currentStamina >= 15)
            {
                moveDirection.y = jumpSpeed;
                lastJumpTime = Time.time;
                playerStamina.TakeFatigue(15); // Consume stamina for jumping

                // Play a random jump sound
                if (jumpSounds.Length > 0 && audioSource != null)
                {
                    int randomIndex = UnityEngine.Random.Range(0, jumpSounds.Length);
                    audioSource.clip = jumpSounds[randomIndex];
                    audioSource.Play();
                }
            }
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            //rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            //rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            //playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            //transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        // Consume stamina while running
        if (isRunning && Time.time - lastRunTime >= 1f)
        {
            playerStamina.TakeFatigue(10);
            lastRunTime = Time.time;
        }
    }

    public void LoadData(GameData data)
    {
        this.transform.position = data.playerPosition;
    }

    public void SaveData(GameData data)
    {
        data.playerPosition = this.transform.position;
    }
}