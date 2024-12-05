using System.Net;

namespace AdventOfCode.Solutions;

public static class InputProvider
{
    private static readonly HttpClientHandler _handler = new()
    {
        CookieContainer = GetCookieContainer(),
        UseCookies = true,
    };

    private static readonly HttpClient _client = new(_handler)
    {
        BaseAddress = new Uri("https://adventofcode.com/"),
    };

    public static async Task<string> Fetch(int year, int day)
    {
        if (Settings.CurrentTime < new DateTime(year, 12, day))
        {
            throw new InvalidOperationException("Too early to get puzzle input.");
        }

        HttpResponseMessage response = await _client.GetAsync($"{year}/day/{day}/input");

        return await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
    }

    private static CookieContainer GetCookieContainer()
    {
        CookieContainer container = new();

        container.Add(new Cookie
        {
            Name = "session",
            Domain = ".adventofcode.com",
            Value = Settings.GetInstance().Cookie.Replace("session=", ""),
        });

        return container;
    }
}