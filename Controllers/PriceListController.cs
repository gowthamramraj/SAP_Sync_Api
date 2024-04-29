using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Http;
using System.Web.UI;
using SAPbobsCOM;
using SAP_Sync_Api.Models;

namespace SAP_Sync_Api.Controllers
{
    public class PriceListController : ApiController
    {
        public Company oCompany;
        public Recordset Orecordset;
        Int32 lRetCode;
        Int32 lErrCode;
        String sErrMsg;
        int RetVal;
        String tempStr;
        int ErrCode;
        String ErrMsg;
        String a;
        Connectivity con = new Connectivity();
        public Documents Opln = null;

        private Recordset oRecSet = null;
        private Recordset oRecSet1 = null;
        public string response { get; set; }

        // GET: PriceList
        [HttpPost]
        [Route("newitem/price")]
        public string InsertNewPrice(PriceListMaster PriceListData)
        {

            oCompany = new Company();
            oCompany.Server = con.server;
            oCompany.CompanyDB = con.db_name;

            if (con.db_version.ToString() == "2008")
            {
                oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2008;
            }
            else if (con.db_version.ToString() == "2012")
            {
                oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2012;
            }
            else if (con.db_version.ToString() == "2014")
            {
                oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2014;
            }
            else if (con.db_version.ToString() == "2016")
            {
                oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2016;
            }
            else if (con.db_version.ToString() == "2017")
            {
                oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2017;
            }
            oCompany.DbUserName = con.db_username;
            oCompany.DbPassword = con.db_password;
            oCompany.UserName = con.sap_username;
            oCompany.Password = con.sap_password;
            oCompany.language = BoSuppLangs.ln_English;
            oCompany.UseTrusted = false;

            oCompany.LicenseServer = con.sap_licenseserver;
            lRetCode = oCompany.Connect();


            if (lRetCode != 0)
            {
                oCompany.GetLastError(out lErrCode, out sErrMsg);
                if (oCompany != null)
                {
                    if (oCompany.Connected) oCompany.Disconnect(); 
                }
                return sErrMsg;
            }
            else
            {
                return NewItemPrice(PriceListData);
            }
        }

        [HttpPost]
        [Route("update/price")]
        public IHttpActionResult UpdateNewPrice(PriceList PriceListData)
        {

            oCompany = new Company();
            oCompany.Server = con.server;
            oCompany.CompanyDB = con.db_name;

            if (con.db_version.ToString() == "2008")
            {
                oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2008;
            }
            else if (con.db_version.ToString() == "2012")
            {
                oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2012;
            }
            else if (con.db_version.ToString() == "2014")
            {
                oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2014;
            }
            else if (con.db_version.ToString() == "2016")
            {
                oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2016;
            }
            else if (con.db_version.ToString() == "2017")
            {
                oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2017;
            }
            oCompany.DbUserName = con.db_username;
            oCompany.DbPassword = con.db_password;
            oCompany.UserName = con.sap_username;
            oCompany.Password = con.sap_password;
            oCompany.language = BoSuppLangs.ln_English;
            oCompany.UseTrusted = false;

            oCompany.LicenseServer = con.sap_licenseserver;
            lRetCode = oCompany.Connect();


            if (lRetCode != 0)
            {
                oCompany.GetLastError(out lErrCode, out sErrMsg);
                if (oCompany != null)
                {
                    if (oCompany.Connected) oCompany.Disconnect();
                    Console.WriteLine(lErrCode + " - " + sErrMsg);
                    return Content(System.Net.HttpStatusCode.BadRequest, sErrMsg);
                }
                else
                {
                    Console.WriteLine(lErrCode + " - " + sErrMsg);
                    return Content(System.Net.HttpStatusCode.BadRequest, sErrMsg);
                }
                /// required error handle

            }
            else
            {
                try
                {
                    var resp = UpdateItemPrice(PriceListData);
                  //  return resp;
                    return Ok(resp);
                    
                }
                catch (Exception ex)
                {
                    if (oCompany.Connected) oCompany.Disconnect();
                    //return ex.Message.ToString();
                    return Content(System.Net.HttpStatusCode.BadRequest, ex.Message.ToString());
                }



            }

        }

         

