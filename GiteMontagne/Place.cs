using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiteMontagne
{
    class Place
    {
        private string _Nom;
        private int _id; 
        private List<Place> _places;
        private List<Lit> _lits;
        private string _salle;

        public Place()
        {

        }

        public Place(List<Lit> lits, string salle)
        {
            _lits = lits;
            _salle = salle;
        }


        public Place(string Nom, int id)
        {
            this.Nom = Nom;
            this.Id = id;
        }

        public string Nom { get => _Nom; set => _Nom = value; }
        
        public int Id { get => _id; set => _id = value; }
        public List<Place> Places { get => _places; set => _places = value; }
        public string Salle { get => _salle; set => _salle = value; }
        internal List<Lit> Lits { get => _lits; set => _lits = value; }

        public override string ToString()
        {
            return $"{Id} {Nom} "; 
        }

    }


    class Lit
    {

            private int _litId;
            private string _litNom;
            private DateTime _dateArrivee;
            private DateTime _dateDepart;
            private int _duree;
            private int _nombrePlace;
          //  private bool _disponibilite;
        public bool Disponibilite { get; set; }

        public Lit()
        {

        }
        public Lit(int litId, string litNom)
        {
            LitId = litId;
            LitNom = litNom;
        }

        public Lit(int nombrePlace, bool disponibilite)
        {
            this.NombrePlace = nombrePlace;
            this.Disponibilite = disponibilite;
        }
        public Lit(DateTime dateArrivee, DateTime dateDepart, int duree)
        {
            DateArrivee = dateArrivee;
            DateDepart = dateDepart;
            Duree = duree;
        }

        public int LitId { get => _litId; set => _litId = value; }
        public string LitNom { get => _litNom; set => _litNom = value; }
        public int NombrePlace { get => _nombrePlace; set => _nombrePlace = value; }
        //public bool Disponibilite { get => _disponibilite; set => _disponibilite = value; }
        public DateTime DateArrivee { get => _dateArrivee; set => _dateArrivee = value; }
        public DateTime DateDepart { get => _dateDepart; set => _dateDepart = value; }
        public int Duree { get => _duree; set => _duree = value; }

        public override string ToString()
        {
            return $" {LitId} {LitNom} {(Disponibilite? "libre" : "occupée") }";
        }
    }
}
