using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puzzle_Game
{
    class UI_Button: Button
    {
        public UI_Button(Form1 p_form, int type)
        {
            if ((int)Button_Type.MIX == type)
            {
                //버튼 택스트
                this.Text = "Shuffle";
                //버튼 고정 좌표
                this.Location = new Point(850, 425);
                //버튼 크기
                this.Size = new Size(100, 50);
                //버튼스타일
                this.BackColor = Color.Orange;
                this.ForeColor = Color.Ivory;
                this.Font = new Font("굴림", 18F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(129)));
            }
            
            //버튼 외각선 처리
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 1;

            //포커스 이동 막기
            this.TabStop = false;

            //컨트롤 추가
            p_form.Controls.Add(this);
            this.MouseDown += Rand_Mix_MouseEventHandler;
            this.KeyDown += GameLogic.GetInstance().Controller.Move_KeyEventHandler;
            this.PreviewKeyDown += GameLogic.GetInstance().Controller.Move_preview_KeyEventHandler;
        }
        private void Rand_Mix_MouseEventHandler(object sender, MouseEventArgs e)
        {
            GameLogic.GetInstance().Controller.UndoStack.Clear();
            GameLogic.GetInstance().Controller.RedoStack.Clear();
            if (e.Button == MouseButtons.Left)//마우스 왼쪽버튼 클릭
            {
                GameLogic.GetInstance().Rand_Start();
            }
        }
    }
    
}
