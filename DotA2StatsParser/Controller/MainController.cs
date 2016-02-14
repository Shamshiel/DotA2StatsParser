using DotA2StatsParser.Controller;
using DotA2StatsParser.Controller.Dotabuff;
using DotA2StatsParser.Controller.Yasp;
using DotA2StatsParser.Model.Dotabuff;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DotA2StatsParser.Controller
{
    internal partial class MainController : IDisposable
    {
        internal MainController(DotabuffMappingController dotabuffMappingController, YaspMappingController yaspMappingController)
        {
            this.DotabuffMappingController = dotabuffMappingController;
            this.YaspMappingController = yaspMappingController;

            this.HtmlDocumentController = new HtmlDocumentController(this);
            this.QueryStringController = new QueryStringController(this);

            this.HeroController = new HeroController(this);
            this.ItemController = new ItemController(this);
            this.PlayerController = new PlayerController(this);

            this.WordCloudController = new WordCloudController(this);
        }

        /// <summary>
        /// Converts a string to an instance of <see cref="Kda"/>.
        /// </summary>
        /// <param name="kda">The kda as a string representation.</param>
        /// <returns>An instance of Kda</returns>
        internal Kda ConvertStringToKda(string kda)
        {
            string[] kdaArray = kda.Split('/');
            return new Kda(kdaArray[0], kdaArray[1], kdaArray[2]);
        }

        /// <summary>
        /// Maps a string to an enum value. The input string will be stripped of whitespaces and the character '-'.
        /// </summary>
        /// <typeparam name="T">The enum that should be mapped</typeparam>
        /// <param name="enumString">The enum as a string representation.</param>
        /// <returns>The mapped enum or the enum as undefined</returns>
        internal T MapStringToEnum<T>(string enumString) where T : struct, IConvertible
        {
            foreach (T m in Enum.GetValues(typeof(T)))
            {
                if (m.ToString().Trim().ToLower() == enumString.Replace(" ", "").Replace("-", "").Trim().ToLower())
                    return m;
            }

            return (T)Enum.Parse(typeof(T), "Undefined");
        }

        /// <summary>
        /// Combines a path with the current list count.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="currentListCount">The current list count.</param>
        /// <returns>The combined path and list cout</returns>
        internal string CombinePathWithListCount(string path, int currentListCount)
        {
            return string.Format(path, currentListCount);
        }

        /// <summary>
        /// Determines whether [the specified string] [is digits only].
        /// </summary>
        /// <param name="value">The string that should be cheched.</param>
        /// <returns>If the string had only digits in it</returns>
        internal bool IsDigitsOnly(string value)
        {
            return value.All(c => c >= '0' && c <= '9');
        }

        /// <summary>
        /// Removes all characters.
        /// </summary>
        /// <param name="value">The string that needs to be stripped of all characters.</param>
        /// <returns>The input value without characters</returns>
        internal string RemoveAllCharacters(string value)
        {
            return new string(value.Where(c => (char.IsDigit(c))).ToArray());
        }

        /// <summary>
        /// Converts a string (hh:mm:ss or mm:ss) to an instance of <see cref="TimeSpan"/>.
        /// </summary>
        /// <param name="duration">The duration as a string.</param>
        /// <returns>The input value as an instance of TimeSpan</returns>
        internal TimeSpan ConvertStringToTimeSpan(string duration)
        {
            duration = duration.Replace(":", "");
            if (duration.Length < MainController.TIMESPAN_FORMAT.Length)
            {
                for (int i = 0; i <= MainController.TIMESPAN_FORMAT.Length - duration.Length; i++)
                {
                    duration = "0" + duration;
                }
            }

            TimeSpan durationTimeSpan = new TimeSpan();
            TimeSpan.TryParseExact(duration, MainController.TIMESPAN_FORMAT, CultureInfo.CurrentCulture, out durationTimeSpan);
            return durationTimeSpan;
        }

        /// <summary>
        /// Converts a string to an instance of <see cref="WinRate"/>.
        /// </summary>
        /// <param name="winRate">The win rate as a string.</param>
        /// <returns>The input as an instance of WinRate</returns>
        internal WinRate ConvertStringToWinRate(string winRate)
        {
            return new WinRate(float.Parse(winRate.Replace(".", ",").Replace("%", "")));
        }

        /// <summary>
        /// Converts a string to an instance of <see cref="Popularity"/>.
        /// </summary>
        /// <param name="popularity">The popularity as a string.</param>
        /// <returns>The input as an instance of Popularity</returns>
        internal Popularity ConvertStringToPopularity(string popularity)
        {
            return new Popularity(int.Parse(RemoveAllCharacters(popularity)));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            this.HtmlDocumentController = null;
            this.HeroController = null;
            this.ItemController = null;
            this.PlayerController = null;
        }
    }
}
