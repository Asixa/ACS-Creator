using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ICSharpCode.AvalonEdit.Folding;
using System.Windows.Threading;
using System.Xml;
using ACS.Creator.Components.Editor.Completion;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Rendering;
using Xceed.Wpf.AvalonDock.Layout;
using ICSharpCode.AvalonEdit.Highlighting;



namespace ACS.Creator.Components.Editor
{
    /// <summary>
    /// CodeField.xaml 的交互逻辑
    /// </summary>
    public partial class CodeField : UserControl
    {
        public string file_path = "";
        public LayoutDocument host_panel;
        public string host_panel_name;
        private bool saved=false;



        public CodeField()
        {
            InitializeComponent();
            // "ACS.xshd"
            // Folding
            HighlightingComboBox_SelectionChanged();
                    var folding_update_timer = new DispatcherTimer {Interval = TimeSpan.FromSeconds(2)};
            folding_update_timer.Tick += foldingUpdateTimer_Tick;
            folding_update_timer.Start();
            // Folding

            Editor.TextArea.TextEntering += textEditor_TextArea_TextEntering;
            Editor.TextArea.TextEntered += textEditor_TextArea_TextEntered;   

            Editor.TextArea.TextEntered += TextArea_TextEntered;  
        }

        private void TextArea_TextEntered(object sender, TextCompositionEventArgs e)
        {
            saved = false;
            host_panel.Title = host_panel_name + "*";
        }

        public void CheckFileType(string type)
        {
            string high_light_type;
            switch (type.ToLower())
            {
                case ".cs": { high_light_type = "C#"; break; }
                case ".xml": { high_light_type = "XML"; break; }
                case ".xaml": { high_light_type = "XML"; break; }
                case ".html": { high_light_type = "HTML"; break; }
                case ".htm": { high_light_type = "HTML"; break; }
            }

        }

        
        #region Folding

        public FoldingManager folding_manager;
        private AbstractFoldingStrategy _folding_strategy;

        void HighlightingComboBox_SelectionChanged()
        {
            if (Editor.SyntaxHighlighting == null)
            {
                _folding_strategy = null;
            }
            else
            {
                switch (Editor.SyntaxHighlighting.Name)
                {
                    case "XML":
                        _folding_strategy = new ICSharpCode.AvalonEdit.Folding.XmlFoldingStrategy();
                        Editor.TextArea.IndentationStrategy = new ICSharpCode.AvalonEdit.Indentation.DefaultIndentationStrategy();
                        break;
                    case "C#":
                    case "ACS":
                    case "C++":
                    case "PHP":
                    case "Java":
                        Editor.TextArea.IndentationStrategy = new ICSharpCode.AvalonEdit.Indentation.CSharp.CSharpIndentationStrategy(Editor.Options);
                        _folding_strategy = new BraceFoldingStrategy();
                        break;
                    default:
                        Editor.TextArea.IndentationStrategy = new ICSharpCode.AvalonEdit.Indentation.DefaultIndentationStrategy();
                        _folding_strategy = null;
                        break;
                }
            }
            if (_folding_strategy != null)
            {
                if (folding_manager == null)
                    folding_manager = FoldingManager.Install(Editor.TextArea);
                _folding_strategy.UpdateFoldings(folding_manager, Editor.Document);
            }
            else
            {
                if (folding_manager != null)
                {
                    FoldingManager.Uninstall(folding_manager);
                    folding_manager = null;
                }
            }
        }

        void foldingUpdateTimer_Tick(object sender, EventArgs e)
        {
            _folding_strategy?.UpdateFoldings(folding_manager, Editor.Document);
        }
        #endregion

        #region Scale

        public bool holding_control,holding_shift;
        private const int MaxFontSize = 50;
        private const int MinFontSize = 1;

        private void Editor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                run_code();
            }

