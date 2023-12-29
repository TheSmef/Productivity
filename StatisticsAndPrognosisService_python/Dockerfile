FROM python:3.11
WORKDIR /statistic_service
COPY packages.txt .
COPY . .
RUN pip install --upgrade pip && pip install -r packages.txt
CMD ["python","-m","main"]