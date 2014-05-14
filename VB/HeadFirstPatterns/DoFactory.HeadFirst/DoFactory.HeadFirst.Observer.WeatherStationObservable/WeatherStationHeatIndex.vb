Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic

Namespace DoFactory.HeadFirst.Observer.WeatherStationObservable
	Friend Class WeatherStationHeatIndex
        Shared Sub Main(ByVal args() As String)

            Dim weatherData As New WeatherData()

            Dim currentConditions As New CurrentConditionsDisplay(weatherData)
            Dim statisticsDisplay As New StatisticsDisplay(weatherData)
            Dim forecastDisplay As New ForecastDisplay(weatherData)
            Dim heatIndexDisplay As New HeatIndexDisplay(weatherData)

            weatherData.SetMeasurements(80, 65, 30.4F)
            weatherData.SetMeasurements(82, 70, 29.2F)
            weatherData.SetMeasurements(78, 90, 29.2F)

            ' Wait for user
            Console.ReadKey()
        End Sub
	End Class

	#Region "Observable"

	' This is what the Java built-in Observable class 
	' roughly looks like (except for the generic List)
	Public Class Observable
		Private _changed As Boolean
		Private _observers As List(Of IObserver) = New List(Of IObserver)()

		Public Sub AddObserver(ByVal observer As IObserver)
			_observers.Add(observer)
		End Sub

		Public Sub RemoveObserver(ByVal observer As IObserver)
			_observers.Remove(observer)
		End Sub

		Public WriteOnly Property Changed() As Boolean
			Set(ByVal value As Boolean)
				_changed = value
			End Set
		End Property

		Public Sub NotifyObservers()
			For Each observer As IObserver In _observers
				observer.Update(Me)
			Next observer
		End Sub
	End Class

	Public Class WeatherData
		Inherits Observable
		Private _temperature As Single
		Private _humidity As Single
		Private _pressure As Single

		Public Sub MeasurementsChanged()
			Changed = True
			NotifyObservers()
		End Sub

		Public Sub SetMeasurements(ByVal temperature As Single, ByVal humidity As Single, ByVal pressure As Single)
			Me._temperature = temperature
			Me._humidity = humidity
			Me._pressure = pressure
			MeasurementsChanged()
		End Sub

		Public ReadOnly Property Temperature() As Single
			Get
				Return _temperature
			End Get
		End Property

		Public ReadOnly Property Humidity() As Single
			Get
				Return _humidity
			End Get
		End Property

		Public ReadOnly Property Pressure() As Single
			Get
				Return _pressure
			End Get
		End Property
	End Class

	#End Region

	#Region "Observer"

	' This is what the Java built-in Observer interface 
	' roughly looks like
	Public Interface IObserver
		Sub Update(ByVal subject As Object)
	End Interface

	Public Interface IDisplayElement
		Sub Display()
	End Interface

	Public Class ForecastDisplay
		Implements IObserver, IDisplayElement
		Private _currentPressure As Single = 29.92f
		Private _lastPressure As Single

		Public Sub New(ByVal observable As Observable)
			observable.AddObserver(Me)
		End Sub

		Public Sub Update(ByVal subject As Object) Implements IObserver.Update
			If TypeOf subject Is WeatherData Then
				Dim weatherData As WeatherData = CType(subject, WeatherData)
				_lastPressure = _currentPressure
				_currentPressure = weatherData.Pressure
				Display()
			End If
		End Sub


		Public Sub Display() Implements IDisplayElement.Display
			Console.Write("Forecast: ")
			If _currentPressure > _lastPressure Then
				Console.WriteLine("Improving weather on the way!")
			ElseIf _currentPressure = _lastPressure Then
				Console.WriteLine("More of the same")
			ElseIf _currentPressure < _lastPressure Then
				Console.WriteLine("Watch out for cooler, rainy weather")
			End If
		End Sub
	End Class

	Public Class HeatIndexDisplay
		Implements IObserver, IDisplayElement
		Private _heatIndex As Single = 0.0f

		Public Sub New(ByVal observable As Observable)
			observable.AddObserver(Me)
		End Sub

		Public Sub Update(ByVal subject As Object) Implements IObserver.Update
			If TypeOf subject Is WeatherData Then
				Dim weatherData As WeatherData = CType(subject, WeatherData)
				Dim t As Single = weatherData.Temperature
				Dim rh As Single = weatherData.Humidity
				_heatIndex = CSng((16.923 + (0.185212 * t)) + (5.37941 * rh) - (0.100254 * t * rh) + (0.00941695 * (t * t)) + (0.00728898 * (rh * rh)) + (0.000345372 * (t * t * rh)) - (0.000814971 * (t * rh * rh)) + (0.0000102102 * (t * t * rh * rh)) - (0.000038646 * (t * t * t)) + (0.0000291583 * (rh * rh * rh)) + (0.00000142721 * (t * t * t * rh)) + (0.000000197483 * (t * rh * rh * rh)) - (0.0000000218429 * (t * t * t * rh * rh)) + (0.000000000843296 * (t * t * rh * rh * rh)) - (0.0000000000481975 * (t * t * t * rh * rh * rh)))
				Display()
			End If
		End Sub


		Public Sub Display() Implements IDisplayElement.Display
            Console.WriteLine("Heat index is " & _heatIndex & Constants.vbLf)
		End Sub
	End Class

	Public Class StatisticsDisplay
		Implements IObserver, IDisplayElement
		Private _maxTemp As Single = 0.0f
		Private _minTemp As Single = 200
		Private _tempSum As Single = 0.0f
		Private _numReadings As Integer

		Public Sub New(ByVal observable As Observable)
			observable.AddObserver(Me)
		End Sub

		Public Sub Update(ByVal subject As Object) Implements IObserver.Update
			If TypeOf subject Is WeatherData Then
				Dim weatherData As WeatherData = CType(subject, WeatherData)
				Dim temp As Single = weatherData.Temperature
				_tempSum += temp
				_numReadings += 1

				If temp > _maxTemp Then
					_maxTemp = temp
				End If

				If temp < _minTemp Then
					_minTemp = temp
				End If

				Display()
			End If
		End Sub

		Public Sub Display() Implements IDisplayElement.Display
			Console.WriteLine("Avg/Max/Min temperature = " & (_tempSum / _numReadings) & "/" & _maxTemp & "/" & _minTemp)
		End Sub
	End Class

	Public Class CurrentConditionsDisplay
		Implements IObserver, IDisplayElement
		Private _observable As Observable
		Private _temperature As Single
		Private _humidity As Single

		Public Sub New(ByVal observable As Observable)
			Me._observable = observable
			observable.AddObserver(Me)
		End Sub

		Public Sub Update(ByVal subject As Object) Implements IObserver.Update
			If TypeOf subject Is WeatherData Then
				Dim weatherData As WeatherData = CType(subject, WeatherData)
				Me._temperature = weatherData.Temperature
				Me._humidity = weatherData.Humidity
				Display()
			End If
		End Sub

		Public Sub Display() Implements IDisplayElement.Display
			Console.WriteLine("Current conditions: " & _temperature & "F degrees and " & _humidity & "% humidity")
		End Sub
	End Class
	#End Region
End Namespace
