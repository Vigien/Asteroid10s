using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {

    public float velocity = 10f;
    private Rigidbody2D myRigidbodyd2;

    // Use this for initialization
    void Start () {
        myRigidbodyd2 = gameObject.GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {
        myRigidbodyd2.velocity = transform.right * velocity;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerScript>().die();
        }
        else if(collision.tag == "Asteroid")
        {
            collision.GetComponent<AsteroidScript>().TakeDamage();
            Destroy(gameObject);
        }
        else if(collision.tag == "Bullet")
        {
            Destroy(gameObject);
        }
    }
}
