using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Professor : MonoBehaviour, IInteractable
{
    [SerializeField] protected string _prompt;
    public string InteractionPrompt => _prompt;
    public string[] textLines;

    public Dialogue dialogueBox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hover()
    {
        //throw new System.NotImplementedException();
    }

    virtual public void Interact(Player player)
    {
        dialogueBox.textComponent.text = string.Empty;
        dialogueBox.lines = textLines;
        dialogueBox.StartDialogue();
        Debug.Log("Dialogue ok");
    }

    public void Unhover()
    {
        //throw new System.NotImplementedException();
    }

    public void VisualInteraction(Player player)
    {
        //throw new System.NotImplementedException();
    }
}
