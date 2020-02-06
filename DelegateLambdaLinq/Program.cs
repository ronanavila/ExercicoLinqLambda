﻿using System;
using System.IO;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using DelegateLambdaLinq.Entities;

namespace DelegateLambdaLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter full file path: ");
            string path = Console.ReadLine();
            Console.Write("Enter Salary: ");
            double salary = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);



            List<Employee> list = new List<Employee>();

            try
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] data = sr.ReadLine().Split(',');
                        string name = data[0];
                        string email = data[1];
                        double income = double.Parse(data[2], CultureInfo.InvariantCulture);
                        list.Add(new Employee(name, email, income));
                    }
                }



                var result = list.Where(p => p.Salary >= salary).Select(p => p.Email);
                var startM = list.Where(p => p.Name[0] == 'M').Sum(p => p.Salary);

                Console.WriteLine($"Email of people whose salary is more than " +
                    $"{salary.ToString("F2", CultureInfo.InvariantCulture)}:");

                foreach (string e in result)
                {
                    Console.WriteLine(e);
                }
                Console.WriteLine("Sum of salary of people whose name starts with 'M': "
                    + startM.ToString("F2", CultureInfo.InvariantCulture));
            }
            catch (IOException e)
            {
                Console.WriteLine("An Error Occurred");
                Console.WriteLine(e);
            }
        }
    }
}
