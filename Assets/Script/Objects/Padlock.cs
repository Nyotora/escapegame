using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Padlock : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;

    private bool unlocked = false;
    private string code = "042";

    private Animation anim;

    public GameObject padlockRoot;
    public GameObject locker;
    public Canvas PadlockMenu;
    public Text[] digits;
    public scr_CharacterController playerController;

    // Start is called before the first frame update
    void Start()
    {
        anim = padlockRoot.GetComponent<Animation>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hover()
    {
        //throw new System.NotImplementedException();
    }

    public void Interact(Player player)
    {
        //throw new System.NotImplementedException();
    }

    public void Unhover()
    {
        //throw new System.NotImplementedException();
    }

    public void VisualInteraction(Player player)
    {
        if (!unlocked)
        {
            playerController.disableInput();
            Cursor.lockState = CursorLockMode.Confined;
            PadlockMenu.gameObject.SetActive(true);
        }
    }

    public void Validate()
    {
        string answer = digits[0].text + "" + digits[1].text + "" + digits[2].text;
        if (answer == code)
        {
            unlocked = true;
            StartCoroutine(DestroyHimself());
        } else
        {
            
        }
    }

    public void ClosePadlockInput()
    {
        PadlockMenu.gameObject.SetActive(false);
        playerController.enableInput();
        Cursor.lockState = CursorLockMode.Locked;
    }

    IEnumerator DestroyHimself()
    {
        _prompt = "";
        PadlockMenu.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        anim.Play("UnlockPadlock");
        yield return new WaitForSeconds(1.6f);

        anim = locker.GetComponent<Animation>();
        anim.Play("ArmoireOpen");

        gameObject.SetActive(false);
        PadlockMenu.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        playerController.enableInput();
    }

}
