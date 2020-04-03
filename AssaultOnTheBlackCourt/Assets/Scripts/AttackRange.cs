using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    private bool inAttackRange;
    public GameObject dresden;
    public float attackTimer;

    // Start is called before the first frame update
    void Start()
    {     
    }

    // Update is called once per frame
    void Update()
    {

        if (inAttackRange == true)
        {
            if (attackTimer < 1)
            {
                attackTimer += Time.deltaTime;
            }
            else if (attackTimer >= 1)
            {
                dresden.GetComponent<Dresden>().Health -= 25;
                attackTimer = 0;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (dresden.GetComponent<BoxCollider2D>() == other)
        {
            Debug.Log("entered");
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        //if (other.tag == "Player")
        //{
        //    inAttackRange = false;
        //}
        if (dresden.GetComponent<BoxCollider2D>() == other)
        {
            Debug.Log("exited");
        }
    }
}
