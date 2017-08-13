using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ACS.Creator.Components.Output
{
    /// <summary>
    /// Output.xaml 的交互逻辑
    /// </summary>
    public partial class Output : UserControl
    {
        public static Output instance;
        public Output()
        {
            instance = this;
            InitializeComponent();
        }

        public void Add(string t,bool time=false,bool sync_to_status=false)
        {
            var _time = "";

            if (time) _time = "[" + DateTime.Now.ToLongTimeString().ToString() + "] ";
                Box.Text += _time+t + "\n";

            if(sync_to_status)
                Manager.Set_statusbar(_time + t + "\n");
        }

        public void Clear()
        {
            Box.Text = "";
        }
    }
}
