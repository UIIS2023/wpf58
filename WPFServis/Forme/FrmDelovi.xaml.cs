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
    /// Interaction logic for FrmDelovi.xaml
    /// </summary>
    public partial class FrmDelovi : Window
    {

        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        bool azuriraj;
        DataRowView pomocniRed;
        public FrmDelovi()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuciMeni();
            txtNaziv.Focus();
        }
        public FrmDelovi(bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuciMeni();
            txtNaziv.Focus();
            this.azuriraj = azuriraj;
            this.pomocniRed = pomocniRed;
        }
        private void PopuniPadajuciMeni()
        {
            try
            {
                konekcija.Open();
                string vratiServis = @"select servisID, datumServisa from Servis";
                DataTable dtServis = new DataTable();
                SqlDataAdapter daServis = new SqlDataAdapter(vratiServis, konekcija);
                daServis.Fill(dtServis);
                cbServis.ItemsSource = dtServis.DefaultView;
                cbServis.DisplayMemberPath = "datumServisa";
                daServis.Dispose();
                dtServis.Dispose();
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
                cmd.Parameters.Add("@deoNaziv", System.Data.SqlDbType.NVarChar).Value = txtNaziv.Text;
                cmd.Parameters.Add("@deoCena", System.Data.SqlDbType.Int).Value = txtCena.Text;
                cmd.Parameters.Add("@servisID", System.Data.SqlDbType.Int).Value = cbServis.SelectedValue;
                if(this.azuriraj)
                {
                    DataRowView red = this.pomocniRed;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"Update Delovi
                                        Set deoNaziv = @deoNaziv, deoCena = @deoCena, servisID = @servisID
                                        where deoID = @id";
                    this.pomocniRed = null;
                }    
                else
                {
                    cmd.CommandText = @"insert into Delovi(deoNaziv, deoCena, servisID)
                                        values(@deoNaziv,@deoCena,@servisID)";
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
