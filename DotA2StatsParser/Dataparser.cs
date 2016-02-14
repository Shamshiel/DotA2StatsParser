using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DotA2StatsParser.Model.Dotabuff;
using DotA2StatsParser.Controller;
using DotA2StatsParser.Controller.Dotabuff;
using DotA2StatsParser.Model.HealthCheck;
using DotA2StatsParser.Model.Dotabuff.Interfaces;
using DotA2StatsParser.Model.HealthCheck.Interfaces;
using DotA2StatsParser.Model;
using DotA2StatsParser.Controller.Yasp;
using System.Text.RegularExpressions;
using DotA2StatsParser.Model.Yasp.Interfaces;
using DotA2StatsParser.Model.Yasp;
using DotA2StatsParser.Exceptions;


namespace DotA2StatsParser
{
    public class Dataparser : IDisposable
    {
        private readonly HealthCheck healthCheck;

        private readonly MainController mainController;

        /// <summary>
        /// Initializes a new instance of the Dataparser class.
        /// </summary>
        public Dataparser()
        {
            VersionController versionController = new VersionController();
            JsonController jsonController = new JsonController();

#if !DEBUG
            versionController.RefreshJsonPaths();
#endif

            DotabuffMappingController dotabuffMappingController = new DotabuffMappingController(versionController.GetJsonPaths(ParsingWebsites.Dotabuff));
            YaspMappingController yaspMappingController = new YaspMappingController(versionController.GetJsonPaths(ParsingWebsites.Yasp));

            mainController = new MainController(dotabuffMappingController, yaspMappingController);
            healthCheck = new HealthCheck(mainController);
        }

        /// <summary>
        /// Gathers all the data from the Dotabuff Item-Page and combines it into an Item object.
        /// </summary>
        /// <param name="itemEnum">The Item that you want to get</param>
        /// <returns>Will return the Item of your choice</returns>
        public IItem GetItemPageData(Items itemEnum)
        {
            if (itemEnum == Items.Undefined)
            {
                throw new Exception("You can't select an undefined Item!");
            }

            try
            {
                return mainController.ItemController.GetExtendedItem(itemEnum);
            }
            catch (Exception exception)
            {
                mainController.Dispose();

                throw new Dota2StatParserException("Something went wrong while parsing the item. See the innerexception for details!", exception);
            }
        }

        /// <summary>
        /// Gathers all the data from the Dotabuff Hero-Page and combines it into an Hero object.
        /// </summary>
        /// <param name="heroEnum">The Hero of your choice</param>
        /// <returns>Will return the Hero of your choice</returns>
        public IHero GetHeroPageData(Heroes heroEnum)
        {
            if (heroEnum == Heroes.Undefined)
            {
                throw new Exception("You can't select an undefined Hero!");
            }
            
            try
            {
                return mainController.HeroController.GetExtendedHero(heroEnum);
            }
            catch (Exception exception)
            {
                mainController.Dispose();

                throw new Dota2StatParserException("Something went wrong while parsing the hero. See the innerexception for details!", exception);
            }
        }

        /// <summary>
        /// Gathers all the data from the Dotabuff Player-Page and combines it into a Player object.
        /// </summary>
        /// <param name="playerId">The Player Id (not the Steam Id)</param>
        /// <returns>Will return the Player of your choice</returns>
        public IPlayer GetPlayerPageData(string playerId)
        {
            try
            {
                return mainController.PlayerController.GetPlayer(playerId);
            }
            catch (Exception exception)
            {
                mainController.Dispose();

                throw new Dota2StatParserException("Something went wrong while parsing the player. See the innerexception for details!", exception);
            }
        }

        /// <summary>
        /// Gathers all the matches from the Dotabuff PlayerMatches-Page and combines it into list of matches.
        /// </summary>
        /// <param name="playerId">The Player Id (not the Steam Id)</param>
        /// <param name="playerMatchesOptions">Options for selecting the Player Matches</param>
        /// <returns>Will return all matches that match the criteria of the options</returns>
        public IEnumerable<IMatchExtended> GetPlayerMatchesPageData(string playerId, PlayerMatchesOptions playerMatchesOptions)
        {
            try
            {
                return mainController.PlayerController.GetMatchesFromPlayer(playerId, playerMatchesOptions);
            }
            catch (Exception exception)
            {
                mainController.Dispose();

                throw new Dota2StatParserException("Something went wrong while parsing the player matches. See the innerexception for details!", exception);
            }
        }

        /// <summary>
        /// Gets the word cloud from the Yasp WordCloud-Page of the player.
        /// </summary>
        /// <param name="playerId">The player identifier.</param>
        /// <returns>Returns the word cloud as a list of words and their count</returns>
        public IEnumerable<IWordCloud> GetWordCloud(string playerId)
        {
            try
            {
                return mainController.WordCloudController.GetPlayerWordCloud(playerId);
            }
            catch(Exception exception)
            {
                mainController.Dispose();

                throw new Dota2StatParserException("Something went wrong while parsing the word cloud. See the innerexception for details!", exception);
            }
        }

        /// <summary>
        /// Checks Dotabuff and Yasp Pages for availablity.
        /// </summary>
        /// <returns>Will return the result of the Healthcheck</returns>
        public IHealthCheckResult PerformHealthCheck()
        {
            return healthCheck.PerformHealthCheck();
        }

        /// <summary>
        /// Checks only Dotabuff Pages for availablity.
        /// </summary>
        /// <returns>Will return if the HealthCheck was successfull or not.</returns>
        public IHealthCheckResult PerformHealthCheckForDotabuffOnly()
        {
            return healthCheck.PerformHealthCheckForDotabuffOnly();
        }

        /// <summary>
        /// Checks only Yasp Pages for availablity.
        /// </summary>
        /// <returns>Will return if the HealthCheck was successfull or not.</returns>
        public IHealthCheckResult PerformHealthCheckForYaspOnly()
        {
            return healthCheck.PerformHealthCheckForYaspOnly();
        }

        /// <summary>
        /// Extended Dispose-Metod that will dispose all objects inside this class.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);   
        }

        /// <summary>
        /// Handles the disposing of all objects inside this class.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if(mainController != null)
                mainController.Dispose();

            if(healthCheck != null)
                healthCheck.Dispose();
        }
    }
}
