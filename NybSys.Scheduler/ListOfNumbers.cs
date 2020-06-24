using System.Collections.Generic;
using System.Linq;

namespace NybSys.Scheduler
{
    public class NumbersForAlert
    {
        private static IDictionary<string, CellularTrack> _lstOfNumbers = new Dictionary<string, CellularTrack>();

        public static void AddNumber(CellularTrack cellularTrack)
        {
            if(isExist(cellularTrack.PhoneNo))
            {
                UpdateNumber(cellularTrack);
            }
            else
            {
                _lstOfNumbers.Add(cellularTrack.PhoneNo, cellularTrack);
            }
        }

        public static List<CellularTrack> GetNumbers()
        {
            return _lstOfNumbers.Select(p => p.Value).ToList();
        }

        public static void RemoveNumber(string cellPhoneNo)
        {
            if(isExist(cellPhoneNo))
            {
                _lstOfNumbers.Remove(cellPhoneNo);
            }
        }

        private static bool isExist(string cellPhoneNo)
        {
            return _lstOfNumbers.Any(p => p.Key == cellPhoneNo);
        }

        public static void UpdateNumber(CellularTrack cellularTrack)
        {
            var numberTrack = _lstOfNumbers.Where(p => p.Key == cellularTrack.PhoneNo);
            _lstOfNumbers[cellularTrack.PhoneNo] = cellularTrack;
        }
    }
}
