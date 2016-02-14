using DotA2StatsParser.Controller;
using DotA2StatsParser.Model.HealthCheck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DotA2StatsParser.Model.Dotabuff;
using HtmlAgilityPack;
using DotA2StatsParser.Model.HealthCheck.Interfaces;

namespace DotA2StatsParser.Controller
{
    public class HealthCheck : IDisposable
    {
        internal MainController mainController;

        internal HealthCheck(MainController mainController)
        {
            this.mainController = mainController;
        }

        internal IHealthCheckResult PerformHealthCheck()
        {
            return new HealthCheckResult(IsYaspAvailable(), IsDotabuffAvailable());
        }

        internal IHealthCheckResult PerformHealthCheckForDotabuffOnly()
        {
            return new HealthCheckResult() { IsDotabuffAvailable = IsDotabuffAvailable() };
        }

        internal IHealthCheckResult PerformHealthCheckForYaspOnly()
        {
            return new HealthCheckResult() { IsYaspAvailable = IsYaspAvailable() };
        }

        private bool IsYaspAvailable()
        {
            try
            {
                mainController.HtmlDocumentController.TryLoadingYasp();
                return true;
            }
            catch (WebException)
            {
                return false;
            }
        }

        private bool IsDotabuffAvailable()
        {
            try
            {
                mainController.HtmlDocumentController.TryLoadingDotabuff();

                List<string> nodeErrors = new List<string>();
                nodeErrors.AddRange(IsNodeAvailable(mainController.HtmlDocumentController.GetDotabuffHeroRoot(Heroes.Abaddon.ToString().ToLower()),
                                                            mainController.DotabuffMappingController.GetHeroPathsAsDictionary()));

                nodeErrors.AddRange(IsNodeAvailable(mainController.HtmlDocumentController.GetDotabuffItemRoot(Items.Crystalys.ToString().ToLower()),
                                                            mainController.DotabuffMappingController.GetItemPathsAsDictionary()));

                nodeErrors.AddRange(IsNodeAvailable(mainController.HtmlDocumentController.GetDotabuffPlayerRoot("111620041"),
                                                            mainController.DotabuffMappingController.GetPlayerPathsAsDictionary()));

                return nodeErrors.Count == 0;
            }
            catch (WebException)
            {
                return false;
            }
        }

        private List<string> IsNodeAvailable(HtmlNode root, Dictionary<string, string> paths)
        {
            List<string> nodeErrors = new List<string>();

            foreach (string key in paths.Keys)
            {
                string path = paths[key];
                if (path.Contains("{0}"))
                    path = string.Format(path, 1);

                HtmlNode node = root.SelectSingleNode(path);

                if (node == null)
                    nodeErrors.Add(path);
            }

            return nodeErrors;
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
            if (mainController != null)
                mainController.Dispose();
        }
    }
}
