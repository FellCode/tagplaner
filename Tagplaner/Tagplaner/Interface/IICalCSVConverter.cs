﻿/*
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
    /// Description of Interface1.
    /// </summary>
    public interface IICalCSVConverter
    {
        bool CheckICSFile(DateTime startdate, DateTime enddate, string filename);
        bool CheckCsvFile(DateTime startdate, DateTime enddate, string filename);
        List<MVacation> GetICalEntrys(DateTime startdate, DateTime enddate);
    }
}
