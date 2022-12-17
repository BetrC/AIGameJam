using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoSingleton<GameManager>
{
    private int _coin = 1000;

    // 游戏币
    public int Coin
    {
        get { return _coin; }
        set
        {
            _coin = value;
            SaveData();
        }
    }

    protected override void Init()
    {
        base.Init();
        LoadData();
    }

    public int userId = 0;

    public List<PokemonData> pokemons = new List<PokemonData>();
    public PokemonData currentPokemon;
    public PokemonBattleData BattleData;

    public void AddPokemon(PokemonData pokemon)
    {
        pokemons.Add(pokemon);
        if (pokemons.Count > 10)
        {
            pokemons.RemoveAt(0);
        }

        SaveData();
    }

    public void SetPokemon(PokemonData p)
    {
        currentPokemon = p;
    }

    public void LoadData()
    {
        var save_path = Application.persistentDataPath + "/data.json";
        string text;
        try
        {
            text = File.ReadAllText(save_path);
        }
        catch (Exception e)
        {
            // 没有找到存档就用默认数据
            Coin = 1000;
            pokemons = new List<PokemonData>();
            userId = GenerateUniqueUserId();
            SaveData();
            return;
        }

        var file_save = JsonUtility.FromJson<FileSaveInfo>(text);
        Coin = file_save.money;
        pokemons = file_save.Pokemons;
        userId = file_save.user_id;
        if (file_save.currentPokemon.Monster == "")
            currentPokemon = null;
        else
            currentPokemon = file_save.currentPokemon;
    }

    public void SaveData()
    {
        var fileSaveInfo = new FileSaveInfo()
        {
            Pokemons = pokemons,
            user_id = userId,
            money = Coin,
            currentPokemon = currentPokemon,
        };
        var path = Application.persistentDataPath + "/data.json";
        var json = JsonUtility.ToJson(fileSaveInfo);
        File.WriteAllText(path, json);
    }

    private int GenerateUniqueUserId() => SystemInfo.deviceUniqueIdentifier.GetHashCode();
}

public class FileSaveInfo
{
    public List<PokemonData> Pokemons;
    public int user_id;
    public int money;
    public PokemonData currentPokemon;
}