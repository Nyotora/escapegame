using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class FirstCinematic : MonoBehaviour
{
    public Image blackScreen;
    public Player player;
    public GameObject player_bone;
    public scr_CharacterController playerController;
    //public GameObject camera;
    public VideoPlayer video;

    public Dialogue dialogueBox;

    public Professor profPugel;

    private bool isRunning;

    private int nextIndex;

    private Animation anim;
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
        //playerController.disableCalculating();

        StartCoroutine(FadeImage());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator FadeImage()
    {
        //yield return new WaitForSeconds(28);
        blackScreen.gameObject.SetActive(true);
        video.gameObject.SetActive(false);

        // loop over 1 second backwards
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            blackScreen.color = new Color(0, 0, 0, i);
            yield return null;
        }
        blackScreen.gameObject.SetActive(false);
        StopAllCoroutines();
        StartCoroutine(MoveCameraAround());
    }

    IEnumerator MoveCameraAround()
    {
        //var videoPlayer = camera.AddComponent<VideoPlayer>();

        //videoPlayer.Play();

        // loop over 1 second backwards
        anim = player_bone.GetComponent<Animation>();
        anim.Play("cam_cine1");
        yield return new WaitForSeconds(1);

        dialogueBox.textComponent.text = string.Empty;
        dialogueBox.lines = new string[] { "(...)", "(O� suis-je ?)" };
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
        anim = player_bone.GetComponent<Animation>();
        anim.Play("cam_cine2");
        yield return new WaitForSeconds(1);

        dialogueBox.lines = new string[] { "(C'est le batiment informatique non ?)", "(J'�tais en cours et d'un coup je me suis endormi...)", "(Il y a de la br�me de partout...)" };
        dialogueBox.StartDialogue();
        nextIndex++;
        //StartCoroutine(MoveCameraAround3());
    }

    IEnumerator MoveCameraAround3()
    {
        profPugel.gameObject.SetActive(true);
        // loop over 1 second backwards
        anim = player_bone.GetComponent<Animation>();
        anim.Play("cam_cine3");
        yield return new WaitForSeconds(1.3f);

        isRunning = false;
        dialogueBox.lines = new string[] { "Je vois que tu t'es endormi durant mon cours...", 
            "C'est pas tr�s serieux �a.",
            "...",
            "Bon...",
            "Les autres professeurs m'ont rapport� que tu as l'air de t'ennuyer durant leur cours.",
            "Tu risques de rater ton ann�e si �a continue comme �a.",
            "Pour la peine tu vas aller r�viser tes cours tout de suite.",
            "Va dans le batiment informatique situ� � ta droite.",
            "Tu trouveras diff�rents professeurs qui te confieront plusieurs missions."
        };
        dialogueBox.StartDialogue();
    }
}
