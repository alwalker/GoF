using System;
using System.Collections.Generic;

namespace DoFactory.HeadFirst.Observer.WeatherStationObservable
{
    class WeatherStationHeatIndex
    {
        static void Main(string[] args)
        {
            WeatherData weatherData = new WeatherData();

            CurrentConditionsDisplay currentConditions = new CurrentConditionsDisplay(weatherData);
            StatisticsDisplay statisticsDisplay = new StatisticsDisplay(weatherData);
            ForecastDisplay forecastDisplay = new ForecastDisplay(weatherData);
            HeatIndexDisplay heatIndexDisplay = new HeatIndexDisplay(weatherData);

            weatherData.SetMeasurements(80, 65, 30.4f);
            weatherData.SetMeasurements(82, 70, 29.2f);
            weatherData.SetMeasurements(78, 90, 29.2f);

            // Wait for user
            Console.ReadKey();
        }
    }

    #region Observable

    // This is what the Java built-in Observable class 
    // roughly looks like (except for the generic List)
    public class Observable
    {
        private bool _changed;
        private List<IObserver> _observers = new List<IObserver>();

        public void AddObserver(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public bool Changed
        {
            set { _changed = value; }
        }

        public void NotifyObservers()
        {
            foreach (IObserver observer in _observers)
            {
                observer.Update(this);
            }
        }
    }

    public class WeatherData : Observable
    {
        private float _temperature;
        private float _humidity;
        private float _pressure;

        public void MeasurementsChanged()
        {
            Changed = true;
            NotifyObservers();
        }

        public void SetMeasurements(float temperature, float humidity, float pressure)
        {
            this._temperature = temperature;
            this._humidity = humidity;
            this._pressure = pressure;
            MeasurementsChanged();
        }

        public float Temperature
        {
            get { return _temperature; }
        }

        public float Humidity
        {
            get { return _humidity; }
        }

        public float Pressure
        {
            get { return _pressure; }
        }
    }

    #endregion

    #region Observer

    // This is what the Java built-in Observer interface 
    // roughly looks like
    public interface IObserver
    {
        void Update(object subject);
    }

    public interface IDisplayElement
    {
        void Display();
    }

    public class ForecastDisplay : IObserver, IDisplayElement
    {
        private float _currentPressure = 29.92f;
        private float _lastPressure;

        public ForecastDisplay(Observable observable)
        {
            observable.AddObserver(this);
        }

        public void Update(object subject)
        {
            if (subject is WeatherData)
            {
                WeatherData weatherData = (WeatherData)subject;
                _lastPressure = _currentPressure;
                _currentPressure = weatherData.Pressure;
                Display();
            }
        }


        public void Display()
        {
            Console.Write("Forecast: ");
            if (_currentPressure > _lastPressure)
            {
                Console.WriteLine("Improving weather on the way!");
            }
            else if (_currentPressure == _lastPressure)
            {
                Console.WriteLine("More of the same");
            }
            else if (_currentPressure < _lastPressure)
            {
                Console.WriteLine("Watch out for cooler, rainy weather");
            }
        }
    }

    public class HeatIndexDisplay : IObserver, IDisplayElement
    {
        private float _heatIndex = 0.0f;

        public HeatIndexDisplay(Observable observable)
        {
            observable.AddObserver(this);
        }

        public void Update(object subject)
        {
            if (subject is WeatherData)
            {
                WeatherData weatherData = (WeatherData)subject;
                float t = weatherData.Temperature;
                float rh = weatherData.Humidity;
                _heatIndex = (float)
                    (
                    (16.923 + (0.185212 * t)) +
                    (5.37941 * rh) -
                    (0.100254 * t * rh) +
                    (0.00941695 * (t * t)) +
                    (0.00728898 * (rh * rh)) +
                    (0.000345372 * (t * t * rh)) -
                    (0.000814971 * (t * rh * rh)) +
                    (0.0000102102 * (t * t * rh * rh)) -
                    (0.000038646 * (t * t * t)) +
                    (0.0000291583 * (rh * rh * rh)) +
                    (0.00000142721 * (t * t * t * rh)) +
                    (0.000000197483 * (t * rh * rh * rh)) -
                    (0.0000000218429 * (t * t * t * rh * rh)) +
                    (0.000000000843296 * (t * t * rh * rh * rh)) -
                    (0.0000000000481975 * (t * t * t * rh * rh * rh)));
                Display();
            }
        }


        public void Display()
        {
            Console.WriteLine("Heat index is " + _heatIndex + "\n");
        }
    }

    public class StatisticsDisplay : IObserver, IDisplayElement
    {
        private float _maxTemp = 0.0f;
        private float _minTemp = 200;
        private float _tempSum = 0.0f;
        private int _numReadings;

        public StatisticsDisplay(Observable observable)
        {
            observable.AddObserver(this);
        }

        public void Update(object subject)
        {
            if (subject is WeatherData)
            {
                WeatherData weatherData = (WeatherData)subject;
                float temp = weatherData.Temperature;
                _tempSum += temp;
                _numReadings++;

                if (temp > _maxTemp)
                {
                    _maxTemp = temp;
                }

                if (temp < _minTemp)
                {
                    _minTemp = temp;
                }

                Display();
            }
        }

        public void Display()
        {
            Console.WriteLine("Avg/Max/Min temperature = " + (_tempSum / _numReadings)
                + "/" + _maxTemp + "/" + _minTemp);
        }
    }

    public class CurrentConditionsDisplay : IObserver, IDisplayElement
    {
        private Observable _observable;
        private float _temperature;
        private float _humidity;

        public CurrentConditionsDisplay(Observable observable)
        {
            this._observable = observable;
            observable.AddObserver(this);
        }

        public void Update(object subject)
        {
            if (subject is WeatherData)
            {
                WeatherData weatherData = (WeatherData)subject;
                this._temperature = weatherData.Temperature;
                this._humidity = weatherData.Humidity;
                Display();
            }
        }

        public void Display()
        {
            Console.WriteLine("Current conditions: " + _temperature
                + "F degrees and " + _humidity + "% humidity");
        }
    }
    #endregion
}
