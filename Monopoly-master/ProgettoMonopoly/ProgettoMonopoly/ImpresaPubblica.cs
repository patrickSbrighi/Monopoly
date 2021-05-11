using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ProgettoMonopoly
{
    public class ImpresaPubblica : Proprieta
    {
        public ImpresaPubblica(int numeroCasella, string nome, Contratto contratto, Thickness margine) : base(numeroCasella, nome, contratto, margine)
        {

        }

    }
}