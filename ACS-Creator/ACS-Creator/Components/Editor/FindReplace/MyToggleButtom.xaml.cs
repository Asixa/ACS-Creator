using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ACS.Creator.Components.Editor.FindReplace
{
	/// <summary>
	/// MyToggleButtom.xaml 的交互逻辑
	/// </summary>
	public partial class MyToggleButtom : UserControl
	{
		//public static readonly DependencyProperty CheckedBrushProperty =
		//	DependencyProperty.Register("CheckedBrush", typeof(Brush), typeof(MyToggleButtom),
		//		new FrameworkPropertyMetadata(Brushes.DodgerBlue, OnColorChanged));

		//public static readonly DependencyProperty UncheckedBrushProperty =
		//	DependencyProperty.Register("UncheckedBrush", typeof(Brush), typeof(MyToggleButtom),
		//		new FrameworkPropertyMetadata("#000000", OnColorChanged));

		//public Brush CheckedBrush
		//{
		//	get => (Brush)GetValue(CheckedBrushProperty);
		//	set => SetValue(CheckedBrushProperty, value);
		//}
		//public Brush UncheckedBrush
		//{
		//	get => (Brush)GetValue(UncheckedBrushProperty);
		//	set => SetValue(UncheckedBrushProperty, value);
		//}
		public Brush UncheckedBrush = (Brush)(new BrushConverter().ConvertFrom("#000000"));

		public Brush CheckedBrush = Brushes.DodgerBlue;
		public MyToggleButtom my;
		public bool IsChecked;

		public MyToggleButtom()
		{
			InitializeComponent();
		}
		static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			IsChecked = !IsChecked;
			if (IsChecked)
			{
				Button.Background = CheckedBrush;
			}
			else
			{
				Button.Background = UncheckedBrush;
			}
		}
	}
}
