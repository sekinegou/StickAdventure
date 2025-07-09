using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StickController : MonoBehaviour
{
    //プレイヤーの移動速度
    private const float posVelocity = 8.0f;
    //プレイヤーの回転速度
    private const float rotVelocity = 100.0f;

    //壁にぶつかったorリスタートした場合の再開座標
    public Vector3 restartPos;
    //スタート・リスタート時のx座標
    private const float startPos = -7f;

    //チェックポイントのx座標
    [SerializeField] private float[] checkPoint = new float[2];
    int i = 0;

    //カメラのインスタンス
    public CameraController cameraController;

    //ゴールしたかどうか
    public bool isGoal = false;
    //リスタートしたかどうか
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

        //Rが押されたら
        if (Input.GetKeyDown(KeyCode.R))
        {
            restart();
        }

        //ゴールしたら動けないようにする
        if (isGoal == true)
        {
            return;
        }

        //プレイヤーがチェックポイントに到達したら
        if(transform.position.x >= checkPoint[i])
        {
            //リスタート時の座標を更新
            restartPos.x = checkPoint[i];
            //iが最後のチェックポイントの場合はインクリメントしない
            if (i < checkPoint.Length-1) i++;
        }

        stickMove();
    }

    //リスタート時の処理
    void restart()
    {
        isGoal = false;
        isRestart = true;
        restartPos.x = startPos;
        transform.position = restartPos;
        i = 0;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    //プレイヤーの移動処理
    void stickMove()
    {
        //WASDで移動
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

        //←→で回転
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, 0, rotVelocity * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, 0, -rotVelocity * Time.deltaTime);
        }
    }

    //衝突判定
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Wall")
        {
            //リスタートの座標が初期位置なら、カメラを初期位置に戻す
            if (restartPos.x == -7f)
            {
                cameraController.transform.position = new Vector3(0, 0, -10);
            }

            //チェックポイントに戻る
            transform.position = restartPos;
            transform.rotation = Quaternion.Euler(0, 0, 0);

            audioSource.Play();
        }

        //ゴール判定
        if(collider.tag == "Goal")
        {
            isGoal = true;
        }
    }
}
