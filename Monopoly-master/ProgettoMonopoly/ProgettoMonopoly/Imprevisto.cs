using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ProgettoMonopoly
{
    public class Imprevisto : Casella
    {
        public Imprevisto(string nomeCasella, int numeroCasella, Thickness margine) : base(nomeCasella, numeroCasella, margine)
        {

        }

        public CartaImprevisto Pesca(MazzoImprevisti mazzo, int posizioneCartaPescata)
        {
            return mazzo.ListaImprevisti[posizioneCartaPescata];
        }

    }
}