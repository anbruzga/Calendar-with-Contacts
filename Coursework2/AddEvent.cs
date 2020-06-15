using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Coursework2
{
    public partial class AddEvent : Form
    {
        private static CalEvent cEvent;
        private DateTime EndDateTime;
        private DateTime StartDateTime;
        private CalendarForm calendarForm;
        private DateTime SquareDate;
        private bool StopCBRecurringListener = false;
        private bool StopDateTimeListeners = false;
        private int EventEditedId;

        public AddEvent()
        {
            InitializeComponent();

        }
        private void Alert(string s, Object t)
        {
            MessageBox.Show(s + " " + t.ToString());
        }

        private void Alert(Object t)
        {
            MessageBox.Show(t.ToString());
        }

        public AddEvent(CalendarForm calendarForm, DateTime dateTime)
        {

            this.calendarForm = calendarForm;
            SquareDate = dateTime;
            InitializeComponent();

            //endDate.Enabled = true;
            startDate.Value = SquareDate.Date;
            endDate.Value = SquareDate.Date;
            //endDate.Enabled = false;
        }

        public AddEvent(CalendarForm calendarForm)
        {
            this.calendarForm = calendarForm;
            InitializeComponent();


        }

        private void EndDateChanged_Handler(object sender, EventArgs e)
        {
            if (StopDateTimeListeners) return;

            StartDateTime = startDate.Value.Date +
                    startTime.Value.TimeOfDay;

            EndDateTime = endDate.Value.Date +
            endTime.Value.TimeOfDay;

            if (cbRecurring.Checked &&
                StartDateTime.Subtract(EndDateTime).TotalSeconds > -1
                && endDate.Enabled == true)
            {
                //MessageBox.Show("Start Date is after End Date!");
                btnSubmit.Enabled = false;
            }
            else if (startTime.Value >= endTime.Value)
            {
                //MessageBox.Show("Start Date is after End Date!");
                btnSubmit.Enabled = false;
            }
            else
            {
                btnSubmit.Enabled = true;
            }
            
        }

        private void StartDateChanged_Handler(object sender, EventArgs e)
        {
            if (StopDateTimeListeners) return;


            StartDateTime = startDate.Value.Date +
                startTime.Value.TimeOfDay;

            EndDateTime = endDate.Value.Date +
                endTime.Value.TimeOfDay;

            if (StartDateTime.CompareTo(EndDateTime) > 0)
            {
                //endDate.Value = new startDate.Value;
                endTime.Value = DateUtil.AddHour(EndDateTime, 1);
            }

            endDate.MinDate = startDate.Value;
            //endDate.Enabled = true;
            //endTime.Enabled = true;


            //endDate.Value = startDate.Value;
            //endDate.Value = DateUtil.AddHour(endDate.Value, 1);
            endDate.MinDate = startDate.Value;

            // when start date changes, end date must be not smaller
            // when start date changes, end date is automatically 1h plus
            // when start date changes, end date is enabled
        }



        private void MenuItemClickHandler(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;

            if (sender.ToString().Equals("Contacts"))
            {
                ContactsForm form = new ContactsForm(this);
                form.ShowDialog();
            }

        }
        public void UpdateContactList()
        {
            if (!lbPerson.Enabled)
            {
                lbPerson.Enabled = true;
                lbPerson.DataSource = PopulateCotnactListBox();
                lbPerson.Refresh();
                lbPerson.Enabled = false;
            }
            else
            {
                lbPerson.DataSource = PopulateCotnactListBox();
                lbPerson.Refresh();
            }
        }
        private void buttonSubmit_Click(object sender, EventArgs e)
        {

            if (tbTitle.Text == String.Empty)
            {
                Alert("Events must have title!");
            }
            else if (btnSubmit.Text != "Save")
            {
                CalEvent ev;
                // because class has static event, and i want to use not static
                // but static is needed for RecurringForm, therefore copy recurring parameter into
                // new CalEvent object
                if (cbRecurring.Checked)
                {
                    ev = new CalEvent();
                    ev.SetRecurringType(cEvent.GetRecurringType());
                    ev.SetRecurring(true);
                }
                else
                {
                    ev = new CalEvent();
                }
                try
                {
                    //Set all the values for CalEvent
                    ev.SetAppointment(cbAppointment.Checked);
                    ev.SetTitle(tbTitle.Text);

                    StartDateTime = startDate.Value.Date +
                    startTime.Value.TimeOfDay;

                    if (cbRecurring.Checked)
                    {
                        EndDateTime = endDate.Value.Date +
                            endTime.Value.TimeOfDay;
                    }
                    else
                    {
                        EndDateTime = startDate.Value.Date +
                            endTime.Value.TimeOfDay;
                    }


                    ev.SetDate(StartDateTime, EndDateTime);

                    //retrieve contact
                    if (cbAppointment.Checked)
                    {
                        ArrayList contacts = (ArrayList)lbPerson.DataSource;
                        int index = contacts.IndexOf(lbPerson.SelectedItem);
                        ev.SetContact(GetContactById(index));

                    }

                    ev.SetLocation(tbLocation.Text);
                    ev.InitNullValues();

                    XmlControl.AddEvent(ev);

                    //initListBox();

                }
                catch (ArgumentNullException ex)
                {
                    MessageBox.Show(ex.ToString() + "\n Button Click -> AddEvent.cs");
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString() + "\n Button Click -> AddEvent.cs"); }

                //lbEvent.DataSource = XmlControl.GetEventsList();
                ArrayList Events = XmlControl.GetEventsList();
                ArrayList EventsData = new ArrayList();
                for (int i = 0; i < Events.Count; i++)
                {
                    EventsData.Add(((CalEvent)Events[i]).GetDates(StartDateTime, EndDateTime));
                }
                Thread.Sleep(300);
                SetEventsData();
                tbLocation.Text = "";
                tbTitle.Text = "";
                
                calendarForm.Repaint();
            }
            else
            {
                ArrayList eList = new ArrayList();
                Thread.Sleep(300);
                eList = SetEventsData();

                //eList[eventEditedId] = new CalEvent();
                CalEvent ev = new CalEvent();
                ev.SetTitle(tbTitle.Text);
                ev.SetDate(StartDateTime, EndDateTime);
                if (cbRecurring.Checked)
                {
                    //try (!recurDatesSet && Object.Equals(recurringDates, default(ArrayList)))
                    if (Object.Equals(cEvent, default(CalEvent)))
                    {
                        ev.SetRecurringType(((CalEvent)eList[EventEditedId]).GetRecurringType());
                    }
                    else
                    {
                        ev.SetRecurringType(cEvent.GetRecurringType());
                    }
                    //catch(Exception ex)
                    //{
                    //    (CalEvent)eList[eventEditedId]).SetRecurringType(cEvent.GetRecurringType());
                    //}

                    ev.SetRecurring(true);
                }
                else
                {
                    ev.SetRecurring(false);
                }
                if (cbAppointment.Checked)
                {
                    ArrayList contacts = (ArrayList)lbPerson.DataSource;
                    int index = contacts.IndexOf(lbPerson.SelectedItem);
                    ev.SetContact(GetContactById(index));
                    ev.SetAppointment(true);
                    ev.SetLocation(tbLocation.Text);
                }

                ev.InitNullValues();

                //eList[eventEditedId] = ev;

                ArrayList FullEvents = XmlControl.GetEventsList();
                foreach (CalEvent Event in FullEvents)
                {
                    if(Event.ToString() == ((CalEvent)eList[EventEditedId]).ToString()){
                        FullEvents.Remove(Event);
                        break;
                    }
                }
                //FullEvents.Remove((CalEvent)eList[eventEditedId]);
                FullEvents.Add(ev);
                //((CalEvent)eList[eventEditedId]).SetId();

                //Contact c = new Contact();
                //ContactsListToXml(contactList);
                EventsListToXml(FullEvents);

                //lbEvent.DataSource = eList;
                Thread.Sleep(300);
                SetEventsData();

                lbEvent.Update();

                btnSubmit.Text = "Add New Event!";


                tbLocation.Text = "";
                tbTitle.Text = "";

                StartDateTime = DateTime.Now;
                EndDateTime = DateTime.Now;
                startDate.Value = StartDateTime;
                startTime.Value = StartDateTime;
                endDate.MinDate = new DateTime(1753,1,1,1,1,1);
                endDate.Value = EndDateTime;
                endTime.Value = EndDateTime;

                StopCBRecurringListener = true;
                cbRecurring.Checked = false;
                StopCBRecurringListener = false;

                cbAppointment.Checked = false;

                calendarForm.Repaint();
            }

        }

        private ArrayList SetEventsData()
        {


            ArrayList Events = XmlControl.GetEventsList();
            ArrayList EventsData = new ArrayList();
            ArrayList temp;
            bool Contains = false;
            for (int i = 0; i < Events.Count; i++)
            {
                ((CalEvent)Events[i]).CalcRecurringDates();
                temp = ((CalEvent)Events[i]).GetDates();
                foreach (DateTime e in temp)
                {
                    if (e.Date == SquareDate.Date)
                    {
                        Contains = true;
                        break;
                    }
                }
                if (Contains)
                {
                    //Alert((CalEvent)Events[i]);
                    EventsData.Add((CalEvent)Events[i]);
                    Contains = false;
                }
            }
            ArrayList EventNames = new ArrayList();
            foreach (CalEvent e in EventsData)
            {
                EventNames.Add(e.GetTitle());
            }
            lbEvent.DataSource = EventNames;
            return EventsData;
        }

        private void WriteToXml(Contact contact)
        {

            ArrayList contactList = XmlToContactArrayList();
            if (contactList.Count == 0)
            {
                contact.SetId(0);
            }
            else
            {
                contact.SetId(((Contact)contactList[contactList.Count - 1]).GetId() + 1);
                //
            }
            XDocument doc;
            String workingDir = Directory.GetCurrentDirectory();
            string filePath = workingDir + @"\Contacts.xml";

            if (!File.Exists(filePath))
            {
                doc = new XDocument(new XElement("Contacts"));
                doc.Save(filePath);
                Console.WriteLine("Contacts.xml created succesfully");
            }


            // Xml exists now
            // Adding a child to the xml 
            doc = XDocument.Load(filePath);

            doc.Element("Contacts").Add(new XElement("contact",
                                        new XElement("id", new XText(contact.GetId().ToString())),
                                        new XElement("fname", new XText(contact.FName)),
                                        new XElement("lname", new XText(contact.SName)),
                                        new XElement("address1", new XText(contact.Address1)),
                                        new XElement("address2", new XText(contact.Address2)),
                                        new XElement("postcode", new XText(contact.Postcode))));
            doc.Save(filePath);
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            int position = lbEvent.SelectedIndex;
            EventEditedId = lbEvent.SelectedIndex;
            //Alert(eventEditedId + " : " + position);
            //Alert(eventEditedId);
            ArrayList eList = new ArrayList();
            //eList.Clear();
            Thread.Sleep(300);
            eList = SetEventsData();

            lbEvent.SelectedIndex = EventEditedId;
            try
            {
                tbTitle.Text = ((CalEvent)eList[EventEditedId]).GetTitle();
            }
            catch (Exception ex)
            {
                Alert("Error: Nothing is Selected!");
                return;
            }
            btnSubmit.Text = "Save";
            tbLocation.Text = ((CalEvent)eList[EventEditedId]).GetLocation();
            if (tbLocation.Text == "NIL") tbLocation.Text = "";
            cbAppointment.Checked = ((CalEvent)eList[EventEditedId]).IsAppointment();
            if (cbAppointment.Checked)
            {
                lbPerson.SelectedIndex = ((CalEvent)eList[EventEditedId]).GetContact().GetId();
            }

            // Not to activate the popup for recurring type selection
            StopCBRecurringListener = true;
            cbRecurring.Checked = ((CalEvent)eList[EventEditedId]).IsRecurring();
            StopCBRecurringListener = false;

            if (cbRecurring.Checked)
            {
                endDate.Enabled = true;
            }
            else endDate.Enabled = false;

            StopDateTimeListeners = true;
            StartDateTime = ((CalEvent)eList[EventEditedId]).GetStartDate();
            startDate.Value = StartDateTime;
            startTime.Value = StartDateTime;
            EndDateTime = ((CalEvent)eList[EventEditedId]).GetEndDate();
            endDate.Value = EndDateTime;
            endTime.Value = EndDateTime;
            btnSubmit.Enabled = true;
            StopDateTimeListeners = false;
        }

        private void LbEvent_MouseDown(object sender, MouseEventArgs e)
        {
            lbEvent.SelectedIndex = lbEvent.IndexFromPoint(e.X, e.Y);
        }

        private void RecurringChecked_Handler(object sender, EventArgs e)
        {
            if (!StopCBRecurringListener)
            {

                cEvent = new CalEvent();
                if (cbRecurring.Checked)
                {
                    endDate.Enabled = true;
                    //ev.SetStartDate(new DateTime(2019, 12, 01));
                    //ev.SetEndDate(new DateTime(2019, 12, 11));
                    RecurringForm RecForm = new RecurringForm();
                    RecForm.SetEvent(cEvent);


                }
                else
                {
                    endDate.Enabled = false;
                    cEvent.SetRecurringType("NIL");
                    cEvent.SetRecurring(false);
                }
            }
        }

        private void AppointmentChecked_Handler(object sender, EventArgs e)
        {
            if (cbAppointment.Checked)
            {
                lbPerson.Enabled = true;
                tbLocation.Enabled = true;
            }
            else
            {
                tbLocation.Enabled = false;
                lbPerson.Enabled = false;
            }
        }

        // gets recurringType string[] from RecurringTypePOPUP thru CalEvent
        public static void SetEvent(CalEvent e)
        {
            cEvent = e;
        }
        //deletes contact
        private void Del_Click(object sender, EventArgs e)
        {

            ArrayList eTotalList = XmlControl.GetEventsList();
            ArrayList eList = new ArrayList();
            //eList.Clear();
            int position = lbEvent.SelectedIndex;

            eList = SetEventsData(); 
            //Alert(position);
            try
            {
                ((CalEvent)eList[EventEditedId]).GetTitle();
            }
            catch (Exception ex)
            {
                Alert("Error: Nothing is Selected!");
                return;
            }

            for (int i = 0; i < eTotalList.Count; i++)
            {
                if (((CalEvent)eTotalList[i]).GetTitleAndStartTimeString() == ((CalEvent)eList[position]).GetTitleAndStartTimeString())
                {
                    eTotalList.RemoveAt(i);
                    eList.RemoveAt(position);
                    break;
                }
            }
            //eList.RemoveAt(position);
            EventsListToXml(eTotalList);

            //initListBox();
            Thread.Sleep(300);
            SetEventsData();
            calendarForm.Repaint();
        }


        public void EventsListToXml(ArrayList eList)
        {
            string workingDir = Directory.GetCurrentDirectory();
            string filePath = workingDir + @"\Events.xml";
            XElement doc = XElement.Load(filePath);

            doc.RemoveAll();
            doc.Save(filePath);

            for (int i = 0; i < eList.Count; i++)
            {
                XmlControl.AddEvent((CalEvent)eList[i]);
            }


        }
        public ArrayList XmlToContactArrayList()
        {
            string workingDir = Directory.GetCurrentDirectory();
            string filePath = workingDir + @"\Contacts.xml";

            XDocument doc = XDocument.Load(filePath);
            IEnumerable<XElement> enumContacts = doc.Elements("Contacts").Elements();


            ArrayList XmlToContactsList = new ArrayList();

            foreach (var xmlContacts in enumContacts)
            {
                Contact contact = new Contact();
                int id = Int32.Parse(xmlContacts.Element("id").Value);
                contact.SetId(id);
                contact.FName = xmlContacts.Element("fname").Value;
                contact.SName = xmlContacts.Element("lname").Value;
                contact.Address1 = xmlContacts.Element("address1").Value;
                contact.Address2 = xmlContacts.Element("address2").Value;
                contact.Postcode = xmlContacts.Element("postcode").Value;
                XmlToContactsList.Add(contact);
            }
            //Test line
            for (int i = 0; i < XmlToContactsList.Count; i++)
            {
                Console.WriteLine(((Contact)XmlToContactsList[i]).FName + ((Contact)XmlToContactsList[i]).SName);
            }


            return XmlToContactsList;
        }


        //read all contacts and populate list of contacts
        private ArrayList PopulateCotnactListBox()
        {
            string workingDir = Directory.GetCurrentDirectory();
            string filePath = workingDir + @"\Contacts.xml";

            if (!File.Exists(filePath))
            {
                XDocument createdoc = new XDocument(new XElement("Contacts"));
                createdoc.Save(filePath);
                Console.WriteLine("Contacts.xml created succesfully");
            }
            XDocument doc = XDocument.Load(filePath);

            IEnumerable<XElement> enumContacts = doc.Elements("Contacts").Elements();

            var NameList = new ArrayList();


            foreach (var xmlContacts in enumContacts)
            {
                //Console.WriteLine(xmlContacts);
                StringBuilder fullName = new StringBuilder();
                fullName.Append(xmlContacts.Element("fname").Value);
                fullName.Append(" ");
                fullName.Append(xmlContacts.Element("lname").Value);
                NameList.Add(fullName);
            }
            return NameList;
        }

        private Contact GetContactById(int id)
        {
            string workingDir = Directory.GetCurrentDirectory();
            string filePath = workingDir + @"\Contacts.xml";

            if (!File.Exists(filePath))
            {
                XDocument createdoc = new XDocument(new XElement("Contacts"));
                createdoc.Save(filePath);
                Console.WriteLine("Contacts.xml created succesfully");
            }
            XDocument doc = XDocument.Load(filePath);

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
                if (cId.ToString() == id.ToString())
                {
                    name = xmlContacts.Element("fname").Value;
                    sName = xmlContacts.Element("lname").Value;
                    addr1 = xmlContacts.Element("address1").Value;
                    addr2 = xmlContacts.Element("address2").Value;
                    pcode = xmlContacts.Element("postcode").Value;
                    Contact c = new Contact(name, sName, addr1, addr2, pcode);
                    c.SetId(id);
                    return c;
                }
            }
            MessageBox.Show("AddEvents.cs -> GetContactById failed to find contact");
            return null;
        }




    }
}
