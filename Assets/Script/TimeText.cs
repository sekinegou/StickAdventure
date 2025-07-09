using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeText : MonoBehaviour
{
    [SerializeField] private StickController stickController;

    public Text timeText;
    public Text bestTimeText;

    private float time = 0;
    private int min;
    private float bestTime = 0;

    void Update()
    {
        if (stickController.isRestart == true)
        {
            time = 0;
            timeText.text = "0:00";

        }

        if (stickController.restartPos.x == -7)
        {
            return;
        }

        if (stickController.isGoal == true)
        {
            if(time < bestTime || bestTime == 0)
            {
                bestTime = time;
                bestTimeText.text = "BestTime " + min + ":" + (bestTime - min * 60).ToString("00");
            }
            return;
        }

        time += Time.deltaTime;
        min = (int)(time / 60);
        timeText.text = min + ":" + (time - min * 60).ToString("00");
    }
}
