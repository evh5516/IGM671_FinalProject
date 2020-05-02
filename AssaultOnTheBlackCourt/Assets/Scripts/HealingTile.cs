using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingTile : MonoBehaviour
{

    public FMOD.Studio.EventInstance HealInProgress;
    public bool HealingBool;
    public FMOD.Studio.EventInstance HealComplete;
    public bool HealedBool;

    // Start is called before the first frame update
    void Start()
    {
        //Physics2D.IgnoreLayerCollision(8, 14);
        HealInProgress = FMODUnity.RuntimeManager.CreateInstance("event:/Passive/HealingCharge");
        HealComplete = FMODUnity.RuntimeManager.CreateInstance("event:/Passive/HealingCharged");
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
                if (!HealingBool)
                {
                    HealingBool = true;
                    HealInProgress.start();
                }
                if (HealedBool)
                {
                    HealedBool = false;
                    HealComplete.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                }
                collision.gameObject.GetComponent<Dresden>().Health += Time.deltaTime * 12;
            }
            else if (collision.gameObject.GetComponent<Dresden>().Health >= collision.gameObject.GetComponent<Dresden>().MAX_HEALTH)
            {
                collision.gameObject.GetComponent<Dresden>().Health = collision.gameObject.GetComponent<Dresden>().MAX_HEALTH;
                if (HealingBool)
                {
                    HealingBool = false;
                    HealInProgress.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                }
                if (!HealedBool)
                {
                    HealedBool = true;
                    HealComplete.start();
                }
            }
            
        }
    }
    private void OnDestroy()
    {
        if (HealInProgress.isValid())
        {
            HealInProgress.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            HealInProgress.release();
        }
        UnityEngine.Debug.Log("Destroyed");
    }
}
