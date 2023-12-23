//#define DEBUG//디버그 화면에 출력 - TEST 켜기
#undef DEBUG//디버그 화면에 출력 - TEST 끄기

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
    enum Heuristic_Menu { WrongNumber = 1 , ManhattanDistance, WrongNumber_and_ManhattanDistance , WrongProcession }
    class GameLogic
    {
        #region 게임 로직 데이터
        private int MAX_X { get; set; }
        private int MAX_Y { get; set; }
        //보드 컨트롤
        public Puzzle_Play_Board Controller = null;
        //휴리스틱 선택 변수
        public static int DELAY_TIME = 250;//250 자동완성 속도 - 보통 속도 디폴트
        //메인 그래픽
        public Bitmap main_bitmap = null;
        #endregion

        #region 싱글톤
        static GameLogic m_Instace = null;
        public static GameLogic GetInstance()
        {
            if (m_Instace == null)
            {
                m_Instace = new GameLogic();
            }
            return m_Instace;
        }
        #endregion


        #region 자동완성 버튼 동기화및 완성 과정 출력
        //자동완성 버튼 동기화및 완성 과정 출력
        static public int PrintPath(Puzzle node)
        {
            if (node == null) { return -1; }//재귀함수 중단(시작 노드 찾음)
            //재귀 호출, num은 이동횟수 카운트
            int num = PrintPath(node.Parent) + 1;
            //자동완성 알고리즘 보드와 버튼보드와의 데이더 동기화
            GameLogic.GetInstance().Synchronization_Print(node);
#if DEBUG
            node.Printer();
#endif
            Delay(DELAY_TIME);
            return num;
        }
        #endregion


        #region 딜레이 함수
        //딜레이 함수
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
        #endregion


        #region 게임 클리어 검사 함수
        //게임 클리어 검사 함수
        public bool Game_Clear()
        {
            int i = 0;
            for (int y = 0; y < MAX_Y; y++)
            {
                for (int x = 0; x < MAX_X; x++)
                {
                    if((int)Controller.Board[y,x].Tag != i)
                    {
                        return false;
                    }
                    i++;
                }
            }
            if (MessageBox.Show("계속 하시겠습니까?", "성공", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //계속
            }
            else
            {
                //종료
                Application.Exit();
            }
#if DEBUG
            Debug.WriteLine("성공되었습니다");
#endif
            return true;
        }
        #endregion


        #region 랜덤 섞기
        //랜덤 타일 만들기
        public void Rand_Start()
        {
            Random dir = new Random();
            int move = 0;
            Image temp;
            int bx = Controller.Blank_x;
            int by = Controller.Blank_y;
            int tmp;
            for (int i = 0; i < 100; i++)// 공백타일을 랜덤으로 상하좌우 100번 이동하여 섞기
            {
                move = dir.Next(4);
                if (move == (int)Direction.UP)//up
                {
                    if (Controller.Blank_y > 0)
                    {
                        temp = Controller.Board[by - 1, bx].Image;
                        Controller.Board[by - 1, bx].Image = Controller.Board[by, bx].Image;
                        Controller.Board[by, bx].Image = temp;
                        tmp = (int)Controller.Board[by - 1, bx].Tag;
                        Controller.Board[by - 1, bx].Tag = Controller.Board[by, bx].Tag;
                        Controller.Board[by, bx].Tag = tmp;
                        Controller.Blank_y--;
                    }
                }
                else if (move == (int)Direction.DOWN)//down
                {
                    if (Controller.Blank_y < MAX_Y - 1)
                    {
                        temp = Controller.Board[by + 1, bx].Image;
                        Controller.Board[by + 1, bx].Image = Controller.Board[by, bx].Image;
                        Controller.Board[by, bx].Image = temp;
                        tmp = (int)Controller.Board[by + 1, bx].Tag;
                        Controller.Board[by + 1, bx].Tag = Controller.Board[by, bx].Tag;
                        Controller.Board[by, bx].Tag = tmp;
                        Controller.Blank_y++;
                    }
                }
                else if (move == (int)Direction.LEFT)//left
                {
                    if (Controller.Blank_x > 0)
                    {
                        temp = Controller.Board[by, bx - 1].Image;
                        Controller.Board[by, bx - 1].Image = Controller.Board[by, bx].Image;
                        Controller.Board[by, bx].Image = temp;
                        tmp = (int)Controller.Board[by, bx - 1].Tag;
                        Controller.Board[by, bx - 1].Tag = Controller.Board[by, bx].Tag;
                        Controller.Board[by, bx].Tag = tmp;
                        Controller.Blank_x--;
                    }
                }
                else if (move == (int)Direction.RIGHT)//right
                {
                    if (Controller.Blank_x < MAX_X - 1)
                    {
                        temp = Controller.Board[by, bx + 1].Image;
                        Controller.Board[by, bx + 1].Image = Controller.Board[by, bx].Image;
                        Controller.Board[by, bx].Image = temp;
                        tmp = (int)Controller.Board[by, bx + 1].Tag;
                        Controller.Board[by, bx + 1].Tag = Controller.Board[by, bx].Tag;
                        Controller.Board[by, bx].Tag = tmp;
                        Controller.Blank_x++;
                    }
                }
                bx = Controller.Blank_x;
                by = Controller.Blank_y;
            }
        }
        #endregion


        #region 게임 로직 초기값
        //게임 로직 초기값
        public void InitGame(Form1 p_form, int size_x, int size_y)
        {
            main_bitmap = Properties.Resources.Blemishine;
            MAX_X = size_x;
            MAX_Y = size_y;
            Controller = new Puzzle_Play_Board();
            Controller.InitGame(p_form, size_x, size_y);

            Rand_Start();
            Synchronization_DATA();
        }
        #endregion


        #region 사이즈및 이미지 변경 및 조절
        //메뉴에서 사이즈 및 이미지에 해당하는 것을 변경
        public void Change(Form1 p_form, int size_x, int size_y)
        {
            Controller.Button_Reset();//기존 컨트롤 제거
            MAX_X = size_x;
            MAX_Y = size_y;
            Controller = new Puzzle_Play_Board();
            Controller.InitGame(p_form, MAX_X, MAX_Y);//보드 및 컨트롤 재생성
            Rand_Start();//랜덤 섞기
        }
        #endregion


        //
        #region 백그라운드 데이터 연동
        //

        #region 데이터를 통한 버튼의 태그값을 통해 이미지 데이터와 연동하는 함수
        //버튼의 태그값을 통해 이미지 데이터와 동기화
        public void Synchronization_Image(int[,] DATA)// 현재는 사용하지 않지만 태스트때 int배열의 정보로 이미지 화면 동기화
        {
            int size_x = MAX_X;
            int size_y = MAX_Y;
            for (int y = 0; y < size_y; y++)
            {
                for (int x = 0; x < size_x; x++)
                {
                    if (0 == DATA[y, x])
                    {
                        Controller.Board[y, x].Tag = DATA[y, x];
                        Controller.Board[y, x].Image = Controller.DB_Bitmap[0];
                        Controller.Blank_x = x;
                        Controller.Blank_y = y;
                    }
                    else
                    {
                        Controller.Board[y, x].Tag = DATA[y, x];
                        Controller.Board[y, x].Image = Controller.DB_Bitmap[DATA[y, x]];
                    }
                }
            }
        }
        #endregion


        #region 목표에 대한 원본 정보를 만드는 함수
        private void Goal_Setting(int[,] goal)
        {
            int i = 0;
            for (int y = 0; y < MAX_Y; y++)
            {
                for (int x = 0; x < MAX_X; x++)
                {
                    goal[y, x] = i;
                    ++i;
                }
            }
        }
        #endregion


        #region 자동완성 알고리즘의 Start의 초기정보와 Goal의 정보를 등록 후 퍼즐 생성
        private void Synchronization_DATA()
        {
            int[,] start = new int[MAX_Y,MAX_X];
            int[,] goal = new int[MAX_Y, MAX_X];
            for (int y = 0; y < MAX_Y; y++)
            {
                for (int x = 0; x < MAX_X; x++)
                {
                    start[y,x] = (int)Controller.Board[y, x].Tag;
                }
            }
            Goal_Setting(goal);
            Find_Blank(start, MAX_X, MAX_Y);
            Start = new Puzzle(start, point_x, point_y, 0, MAX_X, MAX_Y);
            Find_Blank(goal, MAX_X, MAX_Y);
            Goal = new Puzzle(goal, point_x, point_y, 0, MAX_X, MAX_Y);
        }
        #endregion


        #region 출력을 위한 자동완성 과정의 퍼즐데이터를 버튼의 이미지에 동기화 시키는 함수
        //동기화
        public void Synchronization_Print(Puzzle data)
        {
            //자동완성시 path데이터를 화면의 이미지와 같게 하여 과정이 보이도록 한다.
            for (int y = 0; y < MAX_Y; y++)
            {
                for (int x = 0; x < MAX_X; x++)
                {
                    if(data.Board[y, x] == 0)
                    {
                        Controller.Board[y, x].Tag = data.Board[y, x];
                        Controller.Board[y, x].Image = Controller.DB_Bitmap[0];//공백 이미지 null
                        Controller.Blank_x = x;//공백좌표면 컨트롤러의 공백좌표와 동기화
                        Controller.Blank_y = y;
                    }
                    else
                    {
                        Controller.Board[y, x].Tag = data.Board[y, x];
                        Controller.Board[y, x].Image = Controller.DB_Bitmap[data.Board[y, x]];
                    }
                }
            }
        }
        #endregion

        //
        #endregion
        //

        //
        #region 자동 완성 로직
        //
        //자동완성에 대한 함수 및 데이터를 관리 region
        private List<Puzzle> _OPEN;// 오픈 리스트 (정확한 A스타 알고리즘은 우선순위 큐를 사용 C#은 우선순위 큐 미지원)
        private List<Puzzle> _CLOSED;// 클로즈 리스트 (탐색한 노드를 넣는 리스트)
        private Puzzle Start;// 시작 보드
        private Puzzle Goal;// 목표 보드
        public long Tick_Count = 0;// 알고리즘 구동 틱시간 값
        public int MOVEING = 0; // 자동완성 이동 횟수
        public long Node_Check_Count = 0;//노드 검사 횟수
        private const int Heuristic_Selection = 3;// 평가함수 선택 
        private int point_x { get; set; }//공백좌표
        private int point_y { get; set; }

        #region 자동완성 시작 함수 및 초기 정보
        public void Auto_Solver()
        {
            GameLogic.GetInstance().Controller.Button_Activation(false);//버튼 조작 막기
            Tick_Count = 0;
            MOVEING = 0;
            Node_Check_Count = 0;
            Puzzle print;
            while (true)
            {
                _OPEN = new List<Puzzle>();
                _CLOSED = new List<Puzzle>();

                
                Synchronization_DATA();//현재 보드 정보와 알고리즘 보드와 동기화
                print = this.A_Star_search();// A스타 알고리즘 실행
                if (print.Equals(Goal))
                {
                    break;
                }
                //오픈 리스트와 클로즈 리스트 클리어로 메모리 관리 및 성능 향상
                _OPEN.Clear();
                _CLOSED.Clear();
                //정해진 한계 시간 및 한계 노드 검사시 중간 풀이 과정 출력
                MOVEING += PrintPath(print);
            }
            if (print == null)
            {
#if DEBUG
                Debug.WriteLine("Fail Auto Puzzle Solve");
#endif
            }
            else
            {
                MOVEING += PrintPath(print);
                GameLogic.GetInstance().Controller.UI_auto_result_text.Text = $"[{Tick_Count} Ms초 경과][{GameLogic.GetInstance().MOVEING}번 이동]"; 
#if DEBUG
                Debug.WriteLine("\nSuccess Auto Puzzle Solve");
                Debug.WriteLine($"{Tick_Count} Ms  ,  {GameLogic.GetInstance().MOVEING} 이동  ,  {Node_Check_Count}번 노드검사");
#endif
            }
            GameLogic.GetInstance().Controller.Button_Activation(true);// 버튼 조작 풀기
            _OPEN.Clear();
            _CLOSED.Clear();
            //오픈 리스트와 클로즈 리스트 클리어
            return;
        }
        //
        #endregion
        //

        #region A스타 알고리즘
        // A 스타 알고리즘 , A* 알고리즘

        /* < A스타 알고리즘 의사코드 >
         * 
         * open ←[시작노드]
         * closed ←[ ]
         * while { open != [ ] do
         *      X ← open pop(리스트에서 가장 평가 함수의 값이 좋은 노드)
         *          if( X == goal ) {return SUCCESS}
         *      else
         *          X의 자식 노드를 생성.
         *          closed ← X push
         *          if(X의 자식노드가 open이나 closed에 있지 않으면)
         *              자식 노드의 평가 함수 값 f(n) = g(n) + h(n) 계산.
         *              open ← 자식 노드들 push
         *      }
         * return FAIL
         */
        private Puzzle A_Star_search()
        {
            Stopwatch sw = new Stopwatch();//ms 측정 + 알고리즘 시간 측정
            sw.Start();
            _OPEN.Add(Start);//시작 정보 입력
            Puzzle X = null;
            int Limit = 0;// 노드 검사 리미트 카운트
            while (_OPEN.Count > 0)//오픈 리스트가 비어있으면 종료
            {
                X = Find_Best_Way();
                _OPEN.Remove(X);//요소 삭제
                ++Limit;
                if (X == null)//오픈 리스트가 비어있으면 검사할 노드가 없으므로 자동완성 불가
                {
                    return null;
                }
                ++Node_Check_Count;//노드 체크 증가
                //퍼즐이 완성되어 전체 과정 출력  +  검사 리미트 횟수 초과 및 시간 초과로 중간과정 출력
                if (X.Clear_Puzzle(Goal.Board) || Limit > 8000 || sw.ElapsedMilliseconds > 10000)
                {
                    sw.Stop();
                    Tick_Count += sw.ElapsedMilliseconds;//전체 완성까지의 틱(ms) 누적
                    sw.Reset();
                    return X;
                }
                else
                {
                    Puzzle[] child = new Puzzle[4];// 상하좌우 자식 노드 생성
                    child[0] = X.Up();
                    child[1] = X.Down();
                    child[2] = X.Left();
                    child[3] = X.Right();

                    _CLOSED.Add(X);//X를 closed 리스트에 추가한다.

                    for (int i = 0; i < 4; i++)
                    {
                        if (child[i] != null)
                        {
                            //자식 노드가 오픈 리스트와 클로즈 리스트에 없으면
                            if (!Find(child[i]))
                            {
                                //자식 노드의 평가값 F(n) , G(n) , H(N) 값 구하기 및 오픈 리스트에 넣기
                                switch(Heuristic_Selection)//코드 속의 상수값 변경으로 평가함수 변경
                                {//유저가 평가함수 변경 막기
                                    case (int)Heuristic_Menu.WrongNumber:
                                        child[i].Heuristic_Func_1(Goal.Board);
                                        break;
                                    case (int)Heuristic_Menu.ManhattanDistance:
                                        child[i].Heuristic_Func_2(Goal.Board);
                                        break;
                                    case (int)Heuristic_Menu.WrongNumber_and_ManhattanDistance:
                                        child[i].Heuristic_Func_3(Goal.Board);
                                        break;
                                    case (int)Heuristic_Menu.WrongProcession:
                                        child[i].Heuristic_Func_4(Goal.Board);
                                        break;
                                }
                                child[i].Evaluation_Calc();
                                _OPEN.Add(child[i]);
                            }
                        }
                    }
                }
            }
            return null;
        }
        #endregion


        #region 평가값이 제일 좋은 노드를 오픈 리스트에서 반환하는 함수
        // 우선순위 큐와 비슷한 기능을 하는 리스트 함수
        private Puzzle Find_Best_Way()
        {
            //평가값이 제일 좋은 함수를 반환
            Puzzle Best = null;
            int count = int.MaxValue;
            if (_OPEN.Count <= 0)
            {
                return null;
            }
            else
            {
                foreach (Puzzle item in _OPEN)
                {
                    if (item.Evaluation < count)
                    {
                        count = item.Evaluation;
                    }
                }
                foreach (Puzzle item in _OPEN)
                {
                    if (item.Evaluation == count)
                    {
                        Best = item;
                        //return item;
                    }
                }
                if (Best != null)
                {
                    return Best;
                }
            }
            return null;
        }
        #endregion


        #region 오픈리스트와 클로즈리스트에 중복된 값이있는지 검사
        //오픈리스트에 같은 보드가 있으면 평가값 계산후 더 좋은 평가값을 가진 보드 등록
        private bool Find(Puzzle data)
        {
            foreach (Puzzle item in _OPEN)
            {
                if (item.Equals(data))
                {
                    switch (Heuristic_Selection)
                    {
                        case (int)Heuristic_Menu.WrongNumber:
                            data.Heuristic_Func_1(Goal.Board);
                            break;
                        case (int)Heuristic_Menu.ManhattanDistance:
                            data.Heuristic_Func_2(Goal.Board);
                            break;
                        case (int)Heuristic_Menu.WrongNumber_and_ManhattanDistance:
                            data.Heuristic_Func_3(Goal.Board);
                            break;
                        case (int)Heuristic_Menu.WrongProcession:
                            data.Heuristic_Func_4(Goal.Board);
                            break;
                    }
                    data.Evaluation_Calc();
                    if(item.Evaluation > data.Evaluation)
                    {
                        _OPEN.Remove(item);//평가 값 낮은 보드 오픈리스트에서 삭제
                        _OPEN.Add(data);//평가 값 더 좋은 보드 오픈리스트에 등록
                    }
                    return true;
                }
            }
            foreach (Puzzle item in _CLOSED)
            {
                if (item.Equals(data))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion


        #region 자동완성 보드에서 공백 좌표 찾기
        //자동완성 보드에서 공백 좌표 찾는 함수
        private void Find_Blank(int[,] board, int Max_x, int Max_y)
        {
            for (int y = 0; y < Max_y; y++)
            {
                for (int x = 0; x < Max_x; x++)
                {
                    if (board[y, x] == 0)
                    {
                        point_x = x;
                        point_y = y;
                        return;
                    }
                }
            }
        }
        #endregion
        
        //
        #endregion
        //
    }
}
