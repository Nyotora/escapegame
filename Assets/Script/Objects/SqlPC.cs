using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SqlPC : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;

    public GameObject inputsFields;
    public Text[] textInputs;

    public GameObject showTableBtn;
    public GameObject studentTableCanvas;

    public StudentTable studentTable;

    public scr_CharacterController playerController;

    public void Hover()
    {
    }

    public void Interact(Player player)
    {
    }

    public void Unhover()
    {
    }

    public void VisualInteraction(Player player)
    {
        inputsFields.SetActive(true);

        if (player.GetInventory().Contains(studentTable))
        {
            showTableBtn.SetActive(true);
        }

        playerController.disableInput();
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void hideSQLquery()
    {
        inputsFields.SetActive(false);

        playerController.enableInput();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Execute()
    {
        string query = "";
        //GameObject[] inputs = inputsFields.GetComponentInChildren<GameObject>();
        foreach (Text input in textInputs)
        {
            //Debug.Log(input.GetComponent<Text>().text);
            //Debug.Log(input.text);

            query += input.text + " ";
        }
        Debug.Log(query);
    }

    public void ShowStudentTable()
    {
        this.studentTableCanvas.SetActive(true);
    }

    public void HideStudentTable()
    {
        this.studentTableCanvas.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
