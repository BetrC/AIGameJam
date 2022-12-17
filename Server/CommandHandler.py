from PokemonManager import *
import json
import os

SUCCESS_MSG = "success"
ERROR_MSG_ARGS_WRONG = "wrong arg!"
ERROR_MSG_PATH_WRONG = "wrong path!"
ERROR_MSG_CANNOT_FIND_IMG = "cannot find image!"


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
        pokemon_info = random_pokemon()
        return json.dumps(pokemon_info).encode('utf-8')

    def on_request_battle(self, param):
        if not 'pokemon_id' in param:
            return ERROR_MSG_ARGS_WRONG.encode('utf-8')
        if not 'user_id' in param:
            return ERROR_MSG_ARGS_WRONG.encode('utf-8')
        return SUCCESS_MSG.encode('utf-8')

    def on_check_battle_info(self, param):
        if not 'user_id' in param:
            return ERROR_MSG_ARGS_WRONG.encode('utf-8')
        battle_info = {}
        battle_info['player'] = get_pokemon_info_by_id(0)
        battle_info['enemy'] = get_pokemon_info_by_id(0)
        battle_info['first_atk'] = 0
        battle_info['battle_steps'] = simulate_battle_steps(0, 0)
        return json.dumps(battle_info).encode('utf-8')

    def on_get_pokemon_img(self, param):
        if not 'pokemon_id' in param:
            return ERROR_MSG_ARGS_WRONG.encode('utf-8')
        img_name = "pokemon_img_"+str(param["pokemon_id"])
        img_name = "pokemon_img_test.png"
        file_path = "Images/" + img_name
        if not os.path.exists(file_path):
            return ERROR_MSG_CANNOT_FIND_IMG.encode('utf-8')
        with open(file_path, 'rb') as file_handle:
            return file_handle.read()
