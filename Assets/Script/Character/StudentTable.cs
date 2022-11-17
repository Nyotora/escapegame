using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentTable : Item, IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;

    public StudentTable(string name, string description) : base(name, description)
    {
        this.name = name;
        this.description = description;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(Player player)
    {
        if (!player.GetInventory().Contains(this))
        {
            player.AddItem(this);
        }
        
    }
}
