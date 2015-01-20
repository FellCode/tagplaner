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
        public static string WARINING_FILE_NOT_FOUND = "Datei konnte nicht geöffnet werden!";

        /// <summary>
        /// Fehler: Datei  enthält keine Einträge zwischen Start- oder Enddatum!
        /// </summary>
        public static string ERROR_NO_ENTRIES_BETWEEN_DATES = "Datei  enthält keine Einträge zwischen Start- oder Enddatum!";
    }
}
