namespace Productivity.Shared.Utility.Constants
{
    public static class ContextConstants
    {
        public const string ExcelContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        public const string ProductivityUNIndex = "UN_Productivity";
        public const string AccountEmailUNIndex = "UN_Email_Account";
        public const string AccountLoginUNIndex = "UN_Login_Account";
        public const string TokenUNIndex = "UN_Token";
        public const string CultureUNIndex = "UN_Culture";
        public const string RegionUNIndex = "UN_Region";

        public const string AccountUNEmailError = "Данная электронная почта уже занята";
        public const string AccountUNLoginError = "Данный логин уже занят";
        public const string CultureUNError = "Данное название культуры уже занято";
        public const string CultureNotFound = "Данная культура не существует";
        public const string RegionNotFound = "Данный регион не существует";
        public const string ProductivityUNError = "Запись данного региона, с данной культурой, за данный год уже существует";
        public const string RegionUNError = "Данное название региона уже занято";
        public const string TokenUNError = "Данный токен уже существует";


        public const string AccountUNEmailErrorCollection = "Данная электронная почта уже занята другим элементом в коллекции";
        public const string AccountUNLoginErrorCollection = "Данный логин уже занят другим элементом в коллекции";
        public const string CultureUNErrorCollection = "Данное название культуры уже занято другим элементом в коллекции";
        public const string ProductivityUNErrorCollection = "Запись данного региона, с данной культурой, за данный год уже существует в коллекции";
        public const string RegionUNErrorCollection = "Данное название региона уже занято другим элементом в коллекции";
        public const string TokenUNErrorCollection = "Данный токен уже существует в коллекции";


        public const string CannotBeDeleted = "Элемент не может быть удалён, так как имеет дочерние элементы";

        public const string ParseError = "Ошибка при обработке строки запроса";
        public const string ParseErrorFile = "Ошибка при обработке файла";
        public const string NotFoundError = "Запись не существует";
        public const string ShortageOfData = "Недостаточно записей для обработки";

        public const string NoConnectionToBroker = "Связь с брокером отсутствует";

        public const string ValidationErrorTitle = "Ошибка валидации";


        public const string MailQueue = "MailQueue";

        public const string ProductivityQueue = "ProductivityQueue";
        public const string ReportExchange = "ReportExchange";
        public const string ProductivityRoutingKey = "productivity";

        public const string CultureQueue = "CultureQueue";
        public const string CultureRoutingKey = "culture";
    }
}
