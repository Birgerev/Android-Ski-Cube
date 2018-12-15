using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeWeapon : MonoBehaviour {

    public Weapon weapon;

    public bool triggerDown;
    public Animator animator;


	// Use this for initialization
	void Start () {
        GetComponent<Animator>().runtimeAnimatorController = weapon.animator;

        animator = GetComponent<Animator>();

        StartCoroutine(loop());
	}
	
	// Update is called once per frame
	void Update () {
    }

    IEnumerator loop()
    {
        yield return new WaitForSeconds(Random.Range(0.01f, (60 / weapon.fireRate) / 2));
        while (true)
        {
            yield return new WaitForSeconds(60 / weapon.fireRate);
            if (triggerDown)
            {
                for (int i = 0; i < weapon.burstAmount; i++)
                {
                    Fire();
                    yield return new WaitForSeconds(weapon.burstTime);
                    animator.SetBool("Fire", false);
                }
            }
        }
    }

    public void Fire()
    {
        CameraController.instance.Flash();
        CameraController.instance.shake += CameraController.shootShake;

        animator.SetBool("Fire", true);

        GameObject obj = Instantiate(WeaponManager.instance.projectile);
        obj.transform.position = transform.position + transform.right * 0.9f;
        obj.transform.rotation = transform.rotation;

        obj.GetComponent<Rigidbody2D>().velocity += transform.parent.GetComponent<Rigidbody2D>().velocity;
    }
}
