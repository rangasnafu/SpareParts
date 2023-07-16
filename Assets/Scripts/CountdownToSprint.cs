using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class CountdownToSprint : MonoBehaviour
{
    public float timeLeft;
    public TextMeshProUGUI TimerText;
    private DateTime targetDateTime;

    private void Start()
    {
        // Set the target date and time
        targetDateTime = new DateTime(2023, 7, 24, 11, 2, 33, DateTimeKind.Utc);
        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine()
    {
        while (true)
        {
            // Calculate the time difference
            TimeSpan timeDifference = targetDateTime - DateTime.UtcNow;
            timeLeft = (float)timeDifference.TotalSeconds;

            // Check if the countdown is complete
            if (timeLeft <= 0)
            {
                // Handle the countdown completion
                timeLeft = 0;
                TimerText.text = "The future";
                yield break;
            }

            // Update the countdown timer display
            float minutes = Mathf.FloorToInt(timeLeft / 60);
            float seconds = Mathf.FloorToInt(timeLeft % 60);

            // Format the time display string
            string timeLeftString = "";
            if (minutes > 0)
            {
                timeLeftString += minutes.ToString("N0", CultureInfo.InvariantCulture) + " minute";
                if (minutes > 1)
                {
                    timeLeftString += "s";
                }
                timeLeftString += " ";
            }
            timeLeftString += seconds + " second";
            if (seconds != 1)
            {
                timeLeftString += "s";
            }

            TimerText.text = timeLeftString;

            yield return new WaitForSecondsRealtime(1f);
        }
    }
}
