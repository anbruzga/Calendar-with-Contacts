using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coursework2
{
    //Calendar Event
    public class CalEvent
    {
        private DateTime dateStart;
        private DateTime dateEnd;
        private string title;
        private bool appointment;
        private bool recurring;
        private string[] recurringType;
        private Contact person;
        private ArrayList recurringDates = new ArrayList();
        private string Location;
        private int Id;
        private bool recurDatesSet = false;

        /*
        public CalEvent(DateTime dateStart, DateTime dateEnd,
            bool appointment, bool recurring,
            string[] recurringType)
        {
            this.dateStart = dateStart;
            this.dateEnd = dateEnd;
            this.appointment = appointment;
            this.recurring = recurring;
            this.recurringType = recurringType;

        }*/

        public CalEvent() { }

        //Getters Setters

        public void SetId(int id)
        {
            Id = id;
        }
        public int GetId()
        {
            return Id;
        }


        public void SetLocation(string location)
        {
            Location = location;
        }
        public string GetLocation()
        {
            return Location;
        }
        public void SetTitle(string name)
        {
            this.title = name;
        }
        public string GetTitle()
        {
            return title;
        }
        public void SetRecurring(bool recurring)
        {
            this.recurring = recurring;
        }
        public bool IsRecurring()
        {
            return recurring;
        }

        public void SetAppointment(bool appointment)
        {
            this.appointment = appointment;
        }

        public bool IsAppointment()
        {
            return appointment;
        }

        public void SetStartDate(DateTime date)
        {
            dateStart = date;
        }
        public void SetEndDate(DateTime date)
        {
            dateEnd = date;
        }
        public void SetDate(DateTime DateStart, DateTime DateEnd)
        {
            SetEndDate(DateEnd);
            SetStartDate(DateStart);
        }
        public DateTime GetStartDate()
        {
            return dateStart;
        }

        public DateTime GetEndDate()
        {
            return dateEnd;
        }

        public DateTime[] GetDateRange()
        {
            //Start date and End date makes 2
            DateTime[] range = new DateTime[2];
            range[0] = dateStart;
            range[1] = dateEnd;
            return range;
        }

        public void SetContact(Contact person)
        {
            this.person = person;
        }

        public Contact GetContact()
        {
            return person;
        }

        public ArrayList GetDates()
        {

            if (!recurDatesSet && Object.Equals(recurringDates, default(ArrayList)))
                CalcRecurringDates();
 
            return recurringDates;
        }


        public ArrayList GetDates(DateTime minDate)
        {
            if (!recurDatesSet)
            {
                CalcRecurringDates();
            }
            ArrayList datesFrom = new ArrayList();
            foreach (DateTime d in recurringDates) {
                if (DateTime.Compare(minDate, d) <= 0) {
                    datesFrom.Add(d);
                }
            }
            return datesFrom;
        }

        public ArrayList GetDates(DateTime minDate, DateTime maxDate)
        {

            
                if (!recurDatesSet)
                {
                    CalcRecurringDates();
                }
                ArrayList datesFrom = new ArrayList();
                foreach (DateTime d in recurringDates)
                {
                    if (d > minDate && d < maxDate)
                    {
                        datesFrom.Add(d);
                    }

                }
            


            return datesFrom;
        }


        public string[] GetRecurringType()
        {
            return recurringType;
        }
        public bool SetRecurringType(String mtype)
        {
            if (mtype == "NIL" || mtype == null)
            {
                recurring = false;
            }
            //recurringType = new String[1];
            else
            {
                recurring = true;
            }
            string[] recType = new string[1];
            recType[0] = mtype;
            recurringType = recType;
            //recurringType[0] = "default";

            //bool TypeOk = true;
            //if (mtype == "Daily") recurringType[0] = "Daily";
            //else if (mtype.Equals("Weekly")) recurringType[0] = "Weekly";
            //else if (mtype.Equals("Monday")) recurringType[0] = "Monday";
            //else if (mtype.Equals("Tuesday")) recurringType[0] = "Tuesday";
            //else if (mtype.Equals("Wednesday")) recurringType[0] = "Wednesday";
            //else if (mtype.Equals("Thursday")) recurringType[0] = "Thursday";
            //else if (mtype.Equals("Friday")) recurringType[0] = "Friday";
            //else if (mtype.Equals("Saturday")) recurringType[0] = "Saturday";
            //else if (mtype.Equals("Sunday")) recurringType[0] = "Sunday";
            //else TypeOk = false;

            //CalcRecurringDates();
            return true;

        }

        public bool SetRecurringType(string[] type)
        {
            bool recurs = false; ;
            if (type.Length != 0)
            {
                foreach (string t in type)
                {
                    if (!(t.ToString() == "NIL" || t.ToString() == null))
                    {
                        recurs = true;
                        break;
                    }
                }
                recurring = recurs;
            }
            else recurring = false;
            //recurringType = new string[type.Length];
            //Boolean ValuesCorrect = true;
            //for (int i = 0; i < type.Length; i++)
            //{
            //    if (type[i].Equals("Daily"))
            //    {
            //        recurringType[i] = "Daily";
            //    }
            //    else if (type[i].Equals("Weekly"))
            //    {
            //        recurringType[i] = "Weekly";
            //    }
            //    else if (type[i].Equals("Monday"))
            //    {
            //        recurringType[i] = "Monday";
            //    }
            //    else if (type[i].Equals("Tuesday"))
            //    {
            //        recurringType[i] = "Tuesday";
            //    }
            //    else if (type[i].Equals("Wednesday"))
            //    {
            //        recurringType[i] = "Wednesday";
            //    }
            //    else if (type[i].Equals("Thursday"))
            //    {
            //        recurringType[i] = "Thursday";
            //    }
            //    else if (type[i].Equals("Friday"))
            //    {
            //        recurringType[i] = "Friday";
            //    }
            //    else if (type[i].Equals("Saturday"))
            //    {
            //        recurringType[i] = "Saturday";
            //    }
            //    else if (type[i].Equals("Sunday"))
            //    {
            //        recurringType[i] = "Sunday";
            //    }
            //    else ValuesCorrect = false;
            //
            //
            //}

            //for (int i = 0; i < type.Length; i++)
            //{
            //    if (recurringType[i] != null) 
            //    MessageBox.Show(recurringType[i]+ "reccurringType[" + i.ToString());
            //}
            recurringType = type;

            //CalcRecurringDates();
            return true;
        }


       


        /*
         * take start date
        see if the start date belongs
        if it belongs add to the list
        increment start date by 1 day

        how to know if it belongs:
        getweekday -> compare it to all values of recurring array
         * */
        public void CalcRecurringDates()
        {
            //Console.WriteLine("loop1");
            DateTime tempDate = new DateTime(dateStart.Year, dateStart.Month,
                dateStart.Day, dateStart.Hour, dateStart.Minute, dateStart.Second);

            bool foundAll = false;
            //      || recurringType[0] != "Weekly")) ;
            if (!recurring && (Object.Equals(recurringType, default(string[]))))
            {
                while (!foundAll)
                {
                    recurringDates.Add(tempDate);
                    tempDate = DateUtil.AddDay(tempDate);

                    if (DateUtil.DatesEqual(tempDate.AddDays(-1), dateEnd))
                    {
                        foundAll = true;
                    }
                }
            }


            while (!foundAll) // what if start and end dates are reversed or equal?
                              //what if selected weekly and daily and mondays for ex?
            {
                if (recurringType.Contains("Weekly"))
                {
                    recurringDates.Add(tempDate);

                    tempDate = DateUtil.AddDay(tempDate, 7);
                    //Console.WriteLine("loop2");
                }

                else if (((recurringType.Length > 1) && recurringType[1].ToString().Contains("Daily"))
                    || !recurring)
                {
                   
                    recurringDates.Add(tempDate);
                    tempDate = DateUtil.AddDay(tempDate);
                    //Console.WriteLine("loop3");
                }

                //else if (Array.Exists(recurringType, element => element == tempDate.DayOfWeek.ToString()))
                else if (recurringType.Contains(tempDate.DayOfWeek.ToString()))
                {
                    DateTime recurringDay = new DateTime(tempDate.Year, tempDate.Month, tempDate.Day);
                    recurringDates.Add(recurringDay);
                    tempDate = DateUtil.AddDay(tempDate);
                    if (DateUtil.DatesEqual(tempDate.AddDays(-1), dateEnd))
                    {
                        foundAll = true;
                        break;
                    }
                }
                else
                {
                    tempDate = DateUtil.AddDay(tempDate);
                }

                if (tempDate >= dateEnd)
                {
                    foundAll = true;
                    break;
                }




            }

        }

        public void InitNullValues()
        {
            //DateTime dateStart; // cant be empty
            //DateTime dateEnd; // cant be empty
            // bool appointment; default value is false
            // bool recurring; default value is false
            //if(recurringDates.Count == 0);

            if (title.Equals(null)) title = "NIL";
            if (recurringType == null)
            {
                recurringType = new String[1];
                recurringType[0] = "NIL";
            }
            if (person==null) person = new Contact("NIL", "NIL", "NIL", "NIL", "NIL");
            if(Location == null) Location = "NIL";
        }

        public override string ToString()
        {
            InitNullValues();
            StringBuilder str = new StringBuilder();
            str.Append("Title: " + title + "\n");
            str.Append("Start Date: " + dateStart.ToString() + "\n");
            str.Append("End Date: " + dateEnd.ToString() + "\n");
            str.Append("Recurring: " + recurring.ToString() + "\n");
            str.Append("Appointment:  " + appointment.ToString() + "\n");
            str.Append("Recurring Type: ");
            if (recurring)
                foreach (var item in recurringType)
                {
                    str.Append(item + ",");
                }
            else { str.Append("NIL"); }
            str.Append("\n");


            str.Append("Contact " + person.ToString() + "\n");
            str.Append("location " + Location+ "\n");
            str.Append("id " + Id.ToString() + "\n");


            return str.ToString();
        }
        public string GetRecurringTypeString()
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < recurringType.Length; i++)
            {
                str.Append(recurringType[i].ToString() + "\n");
            }
            str.Append("recurring: " + recurring);
            return str.ToString() ;
        }
        public string GetTitleAndStartTimeString()
        {
            StringBuilder str = new StringBuilder();
            str.Append("title: " + title + "\n");
            str.Append("time:");
            if(Int32.Parse(dateStart.Hour.ToString()) < 10)
                str.Append("0" + dateStart.Hour.ToString() + ":");
            else
            {
                str.Append(dateStart.Hour.ToString() + ":");
            }
            if(Int32.Parse(dateStart.Minute.ToString()) < 10)
                str.Append("0" + dateStart.Minute.ToString());
            else
                str.Append(dateStart.Minute.ToString());

            return str.ToString();
        }
    }
}
