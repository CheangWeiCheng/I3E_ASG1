using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    MeshRenderer meshRenderer; // Reference to the MeshRenderer component for highlighting
    [SerializeField]
    Material originalMaterial; // Store the original material for the coin
    [SerializeField]
    Material highlightMaterial; // Material used for highlighting the coin
    // Coin value that will be added to the player's score
    [SerializeField]
    int coinValue = 1;

    // Method to collect the coin
    // This method will be called when the player interacts with the coin
    // It takes a PlayerBehaviour object as a parameter
    // This allows the coin to modify the player's score
    // The method is public so it can be accessed from other scripts
    
    public void Start()
    {
        // Get the MeshRenderer component attached to this GameObject
        meshRenderer = GetComponent<MeshRenderer>();
        // Store the original color of the coin for later use
        originalMaterial = meshRenderer.material;
    }

    public void Highlight()
    {
        // Change the color of the coin to highlight it
        // This is done by setting the material color to the highlight color
        meshRenderer.material = highlightMaterial;
    }
    public void Unhighlight()
    {
        // Reset the color of the coin to its original color
        // This is done by setting the material color back to the original color
        meshRenderer.material = originalMaterial;
    }

    public void Collect(PlayerBehaviour player)
    {
        // Logic for collecting the coin
        Debug.Log("Coin collected!");

        // Add the coin value to the player's score
        // This is done by calling the ModifyScore method on the player object
        // The coinValue is passed as an argument to the method
        // This allows the player to gain points when they collect the coin
        player.ModifyScore(coinValue);

        Destroy(gameObject); // Destroy the coin object
    }
}
