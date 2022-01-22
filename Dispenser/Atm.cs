using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DispenserTask.Models
{
    public class Atm
    {
        public const int MaxBillsCountToProcess = 40;
        public const int MaxCassettes = 5;

        private readonly ICollection<Cassette> _cassettesStore = new Collection<Cassette>();

        /// <summary>
        /// Dispenses the cash.
        /// </summary>
        /// <param name="amount">Amount to cash out.</param>
        /// <returns>Collection of <see cref="Bill"/> objects.</returns>
        public IEnumerable<Bill> Dispense(int amount)
        {
            // 1. Add check for a 'amount' parameter. It must be a positive number. If not, throw the ArgumentException with an elaboration message.
            // 2. Implement an algorithm to take a needed number of bills from '_cassettesStore' to dispense the requested amount.
            // 3. The algorithm has to check the number of bills intended to dispense. If the number is bigger than MaxBillsCountToProcess ArgumentException have to be thrown.

            if (amount <= 0)
                throw new ArgumentException("amount must be a positive number", nameof(amount));

            var theamount = amount;




               List<Bill> bills = new List<Bill>();

           
                foreach (var cassette in _cassettesStore)
                {


                    if (theamount >= cassette.BillsValue)
                    {
                        var number = theamount / cassette.BillsValue;


                        if (cassette.TryTake(number, out IEnumerable<Bill> takenBill))
                            theamount -= number * cassette.BillsValue;

                        bills
                        .AddRange(takenBill);
                    }



                }



            if (bills.Count > MaxBillsCountToProcess)
            {
                throw new ArgumentException("It is impossible to withdraw cash from an ATM*" + $"{Atm.MaxBillsCountToProcess}", nameof(amount));
            }

            return bills;
        }

        /// <summary>
        /// Loads cassettes with bills into ATM.
        /// </summary>
        /// <param name="cassettes">A collection of cassettes to load.</param>
        public void LoadCassettes(IReadOnlyCollection<Cassette> cassettes)
        {
            if (cassettes.Count > MaxCassettes)
            {
                throw new ArgumentException(string.Format("ATM can keep only {0} cassettes.", MaxCassettes), nameof(cassettes));
            }

            foreach (var cassette in cassettes.OrderByDescending(x => x.BillsValue).ToArray())
            {
                _cassettesStore.Add(cassette);
            }
        }
    }
}