using UnityEngine;

public class Gravity
{
    static public float G = 6.67f;
    static public Vector3 GetAttractForce(SpaceBody attractor, SpaceBody objectToAttract, Vector3 attractorPosition, Vector3 attracteblePosition)
    {
        float speed = objectToAttract.attractionSpeed;

        Vector3 deltaVector = attractorPosition - attracteblePosition;
        float distance = deltaVector.magnitude;
        float forceMagnitude = G * attractor.mass * objectToAttract.mass * speed / Mathf.Pow(distance, 2);
        return forceMagnitude * deltaVector.normalized;
    }
}
