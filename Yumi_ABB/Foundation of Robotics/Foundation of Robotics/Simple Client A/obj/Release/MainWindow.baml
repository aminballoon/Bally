   M S B A M L   `   `   `  ���� J  FSimple Client A, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Simple_Client_ASimple_Client_A       Simple_Client_A.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     /localclr-namespace:Simple_Client_A     -     � Title$  Simple Client A��$	��400��$	��600��.��]� .+��� $��
Connection��$	��Top=�.��� .+��� .��NAME=�� -    ��Name$ in_name��$	��100��.��A�� -    ��Name$ 
bt_connect��.��	Connect�� -   $ bt_disconnect��.��
Disconnect�� -    ��Name$ label��$��Yumiko���� -   $ 
btCon_LArm��$��
ConLeftArm��$��80���� -   $ 
btCon_RArm��$��ConRightArm��$��80���� -    ��Name$ cbSelect��$��90�� �� SelectedIndex$ 0��.@��� $��Server��$
��Left=�$��92���� $��YumiR��$
��Left=�$��92���� $��YumiL��$
��Left=�$��92���� $��Convert Data��$��Bottom=�.���� .+��� .+��� -	   $ 
disp_color��.��COLOR�� -
   $ disp_position��.��
POSITION�� $
��Data��.��]� .+��� $
��Send��$��Bottom=�.��]� .+��� -   $ bt_send��$��Right=�$��21��$	��Top=�.��SEND�� -   $ in_send��$	��300���� $��Receive��.���� -   $ disp_receive�� �� 
IsReadOnly . " �� VerticalScrollBarVisibility$
 Auto=�$ �� HorizontalScrollBarVisibility$ Disabled=�	 �� TextWrapping$
	 Wrap=