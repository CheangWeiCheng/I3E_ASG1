/*
* Author: Cheang Wei Cheng
* Date: 14 June 2025
* Description:This script controls the player's behavior in the game.
* It handles player interactions with coins, keycards, and doors, as well as player health and score management.
* The player can collect coins, pick up keycards, and interact with doors to unlock them if they have a keycard.
* The player can also fire projectiles, recover health in healing areas, and take damage from hazards like lava and spikes.
* The script uses Unity's Input System for firing projectiles and handles raycasting to detect interactable objects in the game world.
* The player's score and health are displayed on the UI, and the player respawns at a designated spawn point upon death.
* The script also includes audio feedback for firing projectiles and interacting with objects.
*/

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    AudioSource fireAudioSource;
    public GameObject projectile;
    public Transform gunPoint;
    public float fireStrength = 5f;

    int maxHealth = 100;
    int currentHealth = 100;

    [SerializeField]
    TMP_Text scoreUI;
    [SerializeField]
    TMP_Text healthUI;
    [SerializeField]
    Image keycardUI;
    int currentScore = 0;
    bool canInteract = false;
    private bool hasKeycard = false;
    CoinBehaviour currentCoin = null;
    DoorBehaviour currentDoor = null;
    KeycardBehaviour currentKeycard = null;

    [SerializeField]
    float interactionDistance = 2f;

    public Transform spawnPoint;

    Camera mainCamera;

    void Start()
    {
        scoreUI.text = "SCORE: " + currentScore.ToString();
        healthUI.text = "HEALTH: " + currentHealth.ToString();
        keycardUI.enabled = false; // Hide the keycard image initially
        if (!mainCamera) mainCamera = Camera.main;
        fireAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        HandleRaycastHighlighting();
    }

    void OnJump(InputValue value)
    {
        GetComponent<ThirdPersonController>()?.Jump();
    }

    void OnInteract(InputValue value)
    {
        if (canInteract)
            if (currentCoin != null)
            {
                Debug.Log("Interacting with coin");
                currentCoin.Collect(this);
                currentCoin = null; // Reset current coin after interaction
            }
            else if (currentKeycard != null)
            {
                Debug.Log("Interacting with keycard");
                currentKeycard.Collect(this);
                hasKeycard = true;
                keycardUI.enabled = true; // Show the keycard image
                currentKeycard = null; // Reset current keycard after interaction
            }
            else if (currentDoor != null)
            {
                Debug.Log("Interacting with door");
                currentDoor.Interact(this);
            }
    }

    void OnFire(InputValue value)
    {
        if (fireAudioSource != null)
        {
            fireAudioSource.Play();
        }
        // Make player face the same direction as the camera
        Vector3 cameraForward = mainCamera.transform.forward;
        cameraForward.y = 0; // Keep the player upright (ignore camera pitch)
        transform.forward = cameraForward.normalized;

        var bullet = Instantiate(projectile, gunPoint.position, transform.rotation);
        var fireForce = transform.forward * fireStrength;
        var rigidBody = bullet.GetComponent<Rigidbody>();
        rigidBody.AddForce(fireForce);
        Destroy(bullet, 5f);
    }

    public bool HasKeycard()
    {
        return hasKeycard;
    }

    /// <summary>
    /// Handles raycasting to detect and highlight interactable objects in the game world.
    /// This method checks for collectibles (coins), keycards, and doors within a specified interaction distance.
    /// If an interactable object is detected, it highlights the object and allows interaction.
    /// If no interactable objects are detected, it resets the current objects and disables interaction.
    /// </summary>
    void HandleRaycastHighlighting()
    {
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hitinfo;

        if (Physics.Raycast(ray, out hitinfo, interactionDistance))
        {
            // Handle coin detection
            if (hitinfo.collider.CompareTag("Collectible"))
            {
                var newCoin = hitinfo.collider.GetComponent<CoinBehaviour>();
                if (currentCoin != newCoin)
                {
                    // Unhighlight the previous coin if it exists
                    if (currentCoin != null) currentCoin.Unhighlight();
                    currentCoin = newCoin;
                    currentCoin.Highlight();
                    currentDoor = null;
                    currentKeycard = null;
                    canInteract = true; // Enable interaction
                    Debug.Log("Coin detected");
                }
                return;
            }
            // Handle keycard detection
            else if (hitinfo.collider.CompareTag("Keycard"))
            {
                var newKeycard = hitinfo.collider.GetComponent<KeycardBehaviour>();
                if (currentKeycard != newKeycard)
                {
                    // Unhighlight the previous keycard if it exists
                    if (currentKeycard != null) currentKeycard.Unhighlight();
                    currentKeycard = newKeycard;
                    currentKeycard.Highlight();
                    currentCoin = null;
                    currentDoor = null;
                    canInteract = true; // Enable interaction
                    Debug.Log("Keycard detected");
                }
                return;
            }
            // Handle door detection
            else if (hitinfo.collider.CompareTag("Door"))
            {
                var newDoor = hitinfo.collider.GetComponent<DoorBehaviour>();
                var doorVisual = newDoor.GetComponentInChildren<DoorLockVisual>(); // Get from newDoor
                
                if (currentDoor != newDoor)
                {
                    // Unhighlight previous door if it exists
                    if (currentDoor != null)
                    {
                        var prevVisual = currentDoor.GetComponentInChildren<DoorLockVisual>();
                        if (prevVisual != null) prevVisual.Unhighlight();
                    }
                    
                    // Highlight new door
                    if (doorVisual != null) doorVisual.Highlight();
        
                    currentDoor = newDoor;
                    currentCoin = null;
                    currentKeycard = null;
                    canInteract = true; // Enable interaction
                    Debug.Log("Door detected");
                }
                return;
            }
        }

        // If no valid object is detected, reset current objects and disable interaction
        if (currentCoin != null)
        {
            currentCoin.Unhighlight();
            currentCoin = null;
        }
        if (currentKeycard != null)
        {
            currentKeycard.Unhighlight();
            currentKeycard = null;
        }
        if (currentDoor != null)
        {
            var doorVisual = currentDoor.GetComponentInChildren<DoorLockVisual>();
            if (doorVisual != null) doorVisual.Unhighlight();
            currentDoor = null;
        }
    }

    public void ModifyScore(int amount)
    {
        currentScore += amount;
        scoreUI.text = "SCORE: " + currentScore.ToString();
    }

    public void ModifyHealth(int amount)
    {
        if (currentHealth <= maxHealth)
        {
            currentHealth += amount;
            healthUI.text = "HEALTH: " + currentHealth.ToString();
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
                healthUI.text = "HEALTH: " + currentHealth.ToString();
            }
            if (currentHealth <= 0)
            {
                Debug.Log("You died.");
                currentHealth = maxHealth;
                healthUI.text = "HEALTH: " + currentHealth.ToString();
                transform.position = spawnPoint.position;
            }
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("HealingArea"))
        {
            collision.gameObject.GetComponent<HealthBehaviour>().RecoverHealth(this);
        }

        if (collision.gameObject.CompareTag("HazardArea"))
        {
            collision.gameObject.GetComponent<HealthBehaviour>().ApplyDamage(this);
        }
    }
}