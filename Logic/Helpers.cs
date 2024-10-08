using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Logic
{
    public static class Helpers
    {
        public const string Admin_Role = "Admin";
        public const string Customer_Role = "Customer";
    }

    public readonly struct Currency
    {
        private readonly decimal _amount;

        public Currency(decimal amount)
        {
            _amount = amount;
        }

        public decimal Amount => _amount;

        public override string ToString()
            => $"${_amount}";
    }

    public class CurrencyConverter : ValueConverter<Currency, decimal>
    {
        public CurrencyConverter()
            : base(
                v => v.Amount,
                v => new Currency(v))
        {
        }
    }
}
