using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DoorCloseCinematic : Cinematic
{
    private bool finished;
    public GameObject doubleDoor;
    public Player player;
    public GameObject player_bone;
    public scr_CharacterController playerController;
    public Dialogue dialogueBox;
    public GameObject keyLock;
    public Canvas SixKeyLockMenuImage;

    public Camera playerCamera;
    public Camera cinematicCamera;

    public Canvas gameSummary;

    private Animation anim;

    // Start is called before the first frame update
    void Start()
    {
        isRunning = true;
        finished = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!finished)
        {
            Collision();
        }
    }

    public override void Next()
    {
        switch (nextIndex)
        {
            case 1:
                StopAllCoroutines();
                StartCoroutine(ShowGameSummary());
                break;
        }
    }

    void Collision()
    {
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity);
        int i = 0;

        while (i < hitColliders.Length)
        {   
            i++;
            finished = true;

            playerController.disableInput();

            //Output all of the collider names
            //Debug.Log("Hit : " + hitColliders[i].name + i);

            StartCoroutine(MoveCameraAround());
            
        }
    }

    //Draw the Box Overlap as a gizmo to show where it currently is testing. Click the Gizmos button to see this
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }

    IEnumerator MoveCameraAround()
    {
        yield return new WaitForSeconds(1);

        //anim = player_bone.GetComponent<Animation>();
        //anim.Play("180");

        //yield return new WaitForSeconds(1);

        cinematicCamera.gameObject.SetActive(true);
        playerCamera.gameObject.SetActive(false);

        yield return new WaitForSeconds(1.3f);

        anim = doubleDoor.GetComponent<Animation>();
        anim.Play("doubleDoorClose");

        yield return new WaitForSeconds(0.7f);

        anim = keyLock.GetComponent<Animation>();
        anim.Play("KeyLockSpawn");

        yield return new WaitForSeconds(2.3f);

        playerCamera.gameObject.SetActive(true);
        cinematicCamera.gameObject.SetActive(false);

        dialogueBox.setCinematic(this);

        dialogueBox.textComponent.text = string.Empty;
        dialogueBox.lines = new string[] { "(...)", "(Hein ?)", "(Attends attends !!)", "(Qu'est-ce qui ce passe ?!)", "(Je rêve ou quoi ?)", "(Il faut que je sorte de là !)" };
        dialogueBox.StartDialogue();
        SixKeyLockMenuImage.gameObject.SetActive(true);
        nextIndex++;

        //anim = player_bone.GetComponent<Animation>();
        //anim.Play("r180");
        //StartCoroutine(MoveCameraAround2());
    }

    IEnumerator ShowGameSummary()
    {
        yield return new WaitForSeconds(0.4f);
        gameSummary.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        isRunning = false;
        StopAllCoroutines();
    }

    public void hideAndFinish()
    {
        gameSummary.gameObject.SetActive(false);
        dialogueBox.ShowHud();
        Cursor.lockState = CursorLockMode.Locked;
        playerController.enableInput();
    }
}
