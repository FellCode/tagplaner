﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    /// <summary>
    /// Diese Klasse bildet einen Kalendereintrag ab. Dieser Kann z. B. einen Seminartag, einen Praxistag oder einen Schultag abbilden.
    /// </summary>
    [Serializable()]
    public class MCalendarEntry
    {
        private int id;
        private MTrainer trainer;
        private MTrainer cotrainer;
        private MSeminar seminar;
        private MPractice practice;
        private MSchool school;
        private MPlace place;
        private MRoom room;

        #region getter

        /// <summary>
        /// Getter und Setter für ID
        /// </summary>
        public int Id
        {
            get { return id; }
            set { this.id = value; }
        }

        /// <summary>
        /// Getter und Setter für Trainer
        /// </summary>
        public MTrainer Trainer
        {
            get { return trainer; }
            set { this.trainer = value; }
        }

        /// <summary>
        /// Getter und Setter für Container
        /// </summary>
        public MTrainer Cotrainer
        {
            get { return cotrainer; }
            set { this.cotrainer = value; }
        }

        /// <summary>
        /// Getter und Setter für Seminar
        /// </summary>
        public MSeminar Seminar
        {
            get { return seminar; }
            set { this.seminar = value; }
        }

        /// <summary>
        /// Getter und Setter für Practice
        /// </summary>
        public MPractice Practice
        {
            get { return practice; }
            set { this.practice = value; }
        }

        /// <summary>
        /// Getter und Setter für School
        /// </summary>
        public MSchool School
        {
            get { return school; }
            set { this.school = value; }
        }

        /// <summary>
        /// Getter und Setter für Place
        /// </summary>
        public MPlace Place
        {
            get { return place; }
            set { this.place = value; }
        }

        /// <summary>
        /// Getter und Setter für Room
        /// </summary>
        public MRoom Room
        {
            get { return room; }
            set { this.room = value; }
        }
        #endregion

        #region Konstruktoren
        /// <summary>
        /// Konstruktor für einen Seminartag.
        /// </summary>
        /// <param name="trainer">Seminartrainer</param>
        /// <param name="cotrainer">Cotrainer</param>
        /// <param name="seminar">Seminar </param>
        /// <param name="place">Platz an dem das Seminar statt findet.</param>
        /// <param name="room">Raum indem das Seminar statt findet.</param>
        public MCalendarEntry(MTrainer trainer, MTrainer cotrainer,
            MSeminar seminar, MPlace place, MRoom room)
        {
            this.trainer = trainer;
            this.cotrainer = cotrainer;
            this.seminar = seminar;
            this.place = place;
            this.room = room;
        }
        /// <summary>
        /// Konstruktor für einen bereits bestehendes Seminarobjekt aus der Datenbank.
        /// </summary>
        /// <param name="id">Id des Seminarobjektes aus der Datenbank</param>
        /// <param name="trainer">Seminartrainer</param>
        /// <param name="cotrainer">Cotrainer</param>
        /// <param name="seminar">Seminar </param>
        /// <param name="place">Platz an dem das Seminar statt findet.</param>
        /// <param name="room">Raum indem das Seminar statt findet.</param>
        public MCalendarEntry(int id, MTrainer trainer, MTrainer cotrainer,
    MSeminar seminar, MPlace place, MRoom room)
        {
            this.id = id;
            this.trainer = trainer;
            this.cotrainer = cotrainer;
            this.seminar = seminar;
            this.place = place;
            this.room = room;
        }

        /// <summary>
        /// Konstruktor für einen bestehenden Praxistag aus der Datenbank.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="practice"></param>
        public MCalendarEntry(int id, MPractice practice)
        {
            this.id = id;
            this.practice = practice;
        }

        /// <summary>
        /// Konstruktor für einen Praxistag.
        /// </summary>
        /// <param name="practice"></param>
        public MCalendarEntry(MPractice practice)
        {
            this.practice = practice;
        }

        /// <summary>
        /// Konstruktor für einen Schultag.
        /// </summary>
        /// <param name="school"></param>
        public MCalendarEntry(MSchool school)
        {
            this.school = school;
        }

        /// <summary>
        /// Konstruktor für einen bestehenden Schultag aus der Datenbank.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="school"></param>
        public MCalendarEntry(int id, MSchool school)
        {
            this.id = id;
            this.school = school;
        }

        /// <summary>
        /// Kunstruktur zur befüllung eines Leeren Entrys aus dem Changepanel
        /// </summary>
        public MCalendarEntry()
        {

        }
        #endregion


    }
}
