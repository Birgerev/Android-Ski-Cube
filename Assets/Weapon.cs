using UnityEngine;

[System.Serializable]
public class Weapon {

    public string name;
    public RuntimeAnimatorController animator;

    public float fireRate;
    public int burstAmount;
    public float burstTime;

}
