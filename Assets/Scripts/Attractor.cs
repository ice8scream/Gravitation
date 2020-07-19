using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpaceBody))]
public class Attractor : MonoBehaviour
{
    SpaceBody owner;

    // Start is called before the first frame update
    void Start()
    {
        owner = GetComponent<SpaceBody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        List<SpaceBody> attractables = Attractable.attractebles;
        foreach (SpaceBody attractable in attractables)
        {
            if (attractable.gameObject != this.gameObject)
            {
                attractable.GetRigB.AddForce(Gravity.GetAttractForce(owner,attractable,transform.position,attractable.transform.position));

            }
        }
    }
}
