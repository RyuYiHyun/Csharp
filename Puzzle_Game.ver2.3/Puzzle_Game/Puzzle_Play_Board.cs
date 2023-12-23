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
    //플레이하는 보드에 대한 클래스
    class Puzzle_Play_Board
    {
        #region Puzzle_Play_Board 클래스 데이터
        // 플레이 보드에서 공백버튼 좌표 값
        public int Blank_x { get; set; }
        public int Blank_y { get; set; }
        // 보드정보
        public Puzzle_Button[,] Board = null;
        //상하좌우 버튼
        public Move_Button[] move_Button = new Move_Button[4];
        //자동 완성 버튼
        public Auto_Solver_Algorithm_Button Auto_button = null;
        //완성 이미지 박스
        public PictureBox Original = null;
        // Undo, Redo 버튼
        public Undo_Redo_Button[] _Undo_Redo = new Undo_Redo_Button[2];
        // Undo, Redo 스택
        public Stack<Point> UndoStack = null;
        public Stack<Point> RedoStack = null;

        // UI_자동완성 결과 표시
        public Label UI_auto_result_text = null;

        //UI_TEXT모음
        public Label[] UI_Text = new Label[2];

        //랜덤섞기 버튼
        public UI_Button Mix_Button = null;
        //보드판 크기
        private int MAX_X { get; set; }
        private int MAX_Y { get; set; }
        //비트맵
        public Bitmap _bitmap = null; 

        //비트맵 보드 정보
        public Bitmap[] DB_Bitmap = null;
        #endregion


        #region 초기 정보 등록
        //초기 정보 등록
        public void InitGame(Form1 p_form, int size_x, int size_y)
        {
            _bitmap = GameLogic.GetInstance().main_bitmap;
            //보드 사이즈 등록
            MAX_X = size_x;
            MAX_Y = size_y;


            #region 완성 이미지 생성
            //완성 이미지 크기 조절
            _bitmap = new Bitmap(_bitmap, new Size(300, 300));
            //사진박스 생성
            Original = new PictureBox();
            //박스 데이터
            Original.Location = new Point(600, 100);
            Original.Size = new Size(300, 300);
            Original.Image = _bitmap;
            //컨트롤 등록
            p_form.Controls.Add(Original);
            #endregion

            //언도 리도우 버튼
            for (int j = 0; j < 2; j++)
            {
                _Undo_Redo[j] = new Undo_Redo_Button(p_form, j);
                _Undo_Redo[j].MouseDown += Undo_Redo_Button_MouseEventHandler;
            }
            UndoStack = new Stack<Point>();
            RedoStack = new Stack<Point>();

            //보드 크기와 같은 이미지크기로 변경
            _bitmap = new Bitmap(_bitmap, new Size(size_x * 100, size_y * 100));

            //각 이미지의 자르는 비율 계산
            int x_part = _bitmap.Width / size_x;
            int y_part = _bitmap.Height / size_y;

            //숫자 보드와 이미지 보드의 동기화 조각 정보
            DB_Bitmap = new Bitmap[size_x * size_y];

            //보드 크기만큼 버튼 생성;
            Board = new Puzzle_Button[size_y, size_x];
            int i = 0;//카운트 변수

            //보드의 정보 생성
            for (int y = 0; y < size_y; y++)
            {
                for (int x = 0; x < size_x; x++)
                {
                    //버튼 생성
                    Puzzle_Button Button = new Puzzle_Button(p_form, x, y, /*인덱스*/i/**/ );
                    if (i == 0)
                    {
                        //공백 이미지
                        DB_Bitmap[i] = null;
                        Button.Image = null;
                    }
                    else
                    {
                        //이미지 조각 정보 생성 및 버튼과 동기화
                        DB_Bitmap[i] = (Bitmap)resizeImage(_bitmap, x * x_part, y * y_part);
                        Button.Image = resizeImage(_bitmap, x * x_part, y * y_part);
                    }
                    //버튼 이벤트 할당
                    Button.MouseDown += LocalMouseEventHandler;//버튼을 마우스로 클릭하여 이동하는 방법
                    //보드에 버튼 정보 등록
                    Board[y, x] = Button;
                    ++i;//카운트 증가
                }
            }

            //상하좌우 버튼으로 이동 등록 
            for (int j = 0; j < 4; j++)
            {
                move_Button[j] = new Move_Button(p_form, j);//버튼 등록  UP =0 ,DOWN = 1, LEFT = 2, RIGHT = 3
                move_Button[j].MouseDown += Move_Button_MouseEventHandler;//상하좌우 버튼 클릭 이벤트
            }

            //자동완성 버튼 생성 및 이벤트 할당
            Auto_button = new Auto_Solver_Algorithm_Button(p_form);

            #region 택스트  모음
            //택스트  모음
            #region 자동완성 결과 라벨
            //자동완성 결과창
            UI_auto_result_text = new Label
            {
                ForeColor = Color.Ivory,
                BackColor = Color.Orange,
                Location = new Point(0, 800),
                Size = new Size(500, 50),
                Font = new Font("굴림", 15F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(129))),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "[자동완성 결과 표시]"
            };
            p_form.Controls.Add(UI_auto_result_text);
            UI_auto_result_text.BringToFront();
            #endregion

            #region 목표 이미지 택스트 
            // 목표 이미지 
            UI_Text[0] = new Label
            {
                ForeColor = Color.White,
                BackColor = Color.Orange,
                Location = new Point(600, 75),
                Size = new Size(300, 25),
                Font = new Font("굴림", 20F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(129))),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "목표 이미지"
            };
            p_form.Controls.Add(UI_Text[0]);
            UI_Text[0].BringToFront();
            #endregion

            #region 도움말 택스트 
            // 목표 이미지 
            UI_Text[1] = new Label
            {
                ForeColor = Color.White,
                BackColor = Color.Orange,
                Location = new Point(925, 75),
                Size = new Size(150, 300),
                Font = new Font("굴림", 10F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(129))),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "Shuffle : 타일섞기\n\n\nAuto : 자동완성\n\n\nUndo : 뒤로가기\n\n\nRedo : 앞으로가기\n\n\n[조작법]\n\nWASD\n방향키\n퍼즐 마우스클릭\n상하좌우 버튼클릭"
            };
            p_form.Controls.Add(UI_Text[1]);
            UI_Text[1].BringToFront();
            #endregion

            //end of 택스트 모음
            #endregion


            //랜덤 섞기 버튼
            Mix_Button = new UI_Button(p_form,(int)Button_Type.MIX);

            //공백 위치 찾기
            Find_Blank();
            UndoStack.Clear();
            RedoStack.Clear();
            Change_button_color();
        }
        #endregion


        #region 이미지 영역 추출 함수
        //이미지 영역 추출 함수
        private Image resizeImage(Image image, int p_x, int p_y)
        {
            if (image != null)
            {
                Bitmap Extraction_Bitmap = new Bitmap(image);
                Extraction_Bitmap = Extraction_Bitmap.Clone(new Rectangle(p_x, p_y, 100, 100),
                        System.Drawing.Imaging.PixelFormat.DontCare);
                return Extraction_Bitmap;
            }
            else
            {
                return image;
            }
        }
        #endregion


        #region 생성 버튼 해제 함수 
        //기존 생성된 버튼 삭제 함수
        public void Button_Reset()
        {
            int size_x = MAX_X;
            int size_y = MAX_Y;
            for (int y = 0; y < size_y; y++)
            {
                for (int x = 0; x < size_x; x++)
                {
                    Board[y, x].Dispose();
                }
            }

            for (int j = 0; j < 4; j++)
            {
                move_Button[j].Dispose();
            }
            Auto_button.Dispose();
            Original.Dispose();
            for (int j = 0; j < 2; j++)
            {
                _Undo_Redo[j].Dispose();
                UI_Text[j].Dispose();
            }
            UI_auto_result_text.Dispose();
            Mix_Button.Dispose();

            UndoStack.Clear();
            RedoStack.Clear();
            Change_button_color();
        }
        #endregion


        #region 자동완성에서의 버튼 비활성화 및 활성화 함수 
        //자동완성에서의 버튼 비활성화 및 활성화 함수 
        public void Button_Activation(bool active_type)
        {
            if(active_type)
            {
                for (int j = 0; j < 4; j++)
                {
                    move_Button[j].Enabled = true;
                }
                Auto_button.Enabled = true;
                Original.Enabled = true;
                for (int j = 0; j < 2; j++)
                {
                    _Undo_Redo[j].Enabled = true;
                }
                Mix_Button.Enabled = true;
            }
            else
            {
                for (int j = 0; j < 4; j++)
                {
                    move_Button[j].Enabled = false;
                }
                Auto_button.Enabled = false;
                Original.Enabled = false;
                for (int j = 0; j < 2; j++)
                {
                    _Undo_Redo[j].Enabled = false;
                }
                Mix_Button.Enabled = false;
            }
            UndoStack.Clear();
            RedoStack.Clear();
            Change_button_color();
        }
        #endregion


        #region 공백 버튼 좌표 찾아서 등록하는 함수
        //공백 버튼 좌표 찾기
        private void Find_Blank()
        {
            int size_x = MAX_X;
            int size_y = MAX_Y;
            for (int y = 0; y < size_y; y++)
            {
                for (int x = 0; x < size_x; x++)
                {
                    if ((int)Board[y, x].Tag == 0)
                    {
                        this.Blank_x = x;
                        this.Blank_y = y;
                        return;
                    }
                }
            }
        }
        #endregion


        #region 버튼을 눌러 이동하는 함수
        //버튼을 눌러서 이동하는 함수(클릭한 버튼의 좌표 전달)
        private bool Click_Move(int p_x, int p_y)
        {
            if (Up(p_x, p_y))
            {
                return true;
            }
            else if (Down(p_x, p_y))
            {
                return true;
            }
            else if (Left(p_x, p_y))
            {
                return true;
            }
            else if (Right(p_x, p_y))
            {
                return true;
            }
            return false;
        }
        #endregion


        #region 버튼 이미지 방향 이동 함수
        public bool Up(int p_x, int p_y)
        {
            int P_X = this.Blank_x;
            int P_Y = this.Blank_y;
            if (P_Y - 1 == p_y && P_X == p_x)
            {
                Input_Undo();
                //이미지 스왑
                Image tmp = Board[P_Y, P_X].Image;
                Board[P_Y, P_X].Image = Board[P_Y - 1, P_X].Image;
                Board[P_Y - 1, P_X].Image = tmp;
                //태그 정보 스왑
                int tmp2 = (int)Board[P_Y, P_X].Tag;
                Board[P_Y, P_X].Tag = Board[P_Y - 1, P_X].Tag;
                Board[P_Y - 1, P_X].Tag = tmp2;
                --this.Blank_y;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Down(int p_x, int p_y)
        {
            int P_X = this.Blank_x;
            int P_Y = this.Blank_y;
            if (P_Y + 1 == p_y && P_X == p_x)
            {
                Input_Undo();
                //이미지 스왑
                Image tmp = Board[P_Y, P_X].Image;
                Board[P_Y, P_X].Image = Board[P_Y + 1, P_X].Image;
                Board[P_Y + 1, P_X].Image = tmp;
                //태그 정보 스왑
                int tmp2 = (int)Board[P_Y, P_X].Tag;
                Board[P_Y, P_X].Tag = Board[P_Y + 1, P_X].Tag;
                Board[P_Y + 1, P_X].Tag = tmp2;
                ++this.Blank_y;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Left(int p_x, int p_y)
        {
            int P_X = this.Blank_x;
            int P_Y = this.Blank_y;
            if (P_X - 1 == p_x && P_Y == p_y)
            {
                Input_Undo();
                //이미지 스왑
                Image tmp = Board[P_Y, P_X].Image;
                Board[P_Y, P_X].Image = Board[P_Y, P_X - 1].Image;
                Board[P_Y, P_X - 1].Image = tmp;
                //태그 정보 스왑
                int tmp2 = (int)Board[P_Y, P_X].Tag;
                Board[P_Y, P_X].Tag = Board[P_Y, P_X - 1].Tag;
                Board[P_Y, P_X - 1].Tag = tmp2;
                --this.Blank_x;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Right(int p_x, int p_y)
        {
            int P_X = this.Blank_x;
            int P_Y = this.Blank_y;
            if (P_X + 1 == p_x && P_Y == p_y)
            {
                Input_Undo();
                //이미지 스왑
                Image tmp = Board[P_Y, P_X].Image;
                Board[P_Y, P_X].Image = Board[P_Y, P_X + 1].Image;
                Board[P_Y, P_X + 1].Image = tmp;
                //태그 정보 스왑
                int tmp2 = (int)Board[P_Y, P_X].Tag;
                Board[P_Y, P_X].Tag = Board[P_Y, P_X + 1].Tag;
                Board[P_Y, P_X + 1].Tag = tmp2;
                ++this.Blank_x;
                return true;
            }
            else
            {
                return false;
            }
        }
        //Point 값의 위치의 버튼과 현재 공백을 스왑한다
        public void Swap(Point _point)
        {
            int P_X = _point.X;
            int P_Y = _point.Y;
            int B_X = this.Blank_x;
            int B_Y = this.Blank_y;

            //이미지 스왑
            Image tmp = Board[P_Y, P_X].Image;
            Board[P_Y, P_X].Image = Board[B_Y, B_X].Image;
            Board[B_Y, B_X].Image = tmp;
            //태그 정보 스왑
            int tmp2 = (int)Board[P_Y, P_X].Tag;
            Board[P_Y, P_X].Tag = Board[B_Y, B_X].Tag;
            Board[B_Y, B_X].Tag = tmp2;

            this.Blank_x = _point.X;
            this.Blank_y = _point.Y;
        }
        // 움직일때 Undo에 기록 후 Redo 파기
        public void Input_Undo()
        {
            Point temp_Point = new Point(this.Blank_x, this.Blank_y);
            UndoStack.Push(temp_Point);
            RedoStack.Clear();
            Change_button_color();
        }
        #endregion


        #region 이벤트 모음


        #region 보드 버튼을 클릭 이동하는 이벤트
        //버튼 눌러 이동하는 이벤트
        public void LocalMouseEventHandler(object sender, MouseEventArgs e)
        {
            int size_x = MAX_X;
            int size_y = MAX_Y;
            Button tempbtn = sender as Button;//버튼이 아니면 null 반환
            if (e.Button == MouseButtons.Left)//마우스 왼쪽버튼 클릭
            {
                for (int y = 0; y < size_y; y++)
                {
                    for (int x = 0; x < size_x; x++)
                    {
                        if (tempbtn == Board[y, x])//클릭한 보드 좌표
                        {
                            if (Click_Move(x, y))//움직였으면
                            {
                                GameLogic.GetInstance().Game_Clear();//게임을 클리어했는 지 검사
                            }
                            return;
                        }
                    }//for( x )
                }//for( y )
            }
        }
        #endregion


        #region 상하좌우 버튼을 클릭하여 이동하는 이벤트
        //상하좌우 버튼을 클릭하여 이동하는 이벤트
        public void Move_Button_MouseEventHandler(object sender, MouseEventArgs e)
        {
            Button tempbtn = sender as Button;//버튼이 아니면 null 반환
            if (e.Button == MouseButtons.Left)//마우스 왼쪽버튼 클릭
            {
                int P_X = this.Blank_x;
                int P_Y = this.Blank_y;
                if (tempbtn == move_Button[(int)Direction.UP])
                {
                    if (P_Y > 0)
                    {
                        if (Up(P_X, P_Y - 1)) { GameLogic.GetInstance().Game_Clear(); }//상으로 이동및 완료 검사
                    }
                }
                else if (tempbtn == move_Button[(int)Direction.DOWN])
                {
                    if (P_Y < MAX_Y - 1)
                    {
                        if (Down(P_X, P_Y + 1)) { GameLogic.GetInstance().Game_Clear(); }//하로 이동및 완료 검사
                    }
                }
                else if (tempbtn == move_Button[(int)Direction.LEFT])
                {
                    if (P_X > 0)
                    {
                        if (Left(P_X - 1, P_Y)) { GameLogic.GetInstance().Game_Clear(); }//좌로 이동및 완료 검사
                    }
                }
                else if (tempbtn == move_Button[(int)Direction.RIGHT])
                {
                    if (P_X < MAX_X - 1)
                    {
                        if (Right(P_X + 1, P_Y)) { GameLogic.GetInstance().Game_Clear(); }//우로 이동및 완료 검사
                    }
                }
                return;
            }//end of if
        }//end of func
        #endregion


        #region WASD 키보드로 이동하는 이벤트
        //WASD 키보드로 이동하는 이벤트
        public void Move_KeyEventHandler(object sender, KeyEventArgs e)
        {
            int P_X = this.Blank_x;
            int P_Y = this.Blank_y;
            if (e.KeyData == Keys.W)
            {
                if (P_Y > 0)
                {
                    if (Up(P_X, P_Y - 1)) { GameLogic.GetInstance().Game_Clear(); }
                }
            }
            else if (e.KeyData == Keys.S)
            {
                if (P_Y < MAX_Y - 1)
                {
                    if (Down(P_X, P_Y + 1)) { GameLogic.GetInstance().Game_Clear(); }
                }
            }
            else if (e.KeyData == Keys.A)
            {
                if (P_X > 0)
                {
                    if (Left(P_X - 1, P_Y)) { GameLogic.GetInstance().Game_Clear(); }
                }
            }
            else if (e.KeyData == Keys.D)
            {
                if (P_X < MAX_X - 1)
                {
                    if (Right(P_X + 1, P_Y)) { GameLogic.GetInstance().Game_Clear(); }
                }
            }
            return;
        }//end of func
        #endregion


        #region 방향키 키보드로 이동하는 이벤트
        //방향키 키보드로 이동하는 이벤트
        public void Move_preview_KeyEventHandler(object sender, PreviewKeyDownEventArgs e)
        {
            int P_X = this.Blank_x;
            int P_Y = this.Blank_y;
            if (e.KeyData == Keys.Up)
            {
                if (P_Y > 0)
                {
                    if (Up(P_X, P_Y - 1)) { GameLogic.GetInstance().Game_Clear(); }
                }
            }
            else if (e.KeyData == Keys.Down)
            {
                if (P_Y < MAX_Y - 1)
                {
                    if (Down(P_X, P_Y + 1)) { GameLogic.GetInstance().Game_Clear(); }
                }
            }
            else if (e.KeyData == Keys.Left)
            {
                if (P_X > 0)
                {
                    if (Left(P_X - 1, P_Y)) { GameLogic.GetInstance().Game_Clear(); }
                }
            }
            else if (e.KeyData == Keys.Right)
            {
                if (P_X < MAX_X - 1)
                {
                    if (Right(P_X + 1, P_Y)) { GameLogic.GetInstance().Game_Clear(); }
                }
            }
            return;

        }
        #endregion


        #region Undo , Redo 이벤트
        //Undo , Redo 이벤트
        public void Undo_Redo_Button_MouseEventHandler(object sender, MouseEventArgs e)
        {
            Button tempbtn = sender as Button;//버튼이 아니면 null 반환
            if (e.Button == MouseButtons.Left)//마우스 왼쪽버튼 클릭
            {
                Point point = new Point();
                if (tempbtn == _Undo_Redo[(int)Button_Type.Undo])
                {
                    if (UndoStack.Count > 0 && UndoStack.Any())
                    {
                        point = UndoStack.Pop();
                        RedoStack.Push(new Point(this.Blank_x, this.Blank_y));
                        Swap(point);
                        Change_button_color();
                        GameLogic.GetInstance().Game_Clear();
                    }
                }
                else if (tempbtn == _Undo_Redo[(int)Button_Type.Redo])
                {
                    if (RedoStack.Count > 0 && RedoStack.Any())
                    {
                        point = RedoStack.Pop();
                        UndoStack.Push(new Point(this.Blank_x, this.Blank_y));
                        Swap(point);
                        Change_button_color();
                        GameLogic.GetInstance().Game_Clear();
                    }
                }
                return;
            }//end of if
        }
        #endregion
        
        //undo redo버튼 활성화 비활성화시 버튼 색 변경함수
        public void Change_button_color()
        {
            if (UndoStack.Count > 0)
            {
                _Undo_Redo[(int)Button_Type.Undo].BackColor = Color.Orange;
            }
            else
            {
                _Undo_Redo[(int)Button_Type.Undo].BackColor = Color.LightSlateGray;
            }
            if (RedoStack.Count > 0)
            {
                _Undo_Redo[(int)Button_Type.Redo].BackColor = Color.Orange;
            }
            else
            {
                _Undo_Redo[(int)Button_Type.Redo].BackColor = Color.LightSlateGray;
            }
        }

        #endregion
        //end of class
    }
}