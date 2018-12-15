using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpMultiplierArea : MonoBehaviour {

    public float multiplier = 2;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Skier>() != null)
        {
            col.GetComponent<Skier>().jumpMultiplier = multiplier;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.GetComponent<Skier>() != null)
        {
            col.GetComponent<Skier>().jumpMultiplier = 1;
        }
    }
}
