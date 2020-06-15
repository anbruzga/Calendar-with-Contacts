using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coursework2
{
    public partial class CalendarForm : Form
    {
        public CalendarForm()
        {

            InitializeComponent();
        }
        private void Alert(Size t)
        {
            MessageBox.Show(t.ToString());
        }

        private void Alert(String t)
        {
            MessageBox.Show(t.ToString());
        }

        private void Alert(int t)
        {
            MessageBox.Show(t.ToString());
        }

        private void Alert(string s, Object t)
        {
            MessageBox.Show(s + " " + t.ToString());
        }

        private void OnSizeChange(object sender, EventArgs e)
        {



            //weekdaysRow.Size = new System.Drawing.Size((int)(ClientSize.Width *0.99202), 35);
            //Alert(weekdaysRow.Size.ToString());
            //Alert(frame.Size.ToString());

            //NextBtn.Width = (int)(0.1395 * ClientSize.Width);
            //NextBtn.Height = (int)(0.06 * ClientSize.Height);
            //PreviousBtn.Width = (int)(0.1395 * ClientSize.Width);
            //PreviousBtn.Height = (int)(0.06 * ClientSize.Height);

            Size BtnSize = new Size((int)(ClientSize.Width * 0.1462765), 30);
            NextBtn.Size = PreviousBtn.Size = BtnSize;

            //frame.Size = this.Size;

            //for (int i = 0; i < squares.Length; i++)
            //{
            //    squares[i].GetSquare().Width = (int)(headers[i % 7].Size.Width);

            //    squares[i].GetSquare().Height = (int)(frame.Height / 20);
            //    //square.Size = new Size(1000 / 7 - 5, 70);
            //}



            //MonthLabel.Width = (int)(ClientSize.Width / 2.5);
            //MonthLabel.Height = (int)(0.04 * ClientSize.Height);
            //MonthLabel.Left = (this.ClientSize.Width - MonthLabel.Width) / 2;
            //MonthLabel.Top = (this.ClientSize.Height - MonthLabel.Height) / 2;



            //frame.AutoSize = true;


            //weekdaysRow.Width = (int)(ClientSize.Width * 0.992);
            //weekdaysRow.Height = (int)(ClientSize.Height * 0.06925);

            //for (int i = 0; i < headers.Length; i++)
            //{
            //    //headers[i].Size = new Size(991 / 7 - 5, 25);
            //    headers[i].Width = (int)((ClientSize.Width * 0.988)/7 - ClientSize.Width *0.05);
            //    headers[i].Height = (int)(ClientSize.Height * 0.06925);
            //    Centroid(weekdayLabels[i],headers[i] );
            //}
        }

        private void SquareHandler(object sender, EventArgs e)
        {
            AddEvent form;

            if (sender is Label)
            {
                Label p = ((Label)sender);
                foreach (CalSquare sq in squares)
                {
                    if (sq.GetLabel1().Equals(p)
                        || sq.GetLabel2().Equals(p)
                        || sq.GetDayLabel().Equals(p))
                    {
                        //MessageBox.Show("Suc)");
                        form = new AddEvent(this, sq.GetDate());
                        form.ShowDialog();

                        break;

                    }


                }

            }
            if (sender is DoubleBufferedTableLayoutPanel)
            {
                var p = ((DoubleBufferedTableLayoutPanel)sender);
                foreach (CalSquare sq in squares)
                {
                    if (sq.GetSquare().Equals(p))
                    {

                        form = new AddEvent(this, sq.GetDate());
                        form.ShowDialog();

                        break;

                    }
                }
            }
            //else //MessageBox.Show("Didnt recognice the obj" + sender.ToString());


        }

        private void MenuItemClickHandler(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;

            //MessageBox.Show(sender.ToString()); //test line
            if (sender.ToString().Equals("Contacts"))
            {
                var form = new ContactsForm();
                form.ShowDialog();

            }
            if (sender.ToString().Equals("Prediction"))
            {
                var form = new PredictionForm();
                form.Show();
            }
        }

        private void PreviousBtnHandler(object sender, EventArgs e)
        {
            CurrentMonth = DateUtil.AddMonth(CurrentMonth, -1);
            Repaint();
        }
        private void NextBtnHandler(object sender, EventArgs e)
        {
            CurrentMonth = DateUtil.AddMonth(CurrentMonth, 1);
            Repaint();
        }

    }
}
