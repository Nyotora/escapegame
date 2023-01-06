using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SQLprofessor : Professor
{
    public HUD hud;
    public SqlPC pc;
    public Player player;
    public scr_CharacterController controller;
    public SQLkeyCinematic cinematic;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override public void Interact(Player player)
    {
        if (!player.GetProgression().getCompetence(3).IsValidated())
        {
            if (pc.IsQueryValid())
            {
                controller.disableInput();
                textLines = new string[] { "Oh bravo tu as réussi !", "Laisse moi valider ta compétence..." };
                player.GetProgression().ValidCompetence(3);
                dialogueBox.setCinematic(cinematic);
                _prompt = "";

            }
            else if (pc.canBeAccessed())
            {
                textLines = new string[] { "Tu veux que je répète ? Alors écoute.",
                    "Je dois afficher les notes des élèves mais j’ai renversé du café sur la feuille, peux-tu retrouver la note de l’élève Lamarche Mathieu dans la base de donnée de l’IUT ?",
                "Pour cela, tu peux utiliser mon ordinateur que j'ai laissé dans la salle R50.",
                "Tu pourras y rentrer ce qu'on appelle une requête SQL. Tu peux trouver de l'aide directement sur le PC.",
                "Il y a une fiche contenant les détails des données existantes, comme le nom des différents champs ou encore les détails des étudiants.",
                "Tu en aura certainement besoin pour choisir le bon étudiant. Cette fiche est quelque part dans une salle.",
                "Je compte sur toi !"};
            }
            else
            {
                hud.setCompetenceText(3);
                pc.giveAccess();
            }
            dialogueBox.textComponent.text = string.Empty;
            dialogueBox.lines = textLines;
            dialogueBox.StartDialogue();
        }

    }
}
