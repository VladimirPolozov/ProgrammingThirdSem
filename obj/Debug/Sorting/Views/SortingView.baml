   M S B A M L   `   `   `  ����N  JProgrammingThirdSem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:ProgrammingThirdSem.Sorting.ViewModels&ProgrammingThirdSem.Sorting.ViewModels  5      9!clr-namespace:ProgrammingThirdSemProgrammingThirdSem  5      3    -ProgrammingThirdSem.Sorting.Views.SortingView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      K
viewModels4clr-namespace:ProgrammingThirdSem.Sorting.ViewModels     5      3local!clr-namespace:ProgrammingThirdSem     5      .��5      � .+�5      �� $��	24px 12pxq�6   .+�5      �� $	��Top=�6     �� Orientation$  
Horizontal=�6+   $��0px 0 0 36pxq�6D   .+�5   
   �� $ ��Сгенерировать��6   $��
0 0 24px 0q�6M   $��
Montserrat)�6a   $
��11px(�6y   $	��600"�6�   $��0q�6�   
���������6)   �� *GenerateCommand+6�   �� 5   
   $(��"Выгрузить из файла��6   $��
0 0 24px 0q�6R   $��
Montserrat)�6f   $
��11px(�6~   $	��600"�6�   $��0q�6�   
���������6.   �� *LoadDataCommand+6�   �� 5   
   $��Очистить��6   $��
0 0 24px 0q�6E   $��
Montserrat)�6Y   $
��11px(�6q   $	��600"�6�   $��0q�6�   
���������6$   �� *ClearCommand+6�   �� 5   
   $��Сортировать��6   $��
0 0 24px 0q�6G   $��
Montserrat)�6[   $
��11px(�6s   $	��600"�6�   $��0q�6�   
���������6'   �� *SortCommand+6�   5   	   � 5      $��0 0px 0 48pxq�6    � RowDefinitions 5   
   �� 5      6   �� 5      $
��50px �6   �� 5      $
��50px �6   �� 5      $
��50px �6   �� 5      $
��50px �6   �� 5      $
��50px �6   �� 5      6    � ColumnDefinitions 5   
   �� 5      �� 5      �� 5       �� 5!      .+�5#   
   �� $��0��6   $��0��6$   $��
0 0 0 20pxq�61   $��Center=�6E   $��14(�6`   $��SemiBold"�6n   $��
Montserrat)�6�   Тип сортировки6�   �� 5$   
   $��0��6   $��1��6#   $��Center=�60   $��Пузырьковая��6R   $��
Montserrat)�6h    d� 	IsChecked 6�   �� *IsBubbleSortingChecked+ �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5%   
   $��0��6   $��2��6#   $��Center=�60   $��Вставкой��6R   $��
Montserrat)�6e    6}   �� *IsInsertionSortingChecked+$ PropertyChanged=�6�   �� 5&   
   $��0��6   $��3��6#   $��Center=�60   $��Быстрая��6R   $��
Montserrat)�6d    6|   �� *IsQuickSortingChecked+$ PropertyChanged=�6�   �� 5'   
   $��0��6   $��4��6#   $��Center=�60   $��Шейкерная��6R   $��
Montserrat)�6f    6~   �� *IsShakerSortingChecked+$ PropertyChanged=�6�   �� 5(   
   $��0��6   $��5��6#   $��Center=�60   $
��Bogo��6R   $��
Montserrat)�6a    6y   �� *IsBogoSortingChecked+$ PropertyChanged=�6�   �� 5)   
   $��0��6   $��6��6%   .+�5*      	� $��0 0 0 2q�6   $��Center=�6)   $!��По возрастанию��6K   $��
