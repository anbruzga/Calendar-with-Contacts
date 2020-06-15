using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml;
using System.Xml.Linq;
using ContentAlignment = System.Drawing.ContentAlignment;


namespace Coursework2
{
    partial class ContactsForm : Form
    {

        private TableLayoutPanel panel = new TableLayoutPanel();
        private TableLayoutPanel delPanel = new TableLayoutPanel();
        private TableLayoutPanel frame = new TableLayoutPanel();

        private const int LABEL_AMOUNT = 5;
        private Label[] labelArr;
        private Label LabelFName;
        private Label LabelSName;
        private Label LabelAddress1;
        private Label LabelAddress2;
        private Label LabelPostCode;


        private TextBox[] tbArr;
        private TextBox tbFName;
        private TextBox tbSName;
        private TextBox tbAddress1;
        private TextBox tbAddress2;
        private TextBox tbPostcode;

        private Button btnSubmit;

        private ListBox lbContact;
        private int contactEditedId;

        ToolStrip menu;

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
            Size drawingSize = new Size(657,315);
            this.ClientSize = drawingSize;
            this.MaximumSize = drawingSize;
            this.MinimumSize = drawingSize;
            this.Text = "ContactsForm";
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.init();

        }
        #endregion

        private void init()
        {
            frame.AutoSize = true;

            panel.ColumnCount = 2;
            panel.RowCount = 2;
            frame.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            frame.RowStyles.Add(new System.Windows.Forms.RowStyle(SizeType.AutoSize));

            
            
           // initMenu();
            initLabels();
            initTextBoxes();
            initSubmitButton();
            initTableLayoutPanel();
            
           // frame.Controls.Add(menu, 1, 1);
          //  frame.SetColumnSpan(menu, 2);

            initDeleteContactsPanel();
            frame.Controls.Add(panel, 1, 2);
            frame.Controls.Add(delPanel, 2, 2);
            

            Controls.Add(frame);

            /*
            // old and working/*
            initDeleteContactsPanel();
            frame.Controls.Add(panel);
            frame.Controls.Add(delPanel);

            Controls.Add(frame);
            */
        }

        private void initMenu()
        {
             menu = new ToolStrip();

            ToolStripMenuItem[] items = new ToolStripMenuItem[2]; 

            items[0] = new ToolStripMenuItem();
            //items[i].Name = "dynamicItem" + i.ToString();
            //items[i].Tag = "specialDataHere";
            items[0].Text = "Calendar";
            items[0].Click += new EventHandler(MenuItemClickHandler);
            items[1] = new ToolStripMenuItem();
            items[1].Text = "Prediction";
            items[1].Click += new EventHandler(MenuItemClickHandler);

            menu.Items.AddRange(items);
            //menu.TopLevel = false;
            menu.Show();
            
            
            //myMenu.DropDownItems.AddRange(items);
        }

      

        public void initSubmitButton()
        {
            btnSubmit = new Button();
            btnSubmit.Size = new Size(130, 35);
            btnSubmit.Font = new Font(FontFamily.GenericSansSerif, 10);
            btnSubmit.Text = "Add New Contact!";
            btnSubmit.Click += new System.EventHandler(this.buttonSubmit_Click);
        }

        public void initTextBoxes()
        {
            tbFName = new TextBox();
            tbSName = new TextBox();
            tbAddress1 = new TextBox();
            tbAddress2 = new TextBox();
            tbPostcode = new TextBox();

            tbArr = new TextBox[LABEL_AMOUNT];
            tbArr[0] = tbFName;
            tbArr[1] = tbSName;
            tbArr[2] = tbAddress1;
            tbArr[3] = tbAddress2;
            tbArr[4] = tbPostcode;

            for (int i = 0; i < tbArr.Length; i++)
            {
                tbArr[i].TextAlign = HorizontalAlignment.Left;
                tbArr[i].Font = new Font(FontFamily.GenericSansSerif, 10);
                tbArr[i].Size = new Size(160, 18);
            }


        }

        public void initTableLayoutPanel()
        {
            panel.AutoSize = true;
            panel.ColumnCount = 3;
            panel.RowCount = 6;
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 0F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            panel.RowStyles.Add(new System.Windows.Forms.RowStyle(SizeType.AutoSize));
            panel.Size = new Size(400, 200);
            panel.BorderStyle = BorderStyle.FixedSingle;

            //panel.RowStyles.Add(new System.Windows.Forms.RowStyle(SizeType.Percent, 40F));

            panel.Padding = new Padding(25, 30, 15, 25);

            //adding labels to table
            for (int i = 0; i < labelArr.Length; i++)
            {
                panel.Controls.Add(labelArr[i], 1, i);
            }

            //adding textboxes to table
            for (int i = 0; i < tbArr.Length; i++)
            {
                panel.Controls.Add(tbArr[i], 2, i);
            }

            //adding a mighty button to submit new contact
            panel.Controls.Add(btnSubmit, 2, 6);
        }


        public void initLabels()
        {
            labelArr = new Label[LABEL_AMOUNT];
            LabelFName = new Label();
            LabelSName = new Label();
            LabelAddress1 = new Label();
            LabelAddress2 = new Label();
            LabelPostCode = new Label();

            labelArr[0] = LabelFName;
            labelArr[1] = LabelSName;
            labelArr[2] = LabelAddress1;
            labelArr[3] = LabelAddress2;
            labelArr[4] = LabelPostCode;
            for (int i = 0; i < labelArr.Length; i++)
            {
                labelArr[i].TextAlign = ContentAlignment.MiddleRight;
                labelArr[i].Font = new Font(FontFamily.GenericSansSerif, 10);
            }

            LabelFName.Text = "First Name";
            LabelSName.Text = "Last Name";
            LabelAddress1.Text = "Address Line 1";
            LabelAddress2.Text = "Address Line 2";
            LabelPostCode.Text = "Postcode";

        }

        public void initDeleteContactsPanel()
        {
            delPanel.Size = new Size(310, 243);
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
            if (lbContact == null)
            {
                lbContact = new ListBox();
                lbContact.ScrollAlwaysVisible = true;
                lbContact.Size = new Size(200, 200);
                lbContact.Font = new Font(FontFamily.GenericSansSerif, 10);
                lbContact.DataSource = PopulateListBox();

                lbContact.MouseDown += lbContact_MouseDown;
                delPanel.Controls.Add(lbContact, 0, 0);
                
                //Adding right click menu
                ToolStripMenuItem delCon = new ToolStripMenuItem();
                delCon.Text = "Delete";
                ToolStripMenuItem editCon = new ToolStripMenuItem();
                editCon.Text = "Edit";
                
                // add the clickevent
                delCon.Click += Del_Click;
                editCon.Click += Edit_Click;



                ContextMenuStrip RightClickMenu = new ContextMenuStrip();
                
                // add the item in right click menu
                RightClickMenu.Items.Add(editCon);
                RightClickMenu.Items.Add(delCon);

                // attach the right click menu with form
                //this.ContextMenuStrip = s;
                lbContact.ContextMenuStrip = RightClickMenu;
            }

            else
            {
                lbContact.DataSource = PopulateListBox();
                lbContact.Update();
            }
        }
       

    }
}