using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{

    public FMOD.Studio.EventInstance MusicTrack;

    // Start is called before the first frame update
    void Start()
    {
        MusicTrack = FMODUnity.RuntimeManager.CreateInstance("event:/Misc/Music");
        MusicTrack.start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        GameObject[] OBJS = GameObject.FindGameObjectsWithTag("Music");
        if (OBJS.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
