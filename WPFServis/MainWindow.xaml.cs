using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
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
using WPFServis.Forme;

namespace WPFServis
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string ucitanaTabela;
        bool azuriraj;
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();

        #region Select Upiti
        private static string zaposleniSelect = @"select zaposleniID as ID, zaposleniIme as Ime, zaposleniPrezime as Prezime, zaposleniJmbg as JMBG, zaposleniKontakt as Kontakt, zaposleniAdresa as Adresa from Zaposleni";
        private static string vlasnikSelect = @"select vlasnikID as ID, vlasnikIme as Ime, vlasnikPrezime as Prezime, vlasnikJMBG as JMBG, vlasnikKontakt as Kontakt, vlasnikAdresa as Adresa from Vlasnik";
        private static string modelSelect = @"select modelID as ID, modelNaziv as 'Ime modela' from Model";
        private static string markaSelect = @"select markaID as ID, markaNaziv as 'Ime marke' from Marka";
        private static string tipVozilaSelect = @"select tipVozilaID as ID, nazivVozila as 'Ime tipa vozila' from TipVozila";
        private static string servisSelect = @"select servisID as ID, datumServisa as 'Datum servisa', cenaServisa as 'Cena servisa', zaposleniIme as Zaposleni
                                              from Servis join Zaposleni on Servis.zaposleniID = Zaposleni.zaposleniID";
        private static string deloviSelect = @"select deoID as ID, deoNaziv as 'Ime deo', deoCena as 'Cena deo', datumServisa as Servis 
                                              from Delovi join Servis on Delovi.servisID = Servis.servisID";
        private static string voziloSelect = @"select voziloID as ID, brojSasije as 'Broj sasije', kubikaza as Kubikaza, konjskaSnaga as 'Konjska snaga', modelNaziv as Model, markaNaziv as Marka, vlasnikIme as Vlasnik, nazivVozila as 'Tip vozila' 
                                              from Vozilo join Model on Vozilo.modelID = Model.modelID
                                                          join Marka on Vozilo.markaID = Marka.markaID
                                                          join Vlasnik on Vozilo.vlasnikID = Vlasnik.vlasnikID
                                                          join TipVozila on Vozilo.tipVozilaID = TipVozila.tipVozilaID ";
        #endregion

        #region Select sa uslovom
        string selectUslovZaposleni = @"select * from Zaposleni where zaposleniID=";
        string selectUslovVlasnik = @"select * from Vlasnik where vlasnikID=";
        string selectUslovModel = @"select * from Model where modelID=";
        string selectUslovMarka = @"select * from Marka where markaID=";
        string selectUslovTipVozila = @"select * from TipVozila where tipVozilaID=";
        string selectUslovServis = @"select * from Servis where servisID=";
        string selectUslovDelovi = @"select * from Delovi where deoID=";
        string selectUslovVozilo = @"select * from Vozilo where voziloID=";
        #endregion

        #region Delete sa uslovom
        string zaposleniDelete = @"delete from Zaposleni where zaposleniID=";
        string vlasnikDelete = @"delete from Vlasnik where vlasnikID=";
        string modelDelete = @"delete from Model where modelID=";
        string markaDelete = @"delete from Marka where markaID=";
        string tipVozilaDelete = @"delete from TipVozila where tipVozilaID=";
        string servisDelete = @"delete from Servis where servisID=";
        string deloviDelete = @"delete from Delovi where deoID=";
        string voziloDelete = @"delete from Vozilo where voziloID=";

        #endregion
        public MainWindow()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            UcitajPodatke(dataGridCentralni, voziloSelect);

        }
        private void UcitajPodatke(DataGrid grid, string selectUpit)
        {
            try
            {
                konekcija.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(selectUpit, konekcija);
                DataTable dt = new DataTable();

                dataAdapter.Fill(dt);
                if (grid != null)
                {
                    grid.ItemsSource = dt.DefaultView;
                }
                ucitanaTabela = selectUpit;
                dt.Dispose();
                dataAdapter.Dispose();
            }
            catch (SqlException)
            {
                MessageBox.Show("Neuspesno Ucitani podaci!",
                                "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }
        void PopuniFormu(DataGrid grid, string selectUslov)
        {
            try
            {

                konekcija.Open();
                azuriraj = true;
                DataRowView red = (DataRowView)grid.SelectedItems[0];
                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                cmd.CommandText = selectUslov + "@id";
                SqlDataReader citac = cmd.ExecuteReader();
                cmd.Dispose();
                if (citac.Read())
                {
                    if (ucitanaTabela.Equals(voziloSelect))
                    {
                        FrmVozilo prozorVozilo = new FrmVozilo(azuriraj, red);
                        prozorVozilo.txtBrSasije.Text = citac["brojSasije"].ToString();
                        prozorVozilo.txtKonjskaSnaga.Text = citac["konjskaSnaga"].ToString();
                        prozorVozilo.txtKubikaza.Text = citac["kubikaza"].ToString();
                        prozorVozilo.cbModel.SelectedValue = citac["modelID"].ToString();
                        prozorVozilo.cbMarka.SelectedValue = citac["markaID"].ToString();
                        prozorVozilo.cbTipVozila.SelectedValue = citac["tipVozilaID"].ToString();
                        prozorVozilo.cbVlasnik.SelectedValue = citac["vlasnikID"].ToString();
                        prozorVozilo.ShowDialog();
                    }
                    if (ucitanaTabela.Equals(zaposleniSelect))
                    {
                        FrmZaposleni prozorZaposleni = new FrmZaposleni(azuriraj, red);
                        prozorZaposleni.txtIme.Text = citac["zaposleniIme"].ToString();
                        prozorZaposleni.txtPrezime.Text = citac["zaposleniPrezime"].ToString();
                        prozorZaposleni.txtJMBG.Text = citac["zaposleniJmbg"].ToString();
                        prozorZaposleni.txtKontakt.Text = citac["zaposleniKontakt"].ToString();
                        prozorZaposleni.txtAdresa.Text = citac["zaposleniAdresa"].ToString();
                        prozorZaposleni.ShowDialog();
                    }
                    if (ucitanaTabela.Equals(vlasnikSelect))
                    {
                        FrmVlasnik prozorVlasnik = new FrmVlasnik(azuriraj, red);
                        prozorVlasnik.txtIme.Text = citac["vlasnikIme"].ToString();
                        prozorVlasnik.txtPrezime.Text = citac["vlasnikPrezime"].ToString();
                        prozorVlasnik.txtJMBG.Text = citac["vlasnikJMBG"].ToString();
                        prozorVlasnik.txtKontakt.Text = citac["vlasnikKontakt"].ToString();
                        prozorVlasnik.txtAdresa.Text = citac["vlasnikAdresa"].ToString();
                        prozorVlasnik.ShowDialog();
                    }
                    if (ucitanaTabela.Equals(modelSelect))
                    {
                        FrmModel prozorModel = new FrmModel(azuriraj, red);
                        prozorModel.txtNaziv.Text = citac["modelNaziv"].ToString();
                        prozorModel.ShowDialog();
                    }
                    if (ucitanaTabela.Equals(markaSelect))
                    {
                        FrmMarka prozorMarka = new FrmMarka(azuriraj, red);
                        prozorMarka.txtNaziv.Text = citac["markaNaziv"].ToString();
                        prozorMarka.ShowDialog();
                    }
                    if (ucitanaTabela.Equals(tipVozilaSelect))
                    {
                        FrmTipVozila prozorTipVozila = new FrmTipVozila(azuriraj, red);
                        prozorTipVozila.txtTipVozila.Text = citac["nazivVozila"].ToString();
                        prozorTipVozila.ShowDialog();
                    }
                    if (ucitanaTabela.Equals(servisSelect))
                    {
                        FrmServis prozorServis = new FrmServis(azuriraj, red);
                        prozorServis.txtDatum.Text = citac["datumServisa"].ToString();
                        prozorServis.txtCena.Text = citac["cenaServisa"].ToString();
                        prozorServis.cbZaposleni.SelectedValue = citac["zaposleniID"].ToString();
                        prozorServis.ShowDialog();
                    }
                    if (ucitanaTabela.Equals(deloviSelect))
                    {
                        FrmDelovi prozorDelovi = new FrmDelovi(azuriraj, red);
                        prozorDelovi.txtNaziv.Text = citac["deoNaziv"].ToString();
                        prozorDelovi.txtCena.Text = citac["deoCena"].ToString();
                        prozorDelovi.cbServis.SelectedValue = citac["servisID"].ToString();
                        prozorDelovi.ShowDialog();
                    }
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Niste selektovali red!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }

            }
        }
        void ObrisiZapis(DataGrid grid, string deleteUpit)
        {
            try
            {
                konekcija.Open();
                DataRowView red = (DataRowView)grid.SelectedItems[0];
                MessageBoxResult rezultat = MessageBox.Show("Da li ste sigurni da zelite da obrisete?", "Upozorenje", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (rezultat == MessageBoxResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand()
                    {
                        Connection = konekcija
                    };
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = deleteUpit + "@id";
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Niste selektovali red", "Obavestenje", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (SqlException)
            {
                MessageBox.Show("Postoje povezani podaci u nekim drugim tabelama", "Obavestenje", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }
        private void btnVozilo_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, voziloSelect);
        }
        private void btnZaposleni_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, zaposleniSelect);
        }
        private void btnVlasnik_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, vlasnikSelect);
        }
        private void btnModel_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, modelSelect);
        }
        private void btnMarka_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, markaSelect);
        }
        private void btnTipVozila_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, tipVozilaSelect);
        }
        private void btnServis_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, servisSelect);
        }
        private void btnDelovi_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, deloviSelect);
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            Window prozor;
            if(ucitanaTabela.Equals(voziloSelect))
            {
                prozor = new FrmVozilo();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, voziloSelect);
            }
            else if (ucitanaTabela.Equals(zaposleniSelect))
            {
                prozor = new FrmZaposleni();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, zaposleniSelect);
            }
            else if (ucitanaTabela.Equals(vlasnikSelect))
            {
                prozor = new FrmVlasnik();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, vlasnikSelect);
            }
            else if (ucitanaTabela.Equals(modelSelect))
            {
                prozor = new FrmModel();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, modelSelect);
            }
            else if (ucitanaTabela.Equals(markaSelect))
            {
                prozor = new FrmMarka();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, markaSelect);
            }
            else if (ucitanaTabela.Equals(tipVozilaSelect))
            {
                prozor = new FrmTipVozila();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, tipVozilaSelect);
            }
            else if (ucitanaTabela.Equals(servisSelect))
            {
                prozor = new FrmServis();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, servisSelect);
            }
            else if (ucitanaTabela.Equals(deloviSelect))
            {
                prozor = new FrmDelovi();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, deloviSelect) ;
            }
        }
        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            if(ucitanaTabela.Equals(voziloSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovVozilo);
                UcitajPodatke(dataGridCentralni, voziloSelect);
            }
            else if(ucitanaTabela.Equals(zaposleniSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovZaposleni);
                UcitajPodatke(dataGridCentralni, zaposleniSelect);
            }
            else if (ucitanaTabela.Equals(vlasnikSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovVlasnik);
                UcitajPodatke(dataGridCentralni, vlasnikSelect);
            }
            else if (ucitanaTabela.Equals(modelSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovModel);
                UcitajPodatke(dataGridCentralni, modelSelect);
            }
            else if (ucitanaTabela.Equals(markaSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovMarka);
                UcitajPodatke(dataGridCentralni, markaSelect);
            }
            else if (ucitanaTabela.Equals(tipVozilaSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovTipVozila);
                UcitajPodatke(dataGridCentralni, tipVozilaSelect) ;
            }
            else if (ucitanaTabela.Equals(servisSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovServis);
                UcitajPodatke(dataGridCentralni, servisSelect);
            }
            else if (ucitanaTabela.Equals(deloviSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovDelovi);
                UcitajPodatke(dataGridCentralni, deloviSelect);
            }
        }
        private void btnObrisi_Click_1(object sender, RoutedEventArgs e)
        {
            if (ucitanaTabela.Equals(voziloSelect))
            {
                ObrisiZapis(dataGridCentralni, voziloDelete);
                UcitajPodatke(dataGridCentralni, voziloSelect);
            }
            else if (ucitanaTabela.Equals(zaposleniSelect))
            {
                ObrisiZapis(dataGridCentralni, zaposleniDelete);
                UcitajPodatke(dataGridCentralni, zaposleniSelect);
            }
            else if (ucitanaTabela.Equals(vlasnikSelect))
            {
                ObrisiZapis(dataGridCentralni, vlasnikDelete);
                UcitajPodatke(dataGridCentralni, vlasnikSelect);
            }
            else if (ucitanaTabela.Equals(modelSelect))
            {
                ObrisiZapis(dataGridCentralni, modelDelete);
                UcitajPodatke(dataGridCentralni, modelSelect);
            }
            else if (ucitanaTabela.Equals(markaSelect))
            {
                ObrisiZapis(dataGridCentralni, markaDelete);
                UcitajPodatke(dataGridCentralni, markaSelect);
            }
            else if (ucitanaTabela.Equals(tipVozilaSelect))
            {
                ObrisiZapis(dataGridCentralni, tipVozilaDelete);
                UcitajPodatke(dataGridCentralni, tipVozilaSelect);
            }
            else if (ucitanaTabela.Equals(servisSelect))
            {
                ObrisiZapis(dataGridCentralni, servisDelete);
                UcitajPodatke(dataGridCentralni, servisSelect);
            }
            else if (ucitanaTabela.Equals(deloviSelect))
            {
                ObrisiZapis(dataGridCentralni, deloviDelete);
                UcitajPodatke(dataGridCentralni, deloviSelect);
            }
        }
    }
   
}
