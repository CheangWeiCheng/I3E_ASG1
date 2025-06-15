/*
* Author: Cheang Wei Cheng
* Date: 14 June 2025
* Description: This script controls the material visuals of doors in the game.
* It changes the door's material based on whether it is locked or unlocked, and provides methods to highlight and unhighlight the door.
* The script uses Unity's MeshRenderer to change the material dynamically.
* This script is seperate from the DoorBehaviour script because DoorBehaviour is mapped to the door hinge,
* while this script is mapped to the door mesh itself.
*/

using UnityEngine;

public class DoorLockVisual : MonoBehaviour
{
    [SerializeField] Material lockedMaterial;
    [SerializeField] Material defaultMaterial;
    [SerializeField] Material highlightMaterial;
    
    private MeshRenderer meshRenderer;
    private bool isLocked;

    void Start()
    {
        // Get the MeshRenderer component attached to this GameObject
        meshRenderer = GetComponent<MeshRenderer>();
        UpdateMaterial();
    }

    public void SetLocked(bool locked)
    {
        // Set the locked state and update the material accordingly
        isLocked = locked;
        UpdateMaterial();
    }

    public void Highlight()
    {
        // Change the material to highlight the door
        meshRenderer.material = highlightMaterial;
    }

    public void Unhighlight()
    {
        UpdateMaterial(); // Revert to appropriate locked/unlocked material
    }

    void UpdateMaterial()
    {
        // Update the material based on the locked state
        meshRenderer.material = isLocked ? lockedMaterial : defaultMaterial;
    }
}