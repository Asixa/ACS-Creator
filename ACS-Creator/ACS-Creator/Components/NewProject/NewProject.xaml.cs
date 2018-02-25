using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
namespace ACS.Creator.Components.NewProject
{
    /// <summary>
    /// NewProject.xaml 的交互逻辑
    /// </summary>
    public partial class NewProject
    {
        public NewProject()
        {
            InitializeComponent();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {

                FolderBrowserDialog m_Dialog = new FolderBrowserDialog();
                DialogResult result = m_Dialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }
                string m_Dir = m_Dialog.SelectedPath.Trim();
                path.Text = m_Dir;
            
        }

        private void Set_Up_Click(object sender, RoutedEventArgs e)
        {
            if (ProjectName.Text == null)
            {
                return;
            }
            if (path.Text == null)
            {
                return;
            }
            var Path = path.Text + "/" + ProjectName.Text;
            if (!Directory.Exists(Path))
            {
                //System.Windows.MessageBox.Show(Path);
                Directory.CreateDirectory(Path);
            }
            var code = Manager.New_code_field("index.html");
            code.Editor.Text += "<!-- 这里是主页 -->"+Environment.NewLine+"Hello World";
            code.file_path = Path + "/" + "index.html";
            code.Save_file();
            code.Code_Class = "HTML";// code.Editor.SyntaxHighlighting=
            Manager.PreviewWebpage(Path + "/" + "index.html");
            this.Close();}
    }
}
