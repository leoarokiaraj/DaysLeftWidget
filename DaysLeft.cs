using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace DaysLeft
{
    public partial class DaysLeft : Form
    {
        private bool drag = false; // determine if we should be moving the form
        private Point startPoint = new Point(0, 0); // also for the moving
        private DateTime futureDate = DateTime.Now;
        public DaysLeft()
        {
            this.FormBorderStyle = FormBorderStyle.None; // get rid of the standard title bar

            // finally, wouldn't it be nice to get some moveability on this control?
            this.MouseDown += new MouseEventHandler(Title_MouseDown);
            this.MouseUp += new MouseEventHandler(Title_MouseUp);
            this.MouseMove += new MouseEventHandler(Title_MouseMove);
            this.BackColor = Color.DimGray;
            this.TransparencyKey = Color.DimGray;
            this.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, @"..\..\..\Resourse\Background.png"));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            InitializeComponent();
            this.Reset();
        }

        void Title_MouseUp(object sender, MouseEventArgs e)
        {
            this.drag = false;
        }
        void Title_MouseDown(object sender, MouseEventArgs e)
        {
            this.startPoint = e.Location;
            this.drag = true;
        }

        void Title_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.drag)
            { // if we should be dragging it, we need to figure out some movement
                Point p1 = new Point(e.X, e.Y);
                Point p2 = this.PointToScreen(p1);
                Point p3 = new Point(p2.X - this.startPoint.X,
                                     p2.Y - this.startPoint.Y);
                this.Location = p3;
            }
        }

        private void DaysLeft_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.futureDate = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month,
                                           dateTimePicker1.Value.Day, dateTimePicker2.Value.Hour,
                                           dateTimePicker2.Value.Minute, dateTimePicker2.Value.Second);
            this.label1.Hide();
            this.button1.Hide();
            this.dateTimePicker1.Hide();
            this.dateTimePicker2.Hide();
            this.CalculateRemainingTime();
            this.label2.Show();
            this.button3.Show();
            Timer timer = new Timer();
            timer.Interval = (1000); // 1 sec
            timer.Tick += new EventHandler(Refresh);
            timer.Start();

        }

        private void Refresh(object sender, EventArgs e)
        {
             this.CalculateRemainingTime();
        }

        private void CalculateRemainingTime()
        {
            var diff = this.futureDate - DateTime.Now; 
            StringBuilder stringBuilder = new StringBuilder(); 
            if (diff.Days > 0 || diff.Hours > 0 || diff.Minutes > 0 || diff.Seconds > 0)
            {
                if (diff.Days > 0)
                    stringBuilder.Append(diff.Days + " Days\r\n");
                if (diff.Hours > 0 || diff.Days > 0)
                    stringBuilder.Append (diff.Hours + " Hours\r\n");
                if (diff.Days > 0 || diff.Hours > 0 || diff.Minutes > 0)
                    stringBuilder.Append(diff.Minutes + " Minutes\r\n");
                if (diff.Days > 0 || diff.Hours > 0 || diff.Minutes > 0 || diff.Seconds > 0)
                    stringBuilder.Append (diff.Seconds + " Seconds\r\nLeft");
            }
            else
                stringBuilder.Append("0 Seconds");

            this.label2.Text = stringBuilder.ToString() ;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Reset();
        }


        private void Reset()
        {
            this.label1.Show();
            this.button1.Show();
            this.dateTimePicker1.Show();
            this.dateTimePicker2.Show();
            this.dateTimePicker1.Value = DateTime.Now;
            this.dateTimePicker2.Value = DateTime.Now;
            this.label2.Hide();
            this.button3.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
