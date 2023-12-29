from Calculations.model import Model
from RabbitReader.rabbit_reader import RabbitReader


if __name__ == "__main__":
    md = Model()
    Rabbit_response = RabbitReader().receive_message()

