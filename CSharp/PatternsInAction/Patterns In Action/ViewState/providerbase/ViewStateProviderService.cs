using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Configuration.Provider;
using System.Web.Configuration;

namespace ViewState
{
    /// <summary>
    /// ViewStateProviderService makes the viewstate providers available
    /// to the client. This includes loading the providers declared in the 
    /// web.config file.
    /// </summary>
    /// <remarks>
    /// Enterprise Design Patterns: Lazy Load.
    /// 
    /// The Lazy Load Design Pattern is implemented by the LoadProviders() method.
    /// It only loads and creates the viewstate providers when the LoadPageState and the 
    /// SavePageState methods are called. The LoadProviders() method coule have been 
    /// called in the static constructor(Shared constructor in VB) which would turn the 
    /// "Lazy Load" into a socalled "Eager Load".
    /// </remarks>
    public static class ViewStateProviderService
    {
        private static ViewStateProviderBase _provider = null;
        private static ViewStateProviderCollection _providers = null;
        private static object _syncLock = new object();

        /// <summary>
        /// Retrieves the viewstate information from the appropriate viewstate provider. 
        /// Implements Lazy Load Design Pattern.
        /// </summary>
        /// <param name="name">Name of provider.</param>
        /// <returns>Viewstate.</returns>
        public static object LoadPageState(string name)
        {
            // Make sure a provider is loaded. Lazy Load Design Pattern.
            LoadProviders();

            // Delegate to the provider
            return _provider.LoadPageState(name);
        }

        /// <summary>
        /// Saves any view or control state information to the appropriate 
        /// viewstate provider. 
        /// </summary>
        /// <param name="name">Name of viewstate.</param>
        /// <param name="viewState">Viewstate.</param>
        public static void SavePageState(string name, object viewState)
        {
            // Make sure a provider is loaded
            LoadProviders();

            // Delegate to the provider
            _provider.SavePageState(name, viewState);
        }

        /// <summary>
        /// Instantiates and manages the viewstate providers according to the 
        /// registered providers in the "viewStateServices" section in web.config.
        /// </summary>
        private static void LoadProviders()
        {
            // providers are loaded just once
            if (_provider == null)
            {
                // Synchronize the process of loading the providers
                lock (_syncLock)
                {
                    // Confirm that _provider is still null
                    if (_provider == null)
                    {
                        // Get a reference to the <viewstateService> section
                        ViewStateProviderServiceSection section = (ViewStateProviderServiceSection)
                            WebConfigurationManager.GetSection("system.web/viewstateService");

                        // Load all registered providers
                        _providers = new ViewStateProviderCollection();

                        ProvidersHelper.InstantiateProviders
                            (section.Providers, _providers,
                            typeof(ViewStateProviderBase));

                        // Set _provider to the default provider
                        _provider = _providers[section.DefaultProvider];
                    }
                }
            }
        }
    }
}
