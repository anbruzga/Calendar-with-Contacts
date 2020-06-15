using System;
using System.Windows.Forms;

namespace Coursework2
{
    partial class PredictionForm
    {
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
            this.PredictionChart = new LiveCharts.WinForms.CartesianChart();
            this.SuspendLayout();

            PredictionChart.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Day",

                Labels = new[] {
                "1","2","3","4","5","6","7","8","9","10",
                "11","12","13","14","15","16","17","18","19","20",
                "21","22","23","24","25","26","27","28","29","30"}
            });
            PredictionChart.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Hours"
            });

            // 
            // PredictionChart
            // 
            this.PredictionChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PredictionChart.Location = new System.Drawing.Point(16, 12);
            this.PredictionChart.Name = "PredictionChart";
            this.PredictionChart.Size = new System.Drawing.Size(945, 522);
            this.PredictionChart.TabIndex = 0;
            this.PredictionChart.Text = "cartesianChart1";

            this.PredictionChart.ChildChanged += new System.EventHandler<System.Windows.Forms.Integration.ChildChangedEventArgs>(this.cartesianChart1_ChildChanged);
            // 
            // PredictionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 551);
            this.Controls.Add(this.PredictionChart);
            this.Name = "PredictionForm";
            this.Text = "PredictionForm";
            this.ResumeLayout(false);

        }

        #endregion




        private LiveCharts.WinForms.CartesianChart PredictionChart;


        private void form1_load(object sender, EventArgs e)
        {

            
        }
    }




}