using System.Windows;

namespace INFOIBV
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void App_Shutdown(object sender, System.EventArgs e)
        {
            System.Environment.Exit(System.Environment.ExitCode); // Shutdown the entire application
        }
    }
}