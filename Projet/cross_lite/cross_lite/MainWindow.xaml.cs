using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClassSQL;

namespace cross_lite
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Classe> list_c = null;
        public List<Élève> list_e = null;
        public List<Inscrit> l_ins = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Btn_add_eleve_Click(object sender, RoutedEventArgs e)
        {
            cacher_grid();
            ajout_eleve dlg = new ajout_eleve();
            dlg.ShowDialog();
        }

        private void btn_voir_bdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //-------------
                cacher_grid();
                grid_voir_eleve.Visibility = Visibility.Visible;
                //-------------
                SQL sql = new SQL("localhost", "Chronocross", "Cross", "Password1234");
                list_e = sql.GetÉleves();
                lb_voir_eleve.ItemsSource = list_e;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //private void btn_voir_classe_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        //-------------
        //        cacher_grid();
        //        grid_voir_classe.Visibility = Visibility.Visible;
        //        //-------------
        //        SQL sql = new SQL("localhost", "Chronocross", "Cross", "Password1234");

        //        list_c = sql.GetClasses();
        //        lb_voir_classe.ItemsSource = list_c;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}


        private void btn_voir_classe_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //-------------
                cacher_grid();
                grid_voir_classe.Visibility = Visibility.Visible;
                //-------------
                lb_voir_classe.ItemsSource = getclasse();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Btn_eleve_cl_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //-------------
                cacher_grid();
                grid_eleve_par_classe.Visibility = Visibility.Visible;
                //-------------
                list_c = getclasse();
                cb_classe.ItemsSource = list_c;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cacher_grid()
        {
            grid_voir_eleve.Visibility = Visibility.Hidden;
            grid_voir_classe.Visibility = Visibility.Hidden;
            grid_eleve_par_classe.Visibility = Visibility.Hidden;
            grid_voir_inscrit.Visibility = Visibility.Hidden;
            grid_update_temps.Visibility = Visibility.Hidden;
            grid_voir_classement.Visibility = Visibility.Hidden;
            grid_rendre_badge.Visibility = Visibility.Hidden;
        }

        private void btn_inscrire_eleve_Click(object sender, RoutedEventArgs e)
        {
            cacher_grid();
            fen_inscription dlg = new fen_inscription();
            dlg.ShowDialog();
        }

        private List<Classe> getclasse()
        {
            try
            {
                SQL sql = new SQL("localhost", "Chronocross", "Cross", "Password1234");
                return sql.GetClasses();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void Cb_classe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                SQL sql = new SQL("localhost", "Chronocross", "Cross", "Password1234");
                lb_eleve_classe.ItemsSource = sql.GetÉlevesClasse(list_c[cb_classe.SelectedIndex].id);
            }
            catch (Exception ex)
            {
                MessageBox.Show (ex.Message);
            }
        }

        private void Btn_voir_inscrit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //-------------
                cacher_grid();
                grid_voir_inscrit.Visibility = Visibility.Visible;
                //-------------
                SQL sql = new SQL("localhost", "Chronocross", "Cross", "Password1234");
                l_ins = sql.getinscrit();
                foreach (Inscrit i in l_ins)
                {
                    lb_voir_inscrits.Items.Add(" [ N°" + i.num_d + " ] - " + i.nom + " " + i.prenom + " en " + i.classe);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Btn_temps_up_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtbox_tag_temp.Text == "")
                {
                    throw new Exception("Champ TAG vide");
                }

                SQL sql = new SQL("localhost", "Chronocross", "Cross", "Password1234");
                DateTime currentTime = DateTime.Now;
                DateTime new_t = currentTime.AddHours(slider_hour.Value).AddMinutes(slider_min.Value).AddSeconds(slider_seconde.Value);

                TimeSpan diff = new_t.Subtract(currentTime);
                
                sql.update_t(diff, txtbox_tag_temp.Text);

                MessageBox.Show("update effectué");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Btn_update_temps_Click(object sender, RoutedEventArgs e)
        {
            cacher_grid();
            grid_update_temps.Visibility = Visibility.Visible;
        }

        private void Lb_updtate_temps_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                SQL sql = new SQL("localhost", "Chronocross", "Cross", "Password1234");

                lb_updtate_temps.ItemsSource = sql.GetÉleves();
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Btn_vide_bdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SQL sql = new SQL("localhost", "Chronocross", "Cross", "Password1234");
                sql.clear_bdd();
                MessageBox.Show("La bdd a été nettoyer");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Btn_voir_cl_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cacher_grid();
                grid_voir_classement.Visibility = Visibility.Visible;
                SQL sql = new SQL("localhost", "Chronocross", "Cross", "Password1234");
                txtxbox_1_F.Text = sql.get1ereF().ToString();
                txtxbox_1_G.Text = sql.get1erM().ToString();
                txtxbox_1_P.Text = sql.get1erPersonnel().ToString();
                txtxbox_1_Cadet.Text = sql.get1erCadet().ToString();
                txtxbox_1_Junior.Text = sql.get1erJunior().ToString();
                txtxbox_1_Minime.Text = sql.get1erMinime().ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Btn_rendre_badge_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cacher_grid();
                grid_rendre_badge.Visibility = Visibility.Visible;
                SQL sql = new SQL("localhost", "Chronocross", "Cross", "Password1234");
                lb_rendre_badge.ItemsSource = sql.get_transpo_abs();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Btn_rendre_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SQL sql = new SQL("localhost", "Chronocross", "Cross", "Password1234");

                sql.upd_rendu(txtbox_rendre_tag.Text);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Lb_rendre_badge_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                txtbox_rendre_tag.Text = lb_rendre_badge.SelectedItem.ToString().Substring(6, 10);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }    
}
