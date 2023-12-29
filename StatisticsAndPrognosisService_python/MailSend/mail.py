import smtplib, ssl
from email.mime.text import MIMEText
from email.mime.multipart import MIMEMultipart
from Prognosis.prognose_income import Prognose
from Prognosis.prognose_income_reverse import Prognose_Reverse
import strings


class MailSender:
    def __init__(self, receiver):
        self.receiver = receiver

    def send_report_classic(self, year, area, plant, prolificy_model, stock_price=170,
                            planting_price=39100):
        message = MIMEMultipart("alternative")
        message["Subject"] = "Отчёт по урожайности"
        message["From"] = strings.smtp_mail
        message["To"] = self.receiver
        prognosis_income = Prognose(prolificy=prolificy_model, planting_price=planting_price, stock_price=stock_price,
                                    plant=plant)
        html = f"""\
        <html>
          <body>
            <p>
            <br>
               <p>
                На вход было получено: Культура: {plant}, Регион: {area}, Год: {year}.
               </p>
                <p>
                Выходные данные: Урожайность культуры {plant} в регионе  в {year} году потенциально 
                будет составлять {round(prolificy_model, 2)} центнеров на гектар.
                </p>
                 <p>
                Учитывая рост себестоимости, себестоимость реализации продукта на гектаре территории 
                будет составлять {planting_price} рублей.
                </p>
                </p>
                {prognosis_income.return_prognose()}
            </p>
          </body>
        </html>
        """
        text_to_send = MIMEText(html, "html")
        message.attach(text_to_send)
        context = ssl.create_default_context()

        with smtplib.SMTP_SSL("smtp.gmail.com", 465, context=context) as server:
            server.login(strings.smtp_mail, strings.smtp_pass)
            server.sendmail(
                strings.smtp_mail, self.receiver, message.as_string()
            )

    def send_report_reverse(self, year, area, desired_profit, best_plants, stock_planting_price
                            ):
        message = MIMEMultipart("alternative")
        message["Subject"] = "Отчёт по урожайности"
        message["From"] = strings.smtp_mail
        message["To"] = self.receiver
        prognosis = Prognose()
        prognosis_reverse_first = Prognose_Reverse()
        prognosis_reverse_second = Prognose_Reverse()
        list_keys = prognosis.return_cultures(best_plants)
        list_values = prognosis.return_prolificies(best_plants)
        prognosis_reverse_first.plant, prognosis_reverse_second.plant = list_keys[0], list_keys[1]
        prognosis_reverse_first.prolificy, prognosis_reverse_second.prolificy = list_values[0], list_values[1]
        prognosis_reverse_first.stock_price, prognosis_reverse_second.stock_price = prognosis_reverse_first.return_prices(
            stock_planting_price, f"{list_keys[0]} stock price"), prognosis_reverse_second.return_prices(
            stock_planting_price,
            f"{list_keys[1]} stock price")
        prognosis_reverse_first.planting_price, prognosis_reverse_second.planting_price = prognosis_reverse_first.return_prices(
            stock_planting_price, f"{list_keys[0]} planting price"), prognosis_reverse_second.return_prices(
            stock_planting_price,
            f"{list_keys[1]} planting price")
        html = f"""\
        <html>
          <body>
            <p>
            <br>
               <p>
                На вход было получено: Год: {year}, Регион: {area}, Прибыль с гектара: {desired_profit}
               </p>
                <p>
               Учитывая прибыль и урожайность в регионе наиболее оптимальным выбором будет: {list_keys[0]} и {list_keys[1]}
                </p>                
                <p>
                Урожайность культуры {list_keys[0]} в регионе {area} в {year} году  потенциально будет составлять {round(list_values[0], 2)} центнеров на гектар.
                </p>
                 <p>
                Учитывая рост себестоимости культуры {list_keys[0]}, себестоимость реализации продукта на гектаре территории будет составлять {prognosis_reverse_first.planting_price} рублей.
                </p>
                <p>
                Урожайность культуры {list_keys[1]} в регионе {area} в {year} году  потенциально будет составлять {round(list_values[1], 2)} центнеров на гектар.
                </p>
                 <p>
                Учитывая рост себестоимости культуры {list_keys[1]}, себестоимость реализации продукта на гектаре территории будет составлять {prognosis_reverse_second.planting_price} рублей.
                </p>
                <p>
                {prognosis_reverse_first.check_profit(desired_profit=desired_profit)}
                </p>
                <p>
                {prognosis_reverse_second.check_profit(desired_profit=desired_profit)}
                </p>
            </p>
          </body>
        </html>
        """
        text_to_send = MIMEText(html, "html")
        message.attach(text_to_send)
        context = ssl.create_default_context()

        with smtplib.SMTP_SSL("smtp.gmail.com", 465, context=context) as server:
            server.login(strings.smtp_mail, strings.smtp_pass)
            server.sendmail(
                strings.smtp_mail, self.receiver, message.as_string()
            )

    def send_report_fail(self):
        message = MIMEMultipart("alternative")
        message["Subject"] = "Отчет по урожайности"
        message["From"] = strings.smtp_mail
        message["To"] = self.receiver
        html = f"""\
                <html>
                  <body>
                    <p>
                    <br>
                    <p>
                       К сожалению для вашей задачи в наших базах данных недостаточно данных. 
                       Мы приносим глубочайшие извинения.
                    </p>
                  </body>
                </html>
                """
        text_to_send = MIMEText(html, "html")
        message.attach(text_to_send)
        context = ssl.create_default_context()

        with smtplib.SMTP_SSL("smtp.gmail.com", 465, context=context) as server:
            server.login(strings.smtp_mail, strings.smtp_pass)
            server.sendmail(
                strings.smtp_mail, self.receiver, message.as_string()
            )
