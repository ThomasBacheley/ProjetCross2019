using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassSQL
{
    public class Classe
    {
        public int id;
        public string classe;
        public string unite_pedagogique;

        public Classe(int ID, string cl, string up)
        {
            id = ID;
            classe = cl;
            unite_pedagogique = up;
        }

        public override string ToString()
        {
            //return "(id:"+id + ") : " + classe;
            return classe;
        }
    }
}

