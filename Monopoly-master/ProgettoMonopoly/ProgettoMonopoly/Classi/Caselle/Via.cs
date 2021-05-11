using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ProgettoMonopoly
{
    public class Via : Casella
    {
        private const int _valore = 200;
        public Via(string nomeCasella, int numeroCasella, Thickness margine) : base(nomeCasella, numeroCasella, margine)
        {

        }

        public int PassaggioDalVia
        {
            get
            {
                return _valore;
            }
        }
    }
}