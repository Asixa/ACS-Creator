using System.Linq;
using System.Windows;
using ACS.Creator.Components.ProjectView;
using Xceed.Wpf.AvalonDock.Layout;
using System.Windows.Media;
using ACS.Creator.Components.Editor;

namespace ACS.Creator
{
    internal class Manager
    {
        public static Manager instance;

        public static ProjectManager project_manager;

        public Manager()
        {
            instance = this;
       
        }

        public static void Instance_projectView()
        {
            project_manager=new ProjectManager();
            MainWindow.instance.Project_grid.Children.Add(project_manager);
        }

        public static void New_code_field(object sender = null, RoutedEventArgs e = null)
        {
            var first_document_pane =MainWindow.instance.DockManager.Layout.Descendents().OfType<LayoutDocumentPane>().FirstOrDefault();
            if (first_document_pane == null) return;
            var code = new Components.Editor.CodeField();
            var doc = new LayoutDocument
            {
                Title = "新的代码",
                Content = code
            };
            code.host_panel = doc;
            code.host_panel_name = doc.Title;
            first_document_pane.Children.Add(doc);
            doc.IsSelected = true;
        }

        public static void New_code_field_by_path(string path)
        {
            var code = new Components.Editor.CodeField {file_path = path};
            code.Editor.Load(path);
            var first_document_pane = MainWindow.instance.DockManager.Layout.Descendents().OfType<LayoutDocumentPane>().FirstOrDefault();
            if (first_document_pane == null) return;
            var doc = new LayoutDocument
            {
                Title = System.IO.Path.GetFileName(path),
                Content = code,
                IsSelected = true
            };
            code.host_panel = doc;
            code.host_panel_name = doc.Title;
            first_document_pane.Children.Add(doc);
        }

        public static void OpenErrorWindow()
        {
            var anchorable = new LayoutAnchorable
            {
                Title = "错误列表",
                Content = new Components.ErrorList.ErrorList()
            };
            anchorable.AddToLayout(MainWindow.instance.DockManager, AnchorableShowStrategy.Bottom);
            anchorable.AutoHideHeight = 140;
            anchorable.AutoHideMinHeight = 100;
        }

        public static void OpenOutputWindow()
        {
            var anchorable = new LayoutAnchorable
            {
                Title = "输出",
                Content = new Components.Output.Output()
            };
            anchorable.AddToLayout(MainWindow.instance.DockManager, AnchorableShowStrategy.Bottom);
            anchorable.AutoHideHeight = 140;
            anchorable.AutoHideMinHeight = 100;
        }

        public static void Set_statusbar(string t,string color="")
        {
            MainWindow.instance.Status.Text = t;
            if(color!="")
            MainWindow.instance.StatusBar.Background = (Brush)(new BrushConverter().ConvertFrom(color));
        }

       public static void CutTest(object sender=null, RoutedEventArgs e=null)
        {
            LayoutDocumentPane documentPane = new LayoutDocumentPane();
            LayoutDocument document = new LayoutDocument();
            document.Title = "document";

            var brower = new Webbrowser
            {
             mychrome =
             {
                 Address = "www.baidu.com"
             }
            };
          
            documentPane.Children.Add(document);
            document.Content = brower;
            var parent = (LayoutDocumentPaneGroup)MainWindow.instance.DockManager.Layout.Descendents().OfType<LayoutDocumentPane>().FirstOrDefault().Parent;
            parent.Children.Add(documentPane);

        }

    }
}
