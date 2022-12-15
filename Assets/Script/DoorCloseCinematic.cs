using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DoorCloseCinematic : MonoBehaviour
{
    private bool finished;
    public GameObject doubleDoor;
    public Player player;
    public scr_CharacterController playerController;
    public Dialogue dialogueBox;


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

        anim = doubleDoor.GetComponent<Animation>();
        anim.Play("doubleDoorClose");

        yield return new WaitForSeconds(1);

        dialogueBox.textComponent.text = string.Empty;
        dialogueBox.lines = new string[] { "Ah...", "C'est fermé" };
        dialogueBox.StartDialogue();
        //StartCoroutine(MoveCameraAround2());
    }
}
