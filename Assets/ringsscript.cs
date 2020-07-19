using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ringsscript : MonoBehaviour
{
    public GameObject planet;
    public int min, max;
    public int border;
    public float distance;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = min; i <= max; i++)
        {
            for (int j = min; j <= max; j++)
            {
                int k = 0;
                //for (int k = min; k <= max; k++)
                //{
                    if ((new Vector3(i, k, j)).magnitude > border)
                    {
                        GameObject spawnedPlanet = Instantiate(planet, transform.position + (new Vector3(i, k, j) * distance
                            ), Quaternion.identity);
                        spawnedPlanet.GetComponent<Satellite>().Owner = gameObject.GetComponent<SpaceBody>();
                    }
               // }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
