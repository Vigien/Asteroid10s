using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour {

    private float rotationSpeed;
    private float speed;
    private float scale;
    public int health;

    public GameObject Particles;

    private Rigidbody2D myRigidbody2D;

	// Use this for initialization
	void Start () {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        rotationSpeed = Random.Range(-18f, 18f);
        speed = Random.Range(100f,200f);
        scale = Random.Range(0.5f,3f);
        health = (int)Mathf.Ceil(scale);

        transform.localScale = new Vector3(scale, scale, scale);
    }
	
	// Update is called once per frame
	void Update () {
        //transform.Rotate(new Vector3(0, 0, rotationSpeed));

        myRigidbody2D.velocity = transform.right * speed * Time.deltaTime;
        
    }

    public void TakeDamage()
    {
        health--;
        if (health <= 0)
        {
            var particle = Instantiate(Particles, transform.position, transform.rotation) as GameObject;
            Destroy(particle, 2f);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerScript>().die();
        }
    }
}
