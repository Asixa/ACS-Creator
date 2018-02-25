using System;
using System.Linq;
using System.Windows;
using ACS.Creator.Components.ProjectView;
using Xceed.Wpf.AvalonDock.Layout;
using System.Windows.Media;
using ACS.Creator.Components.Editor;
using ACS.Creator.Components.NewProject;

namespace ACS.Creator
{
    internal class Manager
    {
        public static Manager instance;

        public static ProjectManager project_manager;

        public string CurrentProject;

        public Manager()
        {
            instance = this;
        }

        public static void Instance_projectView()
        {
            project_manager=new ProjectManager();
            MainWindow.instance.Project_grid.Children.Add(project_manager);
        }

        public static Components.Editor.CodeField New_code_field(string header="新的代码")
        {
            var first_document_pane =MainWindow.instance.DockManager.Layout.Descendents().OfType<LayoutDocumentPane>().FirstOrDefault();
            if (first_document_pane == null) return null;
            var code = new Components.Editor.CodeField();
            var doc = new LayoutDocument
            {
                Title =header,
                Content = code
            };
            code.host_panel = doc;
            code.host_panel_name = doc.Title;
            first_document_pane.Children.Add(doc);
            doc.IsSelected = true;
            return code;
        }

        public static Components.Editor.CodeField New_code_field_by_path(string path)
        {
            var code = new Components.Editor.CodeField {file_path = path};
            code.Editor.Load(path);
            var first_document_pane = MainWindow.instance.DockManager.Layout.Descendents().OfType<LayoutDocumentPane>().FirstOrDefault();
            if (first_document_pane == null) return null;
            var doc = new LayoutDocument
            {
                Title = System.IO.Path.GetFileName(path),
                Content = code,
                IsSelected = true
            };
            code.host_panel = doc;
            code.host_panel_name = doc.Title;
            first_document_pane.Children.Add(doc);
            return code;
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

        public static Webbrowser web_preview;
       public static Webbrowser PreviewWebpage(string path)
        {
            if (web_preview != null)
            {
                web_preview.Web.Source=new Uri(path);
                web_preview.Web.Refresh();
                return web_preview;
            }LayoutDocumentPane documentPane = new LayoutDocumentPane();
            LayoutDocument document = new LayoutDocument();
            document.Title = "WebPreview";
            Webbrowser web=new Webbrowser();
            document.Content = web;
            documentPane.Children.Add(document);
            var parent = (LayoutDocumentPaneGroup)MainWindow.instance.DockManager.Layout.Descendents().OfType<LayoutDocumentPane>().FirstOrDefault().Parent;
            parent.Children.Add(documentPane);
            web_preview = web;web_preview.Web.Source=new Uri(path);
            return web_preview;
        }

        public static void OnNewProject()
        {
            NewProject window=new NewProject();
            window.Show();
        }

    }
}
