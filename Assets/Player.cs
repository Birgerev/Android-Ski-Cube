using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Player : Skier {

    public static Skier instance;

    public GameObject gravestone;

    private int touchIndex = -1;
    private float startAngle = 0;

    public float touchLength = 0;

    public float angularAcceleration = 1;
    public float airAngularAcceleration = 1;

    public float minTouchLength = 0.4f;

    void Start()
    {
        //Assign a global instance reference for easy access
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
            Die(true);

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        if (Input.touchSupported)
        {
            if (touchIndex == -1 && Input.touchCount > 0)
            {
                touchIndex = Input.GetTouch(0).fingerId;
                print(touchIndex + "");
                touchLength = 0;
            }
            if (touchIndex != -1)
            {
                //TODO touch may be null already
                try
                {
                    if (Input.GetTouch(touchIndex).phase == TouchPhase.Ended)
                    {
                        if (touchLength < minTouchLength)
                        {
                            Jump();
                        }


                        touchIndex = -1;
                        return;
                    }
                }
                catch (Exception)
                {
                    touchIndex = -1;
                    return;
                }


                touchLength += Time.deltaTime;

                float save = 0;

                float rotZ = Mathf.SmoothDampAngle(transform.rotation.eulerAngles.z, 
lookAt(Camera.main.ScreenToWorldPoint(Input.GetTouch(touchIndex).position)).eulerAngles.z - startAngle,
                ref save, 1/ ((grounded) ? angularAcceleration : airAngularAcceleration));


                if (canMove)
                    transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

            }
        }
        else
        {
            float save = 0;

            float rotZ = Mathf.SmoothDampAngle(transform.rotation.eulerAngles.z,
lookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition)).eulerAngles.z - startAngle,
            ref save, 1 / ((grounded) ? angularAcceleration : airAngularAcceleration));

            if(canMove)
                transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (!grounded && canMove)
        {
            CameraController.targetFov += 0.03f;
        }
    }

    public override void Land()
    {
        base.Land();
        CameraController.targetFov = CameraController.normalFov;
    }

    public override void Die(bool canRespawn)
    {
        base.Die(canRespawn);

        GameObject obj = Instantiate(gravestone);

        obj.transform.position = transform.position;

        StateManager.instance.Lose();
    }
}

