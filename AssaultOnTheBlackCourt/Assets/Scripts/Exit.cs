using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(12, 16);
        Physics2D.IgnoreLayerCollision(9, 16);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject dataManager = GameObject.Find("DataManager(Clone)");

        //try
        //{
        //    dataManager = GameObject.Find("DataManager");
        //}
        //catch
        //{
        //    dataManager = GameObject.Find("DataManager(Clone)");
        //}

        dataManager.GetComponent<DataManager>().DresdenHealth = collision.gameObject.GetComponent<Dresden>().Health;
        dataManager.GetComponent<DataManager>().ActivePickups = Camera.main.GetComponent<UIManager>().ActivePickups;
        dataManager.GetComponent<DataManager>().Score = Camera.main.GetComponent<UIManager>().Score;
        ParticleSystem[] lastParticles = GameObject.Find("DresdenParticles(Clone)").GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem ps in lastParticles)
        {
            dataManager.GetComponent<DataManager>().DresdenParticles.Add(ps.gameObject);
        }

        GameObject[] pickups = GameObject.FindGameObjectsWithTag("Pickup"); 
        
        for (int i = 0; i < pickups.Length; i++)
        {
            if (!pickups[i].GetComponent<Pickup>().pickedUp)
            {
                Destroy(pickups[i]);
            }
        }

        Camera.main.GetComponent<UIManager>().LoadNextLevel();
    }
}
