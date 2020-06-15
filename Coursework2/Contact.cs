using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace Coursework2
{
    public class Contact
    {

        //private static LinkedHashSet <Contact> ContactSet = new LinkedHashSet<Contact>();
        private string fName;
        private string sName;
        private string address1;
        private string address2;
        private string postcode;
        private int LastId = -1;


            //Constructors
        public Contact(string fName, string sName, string address1, string address2, string postcode)
        {
            //if (LastId != -1)
            {
                this.fName = fName ?? throw new ArgumentNullException(nameof(fName));
                this.sName = sName;
                this.address1 = address1;
                this.address2 = address2;
                this.postcode = postcode;
                //LastId++;
            }
            //else
            //{
            //    Console.WriteLine("Contact Id not set");
            //    //System.Windows.Forms.Application.ExitThread();
            //}
            //ContactSet.Add(this);
        }

        public Contact()
        {
            //if (LastId == -1)
            //{
            //    Console.WriteLine("Contact Id not set");
            //    //System.Windows.Forms.Application.ExitThread();
            //}
            //else
            //{
                //LastId++;
            //}
        }
        public void SetId(int id)
        {
            LastId = id;
        }
        
        public int GetId()
        {
            return LastId;
        }
        
        //public void AddToContactSet(Contact contact)
        //{
        //    ContactSet.Add(contact);
        //}
        //public void DelFromContactSet(Contact contact)
        //{
        //    ContactSet.Remove(contact);
        //}

        //// Setters Getters
        //public LinkedHashSet<Contact> GetContactSet ()
        //{
        //    return ContactSet;
        //}

       // public Contact GetContact() { return this; }

        public string FName
        {
            get => fName;
            set => fName = value;
        }

        public string SName
        {
            get => sName;
            set => sName = value;
        }

        public string Address1
        {
            get => address1;
            set => address1 = value;
        }

        public string Address2
        {
            get => address2;
            set => address2 = value;
        }

        public string Postcode
        {
            get => postcode;
            set => postcode = value;
        }

        override public string ToString()
        {
            return "" + LastId;
        }

        public static bool ContactsEqual(Contact c1, Contact c2)
        {
            if (c1.SName == c2.SName && c1.SName == c2.SName 
                && c1.Postcode == c2.Postcode 
                && c1.Address1 == c2.Address1
                && c1.Address2 == c2.Address2)
            {
                return true;
            }
            return false;
        }
    }
    

}
