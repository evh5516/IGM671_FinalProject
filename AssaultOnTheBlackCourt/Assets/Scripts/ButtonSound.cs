using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public FMOD.Studio.EventInstance MenuInteraction;
    public FMOD.Studio.EventInstance MenuHover;
    public Button menuButton;
    public bool mouseOver = false;
    public bool triggered;

    // Start is called before the first frame update
    void Start()
    {
        MenuInteraction = FMODUnity.RuntimeManager.CreateInstance("event:/Misc/MenuDing");
        MenuHover = FMODUnity.RuntimeManager.CreateInstance("event:/Misc/MenuHover");
        menuButton.onClick.AddListener(PlaySound);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (mouseOver)
        {
            if (!triggered)
            {
                triggered = true;
                MenuHover.start();
            }
            if (triggered)
            {
                triggered = false;
                MenuHover.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }
        }*/
    }

    void PlaySound()
    {
        MenuInteraction.start();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
        MenuHover.start();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
    }
}
