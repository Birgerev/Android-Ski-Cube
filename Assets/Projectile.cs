using UnityEngine;

public class Projectile : MonoBehaviour {

    public float speed;

	// Use this for initialization
	void Start ()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * speed;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);

        if (col.gameObject.GetComponent<Skier>())
        {
            col.gameObject.GetComponent<Skier>().takeDamage();
        }
    }
}
