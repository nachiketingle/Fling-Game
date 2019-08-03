using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MovingItem
{

    public float speed; //total speed of enemy
    public GameObject bg; //background

    private Score score;
    private Rigidbody2D rb;
    private float angle; //angle of movement
   

    // Start is called before the first frame update
    void Start()
    {
        //Get ridigdbody and make it not rotate
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

        //Get instance of score
        score = Score.Instance();//bg.GetComponent<Score>();

        //Initialize values of enemmy
        speed = speed * Mathf.Pow(1.1F, score.GetLevel() - 1); //Calculate speed based on level
        angle = Random.value * Mathf.PI; //Random angle from 0 to pi
        vy = -1 * Mathf.Sin(angle);
        vx = Mathf.Cos(angle);
        velocity = speed * new Vector3(vx, vy, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Movement Logic
        //transform.position = transform.position + speed * new Vector3(vx, vy, 0);
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {   
        GameObject obj = collision.collider.gameObject;
        //Bounce of wall
        if(obj.CompareTag("Background"))
        {
            velocity.x *= -1;
        }
        else if (obj.CompareTag("Projectile"))
        {
            //Destroy this and increase score
            score.IncreaseScore();
            Destroy(gameObject);
        }
        else if (obj.CompareTag("RemoveAll"))
        {
            //Enemy reached end of screen, take damage
            score.TakeDamage();
        }
    }

}
