using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private StickController stickController;
    [SerializeField] private float startPosX;

    private Transform stickTransform;

    void Update()
    {
        stickTransform = stickController.transform;

        if (stickController.isRestart == true)
        {
            transform.position = new Vector3(0, 0, -10);
        }

        if (stickTransform == null || stickTransform.position.x+2.5f <= startPosX)
        {
            return;
        }
        
        transform.position = new Vector3(stickTransform.position.x+2.5f, transform.position.y, transform.position.z);
    }
}
