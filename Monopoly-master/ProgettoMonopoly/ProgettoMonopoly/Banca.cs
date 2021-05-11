using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace ProgettoMonopoly
{
    public class Banca
    {
        private float _denaro;
        private ObservableCollection<Proprieta> _listaProprieta;
        public Banca()
        {
            
        }

        public float DenaroBanca
        {
            get
            {
                return _denaro;
            }
            set
            {
                _denaro = value;
            }
        }

        public ObservableCollection<Proprieta> ListaProprietaBanca
        {
            get
            {
                return _listaProprieta;
            }
            set
            {
                _listaProprieta = value;
            }
        }

        public void DistribuisciDenaroIniziale(Pedina pedina)
        {
            pedina.DenaroPedina = 1500;
        }

        public void VendiProprietaAPedina(Pedina pedina)
        {
            if(ListaProprietaBanca.Contains(pedina.Posizione as Proprieta))
            {
                pedina.DenaroPedina -= (pedina.Posizione as Proprieta).Contratto.ValoreContratto;
                DenaroBanca += (pedina.Posizione as Proprieta).Contratto.ValoreContratto;
                ListaProprietaBanca.Remove(pedina.Posizione as Proprieta);
                pedina.ListaProprieta.Add(pedina.Posizione as Proprieta);
            }
            else
            {
                throw new Exception();
            }
           
        }

        public void VendiProprietaAVincitoreAsta(Asta asta)
        {
            asta.Vincitore.DenaroPedina -= asta.PuntataAttuale;
            DenaroBanca += asta.PuntataAttuale;
            ListaProprietaBanca.Remove(asta.ProprietaAllAsta);
            asta.Vincitore.ListaProprieta.Add(asta.ProprietaAllAsta);
        }

        public void PagaPassaggioDalVia(Pedina pedina, int pagamento)
        {
            DenaroBanca -= pagamento;
            pedina.DenaroPedina += pagamento;
        }

    }
}