using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    public float radius;
    public int obstacleIndex; 

	// Use this for initialization
	void Start () {
        //inTrigger = false; 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().inTrigger = true;
            collision.gameObject.GetComponent<Enemy>().triggeredObstacles.Add(obstacleIndex); 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().inTrigger = false;
            collision.gameObject.GetComponent<Enemy>().triggeredObstacles.Remove(obstacleIndex); 
        }
    }
}
