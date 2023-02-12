using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public Canvas hud;

    public TextMeshProUGUI textComponent;
    public Image spacebarIcon;
    public string[] lines;
    public float textSpeed;

    public int index;
    public scr_CharacterController playerController;

    private Cinematic cinematic;


    public void setCinematic(Cinematic cinematic)
    {
        this.cinematic = cinematic;
    }

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
                spacebarIcon.gameObject.SetActive(true);
            }
        }
    }

    public void StartDialogue()
    {
        hud.gameObject.SetActive(false);
        playerController.disableInput();
        gameObject.SetActive(true);
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        spacebarIcon.gameObject.SetActive(false);
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        spacebarIcon.gameObject.SetActive(true);
    }


    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {

            if (!cinematic.IsRunning())
            {
                hud.gameObject.SetActive(true);
                gameObject.SetActive(false);
                playerController.enableInput();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = string.Empty;
                cinematic.Next();
                gameObject.SetActive(false);
            }
        }
    }

    public void ShowHud()
    {

        hud.gameObject.SetActive(true);
    }

}
