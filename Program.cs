using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTP_assignment_2
{
    class Person
    {
        public string namn, adress, telefon, email;

        public Person()
        {
            Console.WriteLine("Lägger till ny person");
            Console.Write("  1. ange namn:    ");
            namn = Console.ReadLine();
            Console.Write("  2. ange adress:  ");
            adress = Console.ReadLine();
            Console.Write("  3. ange telefon: ");
            telefon = Console.ReadLine();
            Console.Write("  4. ange email:   ");
            email = Console.ReadLine();
        }
        public Person(string N, string A, string T, string E)
        {
            namn = N; adress = A; telefon = T; email = E;
        }

        public void Edit(string fieldToChange, string newValue)
        {
            switch (fieldToChange)
            {
                case "namn": namn = newValue; break;
                case "adress": adress = newValue; break;
                case "telefon": telefon = newValue; break;
                case "email": email = newValue; break;
                default: break;
            }
        }
        public void Print()
        {
            Console.WriteLine("{0}, {1}, {2}, {3}", namn, adress, telefon, email);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> Dict = LoadAddressList();

            Console.WriteLine("Hej och välkommen till adresslistan");
            Console.WriteLine("Skriv 'sluta' för att sluta!");
            string command;
            do
            {
                Console.Write("> ");
                command = Console.ReadLine();
                if (command == "sluta")
                {
                    Console.WriteLine("Hej då!");
                }
                else if (command == "ny")
                {
                    Dict.Add(new Person()); //Note: The default constructor will ask for user input. 
                }
                else if (command == "ta bort")
                {
                    RemovePersonFromList(Dict);
                }
                else if (command == "visa")
                {
                    PrintAddressList(Dict);
                }
                else if (command == "ändra")
                {
                    ChangePersonInList(Dict);
                }
                else
                {
                    Console.WriteLine("Okänt kommando: {0}", command);
                }
            } while (command != "sluta");
        }

        private static void ChangePersonInList(List<Person> Dict)
        {
            Console.Write("Vem vill du ändra (ange namn): ");
            string villÄndra = Console.ReadLine();
            int found = -1;
            for (int i = 0; i < Dict.Count(); i++)
            {
                if (Dict[i].namn == villÄndra) found = i;
            }
            if (found == -1)
            {
                Console.WriteLine("Tyvärr: {0} fanns inte i telefonlistan", villÄndra);
            }
            else
            {
                Console.Write("Vad vill du ändra (namn, adress, telefon eller email): ");
                string fältAttÄndra = Console.ReadLine();
                Console.Write("Vad vill du ändra {0} på {1} till: ", fältAttÄndra, villÄndra);
                string nyttVärde = Console.ReadLine();
                Dict[found].Edit(fältAttÄndra, nyttVärde);
            }
        }

        private static void PrintAddressList(List<Person> Dict)
        {
            for (int i = 0; i < Dict.Count(); i++)
            {
                Dict[i].Print();
            }
        }

        private static void RemovePersonFromList(List<Person> Dict)
        {
            Console.Write("Vem vill du ta bort (ange namn): ");
            string villTaBort = Console.ReadLine();
            int found = -1;
            for (int i = 0; i < Dict.Count(); i++)
            {
                if (Dict[i].namn == villTaBort) found = i;
            }
            if (found == -1)
            {
                Console.WriteLine("Tyvärr: {0} fanns inte i telefonlistan", villTaBort);
            }
            else
            {
                Dict.RemoveAt(found);
            }
        }

        private static List<Person> LoadAddressList()
        {
            Console.Write("Laddar adresslistan ... ");
            List<Person> Dict = new List<Person>();
            using (StreamReader fileStream = new StreamReader(@"..\..\address.lis"))
            {
                while (fileStream.Peek() >= 0)
                {
                    string line = fileStream.ReadLine();
                    // Console.WriteLine(line);
                    string[] word = line.Split('#');
                    // Console.WriteLine("{0}, {1}, {2}, {3}", word[0], word[1], word[2], word[3]);
                    Person P = new Person(word[0], word[1], word[2], word[3]);
                    Dict.Add(P);
                }
            }
            Console.WriteLine("klart!");
            return Dict;
        }
    }
}
