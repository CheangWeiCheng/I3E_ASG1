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

    void Start()
    {
        totalCoins = FindObjectsByType<CoinBehaviour>(FindObjectsSortMode.None).Length;
        totalboxes = FindObjectsByType<BoxBehaviour>(FindObjectsSortMode.None).Length;
        congratulatoryText.gameObject.SetActive(false);
    }

    public void CoinCollected()
    {
        collectedCoins++;
        
        if (collectedCoins >= totalCoins + totalboxes) // accounts for boxes as well
        {
            ShowCongratulations();
        }
    }

    void ShowCongratulations()
    {
        congratulatoryText.text = "CONGRATULATIONS! ALL COINS COLLECTED!";
        congratulatoryText.gameObject.SetActive(true);
        Invoke("HideMessage", messageDuration);
    }

    void HideMessage()
    {
        congratulatoryText.gameObject.SetActive(false);
    }
}