using System;
using UnityEngine.Serialization;

[Serializable]
public class PokemonData
{
    [FormerlySerializedAs("id")] public int ID;
    [FormerlySerializedAs("name")] public string Monster;
    [FormerlySerializedAs("race")] public string Rarity;
    [FormerlySerializedAs("level")] public int Level;
    [FormerlySerializedAs("hP")] public int HP;
    [FormerlySerializedAs("atk")] public int Attack;
    [FormerlySerializedAs("defence")] public int Defense;
    [FormerlySerializedAs("desc")] public string Description;
}