

using System;
using System.Windows.Forms;

namespace Wordly
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            PlayerManager.player.WordLength = 5;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
 
            Application.Run(new Game_Area());


        }
    }
}
