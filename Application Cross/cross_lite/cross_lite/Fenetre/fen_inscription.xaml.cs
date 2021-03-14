using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
                    throw new Exception("pas d'Élève sélectionné");
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
                            throw new Exception("le Tag du badge n'a pas été renseingné / le badge n'a pas été lu");
                        }
                        else
                        {
                            Élève Select_el = (Élève)lb_eleve.SelectedItem;
                            SQL sql = new SQL("localhost", "Chronocross", "Cross", "Password1234");
                            sql.Inscrire(Select_el.id, Convert.ToUInt32(txtbox_dossard.Text), txtbox_tag.Text);
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

        private void Txtbox_dossard_MouseEnter(object sender, MouseEventArgs e)
        {
            Random random = new Random();
            int randomNumber;
            randomNumber = random.Next(0, 99999);
            txtbox_dossard.Text = randomNumber.ToString();
        }

        private void Txtbox_tag_MouseEnter(object sender, MouseEventArgs e)
        {
            string caracteres = "azertyuiopqsdfghjklmwxcvbn0123456789";
            Random selAlea = new Random();


            string sel = "";
            for (int i = 0; i < 10; i++) // 10 caracteres
            {
                int majOrMin = selAlea.Next(2);
                string carac = caracteres[selAlea.Next(0, caracteres.Length)].ToString();
                sel += carac.ToUpper(); // Maj

            }
            txtbox_tag.Text = sel;
        }

        private void Lb_eleve_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Partie Tag
            string caracteres = "azertyuiopqsdfghjklmwxcvbn0123456789";
            Random selAlea = new Random();
            string sel = "";
            for (int i = 0; i < 10; i++) // 10 caracteres
            {
                int majOrMin = selAlea.Next(2);
                string carac = caracteres[selAlea.Next(0, caracteres.Length)].ToString();
                sel += carac.ToUpper(); // Maj

            }
            txtbox_tag.Text = sel;

            // Partie Dossard
            Random random = new Random();
            int randomNumber;
            randomNumber = random.Next(0, 99999);
            txtbox_dossard.Text = randomNumber.ToString();

        }

        private void Lb_eleve_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Élève Select_el = (Élève)lb_eleve.SelectedItem;
                SQL sql = new SQL("localhost", "Chronocross", "Cross", "Password1234");
                sql.Inscrire(Select_el.id, Convert.ToUInt32(txtbox_dossard.Text), txtbox_tag.Text);
                MessageBox.Show(Select_el.nom + " " + Select_el.prenom + " est maintenant inscrit au Cross");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
