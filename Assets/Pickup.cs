using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    public int weaponId;

    private Weapon weapon;
    
	// Update is called once per frame
	void Update () {
		if(weapon == null)
        {
            //Start
            weapon = WeaponManager.instance.weapons[weaponId];

            GetComponent<Animator>().runtimeAnimatorController = weapon.animator;

            GetComponent<Animator>().SetBool("pickup", true);
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Skier>() != null)
        {
            col.GetComponent<Skier>().HoldWeapon(weapon);
            Destroy(gameObject);
        }
    }
}
