using System;
using System.Diagnostics;
using ProgrammingThirdSem.Sorting.ViewModels;


namespace ProgrammingThirdSem.Sorting.Models
{
    public static class SortingModel
    {
        // Сортировка пузырьком
        public static (MixedValue[], int, double) BubbleSort(MixedValue[] array, bool isAscendingSortChecked)
        {
            if (IsSorted(array, isAscendingSortChecked))
            {
                return (array, 0, 0);
            }
            
            var stopwatch = Stopwatch.StartNew();
            var iterations = 0;
            
            for (var i = 0; i < array.Length - 1; i++)
            {
                for (var j = 0; j < array.Length - i - 1; j++)
                {
                    iterations++;
                    var needSwap = isAscendingSortChecked
                        ? array[j].CompareTo(array[j + 1]) > 0
                        : array[j].CompareTo(array[j + 1]) < 0;

                    if (needSwap)
                    {
                        (array[j], array[j + 1]) = (array[j + 1], array[j]);
                    }
                }
            }

            stopwatch.Stop();
            return (array, iterations, stopwatch.Elapsed.TotalMilliseconds);
        }
        
        // Сортировка вставками
        public static (MixedValue[], int, double) InsertionSort(MixedValue[] array, bool isAscendingSortChecked)
        {
            if (IsSorted(array, isAscendingSortChecked))
            {
                return (array, 0, 0);
            }
            
            var stopwatch = Stopwatch.StartNew();
            var iterations = 0;
            
            for (var i = 1; i < array.Length; ++i)
            {
                var current = array[i];
                var j = i - 1;
                while (j >= 0 && (isAscendingSortChecked 
                           ? array[j].CompareTo(current) > 0 
                           : array[j].CompareTo(current) < 0))
                {
                    ++iterations;
                    array[j + 1] = array[j];
                    --j;
                }
                array[j + 1] = current;
            }
            
            stopwatch.Stop();
            return (array, iterations, new TimeSpan(stopwatch.ElapsedTicks).TotalMilliseconds);
        }
        
        // Быстрая сортировка
        public static (MixedValue[], int, double) QuickSort(MixedValue[] array, bool isAscendingSortChecked)
        {
            if (IsSorted(array, isAscendingSortChecked))
            {
                return (array, 0, 0);
            }

            var stopwatch = Stopwatch.StartNew();
            var iterations = 0;

            QuickSorting(array, 0, array.Length - 1, isAscendingSortChecked, ref iterations);

            stopwatch.Stop();
            return (array, iterations, new TimeSpan(stopwatch.ElapsedTicks).TotalMilliseconds);
        }

        // Метод быстрой сортировки
        private static void QuickSorting(MixedValue[] array, int left, int right, bool isAscendingSortChecked, ref int iterations)
        {
            if (left < right)
            {
                var pivot = Partition(array, left, right, isAscendingSortChecked, ref iterations);
                QuickSorting(array, left, pivot - 1, isAscendingSortChecked, ref iterations);
                QuickSorting(array, pivot + 1, right, isAscendingSortChecked, ref iterations);
            }
        }

        // Метод разделения массива
        private static int Partition(MixedValue[] array, int left, int right, bool isAscendingSortChecked, ref int iterations)
        {
            var pivot = array[left];

            while (true)
            {
                while (isAscendingSortChecked
                           ? array[left].CompareTo(pivot) < 0
                           : array[left].CompareTo(pivot) > 0)
                {
                    ++left;
                    ++iterations;
                }

                while (isAscendingSortChecked
                           ? array[right].CompareTo(pivot) > 0
                           : array[right].CompareTo(pivot) < 0)
                {
                    --right;
                    ++iterations;
                }

                if (left >= right)
                {
                    return right;
                }

                (array[left], array[right]) = (array[right], array[left]);
                ++iterations;

                ++left;
                --right;
            }
        }
        
        // Шейкерная сортировка
        public static (MixedValue[], int, double) ShakerSort(MixedValue[] array, bool isAscendingSortChecked)
        {
            if (IsSorted(array, isAscendingSortChecked))
            {
                return (array, 0, 0);
            }
            
            var stopwatch = Stopwatch.StartNew();
            var iterations = 0;

            var left = 0;
            var right = array.Length - 1;
            while (left <= right)
            {
                // Проход справа налево
                for (var i = right; i > left; --i)
                {
                    if ((isAscendingSortChecked && array[i - 1].CompareTo(array[i]) > array[i].CompareTo(array[i - 1])) ||
                        (!isAscendingSortChecked && array[i - 1].CompareTo(array[i]) < array[i].CompareTo(array[i - 1])))
                    {
                        (array[i - 1], array[i]) = (array[i], array[i - 1]);
                        ++iterations;
                    }
                }
                ++left;

                // Проход слева направо
                for (var i = left; i < right; ++i)
                {
                    if ((isAscendingSortChecked && array[i].CompareTo(array[i + 1]) > array[i + 1].CompareTo(array[i])) ||
                        (!isAscendingSortChecked && array[i].CompareTo(array[i + 1]) < array[i + 1].CompareTo(array[i])))
                    {
                        (array[i], array[i + 1]) = (array[i + 1], array[i]);
                        ++iterations;
                    }
                }
                --right;
            }

            stopwatch.Stop();
            return (array, iterations, new TimeSpan(stopwatch.ElapsedTicks).TotalMilliseconds);
        }


        // Bogo-сортировка
        public static (MixedValue[], int, double) BogoSort(MixedValue[] array, bool isAscendingSortChecked)
        {
            if (IsSorted(array, isAscendingSortChecked))
            {
                return (array, 0, 0);
            }
            
            var stopwatch = Stopwatch.StartNew();
            var iterations = 0;
            
            while (!IsSorted(array, isAscendingSortChecked))
            {
                ++iterations;
                Shuffle(array);
                if (iterations > 1000)
                {
                    break;
                }
            }
            
            stopwatch.Stop();
            return (array, iterations, new TimeSpan(stopwatch.ElapsedTicks).TotalMilliseconds);
        }
        
        // Метод проверки отсортированности массива
        private static bool IsSorted(MixedValue[] array, bool isAscendingSortChecked)
        {
            for (var i = 0; i < array.Length - 1; ++i)
            {
                var comparisonResult = array[i].CompareTo(array[i + 1]);
                if (isAscendingSortChecked)
                {
                    // Если по возрастанию, проверяем, что текущий элемент больше следующего
                    if (comparisonResult > 0)
                    {
                        return false;
                    }
                }
                else
                {
                    // Если по убыванию, проверяем, что текущий элемент меньше следующего
                    if (comparisonResult < 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        
        // Метод перемешивания массива
        private static void Shuffle(MixedValue[] array)
        {
            var random = new Random();
            for (var i = 0; i < array.Length; ++i)
            {
                var j = random.Next(i, array.Length);
                (array[i], array[j]) = (array[j], array[i]);
            }
        }
    }
}