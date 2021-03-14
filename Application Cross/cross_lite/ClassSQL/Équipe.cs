using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassSQL
{
    public class Équipe
    {
        uint Place;
        uint Points_Total;
        string Nom_Équipe;

        string Eleve1, Eleve2, Eleve3, Eleve4;
        uint Points_eleve1, Points_eleve2, Points_eleve3, Points_eleve4;

        public Équipe(uint points_total,string nom_équipe,uint place,string eleve1,string eleve2,string eleve3,string eleve4,uint pts_el1,uint pts_el2, uint pts_el3, uint pts_el4)
        {
            Place = place;
            Points_Total = points_total;
            Nom_Équipe = nom_équipe;
            //--------------
            Eleve1 = eleve1;
            Eleve2 = eleve2;
            Eleve3 = eleve3;
            Eleve4 = eleve4;
            //--
            Points_eleve1 = pts_el1;
            Points_eleve2 = pts_el2;
            Points_eleve3 = pts_el3;
            Points_eleve4 = pts_el4;

        }

        public override string ToString()
        {
            string chaine ="";
            switch (Place)
            {
                case 1: chaine = "[" + Place + "er] " + Nom_Équipe + " - " + Points_Total + " pts ( " + Eleve1 + " - " + Points_eleve1 +"pts, " + Eleve2 + " - " + Points_eleve2+ "pts, " + Eleve3 + " - " + Points_eleve3+ "pts, " + Eleve4 + " - " + Points_eleve4 + "pts )"; break;
                case 2: chaine = "[" + Place + "ième] " + Nom_Équipe + " - " + Points_Total + " pts ( " + Eleve1 + " - " + Points_eleve1 + "pts, " + Eleve2 + " - " + Points_eleve2 + "pts, " + Eleve3 + " - " + Points_eleve3 + "pts, " + Eleve4 + " - " + Points_eleve4 + "pts )"; break;
                case 3: chaine = "[" + Place + "ième]  " + Nom_Équipe + " - " + Points_Total + " pts ( " + Eleve1 + " - " + Points_eleve1 + "pts, " + Eleve2 + " - " + Points_eleve2 + "pts, " + Eleve3 + " - " + Points_eleve3 + "pts, " + Eleve4 + " - " + Points_eleve4 + "pts )"; break;
                default: chaine = "[" + Place + "]  " + Nom_Équipe + " - " + Points_Total + " pts ( " + Eleve1 + " - " + Points_eleve1 + "pts, " + Eleve2 + " - " + Points_eleve2 + "pts, " + Eleve3 + " - " + Points_eleve3 + "pts, " + Eleve4 + " - " + Points_eleve4 + "pts )"; break;
            }
            return chaine;
        }
    }
}
