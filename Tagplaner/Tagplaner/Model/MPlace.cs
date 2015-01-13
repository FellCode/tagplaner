using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    [Serializable()]
    public class MPlace
    {
        private int id;
        private string place;
        private string contact;
        private List<MRoom> rooms;

        #region getter
        public int Id
        {
            get { return id; }
            set { this.id = value; }
        }
        public string Place
        {
            get { return place; }
        }
        public string Contact
        {
            get { return contact; }
        }
        public List<MRoom> Rooms
        {
            get { return rooms; }
        }
        #endregion

        #region constructor
        public MPlace(string place, string contact, List<MRoom> rooms)
        {
           
            this.place = place;
            this.contact = contact;
            this.rooms = rooms;
        }
        public MPlace(int id, string place, string contact)
        {
            this.id = id;
            this.place = place;
            this.contact = contact;
        }
        #endregion

        public override string ToString()
        {
            return place;
        }
    }
}
