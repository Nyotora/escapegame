using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DoorCloseCinematic : MonoBehaviour
{
    private bool finished;
    public GameObject doubleDoor;
    public Player player;
    public GameObject player_bone;
    public scr_CharacterController playerController;
    public Dialogue dialogueBox;
    public GameObject keyLock;


    private Animation anim;

    // Start is called before the first frame update
    void Start()
    {
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

        anim = player_bone.GetComponent<Animation>();
        anim.Play("180");

        yield return new WaitForSeconds(1);

        anim = doubleDoor.GetComponent<Animation>();
        anim.Play("doubleDoorClose");

        yield return new WaitForSeconds(0.7f);

        anim = keyLock.GetComponent<Animation>();
        anim.Play("KeyLockSpawn");

        yield return new WaitForSeconds(1.7f);

        dialogueBox.textComponent.text = string.Empty;
        dialogueBox.lines = new string[] { "(...)", "(Hein ?)", "(Attends attends !!)", "(C'est quoi ce bordel ?!)", "(Je rêve ou quoi ?)", "(Il faut que je sorte de là !)" };
        dialogueBox.StartDialogue();

        anim = player_bone.GetComponent<Animation>();
        anim.Play("r180");
        //StartCoroutine(MoveCameraAround2());
    }
}
