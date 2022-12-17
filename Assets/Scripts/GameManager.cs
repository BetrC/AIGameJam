using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 游戏币
    public int Coin = 100;
    
    public List<PokemonData> pokemons = new List<PokemonData>();
    public static GameManager Instance;

    public PokemonBattleData BattleData;
    
    private void Start() 
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    public void AddPokemon(PokemonData pokemon)
    {
        pokemons.Add(pokemon);
    }
}