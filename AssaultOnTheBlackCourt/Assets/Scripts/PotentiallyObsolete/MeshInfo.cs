using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MeshInfo : MonoBehaviour {

    #region Fields
    public GameObject obj;
    public SpriteRenderer spriteRenderer;
    public Vector3 center;
    public Vector3 min;
    public Vector3 max;
    public Vector3 size;
    public Vector3 extents;

    public float radius;

    public List<bool> collidingBools = new List<bool>();
    #endregion

    // Use this for initialization
    void Start ()
    {
        if (obj == null)
        {
            obj = gameObject;
        }

        if (spriteRenderer == null) spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        center = spriteRenderer.bounds.center;
        min = spriteRenderer.bounds.min;
        max = spriteRenderer.bounds.max;
        size = spriteRenderer.bounds.size;
        extents = spriteRenderer.bounds.extents;
    }
	
	// Update is called once per frame
	void Update ()
    {
        center = spriteRenderer.bounds.center;
        min = spriteRenderer.bounds.min;
        max = spriteRenderer.bounds.max;
        collidingBools.Clear();
    }
}
