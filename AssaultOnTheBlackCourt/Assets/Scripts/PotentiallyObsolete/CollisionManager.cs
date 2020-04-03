using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour {

    #region Fields
    public GameObject dresden;
    public List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> projectiles = new List<GameObject>(); 

    public CollisionDetection collisionDetection;
    public GameObject agentManager;

    public bool showDebugLines;
    #endregion

    // Use this for initialization
    void Start ()
    {
        dresden = agentManager.GetComponent<AgentManager>().dresden;
        enemies = agentManager.GetComponent<AgentManager>().enemies;

        collisionDetection = gameObject.GetComponent<CollisionDetection>();
    }

    // Update is called once per frame
    void Update()
    {
        ////and applies the AABB collision method to all of them 
        ////to check if they're colliding
        //for (int j = 0; j < enemies.Count; j++)
        //{
        //    bool colliding = collisionDetection.AABBCollision(enemies[j], dresden);
        //    dresden.GetComponent<MeshInfo>().collidingBools.Add(colliding);

        //    for (int i = 0; i < projectiles.Count; i++)
        //    {
        //        colliding = collisionDetection.AABBCollision(enemies[j], projectiles[i]);
        //        enemies[j].GetComponent<MeshInfo>().collidingBools.Add(colliding); 
        //    }
        //}

        ////Collision resolution

        //for (int i = 0; i < enemies.Count; i++)
        //{
        //    for (int j = 0; j < projectiles.Count; j++)
        //    {
        //        if (enemies[i].GetComponent<MeshInfo>().collidingBools[j])
        //        {
        //            enemies[i].GetComponent<Enemy>().Health -= projectiles[j].GetComponent<Projectile>().Damage;
        //            projectiles[j].GetComponent<Projectile>().DestroySpell();
        //            j--;
        //        }
        //    }
        //}
        //if (dresden.GetComponent<MeshInfo>().collidingBools[j])
        //{


        //    //GameObject newZombie = Instantiate(zombiePrefab, humans[i].transform.position, humans[i].transform.rotation);
        //    //newZombie.transform.position = new Vector3(newZombie.transform.position.x, newZombie.transform.localScale.y / 2, newZombie.transform.position.z);
        //    //newZombie.GetComponent<Zombie>().humans = humans;
        //    //zombies.Add(newZombie);
        //    //newZombie.GetComponent<Zombie>().zombies = zombies;
        //    //newZombie.GetComponent<Zombie>().obstacles = humans[i].GetComponent<Human>().obstacles;
        //    //newZombie.GetComponent<Zombie>().showDebugLines = agentManager.GetComponent<AgentManager>().showDebugLines;
        //    //GameObject infectedHuman = humans[i];
        //    //humans.RemoveAt(i);
        //    //Destroy(infectedHuman);
        //    //i--;
        //    //break;
        //}
    }
}
