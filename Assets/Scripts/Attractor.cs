using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    public Rigidbody rigB;
    static float G = 6.67f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        List<Attractable> attractables = Attractable.Attractebles;
        foreach (Attractable attractable in attractables)
        {
            if (attractable.gameObject != this.gameObject)
            {
                attractable.rigB.AddForce(GetAttractForce(attractable));

            }
        }
    }

    public Vector3 GetAttractForce(Attractable objectToAttract)
    {
        Rigidbody rbToAttract = objectToAttract.rigB;
        float speed = objectToAttract.AttractableSpeed;

        Vector3 deltaVector = rigB.position - rbToAttract.position;
        float distance = deltaVector.magnitude;
        float forceMagnitude = G * rigB.mass * rbToAttract.mass * speed / Mathf.Pow(distance, 2);
        return forceMagnitude* deltaVector.normalized;
    }
}
