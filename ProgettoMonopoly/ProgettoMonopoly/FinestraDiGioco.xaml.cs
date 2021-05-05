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
using System.Threading;

namespace ProgettoMonopoly
{
    /// <summary>
    /// Logica di interazione per FinestraDiGioco.xaml
    /// </summary>
    public partial class FinestraDiGioco : Window
    {
        Server server;
        Gioco client;
        Tabellone tabellone;

        readonly Uri uriFaccia1 = new Uri(@"\Immagini\FacceDadi\faccia1.png", UriKind.Relative);
        readonly Uri uriFaccia2 = new Uri(@"\Immagini\FacceDadi\faccia2.png", UriKind.Relative);
        readonly Uri uriFaccia3 = new Uri(@"\Immagini\FacceDadi\faccia3.png", UriKind.Relative);
        readonly Uri uriFaccia4 = new Uri(@"\Immagini\FacceDadi\faccia4.png", UriKind.Relative);
        readonly Uri uriFaccia5 = new Uri(@"\Immagini\FacceDadi\faccia5.png", UriKind.Relative);
        readonly Uri uriFaccia6 = new Uri(@"\Immagini\FacceDadi\faccia6.png", UriKind.Relative);

        Random r;
        int dado1;
        int dado2;
        bool estratti = false;

        public FinestraDiGioco(Server server, Tabellone tabellone)
        {
            InitializeComponent();
            SorteggioDadi();
            this.server = server;
            client = new Gioco(tabellone, server.Turni);
        }

        public FinestraDiGioco()
        {
            InitializeComponent();
            SorteggioDadi();
            server = new Server();
            tabellone = new Tabellone();
            client = new Gioco(tabellone, server.Turni);
        }

        private async void SorteggioDadi()
        {
            try
            {
                r = new Random();
                await Task.Run(() =>
                {
                    while (!estratti)
                    {
                        dado1 = r.Next(1, 7);
                        dado2 = r.Next(1, 7);
                        this.Dispatcher.BeginInvoke(new Action(() => {
                            AssegnazioneImmagine(imgDado1, dado1);
                            AssegnazioneImmagine(imgDado2, dado2);
                        }));
                        Thread.Sleep(10);
                    }

                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void AssegnazioneImmagine(Image img, int dado)
        {
            try
            {
                switch (dado)
                {
                    case 1:
                        img.Source = new BitmapImage(uriFaccia1);
                        break;
                    case 2:
                        img.Source = new BitmapImage(uriFaccia2);
                        break;
                    case 3:
                        img.Source = new BitmapImage(uriFaccia3);
                        break;
                    case 4:
                        img.Source = new BitmapImage(uriFaccia4);
                        break;
                    case 5:
                        img.Source = new BitmapImage(uriFaccia5);
                        break;
                    case 6:
                        img.Source = new BitmapImage(uriFaccia6);
                        break;
                    default:
                        throw new Exception("Errore");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnLanciaDadi_Click(object sender, RoutedEventArgs e)
        {
            estratti = true;
            int sommaDadi = int.Parse(imgDado1.Source.ToString()[imgDado1.Source.ToString().Length - 5].ToString()) + int.Parse(imgDado2.Source.ToString()[imgDado2.Source.ToString().Length - 5].ToString());
            string messaggio = $"MOVE {sommaDadi}";
            server.InviaMessaggio(messaggio);

            client.MuoviPedina(sommaDadi);

            if(client.TurnoAttuale.Pedina.Posizione is Proprieta && (client.TurnoAttuale.Pedina.Posizione as Proprieta).Comprata == false)
            {
                btnCompra.IsEnabled = true;
                btnNonComprare.IsEnabled = true;
            }
           
        }

        private void btnCompra_Click(object sender, RoutedEventArgs e)
        {
            string messaggio = $"BUY {client.TurnoAttuale.Pedina.Posizione.Numerocasella}";
            server.InviaMessaggio(messaggio);

            client.CompraProprieta();

        }

        private void btnNonComprare_Click(object sender, RoutedEventArgs e)
        {
            string messaggio = $"NOBUY";
            server.InviaMessaggio(messaggio);
        }

        private void CambiaTurno()
        {

        }
    }
}
