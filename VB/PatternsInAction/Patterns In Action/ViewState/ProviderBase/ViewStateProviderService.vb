Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Configuration
Imports System.Configuration.Provider
Imports System.Web.Configuration

Namespace ViewState
	''' <summary>
	''' ViewStateProviderService makes the viewstate providers available
	''' to the client. This includes loading the providers declared in the 
	''' web.config file.
	''' </summary>
	''' <remarks>
	''' Enterprise Design Patterns: Lazy Load.
	''' 
	''' The Lazy Load Design Pattern is implemented by the LoadProviders() method.
	''' It only loads and creates the viewstate providers when the LoadPageState and the 
	''' SavePageState methods are called. The LoadProviders() method coule have been 
	''' called in the static constructor(Shared constructor in VB) which would turn the 
	''' "Lazy Load" into a socalled "Eager Load".
	''' </remarks>
	Public NotInheritable Class ViewStateProviderService
		Private Shared _provider As ViewStateProviderBase = Nothing
		Private Shared _providers As ViewStateProviderCollection = Nothing
		Private Shared _syncLock As Object = New Object()

		''' <summary>
		''' Retrieves the viewstate information from the appropriate viewstate provider. 
		''' Implements Lazy Load Design Pattern.
		''' </summary>
		''' <param name="name">Name of provider.</param>
		''' <returns>Viewstate.</returns>
		Private Sub New()
		End Sub
		Public Shared Function LoadPageState(ByVal name As String) As Object
			' Make sure a provider is loaded. Lazy Load Design Pattern.
			LoadProviders()

			' Delegate to the provider
			Return _provider.LoadPageState(name)
		End Function

		''' <summary>
		''' Saves any view or control state information to the appropriate 
		''' viewstate provider. 
		''' </summary>
		''' <param name="name">Name of viewstate.</param>
		''' <param name="viewState">Viewstate.</param>
		Public Shared Sub SavePageState(ByVal name As String, ByVal viewState As Object)
			' Make sure a provider is loaded
			LoadProviders()

			' Delegate to the provider
			_provider.SavePageState(name, viewState)
		End Sub

		''' <summary>
		''' Instantiates and manages the viewstate providers according to the 
		''' registered providers in the "viewStateServices" section in web.config.
		''' </summary>
		Private Shared Sub LoadProviders()
			' providers are loaded just once
			If _provider Is Nothing Then
				' Synchronize the process of loading the providers
				SyncLock _syncLock
					' Confirm that _provider is still null
					If _provider Is Nothing Then
						' Get a reference to the <viewstateService> section
						Dim section As ViewStateProviderServiceSection = CType(WebConfigurationManager.GetSection("system.web/viewstateService"), ViewStateProviderServiceSection)

						' Load all registered providers
						_providers = New ViewStateProviderCollection()

						ProvidersHelper.InstantiateProviders (section.Providers, _providers, GetType(ViewStateProviderBase))

						' Set _provider to the default provider
						_provider = _providers(section.DefaultProvider)
					End If
				End SyncLock
			End If
		End Sub
	End Class
End Namespace
