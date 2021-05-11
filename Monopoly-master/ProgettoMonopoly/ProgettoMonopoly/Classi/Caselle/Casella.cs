using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Xml.Serialization;

namespace ProgettoMonopoly
{
    public abstract class Casella
    {
        //Attributi casella normale
        private string _nomeCasella;
        private int _numeroCasella;
        private Thickness _margine;

        //Attributi casella proprietà
        private bool _comprata;
        private Contratto _contratto;
        private int _livelloProprieta;
        private Pedina _proprietario;

        //Costanti valori
        private const int _QUOTA_PRIGIONE = 125;
        private const int _VALORE = 200;

        //Costanti Id Caselle
        private const int _ID_IN_PRIGIONE = 30;
        private readonly int[] _ID_PROPRIETA = { 1, 3, 6, 8, 9, 11, 13, 14, 16, 18, 19, 21, 23, 24, 26, 27, 29, 30, 32, 34, 37, 39 };
        private const int _ID_VIA = 0;
        private readonly int[] _ID_PROBABILITA = { 2, 17, 33 };
        private readonly int[] _ID_TASSA = { 4, 38 };
        private readonly int[] _ID_STAZIONI = { 5, 15, 25, 35 };
        private readonly int[] _ID_IMPREVISTI = { 7, 22, 36 };
        private const int _ID_PRIGIONE = 10;
        private readonly int[] _ID_IMPRESE = { 12, 28 };
        private const int _ID_PARCHEGGIO = 20;

        public Casella(string nomeCasella, int numeroCasella, Thickness margine)
        {
            NomeCasella = nomeCasella;
            Numerocasella = numeroCasella;
            Margine = margine;

            foreach (int id in _ID_PROPRIETA)
            {
                if (id == Numerocasella)
                {
                    Comprata = false;
                    LivelloProprieta = 0;
                }
            }
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

        //DANNI-----------------------------------------------------------------------------------------------------------------------------------
        public void SpostaInPrigione(int idCasella, Pedina pedina)
        {
            if (idCasella == _ID_IN_PRIGIONE)
            {
                pedina.PedinaInPrigione = true;
                pedina.Posizione = _ID_IN_PRIGIONE;
            }
            //prigione.PedineInPrigione.Add(pedina);
        }

        public void EntraInPrigione(Pedina pedina)
        {
            if (!pedina.PedinaInPrigione && pedina.Posizione == this.Numerocasella)
            {
                pedina.PedinaInPrigione = true;
            }
        }

        public void EntraInPrigione(Pedina pedina, Carta carta)
        {
            if (!pedina.PedinaInPrigione && carta.Id == 14)//da mettere l'id dell'imprevisto prigione
            {
                pedina.PedinaInPrigione = true;
            }
        }

        public void EsciDaPrigione(Pedina pedina, float pagamento)
        {
            if (pagamento == _QUOTA_PRIGIONE)
            {
                pedina.PedinaInPrigione = false;
            }
        }

        public void EsciDaPrigione(Pedina pedina, Carta carta)
        {
            if (carta.Id == 15)//da mettere l'id dell'imprevisto esci di prigione
            {
                pedina.PedinaInPrigione = false;
            }
        }

        /*public void AumentaRoundTrascorsi() TODO uscita turni
        {
            NumeroRound++;
        }*/
        public int PassaggioDalVia()
        {
            return _VALORE;
        }

        public bool Comprata
        {
            get
            {
                foreach(int id in _ID_PROPRIETA)
                {
                    if(id == Numerocasella)
                        return _comprata;
                }

                throw new Exception("Non è una proprietà");
            }
            set
            {
                foreach (int id in _ID_PROPRIETA)
                {
                    if (id == Numerocasella)
                        _comprata = value;
                }
            }
        }

        public Pedina Proprietario
        {
            get
            {
                foreach (int id in _ID_PROPRIETA)
                {
                    if (id == Numerocasella)
                        return _proprietario;
                }

                throw new Exception("Non è una proprietà");                
            }
            set
            {
                foreach (int id in _ID_PROPRIETA)
                {
                    if (id == Numerocasella)
                        _proprietario = value;
                }                
            }
        }

        public Contratto Contratto
        {
            get
            {
                foreach (int id in _ID_PROPRIETA)
                {
                    if (id == Numerocasella)
                        return _contratto;
                }
                throw new Exception("Non è una proprietà");
            }
            private set
            {
                foreach (int id in _ID_PROPRIETA)
                {
                    if (id == Numerocasella)
                        _contratto = value;
                }                
            }
        }

        public int LivelloProprieta
        {
            get
            {
                foreach (int id in _ID_PROPRIETA)
                {
                    if (id == Numerocasella)
                        return _livelloProprieta;
                }
                throw new Exception("Non è una proprietà");
                
            }
            set
            {
                foreach (int id in _ID_PROPRIETA)
                {
                    if (id == Numerocasella)
                        _livelloProprieta = value;
                }                
            }
        }

        //riconoscimento casella
        public bool IsProprieta()
        {
            foreach (int id in _ID_PROPRIETA)
            {
                if (id == Numerocasella)
                    return true;
            }

            return false;
        }

        public bool IsVia()
        {
            if (Numerocasella == _ID_VIA)
                return true;
            return false;
        }

        public bool IsProbabilita()
        {
            foreach (int id in _ID_PROBABILITA)
            {
                if (id == Numerocasella)
                    return true;
            }

            return false;
        }

        public bool IsTassa()
        {
            foreach (int id in _ID_TASSA)
            {
                if (id == Numerocasella)
                    return true;
            }

            return false;
        }

        public bool IsStazione()
        {
            foreach (int id in _ID_STAZIONI)
            {
                if (id == Numerocasella)
                    return true;
            }

            return false;
        }

        public bool IsImprevisti()
        {
            foreach (int id in _ID_IMPREVISTI)
            {
                if (id == Numerocasella)
                    return true;
            }

            return false;
        }

        public bool IsPrigione()
        {
            if (Numerocasella == _ID_PRIGIONE)
                return true;
            return false;
        }

        public bool IsImpresa()
        {
            foreach (int id in _ID_IMPRESE)
            {
                if (id == Numerocasella)
                    return true;
            }

            return false;
        }

        public bool IsParcheggio()
        {
            if (Numerocasella == _ID_PARCHEGGIO)
                return true;
            return false;
        }

        public bool IsInPrigione()
        {
            if (Numerocasella == _ID_IN_PRIGIONE)
                return true;
            return false;
        }
    }
}
