using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeText : MonoBehaviour
{
    //プレイヤーのインスタンス
    [SerializeField] private StickController stickController;

    //経過時間のText
    public Text timeText;
    //ベストタイムのText
    public Text bestTimeText;

    //タイム
    private float time = 0;
    //分
    private int min;
    //ベストタイム
    private float bestTime = 0;

    void Update()
    {
        //リスタート時タイムをリセット
        if (stickController.isRestart == true)
        {
            time = 0;
            timeText.text = "0:00";

        }

        //プレイヤーがスタートするまでタイムを進めない
        if (stickController.restartPos.x == -7)
        {
            return;
        }

        //プレイヤーがゴールした場合
        if (stickController.isGoal == true)
        {
            //ベストタイムを更新したor初回ゴール時、ベストタイム更新
            if(time < bestTime || bestTime == 0)
            {
                bestTime = time;
                bestTimeText.text = "BestTime " + min + ":" + (bestTime - min * 60).ToString("00");
            }
            return;
        }

        //時間経過
        time += Time.deltaTime;
        min = (int)(time / 60);
        timeText.text = min + ":" + (time - min * 60).ToString("00");
    }
}
