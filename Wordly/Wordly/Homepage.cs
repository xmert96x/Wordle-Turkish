﻿using System;
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
    public partial class Homepage : Form
    {
        public Homepage()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Game_Area game_area = new Game_Area();
            game_area.Show();
            this.Hide();
        }
    }
}