Montserrat)�6d   $��12(�6|    6�   �� *IsAscendingSortChecked+$ PropertyChanged=�6�   	� 5+      $��Center=�6   $��По убыванию��6;   $��
Montserrat)�6Q   $��12(�6i   6y   5,      �� 5-   
   $��1��6   $��0��6$    �� TextAlignment$ Center=�61   $��14(�6H   $��SemiBold"�6V   $��
Montserrat)�6l   $
��Wrap=�6�   64Среднее количество итераций6�   �� 5.   
   $��1��6   $��1��6$   $��Center=�61   $ Center=�6L   $��12(�6c   $��
Montserrat)�6q   ��6�   �� *'%AverageCountOfBubbleSortingIterations+�� 5/   
   $��1��6   $��2��6$   $��Center=�61   $ Center=�6L   $��12(�6c   $��
Montserrat)�6q   ��6�   �� **(AverageCountOfInsertionSortingIterations+�� 50   
   $��1��6   $��3��6$   $��Center=�61   $ Center=�6L   $��12(�6c   $��
Montserrat)�6q   ��6�   �� *&$AverageCountOfQuickSortingIterations+�� 51   
   $��1��6   $��4��6$   $��Center=�61   $ Center=�6L   $��12(�6c   $��
Montserrat)�6q   ��6�   �� *'%AverageCountOfShakerSortingIterations+�� 52   
   $��1��6   $��5��6$   $��Center=�61   $ Center=�6L   $��12(�6c   $��
Montserrat)�6q   ��6�   �� *%#AverageCountOfBogoSortingIterations+�� 53   
   $��2��6   $��0��6$   $ Center=�61   $��14(�6H   $��SemiBold"�6V   $��
Montserrat)�6l   $
��Wrap=�6�   31Среднее время выполнения ms6�   �� 54   
   $��2��6   $��1��6$   $��Center=�61   $ Center=�6L   $��12(�6c   $��
Montserrat)�6q   ��6�   �� *%#AverageTimeOfBubbleSortingExecution+6�   �� 55   
   $��2��6   $��2��6$   $��Center=�61   $ Center=�6L   $��12(�6c   $��
Montserrat)�6q   ��6�   �� *(&AverageTimeOfInsertionSortingExecution+�� 56   
   $��2��6   $��3��6$   $��Center=�61   $ Center=�6L   $��12(�6c   $��
Montserrat)�6q   ��6�   �� *$"AverageTimeOfQuickSortingExecution+�� 57   
   $��2��6   $��4��6$   $��Center=�61   $ Center=�6L   $��12(�6c   $��
Montserrat)�6q   ��6�   �� *%#AverageTimeOfShakerSortingExecution+�� 58   
   $��2��6   $��5��6$   $��Center=�61   $ Center=�6L   $��12(�6c   $��
Montserrat)�6q   ��6�   �� *#!AverageTimeOfBogoSortingExecution+�� 59   
   $��3��6   $��0��6$   $ Center=�61   $��14(�6H   $��SemiBold"�6V   $��
Montserrat)�6l   Результат6�   �� 5:   
   $��3��6   $��1��6'   .��5;      �� $
��Wrap=�6   $��
Montserrat)�6*   ��6B   �� *ResultOfBubbleSorting+$ PropertyChanged=�6�   5<      �� 5=   
   $��3��6   $��2��6'   .��5>      �� $
��Wrap=�6   $��
Montserrat)�6*   ��6B   �� *ResultOfInsertionSorting+$ PropertyChanged=�6�   5?      �� 5@   
   $��3��6   $��3��6'   .��5A      �� $
��Wrap=�6   $��
Montserrat)�6*   ��6B   �� *ResultOfQuickSorting+$ PropertyChanged=�6�   5B      �� 5C   
   $��3��6   $��4��6'   .��5D      �� $
��Wrap=�6   $��
Montserrat)�6*   ��6B   �� *ResultOfShakerSorting+$ PropertyChanged=�6�   5E      �� 5F   
   $��3��6   $��5��6'   .��5G      �� $
��Wrap=�6   $��
Montserrat)�6*   ��6B   �� *ResultOfBogoSorting+$ PropertyChanged=�6�   5H      5I   	   � 5J       5K   
   �� 5L      6   �� 5M      6   �� 5N      6   .+�5P   
   �� $��0��6   $��0 0 0 8q�6$   $	��Top=�65   $��16(�6M   $��SemiBold"�6[   $��
Montserrat)�6q   $"Ввод (через пробел)6�   �� 5Q   
   $��1��6   $��2��6%   $  
Horizontal=�69   $��Right=�6R   $	��Top=�6n   .+�5R      �� $  
Horizontal=�6   $��Center=�60   .+�5S      �� $��	0 0 8 0pxq�6   $
��12px(�6+   $��
Montserrat)�6;   $��SemiBold"�6S   $��Center=�6i   $"Количество тестов:6�   �� 5T      $
��50px��6   $
��20px��6#   $��Right=�61   $��	0 0 5px 0q�6T   $
��12px(�6h   $��
Montserrat)�6x    � 
InputScope$ Number��6�   ��6�   �� *CountOfTests+$ PropertyChanged=�6�   5U      5V      5W   	   �� 5X      .��5Y   
   �� $��125px��6    �� TextWrapping$
 Wrap=�6!   $��12(�65   $��
Montserrat)�6C   ��6[   �� *	UserInput+$ PropertyChanged=�6�   5Z   	   5[      5\      5]      