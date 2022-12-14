using ProfileSharp.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication1
{
    [EnableProfileSharp]
    public interface IWeatherRepository
    {
        WeatherForecast Get();
        IEnumerable<WeatherForecast> GetRange(int days, object temp = null);
    }

    public class WeatherRepository : IWeatherRepository
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public WeatherForecast Get()
        {
            var rng = new Random();

            return new WeatherForecast
            {
                Date = DateTime.Now,
                TemperatureC = 25,
                Summary = Summaries[0]
            };
        }

        public IEnumerable<WeatherForecast> GetRange(int days, object temp = null)
        {
            if (days > 7)
            {
                throw new ArgumentException("Days cannot be greater than 7");
            }

            var rng = new Random();
            return Enumerable.Range(1, days).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = 25,
                Summary = Summaries[index]
            })
                .ToArray();
        }
    }
}
