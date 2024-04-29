using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAP_Sync_Api.Models
{

 

    public class PriceListMaster
    {
        public string DivisionCode { get; set; }
        public List<OPLM> oPLMs { get; set; }

    }

    public class OPLM
    {
        public Nullable<System.DateTime> U_LstMdyDt { get; set; }
        public Nullable<System.DateTime> U_DocDate { get; set; }
        public string U_Remarks { get; set; }
        public string U_Status { get; set; }
        public string U_LstMdyBy { get; set; }
        public string U_ItemCode { get; set; }
        public string U_ItemName { get; set; }
        public string U_ModBy { get; set; }
        public string U_ItemGrp { get; set; }
        public string U_Size { get; set; }
        public string U_Style { get; set; }
        public string U_UserSign { get; set; }
        public Nullable<decimal> U_PurPrice { get; set; }
        public string U_ItmGrpCd { get; set; }
        public string U_PriLstNa { get; set; }
        public string U_BaseList { get; set; }
        public Nullable<decimal> U_SalFact { get; set; }
        public Nullable<decimal> U_PurFact { get; set; }
        public Nullable<System.DateTime> U_EffDate { get; set; }
        public string U_PreparedBy { get; set; }
        public string U_PreBy { get; set; }
        public List<PLM1> pLM1 { get; set; }
    }

    public class PLM1
    {
        public string U_CatalgCode { get; set; }
        public string U_ItemName { get; set; }
        public string U_Brand { get; set; }
        public string U_SubBrand { get; set; }
        public string U_Remarks { get; set; }
        public string U_RowID { get; set; }
        public string U_Lock { get; set; }
        public string U_PrntName { get; set; }
        public string U_OodoCode { get; set; }
        public List<PLM2> pLM2 { get; set; }
    }

    public class PLM2
    {
        public string U_State { get; set; }
        public string U_RowID { get; set; }
        public Nullable<decimal> U_MRP { get; set; }
        public Nullable<decimal> U_SelPrice { get; set; }
        public Nullable<decimal> U_Dirprice { get; set; }
        public Nullable<decimal> U_Franprice { get; set; }
        public string U_Remarks { get; set; }
        public Nullable<decimal> U_OMRP { get; set; }
        public Nullable<decimal> U_OSelPrice { get; set; }
        public Nullable<decimal> U_ODirprice { get; set; }
        public Nullable<decimal> U_OFranprice { get; set; }
        public Nullable<decimal> U_DirMRP { get; set; }
        public Nullable<decimal> U_FranMRP { get; set; }
        public Nullable<decimal> U_ODirMRP { get; set; }
        public Nullable<decimal> U_OFranMRP { get; set; }
        public Nullable<decimal> U_OffMRP { get; set; }
    }

    public class INS_OPLM
    {
        public int DocEntry { get; set; }
        public Nullable<int> DocNum { get; set; }
        public Nullable<int> Period { get; set; }
        public Nullable<short> Instance { get; set; }
        public Nullable<int> Series { get; set; }
        public string Handwrtten { get; set; }
        public string Canceled { get; set; }
        public string Object { get; set; }
        public Nullable<int> LogInst { get; set; }
        public Nullable<int> UserSign { get; set; }
        public string Transfered { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<short> CreateTime { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<short> UpdateTime { get; set; }
        public string DataSource { get; set; }
        public string RequestStatus { get; set; }
        public string Creator { get; set; }
        public string Remark { get; set; }
        public Nullable<System.DateTime> U_LstMdyDt { get; set; }
        public Nullable<System.DateTime> U_DocDate { get; set; }
        public string U_Remarks { get; set; }
        public string U_Status { get; set; }
        public string U_LstMdyBy { get; set; }
        public string U_ItemCode { get; set; }
        public string U_ItemName { get; set; }
        public string U_ModBy { get; set; }
        public string U_ItemGrp { get; set; }
        public string U_Size { get; set; }
        public string U_Style { get; set; }
        public string U_UserSign { get; set; }
        public Nullable<decimal> U_PurPrice { get; set; }
        public string U_ItmGrpCd { get; set; }
        public string U_PriLstNa { get; set; }
        public string U_BaseList { get; set; }
        public Nullable<decimal> U_SalFact { get; set; }
        public Nullable<decimal> U_PurFact { get; set; }
        public Nullable<System.DateTime> U_EffDate { get; set; }
        public string U_PreparedBy { get; set; }
        public string U_PreBy { get; set; }
    }

    public class INS_PLM1
    {
        public int DocEntry { get; set; }
        public int LineId { get; set; }
        public Nullable<int> VisOrder { get; set; }
        public string Object { get; set; }
        public Nullable<int> LogInst { get; set; }
        public string U_CatalgCode { get; set; }
        public string U_ItemName { get; set; }
        public string U_Brand { get; set; }
        public string U_SubBrand { get; set; }
        public string U_Remarks { get; set; }
        public string U_RowID { get; set; }
        public string U_Lock { get; set; }
        public string U_PrntName { get; set; }
        public string U_OodoCode { get; set; }
    }

    public class INS_PLM2
    {
        public int DocEntry { get; set; }
        public int LineId { get; set; }
        public Nullable<int> VisOrder { get; set; }
        public string Object { get; set; }
        public Nullable<int> LogInst { get; set; }
        public string U_State { get; set; }
        public string U_RowID { get; set; }
        public Nullable<decimal> U_MRP { get; set; }
        public Nullable<decimal> U_SelPrice { get; set; }
        public Nullable<decimal> U_Dirprice { get; set; }
        public Nullable<decimal> U_Franprice { get; set; }
        public string U_Remarks { get; set; }
        public Nullable<decimal> U_OMRP { get; set; }
        public Nullable<decimal> U_OSelPrice { get; set; }
        public Nullable<decimal> U_ODirprice { get; set; }
        public Nullable<decimal> U_OFranprice { get; set; }
        public Nullable<decimal> U_DirMRP { get; set; }
        public Nullable<decimal> U_FranMRP { get; set; }
        public Nullable<decimal> U_ODirMRP { get; set; }
        public Nullable<decimal> U_OFranMRP { get; set; }
        public Nullable<decimal> U_OffMRP { get; set; }
    }
     
}