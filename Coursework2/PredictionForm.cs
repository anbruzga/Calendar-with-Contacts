using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections;
using System.Windows.Forms;

namespace Coursework2
{
    public partial class PredictionForm : Form
    {
        private int DaysInMonth;
        private double[] HoursArr;
        private ObservablePoint[] Points;
        private ArrayList EvList;
        public PredictionForm()
        {
            InitializeComponent();
            InitVariables();
            Prediction();
            InitObservablePoint();
            PredictionChart.Series = new SeriesCollection
            {
                new LineSeries
                {

                    Values = new ChartValues<ObservablePoint>
                    {

                       Points[0],
                       Points[1],
                       Points[2],
                       Points[3],
                       Points[4],
                       Points[5],
                       Points[6],
                       Points[7],
                       Points[8],
                       Points[9],
                       Points[10],
                       Points[11],
                       Points[12],
                       Points[13],
                       Points[14],
                       Points[15],
                       Points[16],
                       Points[17],
                       Points[18],
                       Points[19],
                       Points[20],
                       Points[21],
                       Points[22],
                       Points[23],
                       Points[24],
                       Points[25],
                       Points[26],
                       Points[27],
                       Points[28],
                       Points[29],
                      // Points[30],
                    },
                    PointGeometrySize = 15
                }
            };

        }



        private void Alert(Object t)
        {
            MessageBox.Show(t.ToString());
        }


