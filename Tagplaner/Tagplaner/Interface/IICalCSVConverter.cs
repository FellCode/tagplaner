/*
 * Created by SharpDevelop.
 * User: tbender2
 * Date: 07.01.2015
 * Time: 13:24
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Tagplaner
{
	/// <summary>
	/// Die Schnittstelle für die Klasse CICalCSVConverter
	/// <seealso cref="CICalCSVConverter"></seealso>
	/// </summary>
	public interface IICalCSVConverter
	{
		/// <summary>
		/// Testet, ob in der ICS-Datei Ferien-Einträge für einen Zeitraum  (inklusiv) vorhandenen sind
		/// <para name="startdate">Startdatum, inklusiv</para>
		/// <para name="enddate">Enddatum, inklusiv</para>
		/// <para name="filename">Name der ICS-Datei</para>
		/// <returns>liefert true, wenn mindestens ein Ferien-Eintrag im Zeitraum liegt, sonst false</returns>
		/// </summary>
		bool CheckICSFile(DateTime startdate, DateTime enddate, string filename);
		/// <summary>
		/// Liest alle Einträge in den ICS-Dateien Ferien-Einträge für einen Zeitraum  (inklusiv)
		/// <para name="startdate">Startdatum, inklusiv</para>
		/// <para name="enddate">Enddatum, inklusiv</para>
		/// <returns>Liste der MVacation-Objekte, die im Zeitraum liegen, sonst null</returns>
		/// </summary>
		List<MVacation> GetICalEntrys(DateTime startdate, DateTime enddate);
	}
}
