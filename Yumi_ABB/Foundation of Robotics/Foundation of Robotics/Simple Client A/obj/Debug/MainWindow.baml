   M S B A M L   `   `   `  ����J  FSimple Client A, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Simple_Client_ASimple_Client_A  5   	        Simple_Client_A.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:Simple_Client_A     5   	   -   5          � Title$  Simple Client A��5   	   $	��400��6!   $	��761��6.   .��5	      ]� .+�5   
   �� $��
Connection��6   $	��Top=�6'   .��5      � .+�5      �� .��6   NAME=6   �� 5      -   5         ��Name$ in_name��5      $	��100��6+   .��6:   YUMIKO6B   �� 5      -   5         ��Name$ 
bt_connect��5      .��6C   	Connect6L   �� 5      -   5        $ bt_disconnect��5      .��6F   
Disconnect6R   �� 5      -   5         ��Name$ label��5      $��Yumiko��6'   �� 5      -   5        $ 
btCon_LArm��5      $��
ConLeftArm��6-   $��80��6B   6M   �� 5      -   5        $ 
btCon_RArm��5      $��ConRightArm��6-   $��80��6C   6N   �� 5      -   5         ��Name$ cbSelect��5      $��90��6-    �� SelectedIndex$ 0��68   .@�5      �� $��Server��6#   $
��Left=�64   $��92��6O   �� 5      $��YumiR��6#   $
��Left=�63   $��92��6N   �� 5      $��YumiL��6#   $
��Left=�63   $��92��6N   5      5      5      �� 5   
   $��Convert Data��6   $��Bottom=�6)   .��5      �� .+�5      �� .+�5      �� -	   5        $ 
disp_color��5      .��60   COLOR67   �� 5       -
   5        $ disp_position��5       .��63   
POSITION6=   5!      5"      5#      �� 5%   
   $
��Data��6   .��5&      ]� .+�5'      �� $
��Send��6   $��Bottom=�6)   .��5(      ]� .+�5)      �� -   5        $ bt_send��5)   !   $��Right=�62   $��21��6_   $	��Top=�6k   .��6�   SEND6�   �� 5*      -   5        $ in_send��5*   "   $	��300��63   6^   5+      5,      �� 5.      $��Receive��6   .��5/      �� -   5        $ disp_receive��5/       �� 
IsReadOnly . " �� VerticalScrollBarVisibility$
 Auto=�6F   $ �� HorizontalScrollBarVisibility$ Disabled=�6i   	 �� TextWrapping$
	 Wrap=�6�   6�   50      51      52      54      55      