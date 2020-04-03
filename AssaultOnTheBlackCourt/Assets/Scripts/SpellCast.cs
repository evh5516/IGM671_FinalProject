using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCast : MonoBehaviour
{
    [SerializeField]
    private float holdStrength;

    public List<float> powerLimits;
    public List<float> damages; 

    public GameObject[] spellPrefabs;
    public int currentSpellIndex;

    public GameObject dresden;

    public int powerDraining = 0;

    public GameObject idleAnim;
    public GameObject shootAnim;
    private float shootTime = 0.0f;

    [SerializeField]
    private bool enemy;
    private bool active; 

    public float HoldStrength
    {
        get { return holdStrength; }
    }

    public List<float> PowerLimits
    {
        get { return powerLimits; }
    }

    public bool Active
    {
        get { return active; }
        set { active = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        dresden = GameObject.Find("Dresden"); 
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.GetComponent<Vehicle>().Paused)
        {
            if (!enemy)
            {
                if (Input.GetMouseButton(0))
                {
                    holdStrength += Time.deltaTime;
                }

                if (Input.GetMouseButtonUp(0))
                {
                    GameObject spell = Instantiate(spellPrefabs[0], dresden.transform.position + dresden.GetComponent<Dresden>().velocity.normalized, dresden.transform.rotation);
                    //Camera.main.GetComponent<CollisionManager>().projectiles.Add(spell);

                    Vector3 velocity = (new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0) - dresden.transform.position).normalized;
                    velocity *= spell.GetComponent<Projectile>().Speed;

                    if (Vector3.Dot(velocity, dresden.GetComponent<Dresden>().velocity) < 0)
                    {
                        dresden.GetComponent<Dresden>().velocity = -dresden.GetComponent<Dresden>().velocity;
                    }

                    spell.GetComponent<Projectile>().Velocity = velocity;

                    if (holdStrength > 0 && holdStrength < powerLimits[0])
                    {
                        spell.transform.localScale = new Vector3(1, 1, 1);

                        spell.GetComponent<Projectile>().Damage = 25;
                        holdStrength = 0;
                    }
                    else if (holdStrength > powerLimits[0] && holdStrength < powerLimits[1])
                    {
                        spell.transform.localScale = new Vector3(0.75f, 0.75f, 1);
                        //Level 2

                        spell.GetComponent<Projectile>().Damage = 75;
                        holdStrength = 0;
                    }
                    else if (holdStrength > powerLimits[1])
                    {
                        spell.transform.localScale = new Vector3(0.5f, 0.5f, 1);

                        //Level 3

                        spell.GetComponent<Projectile>().Damage = 150;
                        holdStrength = 0;
                    }

                    dresden.GetComponent<Dresden>().Health -= (10 * powerDraining);
                }

                shootTime += Time.deltaTime;

                if (shootTime < 0.3f && shootTime != 0.0f)
                {
                    shootTime += Time.deltaTime;
                }
                else
                {
                    shootTime = 0.0f;
                    idleAnim.SetActive(true);
                    shootAnim.SetActive(false);
                }
            }
            else
            {
                if (active && shootTime >= 1.0f)
                {
                    GameObject spell = Instantiate(spellPrefabs[0], gameObject.transform.position + gameObject.GetComponent<Enemy>().velocity.normalized, gameObject.transform.rotation);
                    //Camera.main.GetComponent<CollisionManager>().projectiles.Add(spell);

                    Vector3 velocity = (new Vector3(dresden.transform.position.x, dresden.transform.position.y, 0) - gameObject.transform.position).normalized;
                    velocity *= spell.GetComponent<Projectile>().Speed;

                    if (Vector3.Dot(velocity, dresden.GetComponent<Dresden>().velocity) < 0)
                    {
                        gameObject.GetComponent<Dresden>().velocity = -gameObject.GetComponent<Enemy>().velocity;
                    }

                    spell.GetComponent<Projectile>().Velocity = velocity;

                    spell.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

                    spell.GetComponent<Projectile>().Damage = 25;

                    shootTime = 0; 
                }

                shootTime += Time.deltaTime;
            }
        }
    }
}
