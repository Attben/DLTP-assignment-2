using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DLTP_assignment_2
{
    /* CLASS: Person
     * PURPOSE: Handle the name, address, phone and email of an entry in an address list
     */
    class Person
    {
        public string name, address, phone, email;

        /* METHOD: Person (default constructor)
         * PURPOSE: Construct a Person object by asking the user to input values for each field.
         * PARAMETERS: none
         * RETURN VALUE: The new Person that was constructed.
         */
        public Person()
        {
            Console.Write("  1. ange namn:    ");
            name = Console.ReadLine();
            Console.Write("  2. ange adress:  ");
            address = Console.ReadLine();
            Console.Write("  3. ange telefon: ");
            phone = Console.ReadLine();
            Console.Write("  4. ange email:   ");
            email = Console.ReadLine();
        }
        /* METHOD: Person (constructor)
         * PURPOSE: Construct a Person object.
         * PARAMETERS: name, address, phone, email - strings to initialize the Person's fields
         * RETURN VALUE: The new Person that was constructed.
         */
        public Person(string name, string address, string phone, string email)
        {
            this.name = name;
            this.address = address;
            this.phone = phone;
            this.email = email;
        }

        /* METHOD: EditField
         * PURPOSE: Change the name, address, phone or email to a new value.
         * PARAMETERS: fieldToChange - The name of the field the caller wants to change.
         *             newValue - The value the caller wants to change the field to.
         * RETURN VALUE: 
         */
        public void EditField(string fieldToChange, string newValue)
        {
            switch (fieldToChange)
            {
                case "namn": name = newValue; break;
                case "adress": address = newValue; break;
                case "telefon": phone = newValue; break;
                case "email": email = newValue; break;
                default: Console.WriteLine($"Fel: Fältet {fieldToChange} existerar inte."); break;
            }
        }
        public void Print()
        {
            Console.WriteLine("{0}, {1}, {2}, {3}", name, address, phone, email);
        }
    }
    class Program
    {
        /* METHOD: Main (static)
         * PURPOSE: Command input loop to let a user interact with an address list.
         * PARAMETERS: none
         * RETURN VALUE: none
         */
        static void Main()
        {
            List<Person> addressList = LoadAddressList();

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
                    Console.WriteLine("Lägger till ny person...");
                    addressList.Add(new Person()); //Note: The default constructor will ask for user input.
                    Console.WriteLine("Klart.");
                }
                else if (command == "ta bort")
                {
                    RemovePersonFromList(addressList);
                }
                else if (command == "visa")
                {
                    PrintAddressList(addressList);
                }
                else if (command == "ändra")
                {
                    EditPersonInList(addressList);
                }
                else
                {
                    Console.WriteLine("Okänt kommando: {0}", command);
                }
            } while (command != "sluta");
        }

        /* METHOD: EditPersonInList (static)
         * PURPOSE: Edit the data of a Person in am address list.
         * PARAMETERS: addressList - A list of Person objects
         * RETURN VALUE: none
         */
        private static void EditPersonInList(List<Person> addressList)
        {
            Console.Write("Vem vill du ändra (ange namn): ");
            string personToChange = Console.ReadLine();
            int found = -1;
            for (int i = 0; i < addressList.Count(); i++)
            {
                if (addressList[i].name == personToChange) found = i;
            }
            if (found == -1)
            {
                Console.WriteLine("Tyvärr: {0} fanns inte i telefonlistan", personToChange);
            }
            else
            {
                Console.Write("Vad vill du ändra (namn, adress, telefon eller email): ");
                string fieldToChange = Console.ReadLine();
                Console.Write("Vad vill du ändra {0} på {1} till: ", fieldToChange, personToChange);
                string newValue = Console.ReadLine();
                addressList[found].EditField(fieldToChange, newValue);
            }
        }
        /* METHOD: PrintAddressList (static)
         * PURPOSE: Print the name, address, phone number and email of every Person in an address list
         * PARAMETERS: addressList - A List of Person objects
         * RETURN VALUE: none
         */
        private static void PrintAddressList(List<Person> addressList)
        {
            for (int i = 0; i < addressList.Count(); i++)
            {
                addressList[i].Print();
            }
        }
        /* METHOD: RemovePersonFromList (static)
         * PURPOSE: Remove a Person from an address list
         * PARAMETERS: addressList - A List of Person objects
         * RETURN VALUE: none
         */
        private static void RemovePersonFromList(List<Person> addressList)
        {
            Console.Write("Vem vill du ta bort (ange namn): ");
            string personToRemove = Console.ReadLine();
            int found = -1;
            for (int i = 0; i < addressList.Count(); i++)
            {
                if (addressList[i].name == personToRemove) found = i;
            }
            if (found == -1)
            {
                Console.WriteLine("Tyvärr: {0} fanns inte i telefonlistan", personToRemove);
            }
            else
            {
                addressList.RemoveAt(found);
            }
        }
        /* METHOD: LoadAddressList (static)
         * PURPOSE: Load a List of Person objects from a file.
         * PARAMETERS: none
         * RETURN VALUE: The List<Person> that was loaded.
         */
        private static List<Person> LoadAddressList()
        {
            Console.Write("Laddar adresslistan ... ");
            List<Person> addressList = new List<Person>();
            using (StreamReader fileStream = new StreamReader(@"..\..\address.lis"))
            {
                while (fileStream.Peek() >= 0)
                {
                    string line = fileStream.ReadLine();
                    // Console.WriteLine(line);
                    string[] word = line.Split('#');
                    // Console.WriteLine("{0}, {1}, {2}, {3}", word[0], word[1], word[2], word[3]);
                    Person P = new Person(word[0], word[1], word[2], word[3]);
                    addressList.Add(P);
                }
            }
            Console.WriteLine("klart!");
            return addressList;
        }
    }
}
