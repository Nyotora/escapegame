using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progression
{
    private List<Competence> competences;


    public Progression()
    {
        competences = new List<Competence>();
        competences.Add(new Competence(1, "Réaliser un développement d'application","La réponse est peut-être corrompue..."));
        competences.Add(new Competence(2, "Optimiser des applications", "Gare au mauvaises odeurs !"));
        competences.Add(new Competence(3, "Administrer des systèmes informatiques communicants complexes", "Une machine peut faire fausse route !"));
        competences.Add(new Competence(4, "Gérer des données de l'information", "Sans moyenne, pas d'année !"));
        competences.Add(new Competence(5, "Conduire un projet", "Se fixer des objectifs, c'est plutôt \"smart\" !"));
        competences.Add(new Competence(6, "Collaborer au sein d'une équipe informatique", "Il faut correctement versionner ses projets !"));
    }

    public void ValidCompetence(int id)
    {
        competences[id].Validate();
    }

    public Competence getCompetence(int id)
    {
        return competences[id];
    }
}
