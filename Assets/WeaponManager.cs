using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

    public static WeaponManager instance;

    public GameObject projectile;
    public GameObject pickup;

    public Weapon[] weapons;

    public Weapon noWeapon;

    private void Start()
    {
        instance = this;
    }

    public Weapon getWeapon(string name)
    {
        foreach (Weapon wep in weapons)
        {
            if(wep.name.ToLower() == name.ToLower())
            {
                return wep;
            }
        }

        return null;
    }

}
