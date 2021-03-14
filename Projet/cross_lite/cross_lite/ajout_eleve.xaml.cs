using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ClassSQL;

namespace cross_lite
{
    /// <summary>
    /// Logique d'interaction pour ajout_eleve.xaml
    /// </summary>
    public partial class ajout_eleve : Window
    {
        public ajout_eleve()
        {
            InitializeComponent();
        }

        private void Btn_enregistrer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(txtbox_nom.Text == "")
                {
                    throw new Exception("Champ nom vide");
                }
                else
                {
                    if(txtbox_prenom.Text =="")
                    {
                        throw new Exception("Champ prenom vide");
                    }
                    else
                    {
                        if(txtbox_classe.Text=="")
                        {
                            throw new Exception("Champ Classe vide");
                        }
                        else
                        {
                            Sexe genre = Sexe.M;
                            if (radiobtn_f.IsChecked == true)
                            {
                                genre = Sexe.F;
                            }

                            Élève élève = new Élève(txtbox_nom.Text, txtbox_prenom.Text, genre, Convert.ToDateTime(calendar_naissance.SelectedDate), (Unité)cb_up.SelectedItem, txtbox_classe.Text);

                            SQL sql = new SQL("localhost", "Chronocross", "Cross", "Password1234");

                            sql.add_e(élève);

                            MessageBox.Show(élève.nom + " " + élève.prenom + " à bien était ajouter a la BDD");
                            txtbox_nom.Text = "";
                            txtbox_prenom.Text = "";
                            txtbox_classe.Text = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
            }
        }

        private void Btn_retour_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Cb_up_Loaded(object sender, RoutedEventArgs e)
        {
            List<Unité> u = new List<Unité>();
            u.Add(Unité.BTS);
            u.Add(Unité.LA);
            u.Add(Unité.LGT);
            u.Add(Unité.LP);
            u.Add(Unité.Personnel);
            u.Add(Unité.UFA);
            cb_up.ItemsSource = u;
        }
    }
}
