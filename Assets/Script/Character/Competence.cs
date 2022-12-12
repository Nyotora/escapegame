using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Competence
{
    private int id;
    private string name;
    private bool isValidated;

    public Competence(int id, string name)
    {
        this.id = id;
        this.name = name;
        this.isValidated = false;
    }

    public int Id()
    {
        return id;
    }

    public string Name()
    {
        return name;
    }

    public bool IsValidated()
    {
        return isValidated;
    }

    public void Validate()
    {
        isValidated = true;
    }
}
