   M S B A M L   `   `   `  ���� c  _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity   MatLabCompiler   MatLabCompiler.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     ` \ICSharpCode.AvalonEdit, Version=4.3.1.9429, Culture=neutral, PublicKeyToken=9cc39be672370310?
avalonedit.http://icsharpcode.net/sharpdevelop/avalonedit    � Title$  MatLab Compiler��$	��550��$
��1025�� � Icon$" /Resources/compiler_icon.ico��.���  � RowDefinitions �� $
��Auto ��� $��* �.+�o� $	��Top=�.@�m� -   $��FileMenuItem��$��_File��.@�m� -   $��openFileMenuItem��$��
_Open File��m� -   $��saveFileMenuItem��$��
_Save File��m� -   $��saveAnalysisMenuItem��$��_Save Analysis��m� -   $��closeFileMenuItem��$��_Close File��� $��1�� � ColumnDefinitions �� $��7* ��� $��3* � �� $��7* ��� $
��Auto �.+��� $��1q�
�������$��5,5,5,0q�$��0��$��1��._�'  !ICSharpCode.AvalonEdit.TextEditor  -   $��
TextEditor��   ShowLineNumbers . $
��Auto=�$
��Auto=�   Text. .   (System.Windows.Interactivity.Interaction   Triggers /   )System.Windows.Interactivity.EventTrigger     	EventName$ TextChanged��.   (System.Windows.Interactivity.TriggerBase   Actions. 6   0System.Windows.Interactivity.InvokeCommandAction  	   Command	 �� *TextChangedCommand+
   CommandParameter
 ��  �� ElementName$ 
TextEditor��  $ Loaded��.   	 �� *OnLoadCommand+
 �� $ 
TextEditor��. �� $��1��$��2��$��25��
�������
�������$��1q�$��5,0,5,5q����� *Parser.SyntaticResults+ �� UpdateSourceTrigger$ PropertyChanged=����� .R��� ._��� ���� ����  �� 
TargetType" ��.���  �� Property � �� �� Value
 �  �� �� Triggers ��  �� Binding �� *Text+ �� RelativeSource �� *Self+ �� Value Success.a���  � ��
 � � ��� $��
0, 5, 5, 0q�$��1q�
�������$��1��$��0��._��  �� $��3* ��� $��3* ��� $��3* �.+�� $��1��$��0�� �� $
��Auto ��� $��* �.+��� $��0��$��Tokens��
���   �$��Center=��� 
�������$��
0, 1, 0, 1q�$��1������ *LexicAnaliyzer.Tokens+$ PropertyChanged=�� $��1��$��1�� �� $
��Auto ��� $��* �.+��� $��0��$��Lexic Errors��
���  ��$��Center=��� 
�������$��
0, 1, 0, 1q�$��1������ *LexicAnaliyzer.Errors+$ PropertyChanged=�� $��1��$��2�� �� $
��Auto ��� $��* �.+��� $��0��$��GeneratedCod��
��� � �$��Center=���  �� 
IsReadOnly . $
��Auto=�$��1������ *GeneratedCode+ �� Mode$ TwoWay=