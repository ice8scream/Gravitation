using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forecast : SpaceTimeLine
{
    SpaceBody owner;
    Vector3[] buffer;
    public void Init(SpaceBody owner)
    {
        this.owner = owner;
        buffer = new Vector3[bufferSize];
        buffer[offset] = owner.transform.position;
    }

    static void CountFuture(int range)
    {

    }
    private void Start()
    {
        
    }
    private void FixedUpdate()
    {

    }
}
