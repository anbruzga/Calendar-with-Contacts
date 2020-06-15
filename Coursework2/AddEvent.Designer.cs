namespace Coursework2
{
    using System;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;
    using ContentAlignment = System.Drawing.ContentAlignment;

    partial class AddEvent
    {

        // VARIABLES
        DoubleBufferedTableLayoutPanel frame = new DoubleBufferedTableLayoutPanel();
        private DoubleBufferedTableLayoutPanel panel = new DoubleBufferedTableLayoutPanel();
        private DoubleBufferedTableLayoutPanel delPanel = new DoubleBufferedTableLayoutPanel();


        private const int LABEL_AMOUNT = 7;
        private Label[] labelArr;
        private Label LabelTitle;
        private Label LabelAppointment;
        private Label LabelRecurring;
        private Label LabelPerson;
        private Label LabelLocation;
        private Label LabelStartDate;
        private Label LabelEndDate;


        private Object[] secondColumnObjArr;
        private TextBox tbTitle;
        private DateTimePicker startDate;
        private DateTimePicker endDate;
        private TextBox tbLocation;
        private ListBox lbPerson;
        private static CheckBox cbRecurring;
        private CheckBox cbAppointment;


        private DateTimePicker startTime;
        private DateTimePicker endTime;

        private Button btnSubmit;

        private ListBox lbEvent;
        private ToolStrip menu;


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
            Size drawingSize = new Size(740, 385);
            this.ClientSize = drawingSize;
            this.MaximumSize = drawingSize;
            this.MinimumSize = drawingSize;
            this.Text = "Add Event";
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.init();

        }
        #endregion
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>

        private void init()
        {
            frame.AutoSize = true;

            //panel.ColumnCount = 2;
            //panel.RowCount = 2;
            frame.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            frame.RowStyles.Add(new System.Windows.Forms.RowStyle(SizeType.AutoSize));

            initMenu();
            initLabels();
            initTextBoxes();
            initThirdColumn();
            initSubmitButton();
            initTableLayoutPanel();

            frame.Controls.Add(menu, 1, 1);
            frame.SetColumnSpan(menu, 3);

            initDeleteEventsPanel();
            frame.Controls.Add(panel, 1, 2);
            frame.Controls.Add(delPanel, 2, 2);

            Controls.Add(frame);

        }

        private void initMenu()
        {
            menu = new ToolStrip();

            ToolStripMenuItem item = new ToolStripMenuItem();

            item = new ToolStripMenuItem();
            item.Text = "Contacts";
            item.Click += new EventHandler(MenuItemClickHandler);

            menu.Items.Add(item);

        }
        public void initThirdColumn()
        {
            startTime = new DateTimePicker();
            endTime = new DateTimePicker();

            startTime.Format = DateTimePickerFormat.Time;
            startTime.ShowUpDown = true;
            startTime.ValueChanged += new EventHandler(this.StartDateChanged_Handler);
            startDate.ValueChanged += new EventHandler(this.StartDateChanged_Handler);
            startTime.Font = new Font(FontFamily.GenericSansSerif, 10);
            startTime.Size = new Size(160, 18);

            endTime.Format = DateTimePickerFormat.Time;
            endTime.ShowUpDown = true;
            //endTime.Enabled = false;
            endTime.ValueChanged += new EventHandler(this.EndDateChanged_Handler);
            endDate.ValueChanged += new EventHandler(this.EndDateChanged_Handler);
            endTime.Font = startTime.Font;
            endTime.Size = startTime.Size;

        }
        public void initSubmitButton()
        {
            btnSubmit = new Button();
            btnSubmit.Size = new Size(130, 35);
            btnSubmit.Font = new Font(FontFamily.GenericSansSerif, 10);
            btnSubmit.Text = "Add New Event!";
            btnSubmit.Click += new System.EventHandler(this.buttonSubmit_Click);
        }

        public void initTextBoxes()
        {
            tbTitle = new TextBox();
            cbAppointment = new CheckBox();
            endDate = new DateTimePicker();
            tbLocation = new TextBox();
            startDate = new DateTimePicker();
            cbRecurring = new CheckBox();
            lbPerson = new ListBox();
            

           
            startDate.MinDate = new DateTime(2015, 01, 01);
            startDate.Value = DateTime.Today;
            startDate.MaxDate = new DateTime(2100, 12, 30);
            endDate.MinDate = new DateTime(2015, 01, 01);
            endDate.Value = DateTime.Today;
            endDate.Enabled = false;
            endDate.MaxDate = new DateTime(2100, 12, 30);


           // endDate.Enabled = false;

            //startDate.ValueChanged += new EventHandler(this.StartDateChanged_Handler);
            //endDate.ValueChanged += new EventHandler(this.EndDateChanged_Handler);

            cbAppointment.CheckedChanged += new EventHandler(this.AppointmentChecked_Handler);
            cbRecurring.CheckedChanged += new EventHandler(this.RecurringChecked_Handler);

            startDate.CustomFormat = "dd, MM, yyyy";
            startDate.Format = DateTimePickerFormat.Custom;
            endDate.CustomFormat = "dd, MM, yyyy";
            endDate.Format = DateTimePickerFormat.Custom;


            secondColumnObjArr = new Object[LABEL_AMOUNT];
            secondColumnObjArr[0] = tbTitle;
            secondColumnObjArr[1] = startDate;
            secondColumnObjArr[2] = endDate;
            secondColumnObjArr[3] = lbPerson;
            secondColumnObjArr[4] = tbLocation;
            secondColumnObjArr[5] = cbAppointment;
            secondColumnObjArr[6] = cbRecurring;

            //disabled by default unless appointment is checked
            tbLocation.Enabled = false;
            lbPerson.Enabled = false;

            
            lbPerson.DataSource = PopulateCotnactListBox();


            // todo refactor: at this point it would be easier not to add them in the loop
            for (int i = 0; i < secondColumnObjArr.Length; i++)
            {
                if (secondColumnObjArr[i] is TextBox)
                {
                    ((TextBox)secondColumnObjArr[i]).TextAlign = HorizontalAlignment.Left;
                    ((TextBox)secondColumnObjArr[i]).Font = new Font(FontFamily.GenericSansSerif, 10);
                    ((TextBox)secondColumnObjArr[i]).Size = new Size(160, 18);
                }
                else if(secondColumnObjArr[i] is CheckBox)
                {
                    ((CheckBox)secondColumnObjArr[i]).Font = new Font(FontFamily.GenericSansSerif, 10);
                    ((CheckBox)secondColumnObjArr[i]).Size = new Size(160, 18);
                }
                else if(secondColumnObjArr[i] is DateTimePicker )
                {
                    ((DateTimePicker)secondColumnObjArr[i]).Font = new Font(FontFamily.GenericSansSerif, 10);
                    ((DateTimePicker)secondColumnObjArr[i]).Size = new Size(160, 18);
                }
                else //ListBox
                {
                    ((ListBox)secondColumnObjArr[i]).ScrollAlwaysVisible = true;
                    
                    ((ListBox)secondColumnObjArr[i]).Font = new Font(FontFamily.GenericSansSerif, 8);
                    ((ListBox)secondColumnObjArr[i]).Size = new Size(160, 556);
                    ((ListBox)secondColumnObjArr[i]).ClearSelected();
                }
            }


        }


        public void initTableLayoutPanel()
        {
            //panel.AutoSize = true;
            panel.ColumnCount = 3;
            panel.RowCount = 7;
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            //panel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            panel.RowStyles.Add(new System.Windows.Forms.RowStyle(SizeType.Percent, 12.5F));
            panel.RowStyles.Add(new System.Windows.Forms.RowStyle(SizeType.Percent, 12.5F));
            panel.RowStyles.Add(new System.Windows.Forms.RowStyle(SizeType.Percent, 12.5F));
            panel.RowStyles.Add(new System.Windows.Forms.RowStyle(SizeType.Percent, 18.5F));
            panel.RowStyles.Add(new System.Windows.Forms.RowStyle(SizeType.Percent, 12.5F));
            panel.RowStyles.Add(new System.Windows.Forms.RowStyle(SizeType.Percent, 12.5F));
            panel.RowStyles.Add(new System.Windows.Forms.RowStyle(SizeType.Percent, 12.5F));


            panel.Size = new Size(402, 300);
            panel.BorderStyle = BorderStyle.FixedSingle;

            panel.Padding = new Padding(25, 30, 15, 25);

            //adding labels to table
            for (int i = 0; i < labelArr.Length-2; i++)
            {
                panel.Controls.Add(labelArr[i], 0, i);
            }

            //adding textboxes to table
            for (int i = 0; i < secondColumnObjArr.Length - 2; i++)
            {
                if (secondColumnObjArr[i] is TextBox)
                {
                    panel.Controls.Add((TextBox)secondColumnObjArr[i], 1, i);

                }
                else if (secondColumnObjArr[i] is CheckBox)
                {
                    panel.Controls.Add((CheckBox)secondColumnObjArr[i], 1, i);
                }
                else if (secondColumnObjArr[i] is DateTimePicker)
                {
                    panel.Controls.Add((DateTimePicker)secondColumnObjArr[i], 1, i);
                }
                else
                {
                    panel.Controls.Add((ListBox)secondColumnObjArr[i], 1, i);
                    //panel.SetRowSpan((ListBox)secondColumnObjArr[i], 2);
                }

            }
            DoubleBufferedTableLayoutPanel p1 = new DoubleBufferedTableLayoutPanel();
            p1.ColumnCount = 2;
            p1.RowCount = 1;
            p1.Size = startTime.Size;

            p1.Controls.Add(labelArr[labelArr.Length - 1], 0, 0);
            p1.Controls.Add((CheckBox)secondColumnObjArr[secondColumnObjArr.Length - 1], 1, 0);
            panel.Controls.Add(p1, 2, 4);

            DoubleBufferedTableLayoutPanel p2 = new DoubleBufferedTableLayoutPanel();
            p2.ColumnCount = 2;
            p2.RowCount = 1;
            p2.Size = startTime.Size;

            p2.Controls.Add(labelArr[labelArr.Length - 2], 0, 0);
            p2.Controls.Add((CheckBox)secondColumnObjArr[secondColumnObjArr.Length - 2], 1, 0);
            panel.Controls.Add(p2, 2, 3);

            //panel.Controls.Add(labelArr[labelArr.Length - 1], 2, 3);
            //panel.Controls.Add(labelArr[labelArr.Length - 2], 2, 4);
            //panel.Controls.Add((CheckBox)secondColumnObjArr[secondColumnObjArr.Length - 1], 3, 3);
            //panel.Controls.Add((CheckBox)secondColumnObjArr[secondColumnObjArr.Length - 2], 3, 4);
           
            ////adding a mighty button to submit new event
            panel.Controls.Add(btnSubmit, 1, 6);

            // adding time pickers
            panel.Controls.Add(startTime, 2, 1);
            panel.Controls.Add(endTime, 2, 2);
            //panel.SetColumnSpan(startTime, 2);
            //panel.SetColumnSpan(endTime, 2);
        }


        public void initLabels()
        {
            labelArr = new Label[LABEL_AMOUNT];
            LabelTitle = new Label();
            LabelEndDate = new Label();
            LabelStartDate = new Label();
            LabelAppointment = new Label();
            LabelLocation = new Label();
            LabelRecurring = new Label();
            LabelPerson = new Label();

            labelArr[0] = LabelTitle;
            labelArr[1] = LabelStartDate;
            labelArr[2] = LabelEndDate;
            labelArr[3] = LabelPerson;
            labelArr[4] = LabelLocation;
            labelArr[5] = LabelAppointment;
            labelArr[6] = LabelRecurring;

            for (int i = 0; i < labelArr.Length; i++)
            {
                labelArr[i].TextAlign = ContentAlignment.MiddleRight;
                labelArr[i].Font = new Font(FontFamily.GenericSansSerif, 10);
            }

            LabelTitle.Text = "Title";
            LabelStartDate.Text = "Start Date";
            LabelEndDate.Text = "End Date";
            LabelAppointment.Text = "Appointment";
            LabelLocation.Text = "Location";
            LabelRecurring.Text = "Recurring";
            LabelPerson.Text = "Person";

        }

        public void initDeleteEventsPanel()
        {
            delPanel.Size = new Size(310, 300);
            delPanel.ColumnCount = 2;
            delPanel.RowCount = 2;
            delPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            delPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            delPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(SizeType.AutoSize));
            delPanel.BorderStyle = BorderStyle.FixedSingle;
            delPanel.Padding = new Padding(15, 25, 15, 25);
            initListBox();
        }

        private void initListBox()
        {
            if (lbEvent == null)
            {
                lbEvent = new ListBox();
                lbEvent.ScrollAlwaysVisible = true;
                
                lbEvent.Size = new Size(500, 250);
                lbEvent.Font = new Font(FontFamily.GenericSansSerif, 10);


                //lbEvent.DataSource = XmlControl.GetEventsList();
                SetEventsData();

                lbEvent.MouseDown += LbEvent_MouseDown;
                delPanel.Controls.Add(lbEvent, 0, 0);

                //Adding right click menu
                ToolStripMenuItem delEv = new ToolStripMenuItem();
                delEv.Text = "Delete";
                ToolStripMenuItem editEv = new ToolStripMenuItem();
                editEv.Text = "Edit";

                // add the clickevent
                delEv.Click += Del_Click;
                editEv.Click += Edit_Click;



                ContextMenuStrip RightClickMenu = new ContextMenuStrip();

                // add the item in right click menu
                RightClickMenu.Items.Add(editEv);
                RightClickMenu.Items.Add(delEv);

                // attach the right click menu with form
                //this.ContextMenuStrip = s;
                lbEvent.ContextMenuStrip = RightClickMenu;
            }

            else
            {
                Thread thread = new Thread((res) => { res = SetEventsData(); });
                thread.Start();
                thread.Join();
                //SetEventsData();
                lbEvent.Update();
            }
        }



    }
}