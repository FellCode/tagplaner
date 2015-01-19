/*
 * Created by SharpDevelop.
 * User: tbender2
 * Date: 07.01.2015
 * Time: 16:22
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Tagplaner
{
	/// <summary>
	/// Erzeugt eine Liste von MCalendarDay-Objekten für den gewählten Zeitraum (inklusiv)
	/// <seealso cref="CCalendarUtilitys"></seealso>
	/// </summary>
	public interface ICalendarUtility
	{
		/// <summary>
		/// Die Schnittstelle für die Klasse CCalendarUtilitys
		/// <returns>Liste von DateTime-Objekten</returns>
		/// </summary>
		List<MCalendarDay> GenerateCalenderDayEntrys();
	}
}
