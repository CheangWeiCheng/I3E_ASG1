/*
* Author: Cheang Wei Cheng
* Date: 14 June 2025
* Description: This script manages the collection of coins in the game.
* It tracks the total number of coins and boxes (since boxes contain coins),
* updates the count when a coin is collected, and displays a congratulatory message when all coins are collected.
* The script uses Unity's UI system to show the message and hides it after a specified duration.
*/

using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    [SerializeField] TMP_Text congratulatoryText;
    [SerializeField] float messageDuration = 3f;
    
    private int totalCoins;
    private int totalboxes;
    private int collectedCoins;

    /// <summary>
    /// Initializes the CoinManager by counting the total number of coins and boxes in the scene.
    /// The congratulatory text is initially hidden.
    /// </summary>
    void Start()
    {
        totalCoins = FindObjectsByType<CoinBehaviour>(FindObjectsSortMode.None).Length;
        totalboxes = FindObjectsByType<BoxBehaviour>(FindObjectsSortMode.None).Length;
        congratulatoryText.gameObject.SetActive(false);
    }

    /// <summary>
    /// Increments the count of collected coins and checks if all coins (including those in boxes) have been collected.
    /// If all coins are collected, it displays a congratulatory message.
    /// </summary>
    public void CoinCollected()
    {
        collectedCoins++;

        if (collectedCoins >= totalCoins + totalboxes) // accounts for boxes as well
        {
            ShowCongratulations();
        }
    }

    /// <summary>
    /// Displays a congratulatory message indicating that all coins have been collected.
    /// The message is shown for a specified duration before being hidden.
    /// </summary>
    void ShowCongratulations()
    {
        congratulatoryText.text = "CONGRATULATIONS! ALL COINS COLLECTED!";
        congratulatoryText.gameObject.SetActive(true);
        Invoke("HideMessage", messageDuration);
    }

    /// <summary>
    /// Hides the congratulatory message after the specified duration.
    /// This method is called by the Invoke method after the message duration has elapsed.
    /// </summary>
    void HideMessage()
    {
        congratulatoryText.gameObject.SetActive(false);
    }
}