        private string NewItemPrice(PriceListMaster PriceListData)
        {
            response = "";
            string division_code = PriceListData.DivisionCode .ToString().ToUpper();
            for (int i = 0; i < PriceListData.oPLMs.Count; i++)
            {
                response = NewPrice(PriceListData.oPLMs[i], division_code);

            }
            if (oCompany.Connected) oCompany.Disconnect();
            return response;


        }


        public string NewPrice(OPLM PriceListData,string division_code)
        {
            if (division_code.ToUpper() != "ACC")
            {
                try
                {
                    SAPbobsCOM.GeneralService oGeneralService;
                    SAPbobsCOM.GeneralData oGeneralData;
                    // Dim oGeneralParams As SAPbobsCOM.GeneralDataParams
                    SAPbobsCOM.GeneralData oChild;
                    SAPbobsCOM.GeneralDataCollection oChildren;
                    SAPbobsCOM.GeneralData oChild1;
                    SAPbobsCOM.GeneralDataCollection oChildren1;
                    SAPbobsCOM.CompanyService sCmp;
                    sCmp = oCompany.GetCompanyService();

                    oGeneralService = sCmp.GetGeneralService("PLM");
                    oGeneralData = oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralData.SetProperty("U_DocDate", DateTime.Now);
                    oGeneralData.SetProperty("U_LstMdyDt", DateTime.Now);
                    oGeneralData.SetProperty("U_LstMdyBy", PriceListData.U_LstMdyBy);
                    oGeneralData.SetProperty("U_Remarks", PriceListData.U_Remarks);
                    oGeneralData.SetProperty("U_ItemCode", PriceListData.U_ItemCode);
                    oGeneralData.SetProperty("U_ItemName", PriceListData.U_ItemName);
                    oGeneralData.SetProperty("U_ItemGrp", PriceListData.U_ItemGrp);
                    oGeneralData.SetProperty("U_Size", PriceListData.U_Size);
                    oGeneralData.SetProperty("U_Style", PriceListData.U_Style);
                    oGeneralData.SetProperty("U_UserSign", PriceListData.U_UserSign);
                    // oGeneralData.SetProperty("U_PurPrice", PriceListData.U_PurPrice);
                    oGeneralData.SetProperty("U_ItmGrpCd", PriceListData.U_ItmGrpCd);
                    oGeneralData.SetProperty("U_Status", PriceListData.U_Status);

                    oCompany.GetNewObjectCode(out tempStr);
                    //  List<INS_PLM1> plm1 = new List<INS_PLM1>();
                    List<PLM1> plm1 = PriceListData.pLM1;
                    for (int index = 0; index <= PriceListData.pLM1.Count - 1; index++)
                    {

                        oChildren = oGeneralData.Child("INS_PLM1");
                        oChild = oChildren.Add();

                        oChild.SetProperty("U_CatalgCode", plm1[index].U_CatalgCode);
                        oChild.SetProperty("U_ItemName", plm1[index].U_ItemName);
                        oChild.SetProperty("U_SubBrand", plm1[index].U_SubBrand);
                        oChild.SetProperty("U_Brand", plm1[index].U_Brand);
                        oChild.SetProperty("U_Remarks", plm1[index].U_Remarks);
                        oChild.SetProperty("U_PrntName", plm1[index].U_PrntName);
                        oChild.SetProperty("U_RowID", plm1[index].U_RowID);
                        oChild.SetProperty("U_Lock", plm1[index].U_Lock);

                        oChildren1 = oGeneralData.Child("INS_PLM2");
                        oChild1 = oChildren1.Add();


                        //int line_id = oChild.GetProperty("LineId");
                        List<PLM2> plm2 = plm1[index].pLM2;
                        for (int i = 0; i <= plm2.Count - 1; i++)
                        {

                            oChild1.SetProperty("U_RowID", index.ToString());
                            //oChild2.SetProperty("U_MRP", plm2[index].U_MRP);
                            oChild1.SetProperty("U_MRP", plm2[i].U_MRP.ToString());
                            oChild1.SetProperty("U_SelPrice", plm2[i].U_SelPrice.ToString());
                            oChild1.SetProperty("U_Dirprice", plm2[i].U_Dirprice.ToString());
                            oChild1.SetProperty("U_Remarks", plm2[i].U_Remarks.ToString());
                            oChild1.SetProperty("U_OMRP", plm2[i].U_OMRP.ToString());
                            oChild1.SetProperty("U_OSelPrice", plm2[i].U_OSelPrice.ToString());
                            oChild1.SetProperty("U_ODirprice", plm2[i].U_ODirprice.ToString());
                            oChild1.SetProperty("U_OFranprice", plm2[i].U_OFranprice.ToString());
                            oChild1.SetProperty("U_DirMRP", plm2[i].U_DirMRP.ToString());
                            oChild1.SetProperty("U_OFranMRP", plm2[i].U_OFranMRP.ToString());
                            oChild1.SetProperty("U_OffMRP", plm2[i].U_OffMRP.ToString());


                        }

                    }
                    oGeneralService.Add(oGeneralData);
                    if (oCompany.Connected) oCompany.Disconnect();
                    return "Success";

                }
                catch (Exception ex)
                {
                    if (oCompany.Connected) oCompany.Disconnect();
                    return ex.Message.ToString();
                }
            }
            else { return ""; }
        }


