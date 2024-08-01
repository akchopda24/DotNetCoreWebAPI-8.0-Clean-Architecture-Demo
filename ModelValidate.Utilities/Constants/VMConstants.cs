using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelValidate.Utilities.Constants
{
    //Use this class for constants message, variable or any comman thing declaration to use in multiple places
    public static class VMConstants
    {
        /// <summary>
        /// We have declared all constant variable property here
        /// </summary>
        public static readonly List<string> PhonePatterns = new List<string>
        {
            // Detect phone numbers
            @"\+?\d{0,3}[-.\s]?\(?\d{1,4}\)?[-.\s]?\d{1,4}[-.\s]?\d{1,9}", 
            // Detect english/hindi words
            //@"\b(ONE|TWO|THREE|FOUR|FIVE|SIX|SEVEN|EIGHT|NINE|ZERO|एक|दो|तीन|चार|पांच|छह|सात|आठ|नौ|शून्य)+\b",
            @"\b(ONE|TWO|THREE|FOUR|FIVE|SIX|SEVEN|EIGHT|NINE|ZERO|एक|दो|तीन|चार|पांच|छह|सात|आठ|नौ|शून्य)",
            // Detect numbers
            @"-?\d+(\.\d+)?"
        };

        public const bool Validated = true;
        public const bool NotValidated = false;
        public const string NotValidatedMsg = "Input message is not validated!";
        public const string ValidatedMsg = "Input message is validated!";
        public const string DataFound = "Data found!";
        public const string DataNotFound = "Data not found!";
    }
}
