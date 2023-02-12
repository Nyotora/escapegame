using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class SQLkeyCinematic : Cinematic
{
    private bool start = false;
    private Animation anim;

    public Player player;
    public Camera playerCamera;
    public Camera cinematicCamera;
    public scr_CharacterController playerController;
    public GameObject key;

    public VideoPlayer credits;
    public Image blackScreen;

    // Start is called before the first frame update
    void Start()
    {
        isRunning = true;        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator insertKey()
    {
        playerController.disableInput();
        cinematicCamera.gameObject.SetActive(true);
        playerCamera.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        key.SetActive(true);
        anim = key.GetComponent<Animation>();
        anim.Play("InsertKey");

        yield return new WaitForSeconds(1.5f);

        //playerCamera.gameObject.SetActive(true);
        //cinematicCamera.gameObject.SetActive(false);
        //playerController.enableInput();
        StartCoroutine(FadeImage());
    }

    public override void Next()
    {
        switch (nextIndex)
        {
            case 0:
                StopAllCoroutines();
                StartCoroutine(insertKey());
                break;
        }
    }

    IEnumerator FadeImage()
    {
        //yield return new WaitForSeconds(28);
        blackScreen.gameObject.SetActive(true);

        // loop over 1 second backwards
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            // set color with i as alpha
            blackScreen.color = new Color(0, 0, 0, i);
            yield return null;
        }
        //blackScreen.gameObject.SetActive(false);
        StartCoroutine(launchCredits());
    }

    IEnumerator launchCredits()
    {
        //yield return new WaitForSeconds(28);
        blackScreen.gameObject.SetActive(false);
        credits.gameObject.SetActive(true);
        credits.Play();
        yield return new WaitForSeconds(27);
        Cursor.lockState = CursorLockMode.Confined;
        SceneManager.LoadScene("MainTitle");
    }
}
