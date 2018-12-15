using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoration : MonoBehaviour {

    public float maxRandomDistance = 16;
    public float maxRandomYPos = 1;

    public int backgroundLayer = -1;
    public int foregroundLayer = 10;
    public float foregroundChance = 30;

    public GameObject debug;

    // Use this for initialization
    void Start () {
        //Increment x position randomly for more variation
        transform.position += new Vector3(Random.Range(0, maxRandomDistance), 0);

        GetComponent<Rigidbody2D>().gravityScale = 0;

        GetComponent<SpriteRenderer>().enabled = false;
    }

    void Update()
    {
        if(GetComponent<Rigidbody2D>())
            if (GetComponent<Rigidbody2D>().gravityScale == 0)
            {
                RaycastHit2D[] rays = Physics2D.RaycastAll(transform.position, new Vector2(0, -1), 50);
                bool canFall = false;


                foreach (RaycastHit2D ray in rays)
                {
                    if (ray.transform != transform)
                    {
                        canFall = true;
                    }
                }

                if (canFall)
                {
                    GetComponent<Rigidbody2D>().gravityScale = 1;

                    //Destroy all physics components if we haven't already hit ground
                    destroyPhysics(10);
                }
            }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        debug = col.gameObject;
        destroyPhysics();

        //Decide wether object should be in foreground or background based on chance
        GetComponent<SpriteRenderer>().sortingOrder =
            (Random.Range(0, 100) <= foregroundChance) ? foregroundLayer : backgroundLayer;

        //Move object downwards randomly
        transform.position += new Vector3(0, -Random.Range(0.0f, maxRandomYPos));

        GetComponent<SpriteRenderer>().enabled = true;
    }

    private void destroyPhysics()
    {
        destroyPhysics(0);
    }

    private void destroyPhysics(int time)
    {
        //Destroy all physics component with a delay
        Destroy(GetComponent<Rigidbody2D>(), time);
        Destroy(GetComponent<BoxCollider2D>(), time);
    }
}
