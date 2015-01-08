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
        private bool hasTechnology;
        private string comment;

        #region getter
        public string Title
        {
            get { return title; }
        }
        public string Subtitle
        {
            get { return subtitle; }
        }
        public string Abbreviation
        {
            get { return abbreviation; }
        }
        public bool HasTechnology
        {
            get { return hasTechnology; }
        }
        public string Comment
        {
            get { return comment; }
        }
        #endregion

        #region constructor
        public MSeminar(string title, string subtitle, string abbreviation, bool hasTechnology, string comment)
        {
            this.title = title;
            this.subtitle = subtitle;
            this.abbreviation = abbreviation;
            this.hasTechnology = hasTechnology;
            this.comment = comment;
        }
        #endregion
    }
}
