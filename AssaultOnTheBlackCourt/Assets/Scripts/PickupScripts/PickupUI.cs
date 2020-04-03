using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupUI : MonoBehaviour
{
    public Pickup linkedPickup; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemovePickup()
    {
        linkedPickup.ReverseEffect();

        Camera.main.GetComponent<UIManager>().ActivePickups.Remove((linkedPickup, gameObject)); 
        Destroy(linkedPickup.gameObject);
        Destroy(gameObject); 
    }
}
