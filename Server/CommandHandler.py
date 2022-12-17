from PokemonManager import *
from BattleManager import *
import json
import os

SUCCESS_MSG = "success"
ERROR_MSG_ARGS_WRONG = "wrong arg!"
ERROR_MSG_PATH_WRONG = "wrong path!"
ERROR_MSG_CANNOT_FIND_IMG = "cannot find image!"
ERROR_MSG_BATTLE_EXIST = "battle already registered!"
ERROR_MSG_BATTLE_NOT_START = "battle not start yet!"


class CommandHandler():
    def on_Command(self, path, params):
        if path == '/request_pokemon':
            return self.on_request_pokemon(params)
        elif path == '/request_battle':
            return self.on_request_battle(params)
        elif path == '/check_battle_info':
            return self.on_check_battle_info(params)
        elif path == '/get_pokemon_img':
            return self.on_get_pokemon_img(params)
        else:
            return ERROR_MSG_PATH_WRONG.encode('utf-8')

    def on_request_pokemon(self, param):
        pokemon_info = pokemonManager.random_pokemon()
        return json.dumps(pokemon_info).encode('utf-8')

    def on_request_battle(self, param):
        if not 'pokemon_id' in param:
            return ERROR_MSG_ARGS_WRONG.encode('utf-8')
        if not 'user_id' in param:
            return ERROR_MSG_ARGS_WRONG.encode('utf-8')
        userId = int(param['user_id'][0])
        pokemonId = int(param['pokemon_id'][0])
        if battleManager.is_battle_list_contains(userId, pokemonId):
            return ERROR_MSG_BATTLE_EXIST.encode('utf-8')
        battleManager.add_into_battle_list(userId, pokemonId)
        return SUCCESS_MSG.encode('utf-8')

    def on_get_pokemon_img(self, param):
        if not 'pokemon_id' in param:
            return ERROR_MSG_ARGS_WRONG.encode('utf-8')
        img_name = "pokemon_" + str(param["pokemon_id"][0])
        file_path = "PokemonData/imgs/" + img_name + ".png"
        if not os.path.exists(file_path):
            return ERROR_MSG_CANNOT_FIND_IMG.encode('utf-8')
        with open(file_path, 'rb') as file_handle:
            return file_handle.read()

    def on_check_battle_info(self, param):
        if not 'user_id' in param:
            return ERROR_MSG_ARGS_WRONG.encode('utf-8')
        userId = int(param['user_id'][0])
        simplyInfo = battleManager.check_battle_info_by_user_id(userId)
        if simplyInfo is None:
            return ERROR_MSG_BATTLE_NOT_START.encode('utf-8')
        battle_info = {'player': pokemonManager.get_pokemon_info_by_id(simplyInfo[0]),
                       'enemy': pokemonManager.get_pokemon_info_by_id(simplyInfo[1]),
                       'first_atk': simplyInfo[2],
                       'battle_steps': pokemonManager.simulate_battle_steps(simplyInfo[0], simplyInfo[1], simplyInfo[2])}
        return json.dumps(battle_info).encode('utf-8')
