using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        string filename = "books.csv";

        // Dictionary to store book number and publisher
        Dictionary<string, string> books = new Dictionary<string, string>();

        // Read the CSV file
        using (StreamReader reader = new StreamReader(filename))
        {
            reader.ReadLine();

            // Extract the publisher
            string line;
            Random random = new Random();
            HashSet<string> generatedNumbers = new HashSet<string>();

            while ((line = reader.ReadLine()) != null)
            {
                string[] row = line.Split(',');
                string publisher = row[4];
                string randomNumber;
                do
                {
                    randomNumber = GenerateRandomNumber(random);
                }
                while (generatedNumbers.Contains(randomNumber));

                generatedNumbers.Add(randomNumber);
                books.Add(randomNumber, publisher);
            }
        }

        // Print the book number and publisher
        foreach (KeyValuePair<string, string> book in books)
        {
            Console.WriteLine("Book Number: {0}, Publisher: {1}", book.Key, book.Value);
        }
    }

    static string GenerateRandomNumber(Random random)
    {
        return random.Next(1000, 10000).ToString();
    }
}