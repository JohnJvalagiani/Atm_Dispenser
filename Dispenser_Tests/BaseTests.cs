using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DispenserTask.Models.Tests
{
    public class BaseTests
    {
        protected Atm Atm => GetConfiguredAtm();

        private Atm GetConfiguredAtm()
        {
            var options = new AtmOptions();
            options.CassettesBillsMaps.Add(5, 100);
            options.CassettesBillsMaps.Add(20, 20);
            options.CassettesBillsMaps.Add(100, 100);

            List<Cassette> cassettes = new();

            foreach (var map in options.CassettesBillsMaps)
            {
                var cassette = new Cassette(map.Key);
                cassette.LoadBills(GenerateBills(map.Key, map.Value));
                cassettes.Add(cassette);
            }

            var atm = new Atm();
            atm.LoadCassettes(cassettes);

            return atm;

            static IReadOnlyCollection<Bill> GenerateBills(int nominal, int count)
            {
                Collection<Bill> bills = new();

                for (var i = 0; i < count; i++)
                {
                    bills.Add(new Bill { NominalValue = nominal });
                }

                return bills;
            }
        }

        private class AtmOptions
        {
            public IDictionary<int, int> CassettesBillsMaps = new Dictionary<int, int>();
        }
    }
}