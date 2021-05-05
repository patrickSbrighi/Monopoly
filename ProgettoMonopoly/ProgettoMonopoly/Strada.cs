using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ProgettoMonopoly
{
    public class Strada : Proprieta
    {
        private Distretto _distretto;
        public Strada(int numeroCasella, string nome, Contratto contratto, Thickness margine) : base(numeroCasella, nome, contratto, margine)
        {

        }

        public Distretto Distretto
        {
            get
            {
                return _distretto;
            }
            set
            {
                _distretto = value;
            }
        }

    }
}