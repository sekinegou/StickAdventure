using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //�v���C���[�̃C���X�^���X
    [SerializeField] private StickController stickController;
    //�J�����̏����ʒu
    [SerializeField] private float startPosX;

    //�v���C���[�̍��W
    private Transform stickTransform;

    void Update()
    {
        stickTransform = stickController.transform;

        //���X�^�[�g���A�J�����������ʒu�Ɉړ�
        if (stickController.isRestart == true)
        {
            transform.position = new Vector3(0, 0, -10);
        }

        //�J�������R�[�X�̊O���f���Ȃ��悤�ɂ���
        if (stickTransform == null || stickTransform.position.x+2.5f <= startPosX)
        {
            return;
        }
        
        //�J�������v���C���[��Ǐ]����
        transform.position = new Vector3(stickTransform.position.x+2.5f, transform.position.y, transform.position.z);
    }
}
