using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgettoMonopoly
{
    public class Pedina : IEquatable<Pedina>
    {
        private bool _pedinaSuQuestoClient;
        private string _nome;
        private string _percorsoImmagine;
        private List<Proprieta> _listaProprieta;
        private Casella _posizione;
        private bool _pedinaInPrigione;
        private float _denaroPedina;
        private List<Proprieta> _listaProprietaIpotecate;

        public Pedina(string nome)
        {
            PedinaInPrigione = false;
        }

        public bool PedinaSuQuestoClient
        {
            get
            {
                return _pedinaSuQuestoClient;
            }
            private set
            {
                _pedinaSuQuestoClient = value;
            }
        }

        public string Nome
        {
            get
            {
                return _nome;
            }
            set
            {
                _nome = value;
            }
        }

        public string PercorsoImmagine
        {
            get
            {
                return _percorsoImmagine;
            }
            set
            {
                _percorsoImmagine = value;
            }
        }

        public List<Proprieta> ListaProprieta
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

        public Casella Posizione
        {
            get
            {
                return _posizione;
            }
            set
            {
                _posizione = value;
            }
        }

        public bool PedinaInPrigione
        {
            get
            {
                return _pedinaInPrigione;
            }
            set
            {
                _pedinaInPrigione = value;
            }
        }
        
        public float DenaroPedina
        {
            get
            {
                return _denaroPedina;
            }
            set
            {
                _denaroPedina = value;
                if (Fallisci())
                    throw new Exception("il giocatore ha fallito");
            }
        }

        public List<Proprieta> ProprietaIpotecate
        {
            get
            {
                return _listaProprietaIpotecate;
            }
            set
            {
                _listaProprietaIpotecate = value;
            }
        }

        public bool Fallisci()
        {
            if (DenaroPedina < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
                
        }

        public bool Equals(Pedina other)
        {
            if(this.Nome == other.Nome)
            {
                return true;
            }
            return false;
        }
    }
}