using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceTimeLine : MonoBehaviour
{
    static protected int offset = 0;
    protected static int bufferSize = 10000000;


    public static int getCurrentMoment()
    {
        return offset;
    }

    public static int getNextTime()
    {
        return getDiffMoment(1);
    }

    public static int getPreviousTime()
    {
        return getDiffMoment(-1);
    }

    public static int getDiffMoment(int delta)
    {
        return (offset + delta + bufferSize) % bufferSize;
    }

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
        offset = (offset + 1) % bufferSize;
        //print("1");
    }
}
