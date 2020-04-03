using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    public bool pickedUp;
    public GameObject sprite;

    // Start is called before the first frame update
    protected void Start()
    {
        DontDestroyOnLoad(gameObject); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Dresden")
        {
            GameObject dresden = collision.gameObject;
            //dresden.GetComponent<Dresden>().activePickups.Enqueue(this);
            Camera.main.GetComponent<UIManager>().AddPickup(this);
            //Effect();
            sprite.SetActive(false);
        }
    }

    public abstract void Effect();

    public abstract void ReverseEffect(); 
}
