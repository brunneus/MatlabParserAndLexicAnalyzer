   M S B A M L   `   `   `  ����c  _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  5   	    MatLabCompiler   MatLabCompiler.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5   	   ` \ICSharpCode.AvalonEdit, Version=4.3.1.9429, Culture=neutral, PublicKeyToken=9cc39be672370310?
avalonedit.http://icsharpcode.net/sharpdevelop/avalonedit  5   	     � Title$  MatLab Compiler��5   	   $	��550��6!   $
��1025��6.    � Icon$" /Resources/compiler_icon.ico��6;   .��5      �  � RowDefinitions 5	   
   �� 5
      $
��Auto �6   �� 5      $��* �6   .+�5   
   o� $	��Top=�6   .@�5      m� -   5        $��FileMenuItem��5      $��_File��6+   .@�5      m� -   5        $��openFileMenuItem��5      $��
_Open File��5      5      m� 5      -   5        $��saveFileMenuItem��5      $��
_Save File��5      5      m� 5      -   5        $��saveAnalysisMenuItem��5      $��_Save Analysis��5      5      m� 5      -   5        $��closeFileMenuItem��5      $��_Close File��5      5      5       5!      � 5#   
   $��1��6    � ColumnDefinitions 5$      �� 5%      $��7* �6#   �� 5&      $��3* �6#    5(      �� 5)      $��7* �6    �� 5*      $
��Auto �6    .+�5,      �� $��1q�5-      
�������$��5,5,5,0q�5/      $��0��50      $��1��51      ._�52      '  !ICSharpCode.AvalonEdit.TextEditor  -   5        $��
TextEditor��53         ShowLineNumbers . $
��Auto=�55      $
��Auto=�56         Text. 6A   .   (System.Windows.Interactivity.Interaction   Triggers 57      /   )System.Windows.Interactivity.EventTrigger  58         	EventName$ TextChanged��6)   .   (System.Windows.Interactivity.TriggerBase   Actions. 59      6   0System.Windows.Interactivity.InvokeCommandAction  	   Command	 5:   3   �� *TextChangedCommand+
   CommandParameter
 5;   3   ��  �� ElementName$ 
TextEditor��5<        5=      $ Loaded��6)   . 5>        	 5?   3   �� *OnLoadCommand+
 5@   3   �� $ 
TextEditor��5A      . 5B   .   5C      5D      �� 5E      $��1��5F      $��2��5G      $��25��5H      
�������
�������$��1q�5K      $��5,0,5,5q�5L      ��5M      �� *Parser.SyntaticResults+ �� UpdateSourceTrigger$ PropertyChanged=���5N      �� 5O      .R�5P      �� ._�5Q      �� ��6(   �� ��5R   "   �� 5S   &    �� 
TargetType" ��6,   .�5T   *   ��  �� Property � �� �� Value
 �  ��6G    �� Triggers 5U   *   �� 5V   .    �� Binding 6:   �� *Text+ �� RelativeSource �� *Self+ �� Value Success6y   .a�5W   2   ��  � ��
 � � �6O   5X   /   5Z   '   5\      5]      5^      5`      �� 5a      $��
0, 5, 5, 0q�5b      $��1q�5c      
�������$��1��5e      $��0��5f      ._�5g      �  5h      �� 5i      $��3* �6(   �� 5j      $��3* �6(   �� 5k      $��3* �6(   .+�5m      � $��1��5n      $��0��5o       5p      �� 5q      $
��Auto �6,   �� 5r      $��* �6,   .+�5t      �� $��0��6$   $��Tokens��61   
���   �$��Center=�6R   �� 5u      
�������$��
0, 1, 0, 1q�5w   !   $��1��5x   !   ��5y   !   �� *LexicAnaliyzer.Tokens+$ PropertyChanged=�5z      � 5{      $��1��5|      $��1��5}       5~      �� 5      $
��Auto �6,   �� 5�      $��* �6,   .+�5�      �� $��0��6$   $��Lexic Errors��61   
���  ��$��Center=�6V   �� 5�      
�������$��
0, 1, 0, 1q�5�   !   $��1��5�   !   ��5�   !   �� *LexicAnaliyzer.Errors+$ PropertyChanged=�5�      � 5�      $��1��5�      $��2��5�       5�      �� 5�      $
��Auto �6,   �� 5�      $��* �6,   .+�5�      �� $��0��6$   $��GeneratedCod��61   
��� � �$��Center=�6X   �� 5�   !    �� 
IsReadOnly . $
��Auto=�6;   $��1��6k   ��6x   �� *GeneratedCode+ �� Mode$ TwoWay=�5�      5�      5�      5�      5�      5�      