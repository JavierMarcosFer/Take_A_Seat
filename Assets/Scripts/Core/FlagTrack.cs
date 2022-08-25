using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagTrack : MonoBehaviour
{
    public static FlagTrack instance;
    public static Hashtable flags;
    public static int suspectCounter;

    private void Awake()
    {
        instance = this;
        suspectCounter = 0;
        flags = new Hashtable();
    }

    public int getSuspectCounter()
    {
        return suspectCounter;
    }

    public void setSuspectCounter(int n)
    {
        suspectCounter = n;
    }

    public void setFlag(string key, bool value = true)
    {
        // If key is missing, add it to the hashtable
        if (flags.ContainsKey(key))
        {
            flags[key] = value;
        }
        else
        {
            flags.Add(key, value);
        }
    }

    public bool getFlag(string key)
    {
        // If key is missing, return false
        if (flags.ContainsKey(key))
        {
            return (bool)flags[key];
        }
        else
        {
            return false;
        }
    }
}
