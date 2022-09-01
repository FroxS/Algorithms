using System;
using System.Collections.Generic;

namespace Algorithms
{
    public static class Algorythms
    {
        /// <summary>
        /// Custom Binary search method to found item in list.
        /// </summary>
        /// <typeparam name="T">Type of element in list</typeparam>
        /// <typeparam name="Key">type of parameter to found</typeparam>
        /// <param name="list">The list of items is searched</param>
        /// <param name="propName">Name of propertes to found</param>
        /// <param name="value">Value of this parameter</param>
        /// <param name="comparer">The IComparer implementation to use in BinarySearch</param>
        /// <returns> The zero-based index of item in the sorted list if item is found; otherwise, a negative number that is the bitwise complement of the index of the next element that is larger than item or, if there is no larger element, the bitwise complement of list</returns>
        public static int BinarySearch<T,Key>(this List<T> list, string propName, Key value,IComparer<T> comparer) 
            where T: new() 
            where Key : IComparable
        {
            var tmpClient = new T();

            try
            {
                typeof(T).GetProperty(propName).SetValue(tmpClient, value);
            }
            catch { throw new ArgumentOutOfRangeException($"Properties {propName } i invalid"); }
            
            return list.BinarySearch(tmpClient, comparer);

        }

        /// <summary>
        /// Custom Binary search method to found item in list with <see cref="BaseComparer{T}"/>
        /// </summary>
        /// <typeparam name="T">Type of element in list</typeparam>
        /// <typeparam name="Key">type of parameter to found</typeparam>
        /// <param name="list">The list of items is searched</param>
        /// <param name="propName">Name of propertes to found</param>
        /// <param name="value">Value of this parameter</param>
        /// <returns> The zero-based index of item in the sorted list if item is found; otherwise, a negative number that is the bitwise complement of the index of the next element that is larger than item or, if there is no larger element, the bitwise complement of list</returns>
        public static int BinarySearch<T, Key>(this List<T> list, string propName, Key value)
            where T : new()
            where Key : IComparable
        {
            var tmpClient = new T();

            typeof(T).GetProperty(propName).SetValue(tmpClient, value);

            return list.BinarySearch(tmpClient, new BaseComparer<T>(propName));

        }

        /// <summary>
        /// Custom Insert item to list. This list must by sorted !!!!
        /// </summary>
        /// <typeparam name="T">Type of element in list</typeparam>
        /// <param name="list">The list of items to inset item</param>
        /// <param name="item">Item to insert in list</param>
        /// <param name="comparer">The IComparer implementation to use in BinarySearch</param>
        /// <returns>Tru if item added and false if item not added becouse item is on the list</returns>
        public static bool SearchAndInsert<T>(this List<T> list, T item, BaseComparer<T> comparer)
        {
            int index = list.BinarySearch(item, comparer);

            if (index < 0)
            {
                list.Insert(~index, item);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Custom Insert item to list. This list must by sorted !!!! Type of element in list must Implement <see cref="IContainsComparer{T}" /> to get base Comparer
        /// </summary>
        /// <typeparam name="T">Type of element in list</typeparam>
        /// <param name="list">The list of items to inset item</param>
        /// <param name="item">Item to insert in list</param>
        /// <returns>Tru if item added and false if item not added becouse item is on the list</returns>
        public static bool SearchAndInsert<T>(this List<T> list, T item)
            where T: IContainsComparer<T>, new()
        {
            BaseComparer<T> comparer = new T().GetBaseComparer();
            return list.SearchAndInsert(item, comparer);
        }
    }

    public interface IContainsComparer<T>
    {
        public static BaseComparer<T> BaseComparer;
        public BaseComparer<T> GetBaseComparer();
    }

    public class BaseComparer<T> : IComparer<T>
    {
        protected bool _sortAscending;
        protected string _columnToSortOn;

        public BaseComparer(bool sortAscending, string columnToSortOn)
        {
            _sortAscending = sortAscending;
            _columnToSortOn = columnToSortOn;
        }

        public BaseComparer(string columnToSortOn) : this(true, columnToSortOn) { }


        protected int SortBy<T>(T x, T y) where T : IComparable
        {
            var lastNameResult = x.CompareTo(y);
            if (lastNameResult != 0)
                return lastNameResult;
            return x.CompareTo(y);
        }

        protected int ApplySortDirection(int result)
        {
            return _sortAscending ? result : (result * -1);
        }

        public virtual int Compare(T? x, T? y)
        {
            IComparable XVAL = null;
            IComparable YVAL = null;
            if (x == null && y == null) return 0;
            if (x == null) return ApplySortDirection(-1);
            if (y == null) return ApplySortDirection(1);

            try
            {
                XVAL = typeof(T).GetProperty(_columnToSortOn).GetValue(x, null) as IComparable;
                YVAL = typeof(T).GetProperty(_columnToSortOn).GetValue(x, null) as IComparable;
            }
            catch { throw new ArgumentOutOfRangeException(string.Format("Can't sort on column {0}", _columnToSortOn)); }


            if (XVAL == null && YVAL == null) return 0;
            if (XVAL == null) return ApplySortDirection(-1);
            if (YVAL == null) return ApplySortDirection(1);
            return ApplySortDirection(SortBy(XVAL, YVAL));
        }
    }

}
