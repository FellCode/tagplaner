using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    class MCalender
    {
        private List<Kalendereintrag> calendarList = new List<Kalendereintrag>();
        private DateTime firstday { get; set; }
        private DateTime endday { get; set; }
        private static MCalender instance;
        public List<Kalendereintrag> CalendarList
        {
            get { return calendarList; }
        }

        private MCalender()
        {

        }

        public static MCalender getInstance()
        {
            if (instance == null)
            {
                instance = new MCalender();
            } return instance;
        }
        public class Kalendereintrag
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

            public Kalendereintrag(MTrainer trainer, MTrainer cotrainer, MCalendarDay calendarDay, MSpeciality speciality,
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

            public Kalendereintrag(MCalendarDay calendarDay, MSpeciality speciality, MPractice practice)
            {
                this.calendarDay = calendarDay;
                this.speciality = speciality;
                this.practice = practice;
            }

            public Kalendereintrag(MCalendarDay calendarDay, MSpeciality speciality, MSchool school)
            {

                this.calendarDay = calendarDay;
                this.speciality = speciality;
                this.school = school;
            }

            public Kalendereintrag(MCalendarDay calendarDay, MSpeciality speciality, MHoliday holiday)
            {

                this.calendarDay = calendarDay;
                this.speciality = speciality;
                this.holiday = holiday;
            }

            public Kalendereintrag(MTrainer trainer, MTrainer cotrainer, MCalendarDay calendarDay, MSpeciality speciality,
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



        }

        public void AddSeminarDay(MTrainer trainer, MTrainer cotrainer, MCalendarDay calendarDay, MSpeciality speciality, MSeminar seminar, MPlace place, MRoom room)
        {
            Kalendereintrag cal = new Kalendereintrag(trainer, cotrainer, calendarDay, speciality, seminar, place, room);
            calendarList.Add(cal);
        }

        public void AddPracticeDay(MCalendarDay calendarDay, MSpeciality speciality, MPractice practice)
        {
            Kalendereintrag cal = new Kalendereintrag(calendarDay, speciality, practice);
            calendarList.Add(cal);
        }

        public void AddSchoolDay(MCalendarDay calendarDay, MSpeciality speciality, MSchool school)
        {
            Kalendereintrag cal = new Kalendereintrag(calendarDay, speciality, school);
            calendarList.Add(cal);
        }

        public void AddHoliday(MCalendarDay calendarDay, MSpeciality speciality, MHoliday holiday)
        {
            Kalendereintrag cal = new Kalendereintrag(calendarDay, speciality, holiday);
            calendarList.Add(cal);
        }

        public void AddPracticeSeminarDay(MTrainer trainer, MTrainer cotrainer, MCalendarDay calendarDay, MSpeciality speciality, MSeminar seminar, MPlace place, MRoom room, MPractice practice)
        {
            Kalendereintrag cal = new Kalendereintrag(trainer, cotrainer, calendarDay, speciality, seminar, place, room, practice);
            calendarList.Add(cal);
        }


    }
}
