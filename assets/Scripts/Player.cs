using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class Player : MonoBehaviour
{
    // Current Movement Direction - Default Right (Maybe randomise later???)
    Vector2 dir = Vector2.right;

    public float[] bounds = new float[4];
    public float moveWait = 0.1f;
    private float screenShakeOnWordComplete = 0.5f;
    public float moveAmount = 1.0f;
    public GameObject BulletPrefab;
    public int EatValue = 10; //how much food is Worth

    // Did the snake eat something?
    bool ate = false;
    // Tail Prefab
    public GameObject tailPrefab;
    List<Transform> tail = new List<Transform>();

    static Player _instance;


    // Oliver code: Something to do with tracking the player for the enemy
    public static Player Instance
    {
        get
        {
            return _instance;
        }
        private set { }
    }
    
    void Awake()
    {
        if(_instance != null &&  _instance != this)
        {
            Destroy(this.gameObject);

        }
        else
        {
            _instance = this;
        }
    }


    //Destroy Player
    public void kill()
    {
//        Debug.Log("Contact");
        Destroy(gameObject);
        
    }

    ///



    void Start()
    {
        // Player Speed (time between positions)
        InvokeRepeating("Move", moveWait, moveWait);
        Cursor.visible = false;
    }

    void Update()
    {
        // Move in a new Direction?
        //UP
        if (Input.GetKey(KeyCode.D))
            dir = Vector2.right;

        //DOWN
        else if (Input.GetKey(KeyCode.S))
            dir = -Vector2.up;

        //LEFT
        else if (Input.GetKey(KeyCode.A))
            dir = -Vector2.right;

        //RIGHT
        else if (Input.GetKey(KeyCode.W))
            dir = Vector2.up;

        // WALL BOUNCE
        // UP
        if (transform.position.y >= bounds[0])
        {
           // Debug.Log("up");
            dir = -Vector2.up;
        }


        if (transform.position.y <= bounds[1])
        {
            //Debug.Log("down");
            dir = Vector2.up;
        }


        if (transform.position.x <= bounds[2])
        {
            //Debug.Log("left");
            dir = Vector2.right;
        }


        if (transform.position.x >= bounds[3])
        {
            //Debug.Log("right");
            dir = -Vector2.right;
        }

        //QUIT GAME
        if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }

        //RESTART
        if (Input.GetKey(KeyCode.R))
        {
            Application.LoadLevel("Main");
        }


        //Shot a gun click
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 myPos = transform.position;

            Vector2 direction = mousePos - myPos;
            direction.Normalize();

            GetComponent<AudioSource>().Play();

            // Instntiate a copy of the Bullet prefab
            GameObject bullet = GameObject.Instantiate(BulletPrefab);

            bullet.transform.position = myPos;
            bullet.GetComponent<bulletScript>().direction = direction;

            Camera.main.GetComponent<Shaker>().StartShake(screenShakeOnWordComplete);
        }
    }


    //collide with food to grow
    void OnTriggerEnter2D(Collider2D coll)
    {
        // Food?
        if (coll.name.StartsWith("FoodPrefab"))
        {
            // Get longer in next Move call
            ate = true;
            ScoreManager.score += EatValue;

            // Remove the Food
            Destroy(coll.gameObject);
        }
        // Collided with Tail or Border
        else
        {
            // ToDo 'You lose' screen
        }
    }


   public void Kill()
    {
        Destroy(gameObject);
    }



    void Move()
    {
        // Save current position (gap will be here)
        Vector2 v = transform.position;

        // Move head into new direction (now there is a gap)
        transform.Translate(dir);

        // Ate something? Then insert new Element into gap
        if (ate)
        {
            // Load Prefab into the world
            GameObject g = (GameObject)Instantiate(tailPrefab,
                                                  v,
                                                  Quaternion.identity);

            // Keep track of it in our tail list
            tail.Insert(0, g.transform);

            // Reset the flag
            ate = false;
        }
        // Do we have a Tail?
        else if (tail.Count > 0)
        {
            // Move last Tail Element to where the Head was
            tail.Last().position = v;

            // Add to front of list, remove from the back
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
    }
}