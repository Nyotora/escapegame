using Codice.CM.Client.Differences;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstCinematic : MonoBehaviour
{
    public Image blackScreen;
    public Player player;
    public scr_CharacterController playerController;

    public Dialogue dialogueBox;

    public Professor profPugel;

    private bool isRunning;

    private int nextIndex;


    public bool IsRunning()
    {
        return isRunning;
    }


    // Start is called before the first frame update
    void Start()
    {
        nextIndex = 0;
        isRunning = true;
        dialogueBox.setCinematic(this);

        playerController.disableInput();
        playerController.disableCalculating();

        blackScreen.gameObject.SetActive(true);
        StartCoroutine(FadeImage());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator FadeImage()
    {
        // loop over 1 second backwards
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            blackScreen.color = new Color(0, 0, 0, i);
            yield return null;
        }

        StopAllCoroutines();
        StartCoroutine(MoveCameraAround());
    }

    IEnumerator MoveCameraAround()
    {
        // loop over 1 second backwards
        for (float i = 2; i >= 1; i -= Time.deltaTime * 2)
        {
            // set color with i as alpha
            playerController.transform.rotation = Quaternion.Euler(new Vector3(0, i * 45, 0));
            yield return null;
        }
        //yield return new WaitForSeconds(1);
        dialogueBox.lines = new string[] { "...", "Où suis-je ?" };
        dialogueBox.StartDialogue();
        nextIndex++;
        //StartCoroutine(MoveCameraAround2());
    }


    public void Next()
    {
        switch (nextIndex)
        {
            case 1:
                StopAllCoroutines();
                StartCoroutine(MoveCameraAround2());
                break;

            case 2:
                StopAllCoroutines();
                StartCoroutine(MoveCameraAround3());
                break;
        }
    }


    IEnumerator MoveCameraAround2()
    {
        // loop over 1 second backwards
        for (float i = 1; i <= 3; i += Time.deltaTime * 8)
        {
            // set color with i as alpha
            playerController.transform.rotation = Quaternion.Euler(new Vector3(0, i * 45, 0));
            yield return null;
        }

        // loop over 1 second backwards
        for (float i = 3; i <= 4; i += Time.deltaTime * 4)
        {
            // set color with i as alpha
            playerController.transform.rotation = Quaternion.Euler(new Vector3(0, i * 45, 0));
            yield return null;
        }
        dialogueBox.lines = new string[] { "C'est le batiment informatique non ?", "J'étais en cours et d'un coup je me suis endormi..." };
        dialogueBox.StartDialogue();
        nextIndex++;
        //StartCoroutine(MoveCameraAround3());
    }

    IEnumerator MoveCameraAround3()
    {
        profPugel.gameObject.SetActive(true);
        // loop over 1 second backwards
        for (float i = 4; i >= 2; i -= Time.deltaTime * 4)
        {
            // set color with i as alpha
            playerController.transform.rotation = Quaternion.Euler(new Vector3(0, i * 45, 0));
            yield return null;
        }
        isRunning = false;
        dialogueBox.lines = new string[] { "Je vois que tu t'es endormi pendant mon cours...", "C'est pas bien ça." };
        dialogueBox.StartDialogue();
    }
}
