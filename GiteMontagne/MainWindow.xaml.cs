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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GiteMontagne
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //this.cbxDuree.Items.Clear();
            for (int i = 1; i < 36; i++)
                this.cbxDuree.Items.Add(i);

        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }

        private void Nom(object sender, RoutedEventArgs e)
        {

        }

        private void Nom(object sender, TextCompositionEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void btnReserver_Click(object sender, RoutedEventArgs e)
        {
            //DAL.EnregistrerClient(DateTime.Parse(dp.SelectedDate.ToString()), (int)cbxDuree.SelectedItem, txtNom.Text, int.Parse(txtTelephone.Text), txtAdresse.Text, int.Parse(txtCP.Text), txtVille.Text, (int)cbxNombrePersonnes.SelectedItem);
            DAL.EnregistrerClient(txtNom.Text, int.Parse(txtTelephone.Text), txtAdresse.Text, int.Parse(txtCP.Text), txtVille.Text, (int)cbxNombrePersonnes.SelectedItem, (DateTime)dp.SelectedDate, (int)cbxDuree.SelectedItem, ((DateTime) dp.SelectedDate).AddDays((int)cbxDuree.SelectedItem));
            //DAL.EnregistrerClient(txtNom.Text, int.Parse(txtTelephone.Text), txtAdresse.Text, int.Parse(txtCP.Text), txtVille.Text);
            MessageBox.Show($" \t Fiche Client \n \n Nom pour la réservation: {txtNom.Text} \n Adresse du client : {txtAdresse.Text} \n C.P. : {txtCP.Text} \n Ville : {txtVille.Text}" +
            $"\n Téléphone du client : {txtTelephone.Text}");

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

            

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void CbxDortoir_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //lstPlaces.Items.Clear();
            //Lit lit = new Lit();
            //Place salle = new Place();
            //lit = cbxDortoir.SelectedItem as Lit;

            //if (lit != null)
            //    if (salle.Lits.Count > 0)
            //        foreach (Lit item in salle.Lits)
            //            lstPlaces.Items.Add(item);

    
            if (cbxDortoir.SelectedItem != null)
                lstPlaces.ItemsSource = DAL.getLits(int.Parse(cbxDortoir.SelectedItem.ToString().Substring(0, 2)));
                

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void LstPlaces_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lstPlaces.SelectedItem != null)
            lstClients.ItemsSource = DAL.retournerClients(((Lit)lstPlaces.SelectedItem).LitId);


            //lstPlaces.ItemsSource  = DAL.afficherPlacesDates(DateTime.Parse(dp.SelectedDate.ToString()), int.Parse(cbxDuree.SelectedItem.ToString()), cbxDortoir.ToString());
        }

        private void CbxDortoir_Loaded(object sender, RoutedEventArgs e)
        {
            
            cbxDortoir.ItemsSource = DAL.getPlaces();
            
        }

        private void DatePicker_CalendarClosed(object sender, RoutedEventArgs e)
        {
            //lstPlaces.ItemsSource = DAL.afficherPlacesDates();
           // lstPlaces.ItemsSource = DAL.afficherPlacesDates(DateTime.Parse(dp.SelectedDate.ToString()));

        }

        private void LstPlaces_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {

        }

        private void btnLancer_Click(object sender, RoutedEventArgs e)
        {
            DateTime DateArrivee = DateTime.Parse(dp.SelectedDate.ToString());
            int duree = (int)cbxDuree.SelectedItem;
            DateTime DateDepart = DateArrivee.AddDays(duree);
            lstPlaces.ItemsSource = DAL.afficherPlacesDates(DateArrivee, duree);
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void LstClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CbxDuree_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { 


        }

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {

            DAL.SupprimerClient(((Client)lstClients.SelectedItem).Id);
            MessageBox.Show($"\t Fiche Client \n\n Le client a bien été supprimé");
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            DAL.UpdateClient(DateTime.Parse(dp.SelectedDate.ToString()), (int)cbxDuree.SelectedItem, ((Lit)lstPlaces.SelectedItem).LitId, txtNom.Text, int.Parse(txtTelephone.Text), txtAdresse.Text, int.Parse(txtCP.Text), txtVille.Text, (int)cbxNombrePersonnes.SelectedItem);
            MessageBox.Show($"\t Fiche Client \n\n Le client: {txtNom.Text} a bien été modifié");
        }

        private void btnEffacer_Click(object sender, RoutedEventArgs e)
        {
            lstPlaces.Items.Clear();
            lstClients.Items.Clear();
            dp.SelectedDate = null;
            dp.DisplayDate = DateTime.Today;
            txtNom.Text = " ";
            txtAdresse.Text = " ";
            txtCP.Text = " ";
            txtVille.Text = " ";
            txtTelephone.Text = " ";
        }

        private void CbxNombrePersonnes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CbxNombrePersonnes_Loaded(object sender, RoutedEventArgs e)
        {
            this.cbxNombrePersonnes.Items.Clear();
            for (int i = 1; i < 15; i++)
                this.cbxNombrePersonnes.Items.Add(i);
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {

        }

        private void btnRemplir_Click(object sender, RoutedEventArgs e)
        {
            //((Client)lstClients.SelectedItem).Id;
            //DAL.RemplirBase(txtNom.Text.ToString(), int.Parse(txtTelephone.Text), txtAdresse.Text.ToString(),
            //int.Parse(txtCP.Text), txtVille.Text.ToString());
        }


    }
}
