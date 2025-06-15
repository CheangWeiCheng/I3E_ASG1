/*
* Author: Cheang Wei Cheng
* Date: 14 June 2025
* Description: This script controls the behavior of moving bridges in the game.
* The bridge moves up and down between a specified maximum and minimum height at a constant speed.
* The movement is continuous, and the bridge reverses direction when it reaches either the maximum or minimum height.
*/

using UnityEngine;

public class BridgeMovementBehaviour : MonoBehaviour
{
    float maxY = 2f; // Maximum height of the bridge
    float minY = 0.1f; // Minimum height of the bridge
    float moveSpeed = 1f; // Speed of the bridge movement
    bool isMovingDown; // Flag to determine the direction of movement

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= maxY)
        {
            // Move the bridge down
            isMovingDown = true;
        }
        else if (transform.position.y <= minY)
        {
            // Move the bridge up
            isMovingDown = false;
        }

        if (isMovingDown) transform.position = transform.position - new Vector3(0f, moveSpeed * Time.deltaTime, 0f);
        else transform.position = transform.position + new Vector3(0f, moveSpeed * Time.deltaTime, 0f);
    }
}
