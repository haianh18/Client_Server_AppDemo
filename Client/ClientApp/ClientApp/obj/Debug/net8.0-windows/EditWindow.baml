   M S B A M L   `   `   `  ����D  @ClientApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:ClientApp	ClientApp  5   	       ClientApp.EditWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:ClientApp     5   	     � WindowStartupLocation$  CenterScreen=�5   	    � Title$
 Edit��5	   	   $	��300��5
   	   $	��600��5   	   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��Name:��5      $��Center=�5      �� 5   
   -   5         ��Name$ NameTextBox��5      $��0��5      $��120,4,110,6q�5      $	��250��5      $��30��5      �� 5   
   $��1��6   $��Price:��5       $��Center=�5!      �� 5"   
   -   5        $ PriceTextBox��5"      $��1��5#      $��120,4,110,6q�5$      $	��250��5%      $��30��5&      �� 5)   
   $��2��6   $��	Category:��5*      $	��Top=�5+      $��0,5,0,0q�5,      �� 5-   
   -   5         ��Name$ CategoryComboBox��5-      $��2��5.      $��120,4,110,6q�5/       �� 
IsEditable$ False��50       �� IsTextSearchEnabled$
 True��51       �� DisplayMemberPath$
 Name��53      $��30��54      $	��250��55      56      �� 59   
   $��3��6    �� Orientation$ 
Horizontal=�5:      $��Center=�5;      $��
0,41,0,-31q�5<      .+�5=      �� -   5        $��
SaveButton��5>      $
��Save��5=      $	��100��5?      $��30��5@      $��10q�5A      5B      �� 5C      -   5        $��CancelButton��5D      $��Cancel��5C      $	��100��5E      $��30��5F      $��10q�5G      5H      5I      5J      5K      