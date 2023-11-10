using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{

    private int camSpeed = 4;
    int speed = 1;

    // Update is called once per frame
    void Update()
    {
        float horz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");

        transform.position += (Vector3.up * vert + Vector3.right * horz) * camSpeed * Time.deltaTime;

        float mouseWheel = Input.GetAxis("Mouse ScrollWheel");

        if (mouseWheel != 0)
        {
            if(Camera.main.orthographicSize <= 7)
            {
                speed = 1;
                Camera.main.orthographicSize = 7;
                Camera.main.orthographicSize -= mouseWheel * speed;
            }
            else if (Camera.main.orthographicSize >= 20)
            {
                speed = 1;
                Camera.main.orthographicSize = 20;
                Camera.main.orthographicSize -= mouseWheel * speed;
            }
            else
            {
                speed = 4;
                Camera.main.orthographicSize -= mouseWheel * speed;
            }
        }
    }
}
