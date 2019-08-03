using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MovingItem
{

    public float speed;

    private Camera cam;
    private Vector2 dir; //Direction of touch
    private Vector3 touchStart; //Initial place finger touches
    private Vector3 touchEnd; //Initial place finger touches
    //private Vector3 velocity;
    private CircleCollider2D cc2d;

    private const int BOUNCE_MAX = 5;
    private int bounce = 0;

    private bool touchBegan = false;
    private float x; //x coord touch
    private float y; //y coord touch

    private Score score;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main; //Camera
        score = Score.Instance(); //Instance of score
        cc2d = GetComponent<CircleCollider2D>(); //Circle collider
        cc2d.enabled = false; //Lets everything move through it if false
    
        //Initialize variables for projectile
        x = transform.position.x;
        y = transform.position.y;
        vx = 0;
        vy = 0;
        velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (paused) return;

        if (!cc2d.enabled && Input.touchCount > 0)
        {    

            Touch myTouch = Input.touches[0];

            if(myTouch.phase == TouchPhase.Began)
            {
                //Initial touch
                x = myTouch.position.x;
                y = myTouch.position.y;
                touchStart = cam.ScreenToWorldPoint(new Vector3(x, y, cam.nearClipPlane));
                touchBegan = true;
            }

            else if (myTouch.phase == TouchPhase.Ended && touchBegan)
            {
                //Final touch
                x = myTouch.position.x;
                y = myTouch.position.y;
                touchEnd = cam.ScreenToWorldPoint(new Vector3(x, y, cam.nearClipPlane));

                //Set velocity direction to difference between two touches
                vx = touchEnd.x - touchStart.x;
                vy = touchEnd.y - touchStart.y;

                //If velocity is 0, set it to random values
                if(vx == 0 && vy == 0)
                {
                    float angle = Random.value * Mathf.PI;
                    vy = Mathf.Sin(angle);
                    vx = Mathf.Cos(angle);
                }
            
                //Enable the projectile
                velocity = speed * Vector3.Normalize(new Vector3(vx, vy, 0));
                cc2d.enabled = true;
                touchBegan = false;
                score.NewProj();
            }

        }

        else if(cc2d.enabled)
        {
            Move();//transform.position = transform.position + velocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obj = collision.collider.gameObject;
        if (obj.CompareTag("Background"))
        {
            //Bounce logic
            velocity.x *= -1;
            bounce++;

            //Destroy projectile if bounced too many times
            if(bounce > BOUNCE_MAX) 
            {
                Destroy(this.gameObject);
            }
        }
        else if(obj.CompareTag("RemoveProj") || obj.CompareTag("RemoveAll")) //Destroy projectile if out of bounds
        {
            Destroy(this.gameObject);
        }

    }
    
}
