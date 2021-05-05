using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ProgettoMonopoly
{
    public class Probabilita : Casella
    {
        public Probabilita(string nomeCasella, int numeroCasella, Thickness margine) : base(nomeCasella, numeroCasella, margine)
        {
            
        }

        public CartaProbabilita Pesca(MazzoProbabilita mazzo, int posizioneCartaPescata)
        {
            return mazzo.ListaProbabilita[posizioneCartaPescata];
        }
    }
}