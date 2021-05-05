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
        private Gioco _gioco;
        private const int _portaServer = 2021;
        private int _portaClient;
        private Socket _socket;
        private EndPoint _endPointLocale;
        
        private bool _inLobby;
        private bool _inGame;
        private string _errore;
        private int _numeroCartaAssegnato;
        private Queue<Turno> _turni;

        public Server()
        {
            Socket = new Socket(SocketType.Dgram, ProtocolType.IP); //da sistemare per tcp
            EndPointLocale = new IPEndPoint(IPAddress.Any, 0);
        }

        public Gioco Gioco
        {
            get
            {
                return _gioco;
            }
            private set
            {
                _gioco = value;
            }
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

        private int NumeroCartaAssegnato
        {
            get
            {
                return _numeroCartaAssegnato;
            }
            set
            {
                _numeroCartaAssegnato = value;
            }
        }

        public Queue<Turno> Turni
        {
            get
            {
                return _turni;
            }
            private set
            {
                _turni = value;
            }
        }

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

        public Socket Socket
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

        public EndPoint EndPointLocale
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
                //implementa
                InLobby = false;
                InGame = true;
                //Turni = DeterminaTurniECreaPedine(messaggioRicezione);
            }
            else if (messaggioRicezione.Contains("TURN"))
            {
                NumeroCartaAssegnato = int.Parse(messaggioRicezione.Split(' ')[1]);
            }
            else if (messaggioRicezione.Contains("BANK"))
            {

            }
            else if (messaggioRicezione.Contains("ISMOVE"))
            {

            }
            else if (messaggioRicezione.Contains("DIED"))
            {
                if(messaggioRicezione.Split(' ')[1] == Gioco.TurnoAttuale.Pedina.Nome)
                {
                    
                }
            }
            //implementa
        }

        /*
        private Queue<Turno> DeterminaTurniECreaPedine(string messaggioRicezione)
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

    }
}