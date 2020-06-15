using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coursework2
{

    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new ContactsForm());

            
            Program program = new Program();
            program.Init();
        }

        public void Init()
        {

            //the true one
            var form = new CalendarForm();
            form.ShowDialog();




            ///AddEvent ad = new AddEvent();
            // ad.TopLevel = false;
            // ad.Show();
            //  ad.Visible = true;

            //PredictionForm pf = new PredictionForm();

            //pf.TopLevel = false;

            //PredictionForm f = new PredictionForm();
            //f.TopLevel = false;


            //TableLayoutPanel panel = new TableLayoutPanel();
            //panel.ColumnCount = 2;
            //panel.RowCount = 1;

            //panel.Controls.Add(pf,0,0);
            //panel.Controls.Add(f,1,0);
            //panel.ClientSize = new System.Drawing.Size(1000, 1000);
            //panel.Visible = true;
            //panel.Show();



            //Test();

            //debugging
            //var form = new AddEvent();
            //form.ShowDialog();
            //var form = new RecurringForm();

            //var form = new PredictionForm();
            //form.ShowDialog();

        }

        public void Test()
        {

            //CalendarEventTest();
            //CalEvent e = CalendarEventTest();
            //XmlControlTest(e);

            //ArrayList ar = XmlControl.GetEventsList();
            //for (int i = 0; i < ar.Count; i++)
            //{
            //    MessageBox.Show(((CalEvent)ar[i]).ToString());
            //}
        }

        public void XmlControlTest(CalEvent e)
        {
            XmlControl.CreateEventsXml();
            XmlControl.AddEvent(e);

        }
        public CalEvent CalendarEventTest()
        {
            // Testing Calendar Event
            CalEvent e = new CalEvent();
            e.SetLocation("Brussels");
            e.SetTitle("Meeting with Zosha");

            e.SetRecurring(true);
            e.SetStartDate(new DateTime(2019, 12, 12));
            e.SetEndDate(new DateTime(2020, 01, 01));
            e.InitNullValues();
            // String[]  arr  ={ "Tuesday", "Monday", "Sunday" };

            e.SetRecurringType("Friday");
            var dates = e.GetDates();

            StringBuilder str = new StringBuilder();
            foreach (var item in dates)
            {
                Console.WriteLine(item.ToString());
                str.Append(item + "\n");
            }
            MessageBox.Show(str.ToString());

            return e;
        }

    }
}
