using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ProgettoMonopoly
{
    public class Gioco
    {
        private Pedina _pedinaPrincipale;
        private Server _server;
        private Tabellone _tabellone;
        private List<Pedina> _listaPedine;
        private Turno _turnoAttuale;
        private Queue<Turno> _listaTurni;

        public Gioco(Tabellone tabellone, Queue<Turno> listaTurni)
        {
            // Setup(listaTurni);
        }

        public Gioco(Tabellone tabellone, Server server)
        {
            Tabellone = tabellone;
            Server = server;
        }

        public bool TurnoPedinaPrincipale
        {
            get
            {
                return Server.TurnoPedinaPrincipale;
            }
        }

        public bool InLobby
        {
            get
            {
                return Server.InLobby;
            }
        }

        public bool InGame
        {
            get
            {
                return Server.InGame;
            }
        }

        public string Errore
        {
            get
            {
                return Server.Errore;
            }
        }

        public Pedina PedinaPrincipale
        {
            get
            {
                return _pedinaPrincipale;
            }
            set
            {
                _pedinaPrincipale = value;
            }
        }

        private Server Server
        {
            get
            {
                return _server;
            }
            set
            {
                _server = value;
            }
        }

        private Tabellone Tabellone
        {
            get
            {
                return _tabellone;
            }
            set
            {
                _tabellone = value;
            }
        }

        public List<Pedina> ListaPedine
        {
            get
            {
                return _listaPedine;
            }
            private set
            {
                _listaPedine = value;
            }
        }

        public void EntraInLobby(string nomeGiocatore)
        {
            Server.EntraInPartita(nomeGiocatore);
        }

        /* setup partita 
        private void Setup(Queue<Turno> listaTurni)
        {
            ListaTurni = listaTurni;
            foreach (Turno turno in ListaTurni)
            {
                turno.Pedina.Posizione = Tabellone.GetCasella(0);
                Banca.DistribuisciDenaroIniziale(turno.Pedina);
            }

        }
        */
        public Casella MuoviPedina(int sommaDadi, string nomeGiocatore)
        {
            if (nomeGiocatore == PedinaPrincipale.Nome)
            {
                if (ListaPedine.Contains(PedinaPrincipale))
                {
                    if (!PedinaPrincipale.PedinaInPrigione)
                    {
                        int posizioneAttualePedina = PedinaPrincipale.Posizione.Numerocasella;

                        PedinaPrincipale.Posizione = Tabellone.GetCasella(posizioneAttualePedina + sommaDadi);

                        if (PedinaPrincipale.Posizione.Numerocasella < posizioneAttualePedina)
                        {
                            PedinaPrincipale.DenaroPedina += (Tabellone.GetCasella(0) as Via).PassaggioDalVia;
                        }

                        Server.MuoviPedina(sommaDadi);

                        return PedinaPrincipale.Posizione;
                    }
                    else
                    {
                        return PedinaPrincipale.Posizione; //implementa prigione
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            else
            {
                foreach (Pedina pedina in ListaPedine)
                {
                    if (pedina.Nome == nomeGiocatore)
                    {
                        int posizioneAttualePedina = pedina.Posizione.Numerocasella;
                        pedina.Posizione = Tabellone.GetCasella(posizioneAttualePedina + sommaDadi);

                        return pedina.Posizione;
                    }
                }
                throw new Exception();
            }
        }

        public void CompraProprieta()
        {
            if (PedinaPrincipale.Posizione is Proprieta && (PedinaPrincipale.Posizione as Proprieta).Comprata == false)
            {
                PedinaPrincipale.DenaroPedina -= (PedinaPrincipale.Posizione as Proprieta).Contratto.ValoreContratto;
                PedinaPrincipale.ListaProprieta.Add(PedinaPrincipale.Posizione as Proprieta);

                Server.CompraProprieta(PedinaPrincipale.Posizione.Numerocasella);
            }
            else
            {
                throw new Exception();
            }
        }

        public void RifiutaProprieta()
        {
            Server.RifiutaProprieta();
        }

        public void Fallisci(string nomeGiocatore)
        {
            
        }

        /* rifiuta proprieta e apri asta
        public Asta RifiutaProprieta()
        {
            Asta asta = new Asta(TurnoAttuale.Pedina.Posizione as Proprieta, ListaPedine);
            return asta;
        }
        */
        public void PagaAffitto()
        {
            int affitto = (PedinaPrincipale.Posizione as Proprieta).Contratto.Rendita[(PedinaPrincipale.Posizione as Proprieta).LivelloProprieta];
            PedinaPrincipale.DenaroPedina -= affitto;
            (PedinaPrincipale.Posizione as Proprieta).Proprietario.DenaroPedina += affitto;
        }

        /* TODO da fare che si miglira la proprietà solo nel proprio turno
        public async void MiglioraProprieta(Proprieta proprieta) // solo strada
        {
            await Task.Run(() =>
            {
                if (proprieta.LivelloProprieta == 0)
                {
                    if (proprieta is Strada)
                    {
                        int i = 0;
                        foreach (Proprieta prop in (proprieta as Strada).Distretto.ListaStrade)
                        {
                            if (proprieta.Proprietario.ListaProprieta.Contains(prop))
                            {
                                i++;
                            }
                        }

                        if (i == (proprieta as Strada).Distretto.ListaStrade.Count)
                        {
                            proprieta.LivelloProprieta++; //migliora anche livello delle altre due proprietà
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }

                }
                else if (proprieta.LivelloProprieta > 0 && proprieta.LivelloProprieta < proprieta.Contratto.Rendita.Count) // e le altre due porprietà sono al tuo stesso livello o al massimo uno sotto
                {
                    proprieta.Proprietario.DenaroPedina -= (proprieta.Contratto as ContrattoStrada).CostoPerCasa;
                    Banca.DenaroBanca += (proprieta.Contratto as ContrattoStrada).CostoPerCasa;
                    proprieta.LivelloProprieta++;
                }
            });

            
        }
        */

        /* paga tassa
        public void PagaTassa()
        {
            TurnoAttuale.Pedina.DenaroPedina -= (TurnoAttuale.Pedina.Posizione as Tassa).Costo;
            Banca.DenaroBanca += (TurnoAttuale.Pedina.Posizione as Tassa).Costo;
        }
        */
        public void Ipoteca(List<Proprieta> proprieta)
        {
            //chiamato quando si sceglie di ipotecare
            foreach (Proprieta item in proprieta)
            {
                PedinaPrincipale.ListaProprieta.Remove(item);
                PedinaPrincipale.DenaroPedina += item.Contratto.ValoreIpotecato;
                Server.IpotecaProprieta(item.Numerocasella);
            } 
        }

        public void NonIpoteca()
        {
            Server.NonIpotecare();
        }

        
        
       
    }
}