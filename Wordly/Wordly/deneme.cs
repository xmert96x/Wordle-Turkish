using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wordly
{
    public partial class deneme : Form
    {
        public deneme()
        {
            InitializeComponent();
            label1.Text = "denedi";
        }

        private void deneme_Load(object sender, EventArgs e)
        {
            InitializeComponent();
        }

        public static void close(Form box,string test)
        {

            Console.WriteLine(test);
            box.Close();
    }
    }
}


  
