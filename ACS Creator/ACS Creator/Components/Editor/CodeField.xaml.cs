using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ICSharpCode.AvalonEdit.Folding;
using System.Windows.Threading;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Rendering;

namespace ACS.Creator.Components.Editor
{
    /// <summary>
    /// CodeField.xaml 的交互逻辑
    /// </summary>
    public partial class CodeField : UserControl
    {
        public string file_path = "";
        public CodeField()
        {
            InitializeComponent();

            // Folding
            HighlightingComboBox_SelectionChanged();
            var folding_update_timer = new DispatcherTimer {Interval = TimeSpan.FromSeconds(2)};
            folding_update_timer.Tick += foldingUpdateTimer_Tick;
            folding_update_timer.Start();
            // Folding

            //Editor.TextArea.TextEntering += textEditor_TextArea_TextEntering; // AutoComplete
            //Editor.TextArea.TextEntered += textEditor_TextArea_TextEntered;   //
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

        public bool holding_control;
        private const int MaxFontSize = 50;
        private const int MinFontSize = 1;

        private void Editor_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyboardDevice.Modifiers) == ModifierKeys.Control)
            {
                holding_control = true;
            }
            if ((e.Key) != Key.S) return;
            if (!holding_control) return;
            if (file_path != "")
            {
                Editor.Save(file_path);
                //((MainWindow)Application.Current.MainWindow).
                //    SetStatusBar(" 保存文件:" + System.IO.Path.GetFileName(filePath));
            }

            else
            {
                var sfd = new Microsoft.Win32.SaveFileDialog
                {
                    Filter =
                        "文本文档(*.txt)|*.txt |C#文件(*.cs)|*.cs| C文件(*.c)|*.c| C++文件(*.cpp)|*.cpp| html文件(*.html)|*.html "
                };

                if (sfd.ShowDialog() == true)
                {
                    Editor.Save(sfd.FileName);
                    //((MainWindow)Application.Current.MainWindow).
                    //    SetStatusBar(" 保存文件:" + System.IO.Path.GetFileName(sfd.FileName));
                }
            }
        }

        private void Editor_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.Key) == Key.LeftCtrl)
            {
                holding_control = false;
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

		#region Line

	    public void line()
	    {
		        
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
    }


	public class ImageElementGenerator : VisualLineElementGenerator
	{
		// To use this class:
		// textEditor.TextArea.TextView.ElementGenerators.Add(new ImageElementGenerator(basePath));

		readonly static Regex imageRegex = new Regex(@"<img src=""([\.\/\w\d]+)""/?>",
			RegexOptions.IgnoreCase);
		readonly string basePath;

		public ImageElementGenerator(string basePath)
		{
			if (basePath == null)
				throw new ArgumentNullException("basePath");
			this.basePath = basePath;
		}

		Match FindMatch(int startOffset)
		{
			// fetch the end offset of the VisualLine being generated
			int endOffset = CurrentContext.VisualLine.LastDocumentLine.EndOffset;
			TextDocument document = CurrentContext.Document;
			string relevantText = document.GetText(startOffset, endOffset - startOffset);
			return imageRegex.Match(relevantText);
		}

		/// Gets the first offset >= startOffset where the generator wants to construct
		/// an element.
		/// Return -1 to signal no interest.
		public override int GetFirstInterestedOffset(int startOffset)
		{
			Match m = FindMatch(startOffset);
			return m.Success ? (startOffset + m.Index) : -1;
		}

		/// Constructs an element at the specified offset.
		/// May return null if no element should be constructed.
		public override VisualLineElement ConstructElement(int offset)
		{
			Match m = FindMatch(offset);
			// check whether there's a match exactly at offset
			if (m.Success && m.Index == 0)
			{
				BitmapImage bitmap = LoadBitmap(m.Groups[1].Value);
				if (bitmap != null)
				{
					Image image = new Image();
					image.Source = bitmap;
					image.Width = bitmap.PixelWidth;
					image.Height = bitmap.PixelHeight;
					// Pass the length of the match to the 'documentLength' parameter
					// of InlineObjectElement.
					return new InlineObjectElement(m.Length, image);
				}
			}
			return null;
		}

		BitmapImage LoadBitmap(string fileName)
		{
			// TODO: add some kind of cache to avoid reloading the image whenever the
			// VisualLine is reconstructed
			try
			{
				string fullFileName = Path.Combine(basePath, fileName);
				if (File.Exists(fullFileName))
				{
					BitmapImage bitmap = new BitmapImage(new Uri(fullFileName));
					bitmap.Freeze();
					return bitmap;
				}
			}
			catch (ArgumentException)
			{
				// invalid filename syntax
			}
			catch (IOException)
			{
				// other IO error
			}
			return null;
		}
	}

	public class ColorizeAvalonEdit : DocumentColorizingTransformer
	{
		protected override void ColorizeLine(DocumentLine line)
		{
			int lineStartOffset = line.Offset;
			string text = CurrentContext.Document.GetText(line);
			int start = 0;
			int index;
			while ((index = text.IndexOf("AvalonEdit", start)) >= 0)
			{
				base.ChangeLinePart(
					lineStartOffset + index, // startOffset
					lineStartOffset + index + 10, // endOffset
					(VisualLineElement element) => {
						// This lambda gets called once for every VisualLineElement
						// between the specified offsets.
						Typeface tf = element.TextRunProperties.Typeface;
						// Replace the typeface with a modified version of
						// the same typeface
						element.TextRunProperties.SetTypeface(new Typeface(
							tf.FontFamily,
							FontStyles.Italic,
							FontWeights.Bold,
							tf.Stretch
						));
					});
				start = index + 1; // search for next occurrence
			}
		}
	}
}

