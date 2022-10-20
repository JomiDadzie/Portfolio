using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Timing {

    public static float currentFrame;
    public static float theFrame;


    public static void SaveFrame()
    {
        currentFrame = Time.frameCount;
    }
    public static void PerfectFrames(bool now)
    {       
        if (now)
           theFrame = Time.frameCount - currentFrame;
    }
    public static void ResetFrame()
    {
        currentFrame = 0;
    }
}
