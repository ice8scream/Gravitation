using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VangaScript : MonoBehaviour
{
    // Start is called before the first frame update
    Dictionary<Attractable, Vector3[]> futureAttractablePosition = new Dictionary<Attractable, Vector3[]>();
    List<Attractor> Attractors = new List<Attractor>();
    float G = 6.67E-11F;

    public int FutureBufferSize = 10000000;
    int currentTime = 0;
    int futureCalculatedTime = 0;

    public GameObject watched;

    void Start()
    {
        Attractors = new List<Attractor>(FindObjectsOfType<Attractor>());
        Attractable[] attractables = FindObjectsOfType<Attractable>();

        futureAttractablePosition = new Dictionary<Attractable, Vector3[]>();

        foreach (var attracteble in attractables)
        {
            Vector3[] futurePositions = new Vector3[FutureBufferSize];
            futurePositions[currentTime] = (attracteble.transform.position);
            futureAttractablePosition.Add(attracteble, futurePositions);
        }

        //if (GetComponent<Attractable>() != null)
        //{
        //    Vector3[] futurePositions = new Vector3[FutureBufferSize];//todo create list with FutureBufferSize
        //    print(futurePositions.Length);
        //    futurePositions[currentTime] = (transform.position);
        //    futureAttractablePosition.Add(GetComponent<Attractable>(), futurePositions);
        //}

        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CalculateFutue(int range)
    {
        if (futureCalculatedTime == 0)
        {
            foreach (var attractable in futureAttractablePosition)
            {
                attractable.Value[currentTime] = attractable.Key.transform.position;
            }
        }

        int lastTime = (currentTime) ;
        futureCalculatedTime += range;
        while (range > 0)
        {
            int nextTime = (lastTime + 1) % FutureBufferSize;
            foreach (Attractor attractor in Attractors) // todo swap with attructebles
            {
                
                Attractable both = attractor.gameObject.GetComponent<Attractable>();
                Vector3 CurrentAttractorPosition;
                if (both != null)
                {
                    CurrentAttractorPosition = futureAttractablePosition[both][lastTime];
                    
                }
                else
                {
                    CurrentAttractorPosition = attractor.transform.position;
                }

                foreach (var attractable in futureAttractablePosition)
                {
                    if (attractable.Key.gameObject != attractor.gameObject)
                    {
                        //print(GetAttractForce(attractor, attractable.Key, CurrentAttractorPosition, attractable.Value[lastTime]));
                        attractable.Value[nextTime] += GetAttractForce(attractor, attractable.Key, CurrentAttractorPosition, attractable.Value[lastTime]);
                       
                    }
                }

            }

            foreach(var attractable in futureAttractablePosition)
            {
                Vector3 velocity;
                Vector3 position;
                if (lastTime == currentTime)
                {
                    velocity = attractable.Key.rigB.velocity;
                    position = attractable.Key.transform.position;
                }
                else
                {
                    int peviousTime = (lastTime - 1 + FutureBufferSize) % FutureBufferSize;
                    velocity = attractable.Value[lastTime] - attractable.Value[peviousTime];
                    position = attractable.Value[lastTime];
                }

                attractable.Value[nextTime] += -position + velocity;
                print(velocity);
            }

            
            print(futureAttractablePosition[watched.GetComponent<Attractable>()][nextTime]);
            range--;
            lastTime = nextTime;
        }
    }

    Vector3 GetAttractForce(Attractor attractor, Attractable objectToAttract, Vector3 attractorPosition, Vector3 attracteblePosition)
    {
        Rigidbody rbAtractor = attractor.rigB;
        Rigidbody rbToAttract = objectToAttract.rigB;
        float speed = objectToAttract.AttractableSpeed;

        Vector3 deltaVector = attractorPosition - attracteblePosition;
        float distance = deltaVector.magnitude;
        float forceMagnitude = G * rbAtractor.mass * rbToAttract.mass * speed / Mathf.Pow(distance, 2);
        return forceMagnitude * deltaVector.normalized;
    }

    private void FixedUpdate()
    {
        currentTime = (currentTime + 1) % FutureBufferSize;
        futureCalculatedTime -= futureCalculatedTime > 0 ? 1 : 0;
        if (futureCalculatedTime == 0)
        {
            CalculateFutue(1000);
        }

        Attractable watchedAttracteble = watched.GetComponent<Attractable>();
        print("Expected: " + futureAttractablePosition[watchedAttracteble][currentTime]);
        print("Actual:   " + watched.transform.position);
        
    }
}
