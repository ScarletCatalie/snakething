using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    //Move Speed
    public float moveWait = 0.1f;
    public int scoreValue = 10;            //Score Value
    public GameObject foodPrefab; 
    private float screenShakeOnWordComplete = 1f; //Death Shake Amount


    // Use this for initialization
    void Start ()
    {
        InvokeRepeating("Move", moveWait, moveWait);
        

    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void Move ()
    {
        Vector2 pos = transform.position; // enemy position
        Vector2 plpos = Player.Instance.transform.position; //players position
        Vector2 dir = ( plpos - pos ).normalized;
        //Debug.Log(dir);

        transform.Translate(dir);
    }

    void OnCollisionEnter2D(Collision2D BulletPrefab)
    {
        Debug.Log("Hit");

    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            collider.GetComponent<Player>().kill();

            Destroy(gameObject);
            Application.LoadLevel("Menu");
        }


    }

   public void Kill()
    {
        // Instntiate a copy of the food prefab
        ScoreManager.score += scoreValue;
        GameObject food = GameObject.Instantiate(foodPrefab);
        Vector2 pos = transform.position;
        food.transform.position = pos;
        Camera.main.GetComponent<Shaker>().StartShake(screenShakeOnWordComplete);

        Destroy(gameObject);        //Destroys
    }


}

