using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Skier {

    public float additionalTilt = 20;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (grounded)
        {
            Vector3 euler = ground.transform.rotation.eulerAngles;

            euler = new Vector3(0, 0, euler.z + additionalTilt);

            Quaternion rot = Quaternion.Euler(euler);

            transform.rotation = rot;
        }else
        {
            if(Player.instance != null)
                transform.rotation = lookAt(Player.instance.transform.position);
        }
	}
}
