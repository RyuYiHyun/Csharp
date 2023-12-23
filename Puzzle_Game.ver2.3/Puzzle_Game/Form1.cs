using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puzzle_Game
{
    public partial class Form1 : Form
    {
        private int Size_X = 4;
        private int Size_Y = 4;
        public Form1()
        {
            InitializeComponent();
            GameLogic.GetInstance().InitGame(this, Size_X, Size_Y);
            GameLogic.GetInstance().Controller.Board[0, 0].Select();//포커스 옮기기
        }

        private void x4ToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            Size_X = 4;
            Size_Y = 4;
            GameLogic.GetInstance().Change(this, Size_X, Size_Y);
            GameLogic.GetInstance().Controller.Board[0, 0].Select();//포커스 옮기기
        }

        private void x5ToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            Size_X = 5;
            Size_Y = 5;
            GameLogic.GetInstance().Change(this, Size_X, Size_Y);
            GameLogic.GetInstance().Controller.Board[0, 0].Select();//포커스 옮기기
        }

        private void x5ToolStripMenuItem1_MouseDown(object sender, MouseEventArgs e)
        {
            Size_X = 4;
            Size_Y = 5;
            GameLogic.GetInstance().Change(this, Size_X, Size_Y);
            GameLogic.GetInstance().Controller.Board[0, 0].Select();//포커스 옮기기
        }

        private void x4ToolStripMenuItem1_MouseDown(object sender, MouseEventArgs e)
        {
            Size_X = 5;
            Size_Y = 4;
            GameLogic.GetInstance().Change(this, Size_X, Size_Y);
            GameLogic.GetInstance().Controller.Board[0, 0].Select();//포커스 옮기기
        }

        private void 블레미샤인ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameLogic.GetInstance().main_bitmap = Properties.Resources.Blemishine;
            GameLogic.GetInstance().Change(this, Size_X, Size_Y);
            GameLogic.GetInstance().Controller.Board[0, 0].Select();//포커스 옮기기
        }

        private void 마젤란ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameLogic.GetInstance().main_bitmap = Properties.Resources.Magallan;
            GameLogic.GetInstance().Change(this, Size_X, Size_Y);
            GameLogic.GetInstance().Controller.Board[0, 0].Select();//포커스 옮기기
        }

        private void 수르트ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameLogic.GetInstance().main_bitmap = Properties.Resources.Surtr;
            GameLogic.GetInstance().Change(this, Size_X, Size_Y);
            GameLogic.GetInstance().Controller.Board[0, 0].Select();//포커스 옮기기
        }

        private void 중간ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameLogic.DELAY_TIME = 250;
            GameLogic.GetInstance().Controller.Board[0, 0].Select();//포커스 옮기기
        }

        private void 빠름ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameLogic.DELAY_TIME = 50;
            GameLogic.GetInstance().Controller.Board[0, 0].Select();//포커스 옮기기
        }

        private void 느림ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameLogic.DELAY_TIME = 500;
            GameLogic.GetInstance().Controller.Board[0, 0].Select();//포커스 옮기기
        }
    }
}
