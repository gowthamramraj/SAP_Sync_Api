using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAP_Sync_Api.Models
{
    public enum Division 
    {
        RHL,
        RR,
        ACC,
        RRF,
        AKG,
        VT,
        ATC,
        TARA,
        VG 
    }


    public class PriceData
    {
        //public string DivisionCode { get; set; }
        public string ItemCode { get; set; }
        public string Brand { get; set; }
        public string SubBrand { get; set; }
        public string SalePriceCode { get; set; }
        public double SellingPrice { get; set; }
        public double MRP { get; set; } 
        public double FranPrice { get; set; } 
        public double FranMRP { get; set; } 
        public double DirPrice { get; set; } 
        public double DirMRP { get; set; }
    }

    public class PriceList 
    {
        public string DivisionCode { get; set; }
        public List<PriceData> PriceDataList { get; set; }
    }
}