        private string UpdateItemPrice(PriceList priceData)
        {
            response = "";
            string division_code = priceData.DivisionCode.ToString().ToUpper();
            for (int i = 0; i < priceData.PriceDataList .Count; i++)
            {
                response =   UpdatePrice(priceData.PriceDataList[i], division_code);
            }
            if (oCompany.Connected) oCompany.Disconnect();
            return response;

        }

        private string UpdatePrice(PriceData priceData,string division_code)
        {
            if ((division_code != "ACC") && (division_code != "AKG"))
            {
                try
                {
                    SAPbobsCOM.GeneralService oGeneralService;
                    SAPbobsCOM.GeneralData oGeneralData;
                    SAPbobsCOM.GeneralDataParams oGeneralParams;
                    SAPbobsCOM.GeneralDataCollection oChildren;
                    SAPbobsCOM.CompanyService sCmp;
                    sCmp = oCompany.GetCompanyService();

                    string DoQuery = "select Distinct a.DocEntry from  [@ins_oplm] a inner join [@ins_plm1] b on a.docentry=b.docentry " +
                        "inner join [@ins_plm2] c on a.docentry=c.docentry and b.lineid=c.u_rowid   " +
                        "where u_itemcode='" + priceData.ItemCode + "'";

                    oRecSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRecSet.DoQuery(DoQuery);
                    int DocEntry = oRecSet.Fields.Item("DocEntry").Value;

                    oGeneralService = sCmp.GetGeneralService("PLM");
                    oGeneralData = oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralParams = oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams);
                    oGeneralParams.SetProperty("DocEntry", DocEntry);
                    oGeneralData = oGeneralService.GetByParams(oGeneralParams);
                    oChildren = oGeneralData.Child("INS_PLM2");
                    string DoQuery1 = "";
                    if ((division_code == "ATC") || (division_code == "RRF") || (division_code == "RHL") || (division_code  == "VT") || (division_code == "VG"))
                    {
                        //// to get the linenumber
                        DoQuery1 = "select C.LineId,c.U_MRP,c.U_SelPrice,U_FranMRP,U_Franprice ,U_DirMRP,U_Dirprice from  [@ins_oplm] a inner join [@ins_plm1] b on a.docentry=b.docentry " +
                          "inner join [@ins_plm2] c on a.docentry=c.docentry and b.lineid=c.u_rowid   " +
                          "where  U_State='" + priceData.SalePriceCode + "' AND u_itemcode='" + priceData.ItemCode + "' " +
                          "AND U_BRAND='" + priceData.Brand + "' AND U_SUBBRAND ='" + priceData.SubBrand + "' ";

                    }
                    else
                    {
                        //// to get the linenumber
                        DoQuery1 = "select C.LineId,c.U_MRP,c.U_SelPrice,U_FranMRP,U_Franprice ,U_DirMRP,U_Dirprice from  [@ins_oplm] a inner join [@ins_plm1] b on a.docentry=b.docentry " +
                          "inner join [@ins_plm2] c on a.docentry=c.docentry and b.lineid=c.u_rowid   " +
                          "where  U_State='" + priceData.SalePriceCode + "' AND u_itemcode='" + priceData.ItemCode + "'";
                    }
                    

                    oRecSet1 = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRecSet1.DoQuery(DoQuery1);
                    int LineNum = oRecSet1.Fields.Item("LineId").Value;
                    double OldMrp = oRecSet1.Fields.Item("U_MRP").Value;
                    double OldSelPrice = oRecSet1.Fields.Item("U_SelPrice").Value;
                      
                    oChildren.Item(LineNum - 1).SetProperty("U_OMRP", OldMrp.ToString());
                    oChildren.Item(LineNum - 1).SetProperty("U_OSelPrice", OldSelPrice.ToString());
                    oChildren.Item(LineNum - 1).SetProperty("U_MRP", priceData.MRP.ToString());
                    oChildren.Item(LineNum - 1).SetProperty("U_SelPrice", priceData.SellingPrice.ToString());

                    oGeneralService.Update(oGeneralData);
                    response = response + priceData.ItemCode + " - Updated";
                    return response;
                }
                catch (Exception ex)
                {
                    response = response + " , " + priceData.ItemCode + " - got error - " + ex.Message.ToString();

                    return response;

                }
            }
            else if ((division_code == "AKG"))
            {
                try
                {
                    SAPbobsCOM.GeneralService oGeneralService;
                    SAPbobsCOM.GeneralData oGeneralData;
                    SAPbobsCOM.GeneralDataParams oGeneralParams;
                    SAPbobsCOM.GeneralDataCollection oChildren;
                    SAPbobsCOM.CompanyService sCmp;
                    sCmp = oCompany.GetCompanyService();

                    string DoQuery = "select Distinct a.DocEntry from  [@ins_oplm] a inner join [@ins_plm1] b on a.docentry=b.docentry " +
                        "inner join [@ins_plm2] c on a.docentry=c.docentry and b.lineid=c.u_rowid   " +
                        "where u_itemcode='" + priceData.ItemCode + "'";

                    oRecSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRecSet.DoQuery(DoQuery);
                    int DocEntry = oRecSet.Fields.Item("DocEntry").Value;

                    oGeneralService = sCmp.GetGeneralService("PLM");
                    oGeneralData = oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralParams = oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams);
                    oGeneralParams.SetProperty("DocEntry", DocEntry);
                    oGeneralData = oGeneralService.GetByParams(oGeneralParams);
                    oChildren = oGeneralData.Child("INS_PLM2");

                    //// to get the linenumber
                    string DoQuery1 = "select C.LineId,c.U_MRP,c.U_SelPrice,U_FranMRP,U_Franprice ,U_DirMRP,U_Dirprice from  [@ins_oplm] a inner join [@ins_plm1] b on a.docentry=b.docentry " +
                        "inner join [@ins_plm2] c on a.docentry=c.docentry and b.lineid=c.u_rowid   " +
                        "where  U_State='" + priceData.SalePriceCode + "' AND u_itemcode='" + priceData.ItemCode + "'";


                    oRecSet1 = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRecSet1.DoQuery(DoQuery1);
                    int LineNum = oRecSet1.Fields.Item("LineId").Value;

                    double OldMrp = oRecSet1.Fields.Item("U_MRP").Value;
                    double OldSelPrice = oRecSet1.Fields.Item("U_SelPrice").Value;

                    double OldFranMRP = oRecSet1.Fields.Item("U_FranMRP").Value;
                    double OldFranprice = oRecSet1.Fields.Item("U_Franprice").Value;

                    double OldDirMRP = oRecSet1.Fields.Item("U_DirMRP").Value;
                    double OldDirprice = oRecSet1.Fields.Item("U_Dirprice").Value;

                    oChildren.Item(LineNum - 1).SetProperty("U_OMRP", OldMrp.ToString());
                    oChildren.Item(LineNum - 1).SetProperty("U_OSelPrice", OldSelPrice.ToString());
                    oChildren.Item(LineNum - 1).SetProperty("U_MRP", priceData.MRP.ToString());
                    oChildren.Item(LineNum - 1).SetProperty("U_SelPrice", priceData.SellingPrice.ToString());


                    oChildren.Item(LineNum - 1).SetProperty("U_FranMRP", priceData.MRP.ToString());
                    oChildren.Item(LineNum - 1).SetProperty("U_Franprice", priceData.SellingPrice.ToString());
                    oChildren.Item(LineNum - 1).SetProperty("U_OFranMRP", OldFranMRP.ToString());
                    oChildren.Item(LineNum - 1).SetProperty("U_OFranprice", OldFranprice.ToString());

                    oChildren.Item(LineNum - 1).SetProperty("U_DirMRP", priceData.MRP.ToString());
                    oChildren.Item(LineNum - 1).SetProperty("U_Dirprice", priceData.SellingPrice.ToString());
                    oChildren.Item(LineNum - 1).SetProperty("U_ODirMRP", OldDirMRP.ToString());
                    oChildren.Item(LineNum - 1).SetProperty("U_ODirprice", OldDirprice.ToString());

                    oGeneralService.Update(oGeneralData);
                    response = response + priceData.ItemCode + " - Updated";
                    return response;
                }
                catch (Exception ex)
                {
                    response = response + " , " + priceData.ItemCode + " - got error - " + ex.Message.ToString();
                    return response;

                }

            }
            else if (division_code == "ACC")
            {
                try
                {
                    //Opln = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPriceLists);
                    Items oItem = (Items)oCompany.GetBusinessObject(BoObjectTypes.oItems);
                    if (oItem.GetByKey(priceData.ItemCode))
                    {
                        string DoQuery = "select  *  from opln where listname = '" + priceData.SalePriceCode + " FR" + " '";

                        oRecSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                        oRecSet.DoQuery(DoQuery);
                        int LineNum = oRecSet.Fields.Item("ListNum").Value;
                        string PriceListName = oRecSet.Fields.Item("ListName").Value;


                        for (int i = 0; i < oItem.PriceList.Count; i++)
                        {
                            oItem.PriceList.SetCurrentLine(i);
                            string h = oItem.PriceList.PriceListName.ToString();
                            if (oItem.PriceList.PriceListName.ToString() == PriceListName)
                            {
                                oItem.PriceList.Price = priceData.SellingPrice;
                                break;
                            }
                        }



                        lRetCode = oItem.Update();

                        if (lRetCode != 0)
                        {
                            oCompany.GetLastError(out ErrCode, out ErrMsg);
                            response = response + "Selling Price Error Code - " + ErrCode + "Error Message - " + ErrMsg + " in " + division_code;
                            // return response;
                        }
                        else
                        {
                            response = response+", " + priceData.ItemCode + " -Selling Price Updated";
                            // return response;
                        }


                        oCompany.GetLastError(out ErrCode, out ErrMsg);

                        string DoQuery1 = "select  *  from opln where listname = '" + priceData.SalePriceCode + " MRP" + " '";

                        oRecSet1 = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                        oRecSet1.DoQuery(DoQuery1);
                        int LineNum1 = oRecSet1.Fields.Item("ListNum").Value;
                        string PriceListName1 = oRecSet1.Fields.Item("ListName").Value;


                        for (int i = 0; i < oItem.PriceList.Count; i++)
                        {
                            oItem.PriceList.SetCurrentLine(i);
                            if (oItem.PriceList.PriceListName.ToString() == PriceListName1)
                            {
                                oItem.PriceList.Price = priceData.MRP;
                                break;
                            }
                        }

                        lRetCode = oItem.Update();
                        if (lRetCode != 0)
                        {
                            oCompany.GetLastError(out ErrCode, out ErrMsg);
                            response = response + "MRP Error Code - " + ErrCode + "Error Message - " + ErrMsg + " in " + division_code;
                            // return response;
                        }
                        else
                        {
                            response = response +", "+ priceData.ItemCode + " -MRP Updated";
                            // return response;
                        }

                    }
                    else
                    {
                        response = response + priceData.ItemCode + " Not exists";
                        return response;
                    }


                    return response;

                }
                catch (Exception ex)
                {
                    response = response + " , " + priceData.ItemCode + " - got error - " + ex.Message.ToString();
                    return response;

                }
            }
            else
            {
                return response;
            }
        }

    }
}