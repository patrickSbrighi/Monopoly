using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgettoMonopoly
{
    public class Turno
    {
        private Pedina _pedina;
        private int _numeroVolteDoppiDadi;

        public Turno(Pedina pedina)
        {
            Pedina = pedina;
        }

        public Pedina Pedina
        {
            get
            {
                return _pedina;
            }
            set
            {
                _pedina = value;
            }
        }
        public int NumeroVolteDoppiDadi
        {
            get
            {
                return _numeroVolteDoppiDadi;
            }
            set
            {
                _numeroVolteDoppiDadi = value;
            }
        }
    }
}