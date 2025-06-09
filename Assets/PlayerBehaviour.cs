using UnityEngine;
using TMPro;

public class PlayerBehaviour : MonoBehaviour
{
    public GameObject projectile; // The projectile prefab to be instantiated
    public Transform gunPoint;
    public float fireStrength = 5f;

    void OnFire()
    {
        Debug.Log("Fire!");
        var bullet = Instantiate(projectile, gunPoint.position, transform.rotation);
        var fireForce = transform.forward * fireStrength;
        var rigidBody = bullet.GetComponent<Rigidbody>();
        rigidBody.AddForce(fireForce);
        Destroy(bullet, 5f); // Destroy the bullet after 5 seconds
    }

    [SerializeField]
    TMP_Text healthUI;
    // Player's maximum health
    int maxHealth = 100;
    // Player's current health
    int currentHealth = 100;

    [SerializeField]
    TMP_Text scoreUI;
    // Player's current score
    int currentScore = 0;
    // Flag to check if the player can interact with objects
    bool canInteract = false;
    // Stores the current coin object the player has detected
    CoinBehaviour currentCoin = null;
    DoorBehaviour currentDoor = null;

    [SerializeField]
    float interactionDistance = 2f;

    Camera mainCamera;

    void Start()
    {
        // Initialize the score UI text
        healthUI.text = "HEALTH: " + currentHealth.ToString();
        scoreUI.text = "SCORE: " + currentScore.ToString();
    }

    void Update()
    {
        // Check if the 'E' key is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnInteract();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            OnFire();
        }

        RaycastHit hitInfo;
        
        if (Physics.Raycast(gunPoint.position, gunPoint.forward, out hitInfo, interactionDistance))
        {
            // Check if the raycast hit a coin or door
            if (hitInfo.collider.gameObject.CompareTag("Collectible"))
            {
                currentCoin = hitInfo.collider.GetComponent<CoinBehaviour>();
                currentDoor = null; // Reset currentDoor if we hit a coin
                currentCoin.Highlight();
            }
            else if (hitInfo.collider.gameObject.CompareTag("Door"))
            {
                if (currentCoin != null)
                {
                    currentCoin.Unhighlight();
                }
                currentDoor = hitInfo.collider.GetComponent<DoorBehaviour>();
                currentCoin = null; // Reset currentCoin if we hit a door
            }
            else
            {
                // If the raycast hits something else, reset both
                if (currentCoin != null)
                {
                    currentCoin.Unhighlight();
                }
                currentCoin = null;
                currentDoor = null;
            }
        }
        else
        {
            if (currentCoin != null)
            {
                currentCoin.Unhighlight();
            }
            // If nothing is hit, reset both
            currentCoin = null;
            currentDoor = null;
        }
    }

    // The Interact callback for the Interact Input Action
    // This method is called when the player presses the interact button
    void OnInteract()
    {
        // Check if the player can interact with objects
        if (canInteract)
        {
            // Check if the player has detected a coin or a door
            if (currentCoin != null)
            {
                Debug.Log("Interacting with coin");
                // Call the Collect method on the coin object
                // Pass the player object as an argument
                currentCoin.Collect(this);
                currentScore++;
                scoreUI.text = "SCORE: " + currentScore.ToString();
            }
            else if (currentDoor != null)
            {
                Debug.Log("Interacting with door");
                currentDoor.Interact();
            }
        }
    }

    // Method to modify the player's score
    // This method takes an integer amount as a parameter
    // It adds the amount to the player's current score
    // The method is public so it can be accessed from other scripts
    public void ModifyScore(int amt)
    {
        // Increase currentScore by the amount passed as an argument
        currentScore += amt;
    }

    // Method to modify the player's health
    // This method takes an integer amount as a parameter
    // It adds the amount to the player's current health
    // The method is public so it can be accessed from other scripts
    public void ModifyHealth(int amount)
    {
        // Check if the current health is less than the maximum health
        // If it is, increase the current health by the amount passed as an argument
        if (currentHealth < maxHealth)
        {
            currentHealth += amount;
            // Check if the current health exceeds the maximum health
            // If it does, set the current health to the maximum health
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }
    }

    // Collision Callback for when the player collides with another object
    void OnCollisionStay(Collision collision)
    {
        // Check if the player collides with an object tagged as "HealingArea"
        // If it does, call the RecoverHealth method on the object
        // Pass the player object as an argument
        // This allows the player to recover health when in a healing area
        if (collision.gameObject.CompareTag("HealingArea"))
        {
            collision.gameObject.GetComponent<RecoveryBehaviour>().RecoverHealth(this);
            // Update the health UI text
            healthUI.text = "HEALTH: " + currentHealth.ToString();
        }
    }

    // Trigger Callback for when the player enters a trigger collider
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        // Check if the player detects a trigger collider tagged as "Collectible" or "Door"
        if (other.CompareTag("Collectible"))
        {
            // Set the canInteract flag to true
            // Get the CoinBehaviour component from the detected object
            canInteract = true;
            currentCoin = other.GetComponent<CoinBehaviour>();
        }
        else if (other.CompareTag("Door"))
        {
            canInteract = true;
            currentDoor = other.GetComponent<DoorBehaviour>();
        }
    }

    // Trigger Callback for when the player exits a trigger collider
    void OnTriggerExit(Collider other)
    {
        // Check if the player has a detected coin or door
        if (currentCoin != null)
        {
            // If the object that exited the trigger is the same as the current coin
            if (other.gameObject == currentCoin.gameObject)
            {
                // Set the canInteract flag to false
                // Set the current coin to null
                // This prevents the player from interacting with the coin
                canInteract = false;
                currentCoin = null;
            }
        }
        else if (currentDoor != null)
        {
            // If the object that exited the trigger is the same as the current door
            if (other.gameObject == currentDoor.gameObject)
            {
                // Set the canInteract flag to false
                // Set the current door to null
                // This prevents the player from interacting with the door
                canInteract = false;
                currentDoor = null;
            }
        }
    }
}
