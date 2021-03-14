using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using ClassSQL;

namespace cross_lite
{
    /// <summary>
    /// Logique d'interaction pour fen_classement.xaml
    /// </summary>
    public partial class fen_classement : Window
    {
        public fen_classement()
        {
            InitializeComponent();
            cb_categorie.ItemsSource = l_caté;
            cb_genre.ItemsSource = l_genre;
        }

        List<Caté> l_caté = new List<Caté>() {Caté.Seniors_Masters,Caté.Espoirs,Caté.Juniors,Caté.Cadets,Caté.Minimes};
        List<Sexe> l_genre = new List<Sexe>() { Sexe.F,Sexe.M };


        private void Btn_cl_équipe_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(lb_classement.Items.Count != 0) { lb_classement.Items.Clear(); }
                SQL sql = new SQL("localhost", "Chronocross", "Cross", "Password1234");
                lb_classement.ItemsSource = sql.GetClassementÉquipe();
                label_nom_classement.Content = "Équipe";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Txtb_nbr_el_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if(e.Key == Key.Enter)
                { 
                    SQL sql = new SQL("localhost", "Chronocross", "Cross", "Password1234");
                    Sexe genre;
                    Caté cat;

                    genre = l_genre[cb_genre.SelectedIndex];

                    cat = l_caté[cb_categorie.SelectedIndex];
                    List<Inscrit> ins = sql.GetClassement(Convert.ToInt32(txtb_nbr_el.Text), cat, genre);
                    label_nom_classement.Content = genre.ToString() + " " + cat.ToString();
                    if (lb_classement.Items.Count != 0) { lb_classement.Items.Clear(); }
                    foreach (Inscrit i in ins)
                    {
                        lb_classement.Items.Add(i.nom.ToString() + " " + i.prenom.ToString() + " - " + i.classe.ToString() + " → " + i.temps.ToString("hh\\:mm\\:ss\\.fff"));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
