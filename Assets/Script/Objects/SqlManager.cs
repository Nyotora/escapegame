using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;
using UnityEngine.Windows;

public class SqlManager
{
    private string[] validColumn = new string[] { "id_etudiant", "nom", "prenom", "moyenne" };
    private string[] validOperations = new string[] { "=", "!=" };
    private bool error;
    private bool succes;

    public static string ERROR_TXT = "ERREUR LORS DE L'EXECUTION SQL :\nErreur syntaxe : ";
    public static string SYNTAX_EXCEPTION_TXT = "SyntaxeException(0)\n\n";
    public static string UNKNOWN_TABLE_EXCEPTION_TXT = "UnknownTableException(0)\n\n";
    public static string UNKNOWN_COLUMN_EXCEPTION_TXT = "UnknownColumnException(0)\n\n";
    public static string MATCH_TYPE_EXCEPTION_TXT = "MatchTypeException(0)\n\nErreur ligne 3 : '";
    public static string UNKNOWN_SYNTAX_TXT = "' n'est pas reconnu dans la syntaxe SQL.";
    public static string ADVICE_TXT = "Conseil de M. Nick :\nTa requête est syntaxiquement juste, mais le résultat n'est pas celui espéré.";

    public SqlManager()
    {
        error = true;
        succes = false;
    }

    public bool errorDetected()
    {
        return error;
    }

    public bool successed()
    {
        return succes;
    }

    public string checkForError(string[] textInputs)
    {
        string[] inputs = new string[textInputs.Length];

        for (int i = 0; i < inputs.Length; i++)
        {
            inputs[i] = removeSpace(textInputs[i]);
        }

        error = true;
        if (inputs[0].ToUpper() != "SELECT")
        {
            return ERROR_TXT + SYNTAX_EXCEPTION_TXT + "Erreur ligne 1 : '"
                + inputs[0] + UNKNOWN_SYNTAX_TXT;

        }
        else if (inputs[2].ToUpper() != "FROM")
        {
            return ERROR_TXT + SYNTAX_EXCEPTION_TXT + "Erreur ligne 2 : '"
                + inputs[2] + UNKNOWN_SYNTAX_TXT;

        }
        else if (inputs[4].ToUpper() != "WHERE")
        {
            return ERROR_TXT + SYNTAX_EXCEPTION_TXT + "Erreur ligne 3 : '"
                + inputs[4] + UNKNOWN_SYNTAX_TXT;

        }
        else if (inputs[3].ToUpper() != "ETUDIANT_G4A")
        {
            return ERROR_TXT + UNKNOWN_TABLE_EXCEPTION_TXT + "Erreur ligne 3 : La table '"
                + inputs[3] + "' n'existe pas.";

        }
        else if (!validColumn.Contains(inputs[1]))
        {
            return ERROR_TXT + UNKNOWN_COLUMN_EXCEPTION_TXT + "Erreur ligne 1 : '"
                + inputs[1] + "' n'est pas une colonne de la table ETUDIANT_G4A.";

        }
        else if (!validColumn.Contains(inputs[5]))
        {
            return ERROR_TXT + UNKNOWN_COLUMN_EXCEPTION_TXT + "Erreur ligne 3 : '"
                + inputs[5] + "' n'est pas une colonne de la table ETUDIANT_G4A.";

        }
        else if (inputs[7] == "")
        {
            return ERROR_TXT + MATCH_TYPE_EXCEPTION_TXT + "Erreur ligne 3 : '"
                + "La condition est incomplète. Il manque la fin.";

        }
        else if (!int.TryParse(inputs[7], out _) && inputs[5] == "id_etudiant")
        {
            return ERROR_TXT + MATCH_TYPE_EXCEPTION_TXT
                + inputs[7] + "' n'est pas un entier et ne correspond pas avec la colonne 'id_etudiant'";

        }
        else if (!float.TryParse(inputs[7], out _) && inputs[5] == "moyenne")
        {
            return ERROR_TXT + MATCH_TYPE_EXCEPTION_TXT
                + inputs[7] + "' n'est pas un réel et ne correspond pas avec la colonne 'moyenne'";

        } else
        {
            return checkForAdvice(inputs);
        }
    }

    public string checkForAdvice(string[] inputs)
    {
        error = false;
        if (inputs[1] != "moyenne")
        {
            return ADVICE_TXT +
                "\n\nEn effet ici tu essaie de sélectionner '"
                + inputs[1] + "' est non la moyenne de l'étudiant recherché.";

        }
        else if (inputs[6] == "!=")
        {
            return ADVICE_TXT +
                "\n\nNous recherchons un seul résultat, et tu essaies d'utiliser l'opérateur '!=' qui exclue un résultat au lieu de le choisir.";

        }
        else if (inputs[5] == "moyenne")
        {
            return ADVICE_TXT +
                "\n\nTu essaie de selectionner la moyenne d'un étudiant grâce à sa moyenne... qui est inconnue. " +
                "Tu devrais essayer d'utiliser une colonne qui différencie tous les étudiants. ";

        }
        else if (inputs[5] != "id_etudiant")
        {
            return ADVICE_TXT +
                "\n\nTu essaie de selectionner la moyenne d'un étudiant où son " + inputs[5] + " n'est pas forcement unique." +
                "Tu devrais essayer d'utiliser une colonne qui différencie tous les étudiants. ";

        }
        else if (inputs[7] != "5")
        {
            return ADVICE_TXT +
                "\n\nTu y es presque ! Mais la moyenne de l'étudiant " + inputs[7] + " est déja connue... ";

        } else
        {
            succes = true;
            return "14.08\n\nMessage de M. Nick :\nBravo, tu as réussi à trouver la moyenne cachée ! Viens donc me retrouver dans le couloir.";
        }
    }


    public string removeSpace(string input)
    {
        string result = input.Trim(' ');
        return result;
    }
}
