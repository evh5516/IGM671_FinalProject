using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LascielsCoin : Pickup
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Effect()
    {
        throw new System.NotImplementedException();
    }

    public override void ReverseEffect()
    {
        throw new System.NotImplementedException();
    }
}
