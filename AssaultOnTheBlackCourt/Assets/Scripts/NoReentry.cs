using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoReentry : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
    }
}
