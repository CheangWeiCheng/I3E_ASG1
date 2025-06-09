using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    private bool isOpen = false;
    public void Interact()
    {
        Vector3 doorRotation = transform.rotation.eulerAngles;
        if (isOpen)
        {
            // Close the door
            doorRotation.y -= 90f;
            isOpen = false;
        }
        else
        {
            // Open the door
            doorRotation.y += 90f;
            isOpen = true;
        }
        transform.eulerAngles = doorRotation;
    }
}
