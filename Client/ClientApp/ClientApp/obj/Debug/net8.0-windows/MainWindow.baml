   M S B A M L   `   `   `  ����D  @ClientApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:ClientApp	ClientApp  5   	       ClientApp.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:ClientApp     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)    � WindowStartupLocation$ CenterScreen=�5	   	   .��5
      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $��* �6   .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�5      .+�5      �� $��	Category:��6   $��Center=�5      $��0,0,0,0q�5      �� 5      -   5         ��Name$ 
cbCategory��5      $
��Left=�5      $	��Top=�5      $	��300��5      $��	0,10,0,10q�5      $��30��5       5"      5#      �� 5'   
   -   5         ��Name$ ProductListView��5'      $��1��5(      $��0,0,0,10q�5)       �� SelectionMode$ Single=�5*       �� View 5,      �� 5-      .P�5.      �� $
��Name��6%   	 �� Width$		 400��50   %   
 �� DisplayMemberBinding
 5/   %   �� *Name+50   %   �� 51      $��Price��6%   $		 400��52   %    �� CellTemplate 53      �� 54      .R�55   "   �� ��6,   �� *Price+ �� StringFormat$ C��56      58      59      5;      �� 5>   	   $��2��6   $ 
Horizontal=�5?      $��Right=�5@      $��Bottom=�5A      $��0,0,0,10q�5B      .+�5C      �� -   5        $��Refresh��5C      $	��100��5D      $��5q�5E      5F      �� 5G      -   5        $
��Edit��5G      $	��100��5H      $��5q�5I      5J      �� 5K      -   5        $��Delete��5K      $	��100��5L      $��5q�5M      5N      �� 5O      -   5        $	��Add��5O      $	��100��5P      $��5q�5Q      5R      5S      5T      5U      