using System.Reflection.Emit;
using System.Windows.Forms;
using Emgu.CV;

namespace WindowsNotForm
{

    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();


            Application.Run(new Form1());
        }
    }
}