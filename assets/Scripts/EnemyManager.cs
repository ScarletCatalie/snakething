using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{

    public GameObject enemyPrefab;
    public float minspawn;
    public float maxspawn;
    private float timer;
    

	// Use this for initialization
	void Start ()
    {
        //timer = Mathf.
        timer = Random.Range(minspawn, maxspawn);
	}
	
	// Update is called once per frame
	void Update ()
    {
    
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = Random.Range(1f, 5f);
            // Spawn an enemyPrefab at a random location within the bounds.
            Instantiate(enemyPrefab,
                new Vector3(Mathf.Round(Random.Range(-70f, 70f)), Mathf.Round(Random.Range(-40f, 40f)), 0f), 
                Quaternion.identity);
        }       

          
	}
}
