using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace DemoMySQL
{
    public class MySqlDemo
    {
        public MySqlConnection? LaConnexion { get; set; } = null;

        public MySqlDemo()
        {

        }

        public void InitialiserConnexion()
        {
            //Remplacer les éléments par les bonnes valeurs pour l'adresse du serveur, l'utilisateur, le mot de passe et la base de données ciblée
            string connectionString = "server=sql.decinfo-cchic.ca;port=33306;uid=dev-2510465;pwd=Diatta1998;database=h26_devappdebutant_2510465";

            LaConnexion = new MySqlConnection(connectionString);
        }

        public void EffectuerSelect()
        {
            MySqlDataReader? resultatRequete = null;
            try
            {
                //Écrire la requête SQL qu'on veut exécuter:
                string requete = "SELECT * FROM table";

                MySqlCommand laCommande = new MySqlCommand(requete, LaConnexion);

                //Choisir le bon mode d'exécution selon le type de procédure qu'on appelle
                resultatRequete = laCommande.ExecuteReader(); //Pour exécuter des procédures qui retournent une ou plusieurs lignes (SELECT)

                while (resultatRequete.Read())
                {

                    /* 
                     * Exemples de lecture de chaque enregistrement et extraire la valeur en fonction du nom de champs ou par la position dans l'ordre des champs du SELECT:
                     * int id = resultatRequete.GetInt16("id");
                     * int? valeurChamp1 = resultatRequete.IsDBNull("champ1") ? null : resultatRequete.GetInt32("champ1");
                     * string valeurChamp2 = resultatRequete.IsDBNull("champ2") ? "" : resultatRequete.GetString("champ2");
                     * string valeurChampNo1 = resultatRequete.GetString(1);
                     */

                    //Affichage ou autres traitements à ajouter en fonction des informations que j'ai reçues
                }
            }
            catch (MySqlException sqlEx)
            {
                Console.WriteLine(sqlEx.ToString());
            }
            finally
            {
                if (resultatRequete != null)
                {
                    resultatRequete.Close();
                }
                if (LaConnexion != null)
                {
                    LaConnexion.Close();
                }
            }
        }

        public void EffectuerInsert()
        {
            try
            {
                //Écrire la requête SQL qu'on veut exécuter:
                string requete = "insert into table (champ1, champ2, champ3) values (1001, 'test', null);";

                MySqlCommand laCommande = new MySqlCommand(requete, LaConnexion);

                //Choisir le bon mode d'exécution selon le type de procédure qu'on appelle
                int nbLignesAffectees = laCommande.ExecuteNonQuery(); //Pour exécuter des procédures qui ne retournent rien (INSERT, UPDATE, DELETE)
            }
            catch (MySqlException sqlEx)
            {
                Console.WriteLine(sqlEx.ToString());
            }
            finally
            {
                if (LaConnexion != null)
                {
                    LaConnexion.Close();
                }
            }
        }

        public void AppelerProcedureStockeeIN()
        {
            try
            {
                //Écrire le nom de la procédure stockée qu'on veut exécuter:
                string requete = "spNomDeLaProcedure";

                MySqlCommand laCommande = new MySqlCommand(requete, LaConnexion);

                laCommande.CommandType = System.Data.CommandType.StoredProcedure;

                //Fournir les paramètres d'entrées selon la procédure stockée appelée.
                laCommande.Parameters.AddWithValue("@enseignantID", 4);

                //Choisir le bon mode d'exécution selon le type de procédure qu'on appelle
                MySqlDataReader resultatRequete = laCommande.ExecuteReader();  //Pour exécuter des procédures qui retournent une ou plusieurs lignes (Select)
                                                                               //int resultatRequete = laCommande.ExecuteScalar(); //Pour exécuter des procédures qui retournent une seule valeur (Count, Max)
                
                while (resultatRequete.Read())
                {
                    //Traiter les données selon la méthode exécutée
                }
            }
            catch (MySqlException sqlEx)
            {
                Console.WriteLine(sqlEx.ToString());
            }
            finally
            {
                if (LaConnexion != null)
                {
                    LaConnexion.Close();
                }
            }
        }

        public void AppelerProcedureStockeeINOUT()
        {
            try
            {
                //Écrire la requête SQL qu'on veut exécuter
                string requete = "spNomDeLaProcedure";

                MySqlCommand laCommande = new MySqlCommand(requete, LaConnexion);
                laCommande.CommandType = System.Data.CommandType.StoredProcedure;

                //Paramètre d'entrée
                laCommande.Parameters.Add("@nomParametreEntree", MySqlDbType.VarChar);
                laCommande.Parameters["@nomParametreEntree"].Direction = System.Data.ParameterDirection.Input;
                laCommande.Parameters["@nomParametreEntree"].Value = "Valeur";

                //Paramètre de sortie
                laCommande.Parameters.Add("@nomParametreSortie", MySqlDbType.Int32);
                laCommande.Parameters["@nomParametreSortie"].Direction = System.Data.ParameterDirection.Output;

                laCommande.ExecuteNonQuery(); //Exécuter la procédure

                //Lire la valeur du paramètre de sortie
                int nbEnseignants = Convert.ToInt32(laCommande.Parameters["@nbrEnseignant"].Value);

                //Affichage ou autres traitements à ajouter en fonction de l'information que j'ai reçue
            }
            catch (MySqlException sqlEx)
            {
                Console.WriteLine(sqlEx.ToString());
            }
            finally
            {
                if (LaConnexion != null)
                {
                    LaConnexion.Close();
                }
            }
        }

        public void VerifierConnexionBD()
        {
            if (LaConnexion == null)
            {
                throw new Exception("La connexion est nulle, veuillez initialiser la connexion au serveur MySQL avant.");
            }
            else
            {
                try
                {
                    LaConnexion.Open();
                }
                catch (MySqlException sqlEx)
                {
                    Console.WriteLine(sqlEx.ToString());
                    throw new Exception("La connexion est nulle, veuillez vérifier pourquoi la connexion au serveur MySQL n'a pas fonctionné.");
                }
            }
        }
    }
}
