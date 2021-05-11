using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace ProgettoMonopoly
{
    public class Server
    {
        private const int _portaServer = 2021;
        private int _portaClient;
        private Socket _socket;
        private EndPoint _endPointLocale;
        
        private bool _inLobby;
        private bool _inGame;
        private string _errore;
        private int _numeroCartaAssegnato;
        private bool _turnoPedinaPrincipale;

        public Server()
        {
            Socket = new Socket(SocketType.Dgram, ProtocolType.IP); //da sistemare per tcp
            EndPointLocale = new IPEndPoint(IPAddress.Any, 0);

            RicezioneMessaggi();
        }

        public bool InLobby
        {
            get
            {
                return _inLobby;
            }
            private set
            {
                _inLobby = value;
            }
        }

        public bool InGame
        {
            get
            {
                return _inGame;
            }
            private set
            {
                _inGame = value;
            }
        }

        public string Errore
        {
            get
            {
                return _errore;
            }
            private set
            {
                _errore = value;
            }
        }

        public int NumeroCartaAssegnato
        {
            get
            {
                return _numeroCartaAssegnato;
            }
            private set
            {
                _numeroCartaAssegnato = value;
            }
        }

        public bool TurnoPedinaPrincipale
        {
            get
            {
                return _turnoPedinaPrincipale;
            }
            private set
            {
                _turnoPedinaPrincipale = value;
            }
        }

        /*
        public Queue<Turno> Turni
        {
            get
            {
                return _turni;
            }
            set
            {
                _turni = value;
            }
        }
        */

        private int PortaClient
        {
            get
            {
                return _portaClient;
            }
            set
            {
                _portaClient = value;
            }
        }

        private Socket Socket
        {
            get
            {
                return _socket;
            }
            set
            {
                _socket = value;
            }
        }

        private EndPoint EndPointLocale
        {
            get
            {
                return _endPointLocale;
            }
            set
            {
                _endPointLocale = value;
            }
        }

        private async void RicezioneMessaggi()
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    if (Socket.Available != 0)
                    {
                        // Ricezione
                        byte[] dati = new byte[1024];
                        int ricevuto = Socket.ReceiveFrom(dati, ref _endPointLocale);

                        // Decodifica
                        string messaggioRicezione = Encoding.ASCII.GetString(dati, 0, ricevuto);

                        RispostaServer(messaggioRicezione);
                    }
                    Thread.Sleep(1);
                }

            });

        }

        public void InviaMessaggio(string messaggioDaInviare)
        {
            byte[] dati = Encoding.ASCII.GetBytes(messaggioDaInviare);
            
            IPEndPoint endPointRemoto = new IPEndPoint(IPAddress.Parse("*****"), _portaServer); // da inserire server

            Socket.SendTo(dati, endPointRemoto);
        }

        private void RispostaServer(string messaggioRicezione)
        {
            if (messaggioRicezione.Contains("INSERTOK"))
            {
                PortaClient = int.Parse(messaggioRicezione.Split(' ')[1]);
                InLobby = true;
            }
            else if (messaggioRicezione.Contains("STARTGAME"))
            {
                InLobby = false;
                InGame = true;
                //Turni = DeterminaTurniECreaPedine(messaggioRicezione);
            }
            else if (messaggioRicezione.Contains("TURN"))
            {
                NumeroCartaAssegnato = int.Parse(messaggioRicezione.Split(' ')[1]);
                TurnoPedinaPrincipale = true;
            }
            else if (messaggioRicezione.Contains("BANK"))
            {

            }
            else if (messaggioRicezione.Contains("ISMOVE"))
            {
               // Gioco.MuoviPedina(int.Parse(messaggioRicezione.Split(' ')[2]),  messaggioRicezione.Split(' ')[1]);
                //implementa che se finisce su imprevisto o probabilità si vede a schermo la carta pescata.
            }
            else if (messaggioRicezione.Contains("DIED"))
            {
                /*
                if(messaggioRicezione.Split(' ')[1] == Gioco.TurnoAttuale.Pedina.Nome)
                {
                    
                }
                */
            }
            //implementa
        }

        /* TODO CREZIONE PEDINE NON DEL CLIENT
        private Queue<Turno> CreaPedine(string messaggioRicezione)
        {
            Queue<Turno> codaTurni = new Queue<Turno>();

            string[] nomiGiocatori = messaggioRicezione.Split(' ');

            for (int i = 1; i < nomiGiocatori.Length; i++)
            {
                Pedina pedina = new Pedina(nomiGiocatori[i]);
                codaTurni.Enqueue(new Turno(pedina));
            }
            return codaTurni;
        }
        */

        public void EntraInPartita(string nomeGiocatore)
        {
            string richiestaGioco = $"INSERT {nomeGiocatore}";
            InviaMessaggio(richiestaGioco);
        }

        public void MuoviPedina(int sommaDadi)
        {
            string messaggio = $"MOVE {sommaDadi}";
            InviaMessaggio(messaggio);
        }

        public void CompraProprieta(int proprietaComprata)
        {
            string messaggio = $"BUY {proprietaComprata}";
            InviaMessaggio(messaggio);
        }

        public void RifiutaProprieta()
        {
            string messaggio = $"NOBUY";
            InviaMessaggio(messaggio);
        }

        public void IpotecaProprieta(int proprietaIpotecata)
        {
            string messaggio = $"IPOTECA {proprietaIpotecata}";
            InviaMessaggio(messaggio);
        }

        public void NonIpotecare()
        {
            string messaggio = $"NOIPOTECA";
            InviaMessaggio(messaggio);
        }
    }
}