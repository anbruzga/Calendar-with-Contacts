using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework2
{
    /** 
    * This class is used for checking how much the of day is occupied
    * Each day consists of 1440 minutes
    * Each minute can be occupied or unoccupied
    * Minutes array is for that reason.
    * 
    * This logic prevents possible occupation of more than 24 hours. 
    * That would be possible, because calendar design allows overlapping events
    * 
    */
    public class DayOccupied
    {
        //VARIABLES
        private bool[] Minutes = new bool[1440]; // minutes in day
        public DateTime DateStart { get; set; }

        // CONSTRUCTOR
        public DayOccupied() { }


        //Acccepts start date and end date
        //Sets the Minutes array for values TRUE which corresponds
        //to minutes, between Start Date Time and End Date Time
        //The seconds are ignored.
        public void SetInterval(DateTime StartDate, DateTime EndDate)
        {
            int Start = StartDate.Hour * 60 + EndDate.Minute;
            int End = EndDate.Hour * 60 + EndDate.Minute;
            for (int i = Start; i <= End; i++)
            {
                Minutes[i] = true;
            }
        }

        // Converts Minutes boleans into hours and returns it
        // Commented out version is for returning percentage of
        // how much of 24h is occupied
        public double GetHoursUsed()
        {
            int Occupied = 0;
            //double Percentage;

            for (int i = 0; i < Minutes.Length; i++)
            {
                if(Minutes[i] == true)
                {
                    Occupied++;
                }
            }

            //Percentage = Occupied * 100 / 1440;
            //Percentage = Occupied * 10 / 144;

            ////Convert the percentage to hours
            //return (Percentage*24/100);

            //Returns hours
            return Occupied / 60;
        }

    }
}
