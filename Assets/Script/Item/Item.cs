using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    protected string name;
    protected string description;
    public Sprite img;
    public ItemBox box;

    public Item(string name, string description)
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

    public void ShowItemBox()
    {
        box.Show(name, description, img);
    }
}
