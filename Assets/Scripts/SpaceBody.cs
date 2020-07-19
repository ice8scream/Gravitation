using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
//[RequireComponent(typeof(Rigidbody))]
public class SpaceBody : MonoBehaviour
{
    Rigidbody rigB;

    public Rigidbody GetRigB {
        get { return rigB; }
    }

    public float mass {
        get { return rigB.mass; }
    }

    public Vector3 position
    {
        get { return transform.position; }
    }

    public Vector3 velocity {
        get { return rigB.velocity; }
        set { rigB.velocity = value; }
    }

    public float attractionSpeed;

    Forecast forecast;

    bool isForecastTracked;

    private void OnEnable()
    {
        rigB = GetComponent<Rigidbody>();
        if (isForecastTracked)
        {
            forecast.Init(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
