using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class HUD : MonoBehaviour
{
    public Player player;

    [Header("Competence panel")]
    public Image competenceBox;
    public Image competenceArrowBox;
    public Image competenceArrow;
    public Text[] competencesText;
    public bool isOpen;

    // Start is called before the first frame update
    void Start()
    {
        ChangeCompetencePanel();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            ChangeCompetencePanel();
        }
    }

    public void ChangeCompetencePanel()
    {
        if (isOpen)
        {
            //competenceBox.enabled = false;
            competenceBox.gameObject.SetActive(false);
            competenceArrowBox.transform.position = new Vector3(5, competenceArrowBox.transform.position.y,0);
        } else
        {
            //competenceBox.enabled = true;
            competenceBox.gameObject.SetActive(true);
            competenceArrowBox.transform.position = new Vector3(352, competenceArrowBox.transform.position.y,0);
        }
        isOpen = !isOpen;
    }

    public void setCompetenceText(int id)
    {
        this.competencesText[id].text = player.GetProgression().getCompetence(id).Name();
    }
}
