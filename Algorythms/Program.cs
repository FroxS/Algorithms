using Algorithms;
using System;
using System.Collections.Generic;

namespace Algorythms
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Client> clients = Random();
            Display(clients);


            Console.WriteLine("\nSort Br Name");
            clients.Sort(new ClientComapre(true,"Name"));
            Display(clients);

            //Console.WriteLine("\nSort Br SecondName");
            //clients.Sort(new ClientComapre(true, "SecondName"));
            //Display(clients);

            //Console.WriteLine("\nSort Br Id");
            //clients.Sort(new ClientComapre(true, "Id"));
            //Display(clients);


            var index = clients.BinarySearch("Name","Karol");

            if(index > 0)
                Display(clients[index]);

            clients.Sort(Client.BaseComparer);
            clients.SearchAndInsert(new Client() { Id = 10, Name = "Tomasz", SecondName = "Gts" });
            Display(clients);

            Console.ReadKey();
        }

        private static void Display(List<Client> list)
        {
            Console.WriteLine();
            foreach (var s in list)
            {
                Console.WriteLine($"[{s?.Id}]{s?.Name} {s?.SecondName} [{s?.Data}]");
            }
        }

        private static void Display(Client item)
        {
            Console.WriteLine();
            Console.WriteLine($"[{item?.Id}]{item?.Name} {item?.SecondName} [{item?.Data}]");
            
        }

        private static List<Client> Random()
        {
            var rtnlist = new List<Client>();
            rtnlist.Add(new Client() { Id = 5, Name = "Tomasz", SecondName = "Gts" });
            rtnlist.Add(new Client() { Id = 6, Name = "Bartek", SecondName = "tjud" });
            rtnlist.Add(new Client() { Id = 7, Name = "Kacper", SecondName = "Fath" });
            rtnlist.Add(new Client() { Id = 8, Name = "Karol", SecondName = "Hery" });
            rtnlist.Add(new Client() { Id = 9, Name = "Zyga", SecondName = "Gtuja" });
            rtnlist.Add(new Client() { Id = 1, Name= "Adam", SecondName= "testc"});
            rtnlist.Add(new Client() { Id = 2, Name = "Kuba", SecondName = "Fast" });
            rtnlist.Add(new Client() { Id = 3, Name = "Maciej", SecondName = "Kast" });
            rtnlist.Add(new Client() { Id = 4, Name = "Jakub", SecondName = "Bast" });
            

            return rtnlist;
        }


    }
}
