using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{

    public FMOD.Studio.EventInstance MenuInteraction;
    public Button menuButton;

    // Start is called before the first frame update
    void Start()
    {
        MenuInteraction = FMODUnity.RuntimeManager.CreateInstance("event:/Misc/MenuDing");
        menuButton.onClick.AddListener(PlaySound);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlaySound()
    {
        MenuInteraction.start();
    }
}
