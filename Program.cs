using OpenTK;
using OpenTK.Windowing.Desktop;

namespace AnemiaEngine
{
    class Program
    {
        public static void Main(string[] args)
        {
            GameWindow window = new Game();
            window.UpdateFrequency = 60f;
            window.Run();

            Console.ReadLine();
        }
    }
}