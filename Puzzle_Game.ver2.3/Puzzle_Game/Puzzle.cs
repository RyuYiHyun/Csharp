using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle_Game
{
    class Puzzle
    {
        #region 데이터
        //부모 노드
        public Puzzle Parent { get; set; }
        // 값
        private int Move_count { get; set; }// g(n) = 퍼즐을 이동한 값 or 노드트리에서의 깊이
        private int Heuristic { get; set; }/* h(n) = 휴리스틱함수 또는 평가함수, 숫자퍼즐에서 사용하는 대표적인 휴리스틱함수
                                           */
        public int Evaluation { get; set; } // Evaluation = Heuristic + Move_count; f(n) = 최종 평가 값
        // 퍼즐에서의 공백 좌표
        private int Blank_x { get; set; } // 퍼즐에서의 공백 좌표 X
        private int Blank_y { get; set; } // 퍼즐에서의 공백 좌표 Y
        // 보드정보
        public int[,] Board { get; set; }
        // 보드 크기
        private int MAX_X { get; set; }
        private int MAX_Y { get; set; }
        #endregion

        #region 함수
        //목표와 시작 보드를 만드는 생성자
        public Puzzle(int[,] _Board, int _X, int _Y, int _Move_count, int max_x, int max_y)
        {
            this.Parent = null;
            this.Board = _Board;

            this.Blank_x = _X;//보드의 공백위치 정보
            this.Blank_y = _Y;

            this.Move_count = _Move_count;
            this.Heuristic = int.MaxValue;//시작의 휴리스틱 값은 크게 해두는 것이 좋음(이론상 무한대가 제일 좋음)
            this.Evaluation = int.MaxValue;// 평가 값
            this.MAX_X = max_x;
            this.MAX_Y = max_y;
        }
        // 시작 보드에서 파생되는 자식을 생성하기 위한 생성자
        public Puzzle(Puzzle _Parent, int[,] _Board, int _X, int _Y, int _Move_count, int max_x, int max_y)
        {
            this.Parent = _Parent;
            this.Board = _Board;

            this.Blank_x = _X;
            this.Blank_y = _Y;

            this.Move_count = _Move_count;
            this.Heuristic = int.MaxValue;
            this.Evaluation = int.MaxValue;

            this.MAX_X = max_x;
            this.MAX_Y = max_y;
        }
        // 최종 평가 값을 구하는 함수
        public int Evaluation_Calc()
        {
            this.Evaluation = this.Move_count + this.Heuristic;
            return this.Evaluation;
        }


        #region 상하좌우_이동_및_노드생성
        public Puzzle Up()
        {
            int B_X = this.Blank_x;
            int B_Y = this.Blank_y;
            if (B_Y <= 0)
            {
                return null;
            }
            else
            {
                int[,] T_Borad = this.Board.Clone() as int[,];//부모의 보드의 정보를 복사하여 변경한 후
                int tmp = T_Borad[B_Y, B_X];
                T_Borad[B_Y, B_X] = T_Borad[B_Y - 1, B_X];
                T_Borad[B_Y - 1, B_X] = tmp;
                // 부모의 노드의 값과 변경된 보드 정보등으로 자식 노드 생성
                return new Puzzle(this, T_Borad, B_X, B_Y - 1, this.Move_count + 1, this.MAX_X, this.MAX_Y);
            }
        }
        public Puzzle Down()
        {
            int B_X = this.Blank_x;
            int B_Y = this.Blank_y;
            if (B_Y >= (MAX_Y - 1))
            {
                return null;
            }
            else
            {
                int[,] T_Borad = this.Board.Clone() as int[,];
                int tmp = T_Borad[B_Y, B_X];
                T_Borad[B_Y, B_X] = T_Borad[B_Y + 1, this.Blank_x];
                T_Borad[B_Y + 1, B_X] = tmp;
                return new Puzzle(this, T_Borad, B_X, B_Y + 1, this.Move_count + 1, this.MAX_X, this.MAX_Y);
            }
        }
        public Puzzle Left()
        {
            int B_X = this.Blank_x;
            int B_Y = this.Blank_y;
            if (B_X <= 0)
            {
                return null;
            }
            else
            {
                int[,] T_Borad = this.Board.Clone() as int[,];
                int tmp = T_Borad[B_Y, B_X];
                T_Borad[B_Y, B_X] = T_Borad[this.Blank_y, B_X - 1];
                T_Borad[B_Y, B_X - 1] = tmp;
                return new Puzzle(this, T_Borad, B_X - 1, B_Y, this.Move_count + 1, this.MAX_X, this.MAX_Y);
            }
        }
        public Puzzle Right()
        {
            int B_X = this.Blank_x;
            int B_Y = this.Blank_y;
            if (B_X >= (MAX_X - 1))
            {
                return null;
            }
            else
            {
                int[,] T_Borad = this.Board.Clone() as int[,];
                int tmp = T_Borad[B_Y, B_X];
                T_Borad[B_Y, B_X] = T_Borad[this.Blank_y, B_X + 1];
                T_Borad[B_Y, B_X + 1] = tmp;
                return new Puzzle(this, T_Borad, B_X + 1, B_Y, this.Move_count + 1, this.MAX_X, this.MAX_Y);
            }
        }
        #endregion


        //클리어 여부 검사
        public bool Clear_Puzzle(int[,] goal)
        {
            for (int y = 0; y < MAX_Y; y++)
            {
                for (int x = 0; x < MAX_X; x++)
                {
                    if (this.Board[y, x] != goal[y, x])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        //두 퍼즐 노드가 같은지 검사하는 함수
        public bool Equals(Puzzle other)
        {
            if (other == null)//검사하는 퍼즐보드가 null이면 false
            {
                return false;
            }
            if (ReferenceEquals(this, other))// 검사하는 퍼즐과 비교하는 퍼즐의 레퍼런스값이 같으면 트루
            {
                return true;
            }
            for (int y = 0; y < MAX_Y; y++)
            {
                for (int x = 0; x < MAX_X; x++)
                {
                    if (this.Board[y, x] != other.Board[y, x])
                    {
                        return false;//보드의 정보가 틀리면 false
                    }
                }
            }
            return true;//보드의 정보가 같으면 true
        }

        #region 휴리스틱 함수
        //휴리스틱 함수1 - 정답 보드와 같지않은 숫자의 개수
        public int Heuristic_Func_1(int[,] goal)
        {
            int h = 0;
            for (int y = 0; y < MAX_Y; y++)
            {
                for (int x = 0; x < MAX_X; x++)
                {
                    if ((this.Board[y, x] != goal[y, x]) && (goal[y, x] != 0))
                    {
                        ++h;
                    }
                }
            }
            this.Heuristic = h;
            return this.Heuristic;
        }
        //휴리스틱 함수 2 - 정답 보드와 같지 않은 숫자의 맨해튼 거리의 합
        public int Heuristic_Func_2(int[,] goal)
        {
            int size_x = MAX_X;
            int size_y = MAX_Y;
            int h = 0;
            for (int y = 0; y < size_y; y++)
            {
                for (int x = 0; x < size_x; x++)
                {
                    if (this.Board[y, x] != goal[y, x] && this.Board[y, x] != 0)
                    {
                        #region ManhattanDistance 
                        //퍼즐 타일에 대한 맨해튼 거리를 구합니다.
                        for (int temp_y = 0; temp_y < size_y; temp_y++)
                        {
                            for (int temp_x = 0; temp_x < size_x; temp_x++)
                            {
                                if (this.Board[y, x] == goal[temp_y, temp_x])
                                {
                                    h += (Math.Abs(x - temp_x) + Math.Abs(y - temp_y));
                                }
                            }
                        }
                        #endregion
                    }
                }
            }
            this.Heuristic = h;
            return this.Heuristic;
        }
        //휴리스틱 함수 3 - 휴리스틱 함수값 1과 2의 곱 -> 태스트 결과 제일 평균 성능이 우수함 (단, 최단거리를 보장하지는 않음. )
        public int Heuristic_Func_3(int[,] goal)
        {
            int size_x = MAX_X;
            int size_y = MAX_Y;
            int h = 0;
            int h2 = 0;
            for (int y = 0; y < size_y; y++)
            {
                for (int x = 0; x < size_x; x++)
                {
                    if (this.Board[y, x] != goal[y, x] && this.Board[y, x] != 0)
                    {
                        ++h2;
                        #region ManhattanDistance 
                        //퍼즐 타일에 대한 맨해튼 거리를 구합니다.
                        for (int temp_y = 0; temp_y < size_y; temp_y++)
                        {
                            for (int temp_x = 0; temp_x < size_x; temp_x++)
                            {
                                if (this.Board[y, x] == goal[temp_y, temp_x])
                                {
                                    h += (Math.Abs(x - temp_x) + Math.Abs(y - temp_y));
                                }
                            }
                        }
                        #endregion
                    }
                }
            }
            this.Heuristic = h * h2;
            return this.Heuristic;
        }
        //휴리스틱 함수 4 - 목표와 행의 값이 같지 않은 수의 개수 + 목표와 열의 값이 같지 않은 수의 개수의 합
        public int Heuristic_Func_4(int[,] goal)
        {
            int size_x = MAX_X;
            int size_y = MAX_Y;
            int h=0;
            int out_of_row = 0;//가로(행)
            int out_of_column = 0;//세로(열)
            for (int y = 0; y < size_y; y++)
            {
                for (int x = 0; x < size_x; x++)
                {
                    if (this.Board[y, x] != goal[y, x] && this.Board[y, x] != 0)
                    {
                        for (int temp_y = 0; temp_y < size_y; temp_y++)
                        {
                            for (int temp_x = 0; temp_x < size_x; temp_x++)
                            {
                                if (this.Board[y, x] == goal[temp_y, temp_x])
                                {
                                    if(y != temp_y)//행이 틀린 개수
                                    {
                                        ++out_of_row;
                                    }
                                    if (x != temp_x)// 열이 틀린 개수
                                    {
                                        ++out_of_column;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            h = out_of_row + out_of_column;
            this.Heuristic = h;
            return this.Heuristic;
        }
        #endregion//휴리스틱

        //디버그때 백그라운드 데이터 확인용
        public void Printer()
        {
            for (int y = 0; y < MAX_Y; y++)
            {
                for (int x = 0; x < MAX_X; x++)
                {
                    Debug.Write($"{this.Board[y, x],3}");
                }
                if (y < MAX_Y - 1)
                {
                    Debug.Write('\n');
                }
                else
                {
                    if (this.Evaluation == int.MaxValue)
                    {
                        Debug.Write($"     Start Node\n\n");
                    }
                    else if (this.Heuristic == 0)
                    {
                        Debug.Write($"     G({this.Move_count}) + H({this.Heuristic}) = F({this.Evaluation})");
                        Debug.Write($"  End Node\n");
                    }
                    else
                    {
                        Debug.Write($"     G({this.Move_count}) + H({this.Heuristic}) = F({this.Evaluation})\n\n");
                    }
                }
            }
        }
    #endregion//함수
    }
}
