import random

class PokemonManager():
    def LoadData(self):
        self.pokemon_list = []
        with open('PokemonData/pokemon_data.txt', 'r') as config_file:
            count = int(config_file.readline())
            for index in range(0, count):
                pokemonInfo = {}
                # split id first
                line = config_file.readline()
                parts = line.split(":", 1)
                key = parts[0]
                value = parts[1]
                pokemonInfo[key] = value[:-1].strip()
                # split others
                for infoId in range(0, 7):
                    line = config_file.readline()
                    parts = line.split(" - ", 1)
                    key = parts[0]
                    value = parts[1]
                    pokemonInfo[key] = value[:-1].strip()
                self.pokemon_list.append(pokemonInfo)
        print(self.pokemon_list)
        # for x in range(1, count):
        #     for y in range(1, count):
        #         self.simulate_battle_steps(x, y)
        print("loadDataFinished!")

    def random_pokemon(self):
        return self.pokemon_list[random.randint(0, len(self.pokemon_list) - 1)]

    def random_pokemon_id(self):
        return random.randint(0, len(self.pokemon_list) - 1)

    def get_pokemon_info_by_id(self, pokemonId):
        if pokemonId < 0 or pokemonId > len(self.pokemon_list) - 1:
            return None
        return self.pokemon_list[pokemonId]

    def simulate_battle_steps(self, pokemonId1, pokemonId2, first_atk):
        if pokemonId1 < 0 or pokemonId1 > len(self.pokemon_list) - 1:
            return None
        if pokemonId2 < 0 or pokemonId2 > len(self.pokemon_list) - 1:
            return None
        pokemon1 = self.get_pokemon_info_by_id(pokemonId1)
        pokemon2 = self.get_pokemon_info_by_id(pokemonId2)
        pokemon1Info = [int(pokemon1["HP"]), int(pokemon1["Attack"]), int(pokemon1["Defense"])]
        pokemon2Info = [int(pokemon2["HP"]), int(pokemon2["Attack"]), int(pokemon2["Defense"])]
        pokemonInfoList = [pokemon1Info, pokemon2Info]
        steps = []
        attacking = 1 - first_atk
        minAtk = 1
        while pokemonInfoList[0][0] > 0 and pokemonInfoList[1][0] > 0:
            stepDic = {}
            stepDic['atk_from'] = attacking
            hurt = pokemonInfoList[attacking][1] - pokemonInfoList[attacking][2]
            if hurt < minAtk:
                hurt = minAtk
            stepDic['hurt'] = hurt
            health = pokemonInfoList[1-attacking][0]
            health -= hurt
            if health < 0:
                health = 0
            pokemonInfoList[1-attacking][0] = health
            stepDic['health_list'] = [pokemonInfoList[0][0], pokemonInfoList[1][0]]
            steps.append(stepDic)
            attacking = 1-attacking
        return steps


pokemonManager = PokemonManager()
