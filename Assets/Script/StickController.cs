using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StickController : MonoBehaviour
{
    
    private const float posVelocity = 8.0f;
    private const float rotVelocity = 100.0f;

    public Vector3 restartPos;
    private const float startPos = -7f;
    [SerializeField] private float[] checkPoint = new float[2];
    int i = 0;

    public CameraController cameraController;

    public bool isGoal = false;
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

        if (Input.GetKeyDown(KeyCode.R))
        {
            restart();
        }

        if (isGoal == true)
        {
            return;
        }

        if(transform.position.x >= checkPoint[i])
        {
            restartPos.x = checkPoint[i];
            if (i < checkPoint.Length-1) i++;
        }

        stickMove();
    }

    void restart()
    {
        isGoal = false;
        isRestart = true;
        restartPos.x = startPos;
        transform.position = restartPos;
        i = 0;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void stickMove()
    {
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

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, 0, rotVelocity * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, 0, -rotVelocity * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Wall")
        {
            if (restartPos.x == -7f)
            {
                cameraController.transform.position = new Vector3(0, 0, -10);
            }

            transform.position = restartPos;
            transform.rotation = Quaternion.Euler(0, 0, 0);

            audioSource.Play();
        }

        if(collider.tag == "Goal")
        {
            isGoal = true;
        }
    }
}
