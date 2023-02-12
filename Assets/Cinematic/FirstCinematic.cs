using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class FirstCinematic : Cinematic
{
    public Image blackScreen;
    public Player player;
    public GameObject player_bone;
    public scr_CharacterController playerController;
    //public GameObject camera;
    public VideoPlayer video;

    public Dialogue dialogueBox;

    public Professor profPugel;

    public Canvas commandsCanvas;

    private Animation anim;


    // Start is called before the first frame update
    void Start()
    {
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
        yield return new WaitForSeconds(28);
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
        dialogueBox.lines = new string[] { "...", "(Où suis-je ?)" };
        dialogueBox.StartDialogue();
        nextIndex++;
        //StartCoroutine(MoveCameraAround2());
    }


    public override void Next()
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

            case 3:
                StopAllCoroutines();
                StartCoroutine(ShowInputs());
                break;
        }
    }


    IEnumerator MoveCameraAround2()
    {

        // loop over 1 second backwards
        anim = player_bone.GetComponent<Animation>();
        anim.Play("cam_cine2");
        yield return new WaitForSeconds(1);

        dialogueBox.lines = new string[] { "(C'est le bâtiment informatique non ?)", "(J'étais en cours et d'un coup je me suis endormi...)", "(Il y a de la brûme de partout...)" };
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

        dialogueBox.lines = new string[] { "Je vois que tu t'es endormi durant mon cours...", 
            "C'est pas très serieux ça.",
            "...",
            "Bon...",
            "Les autres professeurs m'ont rapporté que tu as l'air de t'ennuyer durant leur cours.",
            "Tu risques de rater ton année si ça continue comme ça.",
            "Pour la peine tu vas aller réviser tes cours tout de suite.",
            "Vas dans le bâtiment informatique situé sur ta droite.",
            "Tu trouveras différents professeurs qui te confieront plusieurs missions."
        };
        dialogueBox.StartDialogue();
        nextIndex++;
    }

    IEnumerator ShowInputs()
    {
        yield return new WaitForSeconds(0.4f);
        commandsCanvas.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        isRunning = false;
        StopAllCoroutines();
    }

    public void hideAndStart()
    {
        commandsCanvas.gameObject.SetActive(false);
        dialogueBox.ShowHud();
        Cursor.lockState = CursorLockMode.Locked;
        playerController.enableInput();
    }

}
