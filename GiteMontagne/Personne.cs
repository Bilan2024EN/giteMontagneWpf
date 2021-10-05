using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiteMontagne
{
    class Client
    {
        private int _id;
        private string _nom;
        private string _adresse;
        private int _cp;
        private string _ville;
        private int _telephone;
        private DateTime _dateArrivee;
        private DateTime _dateDepart;
        private int _duree;
        private int _nombrePersonnes;
        private List<Client> clients;


        public Client()
        {

        }

        public Client(int nombrePersonnes)
        {
            NombrePersonnes = nombrePersonnes;
        }

        public Client(List<Client> clients)
        {
            this.Clients = clients;
        }

        public Client(DateTime dateArrivee, int duree)
        {
            DateArrivee = dateArrivee;
           
            Duree = duree;
        }
        public Client(DateTime dateDepart)
        {
            DateDepart = dateDepart;
        }

        public Client(int id, string nom, string adresse, int cp, string ville, int telephone)
        {
            _id = id;
            _nom = nom;
            _adresse = adresse;
            _cp = cp;
            _ville = ville;
            _telephone = telephone;
        }

        public int Id { get => _id; set => _id = value; }
        public string Nom { get => _nom; set => _nom = value; }
        public string Adresse { get => _adresse; set => _adresse = value; }
        public int Cp { get => _cp; set => _cp = value; }
        public string Ville { get => _ville; set => _ville = value; }
        public int Telephone { get => _telephone; set => _telephone = value; }
        public DateTime DateArrivee { get => _dateArrivee; set => _dateArrivee = value; }
        public int Duree { get => _duree; set => _duree = value; }
        public DateTime DateDepart { get => _dateDepart;
                                     set => _dateDepart = value; }
        public int NombrePersonnes { get => _nombrePersonnes; set => _nombrePersonnes = value; }
        internal List<Client> Clients { get => clients; set => clients = value; }





        public override string ToString()
        {
            return $"{Id} {Nom}, Nombre de personnes : {NombrePersonnes} \n" +
                $"Date : {DateArrivee} / {DateDepart}";
        }
    } 
}

