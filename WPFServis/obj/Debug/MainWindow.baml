   M S B A M L   `   `   `  ����D  @WPFServis, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:WPFServis	WPFServis  5   	       WPFServis.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:WPFServis     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      � .+�5
   
   &   System.Windows.Controls.DataGrid  -   5        $��dataGridCentralni��5
      $��Center=�6,   $��Center=�6I   $
��auto��6d   $
��auto��6r      
IsReadOnly .    AlternatingRowBackground
 ��� �   CanUserAddRows .     SelectionUnit$ FullRow=�5   D      SelectionMode$ Single=�6\   �� 5   
   $��Center=�6   $��30��62   $	��Top=�6?   $
��auto��6W    �� Orientation$ 
Horizontal=�6d   .+�5      �� -   5         ��Name$ btnZaposleni��5      $��	Zaposleni��6+   $
��Left=�6?   $��30��6Z   $	��Top=�6f   $��80��6~   6�   �� 5      -   5        $ btnTipVozila��5      $��
Tip Vozila��6+   $
��Left=�6@   $��30��6[   $	��Top=�6g   $��80��6   6�   �� 5      -   5        $ btnModel��5      $��Model��6'   $
��Left=�67   $��30��6R   $	��Top=�6^   $��80��6v   6�   �� 5      -   5        $ btnMarka��5      $��Marka��6'   $
��Left=�67   $��30��6R   $	��Top=�6^   $��80��6v   6�   �� 5      -   5        $ 	btnVozilo��5      $��Vozilo��6(   $
��Left=�69   $��30��6T   $	��Top=�6`   $��80��6x   6�   �� 5      -   5        $ 
btnVlasnik��5      $��Vlasnik��6)   $
��Left=�6;   $��30��6V   $	��Top=�6b   $��80��6z   6�   �� 5      -   5        $ 	btnDelovi��5      $��Delovi��6(   $
��Left=�69   $��30��6T   $	��Top=�6`   $��80��6x   6�   �� 5      -	   5        $ 	btnServis��5      $��Servis��6(   $
��Left=�69   $��30��6T   $	��Top=�6`   $��80��6x   6�   5      �� 5   
   $��Center=�6   $��30��62   $��Bottom=�6?   $	��510��6Z   $ 
Horizontal=�6f   .+�5      �� -
   5        $ btnDodaj��5      $��Dodaj��6'   $	��170��67   6C   �� 5      -   5        $ 	btnIzmeni��5      $��Izmeni��6(   $	��170��69   6E   �� 5      -   5        $ 	btnObrisi��5      $��Obriši��6(   $	��170��69   6E   5      ��5   
   �� 5       �� ImageSource$% C:\Users\User\Desktop\slika.png��6   	 �� Opacity$		 0.9Q�6H   5      5      