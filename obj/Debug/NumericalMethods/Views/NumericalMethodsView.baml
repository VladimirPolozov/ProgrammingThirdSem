   M S B A M L   `   `   `  ����N  JProgrammingThirdSem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullq=clr-namespace:ProgrammingThirdSem.NumericalMethods.ViewModels/ProgrammingThirdSem.NumericalMethods.ViewModels  5      E    ?ProgrammingThirdSem.NumericalMethods.Views.NumericalMethodsView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      T
viewModels=clr-namespace:ProgrammingThirdSem.NumericalMethods.ViewModels     5      .��5
      � $��	24px 12pxq�6	   .+�5      ��   �� Orientation$  Vertical=�6   .+�5      �� $	��Top=�6   $  
Horizontal=�6+   $��0px 0 0 24pxq�6D   .+�5   
   �� $��Очистить��6   $��
0 0 24px 0q�6E   $��
Montserrat)�6Y   $
��11px(�6q   $	��600"�6�   $��0q�6�   
���������6$   �� *ClearCommand+6�   �� 5   
   $��Рассчитать��6   $��
0 0 24px 0q�6K   $��
Montserrat)�6_   $
��11px(�6w   $	��600"�6�   $��0q�6�   
���������6&   �� *CalculateCommand+6�   5   	   � 5      $��
0 0 0 24pxq�6    � RowDefinitions 5   
   �� 5      $
��50px �6   �� 5      $
��24px �6   �� 5      $
��24px �6   �� 5      $
��24px �6   �� 5      $
��24px �6   �� 5      $
��24px �6   �� 5      $
��24px �6   �� 5      $
��24px �6    � ColumnDefinitions 5   
   �� 5      �� 5      �� 5      .+�5    
   �� $��0��6   $��0��6$   $��
0 0 0 20pxq�61   $��Center=�6E   $��14(�6`   $��SemiBold"�6n   $��
Montserrat)�6�   
Метод6�   �� 5!   
   $��0��6   $��1��6#   $��Center=�60   $��Дихотомии��6R   $��
Montserrat)�6f    d� 	IsChecked 6~   �� *IsDichotomyMethodChecked+ �� UpdateSourceTrigger$ PropertyChanged=��� 5"   
   $��0��6   $��2��6#   $��Center=�60   $%��Золотого сечения��6R   $��
Montserrat)�6m    6�   �� *IsGoldenRatioMethodChecked+$ PropertyChanged=��� 5#   
   $��0��6   $��3��6#   $��Center=�60   $��Ньютона��6R   $��
Montserrat)�6d    6|   �� *IsNewtonMethodChecked+$ PropertyChanged=��� 5$   
   $��0��6   $��4��6#   $��Center=�60   $1��+Покоординатного спуска��6R   $��
Montserrat)�6s    6�   �� *" IsCoordinateDescentMethodChecked+$ PropertyChanged=��� 5%   
   $��0��6   $��5��6#   $��Center=�60   $$��Прямоугольников��6R   $��
Montserrat)�6l    6�   �� *IsRectangleMethodChecked+$ PropertyChanged=��� 5&   
   $��0��6   $��6��6#   $��Center=�60   $��Трапеций��6R   $��
Montserrat)�6e    6}   �� *IsTrapezoidMethodChecked+$ PropertyChanged=�6�   �� 5'   
   $��0��6   $��7��6#   $��Center=�60   $��Парабол��6R   $��
Montserrat)�6d    6|   �� *IsParabolaMethodChecked+$ PropertyChanged=�6�   �� 5(   
   $��1��6   $��0��6$   $��
0 0 0 20pxq�61   $��Center=�6E   $��Center=�6`   $��14(�6}   $��SemiBold"�6�   $��
Montserrat)�6�   Результат6�   �� 5)   
   $��1��6   $��1��6$   $��Center=�61    �� TextAlignment$ Center=�6L   $��12(�6c   $��
Montserrat)�6q   ��6�   �� *DichotomyMethodResult+�� 5*   
   $��1��6   $��2��6$   $��Center=�61   $ Center=�6L   $��12(�6c   $��
Montserrat)�6q   ��6�   �� *GoldenRatioMethodResult+�� 5+   
   $��1��6   $��3��6$   $��Center=�61   $ Center=�6L   $��12(�6c   $��
Montserrat)�6q   ��6�   �� *NewtonMethodResult+�� 5,   
   $��1��6   $��4��6$   $��Center=�61   $ Center=�6L   $��12(�6c   $��
Montserrat)�6q   ��6�   �� *CoordinateDescentMethodResult+�� 5-   
   $��1��6   $��5��6$   $��Center=�61   $ Center=�6L   $��12(�6c   $��
Montserrat)�6q   ��6�   �� *RectangleMethodResult+�� 5.   
   $��1��6   $��6��6$   $��Center=�61   $ Center=�6L   $��12(�6c   $��
Montserrat)�6q   ��6�   �� *TrapezoidMethodResult+�� 5/   
   $��1��6   $��7��6$   $��Center=�61   $ Center=�6L   $��12(�6c   $��
Montserrat)�6q   ��6�   �� *ParabolaMethodResult+�� 50   
   $��2��6   $��0��6$   $��
0 0 0 20pxq�61   $��Center=�6E   $��Center=�6`   $��14(�6}   $��SemiBold"�6�   $��
Montserrat)�6�   График6�   �� 51   
   $��2��6   $��1��6!   $��Открыть��6.   $��0q�6n   
