using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Canvas pauseMenu;
    public scr_CharacterController characterController;
    public Player player;
    public TextMeshProUGUI competenceTitle;
    public TextMeshProUGUI competenceHints;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (characterController.canMove() && characterController.escPressed())
        {
            Show();
        }
    }

    public void Show()
    {
        Cursor.lockState = CursorLockMode.Confined;
        characterController.disableInput();
        pauseMenu.gameObject.SetActive(true);
        competenceTitle.text = string.Empty;
        competenceHints.text = string.Empty;
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.gameObject.SetActive(false);
        characterController.enableInput();
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainTitle");
    }

    public void ShowCompetenceDetails(int index)
    {
        competenceTitle.text = "Clé n°" + index.ToString() + " : " + player.GetProgression().getCompetence(index - 1).Name();
        competenceHints.text = "Indice : " +  player.GetProgression().getCompetence(index - 1).Hint();
    }
}
