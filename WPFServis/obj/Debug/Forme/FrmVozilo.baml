   M S B A M L   `   `   `  ����D  @WPFServis, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WPFServis.FormeWPFServis.Forme  5   	       WPFServis.Forme.FrmVozilo   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:WPFServis.Forme     5   	     � Title$  Dodaj/Izmeni Vozilo��5   	   $	��450��6%   $	��300��62   .��5	      � .+�5
   
   �� $��Broj sasije:��6   $
��Left=�6'   $��30��6B   $��	31,52,0,0q�6N   $	��Top=�6a   $	��110��6y   �� 5   
   $��	Kubikaza:��6   $
��Left=�6$   $��30��6?   $��	31,87,0,0q�6K   $	��Top=�6^   $	��110��6v   �� 5   
   $��Konjska snaga:��6   $
��Left=�6)   $��30��6D   $��
31,122,0,0q�6P   $	��Top=�6d   $	��110��6|   �� 5   
   $��Model:��6   $
��Left=�6!   $��30��6<   $��
31,157,0,0q�6H   $	��Top=�6\   $	��110��6t   �� 5   
   $��Marka:��6   $
��Left=�6!   $��30��6<   $��
31,192,0,0q�6H   $	��Top=�6\   $	��110��6t   �� 5   
   $��Tip vozila:��6   $
��Left=�6&   $��30��6A   $��
31,277,0,0q�6M   $	��Top=�6a   $	��110��6y   �� 5   
   $��	Vlasnika:��6   $
��Left=�6$   $��30��6?   $��
31,232,0,0q�6K   $	��Top=�6_   $	��110��6w   �� 5   
   -   5         ��Name$ 
btnSacuvaj��5      $��Sačuvaj��6%   $
��Left=�67   $��
31,340,0,0q�6R   $	��Top=�6f   $��75��6~   6�   �� 5   
   -   5        $ 	btnOtkazi��5      $��Otkaži��6$   $
��Left=�65   $��184,340,0,0q�6P   $	��Top=�6e   $��75��6}   6�   �� 5   
   -   5         ��Name$ txtBrSasije��5      $
��Left=�6'   $��25��6B   $��
150,54,0,0q�6N    �� TextWrapping$
 Wrap=�6b   $�� ��6v   $	��Top=�6~   $	��131��6�   �� 5   
   -   5        $ txtKubikaza��5      $
��Left=�6'   $��25��6B   $��
150,87,0,0q�6N   $
 Wrap=�6b   $�� ��6v   $	��Top=�6~   $	��131��6�   �� 5   
   -   5        $ txtKonjskaSnaga��5      $
��Left=�6+   $��25��6F   $��150,122,0,0q�6R   $
 Wrap=�6g   $�� ��6{   $	��Top=�6�   $	��131��6�   �� 5   
   -   5         ��Name$ cbModel��5      $
��Left=�6$   $��25��6?   $��150,159,0,0q�6K   $	��Top=�6`   $	��131��6x    �� SelectedValuePath$ modelID��6�   �� 5   
   -   5        $ cbMarka��5      $
��Left=�6$   $��25��6?   $��150,194,0,0q�6K   $	��Top=�6`   $	��131��6x   $ markaID��6�   �� 5   
   -   5        $ cbTipVozila��5      $
��Left=�6(   $��25��6C   $��146,282,0,0q�6O   $	��Top=�6d   $	��131��6|   $ tipVozilaID��6�   �� 5   
   -	   5        $ 	cbVlasnik��5      $
��Left=�6&   $��25��6A   $��150,237,0,0q�6M   $	��Top=�6b   $	��131��6z   $ 	vlasnikID��6�   5      5      