        private void Prediction()
        {
            // Set sample size from (CurrentDate - 3 Months) to Current Date
            DateTime CurrentDate = new DateTime(DateTime.Today.Year,
                DateTime.Today.Month, DateTime.Today.Day,0,0,0);
            int FirstDay = -90;
            DateTime SampleStartDate = DateUtil.AddDay(CurrentDate, FirstDay);




            //int i = 0;
            // Crop all dates by sample size;
            //foreach(ArrayList ar in AllDates)
            //{
            //    foreach (CalEvent ev in ar)
            //    {
            //        HoursArr[i] = ev.GetHoursBetweenStartAndEndTime();
            //        i++;
            //    }
            //}


            // Set DateTime Array of 90
            int timespan = (CurrentDate - SampleStartDate).Days;
            DateTime[] dates = new DateTime[timespan];
            DayOccupied[] OccupDayArr = new DayOccupied[timespan];

            // Initialise dates and OccupiedDay array;
            for (int j = 0; j < dates.Length; j++)
            {
                dates[j] = new DateTime();
                dates[j] = DateUtil.AddDay(SampleStartDate, j);
                OccupDayArr[j] = new DayOccupied();
                OccupDayArr[j].DateStart = dates[j];
            }


            ArrayList AllDates = new ArrayList();
            //int i = 0;

            int DateIndex;
            
            
            foreach (CalEvent e in EvList)
            {
                int Incrementer = 0;
                //ArrayList EventDates = e.GetDates(SampleStartDate, CurrentDate);
                //AllDates.Add(EventDates);
                ArrayList EventDateTimes = e.GetDates();
                
                ArrayList EventDates = new ArrayList();
                EventDates = CutTimes(EventDateTimes);
                DateTime SampleStartDateTemp = new DateTime(SampleStartDate.Year,
                    SampleStartDate.Month, SampleStartDate.Day, 0, 0, 0);
                while (Incrementer < 90)
                {
                     //MessageBox.Show(SampleStartDate.ToString());
                    if (EventDates.Contains(SampleStartDateTemp))
                    {
                        DateIndex = EventDates.IndexOf(SampleStartDateTemp);
                        //MessageBox.Show(EventDates[DateIndex].ToString());
                        OccupDayArr[Incrementer].SetInterval(e.GetStartDate(), e.GetEndDate());
                        Incrementer++;
                        SampleStartDateTemp = SampleStartDateTemp.AddDays(1);

                    }
                    else
                    {
                        Incrementer++;
                        SampleStartDateTemp = SampleStartDateTemp.AddDays(1);
                    }
                }
            }

            //foreach (var e in OccupDayArr)
            //{
            //    MessageBox.Show(e.GetHoursUsed().ToString() + " : " + e.DateStart.ToString());
            //}
            bool FirstMonthOccupied = false; // First month from the sample is current month - 3 months
            bool SecondMonthOccupied = false;
            bool ThirdMonthOccupied = false;
            
            for (int i = 0; i < 28; i++)
            {
                if (OccupDayArr[i].GetHoursUsed() != 0)
                {
                    FirstMonthOccupied = true;
                    break;
                }
            }
            for (int i = 29; i < 56; i++)
            {
                if (OccupDayArr[i].GetHoursUsed() != 0)
                {
                    SecondMonthOccupied = true;
                    break;
                }
            }
            for (int i = 57; i < 84; i++)
            {
                if (OccupDayArr[i].GetHoursUsed() != 0)
                {
                    ThirdMonthOccupied = true;
                    break;
                }
            }

            if (ThirdMonthOccupied && SecondMonthOccupied && FirstMonthOccupied)
            {
                Alert("Three months taken");
                for (int i = 0; i < HoursArr.Length; i++)
                {
                    
                    HoursArr[i] = OccupDayArr[i].GetHoursUsed()
                        + OccupDayArr[i + 28].GetHoursUsed() // 28 is after 4 weeks
                        + OccupDayArr[i + 56].GetHoursUsed();

                    HoursArr[i] = HoursArr[i] / 3;


                    //MessageBox.Show(HoursArr[i].ToString() + " : " + i.ToString() + "\n"
                    // + OccupDayArr[i].GetHoursUsed() + " : " + OccupDayArr[i].DateStart.ToString() + "\n"
                    // + OccupDayArr[i + 29].GetHoursUsed() + " : " + OccupDayArr[i + 29].DateStart.ToString() + "\n"
                    // + OccupDayArr[i + 59].GetHoursUsed() + " : " + OccupDayArr[i + 59].DateStart.ToString());


                    Points[i] = new ObservablePoint(i, HoursArr[i]);
                    //this.Refresh();

                }
            }

            else if(ThirdMonthOccupied && SecondMonthOccupied)
            {
                Alert("One two months taken");
                for (int i = 0; i < HoursArr.Length; i++)
                {
                    
                    HoursArr[i] = OccupDayArr[i + 28].GetHoursUsed() // 28 is after 4 weeks
                        + OccupDayArr[i + 56].GetHoursUsed();

                    HoursArr[i] = HoursArr[i] / 2;


                    //MessageBox.Show(HoursArr[i].ToString() + " : " + i.ToString() + "\n"
                    // + OccupDayArr[i].GetHoursUsed() + " : " + OccupDayArr[i].DateStart.ToString() + "\n"
                    // + OccupDayArr[i + 29].GetHoursUsed() + " : " + OccupDayArr[i + 29].DateStart.ToString() + "\n"
                    // + OccupDayArr[i + 59].GetHoursUsed() + " : " + OccupDayArr[i + 59].DateStart.ToString());


                    Points[i] = new ObservablePoint(i, HoursArr[i]);
                    //this.Refresh();

                }
            }

            else if (ThirdMonthOccupied)
            {
                Alert("One month taken");
                for (int i = 0; i < HoursArr.Length; i++)
                {

                    HoursArr[i] =  OccupDayArr[i + 56].GetHoursUsed();

                    //MessageBox.Show(HoursArr[i].ToString() + " : " + i.ToString() + "\n"
                    // + OccupDayArr[i].GetHoursUsed() + " : " + OccupDayArr[i].DateStart.ToString() + "\n"
                    // + OccupDayArr[i + 29].GetHoursUsed() + " : " + OccupDayArr[i + 29].DateStart.ToString() + "\n"
                    // + OccupDayArr[i + 59].GetHoursUsed() + " : " + OccupDayArr[i + 59].DateStart.ToString());


                    Points[i] = new ObservablePoint(i, HoursArr[i]);
                    //this.Refresh();

                }
            }
            else
            {
                for (int i = 0; i < Points.Length; i++)
                {
                    Points[i] = new ObservablePoint(i, 0);
                    Alert("No data.");
                }
            }

            //MessageBox.Show("Finished");

        }

        private ArrayList CutTimes(ArrayList eventDateTimes)
        {
            ArrayList EventDates = new ArrayList();
            

            foreach(DateTime e in eventDateTimes)
            {
                DateTime DateNoTime = new DateTime(e.Year, e.Month, e.Day, 0, 0, 0);
                EventDates.Add(DateNoTime);
            }
            return EventDates;
        }

        private void InitVariables()
        {
            DaysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            HoursArr = new double[DaysInMonth];
            Points = new ObservablePoint[DaysInMonth];
            EvList = XmlControl.GetEventsList();
            foreach (CalEvent e in EvList)
            {
                e.CalcRecurringDates();
            }
        }
        

        private void InitObservablePoint()
        {
            
        }

        private void cartesianChart1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }
    }
}
