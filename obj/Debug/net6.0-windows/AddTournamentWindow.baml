   M S B A M L   `   `   `  ����  TFG$    TFG.Clases.AddTournamentWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Añadir Torneo��5   	   $	��500��6   $	��600��6,   .��5      � $��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��Nombre:��6   $��0��6"   $��0��6/   $��	0,10,10,5q�6?   $��Center=�6R   $
��Bold"�6m   �� 5   
   -   5         ��Name$ NameTextBox��5      $��0��6'   $��1��64   $��0,10,0,5q�6D   $��Center=�6V   �� 5   
   $��Fecha de inicio:��6   $��1��6+   $��0��68   $��	0,10,10,5q�6H   $��Center=�6[   $
��Bold"�6v   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ StartDatePicker��5      $��1��6.   $��1��6;   $��0,10,0,5q�6K   $��Center=�6]   �� 5   
   $��Fecha de fin:��6   $��2��6(   $��0��65   $��	0,10,10,5q�6E   $��Center=�6X   $
��Bold"�6s     5   
   -   5        $ EndDatePicker��5      $��2��6,   $��1��69   $��0,10,0,5q�6I   $��Center=�6[   �� 5   
   $��Participantes:��6   $��3��6)   $��0��66   $��	0,10,10,5q�6F   $��Center=�6Y   $
��Bold"�6t   �� 5   
   -   5         ��Name$ ParticipantsListBox��5      $��3��6/   $��1��6<   $��0,10,0,5q�6L   $��Center=�6^    �� SelectionMode$ Multiple=�6y   ��5      �� *
AllTeams+��5       �� 5!      .R�5"      �� ��6    �� *Name+5#      5%      �� 5'   
   -   5        $��Guardar��5'      $��6��6#   $��0,20,0,0q�60   $��Center=�6B   $
��20,5q�6_   $��14(�6n   $
��Bold"�6|   ���#2196F3
����������#2196F3$��1q�5(   O   $��2��6|   5*      5+      