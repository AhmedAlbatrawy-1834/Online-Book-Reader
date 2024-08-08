using ClassLibrary;
using System.Collections;

namespace MyProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SystemGame.Welcome();
            SystemGame game = new SystemGame();
            game.Run();
        }
    }
}
