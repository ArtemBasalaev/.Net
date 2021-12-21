using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JsonFullReader.Countries;
using Newtonsoft.Json;

namespace JsonFullReader
{
    public class Program
    {
        public static void Main()
        {
            var path = "..\\..\\..\\countriesFormatted.json";

            if (!File.Exists(path))
            {
                Console.WriteLine("Указан неправильный путь");

                return;
            }

            var inputJson = File.ReadAllText(path);

            var countries = JsonConvert.DeserializeObject<List<Country>>(inputJson);

            if (countries != null)
            {
                var americaPopulation = countries.Sum(c => c.Population);

                Console.WriteLine($"Население Америки: {americaPopulation:0,0} человек");

                var americanCurrencies = countries
                    .SelectMany(country => country.Currencies)
                    .Select(c => c.Name)
                    .Distinct()
                    .ToList();

                Console.WriteLine("Валюты Америки: {0}", string.Join(", ", americanCurrencies));

                var outputJson = JsonConvert.SerializeObject(countries);

                File.WriteAllText("output.json", outputJson);
            }
        }
    }
}