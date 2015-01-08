using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    [Serializable()]
    class MCalendar
    {
        private List<MCalendarEntry> calendarList = new List<MCalendarEntry>();
        private static MCalendar instance;

        #region getter
        public DateTime firstDay { get; set; }
        public DateTime endDay { get; set; }
        public List<MCalendarEntry> CalendarList
        {
            get { return calendarList; }
        }

        private MCalendar()
        {

        }

        public static MCalendar getInstance()
        {
            if (instance == null)
            {
                instance = new MCalendar();
            } return instance;
        }
        #endregion

        #region AddDay-Methods
        public void AddSeminarDay(MTrainer trainer, MTrainer cotrainer, MCalendarDay calendarDay, MSpeciality speciality, MSeminar seminar, MPlace place, MRoom room)
        {
            MCalendarEntry cal = new MCalendarEntry(trainer, cotrainer, calendarDay, speciality, seminar, place, room);
            calendarList.Add(cal);
        }

        public void AddPracticeDay(MCalendarDay calendarDay, MSpeciality speciality, MPractice practice)
        {
            MCalendarEntry cal = new MCalendarEntry(calendarDay, speciality, practice);
            calendarList.Add(cal);
        }

        public void AddSchoolDay(MCalendarDay calendarDay, MSpeciality speciality, MSchool school)
        {
            MCalendarEntry cal = new MCalendarEntry(calendarDay, speciality, school);
            calendarList.Add(cal);
        }

        public void AddHoliday(MCalendarDay calendarDay, MSpeciality speciality, MHoliday holiday)
        {
            MCalendarEntry cal = new MCalendarEntry(calendarDay, speciality, holiday);
            calendarList.Add(cal);
        }

        public void AddPracticeSeminarDay(MTrainer trainer, MTrainer cotrainer, MCalendarDay calendarDay, MSpeciality speciality, MSeminar seminar, MPlace place, MRoom room, MPractice practice)
        {
            MCalendarEntry cal = new MCalendarEntry(trainer, cotrainer, calendarDay, speciality, seminar, place, room, practice);
            calendarList.Add(cal);
        }

#endregion
        
    }
}