�������$��
Montserrat)�6�   $	��500"�6�   $��25px 0q�6�   ��6@   �� *DichotomyShowGraphCommand+6�   �� 52   
   $��2��6   $��2��6%   $  
Horizontal=�62   $��Center=�6K   .+�53      �� $	��min��6   $��0q�6T   
�������$��
Montserrat)�6{   $	��500"�6�   $��5px 0q�6�   ��6!   �� * GoldenRatioMinShowGraphCommand+6�   �� 54      $	��max��6   $��0q�6T   
�������$��
Montserrat)�6{   $	��500"�6�   $��5px 0q�6�   ��6!   �� * GoldenRatioMaxShowGraphCommand+6�   55      �� 56   
   $��2��6   $��3��6%   $  
Horizontal=�62   $��Center=�6K   .+�57      �� $
��null��6   $��0q�6Q   
�������$��
Montserrat)�6x   $	��500"�6�   $��5px 0q�6�   ��6"   �� *NewtonNullShowGraphCommand+6�   �� 58      $��extreme��6   $��0q�6W   
�������$��
Montserrat)�6~   $	��500"�6�   $��5px 0q�6�   ��6%   �� *NewtonExtremeShowGraphCommand+6�   59      �� 5:   
   $��2��6   $��4��6!   $��Открыть��6.   $��0q�6v   
�������$��
Montserrat)�6�   $	��500"�6�   $��25px 0q�6�   ��6@   �� *#!CoordinateDescentShowGraphCommand+6�   �� 5;   
   $��2��6   $��5��6!   $��Открыть��6.   $��0q�6n   
�������$��
Montserrat)�6�   $	��500"�6�   $��25px 0q�6�   ��6@   �� *RectangleShowGraphCommand+6�   �� 5<   
   $��2��6   $��6��6!   $��Открыть��6.   $��0q�6n   
�������$��
Montserrat)�6�   $	��500"�6�   $��25px 0q�6�   ��6@   �� *TrapezoidShowGraphCommand+6�   �� 5=   
   $��2��6   $��7��6!   $��Открыть��6.   $��0q�6m   
�������$��
Montserrat)�6�   $	��500"�6�   $��25px 0q�6�   ��6@   �� *ParabolaShowGraphCommand+6�   5>   	   � 5?       5@   
   �� 5A      �� 5B       5D   
   �� 5E      �� 5F      .+�5H   
   �� $��0��6   $��0��6"   $��0 0 24 0q�62   .+�5I      �� $��	0 0 0 5pxq�6   $
��14px(�6)   $��
Montserrat)�69   $��Medium"�6Q   $
��Wrap=�6e   Функция6�   �� 5J      $��
0 21 0 0pxq�6   $��4 2pxq�6(   $
��12px(�68   $��
Montserrat)�6H   $��Medium"�6`   
���������6t   �� *FunctionExpressionString+$ PropertyChanged=�6�   5K      �� 5L   
   $��0��6   $��1��6"   .+�5M      �� $��	0 0 0 5pxq�6   $
��14px(�6)   $��
Montserrat)�69   $��Medium"�6Q   $
��Wrap=�6e   *(Границы интервала (a, b)6�   � 5N      $��0 20px 0 10pxq�6   $��Bottom=�6(    5O      �� 5P      $��* �6!   �� 5Q      $��* �6!   .+�5S      �� $��0��6   $��
0 0 10px 0q�6&   $	��2pxq�6:   $
��12px(�6H   $��
Montserrat)�6X   $��Medium"�6p   
���������6�   �� *
ParameterA+$ PropertyChanged=�6�   �� 5T      $��1��6   $	��2pxq�6&   $
��12px(�64   $��
Montserrat)�6D   $��Medium"�6\   
���������6p   �� *
ParameterB+$ PropertyChanged=�6�   5U      5V      �� 5W   
   $��1��6   $��0��6"   $��	0 12 24 0q�62   .+�5X      �� $��	0 0 0 5pxq�6   $
��14px(�6)   $��
Montserrat)�69   $��Medium"�6Q   $
��Wrap=�6e   '%Точность результата6�   � 5Y      $��
0 0 0 10pxq�6    5Z      �� 5[      $��* �6!   �� 5\      $��* �6!   .+�5^      �� $��0��6   .+�5_      �� $��	0 0 0 5pxq�6   $
��12px(�6-   $��
Montserrat)�6=   $��Medium"�6U   $
��Wrap=�6i   ,*Мин. сужение отрезка (ε)6�   �� 5`      $
��12px(�6   $��
Montserrat)�6)   $��Medium"�6A   $��0q�6�    �� Text 6U   �� *	Epsilon+$ PropertyChanged=�.@�5a      �� 0.0016#   �� 5b      0.016"   �� 5c      0.16!   �� 5d      16   5e      5f      �� 5g      $��1��6   $��	5px 0 0 0q�6)   .+�5h      �� $��	0 0 0 5pxq�6   $
��12px(�6-   $��
Montserrat)�6=   $��Medium"�6U   $
��Wrap=�6i   +)Кол-во знаков после «,»6�   �� 5i      $
��12px(�6   $��
Montserrat)�6)   $��Medium"�6A   $��0q�6�    6U   �� *SingsAfterCommaCount+$ PropertyChanged=�.@�5j      �� 06   �� 5k      16   �� 5l      26   �� 5m      36   �� 5n      46   �� 5o      56   5p      5q      5r      5s      5t   	   5u      5v      5w      