using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpaceBody))]
public class Attractable : MonoBehaviour
{
    public static List<SpaceBody> attractebles;
    SpaceBody owner;
    private void OnEnable()
    {
        owner = GetComponent<SpaceBody>();
        if (attractebles == null)
        {
            attractebles = new List<SpaceBody>();
        }
        attractebles.Add(owner);
    }

    private void OnDisable()
    {
        attractebles.Remove(owner);
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
