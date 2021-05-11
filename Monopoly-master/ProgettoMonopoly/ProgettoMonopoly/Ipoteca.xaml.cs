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

namespace ProgettoMonopoly
{
    /// <summary>
    /// Logica di interazione per Ipoteca.xaml
    /// </summary>
    public partial class Ipoteca : Window
    {
        Gioco gioco;
        public Ipoteca(Gioco g)
        {
            InitializeComponent();
            lstViewProprieta.ItemsSource = g.TurnoAttuale.Pedina.ListaProprieta;
            gioco = g;
        }

        private void btnIpoteca_Click(object sender, RoutedEventArgs e)
        {
            gioco.Ipoteca(lstViewProprieta.SelectedItems as List<Proprieta>);
        }

        private void btnNonIpotecare_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void lstViewProprieta_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int denaroIpotecato = 0;

            foreach (Proprieta item in lstViewProprieta.SelectedItems)
            {
                denaroIpotecato += item.Contratto.ValoreIpotecato;
            }

            lblConto.Content = $"Denaro se ipotechi: {denaroIpotecato}";
        }
    }
}
