   M S B A M L   `   `   `  ����U  QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	   F BACS Creator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:ACS.CreatorACS.Creator 5   K   M+clr-namespace:ACS.Creator.Components.OutputACS.Creator.Components.Output 5   I      ACS.Creator.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   g cXceed.Wpf.AvalonDock.Themes.Aero, Version=3.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4i eXceed.Wpf.AvalonDock.Themes.VS2010, Version=3.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4h dXceed.Wpf.AvalonDock.Themes.Metro, Version=3.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4i	 eXceed.Wpf.AvalonDock.Themes.VS2013, Version=2.9.0.1, Culture=neutral, PublicKeyToken=3e4669d2f30244f4[
 WXceed.Wpf.AvalonDock, Version=3.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4Aavalon,http://schemas.xceed.com/wpf/xaml/avalondock    	 
 5   	   RControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro    	 
 5   	   ` \ICSharpCode.AvalonEdit, Version=4.3.1.9429, Culture=neutral, PublicKeyToken=9cc39be672370310?
avalonedit.http://icsharpcode.net/sharpdevelop/avalonedit  5   	   %localclr-namespace:ACS.Creator  6K   =	iconPacks-http://metro.mahapps.com/winfx/xaml/iconpacks  5   	   8Output+clr-namespace:ACS.Creator.Components.Output  6I   Fdxlc;http://schemas.devexpress.com/winfx/2008/xaml/layoutcontrol  5   	      VSWindowStyleKey#���   5	   	     � WindowStartupLocation$  CenterScreen=�5   	    � WindowStyle$
 None=�5   	     MainwindowTitle � Title# �  5   	   (   "MahApps.Metro.Controls.MetroWindow   TitleForeground �#999988$	��600��5   	   $	��800��5   	   $	��600��5   	   $	��525��5   	       NonActiveWindowTitleBrush �	#FF2D2D30 � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $J Dpack://application:,,,/MahApps.Metro;component/Styles/VS/Colors.xaml?�6%   �� 5      $J Dpack://application:,,,/MahApps.Metro;component/Styles/VS/Styles.xaml?�6%   %    5         LeftWindowCommands 5      +   %MahApps.Metro.Controls.WindowCommands  5   
   	   RightWindowCommands	 5"        5#   
   .@�5%      �� $�� ��6   5&      .��5)      ]� .+�5+   
   o� $	��Top=�6   
 J� SnapsToDevicePixels
 . $��0,0,0,0q�6?   .@�5,      m� $��文件��6   .@�5-      m� $��新建��6   .@�5.      m� -   5         m�Name$ NewFile��5.      $��代码/文档��60   �� 5/      m� 50      -   5        $��NewProject_Menu��50      $��新建项目��6V   52      m� 53      $��打开��6   .@�54      m� $��文件��6   55      56      m� 57      $��编辑��6   .@�58      m� $��(〜￣△￣)〜��6   59      m� 5;      $��视图��6   .@�5<      m� $��(｀・ω・')��6   5=      m� 5?      $��项目��6   .@�5@      m� $��(￣ε(#￣) ��6   5A      m� 5C      $��生成��6   .@�5D      m� $��(/▔□▔)/��6   5E      m� 5G      $��工具��6   .@�5H      m� $��Σ(ﾟдﾟ;)��6   5I      m� 5K      $��窗口��6   .@�5L      m� $��(^・ω・^ )��6   5M      m� 5O      $��帮助��6   .@�5P      m� -   5        $
 Help��5P   3   $��ACS Creator 帮助��6   63   �� 5Q      ���	#FF4343466   m� 5R      -   5        $ About��5R      $��关于 ACS Creator��6*   5S      5T      c� 5V   
   $	��Top=�6   ���	#FF2D2D30$��10,0,0,0q�6>    5W       �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius K� IsItemsHost��%#  'c�       �� 5X      " c�61   .�5Y      ��  �@�OverridesDefaultStyle . 6>   �� 5Z       � �� 5[      �� 5\      " c�6.   .R�5]   "   �� $ 2��6)   $��1q�6:   ���	#FF2D2D30._�5^   &   a�  . $��0,1,2,2q�6F   5_   #   5`      5b      5c      5d      .@�5e      �� $
��Undo��6   .��5f      �� $��Images/ICO/Undo.png��6   $��16��65   5g      �� 5h      $
��Redo��6   .��5i      �� $��Images/ICO/Redo.png��6   $��16��65   5j      �� 5k      ���	#FF4343466   �� 5l      .��5m      �� $��Images/ICO/Open.png��6   $��16��65   5n      �� 5o      .��5p      �� $��Images/ICO/Save.png��6   $��16��65   5q      �� 5r      ���	#FF4343466   �� 5s      $	��Cut��6   .��5t      �� $��Images/ICO/Cut.png��6   $��16��64   5u      �� 5v      $
��Copy��6   .��5w      �� $��Images/ICO/Copy.png��6   $��16��65   5x      �� 5y      $��Paste��6   .��5z      �� $��Images/ICO/Paste.png��6   $��16��66   5{      �� 5|      $��Delete��6   .��5}      �� $��Images/ICO/Delete.png��6   $��16��67   5~      �� 5      ���	#FF4343466   �� 5�      .��5�      �� $��Images\run.png��6   $��16��60   5�      �� 5�      -   5         ��Name$ runway��5�      $��0q�6'   $��85��62   ���	#FF333337
������� J� OpacityMask
 �   ����	#FF434346 �� SelectedIndex$ 1��6�   .@�5�      �� $	��GCC��6   �� 5�      $	��ACS��6   5�      �� 5�      ���	#FF4343466   �� 5�      5�      �� 5�   
   -   5         ��Name$ 	StatusBar��5�      $��23��6'   $��Bottom=�63   ���	#FF007ACC.@�5�      �� .��5�      ��  �� Orientation$ 
Horizontal=�6   .+�5�      �� -   5         ��Name$ Status��5�       
�������$	��245��6C   �� 6P   $�就绪:��6T   6b   5�      5�      5�      � 5�   
   $��1��6   .+�5�      ) 
 #Xceed.Wpf.AvalonDock.DockingManager  -   5          Name$ DockManager��5�   $   
