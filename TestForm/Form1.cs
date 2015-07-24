using Monitor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load;
        }

        void Form1_Load(object sender, EventArgs e)
        {
            var operate = new Operate();
            operate.Init();
        }

        void hook_MouseClickEvent(object sender, MouseEventArgs e)
        {
           
        }

        void hook_MouseMoveEvent(object sender, MouseEventArgs e)
        {
          
        }
    }
}
