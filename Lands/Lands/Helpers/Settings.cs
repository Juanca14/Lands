namespace Lands.Helpers
{
    using Plugin.Settings;
    using Plugin.Settings.Abstractions;

    public class Settings
    {

        const string token = "Token";
        const string tokenType = "TokenType";
        static readonly string stringDefault = string.Empty;

        static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        public static string Token
        {
            get
            {
                return AppSettings.GetValueOrDefault(token, stringDefault);
            }

            set
            {
                AppSettings.AddOrUpdateValue(token, value);
            }
        }

        public static  string TokenType
        {
            get
            {
                return AppSettings.GetValueOrDefault(TokenType, stringDefault);
            }

            set
            {
                AppSettings.AddOrUpdateValue(tokenType, value);
            }
        }
    }
}
