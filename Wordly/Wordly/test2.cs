using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Wordly
{
  

    public partial class test2 : Form
    {
        public test2()
        {
            InitializeComponent();
         Console.WriteLine("The path for the executable file that " +
       "started the application is: " +
       Application.StartupPath);


            PlayerManager.LoadPlayerData(); 
            PlayerManager.player.WordLength = 12;
            PlayerManager.SavePlayerData();


            // Save player data before exiting the application

            Console.WriteLine(PlayerManager.player.WordLength.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            test3 box = new test3();
            box.Show();
            this.Close();
        }
    }

   
}