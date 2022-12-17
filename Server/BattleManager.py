import threading
import time
from threading import Timer

LOSING_TIME = 10


class BattleManager:

    def __init__(self):
        self.timer = Timer(0.5, self.__check_waiting_list, args=[""])
        self.timer1 = Timer(1, self.__check_losing_battle, args=[""])
        self.lock = threading.Lock()
        self.battleWaitingList = []
        self.battlingDic = {}

    def get_battle_list_count(self):
        return len(self.battleWaitingList)

    def is_battle_list_contains(self, userId, pokemonId):
        return self.battleWaitingList.__contains__((userId, pokemonId))

    def add_into_battle_list(self, userId, pokemonId):
        self.lock.acquire()
        self.battleWaitingList.append((userId, pokemonId))
        self.lock.release()

    def check_battle_info_by_user_id(self, userId):
        if not userId in self.battlingDic:
            return None
        return self.battlingDic[userId]

    def start_timer(self):
        self.timer.start()
        self.timer1.start()

    def __check_waiting_list(self, message):
        self.timer = Timer(0.5, self.__check_waiting_list, args=[""])
        self.timer.start()
        if len(self.battleWaitingList) < 2:
            return
        self.lock.acquire()
        player1 = self.battleWaitingList.pop()
        player2 = self.battleWaitingList.pop()
        self.battlingDic[player1[0]] = (player1[1], player2[1], 1, time.time())
        self.battlingDic[player2[0]] = (player2[1], player1[1], 0, time.time())
        self.lock.release()

    def __check_losing_battle(self, message):
        self.timer1 = Timer(1, self.__check_losing_battle, args=[""])
        self.timer1.start()
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


battleManager = BattleManager()
