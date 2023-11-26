namespace Productivity.API.Data.Context.Constants
{
    public static class ContextConstants
    {
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

        public const string ParseError = "Ошибка при обработке строки запроса";
        public const string NotFoundError = "Запись не существует";
    }
}
