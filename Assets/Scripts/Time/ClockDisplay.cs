using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockDisplay : MonoBehaviour
{
    public Image hour_1;
    public Image hour_2;
    public Image minute_1;
    public Image minute_2;

    public Sprite[] numberSprites;

    public void DisplayTime(int hours, int minutes)
    {
        if (hours > 12)
        {
            hours = hours - 12;
        }

        if (hours / 10 == 0)
        {
            hour_2.gameObject.SetActive(false);
        }else{
            hour_2.gameObject.SetActive(true);
            hour_2.sprite = numberSprites[hours / 10];
        }

        hour_1.sprite = numberSprites[hours % 10];
        minute_2.sprite = numberSprites[minutes / 10];
        minute_1.sprite = numberSprites[minutes % 10];
    }
    
}
