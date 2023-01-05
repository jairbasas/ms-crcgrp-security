
namespace Security.Application.Utility
{
    public static class CommonConstants
    {
        public const string TokenType = "Bearer";
        public const int Zero = 0;
        public const string ErrorInterno = "Hubo un problema al procesar la solicitud, intente nuevamente por favor.";
        public const string DefaultPassword = "123456";
        public const int LengthPassword = 10;
    }

    public struct StateEntity
    {
        public const int Active = 1;
        public const int Inactive = 2;
    }

    public struct ResetPasswordOption
    {
        public const int Yes = 1;
        public const int No = 0;
    }
}
