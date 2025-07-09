using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeText : MonoBehaviour
{
    //�v���C���[�̃C���X�^���X
    [SerializeField] private StickController stickController;

    //�o�ߎ��Ԃ�Text
    public Text timeText;
    //�x�X�g�^�C����Text
    public Text bestTimeText;

    //�^�C��
    private float time = 0;
    //��
    private int min;
    //�x�X�g�^�C��
    private float bestTime = 0;

    void Update()
    {
        //���X�^�[�g���^�C�������Z�b�g
        if (stickController.isRestart == true)
        {
            time = 0;
            timeText.text = "0:00";

        }

        //�v���C���[���X�^�[�g����܂Ń^�C����i�߂Ȃ�
        if (stickController.restartPos.x == -7)
        {
            return;
        }

        //�v���C���[���S�[�������ꍇ
        if (stickController.isGoal == true)
        {
            //�x�X�g�^�C�����X�V����or����S�[�����A�x�X�g�^�C���X�V
            if(time < bestTime || bestTime == 0)
            {
                bestTime = time;
                bestTimeText.text = "BestTime " + min + ":" + (bestTime - min * 60).ToString("00");
            }
            return;
        }

        //���Ԍo��
        time += Time.deltaTime;
        min = (int)(time / 60);
        timeText.text = min + ":" + (time - min * 60).ToString("00");
    }
}
