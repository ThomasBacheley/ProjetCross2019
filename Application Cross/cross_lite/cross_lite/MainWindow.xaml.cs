using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using ClassSQL;

namespace cross_lite
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //------------------------------------------------

        public List<Classe> list_c = null;
        public List<Élève> list_e = null;
        List<Inscrit> l_up_t = new List<Inscrit>();
        public int i = 0;

        DateTime currenttime;
        DispatcherTimer timer = new DispatcherTimer();
        TimeSpan Temps = new TimeSpan();

        //------------------------------------------------

        public MainWindow()
        {
            try
            {
                InitializeComponent();
                timer.Interval = TimeSpan.FromMilliseconds(0);
                timer.Tick += timerTicker;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void timerTicker(object sender, EventArgs e)
        {
            try
            {
                label_temps.Content = (DateTime.Now - currenttime).ToString("hh\\:mm\\:ss\\.fff");
                Temps = (DateTime.Now - currenttime);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //------------------------------------------------

        private void Btn_add_eleve_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cacher_grid();
                ajout_eleve dlg = new ajout_eleve();
                dlg.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_inscrire_eleve_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cacher_grid();
                fen_inscription dlg = new fen_inscription();
                dlg.ShowDialog();
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
                fen_classement dlg = new fen_classement();
                dlg.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //------------------------------------------------

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        //------------------------------------------------

        private void cacher_grid()
        {
            grid_update_temps.Visibility = Visibility.Hidden;
            grid_rendre_badge.Visibility = Visibility.Hidden;
        }

        //----------------------------------------------------

        private void Btn_update_temps_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cacher_grid();
                grid_update_temps.Visibility = Visibility.Visible;
                txtb_rang.Text = Properties.Settings.Default.points_cl_rang.ToString();
                SQL sql = new SQL(Properties.Settings.Default.serveur, Properties.Settings.Default.bdd, Properties.Settings.Default.user, Properties.Settings.Default.psw);

                l_up_t = sql.GetInscrit();

                foreach(Inscrit i in l_up_t)
                {
                    lb_updtate_temps.Items.Add("TAG → " + i.tag);
                }

                currenttime = DateTime.Now;
                timer.Start();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Btn_vide_bdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                MessageBoxResult reponse = MessageBox.Show("Souhaitez vous supprimer le contenu de la base ?", "Supprimer",
                    MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);

                if (reponse == MessageBoxResult.Yes)
                {
                    cacher_grid();
                    SQL sql = new SQL(Properties.Settings.Default.serveur, Properties.Settings.Default.bdd, Properties.Settings.Default.user, Properties.Settings.Default.psw);
                    sql.Clear_bdd();
                    MessageBox.Show("La bdd a été nettoyer");
                    Properties.Settings.Default.points_cl_rang = 1;
                }
                else
                {
                    MessageBox.Show("Demande annulée");
                }

                
            }
            catch (Exception ex)
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
                SQL sql = new SQL(Properties.Settings.Default.serveur, Properties.Settings.Default.bdd, Properties.Settings.Default.user, Properties.Settings.Default.psw);
                List<Élève> transpo_abs = new List<Élève>();
                transpo_abs = sql.GetTranspondeurAbsent();
                lb_rendre_badge.ItemsSource = transpo_abs;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Lb_updtate_temps_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                string tag = lb_updtate_temps.SelectedItem.ToString().Substring(6,10 );
                txtbox_tag_temp.Text = tag;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Grid_update_temps_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (txtbox_tag_temp.Text != "" || lb_updtate_temps.SelectedIndex != -1)
                {
                    if (e.Key == Key.Enter)
                    {
                        ListBoxItem list_item;
                        if (txtbox_tag_temp.Text == "")
                        {
                            throw new Exception("Champ TAG vide");
                        }

                        SQL sql = new SQL(Properties.Settings.Default.serveur, Properties.Settings.Default.bdd, Properties.Settings.Default.user, Properties.Settings.Default.psw);

                        sql.Update_Temps(Temps, txtbox_tag_temp.Text, Convert.ToUInt32(txtb_rang.Text));
                        Properties.Settings.Default.points_cl_rang++;
                        txtb_rang.Text = Properties.Settings.Default.points_cl_rang.ToString();

                        //-------------------------------------------------------------------------
                        Inscrit ins = sql.getInscrit_viaTag(txtbox_tag_temp.Text);
                        //-------------------------------------------------------------------------
                        string content = "[" + ins.Rang.ToString() + "] " + ins.nom.ToString() + " " + ins.prenom.ToString() + " " + ins.classe.ToString() + " " + ins.temps;

                        if (ins.genre == Sexe.F)
                        {
                            list_item = new ListBoxItem() { Foreground = Brushes.DeepPink, Content = content };
                        }
                        else
                        {
                            list_item = new ListBoxItem() { Foreground = Brushes.Blue, Content = content };
                        }

                        lb_temps_inscrit.Items.Add(list_item);

                        lb_temps_inscrit.Items.Refresh();
                        if (lb_temps_inscrit.Items.Count > 0)
                        {
                            var border = VisualTreeHelper.GetChild(lb_temps_inscrit, 0) as Decorator;
                            if (border != null)
                            {
                                var scroll = border.Child as ScrollViewer;
                                if (scroll != null)
                                    scroll.ScrollToEnd();
                            }
                        }
                        txtbox_tag_temp.Text = "";
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Txtbox_rendre_tag_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if(txtbox_rendre_tag.Text != "" && e.Key == Key.Enter)
                {
                    SQL sql = new SQL(Properties.Settings.Default.serveur, Properties.Settings.Default.bdd, Properties.Settings.Default.user, Properties.Settings.Default.psw);
                    sql.Update_Badge(txtbox_rendre_tag.Text);
                    lb_rendre_badge.ItemsSource = sql.GetTranspondeurAbsent();
                    MessageBox.Show("Le badge " + txtbox_rendre_tag.Text + " est maintenant rendu");
                    txtbox_rendre_tag.Text = "";
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }    
}
