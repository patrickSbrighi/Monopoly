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
        private Server _server;
        private const int _passaggioDalVia = 200;
        private Tabellone _tabellone;
        private List<Pedina> _listaPedine;
        private Banca _banca;
        private Turno _turnoAttuale;
        private Queue<Turno> _listaTurni;

        public Gioco(Tabellone tabellone, Queue<Turno> listaTurni)
        {
            // Setup(listaTurni);
        }

        public Server Server
        {
            get
            {
                return _server;
            }
            private set
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

        private Banca Banca
        {
            get
            {
                return _banca;
            }
            set
            {
                _banca = value;
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

        public Turno TurnoAttuale
        {
            get
            {
                return ListaTurni.Peek();
            }
            set
            {
                _turnoAttuale = value;
            }
        }

        public Queue<Turno> ListaTurni
        {
            get
            {
                return _listaTurni;
            }
            set
            {
                _listaTurni = value;
            }
        }

        /*
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
        public void MuoviPedina(int sommaDadi)
        {
            if (ListaPedine.Contains(TurnoAttuale.Pedina))
            {
                if (!TurnoAttuale.Pedina.PedinaInPrigione)
                {
                    int posizioneAttualePedina = TurnoAttuale.Pedina.Posizione.Numerocasella;

                    TurnoAttuale.Pedina.Posizione = Tabellone.GetCasella(posizioneAttualePedina + sommaDadi);

                    if (TurnoAttuale.Pedina.Posizione.Numerocasella < posizioneAttualePedina)
                    {
                        Banca.PagaPassaggioDalVia(TurnoAttuale.Pedina, _passaggioDalVia);
                    }
                }
                else
                {

                }


            }
            else
            {
                throw new Exception();
            }
        }

        public void CompraProprieta()
        {
            Banca.VendiProprietaAPedina(TurnoAttuale.Pedina);
        }

        public Asta RifiutaProprieta()
        {
            Asta asta = new Asta(TurnoAttuale.Pedina.Posizione as Proprieta, ListaPedine);
            return asta;
        }

        public void PagaAffitto()
        {
            int affitto = (TurnoAttuale.Pedina.Posizione as Proprieta).Contratto.Rendita[(TurnoAttuale.Pedina.Posizione as Proprieta).LivelloProprieta];
            TurnoAttuale.Pedina.DenaroPedina -= affitto;
            (TurnoAttuale.Pedina.Posizione as Proprieta).Proprietario.DenaroPedina += affitto;
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

        public void PagaTassa()
        {
            TurnoAttuale.Pedina.DenaroPedina -= (TurnoAttuale.Pedina.Posizione as Tassa).Costo;
            Banca.DenaroBanca += (TurnoAttuale.Pedina.Posizione as Tassa).Costo;
        }

        public void Ipoteca(Proprieta proprieta)
        {
            //chiamato quando si sceglie di ipotecare
            string messaggio = $"IPOTECA {proprieta.Numerocasella}";
            TurnoAttuale.Pedina.ListaProprieta.Remove(proprieta);
            Server.InviaMessaggio(messaggio);
        }

        public void NonIpoteca()
        {
            string messaggio = $"NOIPOTECA";
            Server.InviaMessaggio(messaggio);
        }

        public void CambiaTurno()
        {
            ListaTurni.Enqueue(TurnoAttuale);
            ListaTurni.Dequeue();
        }

       
    }
}