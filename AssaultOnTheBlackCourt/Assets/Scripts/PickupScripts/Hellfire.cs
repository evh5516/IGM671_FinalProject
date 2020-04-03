using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hellfire : Pickup
{
    private GameObject dresden;

    // Start is called before the first frame update
    void Start()
    {
        dresden = GameObject.Find("Dresden");
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Effect()
    {
        List<float> spellDamages = GameObject.FindGameObjectWithTag("Player").GetComponent<SpellCast>().damages;

        for (int i = 0; i < spellDamages.Count; i++)
        {
            spellDamages[i] *= 1.5f;
        }

        GameObject.FindGameObjectWithTag("Player").GetComponent<Dresden>().MAX_HEALTH /= 2;

        //changing particles
        ParticleSystem.ShapeModule shape = gameObject.GetComponentInChildren<ParticleSystem>().shape;
        shape.angle = 30.99963f;
        ParticleSystem.MainModule main = gameObject.GetComponentInChildren<ParticleSystem>().main;
        main.startSize = 0.1f;

        GameObject psObject = gameObject.GetComponentInChildren<ParticleSystem>().gameObject;
        psObject.transform.parent = GameObject.Find("DresdenParticles(Clone)").transform;
        psObject.GetComponent<ParticleSystem>().Stop();
        //setting parent
        Instantiate(psObject).transform.parent = GameObject.FindGameObjectWithTag("Player").transform;

        //setting local position
        ParticleSystem[] pss = GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem ps in pss)
        {
            ps.gameObject.transform.localPosition = new Vector3(-0.066f, -0.485f, 0);
        }
    }

    public override void ReverseEffect()
    {
        List<float> spellDamages = GameObject.FindGameObjectWithTag("Player").GetComponent<SpellCast>().damages;

        for (int i = 0; i < spellDamages.Count; i++)
        {
            spellDamages[i] /= 1.5f;
        }

        GameObject.FindGameObjectWithTag("Player").GetComponent<Dresden>().MAX_HEALTH *= 2;

        //setting local position
        ParticleSystem[] pss = GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < pss.Length; i++)
        {
            if (pss[i].gameObject.name == "HellfireParticles")
            {
                Destroy(pss[i].gameObject);
                return;
            }
        }
    }
}
