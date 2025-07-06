using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
using TMPro; // using text mesh for the clock display
 
using UnityEngine.Rendering.Universal; // Changed from UnityEngine.Rendering
 
public class DayNightScript : MonoBehaviour
{
    public TextMeshProUGUI timeDisplay; // Display Time
    public TextMeshProUGUI dayDisplay; // Display Day
    public Light2D ppv; // this is the post processing volume
 
    public float tick; // Increasing the tick, increases second rate
    public float seconds; 
    public int mins;
    public int hours;
    public int days = 1;
 
    public bool activateLights; // checks if lights are on
    public GameObject[] lights; // all the lights we want on when its dark
    public SpriteRenderer[] stars; // star sprites 

    private float targetIntensity; // Add this field at the top with other variables
    private float transitionSpeed = 2f; // Adjust this value to control transition smoothness

    // Start is called before the first frame update
    void Start()
    {
        ppv = gameObject.GetComponent<Light2D>();
        
        // Set initial time values
        tick = 60;
        seconds = 0;
        mins = 0;
        hours = 6; // Starting at 6 AM
        days = 1;
        targetIntensity = 0.7f; // Set initial target
        ppv.intensity = 0.7f; // Set initial intensity
    }
 
    // Update is called once per frame
    void FixedUpdate() // we used fixed update, since update is frame dependant. 
    {
        CalcTime();
        GameManager.instance.clockDisplay.DisplayTime(hours, mins);

    }
 
    public void CalcTime() // Used to calculate sec, min and hours
    {
        seconds += Time.fixedDeltaTime * tick;

        if (seconds >= 60)
        {
            seconds = 0;
            mins += 1;
        }

        if (mins >= 60)
        {
            mins = 0;
            hours += 1;
        }

        if (hours >= 24)
        {
            hours = 0;
            days += 1;
        }
        ControlPPV(); // changes post processing volume after calculation
    }
 
    public void ControlPPV()
    {
        // Calculate target intensity based on time
        if(hours >= 21 && hours < 22) // dusk
        {
            targetIntensity = 1 - ((float)mins / 60);
        }
        else if(hours >= 22 || hours < 6) // night
        {
            targetIntensity = 0f;
        }
        else if(hours >= 6 && hours < 7) // dawn
        {
            targetIntensity = 0.7f + ((float)mins / 60) * 0.3f; // Start at 0.7 and increase to 1.0
        }
        else // daytime
        {
            targetIntensity = 1f;
        }

        // Smoothly transition to target intensity
        ppv.intensity = Mathf.Lerp(ppv.intensity, targetIntensity, Time.deltaTime * transitionSpeed);
    }

}