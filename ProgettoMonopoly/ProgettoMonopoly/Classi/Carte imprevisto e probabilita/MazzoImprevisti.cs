using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgettoMonopoly
{
    public class MazzoImprevisti
    {
        private List<CartaImprevisto> _listaImprevisti;

        public MazzoImprevisti(List<CartaImprevisto> listaImprevisti)
        {
            ListaImprevisti = listaImprevisti;
        }

        public MazzoImprevisti()
        {

        }

        public List<CartaImprevisto> ListaImprevisti
        {
            get
            {
                return _listaImprevisti;
            }
            set
            {
                _listaImprevisti = value;
            }
        }
    }
}