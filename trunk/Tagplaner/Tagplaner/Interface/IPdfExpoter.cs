using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    /// <summary>
    ///  Interface für Klassen die zum erzeuge von PDF-Dokumenten verwendet werden
    /// </summary>
    public interface IPdfExporter
    {
        /// <summary>
        /// Erzeugt ein PDF-Dokument speichert dieses an den angegebenen Ort
        /// </summary>
        /// <param name="filename">Speichertort für das PDF-Dokument</param>
        /// <returns>Ist true wenn die Datei erfolgreich erstellt wurde</returns>s
        bool ExportPdf(string filename);
    }
}
