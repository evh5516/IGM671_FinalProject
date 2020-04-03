using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages Audio in the scene
/// 
/// author: Trenton Plager tlp6760@rit.edu
/// </summary>
public class AudioManager : MonoBehaviour
{
    #region Fields
    //[SerializeField]
    //private AudioClip dungeonAmbience, buttonClick, demonLaugh;     //audio clips for the button, demon laugh, and the ambience
    //private AudioSource globalSource, stoneSource;                  //audio sources to manage what plays when
    //[SerializeField]
    //private GameObject stoneSourceObject;                           //the game object containing the stone audio source
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        ////initialization
        //globalSource = gameObject.GetComponent<AudioSource>();
        //stoneSource = stoneSourceObject.GetComponent<AudioSource>();

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    ///// <summary>
    ///// Plays the button click one time
    ///// </summary>
    //public void PlayButtonClick()
    //{
    //    globalSource.PlayOneShot(buttonClick); 
    //}

    ///// <summary>
    ///// Plays the demon laugh one time
    ///// </summary>
    //public void PlayDemonLaugh()
    //{
    //    globalSource.PlayOneShot(demonLaugh); 
    //}

    ///// <summary>
    ///// Tells the stone source to start playing
    ///// </summary>
    //public void PlayStoneGrinding()
    //{
    //    stoneSource.Play(); 
    //}

    ///// <summary>
    ///// Tells the stone source to stop playing
    ///// </summary>
    //public void StopStoneGrinding()
    //{
    //    stoneSource.Stop(); 
    //}
}
