using UnityEngine;
using System.Collections;

public class FoodScript : MonoBehaviour
{


    public float moveWait = 0.1f;
    public float moveAmount = 1.0f;
    



    // Use this for initialization
    void Start()
    {
        InvokeRepeating("Move", moveWait, moveWait);


    }

    // Update is called once per frame
    void Update()
    {

    }

    void Move()
    {
        Vector2 pos = transform.position; // enemy position
        Vector2 plpos = Player.Instance.transform.position; //players position
        Vector2 dir = (plpos - pos).normalized;
        //Debug.Log(dir);

        transform.Translate(dir);
    }
}