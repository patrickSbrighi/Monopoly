﻿using System;
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
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ProgettoMonopoly
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<BitmapImage> listaPedine;
        Uri uriScarpa = new Uri(@"\Immagini\Pedine\Scarpa.png", UriKind.Relative);
        Uri uriCappello = new Uri(@"\Immagini\Pedine\Cappello.png", UriKind.Relative);
        Uri uriCariola = new Uri(@"\Immagini\Pedine\Cariola.png", UriKind.Relative);
        Uri uriDitale = new Uri(@"\Immagini\Pedine\Ditale.png", UriKind.Relative);

        Gioco client;
        public MainWindow()
        {
            InitializeComponent();
            CaricamentoPedine();
            client = new Gioco(new Tabellone(), new Server());
        }

        private void CaricamentoPedine()
        {
            listaPedine = new List<BitmapImage>();
            listaPedine.Add(new BitmapImage(uriScarpa));
            listaPedine.Add(new BitmapImage(uriCappello));
            listaPedine.Add(new BitmapImage(uriCariola));
            listaPedine.Add(new BitmapImage(uriDitale));
            imgPedina.Source = listaPedine[0];
        }

        private void btnInviaRichiestaGioco_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                client.EntraInLobby(txtBoxNome.Text);
                client.PedinaPrincipale = new Pedina(txtBoxNome.Text, imgPedina.Source.ToString());
                ControlloStatoPartita();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void ControlloStatoPartita()
        {
            try
            {
                await Task.Run(() =>
                {
                    bool giaEntrato = false;
                    while (true)
                    {
                        if (client.InLobby && !giaEntrato)
                        {
                            giaEntrato = true;
                            MessageBox.Show("Sei stato inserito nella partita, aspetta che tutti i giocatori si uniscano");
                        }
                        else if (client.InGame)
                        {
                            FinestraDiGioco finestraDiGioco = new FinestraDiGioco(client);
                            finestraDiGioco.Show();
                            this.Close();
                        }
                        else if (client.Errore != null)
                        {
                            MessageBox.Show(client.Errore);
                        }
                        Thread.Sleep(1);
                    }

                });
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        private void btnPedinaPrima_Click(object sender, RoutedEventArgs e)
        { 
            for (int i = 0; i < listaPedine.Count; i++)
            {
                if(imgPedina.Source == listaPedine[i] && i != 0)
                {
                    imgPedina.Source = listaPedine[i - 1];
                    break;
                }
                else if (imgPedina.Source == listaPedine[i] && i == 0)
                {
                    imgPedina.Source = listaPedine[listaPedine.Count - 1];
                    break;
                }
            }
        }

        private void btnPedinaDopo_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < listaPedine.Count; i++)
            {
                if (imgPedina.Source == listaPedine[i] && i != listaPedine.Count -1)
                {
                    imgPedina.Source = listaPedine[i + 1];
                    break;
                }
                else if(imgPedina.Source == listaPedine[i] && i == listaPedine.Count -1)
                {
                    imgPedina.Source = listaPedine[0];
                    break;
                }
            }
        }
    }
}
