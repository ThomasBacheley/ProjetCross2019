using System;
using System.Collections.Generic;
using System.Windows;
using ClassSQL;
using Microsoft.Win32;
using Microsoft.Office.Interop.Excel;

namespace cross_lite
{
    /// <summary>
    /// Logique d'interaction pour ajout_eleve.xaml
    /// </summary>
    public partial class ajout_eleve : System.Windows.Window
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

        private void btn_lecture_excel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Excel File (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == true)
                {

                    Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                    Workbook xlWorkbook = xlApp.Workbooks.Open(openFileDialog.FileName);
                    _Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                    Range xlRange = xlWorksheet.UsedRange;

                    MessageBox.Show("Lignes:\n"+xlRange.Rows.Count.ToString()+"\nColonnes:\n"+ xlRange.Columns.Count.ToString()+"\n[5][1]:\n"+ xlRange.Cells[5, 1].Value2.ToString(), "Stats");



                    //Read the contents of the file into a stream
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
