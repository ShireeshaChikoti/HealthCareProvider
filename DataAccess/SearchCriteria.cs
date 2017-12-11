using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    /// <summary>
    /// Search Criteria Entity
    /// </summary>
    public class SearchCriteria
    {
        /// <summary>
        /// Default constructor to set default values for search criteria
        /// </summary>
        public SearchCriteria()
        {
            max_discharges = int.MaxValue;
            min_discharges = int.MinValue;
            max_average_covered_charges = decimal.MaxValue;
            min_average_covered_charges = 0;
            max_average_medicare_payments = decimal.MaxValue;
            min_average_medicare_payments = 0;
            state = string.Empty;
        }

        /// <summary>
        /// Gets or sets max_discharges
        /// </summary>
        public Int32 max_discharges { get; set; }

        /// <summary>
        /// Gets or sets min_discharges
        /// </summary>
        public Int32 min_discharges { get; set; }

        /// <summary>
        /// Gets or sets max_average_covered_charges
        /// </summary>
        public decimal max_average_covered_charges { get; set; }

        /// <summary>
        /// Gets or sets min_average_covered_charges
        /// </summary>
        public decimal min_average_covered_charges { get; set; }

        /// <summary>
        /// Gets or sets max_average_medicare_payments
        /// </summary>
        public decimal max_average_medicare_payments { get; set; }

        /// <summary>
        /// Gets or sets min_average_medicare_payments
        /// </summary>
        public decimal min_average_medicare_payments { get; set; }

        /// <summary>
        /// Gets or sets state
        /// </summary>
        public string state { get; set; }
    }
}
