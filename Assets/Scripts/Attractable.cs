using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractable : MonoBehaviour
{
    public static List<Attractable> Attractebles;
    public Rigidbody rigB;
    public float AttractableSpeed;

    private void OnEnable()
    {
        if (Attractebles == null)
        {
            Attractebles = new List<Attractable>();
        }
        Attractebles.Add(this);
    }

    private void OnDisable()
    {
        Attractebles.Remove(this);
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
