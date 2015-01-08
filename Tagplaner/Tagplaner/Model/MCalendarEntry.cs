using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    class MCalendarEntry
        {
            private MTrainer trainer;
            private MTrainer cotrainer;
            private MCalendarDay calendarDay;
            private MSpeciality speciality;
            private MSeminar seminar;
            private MPractice practice;
            private MSchool school;
            private MHoliday holiday;
            private MPlace place;
            private MRoom room;

            #region getter/setter
            public MTrainer Trainer
            {
                get { return trainer; }
            }
            public MTrainer Cotrainer
            {
                get { return cotrainer; }
            }
            public MCalendarDay CalendarDay
            {
                get { return calendarDay; }
            }
            public MSpeciality Speciality
            {
                get { return speciality; }
            }
            public MSeminar Seminar
            {
                get { return seminar; }
            }
            public MPractice Practice
            {
                get { return practice; }
            }
            public MSchool School
            {
                get { return school; }
            }
            public MHoliday Holiday
            {
                get { return holiday; }
            }
            public MHoliday Holiday
            {
                get { return holiday; }
            }
            public MPlace Place
            {
                get { return place; }
            }
            public MRoom Room
            {
                get { return room; }
            }
            #endregion

            #region constructors
            public MCalendarEntry(MTrainer trainer, MTrainer cotrainer, MCalendarDay calendarDay, MSpeciality speciality,
                MSeminar seminar, MPlace place, MRoom room)
            {
                this.trainer = trainer;
                this.cotrainer = cotrainer;
                this.calendarDay = calendarDay;
                this.speciality = speciality;
                this.seminar = seminar;
                this.place = place;
                this.room = room;
            }

            public MCalendarEntry(MCalendarDay calendarDay, MSpeciality speciality, MPractice practice)
            {
                this.calendarDay = calendarDay;
                this.speciality = speciality;
                this.practice = practice;
            }

            public MCalendarEntry(MCalendarDay calendarDay, MSpeciality speciality, MSchool school)
            {

                this.calendarDay = calendarDay;
                this.speciality = speciality;
                this.school = school;
            }

            public MCalendarEntry(MCalendarDay calendarDay, MSpeciality speciality, MHoliday holiday)
            {

                this.calendarDay = calendarDay;
                this.speciality = speciality;
                this.holiday = holiday;
            }

            public MCalendarEntry(MTrainer trainer, MTrainer cotrainer, MCalendarDay calendarDay, MSpeciality speciality,
                MSeminar seminar, MPlace place, MRoom room, MPractice practice)
            {
                this.trainer = trainer;
                this.cotrainer = cotrainer;
                this.calendarDay = calendarDay;
                this.speciality = speciality;
                this.seminar = seminar;
                this.practice = practice;
                this.place = place;
                this.room = room;
            }
            #endregion

        }
}
