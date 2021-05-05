using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.IO;

namespace ProgettoMonopoly
{
    public class Tabellone
    {
        private List <Casella> _listaCaselle;
        private ObservableCollection<Pedina> _listaPedine;
        private MazzoImprevisti _mazzoImprevisti;
        private MazzoProbabilita _mazzoProbabilita;
        private Dictionary<int, int> _listaBanconote; //chiave tipo banconata es:500 valore numero di quelle banconote es:2

        public Tabellone(List<Casella> listaCaselle, ObservableCollection<Pedina> listaPedine, MazzoImprevisti mazzoImprevisti,
            MazzoProbabilita mazzoProbabilita, Dictionary<int, int> listaBanconote)
        {
            ListaCaselle = listaCaselle;
            ListaPedine = listaPedine;
            MazzoImprevisti = mazzoImprevisti;
            MazzoProbabilita = mazzoProbabilita;
            ListaBanconote = listaBanconote;
        }

        public Tabellone()
        {
            DeserializzazioneMazzoImprevisti();
            DeserializzazioneMazzoProbabilita();
        }

        private List<Casella> ListaCaselle
        {
            get
            {
                return _listaCaselle;
            }
            set
            {
                _listaCaselle = value;
            }
        }

        private ObservableCollection<Pedina> ListaPedine
        {
            get
            {
                return _listaPedine;
            }
            set
            {
                _listaPedine = value;
            }
        }

        public MazzoImprevisti MazzoImprevisti
        {
            get
            {
                return _mazzoImprevisti;
            }
            private set
            {
                _mazzoImprevisti = value;
            }
        }

        public MazzoProbabilita MazzoProbabilita
        {
            get
            {
                return _mazzoProbabilita;
            }
            private set
            {
                _mazzoProbabilita = value;
            }
        }

        private Dictionary<int, int> ListaBanconote
        {
            get
            {
                return _listaBanconote;
            }
            set
            {
                _listaBanconote = value;
            }
        }

        public Casella GetCasella(int indice)
        {
            if (indice > 40)
            {
                indice -= 40;
            }

            foreach (Casella item in ListaCaselle)
            {
                if(item.Numerocasella == indice)
                {
                    return item;
                }
            }
            throw new Exception("Casella non trovata");
        }

        public Prigione GetPrigione()
        {
            foreach (Casella casella in ListaCaselle)
            {
                if(casella is Prigione)
                {
                    return casella as Prigione;
                }
            }
            throw new Exception();
        }

        private void DeserializzazioneMazzoImprevisti()
        {
            XmlSerializer deserializzatore = new XmlSerializer(typeof(MazzoImprevisti));
            using (StreamReader sr = new StreamReader("Imprevisti.xml"))
            {
                if(sr.ReadLine() != null)
                {
                    MazzoImprevisti = deserializzatore.Deserialize(sr) as MazzoImprevisti;
                }
                else
                {
                    throw new Exception();
                }
                
            }
        }

        private void DeserializzazioneMazzoProbabilita()
        {
            XmlSerializer deserializzatore = new XmlSerializer(typeof(MazzoProbabilita));
            using (StreamReader sr = new StreamReader("Probabilita.xml"))
            {
                if (sr.ReadLine() != null)
                {
                    MazzoProbabilita = deserializzatore.Deserialize(sr) as MazzoProbabilita;
                }
                else
                {
                    throw new Exception();
                }

            }
        }
    }
}