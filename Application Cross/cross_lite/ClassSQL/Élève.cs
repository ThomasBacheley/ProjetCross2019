using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassSQL
{
    public enum Sexe
    {
        M, F
    }
    public enum Unité
    {
        LA, LGT, LP, BTS, UFA, Personnel
    }
    public enum Caté
    {
        Seniors_Masters,Espoirs,Juniors,Cadets,Minimes
    }
    //--------
    public class Élève
    {
        public int id;
        public string nom;
        public string prenom;
        public Sexe sexe;
        public DateTime DateNaissance;
        public Unité upedagogique;
        public string classe;

        public Élève(string Nom, string Prenom, Sexe Sex, DateTime DatedeNaissance, Unité Upedagogique, string Classe)
        {
            nom = Nom.ToUpper();
            prenom = Prenom.Substring(0, 1).ToUpper() + Prenom.Substring(1, (Prenom.Length - 1)).ToLower();
            classe = Classe.ToUpper();
            sexe = Sex;
            DateNaissance = DatedeNaissance;
            upedagogique = Upedagogique;
        }

        public override string ToString()
        {
            return nom + " " + prenom;
        }
    }
}

