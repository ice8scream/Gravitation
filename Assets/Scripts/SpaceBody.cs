using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
//[RequireComponent(typeof(Rigidbody))]
public class SpaceBody : MonoBehaviour
{
    Rigidbody rigB;
    float mass {
        get { return rigB.mass; }
        set { rigB.mass = value; }
    }

    Vector3 velocity {
        get { return rigB.velocity; }
    }

    public float attractionSpeed;

    Forecast forecast;

    bool isForecastTracked;

    // Start is called before the first frame update
    void Start()
    {
        rigB = GetComponent<Rigidbody>();
        if (isForecastTracked)
        {
            forecast.Init(this);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
