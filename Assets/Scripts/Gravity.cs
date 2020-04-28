using UnityEngine;

public class Gravity
{
    static public float G = 6.67f;
    static public Vector3 GetAttractForce(Attractor attractor, Attractable objectToAttract, Vector3 attractorPosition, Vector3 attracteblePosition)
    {
        Rigidbody rbAtractor = attractor.rigB;
        Rigidbody rbToAttract = objectToAttract.rigB;
        float speed = objectToAttract.AttractableSpeed;

        Vector3 deltaVector = attractorPosition - attracteblePosition;
        float distance = deltaVector.magnitude;
        float forceMagnitude = G * rbAtractor.mass * rbToAttract.mass * speed / Mathf.Pow(distance, 2);
        return forceMagnitude * deltaVector.normalized;
    }
}