�������
�������   AllowMixedOrientation .  � ForceCursor .    Theme 5�      1 	 +Xceed.Wpf.AvalonDock.Themes.Vs2013DarkTheme  5�         Layout. 5�      , 
 &Xceed.Wpf.AvalonDock.Layout.LayoutRoot  -	   5        Root��   LeftSide 5�      2 
 ,Xceed.Wpf.AvalonDock.Layout.LayoutAnchorSide  5�      � 
 �Xceed.Wpf.AvalonDock.Layout.LayoutGroup`1[[Xceed.Wpf.AvalonDock.Layout.LayoutAnchorGroup, Xceed.Wpf.AvalonDock, Version=3.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]]   Children. 5�      3 
 -Xceed.Wpf.AvalonDock.Layout.LayoutAnchorGroup  -
   5        	LeftGroup��5�   7   5�         	RightSide 5�        5�      . 5�        -   5        
RightGroup��5�   7   5�          TopSide  5�        5�      . 5�        -   5        TopGroup��5�   7   5�      !   
BottomSide! 5�        5�      . 5�        -   5        BottomGroup��5�   7   5�      "   	RootPanel." 5�      -	 
 'Xceed.Wpf.AvalonDock.Layout.LayoutPanel	  -   5        	Panel���
 
 �Xceed.Wpf.AvalonDock.Layout.LayoutGroup`1[[Xceed.Wpf.AvalonDock.Layout.ILayoutPanelElement, Xceed.Wpf.AvalonDock, Version=3.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]]# 
  Children.# 5�      ; 
 5Xceed.Wpf.AvalonDock.Layout.LayoutAnchorablePaneGroup  -   5        LeftAnchorableGroup��� 
 �Xceed.Wpf.AvalonDock.Layout.LayoutPositionableGroup`1[[Xceed.Wpf.AvalonDock.Layout.ILayoutAnchorablePane, Xceed.Wpf.AvalonDock, Version=3.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]]$   	DockWidth$	$ 200 �5�   Z   � 
 �Xceed.Wpf.AvalonDock.Layout.LayoutGroup`1[[Xceed.Wpf.AvalonDock.Layout.ILayoutAnchorablePane, Xceed.Wpf.AvalonDock, Version=3.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]]%   Children.% 5�      6 
 0Xceed.Wpf.AvalonDock.Layout.LayoutAnchorablePane  -   5        LeftPane��� 
 �Xceed.Wpf.AvalonDock.Layout.LayoutGroup`1[[Xceed.Wpf.AvalonDock.Layout.LayoutAnchorable, Xceed.Wpf.AvalonDock, Version=3.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]]&   Children.& 5�   "   2 
 ,Xceed.Wpf.AvalonDock.Layout.LayoutAnchorable  -   5        Solution��/ 
 )Xceed.Wpf.AvalonDock.Layout.LayoutContent'   Title$' 目录��5�   L   (   	ContentId$( Solution��6W   )   Content.) 5�   &   � -   5        * �Name$* Project_grid��5�   +   5�   '   5�   #     5�   "   $' 属性��6:   $( Property��6E   .) 5�   &   � -   5        $* shuxing��5�   +   5�   #   5�      5�      9 
 3Xceed.Wpf.AvalonDock.Layout.LayoutDocumentPaneGroup  5�      -   5        DocumentGroup��� 
 �Xceed.Wpf.AvalonDock.Layout.LayoutGroup`1[[Xceed.Wpf.AvalonDock.Layout.ILayoutDocumentPane, Xceed.Wpf.AvalonDock, Version=3.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]]+   Children.+ 5�      4 
 .Xceed.Wpf.AvalonDock.Layout.LayoutDocumentPane  -   5        DocumentPane��5�   8   5�        5�      -   5        RightAnchorableGroup��,   Orientation$, Vertical=�5�   Y   $	$ 200 �6p   .% 5�        -   5        	RightPane��5�      5�      5�      5�      5�      5�      5�      5�      