using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Coursework2
{


    public static class XmlControl
    {
        //Class variables
        private static readonly string workingDir = Directory.GetCurrentDirectory();
        private static readonly string ContactsDir = workingDir + @"\Contacts.xml";
        private static readonly string EventsDir = workingDir + @"\Events.xml";
        private static ArrayList eventsList;
        private static ArrayList contactList;
        private static Object ContactsDirLock = ""; 
        private static Object EventsDirLock = "";
        
        


        public static void CreateContactsXml(CalEvent cEvent)
        {
            Thread thr = new Thread(() => CreateContactsXmlThread(cEvent));
            thr.Start(); 
        }

        private static void CreateContactsXmlThread(CalEvent cEvent)
        {
            
            XDocument doc;
            lock (EventsDirLock)
            {
                if (!File.Exists(ContactsDir))
                {
                    doc = new XDocument(new XElement("Contacts"));
                    doc.Save(ContactsDir);
                    Console.WriteLine("Contacts.xml created succesfully");
                }
            }
        }


        public static void AddContact(Contact c)
        {
            Thread thr = new Thread(() => AddContactThread(c));
            thr.Start();
        }
        private static void AddContactThread(Contact contact)
        {

            XDocument doc;
            doc = XDocument.Load(ContactsDir);
            doc.Element("Contacts").Add(new XElement("contact",
                                        new XElement("fname", new XText(contact.FName)),
                                        new XElement("lname", new XText(contact.SName)),
                                        new XElement("address1", new XText(contact.Address1)),
                                        new XElement("address2", new XText(contact.Address2)),
                                        new XElement("postcode", new XText(contact.Postcode))));
            doc.Save(ContactsDir);
        }



        public static void CreateEventsXml()
        {
            Thread thr = new Thread(() => CreateEventsXmlThread());
            thr.Start();
        }
        private static void CreateEventsXmlThread()
        {
            XDocument doc;
            lock (ContactsDirLock)
            {
                if (!File.Exists(EventsDir))
                {
                    doc = new XDocument(new XElement("Events"));
                    doc.Save(EventsDir);
                    Console.WriteLine("Events.xml created succesfully");
                }
            }
        }

        public static void AddEvent(CalEvent e)
        {
            Thread thr = new Thread(() => AddEventThread(e));
            thr.Start();
        }
        private static void AddEventThread(CalEvent cEvent) 
        {
            lock (EventsDirLock)
            {
                lock (ContactsDirLock)
                {
                    CreateEventsXml();
                    cEvent.InitNullValues();
                    string concatenated = string.Join(",",
                                  cEvent.GetRecurringType().Select(x => x.ToString()).ToArray());

                    XDocument doc;
                    doc = XDocument.Load(EventsDir);
                    string person;
                    if (cEvent.IsAppointment())
                    {
                        person = cEvent.GetContact().ToString();
                    }
                    else
                    {
                        person = "NIL";
                    }

                    //int cId = GetContactId(cEvent.GetContact());
                    //MessageBox.Show(cEvent.ToString() + "xmlAddEvent");
                    doc.Element("Events").Add(new XElement("event",
                                                new XElement("dateStart", new XText(cEvent.GetStartDate().ToString())),
                                                new XElement("dateEnd", new XText(cEvent.GetEndDate().ToString())),
                                                new XElement("title", new XText(cEvent.GetTitle())),
                                                new XElement("appointment", new XText(cEvent.IsAppointment().ToString())),
                                                new XElement("recurring", new XText(cEvent.IsRecurring().ToString())),
                                                new XElement("recurringType", new XText(concatenated)),
                                                new XElement("person",
                                                    new XElement("id", new XText(cEvent.GetContact().GetId().ToString())),
                                                    new XElement("name", new XText(cEvent.GetContact().FName)),
                                                    new XElement("surname", new XText(cEvent.GetContact().SName)),
                                                    new XElement("address1", new XText(cEvent.GetContact().Address1)),
                                                    new XElement("address2", new XText(cEvent.GetContact().Address2)),
                                                    new XElement("postcode", new XText(cEvent.GetContact().Postcode))),
                                                new XElement("postcode", new XText(cEvent.GetLocation().ToString()))
                                                ));
                    doc.Save(EventsDir);
                }
            }
        }

        public static ArrayList GetEventsList()
        {

             CreateEventsXml();
            lock (ContactsDirLock)
            {
                lock (EventsDirLock)
                {

                        eventsList = new ArrayList();

                        XDocument doc = XDocument.Load(EventsDir);
                        IEnumerable<XElement> enumEvents = doc.Elements("Events").Elements();


                        DateTime dateStart;
                        DateTime dateEnd;
                        bool appointment;
                        bool recurring;
                        //ArrayList recurringType;
                        Contact person;
                        int cId;
                        string cName, cSName, cAddr1, cAddr2, cLoc;
                        int eId = 0;

                        foreach (var xmlEvents in enumEvents)
                        {

                            CalEvent ev = new CalEvent();
                            
                            ev.SetId(eId);
                            eId++;
                            String format = (xmlEvents.Element("dateStart").Value);
                            dateStart = Convert.ToDateTime(format);
                            format = (xmlEvents.Element("dateEnd").Value);
                            dateEnd = Convert.ToDateTime(format);
                            ev.SetStartDate(dateStart);
                            ev.SetEndDate(dateEnd);

                            //dateStart = XmlConvert.ToDateTime(xmlEvents.Element("dateStart").Value);
                            Console.WriteLine(dateStart.ToString());
                            //dateStart = new DateTime((xmlEvents.Element("dateStart").Value).ToString());
                            //ev.SetStartDate(xmlEvents.Element("dateStart").Value);
                            //ev.SetEndDate(xmlEvents.Element("dateEnd").Value);
                            ev.SetTitle(xmlEvents.Element("title").Value);
                            if ((xmlEvents.Element("appointment").Value) == "False")
                            {
                                appointment = false;
                            }
                            else appointment = true;
                            ev.SetAppointment(appointment);

                            if ((xmlEvents.Element("recurring").Value) == "False")
                            {
                                recurring = false;

                            }
                            else recurring = true;
                            ev.SetRecurring(recurring);
                            //MessageBox.Show(recurring.ToString());
                            //MessageBox.Show(ev.IsRecurring().ToString());

                            //MessageBox.Show(xmlEvents.Element("recurringType").Value);
                            if (recurring)
                            {
                                string recLong = xmlEvents.Element("recurringType").Value;
                                string[] recType = recLong.Split(',');
                                ev.SetRecurringType(recType);
                            }
                            else if (xmlEvents.Element("recurringType").Value == "NIL")
                            {
                                ev.SetRecurringType("NIL");
                            }
                            else if (ev.IsRecurring() && ev.GetRecurringTypeString().Contains("Weekly"))
                            {
                                ev.SetRecurring(true);
                                ev.SetRecurringType("Weekly");
                            }

                            else MessageBox.Show("Something is wrong with recType: XmlControl.GetEventsList()");
                            //MessageBox.Show(ev.ToString());

                            cId = int.Parse(xmlEvents.Element("person").Element("id").Value);
                            cName = (xmlEvents.Element("person").Element("name").Value);
                            cSName = (xmlEvents.Element("person").Element("surname").Value);
                            cAddr1 = (xmlEvents.Element("person").Element("address1").Value);
                            cAddr2 = (xmlEvents.Element("person").Element("address2").Value);
                            cLoc = (xmlEvents.Element("person").Element("postcode").Value);
                            Contact contact = new Contact(cName, cSName, cAddr1, cAddr2, cLoc);
                            contact.SetId(cId);

                            person = contact;
                            ev.SetContact(person);

                            ev.SetLocation(xmlEvents.Element("postcode").Value);
                            eventsList.Add(ev);
                        
                    }
                }
            }

            return eventsList;
        }

        public static Contact GetContactById(int id)
        {
            
            if (!File.Exists(ContactsDir))
            {
                XDocument createdoc = new XDocument(new XElement("Contacts"));
                createdoc.Save(ContactsDir);
                Console.WriteLine("Contacts.xml created succesfully");
            }
            XDocument doc = XDocument.Load(ContactsDir);

            IEnumerable<XElement> enumContacts = doc.Elements("Contacts").Elements();

            var NameList = new ArrayList();

            int cId;
            string name;
            string sName;
            string addr1;
            string addr2;
            string pcode;

            foreach (var xmlContacts in enumContacts)
            {
                cId = Int32.Parse(xmlContacts.Element("id").Value);
                if (cId == id)
                {
                    name = xmlContacts.Element("fname").Value;
                    sName = xmlContacts.Element("lname").Value;
                    addr1 = xmlContacts.Element("address1").Value;
                    addr2 = xmlContacts.Element("address2").Value;
                    pcode = xmlContacts.Element("postcode").Value;
                    return new Contact(name, sName, addr1, addr2, pcode);
                }
            }
            Console.WriteLine("XmlControl.cs -> GetContactById failed to find contact");
            return null;
        }



        public static int GetContactId(Contact contact)
        {
            //if (contact.GetId() == -1)
            //{
            //     contact.SetId(0);
            // }
            ArrayList contactList = XmlToContactArrayList();
            if (contactList.Count == 0)
            {
                contact.SetId(0);
            }
            else
            {
                contact.SetId(((Contact)contactList[contactList.Count - 1]).GetId() + 1);
            }

            int cId = -1;
            for (int i = 0; i < contactList.Count; i++)
            {
                if (Contact.ContactsEqual((Contact)contactList[i], contact))
                {
                    cId = i;
                }
            }
            return cId;   
        }

       
        public static ArrayList XmlToContactArrayList()
        {
            lock (ContactsDir)
            {
                lock (contactList)
                {
                    XDocument doc = XDocument.Load(ContactsDir);
                    IEnumerable<XElement> enumContacts = doc.Elements("Contacts").Elements();


                    contactList= new ArrayList();

                    foreach (var xmlContacts in enumContacts)
                    {
                        Contact contact = new Contact();
                        int id = Int32.Parse(xmlContacts.Element("id").Value);
                        //MessageBox.Show("XmlToContactsList id " + id);
                        contact.SetId(id);
                        contact.FName = xmlContacts.Element("fname").Value;
                        contact.SName = xmlContacts.Element("lname").Value;
                        contact.Address1 = xmlContacts.Element("address1").Value;
                        contact.Address2 = xmlContacts.Element("address2").Value;
                        contact.Postcode = xmlContacts.Element("postcode").Value;
                        contactList.Add(contact);
                    }
                }
            }
            return contactList;
        }
    }

}



