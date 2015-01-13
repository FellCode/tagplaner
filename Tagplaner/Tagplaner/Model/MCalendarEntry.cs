using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    [Serializable()]
    public class MCalendarEntry
    {
        private int id;
        private MCalendarDay calendarDay;
        private MTrainer trainer;
        private MTrainer cotrainer;
        private MSeminar seminar;
        private MPractice practice;
        private MSchool school;
        private MPlace place;
        private MRoom room;

        #region getter
        public MCalendarDay CalendarDay
        {
            get { return calendarDay; }
            set { calendarDay = value; }
        }
        public int Id
        {
            get { return id; }
            set { this.id = value; }
        }
        public MTrainer Trainer
        {
            get { return trainer; }
            set { this.trainer = value; }
        }
        public MTrainer Cotrainer
        {
            get { return cotrainer; }
            set { this.cotrainer = value; }
        }
        public MSeminar Seminar
        {
            get { return seminar; }
            set { this.seminar = value; }
        }
        public MPractice Practice
        {
            get { return practice; }
            set { this.practice = value; }
        }
        public MSchool School
        {
            get { return school; }
            set { this.school = value; }
        }
        public MPlace Place
        {
            get { return place; }
            set { this.place = value; }
        }
        public MRoom Room
        {
            get { return room; }
            set { this.room = value; }
        }
        #endregion

        #region Konstruktoren
        public MCalendarEntry( MCalendarDay calendarDay, MTrainer trainer, MTrainer cotrainer,
            MSeminar seminar, MPlace place, MRoom room)
        {
            this.calendarDay = calendarDay;
            this.trainer = trainer;
            this.cotrainer = cotrainer;
            this.seminar = seminar;
            this.place = place;
            this.room = room;
        }
        public MCalendarEntry(int id,MCalendarDay calendarDay, MTrainer trainer, MTrainer cotrainer,
    MSeminar seminar, MPlace place, MRoom room)
        {
            this.id = id;
            this.calendarDay = calendarDay;
            this.trainer = trainer;
            this.cotrainer = cotrainer;
            this.seminar = seminar;
            this.place = place;
            this.room = room;
        }

        public MCalendarEntry(int id, MCalendarDay calendarDay, MPractice practice)
        {
            this.id = id;
            this.calendarDay = calendarDay;
            this.practice = practice;
        }

        public MCalendarEntry(MCalendarDay calendarDay, MPractice practice)
        {
            this.calendarDay = calendarDay;
            this.practice = practice;
        }

        public MCalendarEntry(MCalendarDay calendarDay, MSchool school)
        {
            this.calendarDay = calendarDay;
            this.school = school;
        }
        public MCalendarEntry(int id, MCalendarDay calendarDay, MSchool school)
        {
            this.id = id;
            this.calendarDay = calendarDay;
            this.school = school;
        }
        #endregion








    }
}
