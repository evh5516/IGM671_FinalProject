﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private bool game; 
    [SerializeField]
    private GameObject player;        //Public variable to store a reference to the player game object
    public List<GameObject> enemies; 
    private Vector3 offset;            //Private variable to store the offset distance between the player and camera
    private int score = 0; 
    
    [SerializeField]
    private Canvas pauseCanvas;
    private bool paused;
    [SerializeField]
    private Slider healthSlider;
    [SerializeField]
    private Slider chargeBar;
    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private GameObject blankPickupPrefab;
    
    private Queue<(Pickup, GameObject)> activePickups = new Queue<(Pickup, GameObject)>();
    
    private Vector3[] pickupPositions = {
        new Vector3(-100, 0, 0),
        new Vector3(0, 0, 0), 
        new Vector3(100, 0, 0) 
    };

    private int nextPickupPos = 0; 

    public Queue<(Pickup, GameObject)> ActivePickups
    {
        get { return activePickups; }
    }
    public int Score
    {
        get { return score; }
        set { score = value; }
    }
    public bool Game
    {
        get { return game; }
        set { game = value; }
    }

    public FMOD.Studio.EventInstance MenuInteraction;
    public FMOD.Studio.EventInstance BackgroundAmbiance;
    public FMOD.Studio.EventInstance BackgroundMusic;

 

    public FMOD.Studio.EventInstance PauseSnap;

    // Use this for initialization
    void Start()
    {
        if (game)
        {
            //Calculate and store the offset value by getting the distance between the player's position and camera's position.
            offset = transform.position - player.transform.position;

            GameObject[] temp = GameObject.FindGameObjectsWithTag("Enemy");
            
            foreach(GameObject e in temp)
                enemies.Add(e); 
        }

        Queue<(Pickup, GameObject)> lastActivePickups = GameObject.Find("DataManager(Clone)").GetComponent<DataManager>().ActivePickups;
        List<GameObject> lastParticles = GameObject.Find("DataManager(Clone)").GetComponent<DataManager>().DresdenParticles; 

        for (int i = 0; i < lastActivePickups.Count; i++)
        {
            AddPickup(lastActivePickups.Dequeue().Item1);
            i--;
        }

        for (int i = 0; i < lastParticles.Count; i++)
        {
            GameObject ps = Instantiate(lastParticles[i]);
            ps.transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
            ps.gameObject.transform.localPosition = new Vector3(-0.066f, -0.485f, -1.0f);
        }

        score = GameObject.Find("DataManager(Clone)").GetComponent<DataManager>().Score;
        scoreText.text = score.ToString();

        BackgroundAmbiance = FMODUnity.RuntimeManager.CreateInstance("event:/Misc/OfficeBackground");
        BackgroundMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Misc/Music");

        PauseSnap = FMODUnity.RuntimeManager.CreateInstance("snapshot:/PauseSnapshot");

        BackgroundAmbiance.start();
    }

    private void Update()
    {
        if (game)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            {
                if (!paused)
                {
                    PauseGame();
                }
                else
                {
                    ResumeGame();
                }
            }

            healthSlider.value = player.GetComponent<Dresden>().Health / player.GetComponent<Dresden>().MAX_HEALTH;
            chargeBar.value = player.GetComponent<SpellCast>().HoldStrength / player.GetComponent<SpellCast>().PowerLimits[1];
            Image[] tempComponents = chargeBar.GetComponentsInChildren<Image>();
            if (chargeBar.value >= 0.5f && chargeBar.value < 1.0f)
            { 
                tempComponents[0].color = new Color(0, 1, 0, 0.5f);
                tempComponents[1].color = Color.green;
            }
            else if (chargeBar.value < 0.5f)
            {
                tempComponents[0].color = new Color(0, 1, 1, 0.5f);
                tempComponents[1].color = Color.cyan;
            }
            else
            {
                tempComponents[1].color = Color.yellow;
            }
        }
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        if (game) 
            // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
            transform.position = player.transform.position + offset;
    }

    public void LoadNewScene(string sceneName)
    {
        
        if (sceneName == "Level1") GameObject.Find("DataManager(Clone)").GetComponent<DataManager>().Score = 0;

        //Menu Button Sound
        //if (sceneName == "Instructions" || sceneName == "Main Menu" || sceneName == "Credits" || sceneName == "Tutorial") MenuInteraction.start();
        // Adjusting parameter of music and background depending on level
        if (sceneName == "Tutorial" || sceneName == "Level2" || sceneName == "Level4" || sceneName == "Level6" || sceneName == "Level6" || sceneName == "Level8" || sceneName == "Level10")
        {
            BackgroundAmbiance.setParameterByName("BackgroundParam", 0);
            BackgroundMusic.setParameterByName("MusicParam", 25);
        }
        else if (sceneName == "Level1" || sceneName == "Level3" || sceneName == "Level5" || sceneName == "Level7" || sceneName == "Level9" || sceneName == "Level11")
        {
            BackgroundAmbiance.setParameterByName("BackgroundParam", 1);
            if (sceneName == "Level5" ||  sceneName == "Level11")
            {
                BackgroundMusic.setParameterByName("MusicParam", 100);
            }
            else
            {
                BackgroundMusic.setParameterByName("MusicParam", 50);
            }
        }

        else
        {
            BackgroundAmbiance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        //Footsteps.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void PauseGame()
    {
        player.GetComponent<Dresden>().Paused = true;
        foreach (GameObject e in enemies)
            e.GetComponent<Enemy>().Paused = true;

        //PlayerBus.setMute(true);
        //EnemyBus.setMute(true);
        //PassiveBus.setMute(true);

        PauseSnap.start();

        pauseCanvas.enabled = true;

        paused = true;
    }

    public void ResumeGame()
    {
        MenuInteraction.start();
        player.GetComponent<Dresden>().Paused = false;
        foreach (GameObject e in enemies)
            e.GetComponent<Enemy>().Paused = false;

        //PlayerBus.setMute(false);
        //EnemyBus.setMute(false);
        //PassiveBus.setMute(false);

        PauseSnap.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        pauseCanvas.enabled = false;

        paused = false;
    }

    public void QuitGame()
    {
        MenuInteraction.start();
        Application.Quit(); 
    }

    public void AddPickup(Pickup newPickup)
    {
        if (nextPickupPos == 3)
        {
            nextPickupPos = 0;
            (Pickup, GameObject) removedPickup = activePickups.Dequeue();
            removedPickup.Item1.ReverseEffect();
            //player.GetComponent<Dresden>().activePickups.Dequeue(); 
            Destroy(removedPickup.Item1.gameObject);
            Destroy(removedPickup.Item2);
        }
        newPickup.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        newPickup.gameObject.GetComponent<BoxCollider2D>().enabled = false;

        newPickup.Effect(); 

        GameObject newPickupUI = Instantiate(blankPickupPrefab, pickupPositions[nextPickupPos], Quaternion.Euler(0, 0, 0));
        newPickupUI.GetComponent<Image>().sprite = newPickup.gameObject.GetComponent<SpriteRenderer>().sprite;

        newPickupUI.gameObject.transform.localScale = new Vector3(0.3f, 0.3f, 1);
        newPickupUI.gameObject.transform.SetParent(GameObject.Find("ActiveItems").transform);
        newPickupUI.gameObject.transform.localPosition = pickupPositions[nextPickupPos];

        newPickup.pickedUp = true;

        activePickups.Enqueue((newPickup, newPickupUI));

        nextPickupPos++; 
    }

    public void EnemyKilled(GameObject enemyKilled)
    {
        enemies.Remove(enemyKilled);
        score += 10;
        scoreText.text = score.ToString(); 
    }
}
