﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DashingWanderer.Data.Explorers.Enums;
using DashingWanderer.Data.Explorers.Moves.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RawDataPMDExplorers.Data.Moves;

namespace DashingWanderer.Data.Explorers.Moves
{
    public class ExplorersMove
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int MoveId { get; set; }
        public string Description { get; set; }
        public CatergoryEnum.MoveCategory Category { get; set; }
        public int BasePower { get; set; }
        public TypeEnum.PokemonType Type { get; set; }
        public int BasePP { get; set; }
        public int Accuracy { get; set; }

        public static ExplorersMove FromRawData(RawMoveData rawMoveData)
        {
            ExplorersMove move = new ExplorersMove
            {
                Id = new string(rawMoveData.Move.Strings.English.Name.Replace(" ", "-").ToLower().Where(e => !Path.GetInvalidFileNameChars().Contains(e)).ToArray()),
                Name = rawMoveData.Move.Strings.English.Name,
                Description = rawMoveData.Move.Strings.English.Category
            };

            switch (rawMoveData.Move.Data.Length)
            {
                case 2:
                    if (Equals(rawMoveData.Move.Data[0], rawMoveData.Move.Data[1]))
                    {
                        move.MoveId = Convert.ToInt32(rawMoveData.Move.Data[0].MoveID);
                        move.Category = (CatergoryEnum.MoveCategory)Convert.ToInt32(rawMoveData.Move.Data[0].Category);
                        move.BasePower = Convert.ToInt32(rawMoveData.Move.Data[0].BasePower);
                        move.Type = (TypeEnum.PokemonType)Convert.ToInt32(rawMoveData.Move.Data[0].Type);
                        move.BasePP = Convert.ToInt32(rawMoveData.Move.Data[0].BasePP);
                        move.Accuracy = Convert.ToInt32(rawMoveData.Move.Data[0].Accuracy);
                    }
                    else
                    {
                        Console.WriteLine($"{rawMoveData.Move.Strings.English.Name} has differing move datas! Using main move data, but it is important to research this more.");
                        move.MoveId = Convert.ToInt32(rawMoveData.Move.Data[0].MoveID);
                        move.Category = (CatergoryEnum.MoveCategory)Convert.ToInt32(rawMoveData.Move.Data[0].Category);
                        move.BasePower = Convert.ToInt32(rawMoveData.Move.Data[0].BasePower);
                        move.Type = (TypeEnum.PokemonType)Convert.ToInt32(rawMoveData.Move.Data[0].Type);
                        move.BasePP = Convert.ToInt32(rawMoveData.Move.Data[0].BasePP);
                        move.Accuracy = Convert.ToInt32(rawMoveData.Move.Data[0].Accuracy);
                    }
                    break;
                case 1:
                    move.MoveId = Convert.ToInt32(rawMoveData.Move.Data[0].MoveID);
                    move.Category = (CatergoryEnum.MoveCategory)Convert.ToInt32(rawMoveData.Move.Data[0].Category);
                    move.BasePower = Convert.ToInt32(rawMoveData.Move.Data[0].BasePower);
                    move.Type = (TypeEnum.PokemonType)Convert.ToInt32(rawMoveData.Move.Data[0].Type);
                    move.BasePP = Convert.ToInt32(rawMoveData.Move.Data[0].BasePP);
                    move.Accuracy = Convert.ToInt32(rawMoveData.Move.Data[0].Accuracy);
                    break;
                default:
                    throw new NullReferenceException($"{rawMoveData.Move.Strings.English.Name} has invalid move data!");
            }

            return move;
        }
    }
}