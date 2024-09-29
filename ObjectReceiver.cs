using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;
using UnityEngine.UI;  // Import for UI elements

using TMPro;
using System;


public class ObjectReceiver : MonoBehaviour
{
    public GameObject HoldingObject;


    public float startTime = 60f;  // Start time in seconds
    private float currentTime;
    //public Text timerText;         // Reference to UI Text component
    public TMP_Text timerText;

    private void Start()
    {
        // Set the current time to the starting time at the beginning
        currentTime = startTime;
        UpdateTimerDisplay();
    }

    private void Update()
    {
        // Reduce current time by deltaTime each frame
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;

            // Clamp the value to ensure it doesn't go below zero
            currentTime = Mathf.Clamp(currentTime, 0, startTime);

            // Update the timer display on the canvas
            UpdateTimerDisplay();
        }
    }

    // Method to update the UI text with the current time
    private void UpdateTimerDisplay()
    {
        // Convert time to minutes and seconds (if needed)
        int minutes = Mathf.FloorToInt(currentTime / 60F);
        int seconds = Mathf.FloorToInt(currentTime % 60F);

        // Update the text component (e.g. "02:30")
        //timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }


    public void ReceiveHoldingObject(GameObject receivedObject)
    {
        if (receivedObject != null)
        {
            receivedObject.transform.position = transform.position;
            this.HoldingObject = receivedObject;
        }
    }

}