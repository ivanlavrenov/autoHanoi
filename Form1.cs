using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AutoHanoi
{
    public partial class Form1 : Form
    {
        private List<string> moves = new List<string>(); // список с действиями
        private List<List<int>> towers = new List<List<int>>();
        public Form1()
        {
            towers.Add(new List<int>());
            towers.Add(new List<int>());
            towers.Add(new List<int>());

            InitializeComponent();
             
            for (int i = 1; i <= 6; i++)
            {
                towers[0].Add(i);
            }

            textBox1.Text = string.Join(Environment.NewLine, towers[0]); //Environment.NewLine делает перенос на новую строку
        }

        private void startClick(object sender, EventArgs e)
        {
            int from = 0;
            int to = 2;
            int other = 1;

            solveTowers(6, from, to, other);
        }


        private void solveTowers(int n, int from, int to, int other)
        {
            // solveTowers->6 -> solveTowers->5 -> solveTowers->4 -> solveTowers->3 -> solveTowers->2 -> solveTowers->1 -> solveTowers->0 end 
            //                   solveTowers->5 -> solveTowers->4
            //                                  -> solveTowers->4 
            //                                  -> solveTowers->4
            //

            if (n > 0)
            {
                solveTowers(n - 1, from, other, to);

                towers[from].Remove(n); //удаляем значение диска в списке башни, откуда взяли диск

                towers[to].Reverse();
                towers[to].Add(n);
                towers[to].Reverse();

                moves.Add(string.Format("Диск {0} перемещен с {1} на {2}", n, from + 1, to + 1)); // добавление в список дейстивия
                
                
                //передаем элементу textBox значение списка
                textBox1.Text = string.Join(Environment.NewLine, towers[0]); //Environment.NewLine делает перенос на новую строку
                textBox2.Text = string.Join(Environment.NewLine, towers[1]);
                textBox3.Text = string.Join(Environment.NewLine, towers[2]);
                textBox4.Text = string.Join(Environment.NewLine, moves);

                Thread.Sleep(200); // таймауты для постепенного отображения
                textBox1.Refresh(); // обновление компонентов для визуализации действий
                textBox2.Refresh();
                textBox3.Refresh();
                textBox4.Refresh();

                solveTowers(n - 1, other, to, from);
            }
        }
    }
}