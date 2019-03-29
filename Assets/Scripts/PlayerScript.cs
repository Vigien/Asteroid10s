using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerScript : MonoBehaviour {

    private int playerID;

    public float speed = 10.0f;
    public float rotationSpeed = 10.0f;

    public Transform launcher;
    public GameObject projectile;
    private float attackCooldown = 0.2f;
    private bool canAttack = true;
    public ParticleSystem ParticleSystem;
    public ParticleSystemRenderer ParticleSystemRenderer;

    private Rigidbody2D myRigidbody2d;

    private string forwardInput;
    private string horizontalInput;
    private string attackInput;

    private Material myMaterial;

    private bool NotDead;

    private void Start()
    {
        myRigidbody2d = gameObject.GetComponent<Rigidbody2D>();
        NotDead = true; 
    }

    // Update is called once per frame
    void Update()
    {
        if (NotDead)
        {
            Movement();
            Attack();
        }
    }

    private void Movement()
    {
        if (Input.GetButton(forwardInput))
        {
            //not working
            myRigidbody2d.AddForce(transform.right * speed);
        }
        float rotate = Input.GetAxis(horizontalInput) * rotationSpeed;
        transform.Rotate(0, 0, -rotate);
    }

    private void Attack()
    {
        if (Input.GetButton(attackInput) && canAttack)
        {
            canAttack = false;
            GameObject obj = Instantiate(projectile, launcher.position, launcher.rotation) as GameObject;
            obj.GetComponent<SpriteRenderer>().material = myMaterial;

            Invoke("AttackCooldown", attackCooldown);
        }
    }

    private void AttackCooldown()
    {
        canAttack = true;
    }


    public void setControll(int playerID, string forwardInput, string horizontalInput, string attackInput)
    {
        this.playerID = playerID;
        this.forwardInput = forwardInput;
        this.horizontalInput = horizontalInput;
        this.attackInput = attackInput;
        myMaterial = gameObject.GetComponentInChildren<SpriteRenderer>().material;
        ParticleSystemRenderer.material = myMaterial;
    }

    public void die()
    {
        NotDead = false;
        Time.timeScale = 0.7f;
        transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
        ParticleSystem.Play();
        StartCoroutine(Endgame());
    }

    IEnumerator Endgame()
    {
        yield return new WaitForSeconds(1.5f);
        Time.timeScale = 1f;
        GameManager.getGameManger().endGame(playerID);
        yield break;
    }
}
