using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public StudentTable studentTable;

    public scr_CharacterController playerController;

    private string[] validColumn = new string[] {"id_etudiant","nom","prenom","moyenne"};
    private string[] validOperations = new string[] { "=", "!=" };

    private bool success = false;
    private bool access = false;

    public bool IsQueryValid()
    {
        return success;
    }

    public bool canBeAccessed()
    {
        return access;
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
    }

    public void Unhover()
    {
    }

    public void VisualInteraction(Player player)
    {
        if (access)
        {
            sqlElements.SetActive(true);

            if (player.GetInventory().Contains(studentTable))
            {
                showTableBtn.SetActive(true);
            }

            playerController.disableInput();
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    public void hideSQLquery()
    {
        sqlElements.SetActive(false);

        playerController.enableInput();
        Cursor.lockState = CursorLockMode.Locked;

        HideAdvert();
        HideError();
    }

    public void Execute()
    {
        if (textInputs[0].text.ToUpper() != "SELECT")
        {
            ShowError("ERREUR LORS DE L'EXECUTION SQL :\nErreur syntaxe : SyntaxeException(0)\n\nErreur ligne 1 : '"
                + textInputs[0].text + "' n'est pas reconnu dans la syntaxe SQL.");

        }
        else if (textInputs[2].text.ToUpper() != "FROM")
        {
            ShowError("ERREUR LORS DE L'EXECUTION SQL :\nErreur syntaxe : SyntaxeException(0)\n\nErreur ligne 2 : '"
                + textInputs[2].text + "' n'est pas reconnu dans la syntaxe SQL.");

        }
        else if (textInputs[4].text.ToUpper() != "WHERE")
        {
            ShowError("ERREUR LORS DE L'EXECUTION SQL :\nErreur syntaxe : SyntaxeException(0)\n\nErreur ligne 3 : '"
                + textInputs[4].text + "' n'est pas reconnu dans la syntaxe SQL.");

        }
        else if (textInputs[1].text.ToUpper() == "")
        {
            ShowError("ERREUR LORS DE L'EXECUTION SQL :\nErreur syntaxe : NullPointerException(0)\n\nErreur ligne 1 : "
                + "Un nom de colonne est attendu après 'SELECT'.");

        }
        else if (textInputs[3].text.ToUpper() == "")
        {
            ShowError("ERREUR LORS DE L'EXECUTION SQL :\nErreur syntaxe : NullPointerException(0)\n\nErreur ligne 2 : "
                + "Un nom de table est attendu après 'FROM'.");

        }
        else if (textInputs[5].text.ToUpper() == "" || textInputs[6].text.ToUpper() == "" || textInputs[7].text.ToUpper() == "")
        {
            ShowError("ERREUR LORS DE L'EXECUTION SQL :\nErreur syntaxe : NullPointerException(0)\n\nErreur ligne 3 : "
                + "Une condition est attendue après 'WHERE'.");

        }
        else if (textInputs[3].text.ToUpper() != "ETUDIANT_G4A")
        {
            ShowError("ERREUR LORS DE L'EXECUTION SQL :\nErreur syntaxe : UnknownTableException(0)\n\nErreur ligne 3 : La table '"
                + textInputs[3].text + "' n'existe pas.");

        }
        else if (!validColumn.Contains(textInputs[1].text))
        {
            ShowError("ERREUR LORS DE L'EXECUTION SQL :\nErreur syntaxe : UnknownColumnException(0)\n\nErreur ligne 3 : '"
                + textInputs[1].text + "' n'est pas une colonne de la table ETUDIANT_G4A.");

        }
        else if (!validColumn.Contains(textInputs[5].text))
        {
            ShowError("ERREUR LORS DE L'EXECUTION SQL :\nErreur syntaxe : UnknownColumnException(0)\n\nErreur ligne 3 : '"
                + textInputs[5].text + "' n'est pas une colonne de la table ETUDIANT_G4A.");

        }
        else if (!validOperations.Contains(textInputs[6].text))
        {
            ShowError("ERREUR LORS DE L'EXECUTION SQL :\nErreur syntaxe : UnknownOperatorException(0)\n\nErreur ligne 3 : Impossible d'effectuer l'opération '"
                + textInputs[6].text + "' entre '"+ textInputs[5].text + "' et '"+ textInputs[7].text + "'.");

        }
        else if (!int.TryParse(textInputs[7].text, out _) && textInputs[5].text == "id_etudiant")
        {
            ShowError("ERREUR LORS DE L'EXECUTION SQL :\nErreur syntaxe : MatchTypeException(0)\n\nErreur ligne 3 : '"
                + textInputs[7].text + "' n'est pas un entier est ne correspond pas avec la colonne 'id_etudiant'");

        }
        else if (!float.TryParse(textInputs[7].text, out _) && textInputs[5].text == "moyenne")
        {
            ShowError("ERREUR LORS DE L'EXECUTION SQL :\nErreur syntaxe : MatchTypeException(0)\n\nErreur ligne 3 : '"
                + textInputs[7].text + "' n'est pas un entier est ne correspond pas avec la colonne 'moyenne'");

        }
        // Fin des erreurs de syntaxes : début des erreurs de resultats
        else if (textInputs[1].text != "moyenne")
        {
            ShowAdvert("Conseil de M. Nick :\nTa requête est syntaxiquement juste, mais le résultat n'est pas celui espéré." +
                "\n\nEn effet ici tu essaie de sélectionner '"
                + textInputs[1].text + "' est non la moyenne de l'étudiant recherché.");

        }
        else if (textInputs[6].text == "!=")
        {
            ShowAdvert("Conseil de M. Nick :\nTa requête est syntaxiquement juste, mais le résultat n'est pas celui espéré." +
                "\n\nNous recherchons un seul résultat, et tu essaies d'utiliser l'opérateur '!=' qui exclue un résultat au lieu de le choisir.");

        }
        else if (textInputs[5].text == "moyenne")
        {
            ShowAdvert("Conseil de M. Nick :\nTa requête est syntaxiquement juste, mais le résultat n'est pas celui espéré." +
                "\n\nTu essaie de selectionner la moyenne d'un étudiant grâce à sa moyenne... qui est inconnue. " +
                "Tu devrais essayer d'utiliser une colonne qui différencie tous les étudiants. ");

        }
        else if (textInputs[5].text != "id_etudiant")
        {
            ShowAdvert("Conseil de M. Nick :\nTa requête est syntaxiquement juste, mais le résultat n'est pas celui espéré." +
                "\n\nTu essaie de selectionner la moyenne d'un étudiant où son "+ textInputs[5].text + " n'est pas forcement unique." +
                "Tu devrais essayer d'utiliser une colonne qui différencie tous les étudiants. ");

        }
        else if (textInputs[7].text != "5")
        {
            ShowAdvert("Conseil de M. Nick :\nTa requête est syntaxiquement juste, mais le résultat n'est pas celui espéré." +
                "\n\nTu y es presque ! Mais la moyenne de l'étudiant " + textInputs[7].text + " est déja connue... ");

        }
        else
        {

            foreach (InputField input in inputFields)
            {
                input.interactable = false;
            }

            ShowResult("14.08\n\nMessage de M. Nick :\nBravo, tu as réussi à trouver la moyenne cachée ! Viens donc me retrouver dans le couloir.");
            success = true;
            

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
