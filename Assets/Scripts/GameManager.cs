using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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