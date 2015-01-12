using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    public class MSeminar
    {
        private string title;
        private string subtitle;
        private string abbreviation;
        private string hasTechnology;
        private string comment;

        #region getter
        public string Title
        {
            get { return title; }
            set { this.title = value; }
        }
        public string Subtitle
        {
            get { return subtitle; }
            set { this.Subtitle = value; }
        }
        public string Abbreviation
        {
            get { return abbreviation; }
            set { this.abbreviation = value; }
        }
        public string HasTechnology
        {
            get { return hasTechnology; }
            set { this.hasTechnology = value; }
        }
        public string Comment
        {
            get { return comment; }
            set { this.comment = value; }
        }
        #endregion

        #region constructor
        public MSeminar(string title, string subtitle, string abbreviation, string hasTechnology, string comment)
        {
            this.title = title;
            this.subtitle = subtitle;
            this.abbreviation = abbreviation;
            this.hasTechnology = hasTechnology;
            this.comment = comment;
        }

        public MSeminar()
        {

        }
        #endregion
    }
}
