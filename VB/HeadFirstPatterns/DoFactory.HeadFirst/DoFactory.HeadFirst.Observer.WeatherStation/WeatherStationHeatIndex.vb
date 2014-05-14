Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic

Namespace DoFactory.HeadFirst.Observer.WeatherStation
	Friend Class WeatherStationHeatIndex
        Shared Sub Main(ByVal args() As String)

            Dim weatherData As New WeatherData()

            Dim currentDisplay As New CurrentConditionsDisplay(weatherData)
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

	#Region "Subject"

	Public Interface ISubject
		Sub RegisterObserver(ByVal observer As IObserver)
		Sub RemoveObserver(ByVal observer As IObserver)
		Sub NotifyObservers()
	End Interface

	Public Class WeatherData
		Implements ISubject
		Private _observers As List(Of IObserver) = New List(Of IObserver)()
		Private _temperature As Single
		Private _humidity As Single
		Private _pressure As Single

		Public Sub RegisterObserver(ByVal observer As IObserver) Implements ISubject.RegisterObserver
			_observers.Add(observer)
		End Sub

		Public Sub RemoveObserver(ByVal observer As IObserver) Implements ISubject.RemoveObserver
			_observers.Remove(observer)
		End Sub

		Public Sub NotifyObservers() Implements ISubject.NotifyObservers
			For Each observer As IObserver In _observers
				observer.Update(_temperature, _humidity, _pressure)
			Next observer
		End Sub

		Public Sub MeasurementsChanged()
			NotifyObservers()
		End Sub

		Public Sub SetMeasurements(ByVal temperature As Single, ByVal humidity As Single, ByVal pressure As Single)
			Me._temperature = temperature
			Me._humidity = humidity
			Me._pressure = pressure
			MeasurementsChanged()
		End Sub
	End Class
	#End Region

	#Region "Observer"

	Public Interface IObserver
		Sub Update(ByVal temperature As Single, ByVal humidity As Single, ByVal pressure As Single)
	End Interface

	Public Interface IDisplayElement
		Sub Display()
	End Interface

	Public Class CurrentConditionsDisplay
		Implements IObserver, IDisplayElement
		Private _temperature As Single
		Private _humidity As Single
		Private _weatherData As ISubject

		Public Sub New(ByVal weatherData As ISubject)
			Me._weatherData = weatherData
			weatherData.RegisterObserver(Me)
		End Sub

		Public Sub Update(ByVal temperature As Single, ByVal humidity As Single, ByVal pressure As Single) Implements IObserver.Update
			Me._temperature = temperature
			Me._humidity = humidity
			Display()
		End Sub

		Public Sub Display() Implements IDisplayElement.Display
			Console.WriteLine("Current conditions: " & _temperature & "F degrees and " & _humidity & "% humidity")
		End Sub
	End Class

	Public Class ForecastDisplay
		Implements IObserver, IDisplayElement
		Private _currentPressure As Single = 29.92f
		Private _lastPressure As Single
		Private _weatherData As WeatherData

		Public Sub New(ByVal weatherData As WeatherData)
			Me._weatherData = weatherData
			weatherData.RegisterObserver(Me)
		End Sub

		Public Sub Update(ByVal temperature As Single, ByVal humidity As Single, ByVal pressure As Single) Implements IObserver.Update
			_lastPressure = _currentPressure
			_currentPressure = pressure

			Display()
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
		Private _weatherData As WeatherData

		Public Sub New(ByVal weatherData As WeatherData)
			Me._weatherData = weatherData
			weatherData.RegisterObserver(Me)
		End Sub

		Public Sub Update(ByVal temperature As Single, ByVal humidity As Single, ByVal pressure As Single) Implements IObserver.Update
			_heatIndex = ComputeHeatIndex(temperature, humidity)
			Display()
		End Sub

		Private Function ComputeHeatIndex(ByVal t As Single, ByVal rh As Single) As Single
			Dim heatindex As Single = CSng((16.923 + (0.185212 * t)) + (5.37941 * rh) - (0.100254 * t * rh) + (0.00941695 * (t * t)) + (0.00728898 * (rh * rh)) + (0.000345372 * (t * t * rh)) - (0.000814971 * (t * rh * rh)) + (0.0000102102 * (t * t * rh * rh)) - (0.000038646 * (t * t * t)) + (0.0000291583 * (rh * rh * rh)) + (0.00000142721 * (t * t * t * rh)) + (0.000000197483 * (t * rh * rh * rh)) - (0.0000000218429 * (t * t * t * rh * rh)) + (0.000000000843296 * (t * t * rh * rh * rh)) - (0.0000000000481975 * (t * t * t * rh * rh * rh)))
			Return heatindex
		End Function

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
		Private _weatherData As WeatherData

		Public Sub New(ByVal weatherData As WeatherData)
			Me._weatherData = weatherData
			weatherData.RegisterObserver(Me)
		End Sub

		Public Sub Update(ByVal temperature As Single, ByVal humidity As Single, ByVal pressure As Single) Implements IObserver.Update
			_tempSum += temperature
			_numReadings += 1

			If temperature > _maxTemp Then
				_maxTemp = temperature
			End If

			If temperature < _minTemp Then
				_minTemp = temperature
			End If

			Display()
		End Sub

		Public Sub Display() Implements IDisplayElement.Display
			Console.WriteLine("Avg/Max/Min temperature = " & (_tempSum / _numReadings) & "/" & _maxTemp & "/" & _minTemp)
		End Sub
	End Class

	#End Region
End Namespace