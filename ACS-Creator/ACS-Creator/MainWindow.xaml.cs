namespace ACS.Creator
{
    public partial class MainWindow
    {
        public static MainWindow instance;
        public MainWindow()
        {
            instance = this;
            InitializeComponent();
            Manager.New_code_field();
            Manager.Instance_projectView();
            Manager.OpenOutputWindow();
            Manager.OpenErrorWindow();
        }



    }
}
