using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawnerScript : MonoBehaviour {

    public GameObject asteroid;

	// Use this for initialization
	void Start () {
        InvokeRepeating("spawnAsteroid", 0.5f, Random.Range(1f,4f));
	}
	
	void spawnAsteroid()
    {
        Vector3 diff = new Vector3(0,0, transform.position.z) - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        var randomRot = Quaternion.Euler(0f, 0f, rot_z + Random.Range(-45f, 45f));

        Instantiate(asteroid, transform.position, randomRot);
    }
}
