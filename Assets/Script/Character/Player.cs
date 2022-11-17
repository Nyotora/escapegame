using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Player : MonoBehaviour
{

    private List<Item> inventory;

    public Player()
    {
        this.inventory = new List<Item>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<Item> GetInventory()
    {
        return this.inventory;
    }

    public void AddItem(Item item)
    {
        this.inventory.Add(item);
    }
}
