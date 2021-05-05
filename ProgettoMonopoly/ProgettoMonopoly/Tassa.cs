using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgettoMonopoly
{
    public class Tassa : Casella
    {
        private int _costo;
        public Tassa() : base()
        {

        }

        public int Costo
        {
            get
            {
                return _costo;
            }
            private set
            {
                _costo = value;
            }
        }
    }
}