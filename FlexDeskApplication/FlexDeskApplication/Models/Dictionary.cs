using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class Dictionary
    {
        public string Label1 { get; set; }
        public string Label2 { get; set; }
        public string Label3 { get; set; }
        public string Label4 { get; set; }
        public string Label5 { get; set; }
        public string Label6 { get; set; }
        public string Label7 { get; set; }
        public string Label8 { get; set; }
        public string Label9 { get; set; }
        public string Label10 { get; set; }
        public string Label11 { get; set; }
        public string Label12 { get; set; }
        public string Label13 { get; set; }
        public string Label14 { get; set; }
        public string Label15 { get; set; }
        public string Label16 { get; set; }
        public string Label17 { get; set; }
        public string Label18 { get; set; }
        public string Label19 { get; set; }
        public string Label20 { get; set; }
        public string Label21 { get; set; }
        public string Label22 { get; set; }
        public string Label23 { get; set; }
        public string Label24 { get; set; }
        public string Label25 { get; set; }
        public string Label26 { get; set; }
        public string Label27 { get; set; }
        public string Label28 { get; set; }

        public Dictionary(int? language)
        {
            if (language == 0 || language == null)
            {
                Label1 = "Vind de desks voor";
                Label2 = "Begindatum";
                Label3 = "Einddatum";
                Label4 = "Lijst reservaties bekijken";
                Label5 = "Verwijderen";
                Label6 = "Paswoord";
                Label7 = "Inloggen";
                Label8 = "Afwezigheden";
                Label9 = "Aanmaken";
                Label10 = "Commentaar";
                Label11 = "Afwezigheid toevoegen";
                Label12 = "Reservaties";
                Label13 = "Reserveer een desk voor";
                Label14 = "van";
                Label15 = "tot";
                Label16 = "Ben je zeker dat je dit wilt verwijderen?";
                Label17 = "Afwezigheid";
                Label18 = "Terug naar de lijst";
                Label19 = "Reservatie";
                Label20 = "van deze";
                Label21 = "U heeft een reservatie in deze periode. U moet deze eerst annuleren.";
                Label22 = "De einddatum  mag niet kleiner zijn dan de startdatum.";
                Label23 = "U kan maar één week tegelijk reserveren.";
                Label24 = "U kan maar reserveren tot ";
                Label25 = "U heeft al een reservatie.";
                Label26 = "Aangemaakt door";
                Label27 = "Datum aanmaak";
                Label28 = "Nieuwe reservatie aangemaakt";
            }
            else
            {
                Label1 = "Trouvez les desks pour";
                Label2 = "Date de début";
                Label3 = "Date de fin";
                Label4 = "Voir la liste des réservations";
                Label5 = "Supprimer";
                Label6 = "Mot de passe";
                Label7 = "Connecter";
                Label8 = "Absences";
                Label9 = "Créer";
                Label10 = "Commentaire";
                Label11 = "Ajouter une absence";
                Label12 = "Réservations";
                Label13 = "Réserver un desk pour";
                Label14 = "du";
                Label15 = "au";
                Label16 = "Vous êtes sur que vous voulez supprimer ceci?";
                Label17 = "Absence";
                Label18 = "Retourner à la liste";
                Label19 = "Réservation";
                Label20 = "de ce";
                Label21 = "Vous avez déjà une réservation dans cette période. Il faut l'annuler.";
                Label22 = "Le date de fin peut pas être avant le date de début.";
                Label23 = "Vous ne pouvez que réserver pour une semaine.";
                Label24 = "Vous ne pouvez que réserver jusqu'au ";
                Label25 = "Vous avez déjà une réservation";
                Label26 = "Créé par";
                Label27 = "Date de création";
                Label28 = "Nouvelle réservation créée";
            }
        }
    }
}
