using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ProgettoMonopoly
{
    public class Via : Casella
    {
        private const int _valore = 400;
        public Via(string nomeCasella, int numeroCasella, Thickness margine) : base(nomeCasella, numeroCasella, margine)
        {

        }

        public void Paga(Pedina pedina)
        {
            pedina.DenaroPedina += _valore;
        }
    }
}