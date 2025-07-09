using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //プレイヤーのインスタンス
    [SerializeField] private StickController stickController;
    //カメラの初期位置
    [SerializeField] private float startPosX;

    //プレイヤーの座標
    private Transform stickTransform;

    void Update()
    {
        stickTransform = stickController.transform;

        //リスタート時、カメラを初期位置に移動
        if (stickController.isRestart == true)
        {
            transform.position = new Vector3(0, 0, -10);
        }

        //カメラがコースの外を映さないようにする
        if (stickTransform == null || stickTransform.position.x+2.5f <= startPosX)
        {
            return;
        }
        
        //カメラがプレイヤーを追従する
        transform.position = new Vector3(stickTransform.position.x+2.5f, transform.position.y, transform.position.z);
    }
}
