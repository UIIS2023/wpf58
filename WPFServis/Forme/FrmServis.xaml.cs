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
using System.Data.SqlClient;
using System.Data;

namespace WPFServis.Forme
{
    /// <summary>
    /// Interaction logic for FrmServis.xaml
    /// </summary>
    public partial class FrmServis : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        bool azuriraj;
        DataRowView pomocniRed;
        public FrmServis()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            txtDatum.Focus();
            PopuniPadajuceListe();
        }
        public FrmServis(bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            txtDatum.Focus();
            PopuniPadajuceListe();
            this.azuriraj = azuriraj;
            this.pomocniRed = pomocniRed;
        }
        private void PopuniPadajuceListe()
        {
            try
            {
                konekcija.Open();
                string vratiZaposleni = @"select zaposleniID, zaposleniIme from Zaposleni";
                DataTable dtZaposleni = new DataTable();
                SqlDataAdapter daZaposleni = new SqlDataAdapter(vratiZaposleni, konekcija);
                daZaposleni.Fill(dtZaposleni);
                cbZaposleni.ItemsSource = dtZaposleni.DefaultView;
                cbZaposleni.DisplayMemberPath = "zaposleniIme";
                daZaposleni.Dispose();
                dtZaposleni.Dispose();
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
                cmd.Parameters.Add("@datumServisa", System.Data.SqlDbType.NVarChar).Value = txtDatum.Text;
                cmd.Parameters.Add("@cenaServisa", System.Data.SqlDbType.Int).Value = txtCena.Text;
                cmd.Parameters.Add("@zaposleniID", System.Data.SqlDbType.Int).Value = cbZaposleni.SelectedValue;
                if (this.azuriraj)
                {
                    DataRowView red = this.pomocniRed;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"Update Servis
                                        Set datumServisa = @datumServisa, cenaServisa = @cenaServisa, zaposleniID = @zaposleniID
                                        where servisID = @id";
                    this.pomocniRed = null;
                }
                else
                {
                    cmd.CommandText = @"insert into Servis(datumServisa, cenaServisa, zaposleniID)
                                        values(@datumServisa,@cenaServisa,@zaposleniID)";
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
