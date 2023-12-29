from Prognosis.prognose_income import Prognose


class Prognose_Reverse(Prognose):

    def check_profit(self, desired_profit):
        profit = round(self.prognose_profit(), 2)
        if profit <= 0:
            return f"Посадка культуры {self.plant} в регионе принесёт убытки: {round(self.prolificy * self.stock_price - self.planting_price, 2) * -1} руб с гектара."
        if desired_profit <= profit:
            return f"Посадка культуры {self.plant} в регионе принесёт прибыль {profit} руб с гектара."
        else:
            return f"К сожалению, максимальную ожидаюмую прибыль с посадки культуры {self.plant} в регионе принесёт прибыль {profit} руб с гектара, что меньше вашей ожидаемой прибыли"
