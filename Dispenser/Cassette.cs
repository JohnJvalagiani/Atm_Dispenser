using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DispenserTask.Models
{
    public class Cassette
    {
        private const int MaxBillsCountCassetteCanHold = 2500;

        private readonly IList<Bill> _billsStore = new List<Bill>();
        private readonly int _billsValue;

        /// <summary>
        /// Instantiates the <see cref="Cassette"/> object.
        /// </summary>
        /// <param name="billsValue">Bill's nominal value which <see cref="Cassette"/> can keep.</param>
        public Cassette(int billsValue)
        {
            if (billsValue <= 0)
            {
                throw new ArgumentException("The cassette can keep only bills, which have value.");
            }

            _billsValue = billsValue;
        }

        public int BillsValue => _billsValue;

        /// <summary>
        /// Transfers bills from <see cref="Cassette"/> to the output collection.
        /// </summary>
        /// <param name="number">Amount of the bills to dispense from cassette.</param>
        /// <param name="takenBills">A collection of taken bills.</param>
        /// <returns>Result of the operation.</returns>
        public bool TryTake(int number, out IEnumerable<Bill> takenBills)
        {
            takenBills = default;

            if (number > _billsStore.Count)
            {
                return false;
            }

            var bills = new Collection<Bill>();

            for (var i = number - 1; i >= 0; i--)
            {
                bills.Add(_billsStore[i]);
                _billsStore.RemoveAt(i);
            }

            takenBills = bills;
            return true;
        }

        /// <summary>
        /// Loads <see cref="Cassette"/> with bills.
        /// </summary>
        /// <param name="bills">Bills to load.</param>
        public void LoadBills(IReadOnlyCollection<Bill> bills)
        {
            if (bills.Count > MaxBillsCountCassetteCanHold)
            {
                throw new ArgumentException(string.Format("Cassette can hold only {0} cassettes.", MaxBillsCountCassetteCanHold), nameof(bills));
            }

            foreach (var bill in bills)
            {
                _billsStore.Add(bill);
            }
        }
    }
}
