using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ProgettoMonopoly
{
    public class Stazione : Proprieta
    {
        public Stazione(int numeroCasella, string nome, Contratto contratto, Thickness margine) : base(numeroCasella, nome, contratto, margine)
        {

        }
    }
}