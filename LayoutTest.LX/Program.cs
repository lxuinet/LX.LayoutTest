using LX;

namespace LayoutTest.LX
{
    internal class Program
    {
        static void Main(string[] args)
        {
            App.OnRun += () => new MainForm();
            App.Run();
        }
    }
}
