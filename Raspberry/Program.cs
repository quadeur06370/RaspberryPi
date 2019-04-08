using System;
using System.Text.RegularExpressions;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit;
using MimeKit; //librairie pour l'utilisation du mail
using System.Net;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using System.Collections;

namespace Raspberry
{
    class Program
    {
        private static IMailFolder inbox;



        public static void Main(string[] args)
        {

            Client connection = new Client(); // crée une instance de la classe

            var logger = new ProtocolLogger(Console.OpenStandardError()); //protocol pour acceder aux informations necessaires pour la boite mail

            using (var client = new ImapClient())
            {
                var credentials = new NetworkCredential("xxx@gmail.com", "xxx"); // connexion à ma boite mail
                var uri = new Uri("imaps://imap.gmail.com"); // en imap , car cela permet l'utilisation sur plusieurs ordinateurs en même temps ( synchronisation)
                // beaucoup plus rapide car ne telecharge que l'entete en premier

                client.Connect(uri);

                client.AuthenticationMechanisms.Remove("XOAUTH2"); // autorisation pour se connecter

                client.Authenticate(credentials);
                inbox = client.Inbox; // se connecte à la boite de reception
                inbox.Open(FolderAccess.ReadOnly); // seulement la possibilité de lire les messages

                var query = SearchQuery.FromContains("no-reply@appveyor.com"); // variable query qui cherche l'expediteur 
                foreach (var uid in inbox.Search(query))
                {
                    var message = inbox.GetMessage(uid); // la variable message qui prends en parametre uid qui contient query 
                    var date = message.Date; // une variable date qui prends la date du message
                    Console.WriteLine("la date du message est : {0}", date); // affichage dans la console

                    Regex regex1 = new Regex("completed|failed"); // regex pour prendre deux mots | -> signifie "ou"
                    Match build = regex1.Match(message.HtmlBody); // prends le message en format html
                    Console.WriteLine("le build est : {0}", build); // affichage console
                    connection.send(build.ToString() + " " + date.ToString()); // appel la methode send


                    Console.WriteLine("Appuyez pour le mail suivant");
                    Console.ReadKey();

                }

            }



            Console.WriteLine("Press any key to exit.");


        }
    }
}




