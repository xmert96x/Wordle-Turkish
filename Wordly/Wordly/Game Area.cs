using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TextBox = System.Windows.Forms.TextBox;
namespace Wordly
{
    public partial class Game_Area : Form
    {
        string ans = "çantaka";
        int WordLength = PlayerManager.player.WordLength;
        public int tempindex = 0;
        int responseCount = 5;
        int? temp_index = null;
        List<TextBox> buttons = new List<TextBox>();
        List<String> Words = new List<String>();
        public Color bgcolor;
        public Color textcolor;
        public TextBox active;

        public Game_Area()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            WordLength = ans.Length;
            InitializeComponent();
            buttons = generator(WordLength, responseCount);
            buttons.ForEach(x => Console.WriteLine(x.Name));
            ans = ans.ToUpper();
            this.KeyPreview = true;
            button1.Enabled = false;

            active = null;

            if (this.ActiveControl is TextBox textBox)
            {
                active = textBox;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Tab)
            {
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }



        private void next(object sender, EventArgs e)
        {
            string word = string.Empty;
            TextBox box = (TextBox)sender;
            box.Text = box.Text.ToUpper();
            int number = get_number(box.Name);
            box.SelectionStart = box.Text.Length;
            box.SelectionLength = 0;
            bool containsLetter = test_letter(box.Text);
            if (containsLetter)
            {
                if (box.Text.Length > 1)
                {
                    box.Text = box.Text.Substring(1);
                }

                if (box.Text.Length == 1)
                {
                    int indexOfReadOnlyEmpty = buttons.FindIndex(tb => !tb.ReadOnly && string.IsNullOrEmpty(tb.Text));
                    if (indexOfReadOnlyEmpty > 0) this.ActiveControl = buttons[indexOfReadOnlyEmpty];
                }


                    }
            else
            {
                box.Text = box.Text.Length > 1 ? box.Text.Substring(0, box.Text.Length - 1) : string.Empty;
            }
            int count = 0;
            for (int i = 0; i < buttons.Count / responseCount; i++)
            {
                if (i * WordLength < number) count = i + 1;
            }
            for (int i = (count - 1) * WordLength; i < count * WordLength; i++)
            {
                word += buttons[i].Text;
            }
            button1.Enabled = (word.Length == WordLength);
            TextBox tmp = (TextBox)this.ActiveControl;
            tmp.SelectionStart = tmp.Text.Length;
            tmp.SelectionLength = 0;
        }
        private void prev(object sender, System.Windows.Forms.KeyEventArgs e)
        {   if (e.KeyCode == Keys.Space)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }  

