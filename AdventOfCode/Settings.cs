using Microsoft.Extensions.Configuration;

namespace AdventOfCode;

internal class Settings
{
    private static readonly Lazy<Settings> _instance = new(() => new Settings());

    private static readonly string _configFilePath = "config.json";

    private Settings()
    {
        if (!File.Exists(_configFilePath))
        {
            return;
        }

        IConfiguration configuration = new ConfigurationBuilder().AddJsonFile(_configFilePath).Build();

        Year = int.TryParse(configuration["Year"], out int year) ? year : Year;
        Day = int.TryParse(configuration["Day"], out int day) ? day : Day;
        Cookie = configuration["Cookie"] ?? Cookie;
    }

    public static DateTime CurrentTime => TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));

    public int Year { get; private set; } = CurrentTime.Year;

    public int Day { get; private set; } = (CurrentTime.Month == 12 && CurrentTime.Day <= 25) ? CurrentTime.Day : 0;

    public string Cookie { get; private set; } = string.Empty;

    public static Settings GetInstance() => _instance.Value;
}
