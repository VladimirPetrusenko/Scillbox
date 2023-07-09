using System;
using System.Collections.Generic;

namespace ConsultantManagerLibrary
{
    [Serializable]
    public class Manager : Consultant
    {
        public Manager()
        {

        }
        public Manager(string surname_, string name_, string patronymic_, string number_phone_, string passport_,
            string status_, string datetimechangenote_, string changedata_, string changetype_, string changeagent_) :
            base(surname_, name_, patronymic_, number_phone_, passport_, status_, datetimechangenote_, changedata_, 
                changetype_, changeagent_)
        {

        }

        public class SortByName : IComparer<Manager>
        {
            public int Compare(Manager x, Manager y)
            {
                Manager X = (Manager)x;
                Manager Y = (Manager)y;

                return String.Compare(X.Name, Y.Name);
            }
        }

        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Patronymic
        {
            get { return patronymic; }
            set { patronymic = value; }
        }

        public string Number_phone
        {
            get { return number_phone; }
            set { if (value != null) number_phone = value; else number_phone = "000-000"; }
        }

        public string Passport
        {
            get { return passport; }
            set { passport = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public string Datetimechangenote
        {
            get { if (datetimechangenote == "") return "-"; else return datetimechangenote; }
        }

        public string Changedata
        {
            get { if (changedata == "") return "-"; else return changedata; }
        }

        public string Changetype
        {
            get { if (changetype == "") return "-"; else return changetype; }
        }

        public string Changeagent
        {
            get { if (changeagent == "") return "-"; else return changeagent; }
        }
    }
}
