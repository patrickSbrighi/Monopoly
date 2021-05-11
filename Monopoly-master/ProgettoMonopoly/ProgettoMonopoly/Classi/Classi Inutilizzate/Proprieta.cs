using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ProgettoMonopoly
{
    public abstract class Proprieta : Casella
    {
        private bool _comprata;
        private Contratto _contratto;
        private int _livelloProprieta;
        private Pedina _proprietario;

        public Proprieta(int numeroCasella, string nome, Contratto contratto, Thickness margine) : base(nome, numeroCasella, margine)
        {
            LivelloProprieta = 0;
            Comprata = false;
            Contratto = contratto;
        }

        public bool Comprata
        {
            get
            {
                return _comprata;
            }
            set
            {
                _comprata = value;
            }
        }

        public Pedina Proprietario
        {
            get
            {
                return _proprietario;
            }
            set
            {
                _proprietario = value;
            }
        }

        public Contratto Contratto
        {
            get
            {
                return _contratto;
            }
            private set
            {
                _contratto = value;
            }
        }

        public int LivelloProprieta
        {
            get
            {
                return _livelloProprieta;
            }
            set
            {
                _livelloProprieta = value;
            }
        }


    }
}