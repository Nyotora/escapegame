using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;

    private Animation anim;
    private bool isOpen;

    [Header("Animation")]
    public GameObject gameObj;
    public GameObject meshObj;

    private Color startcolor;

    public Door(bool isOpen)
    {
        this.isOpen = isOpen;
    }

    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
        anim = gameObj.GetComponent<Animation>();
        //anim.Play("doorOpen");

        startcolor = meshObj.GetComponent<Renderer>().material.color;

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Hover()
    {
        meshObj.GetComponent<Renderer>().material.color = Color.grey;
    }

    public void Unhover()
    {
        meshObj.GetComponent<Renderer>().material.color = startcolor; 
    }

    public void Interact(Player player)
    {

        if (isOpen)
        {
            Debug.Log("close");
            this.isOpen = false;
        }
        else
        {
            Debug.Log("Open");
            this.isOpen = true;
        }

    }

    public void VisualInteraction(Player player)
    {
        if (isOpen)
        {
            _prompt = "E : Fermer";
            anim.Play("doorOpen");
        }
        else
        {
            _prompt = "E : Ouvrir";
            anim.Play("doorClose"); 
        }
    }

    public bool IsOpen()
    {
        return this.isOpen;
    }

}
