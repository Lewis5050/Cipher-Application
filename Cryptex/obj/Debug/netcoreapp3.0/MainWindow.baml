   M S B A M L   `   `   `  ����B  >Cryptex, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:CryptexCryptex  5   	       Cryptex.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Cryptex     5   	     � Title$  
MainWindow��5   	   $	��400��6   $	��649��6)    � WindowStyle$
 None=�65    � AllowsTransparency$
 True��6H    � 	Resources 5
         CipherButton �� 
TargetType �� Property �� Value �� 
TargetType �� Triggers	 P� Property
 P� Value��%�  &	        �� 5   
   " ��6%   .�5      ��  �@�OverridesDefaultStyle$
 True��66   �� 5       � ��$ 0q�6'   �� 5       � ��$ Global User Interface)�6+   �� 5       � ��$ 11(�6)   �� 5       � �� �	#FF61892F6+   �� 5       � �� �	#FF3E42476+   �� 5       � �� 5      �� 5      " ��6&   .R�5      � .+�5      �� 
������ $��0q�6>   ._�5   "   �� $��Center=�63   $��Center=�6N   6m   5      5       5      P� 5      	 �@J�IsMouseOver$

 True��6=   . �5   "   ��  � �� �#86C2326?   �� 5   "    � �� �#86C2326?   5       5"      5$      5%      5&      .��5)      �� ���	#FF3E4247.+�5,   
      Cryptex.TitleBar  $��Center=�6   $��30��66   $	��Top=�6B   $	��650��6Z   � 5.   
    � ColumnDefinitions 5/      �� 50      $	��150 �6#   �� 51      $��* �6#   .+�55      �� $��	0 0 1px 0q�6   ���	#FF505355$��340px��6J   $��Center=�6Y   $��0 14 0 0q�6t   $��0��6�   ._�56      ��  �� Orientation$ Vertical=�6   $	��335��64   .+�59      �� $��Ciphers��6   $��Center=�6.   ���#838789$��Global User Interface)�6`   �� 5:      $	��100��6   $��	0 1px 0 0q�6)   ���	#FF505355$��0 0 0 10q�6]   6q   �� 5=      -   5         ��Name$ HomeBtn��5=      #��[  6.   $
��30px��6T   $
��Home��6b   6q   �� 5>      -   5        $ BeaufortBtn��5>      #��[  62   $
��30px��6X   $��Beaufort Cipher��6f   6�   �� 5?      -   5        $ BifidBtn��5?      #��[  6/   $
��30px��6U   $��Bifid Cipher��6c   6z   �� 5@      -   5        $ HomophonicBtn��5@      #��[  64   $
��30px��6Z   $��Homophonic Substitution��6h   6�   �� 5A      -   5        $ PortaBtn��5A      #��[  6/   $
��30px��6U   $��Porta Cipher��6c   6z   �� 5B      -   5        $ 
StaddleBtn��5B      #��[  61   $
��30px��6W   $��Straddle Checkerboard��6e   6�   5D      5E       � 5H      -   5          �Name$ displayWindowFrame��5H        � Source$ 	Home.xaml?�60   $��0q�6D   $��1��6O   $	��500��6_   $	��354��6k     � NavigationUIVisibility$ Hidden=�6x   6�   5J      5K      5L      