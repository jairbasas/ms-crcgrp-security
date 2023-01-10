
namespace Security.Application.Queries.Generics
{
    public interface IValuesSettings 
    {
        string GetTimeZone();
        string GetEncryptKey();
        string GetEncryptIv();
    }

    public class ValuesSettings : IValuesSettings
    {
        private readonly string _timeZone;
        private readonly string _encryptKey;
        private readonly string _encryptIv;

        public ValuesSettings(string timeZone, string encryptKey, string encryptIv)
        {
            _timeZone = timeZone;
            _encryptKey = encryptKey;
            _encryptIv = encryptIv;
        }

        public string GetEncryptIv()
        {
            return _encryptIv;
        }

        public string GetEncryptKey()
        {
            return _encryptKey;
        }

        public string GetTimeZone()
        {
            return this._timeZone;
        }
    }
}
