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
            get { return calendarList;}
        }

        private MCalender(){

        }
       
        public static MCalender getInstance(){
            if (instance == null)
            {
                instance = new MCalender();
            } return instance;
        }
        public class Kalendereintrag
        {
            private Trainer trainer { get;  }
            private Trainer cotrainer { get;  }
            private Kalendertag kalendertag{ get;  }
            private Fachrichtung fachrichtung { get;  }
            private Seminar seminar { get;  }
            private Praxis praxis { get;  }
            private Berufsschule berufsschule { get; }
            private Feiertag feiertag { get; set; }
            private Seminarort seminarort { get; }
            private Raum raum { get;  }

            private Kalendereintrag(Trainer trainer,Trainer cotrainer, Kalendertag kalendertag, Fachrichtung fachrichtung, Seminar seminar, Seminarort seminarort, Raum raum)
            {
                this.trainer = trainer;
                this.cotrainer = cotrainer;
                this.kalendertag = kalendertag;
                this.fachrichtung = fachrichtung;
                this.seminar = seminar;
                this.seminarort = seminarort;
                this.raum = raum;
            }

            private Kalendereintrag(Kalendertag kalendertag, Fachrichtung fachrichtung, Praxis praxis){
                this.kalendertag = kalendertag;
                this.fachrichtung = fachrichtung;
                this.praxis = praxis;
            }

            private Kalendereintrag(Kalendertag kalendertag, Fachrichtung fachrichtung, Berufsschule berufsschule)
            {
        
                this.kalendertag = kalendertag;
                this.fachrichtung = fachrichtung;
                this.berufsschule = berufsschule;
            }

            private Kalendereintrag( Kalendertag kalendertag, Fachrichtung fachrichtung, Feiertag feiertag)
            {
             
                this.kalendertag = kalendertag;
                this.fachrichtung = fachrichtung;
                this.feiertag = feiertag;
            }

            private Kalendereintrag(Trainer trainer, Trainer cotrainer, Kalendertag kalendertag, Fachrichtung fachrichtung, Seminar seminar, Seminarort seminarort, Raum raum, Praxis praxis)
            {
                this.trainer = trainer;
                this.cotrainer = cotrainer;
                this.kalendertag = kalendertag;
                this.fachrichtung = fachrichtung;
                this.seminar = seminar;
                this.praxis = praxis;
                this.seminarort = seminarort;
                this.raum = raum;
            }

            

        }

        public void FillCalenderSeminar(Trainer trainer, Kalendertag kalendertag, Fachrichtung fachrichtung, Seminar seminar, Seminarort seminarort, Raum raum)
            {
                Kalendereintrag cal = new Kalendereintrag(Trainer trainer, Kalendertag kalendertag, Fachrichtung fachrichtung, Seminar seminar, Seminarort seminarort, Raum raum);
                calendarList.Add(cal);
            }

        public void FillCalenderPraxis(Kalendertag kalendertag, Fachrichtung fachrichtung, Praxis praxis)
            {
                Kalendereintrag cal = new Kalendereintrag(Kalendertag kalendertag, Fachrichtung fachrichtung, Praxis praxis);
                calendarList.Add(cal);
            }

        public void FillCalenderBerufsschule(Kalendertag kalendertag, Fachrichtung fachrichtung, Berufsschule berufsschule)
            {
                Kalendereintrag cal = new Kalendereintrag(Kalendertag kalendertag, Fachrichtung fachrichtung, Berufsschule berufsschule);
                calendarList.Add(cal);
            }

        public void FillCalenderFeiertag(Kalendertag kalendertag, Fachrichtung fachrichtung, Feiertag feiertag)
            {
                Kalendereintrag cal = new Kalendereintrag(Kalendertag kalendertag, Fachrichtung fachrichtung, Feiertag feiertag);
                calendarList.Add(cal);
            }

        public void fillCalenderSeminarPraxis(Trainer trainer, Kalendertag kalendertag, Fachrichtung fachrichtung, Seminar seminar, Seminarort seminarort, Raum raum, Praxis praxis)
            {
                Kalendereintrag cal = new Kalendereintrag(Trainer trainer, Kalendertag kalendertag, Fachrichtung fachrichtung, Seminar seminar, Seminarort seminarort, Raum raum, Praxis praxis);
                calendarList.Add(cal);
            }

        
    }
}
