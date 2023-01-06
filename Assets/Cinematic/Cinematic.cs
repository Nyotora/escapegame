using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinematic : MonoBehaviour
{
    protected bool isRunning;
    protected int nextIndex;

    // Start is called before the first frame update
    void Start()
    {
        nextIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsRunning()
    {
        return isRunning;
    }

    public virtual void Next()
    {
        //
    }
}
