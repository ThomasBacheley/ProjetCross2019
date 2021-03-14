
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

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

        public void Enregistrement(Élève eleve)
        {
            SqlConnection connexion = null;

            try
            {
                connexion = new SqlConnection(connexionString);

                SqlCommand cmd = new SqlCommand("enregistrement", connexion);
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
                        throw new Exception("l'élève existe déjà");
                    case 4:
                        throw new Exception("l'élève n'a pas été ajouté");
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

                if(reader!=null)
                {
                    if(reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Classe cl = new Classe(Convert.ToInt32(reader["id_classe"]), reader["classe"].ToString(), reader["unite_pedagogique"].ToString());
                            classes.Add(cl);
                        }
                    }
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

        public List<Inscrit> GetInscrit()
        {
            SqlConnection connexion = null;
            SqlDataReader reader = null;

            try
            {
                connexion = new SqlConnection(connexionString);
                List<Inscrit> inscrits = new List<Inscrit>();

                connexion.Open();
                string requete = "SELECT id_inscrit,idtranspondeur,ideleve,table_eleve.nom,table_eleve.prenom,numero_dossard,classe,tag FROM table_inscrit,table_eleve,table_classe,table_transpondeur WHERE table_transpondeur.id_transpondeur=table_inscrit.idtranspondeur AND table_inscrit.ideleve=table_eleve.id_eleve AND table_eleve.idclasse=table_classe.id_classe AND temps IS NULL ORDER BY classe ASC,table_eleve.nom ASC,table_eleve.prenom ASC";

                SqlCommand cmd = new SqlCommand(requete, connexion);

                reader = cmd.ExecuteReader();

                if(reader!=null)
                {
                    if(reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Inscrit ins = new Inscrit(Convert.ToInt32(reader["ideleve"]), Convert.ToInt32(reader["idtranspondeur"]), Convert.ToInt32(reader["numero_dossard"]));
                            ins.nom = reader["nom"].ToString();
                            ins.prenom = reader["prenom"].ToString();
                            ins.classe = reader["classe"].ToString();
                            ins.tag = reader["tag"].ToString();

                            inscrits.Add(ins);
                        }
                    }
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

        public List<Élève> GetÉlevesClasse(int idclasse)
        {
            SqlConnection connexion = null;
            SqlDataReader reader = null;

            try
            {
                connexion = new SqlConnection(connexionString);
                List<Élève> eleve = new List<Élève>();

                connexion.Open();
                string requete = " SELECT * FROM table_eleve,table_classe WHERE idclasse=" + idclasse + "AND table_eleve.idclasse=table_classe.id_classe ORDER BY nom ASC,prenom ASC";
                SqlCommand cmd = new SqlCommand(requete, connexion);

                reader = cmd.ExecuteReader();
                        if(reader!=null)
                        {
                            if(reader.HasRows)
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

                                    Élève el = new Élève(reader["nom"].ToString(), reader["prenom"].ToString(), s, Convert.ToDateTime(reader["date_naissance"]), up,
                                        reader["classe"].ToString());
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

        public void Inscrire(int ideleve, uint num_dos, string tag)
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

                id_el.Value = ideleve;
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
                        throw new Exception("l'élève n'existe pas");
                    case 2:
                        throw new Exception("l'élève est déjà inscrit");
                    case 3:
                        throw new Exception("le badge na pas été ajouté");
                    case 4:
                        throw new Exception("le transpondeur est déjà associé à un élève");
                    case 5:
                        throw new Exception("le numéro de dossard est déjà pris");
                    case 6:
                        throw new Exception("l'élève n'a pas été ajouté");
                    default:
                        throw new Exception("erreur SQL inconnue : l'inscription n'a pas abouti");
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

        public void Update_Temps(TimeSpan temps_calc,string tag,uint rang)
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

                SqlParameter pts = cmd.Parameters.Add("@pts", SqlDbType.Int);
                TAG.Direction = ParameterDirection.Input;

                temps.Value = temps_calc;
                TAG.Value = tag;
                pts.Value = rang;

                connexion.Open();
                cmd.ExecuteNonQuery();

                int ret = Convert.ToInt32(RetVal.Value);

                switch (ret)
                {
                    case 0:
                        return;
                    case 1:
                        throw new Exception("le badge n'est pas répertorié");
                    case 2:
                        throw new Exception("l'élève n'a pas était retrouvé");
                    case 3:
                        throw new Exception("l'élève possède déjà un temps");
                    case 4:
                        throw new Exception("le temps n'a pas était mis à jour");
                    default:
                        throw new Exception("erreur SQL inconnue : le temps n'a pas pu être mis à jour");
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

        public void Clear_bdd()
        {
            SqlConnection connexion = null;
            try
            {
                connexion = new SqlConnection(connexionString);

                SqlCommand cmd = new SqlCommand("clear_table", connexion);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter RetVal = cmd.Parameters.Add("RetVal", SqlDbType.Int);
                RetVal.Direction = ParameterDirection.ReturnValue;

                connexion.Open();

                cmd.ExecuteNonQuery();
                int ret = Convert.ToInt32(RetVal.Value);

                switch (ret)
                {
                    case 0:
                        //RAS
                        return;
                    case 1:
                        throw new Exception("une erreur est survenue");
                    default:
                        throw new Exception("erreur SQL inconnue");
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

        public List<Inscrit> GetClassement(int nbr_element, Caté caté, Sexe sexe)
        {
            SqlConnection connexion = null;
            SqlDataReader reader = null;

            try
            {
                List<Inscrit> l_cl = new List<Inscrit>();

                connexion = new SqlConnection(connexionString);

                SqlCommand cmd = new SqlCommand("get_cl", connexion);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter NBR = cmd.Parameters.Add("@nbr", SqlDbType.Int);
                NBR.Direction = ParameterDirection.Input;

                SqlParameter G = cmd.Parameters.Add("@g", SqlDbType.VarChar);
                G.Direction = ParameterDirection.Input;

                SqlParameter CAT = cmd.Parameters.Add("@cat", SqlDbType.VarChar);
                CAT.Direction = ParameterDirection.Input;

                NBR.Value = nbr_element;
                G.Value = sexe;
                CAT.Value = caté;

                //-----
                SqlParameter RetVal = cmd.Parameters.Add("RetVal", SqlDbType.Int);
                RetVal.Direction = ParameterDirection.ReturnValue;

                connexion.Open();
                reader = cmd.ExecuteReader();

                int ret = Convert.ToInt32(RetVal.Value);

                if (ret == 1)
                {
                    throw new Exception("Ces critères ne possède pas autant d'élèves");
                }

                if(reader!=null)
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Inscrit inscrit = new Inscrit(Convert.ToInt32(reader["ideleve"]), Convert.ToInt32(reader["idtranspondeur"]), Convert.ToInt32(reader["numero_dossard"]));
                            inscrit.nom = reader["nom"].ToString();
                            inscrit.prenom = reader["prenom"].ToString();
                            inscrit.classe = reader["classe"].ToString();
                            inscrit.temps = (TimeSpan)reader["temps"];
                            l_cl.Add(inscrit);

                        }
                    }
                }
                else
                {
                    throw new Exception("Pas de classement pour ces choix");
                }
                return l_cl;
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

        public void Update_Badge(string tag)
        {
            SqlConnection connexion = null;
            try
            {
                connexion = new SqlConnection(connexionString);

                connexion.Open();
                string requete = "update table_transpondeur set rendu = 1 WHERE tag = '" + tag.ToString() +"'";

                SqlCommand cmd = new SqlCommand(requete, connexion);

                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if(connexion!=null)
                {
                    connexion.Close();
                }
            }
        }

        public List<Élève> GetTranspondeurAbsent()
        {
            SqlConnection connexion = null;
            SqlDataReader reader = null;

            try
            {
                connexion = new SqlConnection(connexionString);
                List<Élève> list_transpo = new List<Élève>();

                connexion.Open();
                string requete = "SELECT nom,prenom,genre,date_naissance,classe,unite_pedagogique from table_eleve,table_classe,table_inscrit,table_transpondeur WHERE table_transpondeur.id_transpondeur=table_inscrit.idtranspondeur AND table_inscrit.ideleve=table_eleve.id_eleve AND table_eleve.idclasse=table_classe.id_classe AND rendu=0 ORDER by classe ASC,nom ASC ";

                SqlCommand cmd = new SqlCommand(requete, connexion);

                reader = cmd.ExecuteReader();

                if(reader!=null)
                {
                    if(reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Sexe s = Sexe.M;
                            if(reader["genre"].ToString() == "F")
                            {
                                s = Sexe.F;
                            }

                            Unité up = Unité.LA;
                            switch(reader["unite_pedagogique"].ToString().ToUpper())
                            {
                                case "LGT":up = Unité.LGT;break;
                                case "LP":up = Unité.LP;break;
                                case "BTS":up = Unité.BTS;break;
                                case "UFA":up = Unité.UFA;break;
                                case "PERSONNEL":up = Unité.Personnel;break;
                            }
                            Élève el = new Élève(reader["nom"].ToString(), reader["prenom"].ToString(), s, (DateTime)reader["date_naissance"], up, reader["classe"].ToString());
                            list_transpo.Add(el);
                        }
                    }
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

        public Inscrit getInscrit_viaTag(string tag)
        {
            SqlConnection connexion = null;
            SqlDataReader reader = null;
            Inscrit inscrit = null;

            try
            {
                connexion = new SqlConnection(connexionString);

                connexion.Open();
                string requete = "SELECT rang,ideleve,idtranspondeur,numero_dossard,temps,nom,prenom,genre,classe from table_inscrit,table_transpondeur,table_eleve,table_classe WHERE table_transpondeur.id_transpondeur=table_inscrit.id_inscrit AND table_inscrit.ideleve=table_eleve.id_eleve AND table_eleve.idclasse=table_classe.id_classe AND tag='"+tag+"'";

                SqlCommand cmd = new SqlCommand(requete, connexion);

                reader = cmd.ExecuteReader();

                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Sexe g = Sexe.F;
                            if(reader["genre"].ToString() == "M") { g = Sexe.M; }
                            inscrit = new Inscrit(Convert.ToInt32(reader["ideleve"]), Convert.ToInt32(reader["idtranspondeur"]), Convert.ToInt32(reader["numero_dossard"]));
                            inscrit.temps = (TimeSpan)reader["temps"];
                            inscrit.nom = reader["nom"].ToString();
                            inscrit.prenom = reader["prenom"].ToString();
                            inscrit.genre = g;
                            inscrit.classe = reader["classe"].ToString();
                            inscrit.Rang = Convert.ToInt32(reader["rang"]);
                        }
                    }
                }
                return inscrit;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if(connexion !=null)
                {
                    connexion.Close();
                }
                if(reader!=null)
                {
                    reader.Close();
                }
            }
        }

        public List<Équipe> GetClassementÉquipe()
        {
            try
            {
                SqlConnection connexion = null;
                SqlDataReader reader = null;
                uint place = 0;
                try
                {
                    List<Équipe> liste_équipe = new List<Équipe>();

                    connexion = new SqlConnection(connexionString);

                    SqlCommand cmd = new SqlCommand("Classement_Equipe", connexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //-----
                    SqlParameter RetVal = cmd.Parameters.Add("RetVal", SqlDbType.Int);
                    RetVal.Direction = ParameterDirection.ReturnValue;

                    connexion.Open();
                    reader = cmd.ExecuteReader();

                    int ret = Convert.ToInt32(RetVal.Value);

                    //test à faire sur le reader

                    if (reader != null)
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                place++;
                                Équipe é = new Équipe(Convert.ToUInt32(reader["total"]), reader["classe"].ToString(), place, reader["nom_el1"].ToString(), reader["nom_el2"].ToString(), reader["nom_el3"].ToString(), reader["nom_el4"].ToString(), Convert.ToUInt32(reader["Rang1"]), Convert.ToUInt32(reader["Rang2"]), Convert.ToUInt32(reader["Rang3"]), Convert.ToUInt32(reader["Rang4"]));
                                liste_équipe.Add(é);

                            }
                        }
                    }
                    return liste_équipe;
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
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}
 