   M S B A M L   `   `   `  ����N  JProgrammingThirdSem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullq=clr-namespace:ProgrammingThirdSem.NumericalMethods.ViewModels/ProgrammingThirdSem.NumericalMethods.ViewModels  5   	   6    0ProgrammingThirdSem.NumericalMethods.Views.Graph   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   R NOxyPlot.Wpf, Version=2.2.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0Y UOxyPlot.Wpf.Shared, Version=2.2.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0"wpfhttp://oxyplot.org/wpf   5   	   P
viewModels=clr-namespace:ProgrammingThirdSem.NumericalMethods.ViewModels   5   	     � Title$  Graph��5
   	   $	��350��6   $	��700��6$   .��5      �  � ColumnDefinitions 5      �� 5      $	��350 �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5      �� $��0��6   $��Center=�6!   $��	25 0 25 0q�6<   .+�5      �� $��	0 0 0 5pxq�6   $
��14px(�6%   $��
Montserrat)�65   $��Medium"�6M   $
��Wrap=�6a   '%Отображение графика6�   � 5      $��0 0 0 5q�6    5   
   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0 0 10 0q�6%   .+�5      �� $��	0 0 0 5pxq�6   $
��12px(�6)   $��
Montserrat)�69   $��Medium"�6Q   $
��Wrap=�6e   Длина оси Ox6�   �� 5      $��0 2pxq�6   $
��12px(�6$   $��
Montserrat)�64   $��Medium"�6L   ��6`   �� *
WidthXAxis+ �� UpdateSourceTrigger$ PropertyChanged=�6�   5      �� 5   
   $��1��6   $��0 0 0 0q�6%   .+�5      �� $��	0 0 0 5pxq�6   $
��12px(�6)   $��
Montserrat)�69   $��Medium"�6Q   $
��Wrap=�6e   Длина оси Oy6�   �� 5      $��0 2pxq�6   $
��12px(�6$   $��
Montserrat)�64   $��Medium"�6L   ��6`   �� *
WidthYAxis+$ PropertyChanged=�6�   5      5    	   � 5!      $��0 0 0 10q�6    5"   
   �� 5#      �� 5$      .+�5&   
   �� $��0��6   $��0 0 10 0q�6%   .+�5'      �� $��	0 0 0 5pxq�6   $
��12px(�6)   $��
Montserrat)�69   $��Medium"�6Q   $
��Wrap=�6e   Плавность6�   �� 5(      $
��12px(�6   $��
Montserrat)�6%   $��Medium"�6=    �� Text 6Q   �� *GraphicPointsDelta+$ PropertyChanged=�.@�5)      �� 26   �� 5*      16   �� 5+      0.56   �� 5,      0.16   �� 5-      0.016   5.      5/      �� 50   
   $��1��6   $��0 0 0 0q�6%   .+�51      �� $��	0 0 0 5pxq�6   $
��12px(�6)   $��
Montserrat)�69   $��Medium"�6Q   $
��Wrap=�6e   Толщина6�   �� 52      $
��12px(�6   $��
Montserrat)�6%   $��Medium"�6=    6Q   �� *GraphicThickness+$ PropertyChanged=�.@�53      �� 46   �� 54      36   �� 55      26   �� 56      16   57      58      59   	   �� 5:      $
��30px��6   $
��14px(�6   $��
Montserrat)�6-   $��Medium"�6E   ���#24A1DE
�������$��0q�6�   $��0 0 0 20q�6�   ��6�   �� *ConstructGraphCommand+.��6�   0.Построить график функции6�   �� 5;      $
��14px(�6   $��
Montserrat)�6"   $��SemiBold"�6:   $
��Wrap=�6P   ��6d   �� *
ResultText+6�   �� 5<      ��6   �� *IterationsInfo+5=      �� 5>      $��1��6    �� ShowsPreview .  $��3��68   $��Center=�6B   $��Stretch=�6_   �� 5?      $��2��6   $��Stretch=�6!   $��Stretch=�6=   $��
0 50 20 50q�6[   .+�5@        OxyPlot.Wpf.PlotView  $	��400��61     OxyPlot.Wpf.PlotViewBase   Model 6   �� *	PlotModel+61   5A      5B      5C      