            if ((e.KeyboardDevice.Modifiers) == ModifierKeys.Control)
            {
                holding_control = true;
            }
            if ((e.KeyboardDevice.Modifiers) == ModifierKeys.Shift)
            {
                holding_shift = true;
            }
            if (holding_control)
            {
                if (holding_control)
                {
                    if((e.Key) == Key.S)
                    file_path = "";
                }
                if ((e.Key) == Key.S)
                {
                    Save_file();
                    
                }
            }
        }

        private void Editor_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.Key) == Key.LeftCtrl)
            {
                holding_control = false;
            }
            if ((e.Key) == Key.LeftShift)
            {
                holding_shift = false;
            }
        }

        private void Editor_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!holding_control) return;
            var value = e.Delta / 60;
            if (value <= 0 && Editor.FontSize > MinFontSize - value) Editor.FontSize += value;
            if (value > 0 && Editor.FontSize < MaxFontSize - value) Editor.FontSize += value;
        }
        #endregion

        private CompletionWindow _completion_window;
        #region AutoComletion
        void textEditor_TextArea_TextEntered(object sender, TextCompositionEventArgs e)
        {
          //  if (e.Text == ".")
          if(_completion_window==null&&false)
          {
               var bc = new BrushConverter();
              _completion_window = new CompletionWindow(Editor.TextArea) {Width = 300};
              _completion_window.CompletionList.Background = (Brush)bc.ConvertFrom("#FF252526");
              _completion_window.CompletionList.ListBox.Background = (Brush)bc.ConvertFrom("#FF252526");
              _completion_window.CompletionList.ListBox.Foreground = new SolidColorBrush(Colors.White);
              _completion_window.CompletionList.ListBox.BorderBrush = (Brush)bc.ConvertFrom("#FF252526");

              Add_data = _completion_window.CompletionList.CompletionData;
              if(Add_data.Count==0)
              foreach (string t in Completions)
              {
                  Add_data.Add(new MyCompletionData(t));
              }

             //   Add_data.Add(new MyCompletionData("Item1"));
             //   data.Add(new MyCompletionData("Item2"));
             //   data.Add(new MyCompletionData("Item3"));
             //   data.Add(new MyCompletionData("Another item"));

               // _completion_window.CompletionList.
                _completion_window.Show();
                _completion_window.Closed += delegate {
                    
                    _completion_window = null;
                };
            }
        }

        private TextCompositionEventArgs lastLetter;
        void textEditor_TextArea_TextEntering(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.Text[0]))
            {
                if (e.Text.Length > 0 && _completion_window != null)
                {
                    _completion_window.CompletionList.RequestInsertion(e);
                }
               return;
            }

            if (_completion_window == null)
            {
                var bc = new BrushConverter();
                _completion_window = new CompletionWindow(Editor.TextArea) { Width = 300 };
                _completion_window.CompletionList.Background = (Brush)bc.ConvertFrom("#FF252526");
                _completion_window.CompletionList.ListBox.Background = (Brush)bc.ConvertFrom("#FF252526");
                _completion_window.CompletionList.ListBox.Foreground = new SolidColorBrush(Colors.White);
                _completion_window.CompletionList.ListBox.BorderBrush = (Brush)bc.ConvertFrom("#FF252526");

                Add_data = _completion_window.CompletionList.CompletionData;
                if (Add_data.Count == 0)
                    foreach (var t in Completions)
                    {
                        Add_data.Add(new MyCompletionData(t));
                    }

                //IList<ICompletionData> data = _completion_window.CompletionList.CompletionData;
                //data.Add(new MyCompletionData("Item1"));
                //data.Add(new MyCompletionData("Item2"));
                //data.Add(new MyCompletionData("Item3"));
                //data.Add(new MyCompletionData("Another item"));

                // _completion_window.CompletionList.
                _completion_window.Show();
                _completion_window.Closed += delegate {

                    _completion_window = null;
                };
            }



            // do not set e.Handled=true - we still want to insert the character that was typed
        }
        #endregion

        private void NewFile_Drop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;
            if (e.Data.GetData(DataFormats.FileDrop) is System.Array array)
            {
                if (array.Length > 0)
                {
                    var msg = array.GetValue(0).ToString();
                    Manager.New_code_field_by_path(msg);
                }
            }
        }


        private void Save_file()
        {
            holding_control = holding_shift = false;
            if (file_path != "")
            {
                saved = true;
                host_panel.Title = host_panel_name;
                Editor.Save(file_path);
                //((MainWindow)Application.Current.MainWindow).
                //    SetStatusBar(" 保存文件:" + System.IO.Path.GetFileName(filePath));
            }

            else
            {
                var sfd = new Microsoft.Win32.SaveFileDialog
                {
                    Filter =
                        "文本文档(*.txt)|*.txt " +
                        "|ACS文件(*.acs)|*.acs" +
                        "|Python文件(*.py)|*.py" +
                        "|C#文件(*.cs)|*.cs" +
                        "|C文件(*.c)|*.c" +
                        "|C++文件(*.cpp)|*.cpp" +
                        "|html文件(*.html)|*.html "
                };

                if (sfd.ShowDialog() == true)
                {
                    saved = true;
                    file_path = sfd.FileName;
                    host_panel.Title =Path.GetFileName(file_path);
                    host_panel_name = host_panel.Title;
                    Editor.Save(sfd.FileName);
                    
                    //((MainWindow)Application.Current.MainWindow).
                    //    SetStatusBar(" 保存文件:" + System.IO.Path.GetFileName(sfd.FileName));
                }
            }
        }

        public void run_code()
        {
            if (file_path == "")
            {
                if (((ComboBoxItem)MainWindow.instance.runway.SelectedValue).Content.ToString() == "ACS")
                {
                    Editor.Save(Environment.CurrentDirectory+"/Tools/ACS/Data/index.acs");
                    //打开 Environment.CurrentDirectory+"/Tools/ACS/ACS.exe
                    System.Diagnostics.Process.Start(Environment.CurrentDirectory + "/Tools/ACS/ACS.exe");
                }
            }
            else
            {
                
            }

        }


        private IList<ICompletionData> Add_data;

        public List<string> Completions = new List<string>
        {
            "print",
            "input",
            "goto",
            "if"
        };

    }

}

