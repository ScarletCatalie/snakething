using UnityEngine;
using System.Collections;

public class bulletScript : MonoBehaviour {


    public float moveWait = 0.1f;
    public float moveAmount = 1.0f;
    public Vector2 direction;
    public float lifemax = 5.0f;
    private float lifespan = 0;

	// Use this for initialization
	void Start () {
        // Player Speed (time between positions)
        //InvokeRepeating("Move", moveWait, moveAmount);

        lifespan = 0;
    }
	
	// Update is called once per frame
	void Update () {
        // Move head into new direction
        transform.Translate(moveAmount * Time.deltaTime * direction);

        lifespan += Time.deltaTime;
        if (lifespan >= lifemax)
        {
            Destroy(base.gameObject);
        }
    }


    // Kill enemy on contact, remove bullet
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Enemy")
        {
            collider.GetComponent<Enemy>().Kill();
            Destroy(gameObject);
        }


    }

}
