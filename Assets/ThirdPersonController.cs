using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;
    private Rigidbody rb;
    private Camera mainCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main; // Cache the main camera
    }

    void FixedUpdate()
    {
        // Get input from keyboard
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Get camera forward and right vectors (ignoring Y axis)
        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Create camera-relative movement vector
        Vector3 move = (cameraForward * moveZ + cameraRight * moveX) * moveSpeed * Time.fixedDeltaTime;

        // Apply movement using Rigidbody
        rb.MovePosition(rb.position + move);

        // Rotate the player to face the direction of movement
        if (move != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }
}