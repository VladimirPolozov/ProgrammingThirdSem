   M S B A M L   `   `   `  ����  ProgrammingThirdSem6    0ProgrammingThirdSem.NumericalMethods.Views.Graph   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   R NOxyPlot.Wpf, Version=2.2.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0Y UOxyPlot.Wpf.Shared, Version=2.2.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0"wpfhttp://oxyplot.org/wpf   5   	     � Title$  Graph��5   	   $	��600��6   $	��900��6$   .��5      �  � ColumnDefinitions 5      �� 5      $	��300 �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5      �� $��0��6   $��Center=�6!   $��	25 0 25 0q�6<   .+�5      � $��	0 0 0  25q�6    5   
   �� 5      �� 5      �� 5      .+�5   
   �� $��0��6   $��2��6$   $
��12px(�68   $��
Montserrat)�6H   $��SemiBold"�6`   $��0 0 10 0q�6�   ��6v   �� *IterationsInfo+ �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5   
   $��2��6    �� Orientation$ 
Horizontal=�6%   $��Right=�6>   .+�5      �� $
��16px(�6   $��
Montserrat)�6#   $
��Bold"�6;   $��<��6M   ���#24A1DE
�������$��0q�6�   $��10px 4pxq�6�   $��0 0 4 0q�6�   ��6�   �� *ShowPreviousIterationCommand+�� 5      $
��16px(�6   $��
Montserrat)�6#   $
��Bold"�6;   $��>��6M   ���#24A1DE
�������$��0q�6�   $��10px 4pxq�6�   ��6�   �� *ShowNextIterationCommand+5      5   	   �� 5      $��	0 0 0 5pxq�6   $
��14px(�6%   $��
Montserrat)�65   $��Medium"�6M   $
��Wrap=�6a   '%Отображение графика6�   � 5       $��0 0 0 5q�6    5!   
   �� 5"      $��* �6   �� 5#      $��* �6   .+�5%   
   �� $��0��6   $��0 0 10 0q�6%   .+�5&      �� $��	0 0 0 5pxq�6   $
��12px(�6)   $��
Montserrat)�69   $��Medium"�6Q   $
��Wrap=�6e   Длина оси Ox6�   �� 5'      $��0 2pxq�6   $
��12px(�6$   $��
Montserrat)�64   $��Medium"�6L   ��6`   �� *
WidthXAxis+$ PropertyChanged=�6�   5(      �� 5)   
   $��1��6   $��0 0 0 0q�6%   .+�5*      �� $��	0 0 0 5pxq�6   $
��12px(�6)   $��
Montserrat)�69   $��Medium"�6Q   $
��Wrap=�6e   Длина оси Oy6�   �� 5+      $��0 2pxq�6   $
��12px(�6$   $��
Montserrat)�64   $��Medium"�6L   ��6`   �� *
WidthYAxis+$ PropertyChanged=�6�   5,      5-   	   � 5.      $��0 0 0 10q�6    5/   
   �� 50      �� 51      .+�53   
   �� $��0��6   $��0 0 10 0q�6%   .+�54      �� $��	0 0 0 5pxq�6   $
��12px(�6)   $��
Montserrat)�69   $��Medium"�6Q   $
��Wrap=�6e   Плавность6�   �� 55      $
��12px(�6   $��
Montserrat)�6%   $��Medium"�6=    �� Text 6Q   �� *GraphicPointsDelta+$ PropertyChanged=�.@�56      �� 26   �� 57      16   �� 58      0.56   �� 59      0.16   �� 5:      0.016   5;      5<      �� 5=   
   $��1��6   $��0 0 0 0q�6%   .+�5>      �� $��	0 0 0 5pxq�6   $
��12px(�6)   $��
Montserrat)�69   $��Medium"�6Q   $
��Wrap=�6e   Толщина6�   �� 5?      $
��12px(�6   $��
Montserrat)�6%   $��Medium"�6=    6Q   �� *GraphicThickness+$ PropertyChanged=�.@�5@      �� 46   �� 5A      36   �� 5B      26   �� 5C      16   5D      5E      5F   	   �� 5G      $
��30px��6   $
��12px(�6   $��
Montserrat)�6-   $��Medium"�6E   ���#24A1DE
�������$��0q�6�   $#��Обновить график��6�   ��6�   �� *ConstructGraphCommand+6�   5H      �� 5I      $��1��6    �� ShowsPreview .  $��3��68   $��Center=�6B   $��Stretch=�6_   �� 5J      $��2��6   $��Stretch=�6!   $��Stretch=�6=   $��
0 50 20 50q�6[   .+�5K        OxyPlot.Wpf.PlotView  $	��400��61     OxyPlot.Wpf.PlotViewBase   Model 6   �� *	PlotModel+61   5L      5M      5N      