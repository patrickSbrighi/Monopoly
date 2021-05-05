using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ProgettoMonopoly
{
    public class Prigione : Casella
    {
        private List<Pedina> _pedineInPrigione;
        private const int _quotaPerUscire = 125;
        private int _numeroRound;

        public Prigione(string nomeCasella, int numeroCasella, Thickness margine) : base(nomeCasella, numeroCasella, margine)
        {
            NumeroRound = 0;
            _pedineInPrigione = new List<Pedina>();
        }

        public List<Pedina> PedineInPrigione
        {
            get
            {
                return _pedineInPrigione;
            }
            set
            {
                _pedineInPrigione = value;
            }
        }

        public int NumeroRound
        {
            get
            {
                return _numeroRound;
            }
            set
            {
                _numeroRound = value;
            }
        }

        public void EntraInPrigione(Pedina pedina)
        {
            if (!pedina.PedinaInPrigione && pedina.Posizione == this)
            {
                pedina.PedinaInPrigione = true;
                PedineInPrigione.Add(pedina);
            }
        }

        public void EntraInPrigione(Pedina pedina, CartaImprevisto imprevisto)
        {
            if (!pedina.PedinaInPrigione && imprevisto.Id == 14)//da mettere l'id dell'imprevisto prigione
            {
                pedina.PedinaInPrigione = true;
                PedineInPrigione.Add(pedina);
            }
        }

        public void EntraInPrigione(Pedina pedina, CartaProbabilita probabilita)
        {
            if (!pedina.PedinaInPrigione && probabilita.Id == 14)//da mettere l'id dell'probabilita prigione
            {
                pedina.PedinaInPrigione = true;
                PedineInPrigione.Add(pedina);
            }
        }

        public void EsciDaPrigione(Pedina pedina, float pagamento)
        {
            if(PedineInPrigione.Contains(pedina) && pagamento == _quotaPerUscire)
            {
                pedina.PedinaInPrigione = false;
                PedineInPrigione.Remove(pedina);      // implementare interfaccia
            }
        }

        public void EsciDaPrigione(Pedina pedina, int dado1, int dado2)
        {
            if (PedineInPrigione.Contains(pedina) && dado1 == dado2)
            {
                pedina.PedinaInPrigione = false;
                PedineInPrigione.Remove(pedina);
            }
        }

        public void EsciDaPrigione(Pedina pedina, CartaImprevisto imprevisto)
        {
            if (PedineInPrigione.Contains(pedina) && imprevisto.Id == 15)//da mettere l'id dell'imprevisto esci di prigione
            {
                pedina.PedinaInPrigione = false;
                PedineInPrigione.Remove(pedina);
            }
        }

        public void EsciDaPrigione(Pedina pedina, CartaProbabilita probabilita)
        {
            if (PedineInPrigione.Contains(pedina) && probabilita.Id == 15) //da mettere l'id dell'probabilita esci di prigione
            {
                pedina.PedinaInPrigione = false;
                PedineInPrigione.Remove(pedina);
            }
        }

        public void AumentaRoundTrascorsi()
        {
            NumeroRound++;
        }
    }
}