using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointManager : MonoBehaviour
{
    public static PointManager instance;

    public int points = 0;
    public TextMeshProUGUI pointsText;

    public AudioClip decreaseSound; // Sound effect for when points decrease
    public AudioSource audioSource; // Reference to the AudioSource component

    private bool isFirstPlay = true; // Flag to track if it's the first play of the sound effect

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
    }

    public void AddPoints(int amount)
    {
        if (amount < 0 && decreaseSound != null && !isFirstPlay) // If points decrease, decreaseSound is assigned, and it's not the first play
        {
            audioSource.PlayOneShot(decreaseSound); // Play the decrease sound effect
        }

        if (isFirstPlay)
        {
            isFirstPlay = false; // Update flag to indicate it's no longer the first play
        }

        points += amount;
        UpdatePointsUI();
    }

    private void UpdatePointsUI()
    {
        pointsText.text = "" + points.ToString();
    }
}
