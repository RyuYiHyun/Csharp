using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puzzle_Game
{
    enum Button_Type { Undo, Redo, MIX };//undo 뒤로, redo 앞으로
    class Undo_Redo_Button : Button
    {
        public Undo_Redo_Button(Form1 p_form, int type)
        {
            if((int)Button_Type.Undo == type)
            {
                //버튼 택스트
                this.Text = "Undo";
                //버튼 고정 좌표
                this.Location = new Point(850, 500);
            }
            else if ((int)Button_Type.Redo == type)
            {
                //버튼 택스트
                this.Text = "Redo";
                //버튼 고정 좌표
                this.Location = new Point(950, 500);
            }
            
            //버튼 크기
            this.Size = new Size(100, 50);

            //버튼 외각선 처리
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 1;

            //포커스 이동 막기
            this.TabStop = false;

            //버튼스타일
            this.BackColor = Color.Orange;
            this.ForeColor = Color.Ivory;
            this.Font = new Font("굴림", 20F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(129)));

            //컨트롤 추가
            p_form.Controls.Add(this);
            this.KeyDown += GameLogic.GetInstance().Controller.Move_KeyEventHandler;
            this.PreviewKeyDown += GameLogic.GetInstance().Controller.Move_preview_KeyEventHandler;
        }
    }
}
