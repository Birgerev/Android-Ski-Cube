using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public Transform playerSpawn;

    public static int meters = 0;
    public const float meterSize = 0.5f;


    // Use this for initialization
    void Start () {
        instance = this;

        Reset();
    }
	
	// Update is called once per frame
	void Update () {
        meters = (int)((Player.instance.transform.position.x - playerSpawn.transform.position.x) / meterSize);
	}

    public void Reset()
    {
        WorldBuilder.instance.Reset();
        CameraController.targetFov = CameraController.zoomedFov;

        Player.instance.transform.position = playerSpawn.position;
        Player.instance.canMove = false;
    }

    public void Game()
    {
        CameraController.targetFov = CameraController.normalFov;
        Player.instance.canMove = true;
    }
}
