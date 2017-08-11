using System.Linq;
using System.Windows;
using ACS.Creator.Components.ProjectView;
using Xceed.Wpf.AvalonDock.Layout;

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
            var doc = new LayoutDocument
            {
                Title = "新的代码",
                Content = new Components.Editor.CodeField()
            };
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
            first_document_pane.Children.Add(doc);
        }
    }
}
