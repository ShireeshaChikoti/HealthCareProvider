using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Web;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;

namespace DataAccess
{
    /// <summary>
    /// Provider Detail Class
    /// </summary>
    public class ProviderDetail
    {
        string url = "https://s3-us-west-2.amazonaws.com/bain-coding-challenge/Inpatient_Prospective_Payment_System__IPPS__Provider_Summary_for_the_Top_100_Diagnosis-Related_Groups__DRG__-_FY2011.csv";
        
        /// <summary>
        /// Method to cache providers data to prevent multiple server calls for frequently accessed data
        /// </summary>
        /// <returns>Returns cached version of data table</returns>
        public DataTable GetCachedProvidersData()
        {
            string cacheKey = "ProvidersData";
            object cacheItem = HttpRuntime.Cache[cacheKey] as DataTable;
            if (cacheItem == null)
            {
                cacheItem = GetProviderDetails();
                HttpRuntime.Cache.Insert(cacheKey, cacheItem, null,
                                        System.Web.Caching.Cache.NoAbsoluteExpiration,
                                        TimeSpan.FromMinutes(2));
            }
            return (DataTable)cacheItem;
        }

        /// <summary>
        /// Method to access csv data source and return providers data
        /// </summary>
        /// <returns>returns providers data in datatable format</returns>
        public DataTable GetProviderDetails()
        {
            DataTable providerData = new DataTable();
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            #region Retrieve data from csv file
            using (TextFieldParser sr = new TextFieldParser(resp.GetResponseStream()))
            {
                sr.HasFieldsEnclosedInQuotes = true;
                sr.SetDelimiters(",");
                bool setColumns = true;

                while (!sr.EndOfData)
                {
                    // Fulltext = sr.ReadToEnd().ToString(); //read full file text  
                    string[] rows = sr.ReadFields();//Fulltext.Split('\n'); //split full file text into rows  
                    if (setColumns)
                    {
                        for (int i = 2; i < rows.Count(); i++)
                        {
                            //add headers 
                            if (rows[i] == "Total Discharges")
                            {
                                providerData.Columns.Add(rows[i], typeof(Int32));
                            }
                            else
                            {
                                providerData.Columns.Add(rows[i]);
                            }
                        }
                    }
                    else
                    {
                        DataRow dr = providerData.NewRow();
                        int rowNumber = 0;
                        for (int i = 2; i < rows.Count(); i++)
                        {
                            if (i == 8)
                            {
                                dr[rowNumber++] = Convert.ToInt32(rows[i]);
                            }
                            else
                            {
                                dr[rowNumber++] = rows[i].ToString();
                            }
                        }
                        providerData.Rows.Add(dr); //add other rows
                    }
                    setColumns = false;
                }
            }

            #endregion 

            return providerData;
        }

        /// <summary>
        /// Method to filter providers data based on given search criteria
        /// </summary>
        /// <param name="criteria">search criteria</param>
        /// <returns>Returns filtered Providers data in Json format</returns>
        public string GetFilteredProviderData(SearchCriteria criteria)
        {
            DataTable dt = GetCachedProvidersData();
            DataTable selectedTable;
            EnumerableRowCollection<DataRow> rows;
            if (!string.IsNullOrEmpty(criteria.state))
            {
                rows = dt.AsEnumerable()
                            .Where(r => r.Field<int>("Total Discharges") >= criteria.min_discharges
                            && r.Field<int>("Total Discharges") <= criteria.max_discharges
                            && decimal.Parse(r.Field<string>("Average Covered Charges"), NumberStyles.Currency, CultureInfo.CurrentCulture.NumberFormat) >= criteria.min_average_covered_charges
                            && decimal.Parse(r.Field<string>("Average Covered Charges"), NumberStyles.Currency, CultureInfo.CurrentCulture.NumberFormat) <= criteria.max_average_covered_charges
                            && decimal.Parse(r.Field<string>("Average Medicare Payments"), NumberStyles.Currency, CultureInfo.CurrentCulture.NumberFormat) >= criteria.min_average_medicare_payments
                            && decimal.Parse(r.Field<string>("Average Medicare Payments"), NumberStyles.Currency, CultureInfo.CurrentCulture.NumberFormat) <= criteria.max_average_medicare_payments
                            && r.Field<string>("Provider State") == criteria.state);
            }
            else
            {
                rows = dt.AsEnumerable()
                           .Where(r => r.Field<int>("Total Discharges") >= criteria.min_discharges
                            && r.Field<int>("Total Discharges") <= criteria.max_discharges
                            && decimal.Parse(r.Field<string>("Average Covered Charges"), NumberStyles.Currency, CultureInfo.CurrentCulture.NumberFormat) >= criteria.min_average_covered_charges
                            && decimal.Parse(r.Field<string>("Average Covered Charges"), NumberStyles.Currency, CultureInfo.CurrentCulture.NumberFormat) <= criteria.max_average_covered_charges
                            && decimal.Parse(r.Field<string>("Average Medicare Payments"), NumberStyles.Currency, CultureInfo.CurrentCulture.NumberFormat) >= criteria.min_average_medicare_payments
                            && decimal.Parse(r.Field<string>("Average Medicare Payments"), NumberStyles.Currency, CultureInfo.CurrentCulture.NumberFormat) <= criteria.max_average_medicare_payments);
            }
            selectedTable = rows.Any() ? rows.CopyToDataTable() : null;

            string jsonResult = DataTableToJSON(selectedTable);
            return jsonResult;
        }

        /// <summary>
        /// Method to convert datatable to Json Format
        /// </summary>
        /// <param name="table">input datatable table with providers data</param>
        /// <returns>returns json string of providers data</returns>
        public string DataTableToJSON(DataTable table)
        {
            string JSONString = string.Empty;
            if (table != null)
            {
                JSONString = JsonConvert.SerializeObject(table);
            }
            return JSONString;
        }
    }
}
