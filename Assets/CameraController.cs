using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public static CameraController instance;

    //Camera target variables
    public float dampTime = 0.15f;
    public float zoomDampTime = 2f;
    private Vector3 velocity = Vector3.zero;
    private float vel = 0f;
    public static float targetFov = 3.5f;
    public Transform target;

    public const float normalFov = 7.5f;
    public const float zoomedFov = 3.5f;

    private float startZ = -0.1f;
    
    //Camera shake variables
    public float maxRoll = 10;
    public float maxOffset = 0.5f;
    public float shake = 0;
    public float shakedropoffPerFrame = 0.8f;
    public float shakeforce = 1.5f;

    public GameObject flash;

    public float roll;
    public float offsetX;
    public float offsetY;

    int seed = 0;
    
    //constant shake amounts
    public const float shootShake = 1;



    public void Flash()
    {

        flash.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0.8f);
        StartCoroutine(FadeFlash());

    }

    public IEnumerator FadeFlash()
    {
        yield return new WaitForSeconds(0.02f);
        flash.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
    }

    void ShakeCalc()
    {
        //Seeds needs to use fractal numbers
        roll = maxRoll * shake * Mathf.PerlinNoise(seed + 0.4f, seed + 0.4f);
        offsetX = maxOffset * shake * Mathf.PerlinNoise(seed + 1.4f, seed + 1.4f);
        offsetY = maxOffset * shake * Mathf.PerlinNoise(seed + 2.4f, seed + 2.4f);

        seed += 1;

        shake *= shakedropoffPerFrame;
        if (shake < 0.001f)
            shake = 0;
    }

    public void Shake()
    {
        shake += shakeforce;
    }

    void Start()
    {
        instance = this;

        if (startZ == -0.1f)
        {
            startZ = transform.position.z;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Smoothly target player
        if (target)
        {
            Vector3 point = Camera.main.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position + delta;

            destination = new Vector3(destination.x, destination.y, startZ);

            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }

        float currentFov = GetComponent<Camera>().orthographicSize;


        GetComponent<Camera>().orthographicSize = Mathf.SmoothDamp(currentFov, targetFov, ref vel, zoomDampTime);

        
        ShakeCalc();

        Vector2 pos = transform.position;
        float angle = 0;

        pos += new Vector2(offsetX, offsetY);
        angle = roll;

        transform.position = pos;
        transform.rotation = Quaternion.Euler(0, 0, angle);




        transform.position = new Vector3(transform.position.x, transform.position.y, startZ);
    }
}
