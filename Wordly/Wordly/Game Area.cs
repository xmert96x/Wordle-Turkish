using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Wordly
{
    public partial class Game_Area : Form
    {
        List<TextBox> buttons = new List<TextBox>();
        List<TextBox> buttons2 = new List<TextBox>();
        List<String> Words = new List<String>();
        string ans = "kabak";
        public Color bgcolor;
        public Color textcolor;
        public Game_Area()
        {
            InitializeComponent();
            ans = ans.ToUpper();
            button1.Enabled = false;
            var bgcolor = textBox1.BackColor;
            var textcolor = textBox1.ForeColor;
            foreach (TextBox test in groupBox1.Controls)
            {
                buttons.Add(test);
            }
            for (int i = 0; i < buttons.Count; i++)
            {       
                buttons[buttons.Count - 1 - i].TextChanged += new System.EventHandler(this.next);
                buttons[buttons.Count - 1 - i].KeyDown += new System.Windows.Forms.KeyEventHandler(this.prev);
                if (buttons.Count - 1 - i < 20) { buttons[buttons.Count - 1 - i].ReadOnly = true; }
                buttons2.Add(buttons[buttons.Count - 1 - i]);
            }
            buttons = buttons2;
        }
        private void next(object sender, EventArgs e)
        {
            string word = string.Empty;
            TextBox box = (TextBox)sender;
            box.Text = box.Text.ToUpper();
            int number = get_number(box.Name);
            // button1.Enabled = (number % 5 == 0 && box.Text.Length > 0) ? true : false;
            box.SelectionStart = box.Text.Length;
            box.SelectionLength = 0;
            if (box.Text.Length > 1)
            {
                box.Text = box.Text.Remove(box.Text.Length - 1, 1);
            }
            bool latter = test_letter(box.Text);
            if (latter == true)
            {
                if (number % 5 != 0) this.ActiveControl = buttons[number];
            }
            else
            {
                box.Text = null;
            }
            int count = 0;
            for (int i = 0; i < buttons.Count / 5; i++)
            {
                if (i * 5 < number) count = i + 1;
            }
            for (int i = (count - 1) * 5; i < count * 5; i++)
            {   
                word += buttons[i].Text;
            }
            button1.Enabled = (word.Length == 5);
        }
        private void prev(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            TextBox box = (TextBox)sender;
            int number = get_number(box.Name);
            if (button1.Enabled == true && e.KeyCode == Keys.Enter) { button1_Click(button1, e); }
            if (box.Text.Length == 0 && number % 5 != 1 && e.KeyCode.ToString() == "Back") { this.ActiveControl = buttons[number - 2]; }
            if (e.KeyCode == Keys.Left && number % 5 != 1) { this.ActiveControl = buttons[number - 2]; position(number - 2); }
            if (e.KeyCode == Keys.Right && number % 5 != 0) { this.ActiveControl = buttons[number]; position(number); }
            if ((e.KeyCode == Keys.Left && number % 5 == 1) || (e.KeyCode == Keys.Right && number % 5 == 0)) { position(number - 1); }
        }
        private int get_number(string text)
        {
            int number = 0;
            string b = string.Empty;
            for (int i = 0; i < text.Length; i++)
            {   
                if (Char.IsDigit(text[i]))
                    b += text[i];
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
            char[] ans2 = ans.ToArray();
            var result = DialogResult.No;
            Console.WriteLine(e.ToString());
            bool eq = false;
            string word = "";
            int count = 0;
            foreach (TextBox txt in buttons)
            {
                if (txt.Text.Length > 0) { count++; }
            }
            for (int i = count - 5; i < count; i++)
            {
                word += buttons[i].Text;
            }
            Control box = (Control)this.ActiveControl;
            Console.WriteLine(word.ToString());
            foreach (string cpm in Words)
            {
                eq = (cpm == word) ? !eq : eq;
            }
            if (eq != true)
            {
                Words.Add(word);
                var correct = 0;
                for (int i = count - 5, k = 0; i < count; i++, k++)
                {
                    if (word[k] == ans2[k]) { buttons[i].ForeColor = Color.White; buttons[i].BackColor = Color.Green; correct++; }
                    else
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            if (word[j] == ans2[k] && buttons[count - 5 + j].BackColor == SystemColors.Window) {Console.WriteLine(buttons[i].BackColor.ToString());  buttons[count - 5 + j].ForeColor = Color.White; buttons[count - 5 + j].BackColor = Color.Orange; break; }
                        }
                    }
                } 
                if (correct!= 5)
                {
                    for (int i = count; i < count + 5 && i < 25; i++)
                    {
                        buttons[i].ReadOnly = false;
                    }
                    for (int i = 0; i < count; i++)
                    {
                        buttons[i].ReadOnly = true;
                    }
                    this.ActiveControl = (count < 25) ? buttons[count] : buttons[24];
                }
                else { MessageBox.Show("Doğru Buldunuz"); initialize(false); }
                button1.Enabled = false;
            }
            else { result = MessageBox.Show("Aynı Kelimeyi Daha Önce Girdiniz Lütfen Değiştirip Giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            if (result == DialogResult.OK)
            {
                for (int i = count - 5; i < count; i++)
                {
                    buttons[i].Text = null;
                }
                this.ActiveControl = buttons[(count - 5)];
            }
            if (count == 25) { MessageBox.Show("Tekrar Deneyiniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error); initialize(true); }
        }
        private void initialize(bool again)
        {
            ans = (again == true) ? ans : ans = "kalem";
            ans = ans.ToUpper();
            for (int i = 0; i < 5; i++) { buttons[i].Text = null; buttons[i].ReadOnly = default; buttons[i].BackColor = bgcolor; buttons[i].ForeColor = textcolor; }
            for (int i = 5; i < 25; i++) { buttons[i].Text = null; buttons[i].ReadOnly = true; buttons[i].BackColor = bgcolor; buttons[i].ForeColor = textcolor; }
            Words.Clear();
            button1.Enabled = false;
            this.ActiveControl = textBox1;
        }
        private void position(int number)
        {
            buttons[number].SelectionStart = buttons[number].Text.Length;
            buttons[number].SelectionLength = 0;
        }
    }
}
