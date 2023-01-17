using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SQLTest
{
    [Test]
    public void remove_leading_and_trailing_space()
    {
        SqlManager sqlManager = new SqlManager();

        Assert.AreEqual(sqlManager.removeSpace(" a b c "), "a b c");

    }

    [Test]
    public void error_on_select_field()
    {
        string[] inputs = { "s", "moyenne", "from", "etudiant_g4a", "where", "id_etudiant", "=", "5"};
        SqlManager sqlManager = new SqlManager();

        Assert.AreEqual(sqlManager.checkForError(inputs), SqlManager.ERROR_TXT + SqlManager.SYNTAX_EXCEPTION_TXT + "Erreur ligne 1 : '"
                + inputs[0] + SqlManager.UNKNOWN_SYNTAX_TXT);

    }

    [Test]
    public void error_on_from_field()
    {
        string[] inputs = { "select", "moyenne", "f", "etudiant_g4a", "where", "id_etudiant", "=", "5" };
        SqlManager sqlManager = new SqlManager();

        Assert.AreEqual(sqlManager.checkForError(inputs), SqlManager.ERROR_TXT + SqlManager.SYNTAX_EXCEPTION_TXT + "Erreur ligne 2 : '"
                + inputs[2] + SqlManager.UNKNOWN_SYNTAX_TXT);

    }

    [Test]
    public void error_on_where_field()
    {
        string[] inputs = { "select", "moyenne", "from", "etudiant_g4a", "w", "id_etudiant", "=", "5" };
        SqlManager sqlManager = new SqlManager();

        Assert.AreEqual(sqlManager.checkForError(inputs), SqlManager.ERROR_TXT + SqlManager.SYNTAX_EXCEPTION_TXT + "Erreur ligne 3 : '"
                + inputs[4] + SqlManager.UNKNOWN_SYNTAX_TXT);

    }

    [Test]
    public void table_name_field_is_unknown()
    {
        string[] inputs = { "select", "moyenne", "from", "xx", "where", "id_etudiant", "=", "5" };
        SqlManager sqlManager = new SqlManager();

        Assert.AreEqual(sqlManager.checkForError(inputs), SqlManager.ERROR_TXT + SqlManager.UNKNOWN_TABLE_EXCEPTION_TXT + "Erreur ligne 3 : La table '"
                + inputs[3] + "' n'existe pas.");

    }

    [Test]
    public void line_1_column_is_unknown()
    {
        string[] inputs = { "select", "x", "from", "etudiant_g4a", "where", "id_etudiant", "=", "5" };
        SqlManager sqlManager = new SqlManager();

        Assert.AreEqual(sqlManager.checkForError(inputs), SqlManager.ERROR_TXT + SqlManager.UNKNOWN_COLUMN_EXCEPTION_TXT + "Erreur ligne 1 : '"
                + inputs[1] + "' n'est pas une colonne de la table ETUDIANT_G4A.");

    }

    [Test]
    public void line_3_column_is_unknown()
    {
        string[] inputs = { "select", "moyenne", "from", "etudiant_g4a", "where", "x", "=", "5" };
        SqlManager sqlManager = new SqlManager();

        Assert.AreEqual(sqlManager.checkForError(inputs), SqlManager.ERROR_TXT + SqlManager.UNKNOWN_COLUMN_EXCEPTION_TXT + "Erreur ligne 3 : '"
                + inputs[5] + "' n'est pas une colonne de la table ETUDIANT_G4A.");

    }

    [Test]
    public void error_when_id_etudiant_not_integer()
    {
        string[] inputs = { "select", "moyenne", "from", "etudiant_g4a", "where", "id_etudiant", "=", "cinq" };
        SqlManager sqlManager = new SqlManager();

        Assert.AreEqual(sqlManager.checkForError(inputs), SqlManager.ERROR_TXT + SqlManager.MATCH_TYPE_EXCEPTION_TXT
                + inputs[7] + "' n'est pas un entier et ne correspond pas avec la colonne 'id_etudiant'");

    }
    [Test]
    public void error_when_moyenne_not_float()
    {
        string[] inputs = { "select", "moyenne", "from", "etudiant_g4a", "where", "moyenne", "=", "cinq" };
        SqlManager sqlManager = new SqlManager();

        Assert.AreEqual(sqlManager.checkForError(inputs), SqlManager.ERROR_TXT + SqlManager.MATCH_TYPE_EXCEPTION_TXT
                + inputs[7] + "' n'est pas un réel et ne correspond pas avec la colonne 'moyenne'");

    }

}
