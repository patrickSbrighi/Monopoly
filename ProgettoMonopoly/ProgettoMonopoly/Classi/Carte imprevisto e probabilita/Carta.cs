using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ProgettoMonopoly
{
    public abstract class Carta
    {
        private int _id;
        private string _descrizione;
        
        public Carta(int id, string descrizione)
        {
            Id = id;
            Descrizione = descrizione;
        }
        public Carta()
        {

        }

        [XmlAttribute (AttributeName = "Id")]
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public string Descrizione
        {
            get
            {
                return _descrizione;
            }
            set
            {
                _descrizione = value;
            }
        }

    }
}