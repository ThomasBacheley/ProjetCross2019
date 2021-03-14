using System;
using System.Collections.Generic;
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
    /// Logique d'interaction pour fen_inscription.xaml
    /// </summary>
    public partial class fen_inscription : Window
    {

        public List<Classe> list_c = null;
        public List<Élève> list_e = null;

        public fen_inscription()
        {
            InitializeComponent();
        }

        private void Btn_retour_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Cb_classe_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                SQL sql = new SQL("localhost", "Chronocross", "Cross", "Password1234");

                list_c = sql.GetClasses();
                cb_classe.ItemsSource = list_c;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Cb_classe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SQL sql = new SQL("localhost", "Chronocross", "Cross", "Password1234");
            lb_eleve.ItemsSource = sql.GetÉlevesClasse(list_c[cb_classe.SelectedIndex].id);
        }

        private void Btn_inscrire_Click(object sender, RoutedEventArgs e)
        { 
            try
            { 
                if(lb_eleve.SelectedItem == null)
                {
                    throw new Exception("pas d'Élève sélectionner");
                }
                else
                {
                    if(txtbox_dossard.Text=="")
                    {
                        throw new Exception("Le numéro de dossard n'a pas été renseigné");
                    }
                    else
                    {
                        if (txtbox_tag.Text == "")
                        {
                            throw new Exception("le Tag du badge n'a pas été renseingné / le badge na pas été lu");
                        }
                        else
                        {
                            Élève Select_el = (Élève)lb_eleve.SelectedItem;
                            SQL sql = new SQL("localhost", "Chronocross", "Cross", "Password1234");
                            sql.Inscrire(Select_el.id, Convert.ToInt32(txtbox_dossard.Text), txtbox_tag.Text);
                            MessageBox.Show(Select_el.nom + " " + Select_el.prenom + " est maintenant inscrit au Cross");
                        }
                    }
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
