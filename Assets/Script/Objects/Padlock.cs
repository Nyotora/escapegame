using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Padlock
{
    private bool unlocked;
    private string code;

    public Padlock(string code)
    {
        this.code = code;
        unlocked = false;
    }

    public bool isUnlocked()
    {
        return unlocked;
    }

    public void checkCode(string answer)
    {
        if (answer == code)
        {
            unlocked = true;
        } else
        {
            unlocked = false;
        }
    }
}
