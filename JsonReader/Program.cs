using System;
using System.Collections.Generic;
using System.IO;
using JsonReader.Countries;
using Newtonsoft.Json;
using System.Linq;

namespace JsonReader
{
    public class Program
    {
        public static void Main()
        {
            var reader = new StreamReader("..\\..\\..\\countriesFormatted.json");

            var inputJson = reader.ReadToEnd();

            var countries = JsonConvert.DeserializeObject<List<Country>>(inputJson);

            if (countries != null)
            {
                var americaPopulation = countries
                    .Sum(c => c.Population);

                Console.WriteLine($"Население Америки: {americaPopulation:0,0} человек");

                var americanCurrencies = countries
                    .SelectMany(country => country.Currencies)
                    .Select(c => c.Name)
                    .Distinct()
                    .ToList();

                Console.WriteLine("Валюты Америки: {0}", string.Join(", ", americanCurrencies));

                var outputJson = JsonConvert.SerializeObject(countries);

                using var writer = new StreamWriter("output.json");

                writer.Write(outputJson);
            }
        }
    }
}