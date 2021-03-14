using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassSQL
{
    public class SQL
    {
        private string connexionString = "";

        public SQL(string serveur,string bdd,string user,string psw)
        {
            SqlConnection connexion = null;

            try
            {
                string chaine = "Data Source="+serveur+@"\SQLEXPRESS;Initial Catalog="+ bdd + ";User Id=" + user + ";Password="+ psw;
                connexion = new SqlConnection(chaine);
                connexion.Open();
                connexionString = chaine;

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (connexion != null)
                {
                    connexion.Close();
                }
            }
        }       

        public void add_e(Élève eleve)
        {
            SqlConnection connexion = null;

            try
            {
                connexion = new SqlConnection(connexionString);

                SqlCommand cmd = new SqlCommand("enregistrement_eleve", connexion);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter RetVal = cmd.Parameters.Add("RetVal", SqlDbType.Int);
                RetVal.Direction = ParameterDirection.ReturnValue;

                SqlParameter NomE = cmd.Parameters.Add("@nom", SqlDbType.VarChar);
                NomE.Direction = ParameterDirection.Input;

                SqlParameter PrenomE = cmd.Parameters.Add("@prenom", SqlDbType.VarChar);
                PrenomE.Direction = ParameterDirection.Input;

                SqlParameter GenreE = cmd.Parameters.Add("@genre", SqlDbType.VarChar);
                GenreE.Direction = ParameterDirection.Input;

                SqlParameter Date_N = cmd.Parameters.Add("@date_n", SqlDbType.Date);
                Date_N.Direction = ParameterDirection.Input;

                SqlParameter ClasseE = cmd.Parameters.Add("@classe", SqlDbType.VarChar);
                ClasseE.Direction = ParameterDirection.Input;

                SqlParameter UP = cmd.Parameters.Add("@up", SqlDbType.VarChar);
                UP.Direction = ParameterDirection.Input;

                NomE.Value = eleve.nom;
                PrenomE.Value = eleve.prenom;
                GenreE.Value = eleve.sexe.ToString();
                Date_N.Value = eleve.DateNaissance;
                ClasseE.Value = eleve.classe;
                UP.Value = eleve.upedagogique.ToString();

                connexion.Open();

                cmd.ExecuteNonQuery();

                int ret = Convert.ToInt32(RetVal.Value);

                switch (ret)
                {
                    case 0:
                        //RAS
                        return;
                    case 1:
                        throw new Exception("la classe n'a pas été insérée");
                    case 2:
                        throw new Exception("la catégorie n'a pas été trouvée");
                    case 3:
                        throw new Exception("l'élève existe déja");
                    case 4:
                        throw new Exception("l'éleve n'a pas été ajouté");
                    default:
                        throw new Exception("erreur SQL inconnue");
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connexion != null)
                {
                    connexion.Close();
                }
                    
            }
        }

        public List<Classe> GetClasses()
        {
            SqlConnection connexion = null;
            SqlDataReader reader = null;

            try
            {
                connexion = new SqlConnection(connexionString);
                List<Classe> classes = new List<Classe>();

                connexion.Open();
                string requete = " SELECT DISTINCT * FROM table_classe ORDER BY classe ASC";

                SqlCommand cmd = new SqlCommand(requete, connexion);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Classe cl = new Classe(Convert.ToInt32(reader["id_classe"]), reader["classe"].ToString(), reader["unite_pedagogique"].ToString());
                    classes.Add(cl);
                }


                return classes;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connexion != null)
                {
                    connexion.Close();
                }
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        public List<Inscrit> getinscrit()
        {
            SqlConnection connexion = null;
            SqlDataReader reader = null;

            try
            {
                connexion = new SqlConnection(connexionString);
                List<Inscrit> inscrits = new List<Inscrit>();

                connexion.Open();
                string requete = "SELECT id_inscrit,idtranspondeur,ideleve,table_eleve.nom,table_eleve.prenom,numero_dossard,classe FROM table_inscrit,table_eleve,table_classe WHERE table_inscrit.ideleve=table_eleve.id_eleve AND table_eleve.idclasse=table_classe.id_classe ORDER BY classe ASC";

                SqlCommand cmd = new SqlCommand(requete, connexion);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Inscrit ins = new Inscrit(Convert.ToInt32(reader["ideleve"]),Convert.ToInt32(reader["idtranspondeur"]),Convert.ToInt32(reader["numero_dossard"]));
                    ins.nom = reader["nom"].ToString();
                    ins.prenom = reader["prenom"].ToString();
                    ins.classe = reader["classe"].ToString();

                    inscrits.Add(ins);
                }


                return inscrits;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connexion != null)
                {
                    connexion.Close();
                }
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        public List<Élève> GetÉlevesClasse(int id)
        {
            SqlConnection connexion = null;
            SqlDataReader reader = null;

            try
            {
                connexion = new SqlConnection(connexionString);
                List<Élève> eleve = new List<Élève>();

                connexion.Open();
                string requete = " SELECT * FROM table_eleve,table_classe WHERE table_eleve.idclasse=table_classe.id_classe AND idclasse=" + id + " ORDER BY nom ASC,prenom ASC";

                SqlCommand cmd = new SqlCommand(requete, connexion);

                reader = cmd.ExecuteReader();

                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Sexe s = Sexe.M;
                            if (reader["genre"].ToString() == "F")
                            {
                                s = Sexe.F;
                            }

                            Unité up = Unité.LA;
                            switch (reader["unite_pedagogique"].ToString().ToUpper())
                            {
                                case "LGT": up = Unité.LGT; break;
                                case "LP": up = Unité.LP; break;
                                case "BTS": up = Unité.BTS; break;
                                case "UFA": up = Unité.UFA; break;
                                case "PERSONNEL": up = Unité.Personnel; break;
                            }

                            Élève el = new Élève(reader["nom"].ToString(), reader["prenom"].ToString(), s, Convert.ToDateTime(reader["date_naissance"]), up, reader["classe"].ToString());
                            el.id = Convert.ToInt32(reader["id_eleve"]);
                            eleve.Add(el);
                        }

                    }
                }


                return eleve;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connexion != null)
                {
                    connexion.Close();
                }
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }
        public List<Élève> GetÉleves()
        {
            SqlConnection connexion = null;
            SqlDataReader reader = null;

            try
            {
                connexion = new SqlConnection(connexionString);
                List<Élève> eleves = new List<Élève>();

                connexion.Open();
                string requete = " SELECT * FROM table_eleve,table_classe WHERE table_eleve.idclasse=table_classe.id_classe ORDER BY nom ASC,prenom ASC";

                SqlCommand cmd = new SqlCommand(requete, connexion);

                reader = cmd.ExecuteReader();

                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Sexe s = Sexe.M;
                            if (reader["genre"].ToString() == "F")
                            {
                                s = Sexe.F;
                            }

                            Unité up = Unité.LA;
                            switch (reader["unite_pedagogique"].ToString().ToUpper())
                            {
                                case "LGT": up = Unité.LGT; break;
                                case "LP": up = Unité.LP; break;
                                case "BTS": up = Unité.BTS; break;
                                case "UFA": up = Unité.UFA; break;
                                case "PERSONNEL": up = Unité.Personnel; break;
                            }

                            Élève el = new Élève(reader["nom"].ToString(), reader["prenom"].ToString(), s, Convert.ToDateTime(reader["date_naissance"]), up, reader["classe"].ToString());
                            el.id = Convert.ToInt32(reader["id_eleve"]);
                            eleves.Add(el);
                        }

                    }
                }


                return eleves;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connexion != null)
                {
                    connexion.Close();
                }
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        public void Inscrire(int id_e, int num_dos, string tag)
        {
            SqlConnection connexion = null;

            try
            {
                connexion = new SqlConnection(connexionString);

                SqlCommand cmd = new SqlCommand("inscription", connexion);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter RetVal = cmd.Parameters.Add("RetVal", SqlDbType.Int);
                RetVal.Direction = ParameterDirection.ReturnValue;

                SqlParameter id_el = cmd.Parameters.Add("@id_e", SqlDbType.Int);
                id_el.Direction = ParameterDirection.Input;

                SqlParameter num_d = cmd.Parameters.Add("@num_dos", SqlDbType.Int);
                num_d.Direction = ParameterDirection.Input;

                SqlParameter TAG = cmd.Parameters.Add("@tag", SqlDbType.VarChar);
                TAG.Direction = ParameterDirection.Input;

                id_el.Value = id_e;
                num_d.Value = num_dos;
                TAG.Value = tag;

                connexion.Open();

                cmd.ExecuteNonQuery();

                int ret = Convert.ToInt32(RetVal.Value);

                switch (ret)
                {
                    case 0:
                        //RAS
                        return;
                    case 1:
                        throw new Exception("l'eleve est deja inscrit");
                    case 2:
                        throw new Exception("le bagde na pas été ajouté");
                    case 3:
                        throw new Exception("le numero de dossard est déja pris");
                    case 4:
                        throw new Exception("l'éleve n'a pas été ajouté");
                    default:
                        throw new Exception("erreur SQL inconnue : l'inscription n'a pas aboutie");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connexion != null)
                {
                    connexion.Close();
                }

            }
        }

        public void update_t(TimeSpan temps_calc,string tag)
        {
            SqlConnection connexion = null;

            try
            {
                connexion = new SqlConnection(connexionString);

                SqlCommand cmd = new SqlCommand("update_temps", connexion);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter RetVal = cmd.Parameters.Add("RetVal", SqlDbType.Int);
                RetVal.Direction = ParameterDirection.ReturnValue;

                SqlParameter temps = cmd.Parameters.Add("@t", SqlDbType.Time);
                temps.Direction = ParameterDirection.Input;

                SqlParameter TAG = cmd.Parameters.Add("@tag", SqlDbType.VarChar);
                TAG.Direction = ParameterDirection.Input;

                temps.Value = temps_calc;
                TAG.Value = tag;

                connexion.Open();

                cmd.ExecuteNonQuery();

                int ret = Convert.ToInt32(RetVal.Value);

                switch (ret)
                {
                    case 0:
                        //RAS
                        return;
                    case 1:
                        throw new Exception("le badge n'est pas répétorié");
                    case 2:
                        throw new Exception("l'eleve n'a pas était retrouvé");
                    case 3:
                        throw new Exception("le temps n'a pas était mis a jour");
                    default:
                        throw new Exception("erreur SQL inconnue : le temps n'a pas pu etre mis a jour");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connexion != null)
                {
                    connexion.Close();
                }

            }
        }

        public void clear_bdd()
        {
            SqlConnection connexion = null;
            try
            {
                connexion = new SqlConnection(connexionString);

                SqlCommand cmd = new SqlCommand("clear_table", connexion);
                cmd.CommandType = CommandType.StoredProcedure;

                connexion.Open();

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connexion != null)
                {
                    connexion.Close();
                }

            }
        }

        public Élève get1ereF()
        {
            SqlConnection connexion = null;
            SqlDataReader reader = null;
            Élève el = null;
            try
            {
                connexion = new SqlConnection(connexionString);

                connexion.Open();
                string requete = "SELECT MIN(temps),nom,prenom,classe,date_naissance,unite_pedagogique FROM table_eleve,table_classe WHERE genre ='F' AND classe!='Personnel' AND table_eleve.idclasse=table_classe.id_classe";

                SqlCommand cmd = new SqlCommand(requete, connexion);

                reader = cmd.ExecuteReader();

                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Unité up = Unité.LA;
                            switch (reader["unite_pedagogique"].ToString().ToUpper())
                            {
                                case "LGT": up = Unité.LGT; break;
                                case "LP": up = Unité.LP; break;
                                case "BTS": up = Unité.BTS; break;
                                case "UFA": up = Unité.UFA; break;
                            }

                            el = new Élève(reader["nom"].ToString(), reader["prenom"].ToString(), Sexe.F, Convert.ToDateTime(reader["date_naissance"]), up, reader["classe"].ToString());
                        }
                    }
                }
                return el;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connexion != null)
                {
                    connexion.Close();
                }
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        public Élève get1erM()
        {
            SqlConnection connexion = null;
            SqlDataReader reader = null;
            Élève el = null;
            try
            {
                connexion = new SqlConnection(connexionString);

                connexion.Open();
                string requete = "SELECT MIN(temps),nom,prenom,classe,date_naissance,unite_pedagogique FROM table_eleve,table_classe WHERE genre ='M' AND classe!='Personnel' AND table_eleve.idclasse=table_classe.id_classe";

                SqlCommand cmd = new SqlCommand(requete, connexion);

                reader = cmd.ExecuteReader();

                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Unité up = Unité.LA;
                            switch (reader["unite_pedagogique"].ToString().ToUpper())
                            {
                                case "LGT": up = Unité.LGT; break;
                                case "LP": up = Unité.LP; break;
                                case "BTS": up = Unité.BTS; break;
                                case "UFA": up = Unité.UFA; break;
                            }

                            el = new Élève(reader["nom"].ToString(), reader["prenom"].ToString(), Sexe.M, Convert.ToDateTime(reader["date_naissance"]), up, reader["classe"].ToString());
                        }
                    }
                }
                return el;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connexion != null)
                {
                    connexion.Close();
                }
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        public Élève get1erPersonnel()
        {
            SqlConnection connexion = null;
            SqlDataReader reader = null;
            Élève el = null;
            try
            {
                connexion = new SqlConnection(connexionString);

                connexion.Open();
                string requete = "MIN(temps),nom,prenom,genre,classe,unite_pedagogique FROM table_eleve,table_classe WHERE classe='Personnel' AND table_eleve.idclasse=table_classe.id_classe";

                SqlCommand cmd = new SqlCommand(requete, connexion);

                reader = cmd.ExecuteReader();

                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Sexe s = Sexe.M;
                            if (reader["genre"].ToString() == "F")
                            {
                                s = Sexe.F;
                            }
                            Unité up = Unité.LA;
                            switch (reader["unite_pedagogique"].ToString().ToUpper())
                            {
                                case "LGT": up = Unité.LGT; break;
                                case "LP": up = Unité.LP; break;
                                case "BTS": up = Unité.BTS; break;
                                case "UFA": up = Unité.UFA; break;
                            }

                            el = new Élève(reader["nom"].ToString(), reader["prenom"].ToString(), s, Convert.ToDateTime(reader["date_naissance"]), up, reader["classe"].ToString());
                        }
                    }
                }
                return el;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connexion != null)
                {
                    connexion.Close();
                }
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        public Élève get1erCadet()
        {
            SqlConnection connexion = null;
            SqlDataReader reader = null;
            Élève el = null;
            try
            {
                connexion = new SqlConnection(connexionString);

                connexion.Open();
                string requete = "SELECT MIN(temps),nom,prenom,genre,classe,unite_pedagogique FROM table_eleve,table_classe,table_categorie AND table_eleve.idclasse=table_classe.id_classe AND table_eleve.idcategorie=table_categorie.id_categorie AND categorie='Cadets'";

                SqlCommand cmd = new SqlCommand(requete, connexion);

                reader = cmd.ExecuteReader();

                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Sexe s = Sexe.M;
                            if (reader["genre"].ToString() == "F")
                            {
                                s = Sexe.F;
                            }

                            Unité up = Unité.LA;
                            switch (reader["unite_pedagogique"].ToString().ToUpper())
                            {
                                case "LGT": up = Unité.LGT; break;
                                case "LP": up = Unité.LP; break;
                                case "BTS": up = Unité.BTS; break;
                                case "UFA": up = Unité.UFA; break;
                            }

                            el = new Élève(reader["nom"].ToString(), reader["prenom"].ToString(), s, Convert.ToDateTime(reader["date_naissance"]), up, reader["classe"].ToString());
                        }
                    }
                }
                return el;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connexion != null)
                {
                    connexion.Close();
                }
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        public Élève get1erJunior()
        {
            SqlConnection connexion = null;
            SqlDataReader reader = null;
            Élève el = null;
            try
            {
                connexion = new SqlConnection(connexionString);

                connexion.Open();
                string requete = "SELECT MIN(temps),nom,prenom,genre,classe,unite_pedagogique FROM table_eleve,table_classe,table_categorie AND table_eleve.idclasse=table_classe.id_classe AND table_eleve.idcategorie=table_categorie.id_categorie AND categorie='Juniors'";

                SqlCommand cmd = new SqlCommand(requete, connexion);

                reader = cmd.ExecuteReader();

                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Sexe s = Sexe.M;
                            if (reader["genre"].ToString() == "F")
                            {
                                s = Sexe.F;
                            }

                            Unité up = Unité.LA;
                            switch (reader["unite_pedagogique"].ToString().ToUpper())
                            {
                                case "LGT": up = Unité.LGT; break;
                                case "LP": up = Unité.LP; break;
                                case "BTS": up = Unité.BTS; break;
                                case "UFA": up = Unité.UFA; break;
                            }

                            el = new Élève(reader["nom"].ToString(), reader["prenom"].ToString(), s, Convert.ToDateTime(reader["date_naissance"]), up, reader["classe"].ToString());
                        }
                    }
                }
                return el;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connexion != null)
                {
                    connexion.Close();
                }
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        public Élève get1erMinime()
        {
            SqlConnection connexion = null;
            SqlDataReader reader = null;
            Élève el = null;
            try
            {
                connexion = new SqlConnection(connexionString);

                connexion.Open();
                string requete = "SELECT MIN(temps),nom,prenom,genre,classe,unite_pedagogique FROM table_eleve,table_classe,table_categorie AND table_eleve.idclasse=table_classe.id_classe AND table_eleve.idcategorie=table_categorie.id_categorie AND categorie='Minimes'";

                SqlCommand cmd = new SqlCommand(requete, connexion);

                reader = cmd.ExecuteReader();

                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Sexe s = Sexe.M;
                            if (reader["genre"].ToString() == "F")
                            {
                                s = Sexe.F;
                            }

                            Unité up = Unité.LA;
                            switch (reader["unite_pedagogique"].ToString().ToUpper())
                            {
                                case "LGT": up = Unité.LGT; break;
                                case "LP": up = Unité.LP; break;
                                case "BTS": up = Unité.BTS; break;
                                case "UFA": up = Unité.UFA; break;
                            }

                            el = new Élève(reader["nom"].ToString(), reader["prenom"].ToString(), s, Convert.ToDateTime(reader["date_naissance"]), up, reader["classe"].ToString());
                        }
                    }
                }
                return el;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connexion != null)
                {
                    connexion.Close();
                }
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        public List<Élève> GetTOP10()
        {
            SqlConnection connexion = null;
            SqlDataReader reader = null;

            try
            {
                connexion = new SqlConnection(connexionString);
                List<Élève> eleves = new List<Élève>();

                connexion.Open();
                string requete = "SELECT TOP 10 nom,prenom,genre,classe,unite_pedagogique FROM table_eleve,table_classe WHERE table_eleve.idclasse=table_classe.id_classe ORDER BY temps ASC";

                SqlCommand cmd = new SqlCommand(requete, connexion);

                reader = cmd.ExecuteReader();

                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Sexe s = Sexe.M;
                            if (reader["genre"].ToString() == "F")
                            {
                                s = Sexe.F;
                            }

                            Unité up = Unité.LA;
                            switch (reader["unite_pedagogique"].ToString().ToUpper())
                            {
                                case "LGT": up = Unité.LGT; break;
                                case "LP": up = Unité.LP; break;
                                case "BTS": up = Unité.BTS; break;
                                case "UFA": up = Unité.UFA; break;
                                case "PERSONNEL": up = Unité.Personnel; break;
                            }

                            Élève el = new Élève(reader["nom"].ToString(), reader["prenom"].ToString(), s, Convert.ToDateTime(reader["date_naissance"]), up, reader["classe"].ToString());
                            eleves.Add(el);
                        }

                    }
                }


                return eleves;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connexion != null)
                {
                    connexion.Close();
                }
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        public void upd_rendu(string tag)
        {
            SqlConnection connexion = null;
            try
            {
                connexion = new SqlConnection(connexionString);

                connexion.Open();
                string requete = "update table_transpondeur set rendu = 1 WHERE tag = '" + tag.ToString() +"'";

                SqlCommand cmd = new SqlCommand(requete, connexion);

                cmd.ExecuteReader();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<string> get_transpo_abs()
        {
            SqlConnection connexion = null;
            SqlDataReader reader = null;

            try
            {
                connexion = new SqlConnection(connexionString);
                List<string> list_transpo = new List<string>();

                connexion.Open();
                string requete = "select tag,numero_dossard,nom,prenom,classe from table_inscrit,table_transpondeur,table_eleve,table_classe WHERE table_eleve.idclasse=table_classe.id_classe AND table_inscrit.ideleve = table_eleve.id_eleve AND table_inscrit.idtranspondeur=table_transpondeur.id_transpondeur AND rendu != 1";

                SqlCommand cmd = new SqlCommand(requete, connexion);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string transpo = "TAG : " + reader["tag"].ToString() + "N°" + reader["numero_dossard"].ToString() + " - " + reader["nom"].ToString() + " " + reader["prenom"].ToString() + " - " + reader["classe"].ToString();
                    list_transpo.Add(transpo);
                }


                return list_transpo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connexion != null)
                {
                    connexion.Close();
                }
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }
    }
}
