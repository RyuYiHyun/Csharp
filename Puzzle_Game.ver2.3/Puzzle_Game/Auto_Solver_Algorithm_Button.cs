//#define INFINITE_RUN_CHECK//자동완성 무한 루프( 자동완성 클리시 자동완성시행 -> 완성 -> 랜덤 섞기 ->반복)
#undef INFINITE_RUN_CHECK
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
    //자동완성 버튼
    class Auto_Solver_Algorithm_Button : Button
    {

        public Auto_Solver_Algorithm_Button(Form1 p_form)
        {
            //버튼 택스트
            this.Text = "Auto";
            //버튼 크기
            this.Size = new Size(100, 50);
            //버튼 고정 좌표
            this.Location = new Point(950, 425);


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

            //이벤트 추가
            this.MouseDown += Auto_Solver_MouseEventHandler;
            this.KeyDown += GameLogic.GetInstance().Controller.Move_KeyEventHandler;
            this.PreviewKeyDown += GameLogic.GetInstance().Controller.Move_preview_KeyEventHandler;
        }

        private void Auto_Solver_MouseEventHandler(object sender, MouseEventArgs e)
        {
            GameLogic.GetInstance().Controller.UndoStack.Clear();
            GameLogic.GetInstance().Controller.RedoStack.Clear();
            if (e.Button == MouseButtons.Left)//마우스 왼쪽버튼 클릭
            {
                //게임 로직의 자동완성을 실행
#if INFINITE_RUN_CHECK
                int count = 0;
                while (true)
                {
                    ++count;
                    
                    GameLogic.GetInstance().Auto_Solver();
                    if (count>=3)
                    {
                        if(MessageBox.Show("계속 하시겠습니까?", "자동완성 무한루프", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            break;
                        }
                        else
                        {
                            count = 0;
                        }
                    }
                    else
                    {
                        Delay(3000);
                    }
                    GameLogic.GetInstance().Change(GameLogic.GetInstance().m_LinkForm, GameLogic.GetInstance().Controller.MAX_X, GameLogic.GetInstance().Controller.MAX_Y);
                    Delay(1000);
                }
#else
                GameLogic.GetInstance().Auto_Solver();
                if (MessageBox.Show("계속 하시겠습니까?", "자동완성 성공", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //계속

                    GameLogic.GetInstance().Controller.UI_auto_result_text.Text = "[자동완성 결과 표시]";
                }
                else
                {
                    //종료
                    Application.Exit();
                }
#endif
            }
        }
        private static DateTime Delay(int MS)
        {

            DateTime ThisMoment = DateTime.Now;

            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);

            DateTime AfterWards = ThisMoment.Add(duration);

            while (AfterWards >= ThisMoment)

            {

                System.Windows.Forms.Application.DoEvents();

                ThisMoment = DateTime.Now;

            }

            return DateTime.Now;

        }

    }
}
