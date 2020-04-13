using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    // Start is called before the first frame update
    #region Fields
    [SerializeField]
    private float dresdenHealth;
    [SerializeField]
    private AudioSource soundClip;
    private int score;
    private Queue<(Pickup, GameObject)> activePickups = new Queue<(Pickup, GameObject)>();
    private List<GameObject> dresdenParticles = new List<GameObject>();
    #endregion

    #region Properties
    public float DresdenHealth
    {
        get { return dresdenHealth; }
        set { dresdenHealth = value; }
    }
    public Queue<(Pickup, GameObject)> ActivePickups
    {
        get { return activePickups; }
        set { activePickups = value; }
    }
    public int Score
    {
        get { return score; }
        set { score = value; }
    }
    public List<GameObject> DresdenParticles
    {
        get { return dresdenParticles; }
        set { dresdenParticles = value; }
    }
    #endregion
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(Instantiate(new GameObject("DresdenParticles")));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopMusic(AudioClip clip)
    {
        soundClip.clip = clip;
        soundClip.Stop();
    }
}
