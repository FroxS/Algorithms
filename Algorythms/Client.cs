using Algorithms;
using System;
using System.Collections.Generic;

namespace Algorythms
{
    public class Client : IContainsComparer<Client>
    {
        public static BaseComparer<Client> BaseComparer = new ClientComapre(true, nameof(Id));
        public int Id { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public DateTime Data { get; set; }

        public BaseComparer<Client> GetBaseComparer()
        {
            return BaseComparer;
        }
    }


    public class ClientComapre : BaseComparer<Client>
    {
        public ClientComapre(bool sortAscending, string columnToSortOn) : base(sortAscending, columnToSortOn) { }

        public ClientComapre(string columnToSortOn) : this(true, columnToSortOn) { }

        public override int Compare(Client x, Client y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return ApplySortDirection(-1);
            if (y == null) return ApplySortDirection(1);



            switch (_columnToSortOn)
            {
                case nameof(x.Name):
                    return ApplySortDirection(SortBy(x.Name, y.Name));
                case nameof(x.SecondName):
                    return ApplySortDirection(SortBy(x.SecondName, y.SecondName));
                case nameof(x.Id):
                    return ApplySortDirection(SortBy(x.Id, y.Id));
                default:
                    throw new ArgumentOutOfRangeException(string.Format("Can't sort on column {0}",_columnToSortOn));
            }
        }

    }
}
