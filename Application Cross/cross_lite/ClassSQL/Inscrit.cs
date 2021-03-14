using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassSQL
{
    public class Inscrit
    {
        public int id_inscrit;
        public int id_eleve;
        public int id_transpondeur;
        public int num_d;

        public string nom;
        public string prenom;
        public string classe;
        public TimeSpan temps;
        public string tag;
        public Sexe genre;
        public int Rang;

        public Inscrit(int id_e,int id_t,int n_d)
        {
            id_eleve = id_e;
            id_transpondeur = id_t;
            num_d = n_d;
        }

        public override string ToString()
        {
            return nom + " " + prenom;
        }
    }
}

