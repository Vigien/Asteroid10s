using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLimitScript : MonoBehaviour {
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerScript>().die();
        }
        else
            Destroy(collision.gameObject);
    }
}
