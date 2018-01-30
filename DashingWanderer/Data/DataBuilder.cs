﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DashingWanderer.Data.Explorers;
using DashingWanderer.Data.Explorers.Items;
using DashingWanderer.Data.Explorers.Moves;
using DashingWanderer.Data.Explorers.Pokedex;
using DashingWanderer.Data.Explorers.Pokedex.Enums;
using Newtonsoft.Json;
using PortraitsAdder;
using RawDataExplorers.Data;
using RawDataExplorers.Data.Items;
using RawDataExplorers.Data.Pokemon;
using RawDataPMDExplorers.Data.Moves;

namespace DashingWanderer.Data
{
    public static class DataBuilder
    {
        public static List<PortraitEntity> ExplorersPortraits { get; private set; }
        public static List<ExplorersEnitity> ExplorersPokemon { get; private set; }
        public static List<ExplorersItem> ExplorersItems { get; private set; }
        public static List<ExplorersMove> ExplorersMoves { get; private set; }

        public static readonly string PortraitFolder = Path.Combine(Globals.AppPath, "Data", "Portraits");
        public static readonly string PokemonDataFolder = Path.Combine(Globals.AppPath, "Data", "Pokemon");
        public static readonly string ItemDataFolder = Path.Combine(Globals.AppPath, "Data", "Items");
        public static readonly string MoveDataFolder = Path.Combine(Globals.AppPath, "Data", "Moves");

