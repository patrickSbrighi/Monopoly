using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgettoMonopoly
{
    public abstract class Contratto
    {
        private string _nomeContratto;
        private int _valoreContratto;
        private List<int> _rendita;

        public Contratto(string nomeContratto, int valoreContratto, List<int> rendita)
        {
            NomeContratto = nomeContratto;
            ValoreContratto = valoreContratto;
            Rendita = rendita;
        }

        public List<int> Rendita
        {
            get
            {
                return _rendita;
            }
            private set
            {
                _rendita = value;
            }
        }

        public string NomeContratto
        {
            get
            {
                return _nomeContratto;
            }
            private set
            {
                _nomeContratto = value;
            }
        }

        public int ValoreContratto
        {
            get
            {
                return _valoreContratto;
            }
            private set
            {
                _valoreContratto = value;
            }
        }

        public int ValoreIpotecato
        {
            get
            {
                return ValoreContratto / 2;
            }
            private set
            {

            }
        }

        




    }
}