using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puzzle_Game
{
    //Button 클래스 상속받은 퍼즐버튼 클래스
    class Puzzle_Button : Button
    {
        public Puzzle_Button(Form1 p_form, int p_x, int p_y, int index)
        {
            //Index값 설정(백그라운드 데이터와 연동)
            this.Tag = index;
            //버튼 크기
            this.Size = new Size(100, 100);
            //버튼 위치
            this.Location = new Point(p_x * 100, (p_y + 1) * 100);


            //버튼 외각선 처리
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;

            //포커스 이동 막기
            this.TabStop = false;

            //공백 버튼 색 설정
            this.BackColor = Color.WhiteSmoke;

            //컨트롤 추가
            p_form.Controls.Add(this);
            this.KeyDown += GameLogic.GetInstance().Controller.Move_KeyEventHandler;
            this.PreviewKeyDown += GameLogic.GetInstance().Controller.Move_preview_KeyEventHandler;
        }
    }
}

//택스트 설정 코드 이미지 없을 때 사용
//this.Text = this.Tag.ToString();
//this.Font = new Font("굴림", 30F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(129)));