using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Coursework2
{
    public partial class ContactsForm : Form
    {
        private AddEvent addEvent;

        public ContactsForm()
        {
            InitializeComponent();
        }

        public ContactsForm(AddEvent addEvent)
        {
            this.addEvent = addEvent;
            InitializeComponent();
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            btnSubmit.Text = "Save";
            int position = lbContact.SelectedIndex;
            ArrayList ContactsList = new ArrayList();
            ContactsList.Clear();
            ContactsList = XmlToContactArrayList();

            tbFName.Text = ((Contact)ContactsList[position]).FName;
            tbSName.Text = ((Contact)ContactsList[position]).SName;
            tbAddress1.Text = ((Contact)ContactsList[position]).Address1;
            tbAddress2.Text = ((Contact)ContactsList[position]).Address2;
            tbPostcode.Text = ((Contact)ContactsList[position]).Postcode;
            contactEditedId = position;
        }
        private void MenuItemClickHandler(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;

            // MessageBox.Show(sender.ToString()); //test line
            if (sender.ToString().Equals("Calendar"))
            {
                //eventually, not used. Refactor
            }
            if (sender.ToString().Equals("Prediction"))
            {
                //eventually, not used. Refactor
            }
        }


        //  refactor
        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            if (btnSubmit.Text != "Save")
            {
                if (!(tbFName.Text == String.Empty))
                {
                    try
                    {
                        Contact contact = new Contact(tbFName.Text, tbSName.Text, tbAddress1.Text,
                            tbAddress2.Text, tbPostcode.Text);
                        WriteToXml(contact);
                        initListBox();


                    }
                    catch (ArgumentNullException ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    catch (Exception ex) { Console.WriteLine(ex.ToString()); }
                }
                else
                {
                    MessageBox.Show("Name is mandatory!");
                }
            }
            else
            {

                if (tbFName.Text == String.Empty)
                {
                    MessageBox.Show("Name is mandatory!");
                    return;
                }
                ArrayList contactList = XmlToContactArrayList();
                contactList[contactEditedId] = new Contact(tbFName.Text, tbSName.Text, tbAddress1.Text,
                            tbAddress2.Text, tbPostcode.Text);
                ContactsListToXml(contactList);
                initListBox();
                btnSubmit.Text = "Add New Contact!";
                for (int i = 0; i < tbArr.Length; i++)
                {
                    tbArr[i].Text = "";
                }
            }

            // if sent here by Addevent, update a list there..
            if (!Object.Equals(addEvent, default(AddEvent)))
            {
                addEvent.UpdateContactList();
            }
        }


        private void WriteToXml(Contact contact)
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


            //asd
            // Xml exists now
            //MessageBox.Show(contact.GetId().ToString());
            // Adding a child to the xml 
            doc = XDocument.Load(filePath);

            //int id = Int32.Parse(xmlContacts.Element("id").Value);
            //contact.SetId(id);
            // MessageBox.Show((contact.GetId()).ToString());
            doc.Element("Contacts").Add(new XElement("contact",
                                        new XElement("id", new XText(contact.GetId().ToString())),
                                        new XElement("fname", new XText(contact.FName)),
                                        new XElement("lname", new XText(contact.SName)),
                                        new XElement("address1", new XText(contact.Address1)),
                                        new XElement("address2", new XText(contact.Address2)),
                                        new XElement("postcode", new XText(contact.Postcode))));
            doc.Save(filePath);
        }
        private void lbContact_MouseDown(object sender, MouseEventArgs e)
        {
            lbContact.SelectedIndex = lbContact.IndexFromPoint(e.X, e.Y);
        }

        //deletes contact
        private void Del_Click(object sender, EventArgs e)
        {
            ArrayList ContactsList = new ArrayList();
            ContactsList.Clear();
            int position = lbContact.SelectedIndex;

            ContactsList = XmlToContactArrayList();
            ContactsList.RemoveAt(position);
            ContactsListToXml(ContactsList);
            MessageBox.Show(lbContact.SelectedItem.ToString() + " removed.");
            initListBox();
        }

        public void ContactsListToXml(ArrayList cList)
        {
            string workingDir = Directory.GetCurrentDirectory();
            string filePath = workingDir + @"\Contacts.xml";
            XElement doc = XElement.Load(filePath);

            doc.RemoveAll();

            doc.Save(filePath);

            for (int i = 0; i < cList.Count; i++)
            {
                WriteToXml((Contact)cList[i]);
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
                //MessageBox.Show("XmlToContactsList id " + id);
                contact.SetId(id);
                contact.FName = xmlContacts.Element("fname").Value;
                contact.SName = xmlContacts.Element("lname").Value;
                contact.Address1 = xmlContacts.Element("address1").Value;
                contact.Address2 = xmlContacts.Element("address2").Value;
                contact.Postcode = xmlContacts.Element("postcode").Value;
                XmlToContactsList.Add(contact);
            }
            //Test line
            //for (int i = 0; i < XmlToContactsList.Count; i++)
            // {
            //Console.WriteLine(((Contact)XmlToContactsList[i]).FName + ((Contact)XmlToContactsList[i]).SName);
            //}


            return XmlToContactsList;
        }


        //read all contacts and populate list of contacts
        private ArrayList PopulateListBox()
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
    }
}
