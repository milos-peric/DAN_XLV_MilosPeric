using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DAN_XLV_MilosPeric.Validation
{
    public class EntryValidation
    {

        public static bool ValidateProductName(string product)
        {
            bool isOnlyLettersAndNumbers = Regex.IsMatch(product, @"^[a-zA-Z0-9 -]+$");
            return isOnlyLettersAndNumbers;
        }

        public static bool ValidateProductNumber(string product)
        {
            bool isValidItemNumberEntry = Regex.IsMatch(product, @"^[0-9]+$");
            return isValidItemNumberEntry;
        }

        public static bool ValidateAmount(int amount)
        {
            if (amount > 0 && amount <= 100)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ValidatePriceAmount(string price)
        {
            if (double.Parse(price) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ValidatePriceFormat(string price)
        {
            bool isValidPriceFormatEntry = Regex.IsMatch(price.ToString(), @"^[0-9,.]+$");
            return isValidPriceFormatEntry;
        }
    }
}
