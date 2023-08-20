using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
 

namespace Wordly
{
    public partial class test : Form
    {
        public test()
        {
            InitializeComponent();
            int count = 0;
            TextBox last=null;
            TextBox first = null;


            for (int k = 1; k < 5; k++)
            {
                for (int i = 1; i <20; i++)
                {
                    count++;
                    
                    TextBox temp = new TextBox();

                    
                    temp.Size = new System.Drawing.Size(32, 29);


                    temp.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));

                   
                 
                        if (i == 1 && k == 1)
                        {
                            temp.Location = new Point(19, 19);
                        }
                        else if (i == 1 && k > 1)
                        {
                            temp.Location = new Point(19, 38 * (k) - 19);
                        }
                        else if (i > 1 && k == 1)
                        {
                            temp.Location = new Point(38 * (i) - 19, 19);
                        }
                        else if (i > 1 && k > 1)
                        {
                            temp.Location = new Point(38 * (i) - 19, 38 * (k) - 19);
                        }
                     
                    temp.Name = "textBox" + (count).ToString();
                

                   

                    temp.TabIndex = 0;
                    temp.TextAlign = HorizontalAlignment.Center;
                    this.Controls.Add(temp);
                    last = (TextBox)temp;

                    if (count == 1) { first = (TextBox)temp; }
                }
            }

            
 
 
          this.Width=last.Location.X+last.Width+32;
            this.Height = last.Location.Y + last.Height +64;
          

        }
        }
} 
