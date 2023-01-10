using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SqlManager
{
    private string[] validColumn = new string[] { "id_etudiant", "nom", "prenom", "moyenne" };
    private string[] validOperations = new string[] { "=", "!=" };
    private bool error;
    private bool succes;

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

    public string checkForError(string[] inputs)
    {
        error = true;
        if (inputs[0].ToUpper() != "SELECT")
        {
            return "ERREUR LORS DE L'EXECUTION SQL :\nErreur syntaxe : SyntaxeException(0)\n\nErreur ligne 1 : '"
                + inputs[0] + "' n'est pas reconnu dans la syntaxe SQL.";

        }
        else if (inputs[2].ToUpper() != "FROM")
        {
            return "ERREUR LORS DE L'EXECUTION SQL :\nErreur syntaxe : SyntaxeException(0)\n\nErreur ligne 2 : '"
                + inputs[2] + "' n'est pas reconnu dans la syntaxe SQL.";

        }
        else if (inputs[4].ToUpper() != "WHERE")
        {
            return "ERREUR LORS DE L'EXECUTION SQL :\nErreur syntaxe : SyntaxeException(0)\n\nErreur ligne 3 : '"
                + inputs[4] + "' n'est pas reconnu dans la syntaxe SQL.";

        }
        else if (inputs[1].ToUpper() == "")
        {
            return "ERREUR LORS DE L'EXECUTION SQL :\nErreur syntaxe : NullPointerException(0)\n\nErreur ligne 1 : "
                + "Un nom de colonne est attendu apr�s 'SELECT'.";

        }
        else if (inputs[3].ToUpper() == "")
        {
            return "ERREUR LORS DE L'EXECUTION SQL :\nErreur syntaxe : NullPointerException(0)\n\nErreur ligne 2 : "
                + "Un nom de table est attendu apr�s 'FROM'.";

        }
        else if (inputs[5].ToUpper() == "" || inputs[6].ToUpper() == "" || inputs[7].ToUpper() == "")
        {
            return "ERREUR LORS DE L'EXECUTION SQL :\nErreur syntaxe : NullPointerException(0)\n\nErreur ligne 3 : "
                + "Une condition est attendue apr�s 'WHERE'.";

        }
        else if (inputs[3].ToUpper() != "ETUDIANT_G4A")
        {
            return "ERREUR LORS DE L'EXECUTION SQL :\nErreur syntaxe : UnknownTableException(0)\n\nErreur ligne 3 : La table '"
                + inputs[3] + "' n'existe pas.";

        }
        else if (!validColumn.Contains(inputs[1]))
        {
            return "ERREUR LORS DE L'EXECUTION SQL :\nErreur syntaxe : UnknownColumnException(0)\n\nErreur ligne 3 : '"
                + inputs[1] + "' n'est pas une colonne de la table ETUDIANT_G4A.";

        }
        else if (!validColumn.Contains(inputs[5]))
        {
            return "ERREUR LORS DE L'EXECUTION SQL :\nErreur syntaxe : UnknownColumnException(0)\n\nErreur ligne 3 : '"
                + inputs[5] + "' n'est pas une colonne de la table ETUDIANT_G4A.";

        }
        else if (!validOperations.Contains(inputs[6]))
        {
            return "ERREUR LORS DE L'EXECUTION SQL :\nErreur syntaxe : UnknownOperatorException(0)\n\nErreur ligne 3 : Impossible d'effectuer l'op�ration '"
                + inputs[6] + "' entre '" + inputs[5] + "' et '" + inputs[7] + "'.";

        }
        else if (!int.TryParse(inputs[7], out _) && inputs[5] == "id_etudiant")
        {
            return "ERREUR LORS DE L'EXECUTION SQL :\nErreur syntaxe : MatchTypeException(0)\n\nErreur ligne 3 : '"
                + inputs[7] + "' n'est pas un entier et ne correspond pas avec la colonne 'id_etudiant'";

        }
        else if (!float.TryParse(inputs[7], out _) && inputs[5] == "moyenne")
        {
            return"ERREUR LORS DE L'EXECUTION SQL :\nErreur syntaxe : MatchTypeException(0)\n\nErreur ligne 3 : '"
                + inputs[7] + "' n'est pas un entier et ne correspond pas avec la colonne 'moyenne'";

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
            return "Conseil de M. Nick :\nTa requ�te est syntaxiquement juste, mais le r�sultat n'est pas celui esp�r�." +
                "\n\nEn effet ici tu essaie de s�lectionner '"
                + inputs[1] + "' est non la moyenne de l'�tudiant recherch�.";

        }
        else if (inputs[6] == "!=")
        {
            return "Conseil de M. Nick :\nTa requ�te est syntaxiquement juste, mais le r�sultat n'est pas celui esp�r�." +
                "\n\nNous recherchons un seul r�sultat, et tu essaies d'utiliser l'op�rateur '!=' qui exclue un r�sultat au lieu de le choisir.";

        }
        else if (inputs[5] == "moyenne")
        {
            return "Conseil de M. Nick :\nTa requ�te est syntaxiquement juste, mais le r�sultat n'est pas celui esp�r�." +
                "\n\nTu essaie de selectionner la moyenne d'un �tudiant gr�ce � sa moyenne... qui est inconnue. " +
                "Tu devrais essayer d'utiliser une colonne qui diff�rencie tous les �tudiants. ";

        }
        else if (inputs[5] != "id_etudiant")
        {
            return "Conseil de M. Nick :\nTa requ�te est syntaxiquement juste, mais le r�sultat n'est pas celui esp�r�." +
                "\n\nTu essaie de selectionner la moyenne d'un �tudiant o� son " + inputs[5] + " n'est pas forcement unique." +
                "Tu devrais essayer d'utiliser une colonne qui diff�rencie tous les �tudiants. ";

        }
        else if (inputs[7] != "5")
        {
            return "Conseil de M. Nick :\nTa requ�te est syntaxiquement juste, mais le r�sultat n'est pas celui esp�r�." +
                "\n\nTu y es presque ! Mais la moyenne de l'�tudiant " + inputs[7] + " est d�ja connue... ";

        } else
        {
            succes = true;
            return "14.08\n\nMessage de M. Nick :\nBravo, tu as r�ussi � trouver la moyenne cach�e ! Viens donc me retrouver dans le couloir.";
        }
    }
}
