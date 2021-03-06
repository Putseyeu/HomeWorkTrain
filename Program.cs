using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkTrain
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dispatcher dispatcher = new Dispatcher();
            string userInput = "";

            while (userInput != "3")
            {
                dispatcher.ShowInfoTrain();
                Console.WriteLine("\nПрограмма для учета поездов");
                Console.WriteLine("1 - Создать поезд. 2 - Обновить данные.  3 - Закончить работу.");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        dispatcher.AddTrain();
                        break;

                    case "2":
                        break;
                }

                Console.Clear();
            }
        }
    }

    class Dispatcher
    {
        private List<Train> _trains = new List<Train>();

        public void AddTrain()
        {
            _trains.Add(new Train());
        }

        public void ShowInfoTrain()
        {
            for (int i = 0; i < _trains.Count; i++)
            {                
                if (_trains[i].GetDistance(_trains[i].Route) > 0)
                {
                    Console.WriteLine($"Поезд с маршрутом {_trains[i].Route.PointDeparture} - {_trains[i].Route.PointArrival} в пути" +
                    $" Осталось {_trains[i].GetDistance(_trains[i].Route)} км до прибытия.");
                    _trains[i].Route.SkipDidistance();
                }
                else
                {
                    Console.WriteLine($"Поезд маршрутa {_trains[i].Route.PointDeparture} - {_trains[i].Route.PointArrival} прибыл ");
                }
            }            
        }

        class Train
        {
            private Route _route = new Route();
            private Wagons _wagons = new Wagons();

            public Route Route => _route;

            public int GetDistance(Route route)
            {
                int distance = route.Distance;
                return distance;
            }
        }

        class Route
        {
            private string _pointDeparture;
            private string _pointArrival;
            private int _distance;

            public string PointDeparture => _pointDeparture;
            public string PointArrival => _pointArrival;
            public int Distance => _distance;

            public Route()
            {
                GreateRoute();
            }

            private void GreateRoute()
            {
                Console.WriteLine("Укажите пункт отправления");
                _pointDeparture = Console.ReadLine();
                Console.WriteLine("Укадите пункт прибытия");
                _pointArrival = Console.ReadLine();
                Console.WriteLine("Установиь дистанция в км");
                bool completed = false;
                int intValue = 0;

                while (completed == false)
                {
                    string userInput = Console.ReadLine();
                    if (int.TryParse(userInput, out intValue))
                    {
                        completed = true;
                    }
                    else
                    {
                        Console.WriteLine($"Не коректный ввод данных.");
                    }
                }

                _distance = intValue;
            }

            public void SkipDidistance()
            {
                int subtract = 10;
                _distance -= subtract;
            }
        }

        class Wagons
        {
            private Passenger _passenger = new Passenger();
            private string _titleStandard = "стандартный вагон";
            private string _titleDouble = "двух уровневый вагон";
            private int _standartPassenger = 40;
            private int _doublePassenger = 80;
            private int _minNumber = 4;
            private int _maxNumber = 16;

            public Wagons()
            {
                SetQuantity();
            }

            public void SetQuantity()
            {
                Console.WriteLine("Производим расчет количетсва вагонов для состава.");

                if (_standartPassenger * _maxNumber > _passenger.AllNumber)
                {
                    for (int i = _minNumber; i < _maxNumber; i++)
                    {
                        if (_standartPassenger * i > _passenger.AllNumber)
                        {
                            Console.WriteLine($"Для перевозки  {_passenger.AllNumber} требуется {i} {_titleStandard}");
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = _minNumber; i < _maxNumber; i++)
                    {
                        if (_doublePassenger * i > _passenger.AllNumber)
                        {
                            Console.WriteLine($"Для перевозки  {_passenger.AllNumber} требуется {i} {_titleDouble}");
                            break;
                        }
                    }
                }
            }
        }

        class Passenger
        {
            private int _minNumber = 160;
            private int _maxNumber = 1280;
            private Random _random = new Random();
            public int AllNumber { get; private set; }

            public Passenger()
            {
                AllNumber = _random.Next(_minNumber, _maxNumber);
            }
        }
    }
}
