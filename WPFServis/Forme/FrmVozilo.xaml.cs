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
using System.Windows.Shapes;

namespace WPFServis.Forme
{
    /// <summary>
    /// Interaction logic for FrmVozilo.xaml
    /// </summary>
    public partial class FrmVozilo : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        bool azuriraj;
        DataRowView pomocniRed;
        public FrmVozilo()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuceListe();
            txtBrSasije.Focus();
        }
        public FrmVozilo(bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuceListe();
            txtBrSasije.Focus();
            this.azuriraj = azuriraj;
            this.pomocniRed = pomocniRed;
        }
        private void PopuniPadajuceListe()
        {
            try
            {
                konekcija.Open();
                string vratiModel = @"select modelID, modelNaziv from Model";
                DataTable dtModel = new DataTable(); 
                SqlDataAdapter daModel = new SqlDataAdapter(vratiModel, konekcija);
                daModel.Fill(dtModel);
                cbModel.ItemsSource = dtModel.DefaultView;
                cbModel.DisplayMemberPath = "modelNaziv";
                daModel.Dispose();
                dtModel.Dispose();

                string vratiMarka = @"select markaID, markaNaziv from Marka";
                DataTable dtMarka = new DataTable();
                SqlDataAdapter daMarka = new SqlDataAdapter(vratiMarka, konekcija);
                daMarka.Fill(dtMarka);
                cbMarka.ItemsSource = dtMarka.DefaultView;
                cbMarka.DisplayMemberPath = "markaNaziv";
                daMarka.Dispose();
                dtMarka.Dispose();


                string vratiVlasnik = @"select vlasnikID, vlasnikIme from Vlasnik";
                DataTable dtVlasnik = new DataTable();
                SqlDataAdapter daVlasnik = new SqlDataAdapter(vratiVlasnik, konekcija);
                daVlasnik.Fill(dtVlasnik);
                cbVlasnik.ItemsSource = dtVlasnik.DefaultView;
                cbVlasnik.DisplayMemberPath = "vlasnikIme";
                daVlasnik.Dispose();
                dtVlasnik.Dispose();


                string vratiTipVozila = @"select tipVozilaID, nazivVozila from TipVozila";
                DataTable dtTipVozila = new DataTable();
                SqlDataAdapter daTipVozila = new SqlDataAdapter(vratiTipVozila, konekcija);
                daTipVozila.Fill(dtTipVozila);
                cbTipVozila.ItemsSource = dtTipVozila.DefaultView;
                cbTipVozila.DisplayMemberPath = "nazivVozila";
                daTipVozila.Dispose();
                dtTipVozila.Dispose();
            }
            catch (SqlException)
            {
                MessageBox.Show("Padajuce liste nisu popunjene", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }
        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };
                cmd.Parameters.Add(@"brojSasije", System.Data.SqlDbType.NVarChar).Value = txtBrSasije.Text;
                cmd.Parameters.Add(@"kubikaza",System.Data.SqlDbType.Int).Value = txtKubikaza.Text;
                cmd.Parameters.Add(@"konjskaSnaga", System.Data.SqlDbType.Int).Value =txtKonjskaSnaga.Text;
                cmd.Parameters.Add(@"modelID", System.Data.SqlDbType.Int).Value = cbModel.SelectedValue;
                cmd.Parameters.Add(@"markaID", System.Data.SqlDbType.Int).Value = cbMarka.SelectedValue;
                cmd.Parameters.Add(@"vlasnikID", System.Data.SqlDbType.Int).Value = cbVlasnik.SelectedValue;
                cmd.Parameters.Add(@"tipVozilaID", System.Data.SqlDbType.Int).Value = cbTipVozila.SelectedValue;

                if(this.azuriraj)
                {
                    DataRowView red = this.pomocniRed;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"Update Vozilo
                                        Set brojSasije = @brojSasije, kubikaza = @kubikaza, konjskaSnaga = @konjskaSnaga, modelID = @modelID, markaID = @markaID, vlasnikID = @vlasnikID, tipVozilaID = @tipVozilaID
                                        where voziloID = @id";
                    this.pomocniRed = null;
                }
                else
                {
                    cmd.CommandText = @"insert into Vozilo(brojSasije, kubikaza, konjskaSnaga, modelID, markaID, vlasnikID, tipVozilaID)
                                        values(@brojSasije,@kubikaza,@konjskaSnaga,@modelID,@markaID,@vlasnikID,@tipVozilaID)";
                }
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                this.Close();

            }
            catch (SqlException)
            {
                MessageBox.Show("Padajuce liste nisu popunjene", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (FormatException)
            {
                MessageBox.Show("Greska prilikom konverzije podataka", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
                azuriraj = false;
            }
        }
        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}
