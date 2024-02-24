using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
    /// Interaction logic for FrmVlasnik.xaml
    /// </summary>
    public partial class FrmVlasnik : Window
    {

        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        bool azuriraj;
        DataRowView pomocniRed;
        public FrmVlasnik()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            txtIme.Focus();
        }
        public FrmVlasnik(bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            txtIme.Focus();
            this.azuriraj = azuriraj;
            this.pomocniRed = pomocniRed;
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
                cmd.Parameters.Add("@vlasnikIme", System.Data.SqlDbType.NVarChar).Value = txtIme.Text;
                cmd.Parameters.Add("@vlasnikPrezime", System.Data.SqlDbType.NVarChar).Value = txtPrezime.Text;
                cmd.Parameters.Add("@vlasnikJMBG", System.Data.SqlDbType.NVarChar).Value = txtJMBG.Text;
                cmd.Parameters.Add("@vlasnikKontakt", System.Data.SqlDbType.NVarChar).Value = txtKontakt.Text;
                cmd.Parameters.Add("@vlasnikAdresa", System.Data.SqlDbType.NVarChar).Value = txtAdresa.Text;
                if (this.azuriraj)
                {
                    DataRowView red = this.pomocniRed;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"Update Vlasnik
                                    Set vlasnikIme = @vlasnikIme, vlasnikPrezime = @vlasnikPrezime, vlasnikJMBG = @vlasnikJMBG, vlasnikKontakt = @vlasnikKontakt, vlasnikAdresa = @vlasnikAdresa
                                    where vlasnikID = @id";
                    this.pomocniRed = null;
                }
                else
                {
                    cmd.CommandText = @"insert into Vlasnik(vlasnikIme, vlasnikPrezime, vlasnikJMBG, vlasnikKontakt, vlasnikAdresa)
                                    values(@vlasnikIme,@vlasnikPrezime,@vlasnikJMBG,@vlasnikKontakt,@vlasnikAdresa)";
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
