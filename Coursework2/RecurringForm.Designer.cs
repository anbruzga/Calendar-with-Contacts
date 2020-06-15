namespace Coursework2
{

    using System;
    using System.Drawing;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;

    partial class RecurringForm
    {

        ///VARIABLES
        ///
        private DoubleBufferedTableLayoutPanel frame = new DoubleBufferedTableLayoutPanel();
        private const int LABEL_AMOUNT = 9;
        private Label[] labelArr = new Label[LABEL_AMOUNT];
        private Label LabelWeekly = new Label();
        private Label LabelDaily = new Label();
        private Label LabelMonday = new Label();
        private Label LabelTuesday = new Label();
        private Label LabelWednesday = new Label();
        private Label LabelThursday = new Label();
        private Label LabelFriday = new Label();
        private Label LabelSaturday = new Label();
        private Label LabelSunday = new Label();

        private CheckBox[] CbArr = new CheckBox[LABEL_AMOUNT];
        private CheckBox CbWeekly = new CheckBox();
        private CheckBox CbDaily = new CheckBox();
        private CheckBox CbMonday = new CheckBox();
        private CheckBox CbTuesday = new CheckBox();
        private CheckBox CbWednesday = new CheckBox();
        private CheckBox CbThursday = new CheckBox();
        private CheckBox CbFriday = new CheckBox();
        private CheckBox CbSaturday = new CheckBox();
        private CheckBox CbSunday = new CheckBox();

        private Button btnSubmit = new Button();
        private CalEvent cEvent = new CalEvent();

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
            this.ClientSize = new System.Drawing.Size(300, 400);
            this.Text = "RecurringForm";
            init();

            this.FormClosing += RecurringForm_FormClosing;
            this.ShowDialog();

        }

        #endregion



        private void RecurringForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //AddEvent.SetRecurringCheckBox(false);
        }
        private void init()
        {
            
            initFrame();

            Controls.Add(frame);
            frame.Show();
            initLabels();
            InitCheckBoxes();
            InitButton();
            

        }
        private void initFrame()
        {
            frame.ColumnCount = 2;
            frame.RowCount = 10; // 9 labels and 1 button
            frame.Size = new Size(300, 400);
            frame.Dock = DockStyle.Fill;
            frame.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            frame.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));

            frame.RowStyles.Add(new System.Windows.Forms.RowStyle(SizeType.Percent, 10F));
            frame.RowStyles.Add(new System.Windows.Forms.RowStyle(SizeType.Percent, 10F));
            frame.RowStyles.Add(new System.Windows.Forms.RowStyle(SizeType.Percent, 10F));
            frame.RowStyles.Add(new System.Windows.Forms.RowStyle(SizeType.Percent, 10F));
            frame.RowStyles.Add(new System.Windows.Forms.RowStyle(SizeType.Percent, 10F));
            frame.RowStyles.Add(new System.Windows.Forms.RowStyle(SizeType.Percent, 10F));
            frame.RowStyles.Add(new System.Windows.Forms.RowStyle(SizeType.Percent, 10F));
            frame.RowStyles.Add(new System.Windows.Forms.RowStyle(SizeType.Percent, 10F));
            frame.RowStyles.Add(new System.Windows.Forms.RowStyle(SizeType.Percent, 10F));
            frame.RowStyles.Add(new System.Windows.Forms.RowStyle(SizeType.Percent, 10F));




            frame.BorderStyle = BorderStyle.FixedSingle;
            frame.Padding = new Padding(25, 50, 15, 25);
        }
        public void initLabels()
        {
            labelArr[0] = LabelWeekly;
            labelArr[1] = LabelDaily;
            labelArr[2] = LabelMonday;
            labelArr[3] = LabelTuesday;
            labelArr[4] = LabelWednesday;
            labelArr[5] = LabelThursday;
            labelArr[6] = LabelFriday;
            labelArr[7] = LabelSaturday;
            labelArr[8] = LabelSunday;

            for (int i = 0; i < labelArr.Length; i++)
            {
                //labelArr[i].TextAlign = ContentAlignment.MiddleRight;
                labelArr[i].Font = new Font(FontFamily.GenericSansSerif, 10);
            }

            LabelWeekly.Text = "Weekly";
            LabelDaily.Text = "Daily";
            LabelMonday.Text = "Monday";
            LabelTuesday.Text = "Tuesday";
            LabelWednesday.Text = "Wednesday";
            LabelThursday.Text = "Thursday";
            LabelFriday.Text = "Friday";
            LabelSaturday.Text = "Saturday";
            LabelSunday.Text = "Sunday";


            //adding labels to table
            for (int i = 0; i < labelArr.Length; i++)
            {
                labelArr[i].TextAlign = ContentAlignment.MiddleRight;
                //labelArr[i].Padding = new Padding(13);
                frame.Controls.Add(labelArr[i], 0, i);

                labelArr[i].BorderStyle = BorderStyle.FixedSingle;
            }


        }
        private void InitCheckBoxes()
        {
            {
                CbArr[0] = CbWeekly;
                CbArr[1] = CbDaily;
                CbArr[2] = CbMonday;
                CbArr[3] = CbTuesday;
                CbArr[4] = CbWednesday;
                CbArr[5] = CbThursday;
                CbArr[6] = CbFriday;
                CbArr[7] = CbSaturday;
                CbArr[8] = CbSunday;

                Padding p = new Padding(0, 20, 0, 0);
                //CbArr[0].Padding = p;
                for (int i = 0; i < CbArr.Length; i++)
                {
                    //CbArr[i].Padding = p;
                    CbArr[i].CheckedChanged += RecurringSelection_CheckedChanged;
                    CbArr[i].TextAlign = ContentAlignment.MiddleRight;
                    CbArr[i].AutoSize = false;
                    CbArr[i].Size = new Size(25,25); 
                }


                //adding labels to table
                for (int i = 0; i < CbArr.Length; i++)
                {
                    frame.Controls.Add(CbArr[i], 1, i);
                    
                }

            }
        }

        public CalEvent getEvent()
        {
            return cEvent;
        }
        public void SetEvent(CalEvent e)
        {
            cEvent = e;
        }


        private void InitButton()
        {
            btnSubmit.Size = new Size(130, 35);
            btnSubmit.Font = new Font(FontFamily.GenericSansSerif, 10);
            btnSubmit.Text = "Submit!";
            btnSubmit.Click += new System.EventHandler(this.buttonSubmit_Click);
            frame.Controls.Add(btnSubmit, 0, 9);
            btnSubmit.Enabled = false;
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            int CheckedBoxes = 0;
            for (int i = 0; i < CbArr.Length; i++)
            {
                if (CbArr[i].Checked)
                {
                    CheckedBoxes++;
                    //MessageBox.Show(CheckedBoxes.ToString() + " CheckedBoxes");
                }
            }
            String[] RecType = new String[LABEL_AMOUNT];
            for (int i = 0; i < LABEL_AMOUNT; i++)
            {
                if (CbArr[i].Checked)
                {
                    RecType[i] = labelArr[i].Text;
                    //MessageBox.Show(RecType[i]);
                }
                else
                {
                    //MessageBox.Show("NIL");
                    RecType[i] = "NIL";
                }

            }
            cEvent.SetRecurringType(RecType);
            
            AddEvent.SetEvent(cEvent);

            this.Dispose();
        }

        private void RecurringSelection_CheckedChanged(object sender, System.EventArgs e)
        {
            //enable button if at least one value is ok
            bool EnableBtn = false;
            foreach( CheckBox c in CbArr){
                if (c.Checked == true)
                {
                    EnableBtn = true;
                    break;
                }
            }
            btnSubmit.Enabled = EnableBtn;
           


            if (sender == CbArr[0]) // Weekly
            {
                if (CbArr[0].Checked)
                {
                    for (int i = 1; i < CbArr.Length; i++)
                    {
                        CbArr[i].Enabled = false;
                    }
                }
                else
                {
                    for (int i = 1; i < CbArr.Length; i++)
                    {
                        CbArr[i].Enabled = true;
                    }
                }
            }

            else if (sender == CbArr[1]) // Daily
            {
                if (CbArr[1].Checked)
                {
                    for (int i = 0; i < CbArr.Length; i++)
                    {
                        CbArr[i].Enabled = false;
                    }
                    CbArr[1].Enabled = true;
                }
                else
                {
                    for (int i = 0; i < CbArr.Length; i++)
                    {
                        CbArr[i].Enabled = true;
                    }
                }
            }
            else // Monday, Tuesday, Wednesday...
            {
                if (CbArr[2].Checked|| CbArr[3].Checked || CbArr[4].Checked || CbArr[5].Checked || CbArr[6].Checked || CbArr[7].Checked || CbArr[8].Checked)
                {
                    CbArr[0].Enabled = false;
                    CbArr[1].Enabled = false;

                }
                else
                {
                    CbArr[0].Enabled = true;
                    CbArr[1].Enabled = true;
                }
            }

        }

        public CheckBox[] GetCbArray()
        {
            return CbArr;
        }
    }
}
