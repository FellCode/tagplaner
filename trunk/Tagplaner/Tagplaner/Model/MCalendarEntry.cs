﻿using System;
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
        private MTrainer trainer;
        private MTrainer cotrainer;
        private MSpeciality speciality;
        private MSeminar seminar;
        private MPractice practice;
        private MSchool school;
        private MHoliday holiday;
        private MPlace place;
        private MRoom room;

        #region getter
        public int Id
        {
            get { return id; }
            set { this.id = value; }
        }
        public MTrainer Trainer
        {
            get { return trainer; }
        }
        public MTrainer Cotrainer
        {
            get { return cotrainer; }
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
        public MPlace Place
        {
            get { return place; }
        }
        public MRoom Room
        {
            get { return room; }
        }
        #endregion


        public MCalendarEntry( MTrainer trainer, MTrainer cotrainer, MSpeciality speciality,
            MSeminar seminar, MPlace place, MRoom room)
        {
            this.trainer = trainer;
            this.cotrainer = cotrainer;

            this.speciality = speciality;
            this.seminar = seminar;
            this.place = place;
            this.room = room;
        }
        public MCalendarEntry(int id, MTrainer trainer, MTrainer cotrainer, MSpeciality speciality,
    MSeminar seminar, MPlace place, MRoom room)
        {
            this.id = id;
            this.trainer = trainer;
            this.cotrainer = cotrainer;

            this.speciality = speciality;
            this.seminar = seminar;
            this.place = place;
            this.room = room;
        }

        public MCalendarEntry(int id, MCalendarDay calendarDay, MSpeciality speciality, MPractice practice)
        {
            this.id = id;
            this.speciality = speciality;
            this.practice = practice;
        }

        public MCalendarEntry(MCalendarDay calendarDay, MSpeciality speciality, MPractice practice)
        {

            this.speciality = speciality;
            this.practice = practice;
        }
    }
}