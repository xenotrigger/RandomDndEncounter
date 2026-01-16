using static System.Console;
using System;
using System.Text.Json;

namespace RandomDndEncounter
{
    class Program()
    {
        static void Main()
        {

            const string JsonFileName = "srd_5e_monsters.json";

            if (!File.Exists(JsonFileName))
            {
                WriteLine($"Missing file: {JsonFileName}");
                WriteLine("Place Correct Json File In Project Folder!");
                return;
            }

            string json = File.ReadAllText(JsonFileName);

            using JsonDocument doc = JsonDocument.Parse(json);

            JsonElement monsters = doc.RootElement;

            int count = monsters.GetArrayLength();
            if (count == 0)
            {
                WriteLine("No Monsters Found!");
                return;
            }

            Random rng = new Random();

            while(true)
            {
            JsonElement monster = monsters[rng.Next(count)];

            string name = monster.TryGetProperty("name", out JsonElement nameEl)
                ? nameEl.GetString() ?? "(No Name)" : "(No Name)";
                
            string hp = monster.TryGetProperty("Hit Points", out JsonElement hpEl)
                ? hpEl.GetString() ?? "(No HP)" : "(No HP)";

            string cr = monster.TryGetProperty("Challenge", out JsonElement crEl)
                ? crEl.GetString() ?? "(No Challenge Rating)" : "(No Challenge Rating)";
            

            WriteLine("Random DnD Encounter");
            WriteLine("********************");
            WriteLine($"Name: {name}");
            WriteLine($"HP: {hp}");
            WriteLine($"CR: {cr}");

            WriteLine("Would You Like Another Encounter?");
            WriteLine("(Y/N)");
            string? input = ReadLine();

            if (!string.Equals(input, "Y", StringComparison.OrdinalIgnoreCase))
                break;

            }

        }
    }   
} 