using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgettoMonopoly
{
    public class Distretto
    {
        private List<Strada> _listaStrade;
        private string _colore;
        public Distretto()
        {

        }

        public List<Strada> ListaStrade
        {
            get
            {
                return _listaStrade;
            }
            private set
            {
                _listaStrade = value;
            }
        }

        public string Colore
        {
            get
            {
                return _colore;
            }
            private set
            {
                _colore = value;
            }
        }
    }
}