using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ProgettoMonopoly
{
    public abstract class Casella
    {
        private string _nomeCasella;
        private int _numeroCasella;
        private Thickness _margine;

        public Casella(string nomeCasella, int numeroCasella, Thickness margine)
        {
            NomeCasella = nomeCasella;
            Numerocasella = numeroCasella;
            Margine = margine;
        }
        
        public Casella()
        {

        }

        public string NomeCasella
        {
            get
            {
                return _nomeCasella;
            }
            private set
            {
                _nomeCasella = value;
            }
        }

        public int Numerocasella
        {
            get
            {
                return _numeroCasella;
            }
            private set
            {
                _numeroCasella = value;
            }
        }

        public Thickness Margine
        {
            get
            {
                return _margine;
            }
            private set
            {
                _margine = value;
            }
        }

    }
}