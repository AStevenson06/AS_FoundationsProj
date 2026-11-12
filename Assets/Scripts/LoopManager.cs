using UnityEngine;

public class LoopManager : MonoBehaviour
{
    // Remembers how many times the game has looped
    public static int loopCount = 0;

    // Called when the poo-high ending happens
    public static void AddLoop()
    {
        loopCount++;

        Debug.Log("TIME LOOP! Current loop: " + loopCount);
    }

    // Returns the current loop number
    public static int GetLoopCount()
    {
        return loopCount;
    }

    // Resets the loop counter
    public static void ResetLoops()
    {
        loopCount = 0;
    }
}