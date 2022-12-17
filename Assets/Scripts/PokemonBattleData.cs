using System;
using System.Collections.Generic;

[Serializable]
public class BattleStep
{
    public int atk_from;
    public int hurt;
    public List<int> health_list;
}

[Serializable]
public class PokemonBattleData
{
    public PokemonData player;
    public PokemonData enemy;

    public int first_atk;
    public List<BattleStep> battle_steps;
}