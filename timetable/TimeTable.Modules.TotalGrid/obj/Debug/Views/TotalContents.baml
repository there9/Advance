   M S B A M L   `   `   `  ����V  RTimeTable.Modules.TotalGrid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:TimeTable.Modules.TotalGrid.Views!TimeTable.Modules.TotalGrid.Views  5      5    /TimeTable.Modules.TotalGrid.Views.TotalContents   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Alocal/clr-namespace:TimeTable.Modules.TotalGrid.Views     5      P LPrism.Wpf, Version=6.1.0.0, Culture=neutral, PublicKeyToken=91a96d2a154366d8$prismhttp://prismlibrary.com/  5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  . .��5      �  � RowDefinitions 5   
   �� 5      $	��27* �6   �� 5      $
��193* �6   .+�5   
   �� $
��Left=�6.   $
��auto��6I   $��6,3,0,0q�6W    �� TextWrapping$
 Wrap=�6h   $	��Top=�6|   $��86��6�   $��10(�6�   4  .System.Windows.Automation.AutomationProperties   HelpText$ 	검색...��6�   ��6   �� *
SearchText+.��6�   .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5         	EventName$ TextChanged��6!   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      -  'Prism.Interactivity.InvokeCommandAction     Command 60   �� *SearchTextChangeCommnad+5      .��5   &   5      �� 5   
   $��검색��63   $
��Left=�6@   $
��auto��6[   $��98,3,0,0q�6i   $	��Top=�6{   $��47��6�   $��10(�6�   ��6   �� *SerchCommand+6�   �� 5   
   $
��auto��6:   $��159,3,181,0q�6H   $	��Top=�6]   ��6   �� *DepartmentType+ 5        5      $ SelectionChanged��6!   . 5           TriggerParameterPath$ 
AddedItems��6]    60   �� *DepartmentConvertCommand+6]   5      5      �� 5    
   
���  ��$��사용안내��6E   $��Right=�6T   $��15��6p   $��	0,3,119,0q�6|   $	��Top=�6�   $��54��6�   $��10(�6�   $
��Bold"�6�   ��6   �� *MessageCommand+6�   &   System.Windows.Controls.DataGrid  5"   
   	   AutoGenerateColumns	 .  $��1��6Q   $
   HorizontalScrollBarVisibility$

 Auto=�6^   "   VerticalScrollBarVisibility$
 Auto=�6�   $��7��6�   $��0,0,0,0q�6�   $ �� IsSynchronizedWithCurrentItem$ FalseY�6�      AreRowDetailsFrozen .     SelectionUnit$ FullRow=�6  ���	#FFECBEBE��6   �� *	DataExcel+   Columns 5#      0  *System.Windows.Controls.DataGridTextColumn  5$      ,	  &System.Windows.Controls.DataGridColumn 	  Header$ 과목코드��6%    	  Width5
  /System.Windows.Controls.DataGridLengthConverter$ 2*
 63    	  
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6>   �� *Code+6W     5%      $ 	과목명��6%   $ 4*
 62    .  6=   �� *SubjectName+6]     5&      $ 분반��6%   $ 1*
 61    .  6<   �� *ClassNumber+6\     5'      $ 교수��6%   $ 1*
 61    .  6<   �� *ProfessorName+6^     5(      $ 대상��6%   $ 1*
 61    .  6=   �� *Target+6X     5)      $ 학점��6%   $ 1*
 61    .  6<   �� *Grade+6V     5*      $ 설계��6%   $ 1*
 61    .  6>   �� *Design+6Y     5+      $ 비고��6%   $ 1*
 61    .  6<   �� *
EtcClass+6Y     5,      $ 정원��6%   $ 1*
 61    .  6=   �� *MaxStudents+6]     5-      $ 개설학부��6%   $ 1*
 63    .  6>   �� *
Department+6]    5/        50      $ MouseDoubleClick��6!   . 51         60   �� *DoubleClickCommand+52        53      $ SelectionChanged��6!   . 54        $ 
AddedItems��6V    60   �� *SelectItemCommand+6V   55      57      58      59      