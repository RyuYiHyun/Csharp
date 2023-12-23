using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puzzle_Game
{
    public enum Direction {UP,DOWN,LEFT,RIGHT }
    //상하좌우키 버튼
    class Move_Button : Button
    {
        public Move_Button(Form1 p_form,int Dir)
        {
            //버튼 크기
            this.Size = new Size(100, 100);


            #region 방향 버튼에 따른 정보 등록
            // 버튼의 방향에 대한 정보
            if (Dir == (int)Direction.UP)
            {
                //해당 버튼의 표기
                this.Text = "▲";
                //버튼의 고정 좌표값
                this.Location = new Point(700, 500);
            }
            else if (Dir == (int)Direction.DOWN)
            {
                this.Text = "▼";
                this.Location = new Point(700, 700);
            }
            else if (Dir == (int)Direction.LEFT)
            {
                this.Text = "◀";
                this.Location = new Point(600, 600);
            }
            else if (Dir == (int)Direction.RIGHT)
            {
                this.Text = "▶";
                this.Location = new Point(800, 600);
            }
            #endregion


            //버튼 스타일
            this.Font = new Font("굴림", 40F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(129)));
            this.BackColor = Color.Orange;
            this.ForeColor = Color.Ivory;
            
            //버튼 외각선 처리
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 1;

            //포커스 이동 막기
            this.TabStop = false;

            //컨트롤 추가
            p_form.Controls.Add(this);
            this.KeyDown += GameLogic.GetInstance().Controller.Move_KeyEventHandler;
            this.PreviewKeyDown += GameLogic.GetInstance().Controller.Move_preview_KeyEventHandler;
        }
    }
}
