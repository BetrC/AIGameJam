import random


def simulate_battle_steps(pokemon0, pokemon1):
    battle_steps = ['start', 'step1', 'step2', 'step3', 'step4', 'end']
    return battle_steps


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
                pokemonInfo[key] = value[:-1]
                # split others
                for infoId in range(0, 7):
                    line = config_file.readline()
                    parts = line.split(" - ", 1)
                    key = parts[0]
                    value = parts[1]
                    pokemonInfo[key] = value[:-1]
                self.pokemon_list.append(pokemonInfo)
        print(self.pokemon_list)
        print("loadDataFinished!")

    def random_pokemon(self):
        return self.pokemon_list[random.randint(0, len(self.pokemon_list) - 1)]

    def get_pokemon_info_by_id(self, pokemonId):
        if pokemonId < 0 or pokemonId > len(self.pokemon_list) - 1:
            return {}
        return self.pokemon_list[pokemonId]


pokemonManager = PokemonManager()
