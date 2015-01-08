using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    public class MPlace
    {
        private string place;
        private string contact;
        private List<MRoom> rooms;

        #region getter
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
        #endregion
    }
}
