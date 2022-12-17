import random
import threading
import time
from threading import Timer
from PokemonManager import *

LOSING_TIME = 10
ASSIGN_AI_TIME = 20


class BattleManager:

    def __init__(self):
        self.startBattleTimer = Timer(0.5, self.__check_waiting_list, args=[""])
        self.deleteBattleInfoTimer = Timer(1, self.__check_losing_battle, args=[""])
        self.assignAITimer = Timer(1, self.__check_assign_ai, args=[""])
        self.lock = threading.Lock()

        self.battleWaitingList = []
        self.battlingDic = {}
        self.waitingTimeDic = {}

    def get_battle_list_count(self):
        return len(self.battleWaitingList)

    def is_battle_list_contains(self, userId, pokemonId):
        return self.battleWaitingList.__contains__((userId, pokemonId))

    def add_into_battle_list(self, userId, pokemonId):
        self.lock.acquire()
        self.battleWaitingList.append((userId, pokemonId))
        self.waitingTimeDic[userId] = time.time()
        self.lock.release()

    def check_battle_info_by_user_id(self, userId):
        if not userId in self.battlingDic:
            return None
        return self.battlingDic[userId]

    def start_timer(self):
        self.startBattleTimer.start()
        self.deleteBattleInfoTimer.start()
        self.assignAITimer.start()

    def __check_waiting_list(self, message):
        self.startBattleTimer = Timer(0.5, self.__check_waiting_list, args=[""])
        self.startBattleTimer.start()
        if len(self.battleWaitingList) < 2:
            return
        self.lock.acquire()
        player1 = self.battleWaitingList.pop()
        player2 = self.battleWaitingList.pop()
        self.battlingDic[player1[0]] = (player1[1], player2[1], 1, time.time())
        self.battlingDic[player2[0]] = (player2[1], player1[1], 0, time.time())
        self.waitingTimeDic.pop(player1[0])
        self.waitingTimeDic.pop(player2[0])
        self.lock.release()

    def __check_losing_battle(self, message):
        self.deleteBattleInfoTimer = Timer(1, self.__check_losing_battle, args=[""])
        self.deleteBattleInfoTimer.start()
        self.lock.acquire()
        removingList = []
        for key in self.battlingDic.keys():
            startTime = self.battlingDic[key][3]
            if time.time() - startTime < LOSING_TIME:
                continue
            removingList.append(key)
        for key in removingList:
            self.battlingDic.pop(key)
        self.lock.release()

    def __check_assign_ai(self, message):
        self.assignAITimer = Timer(1, self.__check_assign_ai, args=[""])
        self.assignAITimer.start()
        removing_list = []
        for userId, pokemonId in self.battleWaitingList:
            startWaitingTime = self.waitingTimeDic[userId]
            if time.time() - startWaitingTime < ASSIGN_AI_TIME:
                continue
            removing_list.append((userId, pokemonId))
        self.lock.acquire()
        for userId, pokemonId in removing_list:
            player1 = (userId, pokemonId)
            player2 = ('AI', pokemonManager.random_pokemon_id())
            if random.randint(0, 1) == 0:
                temp = player1
                player1 = player2
                player2 = temp
            self.battlingDic[player1[0]] = (player1[1], player2[1], 1, time.time())
            self.battlingDic[player2[0]] = (player2[1], player1[1], 0, time.time())
            if self.waitingTimeDic.__contains__(player1[0]):
                self.waitingTimeDic.pop(player1[0])
            if self.waitingTimeDic.__contains__(player2[0]):
                self.waitingTimeDic.pop(player2[0])
            self.battleWaitingList.remove((userId, pokemonId))
        self.lock.release()


battleManager = BattleManager()
