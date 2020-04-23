using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StrafeOrbit))]
public class Satellite : MonoBehaviour
{
    public Attractor Owner;
    StrafeOrbit orbit;
    Attractable satellite;
    public float ApogVisPerig;
    float G = 6.67f;

    private void OnEnable()
    {
        satellite = GetComponent<Attractable>();
        //orbit = GetComponent<StrafeOrbit>();
    }

    // Start is called before the first frame update
    void Start()
    {
        orbit = GetComponent<StrafeOrbit>();
        print("Satellite Start");
        AddOrbitalSatelliteVelocity();
    }

    // Update is called once per frame
    void Update()
    {
        //print((satellite.transform.position - Owner.transform.position).magnitude);
        //orbit.CalculateOrbit();
    }

    void AddOrbitalSatelliteVelocity()
    {
        Vector3 dir = SatelliteMovenmentDirection(Owner.rigB.position);

        float r = (Owner.rigB.position - satellite.rigB.position).magnitude;
        float a = r * (1 + ApogVisPerig) / 2;

        float M = Owner.rigB.mass;
        Vector3 v = dir * Mathf.Sqrt(G * M * satellite.AttractableSpeed * (2 / r - 1 / a));
        satellite.rigB.velocity = v;

        //orbit.segments = 200;
        //orbit.delay = 0;
        orbit.Perig = r;
        orbit.Apog = r * ApogVisPerig;
        orbit.planetPos = transform.position;
        orbit.starPos = Owner.transform.position;
        orbit.CalculateOrbit();
        //orbit.CalculateOrbit();


        //orbit.xAxis = a * 2;
        //orbit.yAxis = Mathf.Sqrt(1 - Mathf.Pow((a-r) / a, 2));
        //orbit.z = 0;
    }

    Vector3 SatelliteMovenmentDirection(Vector3 ownerPosition)
    {
        Vector3 deltaVector = ownerPosition - transform.position;
        Vector3 dir = Vector3.Cross(Vector3.up, deltaVector);
        if (dir.magnitude == 0)
        {
            dir = Vector3.Cross(Vector3.left, deltaVector);
        }
        return dir.normalized;
    }
}
