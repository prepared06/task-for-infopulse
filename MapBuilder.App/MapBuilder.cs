using System;
using System.Collections.Generic;

namespace MapBuilder.App
{
    public class MapBuilder
    {
        private SignStep _currentOfficeManagerPosition = new()
        {
            Floor = 0,
            Section = 1
        };

        public IEnumerable<string> BuildRouteMap(IEnumerable<SignStep> signatureMap)
        {
            List<string> routeMap = new List<string>();

            foreach (SignStep step in signatureMap)
            {
                int sV = StairsValue(step);
                int sE1 = Elevator1Value(step);
                int sE2 = Elevator2Value(step);

                if (sE1 == 1000)
                {
                    if (sV <= sE2)
                    {
                        routeMap.Add("S");
                    }
                    else
                    {
                        routeMap.Add("E2");
                    }
                }
                else if (sE2 == 1000)
                {
                    if (sV <= sE1)
                    {
                        routeMap.Add("S");
                    }
                    else
                    {
                        routeMap.Add("E1");
                    }
                }
                _currentOfficeManagerPosition = step;
            }
            return routeMap;
        }
        private int StairsValue(SignStep GoalStep)
        {
            int minutes = Math.Abs(_currentOfficeManagerPosition.Floor - GoalStep.Floor);
            minutes *= 2;
            return minutes;
        }
        private int Elevator1Value(SignStep GoalStep)
        {
            if (GoalStep.Floor % 2 == 1)
            {
                return 1000;
            }
            int minutes = 1;
            minutes += Math.Abs(_currentOfficeManagerPosition.Floor - GoalStep.Floor);
            if (_currentOfficeManagerPosition.Section != GoalStep.Section)
            {
                minutes++;
            }
            return minutes;
        }
        private int Elevator2Value(SignStep GoalStep)
        {
            if (GoalStep.Floor % 2 == 0)
            {
                return 1000;
            }
            int minutes = 1;
            if (_currentOfficeManagerPosition.Section != GoalStep.Section)
            {
                minutes++;
            }
            minutes += Math.Abs(_currentOfficeManagerPosition.Floor - GoalStep.Floor);
            return minutes;
        }

    }
}