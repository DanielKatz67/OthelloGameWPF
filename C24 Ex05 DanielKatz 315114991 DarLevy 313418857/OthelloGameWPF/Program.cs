using System.Windows.Forms;

namespace OthelloGameWPF
{
    public static class Program
    {
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormGameSettings());
        }
    }
}
