using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingTile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Physics2D.IgnoreLayerCollision(8, 14); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Dresden")
        {
            if (collision.gameObject.GetComponent<Dresden>().Health < collision.gameObject.GetComponent<Dresden>().MAX_HEALTH)
            {
                collision.gameObject.GetComponent<Dresden>().Health += Time.deltaTime * 12;
            }
            else if (collision.gameObject.GetComponent<Dresden>().Health >= collision.gameObject.GetComponent<Dresden>().MAX_HEALTH)
            {
                collision.gameObject.GetComponent<Dresden>().Health = collision.gameObject.GetComponent<Dresden>().MAX_HEALTH;
            }
            
        }
    }
}
