using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgettoMonopoly
{
    public class Asta
    {
        private const int _prezzoInizio = 10;
        private List<Pedina> _asta;
        public Proprieta ProprietaAllAsta { get; set; }
        public int PuntataAttuale { get; set; }
        public bool AstaFinita { get; set; }
        public Pedina Vincitore { get; set; }
        
        public Asta(Proprieta proprietaAllAsta, List<Pedina> listaPedinePatecipanti)
        {
            ProprietaAllAsta = proprietaAllAsta;
            _asta = listaPedinePatecipanti;
        }

        public void Punta(Pedina pedina, int importo)
        {
            if(importo > PuntataAttuale && _asta.Contains(pedina) && !AstaFinita)
            {
                PuntataAttuale = importo;
            }
            else
            {
                throw new Exception();
            }
            
        }

        public void LasciaAsta(Pedina pedina)
        {
            _asta.Remove(pedina);
            AstaFinita = ControlloVincitore();
        }

        private bool ControlloVincitore()
        {
            if(_asta.Count == 1)
            {
                Vincitore = _asta[0];
                return true;
            }
            else
            {
                return false;
            }
           
        }
    }
}