class Prognose:
    def __init__(self, prolificy="", planting_price="", stock_price="", plant=""):
        self.prolificy = prolificy
        self.planting_price = planting_price
        self.stock_price = stock_price
        self.plant = plant

    def prognose_income(self):
        return round(self.prolificy * self.stock_price, 2)

    def prognose_profit(self):
        return round(self.prolificy * self.stock_price, 2) - self.planting_price

    def return_cultures(self, dict_cultures):
        list = []
        for key in dict_cultures.items():
            list.append(key[0])
        return list

    def return_prices(self, dict_prices, plant):
        for key, value in dict_prices.items():
            if key == plant:
                return value

    def return_prolificies(self, dict_cultures):
        list = []
        for value in dict_cultures.items():
            list.append(value[1])
        return list

    def return_prognose(self):
        if round(self.prognose_profit(), 2) > 0:
            html = f"""\
                             <p>
                            Финальный заработок с реализации продукции 
                            будет {self.prognose_income()} руб.
                            </p>
                             <p>
                            Стоимость посадки: {self.planting_price} руб.
                            </p>
                            <p>
                            Вывод: Посадка культуры {self.plant} в регионе принесёт 
                            прибыль {round(self.prognose_profit(), 2)} руб 
                            с гектара.
                            </p>
                    """
            return html
        else:
            html = f"""\
                                        Вывод: Посадка культуры {self.plant} в регионе принесёт 
                                        убыток {round(self.prognose_profit(), 2) * -1} руб 
                                        с гектара.
                                        </p>
                                """
            return html
