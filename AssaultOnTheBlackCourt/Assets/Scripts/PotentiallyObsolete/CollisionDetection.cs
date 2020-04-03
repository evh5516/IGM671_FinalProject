using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Method to run AABB Collision between 2 supplied GameObjects
    /// </summary>
    /// <param name="object1">The first object to use for collision checking</param>
    /// <param name="object2">The second object to use for collision checking</param>
    /// <returns></returns>
    public bool AABBCollision(GameObject object1, GameObject object2)
    {
        MeshInfo info1 = object1.GetComponent<MeshInfo>();
        MeshInfo info2 = object2.GetComponent<MeshInfo>();

        if (info1.min.x < info2.max.x && info1.max.x > info2.min.x &&
            info1.min.y < info2.max.y && info1.max.y > info2.min.y)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Method to run Bounding Circle Collision between 2 supplied GameObjects
    /// </summary>
    /// <param name="object1">The first object to use for collision checking</param>
    /// <param name="object2">The second object to use for collision checking</param>
    /// <returns></returns>
    public bool CircleCollision(GameObject object1, GameObject object2)
    {
        MeshInfo info1 = object1.GetComponent<MeshInfo>();
        MeshInfo info2 = object2.GetComponent<MeshInfo>();

        Vector3 centerToCenter = info1.center - info2.center;
        float centerMagSquared = centerToCenter.sqrMagnitude;

        if (centerMagSquared < (info1.radius + info2.radius))
        {
            return true;
        }
        return false;
    }
}
