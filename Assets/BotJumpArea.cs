using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotJumpArea : MonoBehaviour {

    public float chance = 100;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Enemy>() != null)
        {
            if (Random.Range(0, 100) < chance)
            {
                col.GetComponent<Enemy>().Jump();
            }
        }
    }
}
