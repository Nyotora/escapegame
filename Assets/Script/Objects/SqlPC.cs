using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SqlPC : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;

    public GameObject sqlElements;
    public InputField[] inputFields;
    public Text[] textInputs;

    public GameObject showTableBtn;
    public GameObject studentTableCanvas;

    public GameObject SQLerrorCanvas;
    public Text errorText;

    public GameObject SQLadvertCanvas;
    public Text advertText;

    public GameObject SQLresultCanvas;
    public Text resultText;

    public GameObject help1Canvas;
    public GameObject help2Canvas;

    public StudentTable studentTable;

    public scr_CharacterController playerController;

    [Header("Loggin Canvas")]
    public Canvas LoginScreenCanvas;
    public TMP_InputField passwordInput;
    public Image hint;


    private string[] validColumn = new string[] {"id_etudiant","nom","prenom","moyenne"};
    private string[] validOperations = new string[] { "=", "!=" };

    private bool success = false;
    private bool access = false;
    private bool logged = false;

    private Player player;

    private SqlManager sqlManager;

    public bool canBeAccessed()
    {
        return access;
    }

    public void checkPassword()
    {
        if (passwordInput.text == "iloveSQL87")
        {
            logged = true;
            LoginScreenCanvas.gameObject.SetActive(false);
            VisualInteraction(player);
        } else
        {
            passwordInput.text = "";
            passwordInput.placeholder.GetComponent<TextMeshProUGUI>().text = "Mot de passe incorrect";
        }
    }

    public void forgetPassword()
    {
        hint.gameObject.SetActive(true);
    }

    //------------------------------------------------------------------------------------------------------------


    public bool IsQueryValid()
    {
        return success;
    }


    public void giveAccess()
    {
        access = true;
    }

    public void Hover()
    {
    }

    public void Interact(Player player)
    {
        this.player = player;
    }

    public void Unhover()
    {
    }

    public void VisualInteraction(Player player)
    {
        if (access)
        {
            if (logged)
            {
                sqlElements.SetActive(true);

                if (player.GetInventory().Contains(studentTable))
                {
                    showTableBtn.SetActive(true);
                }
            } else
            {
                LoginScreenCanvas.gameObject.SetActive(true);
            }
            playerController.disableInput();
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    public void hideSQLquery()
    {
        hint.gameObject.SetActive(false);
        sqlElements.SetActive(false);
        LoginScreenCanvas.gameObject.SetActive(false);

        playerController.enableInput();
        Cursor.lockState = CursorLockMode.Locked;

        HideAdvert();
        HideError();
    }

    public void showHelp1()
    {
        help1Canvas.SetActive(true);
        help2Canvas.SetActive(false);
    }

    public void showHelp2()
    {
        help2Canvas.SetActive(true);
        help1Canvas.SetActive(false);
    }

    public void exitHelp()
    {
        help1Canvas.SetActive(false);
        help2Canvas.SetActive(false);
    }

    public void Execute()
    {
        string[] inputs = { textInputs[0].text,
        textInputs[1].text,
        textInputs[2].text,
        textInputs[3].text,
        textInputs[4].text,
        textInputs[5].text,
        textInputs[6].text,
        textInputs[7].text };

        string message = sqlManager.checkForError(inputs);

        if (sqlManager.errorDetected())
        {
            ShowError(message);
        } else if (sqlManager.successed())
        {
            foreach (InputField input in inputFields)
            {
                input.interactable = false;
            }
            ShowResult(message);
            success = true;
        } else
        {
            ShowAdvert(message);
        }
    }


    public void ShowResult(string result)
    {
        HideAdvert();
        HideError();
        resultText.text = result;
        SQLresultCanvas.SetActive(true);
    }

    public void HideResult()
    {
        resultText.text = string.Empty;
        SQLresultCanvas.SetActive(false);
    }



    public void ShowAdvert(string adv)
    {
        HideError();
        advertText.text = adv;
        SQLadvertCanvas.SetActive(true);
    }

    public void HideAdvert()
    {
        advertText.text = string.Empty;
        SQLadvertCanvas.SetActive(false);
    }



    public void ShowError(string err)
    {
        HideAdvert();
        errorText.text = err;
        SQLerrorCanvas.SetActive(true);
    }

    public void HideError()
    {
        errorText.text = string.Empty;
        SQLerrorCanvas.SetActive(false);
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
        sqlManager = new SqlManager();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
