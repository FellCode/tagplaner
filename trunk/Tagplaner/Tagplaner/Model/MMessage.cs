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
        public static string CONFIRMATION_EXAMPLE = "Beispiel für eine Bestätigung";
        public static string WARNING_EXAMPLE = "Beispiel für eine Warnung";
        public static string INFORMATION_EXAMPLE = "Beispiel für eine Information";
        public static string ERROR_EXAMPLE = "Beispiel für eine Fehlermeldung";

        public static string CONFIRMATION_FILE_SAVE = "Möchten Sie die aktuellen Änderungen speichern?";
    }
}
