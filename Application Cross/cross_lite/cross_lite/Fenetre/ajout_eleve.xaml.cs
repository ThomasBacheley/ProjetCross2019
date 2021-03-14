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
            calendar_naissance.DisplayDate = DateTime.Now.AddYears(-14);
        }

        private void Btn_enregistrer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Sexe genre = Sexe.M;
                if (radiobtn_f.IsChecked == true)
                {
                    genre = Sexe.F;
                }

                Élève élève = new Élève(txtbox_nom.Text, txtbox_prenom.Text, genre, Convert.ToDateTime(calendar_naissance.SelectedDate), (Unité)cb_up.SelectedItem, txtbox_classe.Text);

                SQL sql = new SQL("localhost", "Chronocross", "Cross", "Password1234");

                sql.Enregistrement(élève);

                MessageBox.Show(élève.nom + " " + élève.prenom + " à bien était ajouté a la BDD");
                txtbox_nom.Text = "";
                txtbox_prenom.Text = "";
                txtbox_classe.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        private void Sld_prof_el_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(sld_prof_el.Value == 1)
            {
                grid_el.Visibility = Visibility.Hidden;
                grid_personnel.Visibility = Visibility.Visible;
            }
            else
            {
                grid_personnel.Visibility = Visibility.Hidden;
                grid_el.Visibility = Visibility.Visible;
            }
        }

        private void Btn_add_perso_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Sexe genre = Sexe.M;
                if (rb_f.IsChecked == true)
                { 
                    genre = Sexe.F;
                }

                DateTime date = DateTime.Now.AddYears(-42);

                Élève élève = new Élève(txtb_nom_perso.Text, txtb_prenom_perso.Text, genre, date , Unité.Personnel, "Personnel");

                SQL sql = new SQL("localhost", "Chronocross", "Cross", "Password1234");

                sql.Enregistrement(élève);

                MessageBox.Show(txtb_nom_perso.Text.ToUpper() + " " + txtb_prenom_perso.Text + " à bien était ajouter a la BDD");
                txtb_nom_perso.Text = "";
                txtb_prenom_perso.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
