using System.Windows;

namespace ACS.Creator
{
    public partial class MainWindow
    {
        public static MainWindow instance;
        public MainWindow()
        {
            instance = this;
            InitializeComponent();
            Manager.Instance_projectView();
            Manager.OpenOutputWindow();
            Manager.OpenErrorWindow();
        }


        private void NewProject_Menu_OnClick(object sender, RoutedEventArgs e)
        {
            Manager.OnNewProject();}
    }
}
