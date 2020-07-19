using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalansedSystem : MonoBehaviour
{
    public SpaceBody Star;
    public PlanetOptions[] planetOptions;
    public float Margin = 1;
    public float DistanceUnit = 1;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < planetOptions.Length; i++)
        {
            float planetPos = GetPlanetLocation(i);
            PlanetOptions option = planetOptions[i];

            GameObject createdPlanet = Instantiate(option.PlanetAsset, transform.position + new Vector3(planetPos + Margin, 0, 0), Quaternion.identity);
            createdPlanet.gameObject.transform.localScale = Vector3.one * option.PlanetScale;
            createdPlanet.GetComponent<Rigidbody>().mass = option.PlanetMass;
            createdPlanet.gameObject.GetComponent<Satellite>().Owner = Star;
            createdPlanet.gameObject.GetComponent<SpaceBody>().attractionSpeed = option.AttractableSpeed;
            print("CreatePlanet");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    float GetPlanetLocation(int index)
    {
        float d = index == 0 ? 0 : 3 * Mathf.Pow(2, index);
        return (d + 4) * DistanceUnit;
    }
}
