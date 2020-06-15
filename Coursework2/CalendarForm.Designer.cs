using System;
using System.Collections;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coursework2
{

    partial class CalendarForm
    {
        private ToolStrip menu;
        private DoubleBufferedTableLayoutPanel frame = new DoubleBufferedTableLayoutPanel();
        private Button NextBtn, PreviousBtn;
        protected static Label MonthLabel;
        private DateTime CurrentMonth;
        private CalSquare[] squares;
        private CalSquare[] emptySquares;
        private static Label[] weekdayLabels;
        private static DoubleBufferedTableLayoutPanel weekdaysRow;
        private static Panel[] headers;



        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            //this.ClientSize = new System.Drawing.Size(1003, 578);
            this.ClientSize = new Size(1504, 867);
            this.Text = "Calendar";
            //this.MaximizeBox = false;
            //this.MinimizeBox = false;
            this.ClientSizeChanged += new EventHandler(OnSizeChange);
            CurrentMonth = DateTime.Today;
            Init();
        }

        #endregion



        public void Init()
        {
            InitFrame();
            frame.SuspendLayout();
            this.SuspendLayout();
            this.Controls.Add(frame);


            InitMenu();
            frame.Controls.Add(menu, 1, 1);
            frame.SetColumnSpan(menu, 7);

            InitButtons();
            frame.Controls.Add(PreviousBtn, 1, 2);

            frame.Controls.Add(NextBtn, 6, 2);
            frame.SetColumnSpan(NextBtn, 2);
            frame.SetColumnSpan(PreviousBtn, 2);

            InitMonthLabel();
            frame.Controls.Add(MonthLabel, 3, 2);
            frame.SetColumnSpan(MonthLabel, 3);

            InitCalSquares();

            InitCalendarHeader();
            frame.ResumeLayout();
            this.ResumeLayout();

        }

        public void InitButtons()
        {
            NextBtn = new Button();
            PreviousBtn = new Button();
            NextBtn.Text = ">> Next >>";
            PreviousBtn.Text = "<< Previous <<";
            NextBtn.Font = new Font(FontFamily.GenericSansSerif, 12);
            PreviousBtn.Font = new Font(FontFamily.GenericSansSerif, 12);
            Size BtnSize = new Size((int)(ClientSize.Width * 0.1462765), 30);

            NextBtn.Size = PreviousBtn.Size = BtnSize;
            //PreviousBtn.Size = new Size(220, 30);
            NextBtn.Anchor = (AnchorStyles.Right);
            PreviousBtn.Anchor = AnchorStyles.Left;

            NextBtn.ForeColor = Color.FromArgb(255, 42, 77, 145);
            PreviousBtn.ForeColor = Color.FromArgb(255, 42, 77, 145);
            PreviousBtn.Click += new EventHandler(PreviousBtnHandler);
            NextBtn.Click += new EventHandler(NextBtnHandler);

            // creating a 0 size button - a dirty hack to avoid auto-focus on previousButton
            Button dummyButton = new Button();
            dummyButton.Size = new Size(0, 0);
            dummyButton.Text = "Dummy Button";
            frame.Controls.Add(dummyButton);
            dummyButton.Focus();
        }

        private void DelSquares()
        {
            for (int i = 0; i < 6; i++)
            {
                try
                {
                    frame.Controls.Remove(emptySquares[i].GetSquare());
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(i.ToString());
                }
            }
            for (int i = 0; i < 31; i++)
            {
                try
                {

                    frame.Controls.Remove(squares[i].GetSquare());
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(i.ToString());
                }
            }
        }


        public void Repaint()
        {
            frame.SuspendLayout();

            //DelSquares();
            //InitCalSquares();
            frame.Controls.Clear();
            Init();
            frame.ResumeLayout();
        }
        public void InitMonthLabel()
        {
            MonthLabel = new Label();

            StringBuilder labelText = new StringBuilder();
            labelText.Append(CurrentMonth.ToString("MMMM")
                + " " + CurrentMonth.ToString("yyyy"));
            MonthLabel.Text = labelText.ToString(); // 

            MonthLabel.Font = new Font(FontFamily.GenericSansSerif, 15);
            MonthLabel.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top);
            MonthLabel.Width = (int)(frame.Width / 2.5);
            MonthLabel.Height = 35;
            MonthLabel.Left = (this.ClientSize.Width - MonthLabel.Width) / 2;
            MonthLabel.Top = (this.ClientSize.Height - MonthLabel.Height) / 2;
            MonthLabel.TextAlign = ContentAlignment.MiddleCenter;
            //MonthLabel.BorderStyle = BorderStyle.FixedSingle;
            MonthLabel.ForeColor = Color.FromArgb(255, 42, 77, 145);

        }


        // Nothing like initialising calendar squares in  two tripple-nested loops!
        public void InitCalSquares()
        {

            int daysInMonth = DateUtil.DaysInMonth(CurrentMonth.Year, CurrentMonth.Month);
            int firstWeekDay = DateUtil.FirstWeekDay(CurrentMonth.Year, CurrentMonth.Month);
            squares = new CalSquare[daysInMonth];
            emptySquares = new CalSquare[7];
            int dayIndex = 1;


            ArrayList Events = XmlControl.GetEventsList();




            DateTime tempDate = CurrentMonth;
            //DateTime LastMonthDay = new DateTime(CurrentMonth.Year, CurrentMonth.Month,
            //    DateTime.DaysInMonth(CurrentMonth.Year, CurrentMonth.Month));
            //
            for (int i = 0; i < daysInMonth; i++)
            {
                squares[i] = new CalSquare(dayIndex);
                squares[i].SetDate(new DateTime(CurrentMonth.Year, CurrentMonth.Month, i + 1));
                squares[i].GetSquare().Click += new EventHandler(SquareHandler);
                squares[i].GetLabel1().Click += new EventHandler(SquareHandler);
                squares[i].GetLabel2().Click += new EventHandler(SquareHandler);
                squares[i].GetDayLabel().Click += new EventHandler(SquareHandler);
                dayIndex++;

                //foreach (CalEvent e in Events) // refactor to for loops, they re faster
                for (int j = 0; j < Events.Count; j++)
                {
                    // gets dates which are before current date 
                    ArrayList dates = ((CalEvent)Events[j]).GetDates(new DateTime(CurrentMonth.Year, CurrentMonth.Month, i + 1));

                    //foreach (var d in dates)
                    for (int x = 0; x < dates.Count; x++)
                    {
                        if (DateUtil.DatesEqual((DateTime)dates[x],
                            (new DateTime(CurrentMonth.Year, CurrentMonth.Month, i + 1))))
                        {

                            squares[i].AddEvent(((CalEvent)Events[j]).GetTitleAndStartTimeString());
                        }
                    }
                }

            }

            Boolean noLabel = true;
            for (int i = 0; i < 6; i++)
            {
                emptySquares[i] = new CalSquare(noLabel);
            }


            int index = 0;
            bool firstCycle = true;
            int tempIndex;
            for (int i = 3; i < 10; i++) // rows
            {
                for (int j = 1; j <= 7; j++) // columns
                {
                    if (index >= daysInMonth) return; // stop all the nonsense if run out of days

                    if (firstCycle) // if first cycle, add empty squares
                    {
                        tempIndex = index; // holding this as its needed for real squares
                        while (j < firstWeekDay) // adding dummy squares for the first row
                        {
                            frame.Controls.Add(emptySquares[index].GetSquare(), j, i);
                            j = j + 1;
                            index++;
                        }
                        firstCycle = false;
                        index = tempIndex; // reseting index for real squares
                    }

                    if (j == 7) // for sundays set color
                    {
                        squares[index].GetSquare().BackColor = Color.FromArgb(255, 255, 243, 230);
                    }
                    else
                    {
                        squares[index].GetSquare().BackColor = Color.FromArgb(255, 227, 223, 213);
                    }

                    frame.Controls.Add(squares[index].GetSquare(), j, i);
                    index++;


                }

            }
            //NoDaysLeft:;

        }

        private void InitFrame()
        {
            frame.Size = this.Size;
            frame.Dock = DockStyle.Fill;
            //frame.Show();
            frame.ColumnCount = 7;
            frame.RowCount = 9;
            frame.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.28F));
            frame.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.28F));
            frame.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.28F));
            frame.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.28F));
            frame.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.28F));
            frame.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.28F));
            frame.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.28F));
            frame.RowStyles.Add(new RowStyle(SizeType.Percent, 0.0F));
            frame.RowStyles.Add(new RowStyle(SizeType.Percent, 0.0F));
            frame.RowStyles.Add(new RowStyle(SizeType.Percent, 3.0F));
            frame.RowStyles.Add(new RowStyle(SizeType.Percent, 4.0F));
            frame.RowStyles.Add(new RowStyle(SizeType.Percent, 5.28F));
            frame.RowStyles.Add(new RowStyle(SizeType.Percent, 14.28F));
            frame.RowStyles.Add(new RowStyle(SizeType.Percent, 14.28F));
            frame.RowStyles.Add(new RowStyle(SizeType.Percent, 14.28F));
            frame.RowStyles.Add(new RowStyle(SizeType.Percent, 14.28F));
            frame.RowStyles.Add(new RowStyle(SizeType.Percent, 14.28F));
            frame.RowStyles.Add(new RowStyle(SizeType.Percent, 14.28F));



            frame.BorderStyle = BorderStyle.FixedSingle;

        }
        private void InitMenu()
        {
            menu = new ToolStrip();

            ToolStripMenuItem[] items = new ToolStripMenuItem[2];

            items[0] = new ToolStripMenuItem();
            items[0].Text = "Contacts";
            items[0].Click += new EventHandler(MenuItemClickHandler);
            items[1] = new ToolStripMenuItem();
            items[1].Text = "Prediction";
            items[1].Click += new EventHandler(MenuItemClickHandler);

            menu.Items.AddRange(items);
        }



        private void InitCalendarHeader()
        {

            weekdayLabels = new Label[7];// seven weekdayLabels
            Color foreColor = Color.FromArgb(255, 61, 0, 85);
            Color backgrColor = Color.FromArgb(255, 227, 223, 213);
            Color SundayForeColor = Color.FromArgb(255, 204, 0, 0);
            Color SundayBackColor = Color.FromArgb(255, 255, 243, 230);
            for (int i = 0; i < weekdayLabels.Length; i++)
            {
                weekdayLabels[i] = new Label();

                weekdayLabels[i].Font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold);

                weekdayLabels[i].TextAlign = ContentAlignment.MiddleCenter;
                weekdayLabels[i].Anchor = (AnchorStyles.Left | AnchorStyles.Right);
                weekdayLabels[i].AutoSize = false;

                weekdayLabels[i].BorderStyle = BorderStyle.FixedSingle;

                weekdayLabels[i].ForeColor = foreColor;
                weekdayLabels[i].BackColor = backgrColor;

            }
            //Set Label Anchor to Left and Right
            //Set Label AutoSize to false;
            //Set Label TextAlign to MiddleCenter;

            weekdayLabels[6].BackColor = SundayBackColor;
            // asigning weekday strings
            {
                weekdayLabels[0].Text = "Monday";
                weekdayLabels[1].Text = "Tuesday";
                weekdayLabels[2].Text = "Wednesday";
                weekdayLabels[3].Text = "Thursday";
                weekdayLabels[4].Text = "Friday";
                weekdayLabels[5].Text = "Saturday";
                weekdayLabels[6].Text = "Sunday";
            }

            weekdaysRow = new DoubleBufferedTableLayoutPanel();
            weekdaysRow.RowCount = 1;
            weekdaysRow.ColumnCount = 7;
            float ColumnSize = 14.28f;
            weekdaysRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, ColumnSize));
            weekdaysRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, ColumnSize));
            weekdaysRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, ColumnSize));
            weekdaysRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, ColumnSize));
            weekdaysRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, ColumnSize));
            weekdaysRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, ColumnSize));
            weekdaysRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, ColumnSize));

            weekdaysRow.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top);

            weekdaysRow.Dock = DockStyle.Fill;
            weekdaysRow.Size = new System.Drawing.Size(995, 50);
            weekdaysRow.Height = 35;
            weekdaysRow.BorderStyle = BorderStyle.FixedSingle;
            weekdaysRow.BackColor = Color.FromArgb(255, 206, 201, 192);

            headers = new Panel[7]; // seven panels for each weekday label
            
            for (int i = 0; i < headers.Length; i++)
            {
                headers[i] = new Panel();
                //headers[i].Dock = DockStyle.Top;
                headers[i].Size = new Size(991 / 7 - 5, 25);
                headers[i].BorderStyle = BorderStyle.FixedSingle;

                headers[i].BackColor = backgrColor;
                headers[i].Anchor = (AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top);
                Centroid(weekdayLabels[i], headers[i]);
                headers[i].Controls.Add(weekdayLabels[i]);

                weekdaysRow.Controls.Add(headers[i], i, 0);
            }
            headers[6].BackColor = SundayBackColor;

            frame.Controls.Add(weekdaysRow, 1, 3);
            frame.SetColumnSpan(weekdaysRow, 7);


        }

        // Adapted from:
        // https://stackoverflow.com/questions/39023884/how-to-center-a-label-inside-a-panel-without-setting-dock-to-fill
        // Answer by Manas Hejmadi 
        public void Centroid(Label label, Panel parent)
        {
            int x = (parent.Size.Width - label.Size.Width) / 2;
            label.Location = new Point(x, label.Location.Y);
            label.Dock = DockStyle.Fill;
        }
        //____________________________



        private class CalSquare
        {
            DoubleBufferedTableLayoutPanel square = new DoubleBufferedTableLayoutPanel();
            static int counter = 0;
            //Boolean Sunday;
            DateTime Date;
            ArrayList Events = new ArrayList();
            Label dayLabel;
            Label labelEvent1 = new Label();
            Label labelEvent2 = new Label();
            bool dotsAdded = false;
            Font LabelFont = new Font(FontFamily.GenericSansSerif, 14, FontStyle.Bold);


            public CalSquare(int day)
            {
                SetSquareParams(day);
            }



            public CalSquare(Boolean noLabel)
            {
                square.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top);
                //square.Size = new Size(1000 / 7 - 5, 70);
                square.Dock = DockStyle.Fill;
                counter++;
            }
            private void SetSquareParams(int day)
            {
                counter++;
                dayLabel = new Label();
                dayLabel.Text = day.ToString();
                dayLabel.Dock = DockStyle.Fill;
                dayLabel.Font = LabelFont;
                //dayLabel.BorderStyle = BorderStyle.FixedSingle;

                square.RowCount = 3;
                square.ColumnCount = 2;
                square.BorderStyle = BorderStyle.FixedSingle;
                square.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top);
                //square.Size = new Size(1000 / 7 - 5, 70);
                square.Dock = DockStyle.Fill;
                square.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
                square.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80F));
                square.RowStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
                square.RowStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
                square.RowStyles.Add(new ColumnStyle(SizeType.Percent, 40F));

                square.Controls.Add(dayLabel, 0, 0);
                square.SetRowSpan(dayLabel, 2);
                square.BringToFront();
            }
            public void AddEvent(string e)
            {
                if (labelEvent1.Text == "")
                {
                    square.Controls.Add(labelEvent1, 1, 0);
                    square.SetRowSpan(labelEvent1, 2);
                    labelEvent1.Text = e;
                    labelEvent1.ForeColor = Color.FromArgb(255, 97, 22, 77);
                    //labelEvent1.BorderStyle = BorderStyle.FixedSingle;
                    labelEvent1.Dock = DockStyle.Fill;
                    labelEvent1.Font = LabelFont;
                    //square.Invalidate(square.Region);
                }
                else if (labelEvent2.Text == "")
                {
                    labelEvent2.Font = LabelFont;
                    square.Controls.Add(labelEvent2, 1, 2);
                    if (labelEvent1.Text != e)
                        labelEvent2.Text = e;
                    labelEvent2.ForeColor = Color.FromArgb(255, 117, 38, 26);
                    //labelEvent2.BorderStyle = BorderStyle.FixedSingle;
                    labelEvent2.Dock = DockStyle.Fill;
                }
                else if (!dotsAdded && labelEvent2.Text != e)
                {

                    StringBuilder ltext = new StringBuilder();
                    ltext.Append(labelEvent2.Text);
                    ltext.Append("\n...");
                    labelEvent2.Text = ltext.ToString();
                    dotsAdded = true;
                }

            }
            public void SetDate(DateTime date)
            {
                this.Date = date;
            }
            public DateTime GetDate()
            {
                return Date;
            }
            public DoubleBufferedTableLayoutPanel GetSquare()
            {
                return square;
            }


            public Label GetDayLabel()
            {
                return dayLabel;
            }
            public Label GetLabel1()
            {
                return labelEvent1;
            }
            public Label GetLabel2()
            {
                return labelEvent2;
            }

        }

    }
}