using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_door : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;

    private Animation anim;
    private bool isOpen;

    [Header("Animation")]
    public GameObject gameObj;

    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
        anim = gameObj.GetComponent<Animation>();
        //anim.Play("doorOpen");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(Interactor interactor)
    {

        if (isOpen)
        {
            Debug.Log("close");
            anim.Play("doorClose");
            this.isOpen = false;
        }
        else
        {
            Debug.Log("Open");
            anim.Play("doorOpen");
            this.isOpen = true;
        }

    }
}
