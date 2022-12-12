using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progression
{
    private List<Competence> competences;


    public Progression()
    {
        competences = new List<Competence>();
        competences.Add(new Competence(1, "R�aliser un d�veloppement d'application"));
        competences.Add(new Competence(2, "Optimiser des applications"));
        competences.Add(new Competence(3, "Administrer des syst�mes informatiques communicants complexes"));
        competences.Add(new Competence(4, "G�rer des donn�es de l'information"));
        competences.Add(new Competence(5, "Conduire un projet"));
        competences.Add(new Competence(6, "Collaborer au sein d'une �quipe informatique"));
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
