using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    
    public float rotateSpeed;
    float tempX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // 위아래 무빙
        float mouseMoveY = Input.GetAxis("Mouse Y");
        transform.Rotate(-mouseMoveY * rotateSpeed * Time.deltaTime, 0, 0);
        
        // eulerAngles
        if (transform.eulerAngles.x > 180)
        {
            tempX = transform.eulerAngles.x - 360;
        }
        else
        {
            tempX = transform.eulerAngles.x;
        }

        // 위아래 시야각, X 축 회전 방향이 위아래 시야각임
        tempX = Mathf.Clamp(tempX, -60, 60);
        transform.eulerAngles = new Vector3(tempX, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
