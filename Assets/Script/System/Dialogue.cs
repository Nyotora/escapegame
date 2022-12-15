using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Dialogue : MonoBehaviour
{
    public HUD hud;

    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    public int index;
    public scr_CharacterController playerController;

    private FirstCinematic cinematic;


    public void setCinematic(FirstCinematic cinematic)
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
            }
        }
    }

    public void StartDialogue()
    {
        if (hud.isOpen)
        {
            hud.ChangeCompetencePanel();
        }
        playerController.disableInput();
        gameObject.SetActive(true);
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
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
                gameObject.SetActive(false);
                playerController.enableInput();
                playerController.enableCalculating();
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

}
