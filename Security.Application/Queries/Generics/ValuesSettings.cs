
namespace Security.Application.Queries.Generics
{
    public interface IValuesSettings 
    {
        string GetTimeZone();
    }

    public class ValuesSettings : IValuesSettings
    {
        private readonly string _timeZone;
        public ValuesSettings(string timeZone)
        {
            _timeZone = timeZone;
        }
        public string GetTimeZone()
        {
            return this._timeZone;
        }
    }
}
