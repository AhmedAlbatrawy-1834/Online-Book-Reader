using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace ClassLibrary { 
    class Players
    {
        public int TotalScore=0;
        public string PlayerSimbol;
        public string PlayerName;
        public static int ContPlayers = 0;
        public  ConsoleColor color;

        public Players(string name="")
        {
            name = name.Trim();
            if (ContPlayers == 0)
            {
                this.PlayerSimbol = "X";
                this.PlayerName = (name == "") ? "X" : name;
                color = ConsoleColor.Red;
            }
            else
            {
                this.PlayerSimbol = "O";
                this.PlayerName = (name == "") ? "O" : name;
                color = ConsoleColor.Blue;
            }
            ContPlayers++;
        }

        public void RoundWinner(bool check)
        {
            Console.ForegroundColor = color;
            Console.WriteLine($"{this.PlayerName} Wins!!");
            Console.ResetColor();
        }
    }

    public struct item
    {
        public string value;
        public ConsoleColor color;
        public item(string value =" ", ConsoleColor color=ConsoleColor.White)
        {
            this.value= value;
            this.color = color;
        }
    }
    class GamePlay
    {
        

        private item[,]  GameGrid=new item[3, 3]
        {
            {new item(" "),new item(" ") ,new item(" ")},
            {new item(" "),new item(" ") ,new item(" ")},
            {new item(" "),new item(" ") ,new item(" ")}
        };

        public void ChangeGrid(int r,int c, item s)
        {
            if (this.CheckValidPlace(r, c))
            {
                GameGrid[r-1,c-1].value = s.value;
                GameGrid[r-1, c-1].color = s.color;
                
            }
            else
            {
                throw  new Exception();
            }
            
        }

        public void Reset()
        {
            GameGrid = new item[3, 3]
            {
                {new item(" "),new item(" ") ,new item(" ")},
                {new item(" "),new item(" ") ,new item(" ")},
                {new item(" "),new item(" ") ,new item(" ")}
            };

            SystemGame.cells = 0;

        }

        private bool CheckValidPlace(int r, int c)
        {
            if (this.GameGrid[r-1,c-1].value ==" ")
            {
                return true;
            }
            return false;
        }

        public item[,] GetGrid()
        {
            return GameGrid;
        }
        public  string[,] GetStringGrid()
        {
            string[,] grid = new string[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    grid[i, j] = GameGrid[i, j].value;
                }
            }
            return grid;
        }


    }


    public class SystemGame
    {
        public static int cells = 0;
        private Players P1;
        private Players P2;
        private Players current;
        private GamePlay game=new GamePlay();

        public void Exit()
        {
            Console.WriteLine("thank you for try my game!!!");
            Thread.Sleep(1000);
            Environment.Exit(0);
        }

        private bool CheckWinner()
        {
            
            string[,] grid = game.GetStringGrid();
            if (grid[1, 1] != " " && grid[1, 1] == grid[2, 0] && grid[2, 0] == grid[0, 2] )
                return true;

            else if (grid[1,1]!=" " && grid[1, 1] == grid[0, 0] && grid[0, 0] == grid[2, 2] )
                return true;

            else if (grid[1, 1] != " " && grid[1, 1] == grid[0, 1] && grid[0, 1] == grid[2, 1] )
                return true;

            else if(grid[1, 1] != " " && grid[1, 1] == grid[1, 0] && grid[1, 0] == grid[1, 2] )
                return true;
            else if (grid[0, 0] != " " && grid[0, 0] == grid[0, 1] && grid[0, 1] == grid[0,2] )
                return true;
            else if(grid[0, 0] != " " && grid[0, 0] == grid[1, 0] && grid[1, 0] == grid[2, 0])
                return true;
            else if (grid[2, 2] != " " && grid[2, 2] == grid[2, 0] && grid[2, 0] == grid[2,1] )
                return true;
            else if (grid[2,2]!=" " &&grid[2, 2] == grid[1, 2] && grid[1, 2] == grid[0, 2] )
                return true;

            return false;
            
        }
        public static void Welcome()
        {
            string HiWord = "Welcome In X O Game";
            Console.CursorLeft = 40;
            Console.CursorTop = 10;
            //Console.BackgroundColor = ConsoleColor.Green;
            foreach (char c in HiWord)
            {
                if(c=='X')Console.ForegroundColor = ConsoleColor.Red;
                else if(c=='O')Console.ForegroundColor= ConsoleColor.Blue;

                Console.Write(c);
                Console.ResetColor();
                //Console.BackgroundColor = ConsoleColor.Green;
                Thread.Sleep(175);
            }
            Console.ResetColor();
            Thread.Sleep(1000);
            
        }
        private void Switch(Players current)
        {
            if (current.Equals(P1))
            {
                this.current = P2;
            }
            else
            {
                this.current = P1;
            }
        }
        private void SetPlayersData()
        {
            string name;
            Console.Write("\nEnter player1 name: ");
            name = Console.ReadLine();
            P1 = new Players(name);
            Console.Write("Enter player1 name: ");
            name = Console.ReadLine();
            P2 = new Players(name);
            current = P1;
            Console.Clear();
        }

        private void Continue()
        {
            Console.WriteLine("Do you want continue?(Y/N)");
            if (Console.ReadLine().ToLower() == "y")
            {
                game.Reset();
                Switch(current);
                Play();
            }
            else
            {
                FinalWinner();
                Console.WriteLine("do you want start new game?(Y/N)");
                if (Console.ReadLine().ToLower() == "y")
                {
                    current = P1;
                    P1.TotalScore = 0;
                    P2.TotalScore = 0;
                    Players.ContPlayers = 0;
                    game.Reset();
                    Console.Clear();
                    Run();
                }
                else
                {
                    Exit();
                }
                
            }
        }
        private void PrintFormat()
        {
            Console.Clear();
            item[,] grid = game.GetGrid();
            Console.ForegroundColor = grid[0, 0].color;
            Console.Write($" {grid[0, 0].value} ");
            Console.ResetColor();
            Console.Write("|");
            Console.ForegroundColor = grid[0, 1].color;
            Console.Write($" {grid[0, 1].value} ");
            Console.ResetColor();
            Console.Write("|");
            Console.ForegroundColor = grid[0, 2].color;
            Console.Write($" {grid[0, 2].value}\n");
            Console.ResetColor();
            Console.Write("---+---+---\n");
            Console.ForegroundColor = grid[1, 0].color;
            Console.Write($" {grid[1, 0].value} ");
            Console.ResetColor();
            Console.Write("|");
            Console.ForegroundColor = grid[1, 1].color;
            Console.Write($" {grid[1, 1].value} ");
            Console.ResetColor();
            Console.Write("|");
            Console.ForegroundColor = grid[1, 2].color;
            Console.Write($" {grid[1, 2].value}\n");
            Console.ResetColor();
            Console.Write("---+---+---\n");
            Console.ForegroundColor = grid[2, 0].color;
            Console.Write($" {grid[2, 0].value} ");
            Console.ResetColor();
            Console.Write("|");
            Console.ForegroundColor = grid[2, 1].color;
            Console.Write($" {grid[2, 1].value} ");
            Console.ResetColor();
            Console.Write("|");
            Console.ForegroundColor = grid[2, 2].color;
            Console.Write($" {grid[2, 2].value}\n");
            Console.ResetColor();
            


        }

        private void Valid(int r, int c)
        {
            if (r < 1 || r > 3) throw new Exception();
            if (c < 1 || c > 3) throw new Exception();

        }
        public void Play()
        {
            PrintFormat();
            Console.ForegroundColor = current.color; 
            Console.WriteLine($"{current.PlayerName} turn");
            Console.ResetColor();
            try
            {
                int r, c;
                Console.Write("enter Row (1-3): ");
                r = Convert.ToInt32(Console.ReadLine());
                Console.Write("enter column (1-3): ");
                c = Convert.ToInt32(Console.ReadLine());
                Valid(r, c);
                game.ChangeGrid(r, c, new item(current.PlayerSimbol, current.color));
                cells++;
                if (CheckWinner())
                {
                    PrintFormat();
                    Console.ForegroundColor=current.color;
                    Console.WriteLine($"{current.PlayerName} is Win!!");
                    Console.ResetColor();
                    if (current.Equals(P1)) P1.TotalScore++;
                    else P2.TotalScore++;
                    Continue();
                    
                }
                else if(CheckWinner()==false&&cells==9)
                {
                    PrintFormat();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Draw");
                    Console.ResetColor();
                    Continue();
                }
                else
                {
                    Switch(current);
                    Play();
                }
                

            }
            catch
            {
                Console.CursorLeft = 40;
                Console.WriteLine("****invalid input please try again****");
                Thread.Sleep(1000);
                Console.Clear();
                Play();
            }
            


        }
        public void FinalWinner()
        {
            Console.Clear();
            Console.WriteLine($"{P1.PlayerName} Score:{P1.TotalScore}");
            Console.WriteLine($"{P2.PlayerName} Score:{P2.TotalScore}");
            if (P1.TotalScore > P2.TotalScore)
            {
                Console.ForegroundColor = P1.color;
                Console.WriteLine($"{P1.PlayerName} Wins");
                Console.ResetColor();
            }
            else if (P1.TotalScore < P2.TotalScore)
            {
                Console.ForegroundColor = P2.color;
                Console.WriteLine($"{P2.PlayerName} Wins");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Draw");
                Console.ResetColor();
            }
        }
        public void Run()
        {
            SetPlayersData();
            Play();
        }
    }

}