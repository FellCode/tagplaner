using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    /// <summary>
    /// Klasse für Nachrichten wie zum Beispiel Informationen oder Fehlermeldungen
    /// alle Nachrichten sind static, es wir keine Instanz dieser Klasse benötigt
    /// </summary>
    public class MMessage
    {
        private MMessage() { }
        /// <summary>
        /// Bestätigungsabfrage: Möchten Sie die aktuellen Änderungen speichern?
        /// </summary>
        public static string CONFIRMATION_FILE_SAVE = "Möchten Sie die aktuellen Änderungen speichern?";
        
        /// <summary>
        /// Warnung: Datei konnte nicht geöffnet werden!
        /// </summary>
        public static string WARNING_FILE_NOT_FOUND = "Datei konnte nicht geöffnet werden!";

        /// <summary>
        /// Warnung: Keine Klasse in Jahrgang gewählt!
        /// </summary>
        public static string WARNING_NO_CLASSES_CHOOSEN = "Keine Klasse in Jahrgang gewählt!";

        /// <summary>
        /// Warnung: Keine Klassenbezeichnung angegeben!
        /// </summary>
        public static string WARNING_NO_IDENTIFICATION_SET = "Keine Klassenbezeichnung angegeben!";

        /// <summary>
        /// Fehler: Keine Klasse in Jahrgang gewählt!
        /// </summary>
        public static string ERROR_STARTDATE_BIGGER_ENDDATE = "Das Anfangsdatum liegt vor dem Enddatum!";

        /// <summary>
        /// Fehler: Datei  enthält keine Einträge zwischen Start- oder Enddatum!
        /// </summary>
        public static string ERROR_NO_ENTRIES_BETWEEN_DATES = "Datei  enthält keine Einträge zwischen Start- oder Enddatum!";

        /// <summary>
        /// Warnung: Die im Seminarfeld angegebene Wert existiert nicht
        /// </summary>
        public static string WARNING_NO_SEMINAR_SET = "Der für Seminar angegebene Wert ist existiert nicht";

        /// <summary>
        /// Warnung: Die im Trainerfeld angegebene Wert existiert nicht
        /// </summary>
        public static string WARNING_NO_TRAINER_SET = "Der für Trainer angegebene Wert ist existiert nicht";

        /// <summary>
        /// Warnung: Die im CoTrainerfeld angegebene Wert existiert nicht
        /// </summary>
        public static string WARNING_NO_COTRAINER_SET = "Der für CoTrainer angegebene Wert ist existiert nicht";

        /// <summary>
        /// Warnung: Die im Ortfeld angegebene Wert existiert nicht
        /// </summary>
        public static string WARNING_NO_LOCATION_SET = "Der für Ort angegebene Wert ist existiert nicht";

        /// <summary>
        /// Warnung: Die im Raumfeld angegebene Wert existiert nicht
        /// </summary>
        public static string WARNING_NO_ROOM_SET = "Der für Raum angegebene Wert ist existiert nicht";
    }
}
