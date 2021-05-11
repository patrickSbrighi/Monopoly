using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ProgettoMonopoly
{
    public class InPrigione : Casella
    {
        public InPrigione(string nomeCasella, int numeroCasella, Thickness margine) : base(nomeCasella, numeroCasella, margine)
        {
            
        }

        public void SpostaInPrigione(Prigione prigione, Pedina pedina)
        {
            pedina.PedinaInPrigione = true;
            prigione.PedineInPrigione.Add(pedina);
        }
    }
}