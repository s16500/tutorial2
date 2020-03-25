using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;
using tutorial2.Models;

namespace tutorial2
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputPath = args.Length > 0 ? args[0] : @"Files\data.csv";
            var outputPath = args.Length > 1 ? args[1] : @"Files\result";
            var outputType = args.Length > 2 ? args[2] : "xml";

            Console.WriteLine($"{inputPath}\n{outputPath}\n{outputType}");

            try
            {
                if (!File.Exists(inputPath))
                    throw new FileNotFoundException("ERR", inputPath.Split("\\")[^1]);

                var university = new University
                {
                    Author = "Mustafa Erdem S16500"
                };

                foreach (var line in File.ReadAllLines(inputPath))
                {
                    var splitted = line.Split(",");

                    if (splitted.Length < 9)
                    {
                        File.AppendAllText(@"Files\Log.txt", $"{DateTime.UtcNow} ERR not enough information in line \n");
                        continue;
                       
                    }


                    foreach (var field in splitted)
                    {
                        if (field.Length <= 0)
                        {
                            File.AppendAllText(@"Files\Log.txt", $"{DateTime.UtcNow} ERR empty {splitted[0]} {splitted[1]} \n");
                            continue;
                        }
                    }

                    var studies = new Studies
                    {
                        nameOfStudy = splitted[2],
                        modeOfStudy = splitted[3],
                    };

                    var stud = new Student
                    {
                        FirstName = splitted[0],
                        LastName = splitted[1],
                        Studies = studies,
                        indexNumber = "s" + splitted[4],
                        BirthDate = splitted[5],
                        Email = splitted[6],
                        MothersName = splitted[7],
                        FathersName = splitted[8],

                    };
                    university.Students.Add(stud);
                }

                // xml

                using var writer = new FileStream($"{outputPath}.{outputType}", FileMode.Create);
                var serializer = new XmlSerializer(typeof(University));
                serializer.Serialize(writer, university);

                // json

                var jsonString = JsonSerializer.Serialize(university);
                File.WriteAllText($"{outputPath}.json", jsonString);
                Console.ReadKey();

            }
            catch (FileNotFoundException e)
            {
                
                
            }


        }

    }
}
