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
using ACS.Creator.Components.Editor.FindReplace;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Rendering;
using Xceed.Wpf.AvalonDock.Layout;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Search;
using ACS.Creator.Components.Editor.HighlightLine;
using ICSharpCode.AvalonEdit;

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
        private bool saved = false;

        public string Code_Class="ACS";

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

            // Selection option
            Editor.TextArea.SelectionCornerRadius = 0;
		//	Editor.TextArea.SelectionBorder.Brush = (Brush)(new BrushConverter().ConvertFrom("#000000"));
			//Editor.TextArea.SelectionBrush = new SolidColorBrush(...);
			//Editor.TextArea.SelectionBorder = new SolidColorBrush(...);

			// Editor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("C#");

			// Selected line background
			Editor.TextArea.Caret.PositionChanged += (sender, e) =>
            Editor.TextArea.TextView.InvalidateLayer(KnownLayer.Background);
            Editor.TextArea.TextView.BackgroundRenderers.Add(
                new HighlightCurrentLineBackgroundRenderer(Editor));

    
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
                case ".cs":
                {
                    high_light_type = "C#";
                    break;
                }
                case ".xml":
                {
                    high_light_type = "XML";
                    break;
                }
                case ".xaml":
                {
                    high_light_type = "XML";
                    break;
                }
                case ".html":
                {
                    high_light_type = "HTML";
                    break;
                }
                case ".htm":
                {
                    high_light_type = "HTML";
                    break;
                }
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
                        Editor.TextArea.IndentationStrategy =
                            new ICSharpCode.AvalonEdit.Indentation.DefaultIndentationStrategy();
                        break;
                    case "C#":
                    case "ACS":
                    case "C++":
                    case "PHP":
                    case "Java":
                        Editor.TextArea.IndentationStrategy =
                            new ICSharpCode.AvalonEdit.Indentation.CSharp.CSharpIndentationStrategy(Editor.Options);
                        _folding_strategy = new BraceFoldingStrategy();
                        break;
                    default:
                        Editor.TextArea.IndentationStrategy =
                            new ICSharpCode.AvalonEdit.Indentation.DefaultIndentationStrategy();
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

        public bool holding_control, holding_shift;
        private const int MaxFontSize = 50;
        private const int MinFontSize = 1;

        private void Editor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                run_code();
            }
            if (e.Key == Key.F&&holding_control)
            {
                Search.ShowForReplace(Editor, 0);
            }
            if (e.Key == Key.H && holding_control)
            {
                Search.ShowForReplace(Editor, 1);
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
                    if ((e.Key) == Key.S)
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
            if (_completion_window == null && false)
            {
                var bc = new BrushConverter();
                _completion_window = new CompletionWindow(Editor.TextArea) {Width = 300};
                _completion_window.CompletionList.Background = (Brush) bc.ConvertFrom("#FF252526");
                _completion_window.CompletionList.ListBox.Background = (Brush) bc.ConvertFrom("#FF252526");
                _completion_window.CompletionList.ListBox.Foreground = new SolidColorBrush(Colors.White);
                _completion_window.CompletionList.ListBox.BorderBrush = (Brush) bc.ConvertFrom("#FF252526");

                Add_data = _completion_window.CompletionList.CompletionData;
                if (Add_data.Count == 0)
                    //foreach (string t in Completions)
                    //{
                    //    Add_data.Add(new MyCompletionData(t.na));
                    //}

                    //   Add_data.Add(new MyCompletionData("Item1"));
                    //   data.Add(new MyCompletionData("Item2"));
                    //   data.Add(new MyCompletionData("Item3"));
                    //   data.Add(new MyCompletionData("Another item"));

                    // _completion_window.CompletionList.
                    _completion_window.Show();
                _completion_window.Closed += delegate
                {

                    _completion_window = null;
                };
            }
        }

        private TextCompositionEventArgs lastLetter;

        void textEditor_TextArea_TextEntering(object sender, TextCompositionEventArgs e)
        {
            if (holding_control)
            {
                e.Handled = false;
            }

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
                _completion_window = new CompletionWindow(Editor.TextArea) {Width = 300};
                _completion_window.CompletionList.Background = (Brush) bc.ConvertFrom("#FF252526");
                _completion_window.CompletionList.ListBox.Background = (Brush) bc.ConvertFrom("#FF252526");
                _completion_window.CompletionList.ListBox.Foreground = new SolidColorBrush(Colors.White);
                _completion_window.CompletionList.ListBox.BorderBrush = (Brush) bc.ConvertFrom("#FF252526");

                Add_data = _completion_window.CompletionList.CompletionData;
                if (Add_data.Count == 0)
                    foreach (var t in Completions)
                    {
                        Add_data.Add(new MyCompletionData(t.name, t.description));
                    }

                //IList<ICompletionData> data = _completion_window.CompletionList.CompletionData;
                //data.Add(new MyCompletionData("Item1"));
                //data.Add(new MyCompletionData("Item2"));
                //data.Add(new MyCompletionData("Item3"));
                //data.Add(new MyCompletionData("Another item"));

                // _completion_window.CompletionList.
                _completion_window.Show();
                _completion_window.Closed += delegate
                {

                    _completion_window = null;
                };
            }



            // do not set e.Handled=true - we still want to insert the character that was typed
        }

        #endregion

        private void NewFile_Drop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;
            var msg = ((Array) e.Data.GetData(DataFormats.FileDrop)).GetValue(0)
                .ToString(); //        array.GetValue(0).ToString();
            Manager.New_code_field_by_path(msg);

        }


        public void Save_file()
        {
            holding_control = holding_shift = false;
            if (file_path != ""){
                saved = true;
                host_panel.Title = host_panel_name;
                Editor.Save(file_path);
                return;
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
                {saved = true;
                    file_path = sfd.FileName;
                    host_panel.Title = Path.GetFileName(file_path);
                    host_panel_name = host_panel.Title;
                    Editor.Save(sfd.FileName);

                    //((MainWindow)Application.Current.MainWindow).
                    //    SetStatusBar(" 保存文件:" + System.IO.Path.GetFileName(sfd.FileName));
                }
            }
        }

        private bool code_running=false;
        public void run_code(){
            if(code_running)return;

            if (Code_Class == "HTML")
            {
                Save_file();
                Editor.Save(file_path);Manager.PreviewWebpage(file_path);return;
            }
            
            if (file_path == "")
            {
                if (((ComboBoxItem) MainWindow.instance.runway.SelectedValue).Content.ToString() == "ACS")
                {
                    Output.Output.instance.Add(
                        TryFindResource("output_writing") + " " + Environment.CurrentDirectory +
                        "/Tools/ACS/Data/index.acs", true, true);
                    Editor.Save(Environment.CurrentDirectory + "/Tools/ACS/Data/index.acs");
                    //打开 Environment.CurrentDirectory+"/Tools/ACS/ACS.exe
                    Output.Output.instance.Add(
                        TryFindResource("output_opening") + " " + Environment.CurrentDirectory + "/Tools/ACS/ACS.exe",
                        true, true);
                    var acs = System.Diagnostics.Process.Start(Environment.CurrentDirectory + "/Tools/ACS/ACS.exe");
                    if (acs != null)
                    {
                        code_running = true;
                        acs.EnableRaisingEvents = true;
                        acs.Exited += code_closed;
                    }
                }
            }
            else
            {
                MessageBox.Show("未完成");
            }
        }

        void code_closed(object sender, EventArgs e)
        {
            MainWindow.instance.Dispatcher.BeginInvoke(new Action(() =>
            {
                Output.Output.instance.Add(
                    TryFindResource("output_closed").ToString(),
                    true);
                code_running = false;
                Manager.Set_statusbar(TryFindResource("statusbar_ready").ToString());
            }));
        }

        private IList<ICompletionData> Add_data;

        private void Editor_TextChanged(object sender, EventArgs e)
        {
            //Overview.Text = Editor.Text;
        }

        public List<SingleCompletion> Completions = new List<SingleCompletion>
        {
            new SingleCompletion("print","print(expression);\nPrint texts to the window"),
            new SingleCompletion("input","input(v);\ninput a value to the \"v\""),
            new SingleCompletion("goto","goto tag;\n goto the tag"),
            new SingleCompletion("if","if(expression){};\n if the expression is true,run the codes"),
            new SingleCompletion("true","value true"),
            new SingleCompletion("false","value false"),
        };

    }

    public class SingleCompletion
    {
        public string name, description;

        public SingleCompletion(string n,string d)
        {
            name = n;
            description = d;
        }
    }

}

