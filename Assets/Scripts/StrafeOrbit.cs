using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class StrafeOrbit : MonoBehaviour
{
    LineRenderer Lr;

    [Range(3,1000)]
    public int segments, delay;
    float xAxis;
    float yAxis;
    public float Apog, Perig;

    public Vector3 planetPos;
    public Vector3 starPos;
    public Vector3 ElipseCenter;
    float planetDegres;
    Vector3 AxisVector;
    Vector3[] points;

    private void OnEnable()
    {
        Lr = GetComponent<LineRenderer>();
        //CalculateOrbit();
        //CalculateOrbit();
    }

    float GetPlanetPosAngle( Vector3 a, Vector3 b)
    {
        //float angele = Vector3.Angle(a, b);
        Vector3 up = AxisVector;
        return Vector3.SignedAngle(a, b, up);
    }

    public void CalculateOrbit()
    {
        print("(Apog + Perig) / 2 = " + (Apog + Perig) / 2);
        ElipseCenter = planetPos + (starPos - planetPos).normalized * ((Apog + Perig) / 2);

        Debug.DrawRay(Vector3.zero, ElipseCenter, Color.blue, Time.deltaTime);

        AxisVector = Vector3.Cross(Vector3.up, starPos - ElipseCenter);
        if (AxisVector.magnitude == 0)
        {
            AxisVector = Vector3.Cross(Vector3.left, starPos - ElipseCenter);
        }
        AxisVector = Vector3.Cross(AxisVector, starPos - ElipseCenter);

        //Debug.DrawRay(ElipseCenter, AxisVector, Color.red, Time.deltaTime);

        if (segments == 0 || Lr == null)
        {
            return;
        }

        Vector3 PositiveStarFocus = starPos;
        Vector3 NegativeStarFocus = PositiveStarFocus + (ElipseCenter - PositiveStarFocus)*2;

        //Debug.DrawRay(Vector3.zero, PositiveStarFocus, Color.red, Time.deltaTime);
        //Debug.DrawRay(Vector3.zero, NegativeStarFocus, Color.blue, Time.deltaTime);

        Perig = (planetPos - starPos).magnitude;
        float a = ((transform.position - PositiveStarFocus).magnitude + (transform.position - NegativeStarFocus).magnitude) / 2;
        float c = a - Perig;
        float e = c / a;
        float k = Mathf.Sqrt(1 - Mathf.Pow(e, 2));

        ElipseCenter = (starPos - planetPos).normalized * a + planetPos;

        xAxis = a;
        yAxis = k * a;

        print(xAxis + " " + yAxis);

        points = new Vector3[segments - delay + 1];

        // todo make degrees to iterator
        // todo delay + 1?

        planetDegres = GetPlanetPosAngle(transform.position - ElipseCenter, planetPos - ElipseCenter);

        

        if (planetDegres <= 0)
        {
            planetDegres = 360 + planetDegres;
        }

        print(planetDegres);

        int mult = Mathf.FloorToInt(planetDegres) / 180;
        planetDegres %= 180;

        if (Mathf.Abs(planetDegres) < 0.5)
        {
            planetDegres = 0;
        }

        if (planetDegres != 0)
        {
            //print(planetDegres);

            //planetDegres = (Mathf.Asin(Mathf.Sin((planetDegres + 90) * Mathf.Deg2Rad) / xAxis)) * Mathf.Rad2Deg / 360 * segments;

            //print(planetDegres + " " + (Mathf.Acos(Mathf.Cos((planetDegres + 90) * Mathf.Deg2Rad) * (transform.position - ElipseCenter).magnitude / xAxis)) * Mathf.Rad2Deg);
            planetDegres = Mathf.Acos(Mathf.Cos((planetDegres) * Mathf.Deg2Rad) * (transform.position - ElipseCenter).magnitude / xAxis) * Mathf.Rad2Deg;
        }
        planetDegres = (planetDegres / 360 + (float)(mult + 0.5)/2) * segments;

        for (int i = 0, j = Mathf.FloorToInt(planetDegres); i < points.Length; i++, j = (j + 1) % segments)
        {
            float angle = ((float)j / (float)segments) * 2 * Mathf.PI;
            float x = Mathf.Sin(angle) * xAxis;
            float y = Mathf.Cos(angle) * yAxis;
            
            points[i] = planetPos + starPos + Quaternion.FromToRotation(Vector3.right, planetPos + starPos) * (new Vector3(x - xAxis, 0, y) );
        }
        if (delay == 0)
        {
            points[segments - delay] = points[0];
        }

        Lr.positionCount = segments - delay + 1;
        Lr.SetPositions(points);
    }

    // Start is called before the first frame update
    void Start()
    {
        Lr = GetComponent<LineRenderer>();
        //CalculateOrbit();
    }

    // Update is called once per frame
    void Update()
    {
        //CalculateOrbit();
        //if (planetPos != transform.position)
        //{
        //    planetPos = transform.position;
        //    CalculateOrbit();
        //}

        

    }

    float GetCircleRadius()
    {
        return 0f;
    }

    private void FixedUpdate()
    {
        float lastSegmentLength = (points[points.Length - 1] - points[points.Length - 2]).magnitude;
        //todo while
        while (lastSegmentLength <= (points[1] - transform.position).magnitude)
        {
            for (int i = points.Length - 1; i > 1; i--)
            {
                points[i] = points[i - 1];
            }
            points[1] += (transform.position - points[1]).normalized * lastSegmentLength;
            lastSegmentLength = (points[points.Length - 1] - points[points.Length - 2]).magnitude;
        }

        points[0] = transform.position;

        Lr.SetPositions(points);
        //CalculateOrbit();
    }

    private void OnValidate()
    {
        //if(Application.isPlaying)
         //   CalculateOrbit();
    }
}
