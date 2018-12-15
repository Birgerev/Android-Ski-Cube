using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravestoneDecoration : MonoBehaviour {

    public float startYOffset = 8;

    // Use this for initialization
    void Start()
    {
        //Increment x position randomly for more variation
        transform.position += new Vector3(0, startYOffset);

        GetComponent<Rigidbody2D>().gravityScale = 1;

        //Destroy all physics components if we haven't already hit ground
        destroyPhysics(10);
    }

    void OnCollisionEnter2D()
    {
        destroyPhysics(0);

        Animator anim = GetComponent<Animator>();
        anim.SetTrigger("land");
    }

    private void destroyPhysics(int time)
    {
        //Destroy all physics component with a delay
        Destroy(GetComponent<Rigidbody2D>(), time);
        Destroy(GetComponent<BoxCollider2D>(), time);
    }
}
