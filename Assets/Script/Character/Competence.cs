using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Competence
{
    private int id;
    private string name;
    private string hint;
    private bool isValidated;

    public Competence(int id, string name, string hint)
    {
        this.id = id;
        this.name = name;
        this.isValidated = false;
        this.hint = hint;
    }

    public int Id()
    {
        return id;
    }

    public string Name()
    {
        return name;
    }

    public string Hint()
    {
        return hint;
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
