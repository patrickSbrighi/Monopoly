using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgettoMonopoly
{
    public class MazzoProbabilita
    {
        private List<CartaProbabilita> _listaProbabilita;

        public MazzoProbabilita(List<CartaProbabilita> listaProbabilita)
        {
            ListaProbabilita = listaProbabilita;
        }

        public MazzoProbabilita()
        {

        }

        public List<CartaProbabilita> ListaProbabilita
        {
            get
            {
                return _listaProbabilita;
            }
            set
            {
                _listaProbabilita = value;
            }
        }
    }
}