        public static void GetExplorersData()
        {
            string currentFile = string.Empty;

            try
            {
                ExplorersItems = new List<ExplorersItem>();
                foreach (string file in Directory.GetFiles(ItemDataFolder, "*.json"))
                {
                    currentFile = file;
                    ExplorersItems.Add(JsonConvert.DeserializeObject<ExplorersItem>(File.ReadAllText(file)));
                }

                ExplorersMoves = new List<ExplorersMove>();
                foreach (string file in Directory.GetFiles(MoveDataFolder, "*.json"))
                {
                    currentFile = file;
                    ExplorersMoves.Add(JsonConvert.DeserializeObject<ExplorersMove>(File.ReadAllText(file)));
                }

                ExplorersPokemon = new List<ExplorersEnitity>();
                foreach (string file in Directory.GetFiles(PokemonDataFolder, "*.json"))
                {
                    currentFile = file;
                    ExplorersPokemon.Add(JsonConvert.DeserializeObject<ExplorersEnitity>(File.ReadAllText(file)));
                }

                ExplorersPortraits = new List<PortraitEntity>();
                foreach (string file in Directory.GetFiles(PortraitFolder, "*.json"))
                {
                    currentFile = file;
                    ExplorersPortraits.Add(JsonConvert.DeserializeObject<PortraitEntity>(File.ReadAllText(file)));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(currentFile);
                Console.WriteLine(e);
            }
        }

        public static void BuildExplorerMoveData()
        {
            foreach (string file in Directory.GetFiles(MoveDataFolder))
            {
                File.Delete(file);
            }

            foreach (string file in Directory.GetFiles(Path.Combine(Globals.AppPath, "RawMoveData"), "*.json"))
            {
                try
                {
                    RawMoveData rawMove = JsonConvert.DeserializeObject<RawMoveData>(File.ReadAllText(file));

                    if (rawMove == null)
                    {
                        Console.WriteLine($"{file} deserialized to null!");
                        continue;
                    }

                    ExplorersMove moveData = ExplorersMove.FromRawData(rawMove);

                    File.WriteAllText(Path.Combine(MoveDataFolder, $"{moveData.Id}.json"), JsonConvert.SerializeObject(moveData, Formatting.Indented));
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Failed on file {file}.");
                    Console.WriteLine(e);
                }
            }
        }

        public static void BuildExplorerItemData()
        {
            foreach (string file in Directory.GetFiles(ItemDataFolder))
            {
                File.Delete(file);
            }

            foreach (string file in Directory.GetFiles(Path.Combine(Globals.AppPath, "RawItemData"), "*.json"))
            {
                try
                {
                    Item rawItem = JsonConvert.DeserializeObject<RawItemData>(File.ReadAllText(file)).Item;

                    if (rawItem == null)
                    {
                        Console.WriteLine($"{file} is null!");
                        continue;
                    }

                    ExplorersItem itemData = ExplorersItem.FromRawData(rawItem);

                    itemData.LongDescription = Regex.Replace(itemData.LongDescription, @"[\s]+Select detail:.+", string.Empty);
                    
                    File.WriteAllText(Path.Combine(ItemDataFolder, $"{itemData.Id}.json"), JsonConvert.SerializeObject(itemData, Formatting.Indented));
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Failed on file {file}.");
                    Console.WriteLine(e);
                }
            }
        }

        public static void BuildExplorerPokemonData()
        {
            List<ExplorersEnitity> pokemonDatas = new List<ExplorersEnitity>();
            List<ExplorersEnitity> evoIndexWaiting = new List<ExplorersEnitity>();
            List<string> duplicatePokes = new List<string>();

            foreach (string file in Directory.GetFiles(PokemonDataFolder))
            {
                File.Delete(file);
            }

            foreach (string file in Directory.GetFiles(Path.Combine(Globals.AppPath, "RawPokemonData"), "*.json"))
            {
                try
                {
                    ExplorersEnitity pokemonData = ExplorersEnitity.FromRawData(JsonConvert.DeserializeObject<Pokemon>(File.ReadAllText(file)));

                    pokemonData.RawIndex = Convert.ToInt32(Regex.Match(Path.GetFileNameWithoutExtension(file), @"[0-9]+").Value.TrimStart('0'));

                    pokemonData.Id = new string(pokemonData.Name.Where(e => !Path.GetInvalidPathChars().Contains(e)).ToArray()).ToLower();

                    if (pokemonData.Category.ToCharArray().Distinct().Count() == 1 && pokemonData.Category.ToCharArray().Distinct().First() == '?')
                    {
                        Console.WriteLine($"Skipped special npc entity {pokemonData.Name}");
                        continue;
                    }

                    if (pokemonData.DexId == 0)
                    {
                        continue;
                    }

                    bool preEvoFound = false;

                    foreach (GenderEntity pokemonDataGenderEnitity in pokemonData.GenderEnitities)
                    {
                        switch (pokemonDataGenderEnitity.Evolution.Method)
                        {
                            case EvoEnum.EvolutionMethod.CannotEvolve:
                                if (pokemonDataGenderEnitity.Evolution.PreEvoDexId > 0)
                                {
                                    Console.WriteLine($"{pokemonData.Name} probably has a special evolution.");
                                }
                                else
                                {
                                    preEvoFound = true;
                                    continue;
                                }
                                break;
                        }

                        ExplorersEnitity preEvoData = pokemonDatas.FirstOrDefault(e => e.RawIndex == (pokemonDataGenderEnitity.Evolution.PreEvoDexId > 600 ? pokemonDataGenderEnitity.Evolution.PreEvoDexId - 600 : pokemonDataGenderEnitity.Evolution.PreEvoDexId));

                        if (preEvoData == null)
                        {
                            evoIndexWaiting.Add(pokemonData);
                            preEvoFound = false;
                            break;
                        }

                        pokemonDataGenderEnitity.Evolution.PreEvoDexId = preEvoData.DexId;

                        preEvoFound = true;
                    }

                    if (!preEvoFound)
                    {
                        continue;
                    }

                    if (pokemonDatas.Any(e => e.DexId == pokemonData.DexId))
                    {
                        ExplorersEnitity similarEntity = pokemonDatas.Find(e => e.DexId == pokemonData.DexId);

                        if (!similarEntity.Id.Contains("-base"))
                        {
                            File.Move(Path.Combine(PokemonDataFolder, $"{similarEntity.Id}.json"), Path.Combine(PokemonDataFolder, $"{similarEntity.Id}-base.json"));

                            similarEntity.Id += "-base";

                            duplicatePokes.Add(similarEntity.Id);
                        }

                        if (pokemonData.PrimaryType == similarEntity.PrimaryType)
                        {
                            if (pokemonData.SecondaryType == similarEntity.SecondaryType)
                            {
                                if (pokemonData.GenderEnitities.Select(e => e.Gender).SequenceEqual(similarEntity.GenderEnitities.Select(e => e.Gender)))
                                {
                                    pokemonData.Id += $"-{pokemonData.RawIndex}";
                                }
                                else
                                {
                                    pokemonData.Id = $"-{pokemonData.GenderEnitities.First().Gender}";
                                }
                            }
                            else
                            {
                                pokemonData.Id += $"-{pokemonData.SecondaryType}";
                            }
                        }
                        else
                        {
                            pokemonData.Id += $"-{pokemonData.PrimaryType}";
                        }

                        pokemonData.Id = pokemonData.Id.ToLower();

                        duplicatePokes.Add(pokemonData.Id);
                    }

                    pokemonDatas.Add(pokemonData);

                    File.WriteAllText(Path.Combine(PokemonDataFolder, $"{pokemonData.Id}.json"), JsonConvert.SerializeObject(pokemonData, Formatting.Indented));
                }
                catch (Exception e)
                {
                    if (e.ToString().Contains("DashingWanderer.Data.Pokedex.RawData.Learn[]"))
                    {
                        File.WriteAllText(file, Regex.Replace(File.ReadAllText(file), @"""Learn"": (?<class>\{[^}]+\})", match => $"\"Learn\": [{match.Groups["class"].Captures[0].Value}]", RegexOptions.Singleline));

                        Console.WriteLine($"Failed on file {file}.\n{e}\nAttempted to implement Learn fix.");

                        continue;
                    }

                    Console.WriteLine($"Failed on file {file}.\n{e}");
                }
            }

            foreach (ExplorersEnitity pokemonData in evoIndexWaiting)
            {
                bool preEvoFound = false;

                foreach (GenderEntity pokemonDataGenderEnitity in pokemonData.GenderEnitities)
                {
                    if (pokemonDataGenderEnitity.Evolution.Method == EvoEnum.EvolutionMethod.CannotEvolve)
                    {
                        if (pokemonDataGenderEnitity.Evolution.PreEvoDexId > 0)
                        {
                            Console.WriteLine($"{pokemonData.Name} probably has a special evolution.");
                        }
                        else
                        {
                            preEvoFound = true;
                            continue;
                        }
                    }

                    ExplorersEnitity preEvoData = pokemonDatas.FirstOrDefault(e => e.RawIndex == (pokemonDataGenderEnitity.Evolution.PreEvoDexId > 600 ? pokemonDataGenderEnitity.Evolution.PreEvoDexId - 600 : pokemonDataGenderEnitity.Evolution.PreEvoDexId));

                    if (preEvoData == null)
                    {
                        Console.WriteLine($"Unable to find preEvo DexId for {pokemonData.Name} with preEvoIndex {pokemonDataGenderEnitity.Evolution.PreEvoDexId}!");
                        preEvoFound = false;
                        break;
                    }

                    pokemonDataGenderEnitity.Evolution.PreEvoDexId = preEvoData.RawIndex;

                    preEvoFound = true;
                }

                if (!preEvoFound)
                {
                    continue;
                }

                if (pokemonDatas.Any(e => e.DexId == pokemonData.DexId))
                {
                    ExplorersEnitity similarEntity = pokemonDatas.Find(e => e.DexId == pokemonData.DexId);

                    if (!similarEntity.Id.Contains("-base"))
                    {
                        File.Move(Path.Combine(PokemonDataFolder, $"{similarEntity.Id}.json"), Path.Combine(PokemonDataFolder, "Data", $"{similarEntity.Id}-base.json"));

                        similarEntity.Id += "-base";

                        duplicatePokes.Add(similarEntity.Id);
                    }

                    if (pokemonData.PrimaryType == similarEntity.PrimaryType)
                    {
                        if (pokemonData.SecondaryType == similarEntity.SecondaryType)
                        {
                            if (pokemonData.GenderEnitities.Select(e => e.Gender).SequenceEqual(similarEntity.GenderEnitities.Select(e => e.Gender)))
                            {
                                pokemonData.Id += $"-{pokemonData.RawIndex}";
                            }
                            else
                            {
                                pokemonData.Id = $"-{pokemonData.GenderEnitities.First().Gender}";
                            }
                        }
                        else
                        {
                            pokemonData.Id += $"-{pokemonData.SecondaryType}";
                        }
                    }
                    else
                    {
                        pokemonData.Id += $"-{pokemonData.PrimaryType}";
                    }

                    pokemonData.Id = pokemonData.Id.ToLower();

                    duplicatePokes.Add(pokemonData.Id);
                }
                else
                {
                    pokemonDatas.Add(pokemonData);
                }

                File.WriteAllText(Path.Combine(PokemonDataFolder, $"{pokemonData.Id}.json"), JsonConvert.SerializeObject(pokemonData, Formatting.Indented));
            }

            File.WriteAllLines(Path.Combine(PokemonDataFolder, "dupes.log"), duplicatePokes);
        }
    }
}