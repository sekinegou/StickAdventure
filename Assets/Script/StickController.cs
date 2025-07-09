using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StickController : MonoBehaviour
{
    //�v���C���[�̈ړ����x
    private const float posVelocity = 8.0f;
    //�v���C���[�̉�]���x
    private const float rotVelocity = 100.0f;

    //�ǂɂԂ�����or���X�^�[�g�����ꍇ�̍ĊJ���W
    public Vector3 restartPos;
    //�X�^�[�g�E���X�^�[�g����x���W
    private const float startPos = -7f;

    //�`�F�b�N�|�C���g��x���W
    [SerializeField] private float[] checkPoint = new float[2];
    int i = 0;

    //�J�����̃C���X�^���X
    public CameraController cameraController;

    //�S�[���������ǂ���
    public bool isGoal = false;
    //���X�^�[�g�������ǂ���
    public bool isRestart = false;

    AudioSource audioSource;

    void Start()
    {
        Application.targetFrameRate = 60;
        restartPos = new Vector3(startPos, 0, 0);
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        isRestart = false;

        //R�������ꂽ��
        if (Input.GetKeyDown(KeyCode.R))
        {
            restart();
        }

        //�S�[�������瓮���Ȃ��悤�ɂ���
        if (isGoal == true)
        {
            return;
        }

        //�v���C���[���`�F�b�N�|�C���g�ɓ��B������
        if(transform.position.x >= checkPoint[i])
        {
            //���X�^�[�g���̍��W���X�V
            restartPos.x = checkPoint[i];
            //i���Ō�̃`�F�b�N�|�C���g�̏ꍇ�̓C���N�������g���Ȃ�
            if (i < checkPoint.Length-1) i++;
        }

        stickMove();
    }

    //���X�^�[�g���̏���
    void restart()
    {
        isGoal = false;
        isRestart = true;
        restartPos.x = startPos;
        transform.position = restartPos;
        i = 0;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    //�v���C���[�̈ړ�����
    void stickMove()
    {
        //WASD�ňړ�
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-posVelocity * Time.deltaTime, 0, 0, Space.World);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(posVelocity * Time.deltaTime, 0, 0, Space.World);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, posVelocity * Time.deltaTime, 0, Space.World);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, -posVelocity * Time.deltaTime, 0, Space.World);
        }

        //�����ŉ�]
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, 0, rotVelocity * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, 0, -rotVelocity * Time.deltaTime);
        }
    }

    //�Փ˔���
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Wall")
        {
            //���X�^�[�g�̍��W�������ʒu�Ȃ�A�J�����������ʒu�ɖ߂�
            if (restartPos.x == -7f)
            {
                cameraController.transform.position = new Vector3(0, 0, -10);
            }

            //�`�F�b�N�|�C���g�ɖ߂�
            transform.position = restartPos;
            transform.rotation = Quaternion.Euler(0, 0, 0);

            audioSource.Play();
        }

        //�S�[������
        if(collider.tag == "Goal")
        {
            isGoal = true;
        }
    }
}
