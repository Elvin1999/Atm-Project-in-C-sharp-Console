using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ConsoleApp10
{
    class Card
    {
        public Card() { }
        public Card(string pan, string pin, string cvc, string expireDate, decimal balans)
        {
            Pan = pan;
            Pin = pin;
            Cvc = cvc;
            ExpireDate = expireDate;
            Balans = balans;
        }
        public string Pan { get; set; }
        public string Pin { get; set; }
        public string Cvc { get; set; }
        public string ExpireDate { get; set; }
        public decimal Balans { get; set; }
    }
    class User
    {
        public User(string name, string surname, Card card)
        {
            Name = name;
            Surname = surname;
            Card = card;
        }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Card Card { get; set; }
    }
    class Controller
    {
        int count = 0;
        int cur = 0;
        User[] users = new User[0];
        public DateTime[] currentArray = new DateTime[0];
        public void AddDateTime(DateTime current)
        {
            Array.Resize(ref currentArray, currentArray.Length + 1);
            currentArray[cur] = current;
            ++cur;
        }
        public void Add(User user)
        {
            Array.Resize(ref users, users.Length + 1);
            users[count] = user;
            ++count;
        }
        public decimal GetPersonBalans(string pin)
        {
            for (int i = 0; i < users.Length; i++)
            {
                if (users[i].Card.Pin == pin)
                {
                    return users[i].Card.Balans;
                }
            }
            return 0;
        }
        public void GetPersonProperty(string pin)
        {
            for (int i = 0; i < users.Length; i++)
            {
                if (users[i].Card.Pin == pin)
                {
                    Console.WriteLine($"{users[i].Name}   {users[i].Surname } Welcome to system");
                }
            }
        }
        public void ShowBalansProperty()
        {
            Console.WriteLine("1.10 $");
            Console.WriteLine("2.20 $");
            Console.WriteLine("3.50 $");
            Console.WriteLine("4.100 $");
            Console.WriteLine("5.Opportunity to take the desired money");
        }
        public bool GetMoney(string pin, int money)
        {
            if (money <= GetPersonBalans(pin))
            {
                for (int i = 0; i < users.Length; i++)
                {
                    if (users[i].Card.Pin == pin)
                    {
                        users[i].Card.Balans -= money;
                        return true;
                    }
                }
            }
            else
            {
                throw new Exception("There is not this money in your count");
            }
            return false;
        }
        public void AddMoneyToBalanc(string pin, decimal money)
        {
            for (int i = 0; i < users.Length; i++)
            {
                if (users[i].Card.Pin == pin)
                {
                    users[i].Card.Balans += money;
                    Console.WriteLine("========================================");
                    Console.WriteLine("Operation was successfully");
                    Console.WriteLine("========================================");
                    break;
                }
            }
        }
        public bool Access(string pin)
        {
            for (int i = 0; i < users.Length; i++)
            {
                if (users[i].Card.Pin == pin)
                {
                    return true;
                }
            }
            return false;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\t\t========================================");
            Console.WriteLine("\t\t==================ATM===================");
            Console.WriteLine("\t\t========================================");
            Card a = new Card("123465789", "5689", "123", "11/12", 100000);
            Card a1 = new Card("963258741", "4567", "456", "08/10", 50000);
            Card a2 = new Card("159854726", "9632", "789", "01/10", 7800);
            Card a3 = new Card("363636366", "0125", "951", "03/12", 96500);
            Card a4 = new Card("123465789", "2711", "145", "11/12", 21000);
            Card a5 = new Card("857496321", "1234", "145", "11/12", 280);
            User Elvin = new User("Elvin", "Camalzade", a);
            User Nazim = new User("Nazim", "Memishov", a1);
            User Isi = new User("Isi", "Seyidmemmedli", a2);
            User Nihad = new User("Nihad", "Elekberzade", a3);
            User Tural = new User("Tural", "Eliyev", a4);
            Controller controller = new Controller();
            controller.Add(Elvin);
            controller.Add(Nazim);
            controller.Add(Isi);
            controller.Add(Nihad);
            controller.Add(Tural);
            string pin; string pincopy = "";
            do
            {
                Console.Write("Include PIN - >");
                pin = Console.ReadLine();
                if (!controller.Access(pin))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("========================================");
                    Console.WriteLine("Invalid PIN");
                    Console.WriteLine("========================================");
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("========================================");
                    Console.WriteLine("PIN is right");
                    Console.WriteLine("========================================");
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
            } while (!controller.Access(pin));
            controller.GetPersonProperty(pin);
            DateTime current = DateTime.Now;
            int money = 0;
            while (true)
            {
                if (controller.Access(pin))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    controller.GetPersonBalans(pin);
                    Console.WriteLine("1.Count of Money");
                    Console.WriteLine("2.Cash Money");
                    Console.WriteLine("3.History of Operation");
                    Console.WriteLine("4.Money Transfer");
                    int select = Convert.ToInt32(Console.ReadLine());
                    if (select == 1)
                    {
                        Console.Clear();
                        Console.WriteLine("========================================");
                        Console.Write("Your count->>");
                        Console.WriteLine($"{controller.GetPersonBalans(pin)} $");
                        Console.WriteLine("========================================");
                    }
                    else if (select == 2)
                    {
                        Console.Clear();
                        controller.ShowBalansProperty();
                        Console.Write("Select -> ");
                        int selection = Convert.ToInt32(Console.ReadLine());
                        switch (selection)
                        {
                            case 1: money = 10; break;
                            case 2: money = 20; break;
                            case 3: money = 50; break;
                            case 4: money = 100; break;
                            case 5: money = Convert.ToInt32(Console.ReadLine()); break;
                        }
                        try
                        {
                            controller.GetMoney(pin, money);
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("========================================");
                            Console.WriteLine(ex.Message);
                            Console.WriteLine("========================================");
                        }
                        Console.WriteLine("========================================");
                        Console.Write("Your count->>");
                        Console.WriteLine($"{controller.GetPersonBalans(pin)} $");
                        Console.WriteLine("========================================");
                        current = DateTime.Now;
                        controller.AddDateTime(current);
                    }
                    else if (select == 3)
                    {
                        Console.WriteLine("========================================");
                        Console.WriteLine("==================LOGGER================");
                        Console.WriteLine("========================================");
                        if (controller.currentArray.Length == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("========================================");
                            Console.WriteLine("You did not any operation in system");
                            Console.WriteLine("========================================");
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        for (int i = 0; i < controller.currentArray.Length; i++)
                        {
                            Console.WriteLine("========================================");
                            Console.WriteLine($"Date : {controller.currentArray[i].TimeOfDay} | {controller.currentArray[i].Day}/{controller.currentArray[i].Month}/{controller.currentArray[i].Year}  ");
                            Console.WriteLine("========================================");
                        }
                    }
                    else if (select == 4)
                    {
                        do
                        {
                            Console.WriteLine("========================================");
                            Console.Write("Include PIN - >"); pincopy = Console.ReadLine();
                            if (pin == pincopy)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("========================================");
                                Console.WriteLine("Inaccessable");
                                Console.WriteLine("========================================");
                                Console.ForegroundColor = ConsoleColor.Blue;
                            }
                            else if (!controller.Access(pincopy))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("========================================");
                                Console.WriteLine("Invalid Pin");
                                Console.WriteLine("========================================");
                                Console.ForegroundColor = ConsoleColor.Blue;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("========================================");
                                Console.WriteLine("PIN is right");
                                Console.WriteLine("========================================");
                                Console.ForegroundColor = ConsoleColor.Blue;
                                controller.ShowBalansProperty();
                                Console.Write("Select -> ");
                                int selection = Convert.ToInt32(Console.ReadLine());
                                switch (selection)
                                {
                                    case 1: money = 10; break;
                                    case 2: money = 20; break;
                                    case 3: money = 50; break;
                                    case 4: money = 100; break;
                                    case 5: money = Convert.ToInt32(Console.ReadLine()); break;
                                }
                                try
                                {
                                    if (controller.GetMoney(pin, money))
                                    {
                                        controller.AddMoneyToBalanc(pincopy, money);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("========================================");
                                    Console.WriteLine(ex.Message);
                                    Console.WriteLine("========================================");
                                }
                                Console.WriteLine("========================================");
                                Console.Write("Your count->>");
                                Console.WriteLine($"{controller.GetPersonBalans(pin)} $");
                                Console.WriteLine("========================================");
                                current = DateTime.Now;
                                controller.AddDateTime(current);
                            }
                        } while (!controller.Access(pin));
                    }
                    else
                    {
                        Console.WriteLine("========================================");
                        Console.WriteLine("Please write right selection(between 1-4)");
                        Console.WriteLine("========================================");
                    }
                }
            }
        }
    }
}


