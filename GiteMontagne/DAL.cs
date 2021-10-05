using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiteMontagne
{
    class DAL
    {
        public static string sqlConnexion = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\GiteMontagne_v13\GiteMontagne\LogicielResa.mdf;Integrated Security=True";
        public static void EnregistrerClient( string nom, int telephone, string adresse, int cp, string ville , int nombrePersonnes, DateTime dateArrivee, int duree, DateTime dateDepart)
        {
            Client client = new Client();
            string sqlQuery = $"INSERT INTO Reservations (DateArrivee, Duree, DateDepart) VALUES ('{dateArrivee.ToString("MM/dd/yyyy")}',{duree}, DATEADD(DD,{duree},'{dateDepart.ToString("MM/dd/yyyy")}') as MaDate = '{dateArrivee.AddDays(duree).ToString("MM/dd/yyyy")}'" +
                $"INSERT INTO Clients (Nom, Telephone, NombrePersonnes) VALUES ('{nom}', {telephone}, {nombrePersonnes})" +
                //$"INSERT INTO Reserver (Disponibilite) VALUES ('{False}')" +
            $"INSERT INTO Adresses (Adresse, CP, Ville) VALUES ('{adresse}', {cp}, '{ville}')";

            SqlConnection connection = new SqlConnection(sqlConnexion);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);

            command.ExecuteNonQuery();
        }

        public static void SupprimerClient(int unId)
        {
            Client client = new Client();
            string sqlQuery = $"Delete from Clients WHERE Id= {unId}" +
               $"Delete from Reserver WHERE Client_Id= {unId}" +
               $"Delete from Lits WHERE Client_Id= {unId}";


            SqlConnection connection = new SqlConnection(sqlConnexion);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);

            command.ExecuteNonQuery();
        }


        public static void UpdateClient(DateTime dateArrivee, int duree, int litId, string nom, int telephone, string adresse, int cp, string ville, int nombrePersonnes)
        {
            Client client = new Client();
            string sqlQuery = $"UPDATE Reservations SET DateArrivee = '{dateArrivee.ToString("MM/dd/yyyy")}', Duree = {duree} WHERE DateArrivee = '{dateArrivee.ToString("MM/dd/yyyy")}' OR Duree = {duree}" +
                $"UPDATE Clients SET NOM = '{nom}', Telephone = {telephone}, NombrePersonnes = {nombrePersonnes} WHERE Nom = '{nom}' OR Telephone = {telephone} OR NombrePersonnes = {nombrePersonnes}" +
                $"UPDATE Reserver SET Disponibilite = 'false' WHERE Disponibilite = 'false'" +
            $"UPDATE Adresses SET Adresse = '{adresse}', CP = {cp}, Ville = '{ville}' WHERE Adresse = '{adresse}' OR CP = {cp} OR Ville = '{ville}'";

            SqlConnection connection = new SqlConnection(sqlConnexion);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);

            command.ExecuteNonQuery();
        }

        public static List<Place> getPlaces()
        {
            List<Place> places = new List<Place>();
            string sqlQuery = $"SELECT Id, Chambre_Nom from Places";

            SqlConnection connection = new SqlConnection(sqlConnexion);

            connection.Open();

            SqlCommand command = new SqlCommand(sqlQuery, connection);

            SqlDataReader dataReader = command.ExecuteReader();

            Place salle = null;

            //load into the result object the returned row from the database
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    salle = new Place();

                    salle.Id = Convert.ToInt32(dataReader["Id"].ToString());
                    salle.Nom = dataReader["Chambre_Nom"].ToString();
                    //lit.NombrePlace = Convert.ToInt32(dataReader["NombrePlaces"].ToString());


                    places.Add(salle);
                }
            }

            return places;
        }

        public static List<Lit> getLits(int place_Id)
        {
            List<Lit> lits = new List<Lit>();
            string sqlQuery = $"SELECT distinct l.Id, l.Lit_Nom, re.Disponibilite from Lits l INNER JOIN Reservations r ON l.Place_Id = r.Place_Id INNER JOIN Reserver re ON re.Lit_Id = l.Id WHERE l.Place_Id = {place_Id}";
            //  SELECT Id, Lit_Nom from Lits 

            SqlConnection connection = new SqlConnection(sqlConnexion);

            connection.Open();

            SqlCommand command = new SqlCommand(sqlQuery, connection);

            SqlDataReader dataReader = command.ExecuteReader();

            Place salle = null;
            Lit lit = null;
            

            //load into the result object the returned row from the database
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    salle = new Place();
                    //lit.NombrePlace = Convert.ToInt32(dataReader["NombrePlaces"].ToString());
                    lit = new Lit();
                    lit.LitId = Convert.ToInt32(dataReader["Id"].ToString());
                    lit.LitNom = dataReader["Lit_Nom"].ToString();
                    lit.Disponibilite = Convert.ToBoolean(dataReader["Disponibilite"].ToString());
                    //lit.DateArrivee = DateTime.Parse(dataReader["dateArrivee"].ToString());
                    //lit.Duree = Convert.ToInt32(dataReader["duree"].ToString());
                    //salle = new Place();
                    //salle.Id = place_Id;
                    //salle.Id = Convert.ToInt32(dataReader["Place_Id"].ToString());

                    lits.Add(lit);
                    
                }

               
            }

            return lits;
        }

        public static List<Lit> afficherPlacesDates(DateTime dateArrivee, int duree)
        {
            List<Lit> result = new List<Lit>();
            string sqlQuery = $"Select distinct l.Id, l.Lit_Nom from Lits l inner join Reservations r On r.Id = l.Reservation_Id where not exists (select * from Reservations where r.Id = l.Reservation_Id AND r.DateArrivee BETWEEN {dateArrivee.ToString("MM/dd/yyyy")} AND DATEADD(DD, {duree}, r.DateArrivee) as 'maDate' = '{dateArrivee.AddDays(duree).ToString("MM /dd/yyyy")}'";
            //"SELECT distinct l.Id, l.Lit_Nom from Lits l INNER JOIN Reservations r ON l.Place_Id = r.Place_Id WHERE '" + dateArrivee.ToString("MM / dd / yyyy") + "'  NOT BETWEEN r.DateArrivee AND DATEADD(DD,r.Duree, r.DateArrivee) AND DATEADD(DD,"+ duree + ", r.DateArrivee) = '" + dateArrivee.AddDays(duree).ToString() + "'";
            //"Select Count(p.NombrePlaces) from Places p INNER JOIN Reservations r ON p.Id = r.Place_Id WHERE p.NombrePlaces NOT BETWEEN r.DateArrivee AND DateAdd(day, r.Duree, @r.DateArrivee)"
            //$"SELECT d.* from Places d LEFT JOIN Reservations r ON r.Id = d.Reservation_Id Where litDortoir = '{litDortoir}' AND NombrePlaces = '{nombrePlace}'" +
            //    $"AND DateArrivee = '{dateArrivee}' AND Duree = '{duree}'"
            ;

            SqlConnection connection = new SqlConnection(sqlConnexion);

            connection.Open();

            SqlCommand command = new SqlCommand(sqlQuery, connection);

            SqlDataReader dataReader = command.ExecuteReader();

            //Place salle = null;
            Lit lit = null;


            //load into the result object the returned row from the database
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    //salle = new Place();
                    //lit.NombrePlace = Convert.ToInt32(dataReader["NombrePlaces"].ToString());
                    lit = new Lit();
                    lit.LitId = Convert.ToInt32(dataReader["Id"].ToString());
                    lit.LitNom = dataReader["Lit_Nom"].ToString();
                    lit.DateArrivee = DateTime.Parse(dataReader["dateArrivee"].ToString());
                    lit.Duree = Convert.ToInt32(dataReader["duree"].ToString());
                    //salle = new Place();
                    //salle.Id = place_Id;
                    //salle.Id = Convert.ToInt32(dataReader["Place_Id"].ToString());

                    result.Add(lit);

                }


            }

            return result;
        }

        public static List<Client> retournerClients(int Id)
        {
            List<Client> clients = new List<Client>();
            string sqlQuery = $"SELECT c.Id, c.Nom, c.NombrePersonnes, ro.DateArrivee, ro.DateDepart from Clients c INNER JOIN Reserver r ON c.Id = r.Client_Id  INNER JOIN Reservations ro  ON ro.Id = r.Reservation_Id " +
                $"WHERE r.Lit_Id =" + Id;
            
            SqlConnection connection = new SqlConnection(sqlConnexion);

            connection.Open();

            SqlCommand command = new SqlCommand(sqlQuery, connection);

            SqlDataReader dataReader = command.ExecuteReader();

            Client client = null;


            //load into the result object the returned row from the database
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    client = new Client();
                    client.Id = Convert.ToInt32(dataReader["Id"].ToString());
                    client.Nom = dataReader["Nom"].ToString();
                    client.NombrePersonnes = Convert.ToInt32(dataReader["NombrePersonnes"].ToString());
                    client.DateArrivee = DateTime.Parse(dataReader["DateArrivee"].ToString());
                    //client.Duree = Convert.ToInt32(dataReader["Duree"].ToString());
                    //client.DateDepart = DateTime.Parse(dataReader["DateArrivee"].ToString());
                    client.DateDepart = DateTime.Parse(dataReader["DateDepart"].ToString());
                    clients.Add(client);

                }
               

            }

            return clients;
        }

        //public static void RemplirBase(string nom, int telephone, string adresse, int cp, string ville)
        //{
        //    Client client = new Client();
        //    string sqlQuery = $"Inser c.Id, c.Telephone, a.Adresse, a.Cp, a.Ville from Clients c INNER JOIN Adresses a ON c.Adresse_Id = a.Id WHERE c.Id =" + Id;

        //    string sqlQuery = $"Select c.Nom, c.Telephone, a.Adresse, a.Cp, a.Ville from Clients c INNER JOIN Adresses a ON c.Adresse_Id = a.Id Where c.Nom = '{nom}AND c.Telephone = {telephone}'AND " +
        //        $"a.Adresse = '{adresse}'AND a.Cp = {cp} AND a.Ville = '{ville}'";

        //    SqlConnection connection = new SqlConnection(sqlConnexion);
        //    connection.Open();
        //    SqlCommand command = new SqlCommand(sqlQuery, connection);

        //    command.ExecuteNonQuery();
        //}

    }

}
