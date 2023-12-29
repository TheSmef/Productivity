import requests
import pandas as pd
import jsonpickle

import strings


class API_Reader:
    def __int__(self):
        pass

    def connect_to_api_classic(self, cultureid, regionid):
        try:
            url = f'https://{strings.api_server}/Productivity?Filter=CultureId ="{cultureid}" and RegionId ="{regionid}"'
            response_API = requests.get(url, verify=False)
            result = jsonpickle.decode(response_API.content)
        except ConnectionError:
            return pd.read_csv("data_set.csv", encoding="windows-1251")
        except ValueError:
            return pd.read_csv("data_set.csv", encoding="windows-1251")
        return pd.DataFrame.from_dict(pd.json_normalize(result["collection"]), orient='columns')

    def connect_to_api_reverse(self, regionid):
        try:
            url = f'https://{strings.api_server}/Productivity?Filter=RegionId ="{regionid}"'
            response_API = requests.get(url, verify=False)
            result = jsonpickle.decode(response_API.content)
        except ConnectionError:
            return pd.read_csv("data_set.csv", encoding="windows-1251")
        except ValueError:
            return pd.read_csv("data_set.csv", encoding="windows-1251")
        return pd.DataFrame.from_dict(pd.json_normalize(result["collection"]), orient='columns')

    def get_prices_and_plant(self, cultureid):
        try:
            url = f'https://{strings.api_server}/Culture/{cultureid}'
            response_API = requests.get(url, verify=False)
            result = jsonpickle.decode(response_API.content)
        # response_API = pd.read_csv("data_set.csv", encoding="windows-1251")
        except ConnectionError:
            return pd.read_csv("data_set.csv", encoding="windows-1251")
        except ValueError:
            return pd.read_csv("data_set.csv", encoding="windows-1251")
        return result["costToPlant"], result["priceToSell"], result["name"]

    def get_region(self, regionid):
        try:
            url = f'https://{strings.api_server}/Region/{regionid}'
            response_API = requests.get(url, verify=False)
            result = jsonpickle.decode(response_API.content)
        except ConnectionError:
            return pd.read_csv("data_set.csv", encoding="windows-1251")
        except ValueError:
            return pd.read_csv("data_set.csv", encoding="windows-1251")
        return result["name"]

    def get_plant(self, plantid):
        try:
            url = f'https://{strings.api_server}/Culture/{plantid}'
            response_API = requests.get(url, verify=False)
            result = jsonpickle.decode(response_API.content)
        # response_API = pd.read_csv("data_set.csv", encoding="windows-1251")
        except ConnectionError:
            return pd.read_csv("data_set.csv", encoding="windows-1251")
        except ValueError:
            return pd.read_csv("data_set.csv", encoding="windows-1251")
        return result["name"]
