using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Skier : Shootable {

    public bool canMove = true;
    public bool canJump = true;

    private Rigidbody2D rb;
    public float speed = 15f;
    private float rotationSpeedMultiplier = 4;
    private float rotationSpeedDefault = 3.5f;
    private float slopeSpeedMultiplier = 4;

    private float drag = 0.9999f;
    private float airDrag = 0.998f;
    private float fallGravity = -800f;
    private float gravity = -130f;
    private Vector3 groundCheck = new Vector3(0, -0.5f, 0);
    public GameObject ground;
    public bool grounded;
    public float acceleration = 3;
    public float jumpMultiplier = 1;

    private bool jumping = false;

    private Vector2 forwardMomentum;
    
    public GameObject weaponHolder;

    public Vector2 jumpVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //Disable rotation to prevent character from tipping over
        rb.freezeRotation = true;
        //Disable gravity so that we can simulate gravity ourselves
        rb.gravityScale = 0;

    }

    public virtual void FixedUpdate()
    {
        FindGround();
        if (canMove)
        {
            Gravity();
            Movement();
        }else
        {
            rb.velocity = Vector2.zero;
        }
        if (weaponHolder.GetComponent<RuntimeWeapon>())
            weaponHolder.GetComponent<RuntimeWeapon>().triggerDown = !grounded;
    }

    private void Movement()
    {
        if (grounded)
        {
            //apply drag
            rb.velocity = rb.velocity * drag;

            float forwardVel = 0;
            Vector2 vel = Vector2.zero;

            //assign default speed
            forwardVel =
                ((speed) + (calculateSlopeSpeed() * slopeSpeedMultiplier) +
                ((calculateRotationSpeed() - rotationSpeedDefault) * rotationSpeedMultiplier));

            vel += ((((Vector2)ground.transform.right * forwardVel)));

            Vector2 save = Vector2.zero;
            vel = Vector2.SmoothDamp(rb.velocity, vel, ref save, 1/acceleration);

            if (jumping)
            {
                if (vel.y < 0)
                    vel.y = 0;
                else
                    vel.y *= 0.5f;

                vel += jumpVelocity * jumpMultiplier;
            }

            rb.velocity = vel;




            //forwardMomentum = velChange;
        }
        else
        {
            //apply air drag
            rb.velocity = new Vector2(rb.velocity.x * airDrag, rb.velocity.y);

        }

    }

    public virtual void Land()
    {
        jumping = false;
    }

    private void FindGround()
    {
        Collider2D col = null;

        Collider2D[] cols = null;
        cols = Physics2D.OverlapBoxAll(
            transform.position + groundCheck,
            new Vector2(0.5f, 1f),
            0);

        foreach (Collider2D c in cols)
        {
            if (c.name.Contains("collider"))
            {
                col = c;
                break;
            }
        }

        if (!grounded)
        {
            if (!(col == null))
                Land();
        }

        grounded = !(col == null);
        if (grounded)
        {
            ground = col.gameObject;
        }
        else
        {
            ground = null;
        }
    }

    private float calculateSlopeSpeed()
    {
        float groundRotation = ground.transform.localRotation.eulerAngles.z;

        float result = -(float)Math.Sin((groundRotation / 180) * Math.PI);
        if (result > 0)
            return result;
        else return 0;

    }

    private float calculateRotationSpeed()
    {
        float groundRotation = -ground.transform.localRotation.eulerAngles.z;
        float playerRotation = -transform.localRotation.eulerAngles.z;

        return 1 + (float)Math.Sin(((playerRotation - groundRotation) / 180) * Math.PI);
    }

    private void Gravity()
    {
        //Simulate gravity
        if (!grounded)
        {
            Vector2 vel = new Vector2(0,
                //Make player heavier if falling
                ((rb.velocity.y > 0) ? gravity : fallGravity) * Time.fixedDeltaTime);

            //Apply gravity
            rb.AddForce(vel, ForceMode2D.Force);

        }
    }

    public Quaternion lookAt(Vector3 target)
    {
        Vector3 diff = target - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0f, 0f, rot_z);
    }

    public void HoldWeapon(Weapon weapon)
    {
        if (weaponHolder.GetComponent<RuntimeWeapon>() != null)
            Destroy(weaponHolder.GetComponent<RuntimeWeapon>());

        RuntimeWeapon runtime = weaponHolder.AddComponent<RuntimeWeapon>();
        runtime.weapon = weapon;
    }

    public void DropWeapon()
    {
        //TODO null pointer exception
        if (weaponHolder.GetComponent<RuntimeWeapon>().weapon == WeaponManager.instance.noWeapon)
            return;
        GameObject drop = Instantiate(WeaponManager.instance.pickup);
        drop.transform.position = transform.position;
        drop.GetComponent<Pickup>().weaponId =
            Array.IndexOf(WeaponManager.instance.weapons, 
            weaponHolder.GetComponent<RuntimeWeapon>().weapon);

            HoldWeapon(WeaponManager.instance.noWeapon);
    }

    public void Jump()
    {
        if(grounded && canMove && canJump)
            jumping = true;
    }

    public void takeDamage()
    {
        if(weaponHolder.GetComponent<RuntimeWeapon>() != null)
            if (weaponHolder.GetComponent<RuntimeWeapon>().weapon != WeaponManager.instance.noWeapon)
            {
                DropWeapon();
                return;
            }

        Die(false);
    }

    public virtual void Die(bool canRespawn)
    {
        DropWeapon();

        canMove = false;
    }
}