            TextBox box = (TextBox)sender;
            int number = get_number(box.Name);
            if (button1.Enabled == true && e.KeyCode == Keys.Enter) { button1_Click(button1, e); }
            if (box.Text.Length == 0 && number % WordLength != 1 && e.KeyCode.ToString() == "Back") { this.ActiveControl = buttons[number - 2]; }
            if (e.KeyCode == Keys.Left && number % WordLength != 1) { this.ActiveControl = buttons[number - 2]; position(number - 2); }
            if (box.Text.Length > 0)
            {
                if (e.KeyCode == Keys.Right && number % WordLength != 0) { this.ActiveControl = buttons[number]; position(number); }
            }
            if ((e.KeyCode == Keys.Left && number % WordLength == 1) || (e.KeyCode == Keys.Right && number % WordLength == 0)) { position(number - 1); }
        
        }
        private int get_number(string text)
        {
            int number = 0;
            string b = string.Empty;
            foreach (char c in text)
            {
                if (Char.IsDigit(c))
                    b += c;
            }
            return number = (b.Length > 0) ? int.Parse(b) : number;
        }
        private bool test_letter(string text)
        {
            text = text.Trim().ToUpper();
            string[] tr_letters = new[] { "Ğ", "Ç", "Ş", "Ü", "Ö", "İ" };
            string[] en_letters = new[] { "X", "Q", "W" };
            Regex r = new Regex("^[A-Z]+$");
            bool check;
            return check = (tr_letters.Any(text.Contains) || r.IsMatch(text)) && !en_letters.Any(text.Contains);
        }
        private void button1_Click(object sender, EventArgs e)
        {

            

            List<char> wordstmp = new List<char>();
            buttons.ForEach(x =>
            {if (x.ReadOnly != true) wordstmp.Add(x.Text[0]);
            });
            string resultString = new string(wordstmp.ToArray());
            string resultString2="";
            int cmpcount=0;
             

            for (int l = 0; l < responseCount; l++)
            {
                List<char> wordstmp2 = new List<char>();

                for (int k = 1; k<=WordLength; k++)
                {

                    if (buttons[k+l* WordLength-1].Text != "")
                    wordstmp2.Add(buttons[k + l * WordLength-1].Text[0]);
                    Console.WriteLine("test "+(k + l * WordLength-1));

                    resultString2 = new string(wordstmp2.ToArray());

                }

                if (resultString2 == resultString) cmpcount++;
                Console.WriteLine(cmpcount);
            }



            if (cmpcount >= 2)
            {
                MessageBox.Show("Aynı Kelimeyi Daha Önce Girdiniz Lütfen Değiştirip Giriniz.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
              
                buttons.ForEach(x =>
                {
                    if(x.ReadOnly == false)
                    {
                        x.Text="";
                    }
                });
                int indexOfNull = buttons.FindIndex(tb => string.IsNullOrEmpty(tb.Text));
                this.ActiveControl =buttons[indexOfNull];
                
            }

            DialogResult box= DialogResult.None;
             
            if (cmpcount <2)
            {
                box = MessageBox.Show(resultString + " Kelimesini Girmek İstiyormusun", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


            }
 
            if (box == DialogResult.Yes&& cmpcount<2)
            {
                int indexOfNull = buttons.FindIndex(tb => string.IsNullOrEmpty(tb.Text));
                char[] ans2 = ans.ToArray();
                bool eq = false;
                string word = String.Empty;
                int count = 0;
                foreach (TextBox txt in buttons)
                {
                    if (txt.Text.Length > 0) { count++; }
                }
                for (int i = count - WordLength; i < count; i++)
                {
                    word += buttons[i].Text;
                }
                Console.WriteLine(word.ToString());
                foreach (string cpm in Words)
                {
                    eq = (cpm == word) ? !eq : eq;
                }
                if (eq != true)
                {
                    Words.Add(word);
                    var correct = 0;
                    for (int i = count - WordLength, k = 0; i < count; i++, k++)
                    {
                        if (word[k] == ans2[k]) { buttons[i].ForeColor = Color.White; buttons[i].BackColor = Color.Green; correct++; }
                        else
                        {
                            for (int j = 0; j < WordLength; j++)
                            {
                                if (word[j] == ans2[k] && buttons[count - WordLength + j].BackColor == SystemColors.Window) { buttons[count - WordLength + j].ForeColor = Color.White; buttons[count - WordLength + j].BackColor = Color.Orange; break; }
                            }
                        }
                    }
                    if (correct != WordLength)
                    {
                        for (int i = count; i < count + WordLength && i < WordLength * responseCount; i++)
                        {
                            buttons[i].ReadOnly = false;
                        }
                        for (int i = 0; i < count; i++)
                        {
                            buttons[i].ReadOnly = true;
                        }
                        this.ActiveControl = (count < WordLength * responseCount) ? buttons[count] : buttons[WordLength * responseCount - 1];
                    }
                    else { MessageBox.Show("Doğru Buldunuz"); initialize(false); temp_index = null; }
                    button1.Enabled = false;
                }
                
                if (count == WordLength * responseCount) { MessageBox.Show("Tekrar Deneyiniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error); initialize(true); }
                temp_index = indexOfNull > 0 ? temp_index = indexOfNull : temp_index = -1;
            }

            else if (box == DialogResult.No)
            {
                int number = get_number(this.ActiveControl.Name.ToString());
                this.ActiveControl = buttons[number - 2];

            }

         }
        private void initialize(bool again)
        {
            ans = (again == true) ? ans : ans = "maraton";
            ans = ans.ToUpper();
            for (int i = 0; i < WordLength; i++) { buttons[i].Text = null; buttons[i].ReadOnly = default; buttons[i].BackColor = bgcolor; buttons[i].ForeColor = textcolor; }
            for (int i = WordLength; i < WordLength * responseCount; i++) { buttons[i].Text = null; buttons[i].ReadOnly = true; buttons[i].BackColor = bgcolor; buttons[i].ForeColor = textcolor; }
            Words.Clear();
            button1.Enabled = false;
            this.ActiveControl = buttons[0];
        }
        private void position(int number)
        {
            this.ActiveControl = buttons[number];
            SendKeys.Send("{END}");
        }
        private void click(object sender, EventArgs e)
        {

            TextBox box2 =this.ActiveControl as TextBox;
            TextBox box = sender as TextBox;
             


            if (box.ReadOnly)
            {
                int indexOfNull = buttons.FindIndex(tb => string.IsNullOrEmpty(tb.Text));
                int indexOfReadOnlyEmpty = buttons.FindIndex(tb => tb.ReadOnly && string.IsNullOrEmpty(tb.Text));

                if (indexOfNull != indexOfReadOnlyEmpty)
                {
                    this.ActiveControl = buttons[indexOfNull];
                }
                else
                {
                    if (indexOfNull > 0)
                    {
                     this.ActiveControl = buttons[indexOfNull];
                    }
                    button1_Click(button1, e);
                }
            }
            else
            {
                int indexOfNull = buttons.FindIndex(tb => string.IsNullOrEmpty(tb.Text));
               
                if (buttons[indexOfNull].ReadOnly == true && indexOfNull>0) {
                    
                    button1_Click(button1, e); }
                this.ActiveControl = buttons[indexOfNull];
            }


        }
            public List<TextBox> generator(int row, int column)
        {
            List<TextBox> buttons = new List<TextBox>();
            int xStart = 15;
            int yStart = 15;
            int horizontalGap = 5;
            int verticalGap = 5;
            for (int k = 0; k < column; k++)
            {
                for (int i = 0; i < row; i++)
                {
                    TextBox temp = new TextBox();
                    temp.Size = new System.Drawing.Size(32, 29);
                    temp.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
                    int x = xStart + (temp.Width + horizontalGap) * (i);
                    int y = yStart + (temp.Height + verticalGap) * (k);
                    temp.Location = new Point(x, y);
                    temp.Name = "textBox" + (k * row + i + 1).ToString();
                    temp.TabIndex = 0;
                    temp.TextChanged += new System.EventHandler(this.next);
                    temp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.prev);
                    temp.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MovePointerToEnd);
                    temp.Click += new System.EventHandler(this.click);
                    temp.ReadOnly = (k * row + i + 1) > WordLength;
                    temp.TextAlign = HorizontalAlignment.Center;
                    groupBox1.Controls.Add(temp);
                    buttons.Add((TextBox)temp);
                }
            }
            groupBox1.Width = buttons.LastOrDefault().Location.X + buttons.LastOrDefault().Width + buttons.FirstOrDefault().Left;
            groupBox1.Height = buttons.LastOrDefault().Location.Y + buttons.LastOrDefault().Height + buttons.FirstOrDefault().Top;
            this.Width = groupBox1.Location.X + groupBox1.Left + buttons.FirstOrDefault().Left + groupBox1.Width;
            button1.Top = groupBox1.Height + groupBox1.Top + 15;
            button1.Left = (this.ClientSize.Width - button1.Width) / 2;
            this.Height = button1.Top + button1.Height * 2;
            return buttons;
        }
        private void MovePointerToEnd(object sender, KeyEventArgs e)
        {
      
            if ((Keys)e.KeyValue == Keys.Left || (Keys)e.KeyValue == Keys.Home)
            {
          
                SendKeys.Send("{END}");
            }
        }
    }
}
