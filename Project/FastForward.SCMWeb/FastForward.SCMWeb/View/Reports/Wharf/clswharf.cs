using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Data;
using FF.BusinessObjects.CustService;
using CrystalDecisions.CrystalReports.Engine;
using System.ComponentModel;
using FastMember;
using FF.BusinessObjects;
using FF.BusinessObjects.InventoryNew;


namespace FastForward.SCMWeb.View.Reports.Wharf
{
    public class clswharf
    {

        public FastForward.SCMWeb.View.Reports.Wharf.custom_val_declear _cus_val_declar = new FastForward.SCMWeb.View.Reports.Wharf.custom_val_declear();
        public FastForward.SCMWeb.View.Reports.Wharf.cus_ele _cus_ele = new FastForward.SCMWeb.View.Reports.Wharf.cus_ele();
        public FastForward.SCMWeb.View.Reports.Wharf.custom_workingsheet _cus_work = new FastForward.SCMWeb.View.Reports.Wharf.custom_workingsheet();
        public FastForward.SCMWeb.View.Reports.Wharf.custom_workingsheetcat _cus_workcat = new FastForward.SCMWeb.View.Reports.Wharf.custom_workingsheetcat();
        public FastForward.SCMWeb.View.Reports.Wharf.custom_workingsheetitm _cus_workitm = new FastForward.SCMWeb.View.Reports.Wharf.custom_workingsheetitm();
        public FastForward.SCMWeb.View.Reports.Wharf.cargo_imports _cargo_imp = new FastForward.SCMWeb.View.Reports.Wharf.cargo_imports();
        public FastForward.SCMWeb.View.Reports.Wharf.CVWharf_Working_Sheet_new _cus_work_new = new FastForward.SCMWeb.View.Reports.Wharf.CVWharf_Working_Sheet_new();
        public FastForward.SCMWeb.View.Reports.Wharf.GoodsDeclaration _goods_declaration = new FastForward.SCMWeb.View.Reports.Wharf.GoodsDeclaration();
        public FastForward.SCMWeb.View.Reports.Wharf.GoodsDeclarationSheet _goods_declarationsheet = new FastForward.SCMWeb.View.Reports.Wharf.GoodsDeclarationSheet();
        public FastForward.SCMWeb.View.Reports.Wharf.GoodsDeclarationII _goods_declarationII = new FastForward.SCMWeb.View.Reports.Wharf.GoodsDeclarationII();
        public FastForward.SCMWeb.View.Reports.Wharf.GoodsDeclarationIII _goods_declarationIII = new FastForward.SCMWeb.View.Reports.Wharf.GoodsDeclarationIII();
        public FastForward.SCMWeb.View.Reports.Wharf.GoodsDeclarationSheetother _goods_declarationsheetother = new FastForward.SCMWeb.View.Reports.Wharf.GoodsDeclarationSheetother();
        public FastForward.SCMWeb.View.Reports.Wharf.GoodsDeclarationSheetRebond _goods_declarationsheetRebond = new FastForward.SCMWeb.View.Reports.Wharf.GoodsDeclarationSheetRebond();
        public FastForward.SCMWeb.View.Reports.Wharf.ASTReport _cusdec_assessment = new FastForward.SCMWeb.View.Reports.Wharf.ASTReport();
        public FastForward.SCMWeb.View.Reports.Wharf.ATSReportII _cusdec_assessment2 = new FastForward.SCMWeb.View.Reports.Wharf.ATSReportII();
        public FastForward.SCMWeb.View.Reports.Wharf.Exbond2 _cusdec_Exbond2 = new FastForward.SCMWeb.View.Reports.Wharf.Exbond2();
        public FastForward.SCMWeb.View.Reports.Wharf.Tobond2 _cusdec_Tobond2 = new FastForward.SCMWeb.View.Reports.Wharf.Tobond2();
        public FastForward.SCMWeb.View.Reports.Wharf.ASTAccount _cusdec_assessment3 = new FastForward.SCMWeb.View.Reports.Wharf.ASTAccount();
        public FastForward.SCMWeb.View.Reports.Wharf.Rebond2 _cusdec_Rebond2 = new FastForward.SCMWeb.View.Reports.Wharf.Rebond2();
        //Dulaj 2018/Dec/10
        public FastForward.SCMWeb.View.Reports.Wharf.ShipmentTracker _shipmentTracker = new FastForward.SCMWeb.View.Reports.Wharf.ShipmentTracker();
        public FastForward.SCMWeb.View.Reports.Wharf.EntryClearingClrUser _entryClearingClrUser = new FastForward.SCMWeb.View.Reports.Wharf.EntryClearingClrUser();
        public FastForward.SCMWeb.View.Reports.Wharf.EntryClearingCompany _entryClearingClrcom = new FastForward.SCMWeb.View.Reports.Wharf.EntryClearingCompany();
        public FastForward.SCMWeb.View.Reports.Wharf.EntryClearingShipping _entryClearingClrshipping = new FastForward.SCMWeb.View.Reports.Wharf.EntryClearingShipping();
        //Dulaj 2018/Dec/20
        public FastForward.SCMWeb.View.Reports.Wharf.ValueDeclarationReportNew _valueDeclarationReport = new FastForward.SCMWeb.View.Reports.Wharf.ValueDeclarationReportNew();
        Services.Base bsObj;
        public clswharf()
        {
            bsObj = new Services.Base();

        }




        public void ValueDeclaration(string EntryNo, string com)
        {
            bsObj = new Services.Base();
            DataTable alldata = new DataTable();
            DataTable Param = new DataTable();
            DataRow dr;
            decimal costamm = 0;
            decimal frgtamm = 0;
            decimal insuamm = 0;
            decimal outhamm = 0;
            int i = 0;
            string type = "";

            Param.Columns.Add("CostAmmount", typeof(decimal));
            Param.Columns.Add("InsuAmmount", typeof(decimal));
            Param.Columns.Add("FrgtAmmount", typeof(decimal));
            Param.Columns.Add("OthAmmount", typeof(decimal));


            DataTable Hdrdata = bsObj.CHNLSVC.CustService.GetCusdecHDRData(EntryNo, com);
            alldata = bsObj.CHNLSVC.CustService.GetCusValuDecar(EntryNo, com);

            foreach (DataRow dtRow in alldata.Rows)
            {
                type = alldata.Rows[i].Field<string>(20);

                if (type == "COST")
                {
                    costamm = alldata.Rows[i].Field<decimal>(19);
                }
                if (type == "INSU")
                {
                    insuamm = alldata.Rows[i].Field<decimal>(19);
                }
                if (type == "FRGT")
                {
                    frgtamm = alldata.Rows[i].Field<decimal>(19);
                }
                if (type == "OTH")
                {
                    outhamm = alldata.Rows[i].Field<decimal>(19);
                }
                i++;

            }
            dr = Param.NewRow();
            dr["CostAmmount"] = costamm;
            dr["InsuAmmount"] = insuamm;
            dr["FrgtAmmount"] = frgtamm;
            dr["OthAmmount"] = outhamm;
            Param.Rows.Add(dr);


            _cus_val_declar.Database.Tables["declar_valus"].SetDataSource(Hdrdata);
            _cus_val_declar.Database.Tables["Param"].SetDataSource(Param);
        }
        public void CustomElereport(string company, string docno, string type, string user)
        {
            int i = 0;
            decimal CID = 0;
            decimal EIC = 0;
            decimal PAL = 0;
            decimal NBT = 0;
            decimal VAT = 0;
            decimal XID = 0;
            decimal COM = 0;
            decimal TOTAL = 0;
            string elecode = "";
            string FDocNo = "";
            string remark = "";
            string refno = "";
            string termsp1 = "";
            string termsp2 = "";
            string fileno = "";
            string finarefno = "";

            DataTable data = bsObj.CHNLSVC.CustService.GetCustomElements(company, docno, type);
            if (data != null)
            {
                foreach (DataRow dtRow in data.Rows)
                {
                    elecode = data.Rows[i].Field<string>(3);

                    if (elecode == "VAT")
                    {
                        VAT = data.Rows[i].Field<decimal>(4);
                    }
                    if (elecode == "CID")
                    {
                        CID = data.Rows[i].Field<decimal>(4);
                    }
                    if (elecode == "EIC")
                    {
                        EIC = data.Rows[i].Field<decimal>(4);
                    }
                    if (elecode == "PAL")
                    {
                        PAL = data.Rows[i].Field<decimal>(4);
                    }
                    if (elecode == "NBT")
                    {
                        NBT = data.Rows[i].Field<decimal>(4);
                    }
                    if (elecode == "XID")
                    {
                        XID = data.Rows[i].Field<decimal>(4);
                    }
                    if (elecode == "COM")
                    {
                        COM = data.Rows[i].Field<decimal>(4);
                    }

                    FDocNo = data.Rows[i].Field<string>(9);
                    refno = data.Rows[i].Field<string>(10);
                    remark = data.Rows[i].Field<string>(2);
                    termsp1 = data.Rows[i].Field<string>(5);
                    termsp2 = data.Rows[i].Field<string>(6);
                    fileno = data.Rows[i].Field<string>(7);
                    finarefno = data.Rows[i].Field<string>(8);


                    i++;
                }
                TOTAL = COM + XID + NBT + PAL + EIC + CID + VAT;

                DataTable Param = new DataTable();
                DataRow dr;
                Param.Columns.Add("ReffeNumber", typeof(string));
                Param.Columns.Add("CusRefNumber", typeof(string));
                Param.Columns.Add("Remark", typeof(string));
                Param.Columns.Add("DocNumber", typeof(string));
                Param.Columns.Add("CID", typeof(decimal));
                Param.Columns.Add("EIC", typeof(decimal));
                Param.Columns.Add("PAL", typeof(decimal));
                Param.Columns.Add("NBT", typeof(decimal));
                Param.Columns.Add("VAT", typeof(decimal));
                Param.Columns.Add("XID", typeof(decimal));
                Param.Columns.Add("COM", typeof(decimal));
                Param.Columns.Add("YEAR", typeof(Int32));
                Param.Columns.Add("Total", typeof(decimal));
                Param.Columns.Add("Number", typeof(string));
                Param.Columns.Add("Finrefno", typeof(string));
                Param.Columns.Add("User", typeof(string));

                dr = Param.NewRow();
                dr["Number"] = termsp1;
                dr["CusRefNumber"] = refno;
                dr["Remark"] = remark;
                dr["DocNumber"] = fileno;
                dr["CID"] = CID;
                dr["EIC"] = EIC;
                dr["PAL"] = PAL;
                dr["NBT"] = NBT;
                dr["VAT"] = VAT;
                dr["XID"] = XID;
                dr["COM"] = COM;
                dr["YEAR"] = DateTime.Now.Year;
                dr["Total"] = TOTAL;
                dr["ReffeNumber"] = FDocNo;
                dr["Finrefno"] = finarefno;
                dr["User"] = user;

                Param.Rows.Add(dr);
                _cus_ele.Database.Tables["Param"].SetDataSource(Param);

            }


        }

        public void CreateWorkingSheet(string docno, string cat, string fileno, string exporter)
        {
            DataTable masterData = new DataTable();
            masterData = bsObj.CHNLSVC.CustService.GetCustomWorkingSheet(docno, cat);
            int i = 0;
            string itmcode = "";
            string cost = "COST";
            decimal costprice = 0;
            string frgt = "FRGT";
            decimal frgtprice = 0;
            string insu = "INSU";
            decimal insuprice = 0;
            string oth = "OTH";
            decimal othprice = 0;
            decimal cif = 0;
            int linenumber = 10000000;



            //itm table
            DataTable itmtable = new DataTable();
            DataRow dritm;
            itmtable.Columns.Add("No", typeof(string));
            itmtable.Columns.Add("HScode", typeof(string));
            itmtable.Columns.Add("ItmDescription", typeof(string));
            itmtable.Columns.Add("Quantity", typeof(decimal));
            itmtable.Columns.Add("FOB", typeof(decimal));
            itmtable.Columns.Add("Freight", typeof(decimal));
            itmtable.Columns.Add("Insurance", typeof(decimal));
            itmtable.Columns.Add("Other", typeof(decimal));
            itmtable.Columns.Add("CIF", typeof(decimal));
            itmtable.Columns.Add("NetMass", typeof(decimal));
            itmtable.Columns.Add("GrossMass", typeof(decimal));
            itmtable.Columns.Add("Packages", typeof(decimal));
            itmtable.Columns.Add("MainHS", typeof(string));
            itmtable.Columns.Add("Itemcode", typeof(string));
            itmtable.Columns.Add("Model", typeof(string));  //Wimal @ 26/06/2018

            if (masterData != null)
            {
                //foreach (DataRow dtRow in masterData.Rows)
                for (int k = 0; k < masterData.Rows.Count * 10; k++)
                {
                    if (i < masterData.Rows.Count)
                        if (linenumber != masterData.Rows[i].Field<Int16>(0))
                        {
                            if (i > 0)
                            {
                                //addrows
                                dritm = itmtable.NewRow();
                                dritm["No"] = Convert.ToInt32(masterData.Rows[i - 1].Field<Int16>(0));
                                dritm["HScode"] = masterData.Rows[i - 1].Field<string>(3);
                                dritm["ItmDescription"] = masterData.Rows[i - 1].Field<string>(2);
                                dritm["Quantity"] = Convert.ToDecimal(masterData.Rows[i - 1].Field<decimal>(4));
                                dritm["FOB"] = costprice;
                                dritm["Freight"] = frgtprice;
                                dritm["Insurance"] = insuprice;
                                dritm["Other"] = othprice;
                                dritm["CIF"] = costprice + frgtprice + insuprice + othprice;
                                dritm["NetMass"] = Convert.ToDecimal(masterData.Rows[i - 1].Field<decimal>(8));
                                dritm["GrossMass"] = Convert.ToDecimal(masterData.Rows[i - 1].Field<double>(7));
                                dritm["Packages"] = masterData.Rows[i - 1].Field<string>(9);
                                if (!string.IsNullOrEmpty(dritm["Packages"].ToString()))
                                {
                                    dritm["Packages"] = Convert.ToDecimal(dritm["Packages"].ToString());
                                }
                                dritm["MainHS"] = masterData.Rows[i - 1].Field<string>(10);
                                dritm["Itemcode"] = masterData.Rows[i - 1]["CUI_ITM_CD"].ToString();
                                dritm["Model"] = masterData.Rows[i - 1]["CUI_MODEL"].ToString(); //Wimal @ 26/06/2018
                                itmtable.Rows.Add(dritm);
                                costprice = 0;
                                frgtprice = 0;
                                insuprice = 0;
                                othprice = 0;
                                //i++;

                            }
                            linenumber = masterData.Rows[i].Field<Int16>(0);
                        }
                        else
                        {
                            if (masterData.Rows[i].Field<string>(5) == cost)
                            {
                                costprice = masterData.Rows[i].Field<decimal>(6);

                            }
                            if (masterData.Rows[i].Field<string>(5) == frgt)
                            {
                                frgtprice = masterData.Rows[i].Field<decimal>(6);

                            }
                            if (masterData.Rows[i].Field<string>(5) == insu)
                            {
                                insuprice = masterData.Rows[i].Field<decimal>(6);

                            }
                            if (masterData.Rows[i].Field<string>(5) == oth)
                            {
                                othprice = masterData.Rows[i].Field<decimal>(6);

                            }

                            if (i == (masterData.Rows.Count - 1))
                            {
                                dritm = itmtable.NewRow();
                                dritm["No"] = Convert.ToInt32(masterData.Rows[i - 1].Field<Int16>(0));
                                dritm["HScode"] = masterData.Rows[i - 1].Field<string>(3);
                                dritm["ItmDescription"] = masterData.Rows[i - 1].Field<string>(2);
                                dritm["Quantity"] = Convert.ToDecimal(masterData.Rows[i - 1].Field<decimal>(4));
                                dritm["FOB"] = costprice;
                                dritm["Freight"] = frgtprice;
                                dritm["Insurance"] = insuprice;
                                dritm["Other"] = othprice;
                                dritm["CIF"] = costprice + frgtprice + insuprice + othprice;
                                dritm["NetMass"] = Convert.ToDecimal(masterData.Rows[i - 1].Field<decimal>(8));
                                dritm["GrossMass"] = Convert.ToDecimal(masterData.Rows[i - 1].Field<double>(7));
                                dritm["Packages"] = masterData.Rows[i - 1].Field<string>(9);
                                if (!string.IsNullOrEmpty(dritm["Packages"].ToString()))
                                {
                                    dritm["Packages"] = Convert.ToDecimal(dritm["Packages"].ToString());
                                }
                                dritm["MainHS"] = masterData.Rows[i - 1].Field<string>(10);
                                dritm["Itemcode"] = masterData.Rows[i - 1]["CUI_ITM_CD"].ToString();
                                dritm["Model"] = masterData.Rows[i - 1]["CUI_MODEL"].ToString(); //Wimal @ 26/06/2018
                                itmtable.Rows.Add(dritm);
                                costprice = 0;
                                frgtprice = 0;
                                insuprice = 0;
                                othprice = 0;
                            }

                            i++;
                        }
                }
            }
            else
            {
                dritm = itmtable.NewRow();
                dritm["No"] = 1;
                dritm["HScode"] = "";
                dritm["ItmDescription"] = "";
                dritm["Quantity"] = 0;
                dritm["FOB"] = 0;
                dritm["Freight"] = 0;
                dritm["Insurance"] = 0;
                dritm["Other"] = 0;
                dritm["CIF"] = 0;
                dritm["NetMass"] = 0;
                dritm["GrossMass"] = 0;
                dritm["Packages"] = 0;
                dritm["MainHS"] = "";
                dritm["Itemcode"] = "";
                dritm["Model"] = ""; //Wimal @ 26/06/2018
                itmtable.Rows.Add(dritm);
                costprice = 0;
                frgtprice = 0;
                insuprice = 0;
                othprice = 0;
            }
            if (masterData != null)
            {
                dritm = itmtable.NewRow();
                dritm["No"] = Convert.ToInt32(masterData.Rows[i - 1].Field<Int16>(0));
                dritm["HScode"] = masterData.Rows[i - 1].Field<string>(3);
                dritm["ItmDescription"] = masterData.Rows[i - 1].Field<string>(2);
                dritm["Quantity"] = Convert.ToDecimal(masterData.Rows[i - 1].Field<decimal>(4));
                dritm["FOB"] = costprice;
                dritm["Freight"] = frgtprice;
                dritm["Insurance"] = insuprice;
                dritm["Other"] = othprice;
                dritm["CIF"] = costprice + frgtprice + insuprice + othprice;
                dritm["NetMass"] = Convert.ToDecimal(masterData.Rows[i - 1].Field<decimal>(8));
                dritm["GrossMass"] = Convert.ToDecimal(masterData.Rows[i - 1].Field<double>(7));
                dritm["Packages"] = masterData.Rows[i - 1].Field<string>(9);
                if (!string.IsNullOrEmpty(dritm["Packages"].ToString()))
                {
                    dritm["Packages"] = Convert.ToDecimal(dritm["Packages"].ToString());
                }
                dritm["MainHS"] = masterData.Rows[i - 1].Field<string>(10);
                dritm["Itemcode"] = masterData.Rows[i - 1]["CUI_ITM_CD"].ToString();
                dritm["Model"] = masterData.Rows[i - 1]["CUI_MODEL"].ToString(); //Wimal @ 26/06/2018
                itmtable.Rows.Add(dritm);
                costprice = 0;
                frgtprice = 0;
                insuprice = 0;
                othprice = 0;
            }



            //DataTable countriesTable = itmtable.AsEnumerable().GroupBy(x => new { HScode = x.Field<string>("HScode") })
            //                 .Select(x => new WorkingSheetData
            //                 {
            //                     HScode = x.Key.HScode,
            //                     Quantity = x.Sum(z => z.Field<decimal>("Quantity"))
            //                 }).PropertiesToDataTable<WorkingSheetData>();


            List<WorkingSheetData> worlist = new List<WorkingSheetData>();
            int j = 0;
            foreach (DataRow itrow in itmtable.Rows)
            {
                var wordta = new WorkingSheetData();
                wordta.CIF = Convert.ToDecimal(itmtable.Rows[j]["CIF"]);
                wordta.FOB = Convert.ToDecimal(itmtable.Rows[j]["FOB"]);
                wordta.Freight = Convert.ToDecimal(itmtable.Rows[j]["Freight"]);
                wordta.GrossMass = Convert.ToDouble(itmtable.Rows[j]["GrossMass"]);
                wordta.HScode = itmtable.Rows[j]["HScode"].ToString();
                wordta.Insurance = Convert.ToDecimal(itmtable.Rows[j]["Insurance"]);
                wordta.ItmDescription = itmtable.Rows[j]["ItmDescription"].ToString();
                wordta.NetMass = Convert.ToDecimal(itmtable.Rows[j]["NetMass"]);
                wordta.No = Convert.ToInt32(itmtable.Rows[j]["No"]);
                wordta.Other = Convert.ToDecimal(itmtable.Rows[j]["Other"]);
                wordta.Packages = Convert.ToDecimal(itmtable.Rows[j]["Packages"]);
                wordta.Quantity = Convert.ToDecimal(itmtable.Rows[j]["Quantity"]);
                wordta.MainHS = itmtable.Rows[j]["MainHS"].ToString();
                wordta.Itemcode = itmtable.Rows[j]["Itemcode"].ToString();
                // wordta.Model = itmtable.Rows[j]["Model"].ToString();
                if (wordta.Other == 0 && wordta.FOB == 0 && wordta.Freight == 0 && wordta.Insurance == 0 && wordta.CIF == 0)
                {

                }
                else
                {
                    worlist.Add(wordta);
                }


                j++;
            }
            foreach (var mainhslist in worlist)
            {
                if (mainhslist.MainHS == mainhslist.HScode)
                {
                    mainhslist.MainHS = "1";
                }
                else
                {
                    mainhslist.MainHS = "2";
                }
            }



            decimal sumfrait = worlist.Sum(x => x.FOB);

            if (sumfrait == 0) sumfrait = 1;
            worlist = worlist.OrderBy(a => a.MainHS).ThenBy(n => n.HScode).ToList();
            var result = worlist
                .GroupBy(l => new { l.No })
    .Select(cl => new WorkingSheetData
    {
        MainHS = cl.First().MainHS.ToString(),
        HScode = cl.First().HScode,
        ItmDescription = cl.First().ItmDescription.ToString(),
        CIF = cl.Sum(c => c.CIF),
        No = cl.First().No,
        Quantity = cl.Sum(c => c.Quantity),
        Freight = cl.Sum(c => c.Freight),
        Insurance = cl.Sum(c => c.Insurance),
        Other = cl.Sum(c => c.Other),
        FOB = cl.Sum(c => c.FOB),
        NetMass = cl.Sum(c => c.NetMass),
        GrossMass = cl.Sum(c => c.GrossMass),
        Packages = Math.Round(((cl.First().Packages / sumfrait) * cl.Sum(c => c.FOB)), 2)
    }).ToList();

            var resultModel = worlist
                .GroupBy(l => new { l.No })
    .Select(cl => new WorkingSheetData
    {
        MainHS = cl.First().MainHS.ToString(),
        HScode = cl.First().HScode,
        //Model = cl.First().Model ,
        ItmDescription = cl.First().ItmDescription.ToString(),
        CIF = cl.Sum(c => c.CIF),
        No = cl.First().No,
        Quantity = cl.Sum(c => c.Quantity),
        Freight = cl.Sum(c => c.Freight),
        Insurance = cl.Sum(c => c.Insurance),
        Other = cl.Sum(c => c.Other),
        FOB = cl.Sum(c => c.FOB),
        NetMass = cl.Sum(c => c.NetMass),
        GrossMass = cl.Sum(c => c.GrossMass),
        Packages = Math.Round(((cl.First().Packages / sumfrait) * cl.Sum(c => c.FOB)), 2)
    }).ToList();
//////////////////////////////////////////////////////////////////////////////////////////////////
            //param table
            DataTable Param = new DataTable();
            DataRow dr;
            Param.Columns.Add("DocNo", typeof(string));
            Param.Columns.Add("Total", typeof(decimal));
            Param.Columns.Add("FileNo", typeof(string));
            Param.Columns.Add("Exporter", typeof(string));
            dr = Param.NewRow();
            decimal tot_sum = result.Sum(a => a.Packages);


            string VAL = tot_sum.ToString();

            string[] X = tot_sum.ToString().Split('.');
            if (X.Length > 0 && X[1].Length > 2)
            {
                VAL = X[0] + "." + X[1].Substring(0, 2);
            }
            tot_sum = Convert.ToDecimal(VAL);
            dr["DocNo"] = docno;
            dr["Total"] = tot_sum;
            dr["FileNo"] = fileno;
            dr["Exporter"] = exporter;
            Param.Rows.Add(dr);          
            _cus_work.Database.Tables["Param"].SetDataSource(Param);
            _cus_work.Database.Tables["CustomWorkingSheet"].SetDataSource(result);

            _cus_workitm.Database.Tables["Param"].SetDataSource(Param);
            _cus_workitm.Database.Tables["CustomWorkingSheet"].SetDataSource(resultModel);           

        }
        public void CreateWorkingSheetCat(string docno, string cat, string fileno, string exporter)
        {
            DataTable masterData = new DataTable();
            masterData = bsObj.CHNLSVC.CustService.GetCustomWorkingSheet(docno, cat);
            int i = 0;
            string itmcode = "";
            string cost = "COST";
            decimal costprice = 0;
            string frgt = "FRGT";
            decimal frgtprice = 0;
            string insu = "INSU";
            decimal insuprice = 0;
            string oth = "OTH";
            decimal othprice = 0;
            decimal cif = 0;
            int linenumber = 10000000;



            //itm table
            DataTable itmtable = new DataTable();
            DataRow dritm;
            itmtable.Columns.Add("No", typeof(string));
            itmtable.Columns.Add("HScode", typeof(string));
            itmtable.Columns.Add("ItmDescription", typeof(string));
            itmtable.Columns.Add("Quantity", typeof(decimal));
            itmtable.Columns.Add("FOB", typeof(decimal));
            itmtable.Columns.Add("Freight", typeof(decimal));
            itmtable.Columns.Add("Insurance", typeof(decimal));
            itmtable.Columns.Add("Other", typeof(decimal));
            itmtable.Columns.Add("CIF", typeof(decimal));
            itmtable.Columns.Add("NetMass", typeof(decimal));
            itmtable.Columns.Add("GrossMass", typeof(decimal));
            itmtable.Columns.Add("Packages", typeof(decimal));
            itmtable.Columns.Add("MainHS", typeof(string));
            itmtable.Columns.Add("Itemcode", typeof(string));
            itmtable.Columns.Add("Cat1", typeof(string));
            itmtable.Columns.Add("Cat2", typeof(string));


            if (masterData != null)
            {
                //foreach (DataRow dtRow in masterData.Rows)
                for (int k = 0; k < masterData.Rows.Count * 10; k++)
                {
                    if (i < masterData.Rows.Count)
                        if (linenumber != masterData.Rows[i].Field<Int16>(0))
                        {
                            if (i > 0)
                            {
                                //addrows
                                dritm = itmtable.NewRow();
                                dritm["No"] = Convert.ToInt32(masterData.Rows[i - 1].Field<Int16>(0));
                                dritm["HScode"] = masterData.Rows[i - 1].Field<string>(3);
                                dritm["ItmDescription"] = masterData.Rows[i - 1].Field<string>(2);
                                dritm["Quantity"] = Convert.ToDecimal(masterData.Rows[i - 1].Field<decimal>(4));
                                dritm["FOB"] = costprice;
                                dritm["Freight"] = frgtprice;
                                dritm["Insurance"] = insuprice;
                                dritm["Other"] = othprice;
                                dritm["CIF"] = costprice + frgtprice + insuprice + othprice;
                                dritm["NetMass"] = Convert.ToDecimal(masterData.Rows[i - 1].Field<decimal>(8));
                                dritm["GrossMass"] = Convert.ToDecimal(masterData.Rows[i - 1].Field<double>(7));
                                dritm["Packages"] = masterData.Rows[i - 1].Field<string>(9);
                                if (!string.IsNullOrEmpty(dritm["Packages"].ToString()))
                                {
                                    dritm["Packages"] = Convert.ToDecimal(dritm["Packages"].ToString());
                                }
                                dritm["MainHS"] = masterData.Rows[i - 1].Field<string>(10);
                                dritm["Itemcode"] = masterData.Rows[i - 1]["CUI_ITM_CD"].ToString();

                                string cat1 = bsObj.CHNLSVC.Financial.GetSIItemCat1(dritm["Itemcode"].ToString());
                                string cat2 = bsObj.CHNLSVC.Financial.GetSIItemCat2(dritm["Itemcode"].ToString());

                                dritm["Cat1"] = cat1;
                                dritm["Cat2"] = cat2;


                                itmtable.Rows.Add(dritm);
                                costprice = 0;
                                frgtprice = 0;
                                insuprice = 0;
                                othprice = 0;
                                //i++;

                            }
                            linenumber = masterData.Rows[i].Field<Int16>(0);
                        }
                        else
                        {
                            if (masterData.Rows[i].Field<string>(5) == cost)
                            {
                                costprice = masterData.Rows[i].Field<decimal>(6);

                            }
                            if (masterData.Rows[i].Field<string>(5) == frgt)
                            {
                                frgtprice = masterData.Rows[i].Field<decimal>(6);

                            }
                            if (masterData.Rows[i].Field<string>(5) == insu)
                            {
                                insuprice = masterData.Rows[i].Field<decimal>(6);

                            }
                            if (masterData.Rows[i].Field<string>(5) == oth)
                            {
                                othprice = masterData.Rows[i].Field<decimal>(6);

                            }

                            if (i == (masterData.Rows.Count - 1))
                            {
                                dritm = itmtable.NewRow();
                                dritm["No"] = Convert.ToInt32(masterData.Rows[i - 1].Field<Int16>(0));
                                dritm["HScode"] = masterData.Rows[i - 1].Field<string>(3);
                                dritm["ItmDescription"] = masterData.Rows[i - 1].Field<string>(2);
                                dritm["Quantity"] = Convert.ToDecimal(masterData.Rows[i - 1].Field<decimal>(4));
                                dritm["FOB"] = costprice;
                                dritm["Freight"] = frgtprice;
                                dritm["Insurance"] = insuprice;
                                dritm["Other"] = othprice;
                                dritm["CIF"] = costprice + frgtprice + insuprice + othprice;
                                dritm["NetMass"] = Convert.ToDecimal(masterData.Rows[i - 1].Field<decimal>(8));
                                dritm["GrossMass"] = Convert.ToDecimal(masterData.Rows[i - 1].Field<double>(7));
                                dritm["Packages"] = masterData.Rows[i - 1].Field<string>(9);
                                if (!string.IsNullOrEmpty(dritm["Packages"].ToString()))
                                {
                                    dritm["Packages"] = Convert.ToDecimal(dritm["Packages"].ToString());
                                }
                                dritm["MainHS"] = masterData.Rows[i - 1].Field<string>(10);
                                dritm["Itemcode"] = masterData.Rows[i - 1]["CUI_ITM_CD"].ToString();
                                string cat1 = bsObj.CHNLSVC.Financial.GetSIItemCat1(dritm["Itemcode"].ToString());
                                string cat2 = bsObj.CHNLSVC.Financial.GetSIItemCat2(dritm["Itemcode"].ToString());

                                dritm["Cat1"] = cat1;
                                dritm["Cat2"] = cat2;
                                itmtable.Rows.Add(dritm);
                                costprice = 0;
                                frgtprice = 0;
                                insuprice = 0;
                                othprice = 0;
                            }

                            i++;
                        }
                }
            }
            else
            {
                dritm = itmtable.NewRow();
                dritm["No"] = 1;
                dritm["HScode"] = "";
                dritm["ItmDescription"] = "";
                dritm["Quantity"] = 0;
                dritm["FOB"] = 0;
                dritm["Freight"] = 0;
                dritm["Insurance"] = 0;
                dritm["Other"] = 0;
                dritm["CIF"] = 0;
                dritm["NetMass"] = 0;
                dritm["GrossMass"] = 0;
                dritm["Packages"] = 0;
                dritm["MainHS"] = "";
                dritm["Itemcode"] = "";
                dritm["Cat1"] = "";
                dritm["Cat2"] = "";
                itmtable.Rows.Add(dritm);
                costprice = 0;
                frgtprice = 0;
                insuprice = 0;
                othprice = 0;
            }
            if (masterData != null)
            {
                dritm = itmtable.NewRow();
                dritm["No"] = Convert.ToInt32(masterData.Rows[i - 1].Field<Int16>(0));
                dritm["HScode"] = masterData.Rows[i - 1].Field<string>(3);
                dritm["ItmDescription"] = masterData.Rows[i - 1].Field<string>(2);
                dritm["Quantity"] = Convert.ToDecimal(masterData.Rows[i - 1].Field<decimal>(4));
                dritm["FOB"] = costprice;
                dritm["Freight"] = frgtprice;
                dritm["Insurance"] = insuprice;
                dritm["Other"] = othprice;
                dritm["CIF"] = costprice + frgtprice + insuprice + othprice;
                dritm["NetMass"] = Convert.ToDecimal(masterData.Rows[i - 1].Field<decimal>(8));
                dritm["GrossMass"] = Convert.ToDecimal(masterData.Rows[i - 1].Field<double>(7));
                dritm["Packages"] = masterData.Rows[i - 1].Field<string>(9);
                if (!string.IsNullOrEmpty(dritm["Packages"].ToString()))
                {
                    dritm["Packages"] = Convert.ToDecimal(dritm["Packages"].ToString());
                }
                dritm["MainHS"] = masterData.Rows[i - 1].Field<string>(10);
                dritm["Itemcode"] = masterData.Rows[i - 1]["CUI_ITM_CD"].ToString();
                string cat1 = bsObj.CHNLSVC.Financial.GetSIItemCat1(dritm["Itemcode"].ToString());
                string cat2 = bsObj.CHNLSVC.Financial.GetSIItemCat2(dritm["Itemcode"].ToString());

                dritm["Cat1"] = cat1;
                dritm["Cat2"] = cat2;
                itmtable.Rows.Add(dritm);
                costprice = 0;
                frgtprice = 0;
                insuprice = 0;
                othprice = 0;
            }



            //DataTable countriesTable = itmtable.AsEnumerable().GroupBy(x => new { HScode = x.Field<string>("HScode") })
            //                 .Select(x => new WorkingSheetData
            //                 {
            //                     HScode = x.Key.HScode,
            //                     Quantity = x.Sum(z => z.Field<decimal>("Quantity"))
            //                 }).PropertiesToDataTable<WorkingSheetData>();


            List<WorkingSheetData> worlist = new List<WorkingSheetData>();
            int j = 0;
            foreach (DataRow itrow in itmtable.Rows)
            {
                var wordta = new WorkingSheetData();
                wordta.CIF = Convert.ToDecimal(itmtable.Rows[j]["CIF"]);
                wordta.FOB = Convert.ToDecimal(itmtable.Rows[j]["FOB"]);
                wordta.Freight = Convert.ToDecimal(itmtable.Rows[j]["Freight"]);
                wordta.GrossMass = Convert.ToDouble(itmtable.Rows[j]["GrossMass"]);
                wordta.HScode = itmtable.Rows[j]["HScode"].ToString();
                wordta.Insurance = Convert.ToDecimal(itmtable.Rows[j]["Insurance"]);
                wordta.ItmDescription = itmtable.Rows[j]["ItmDescription"].ToString();
                wordta.NetMass = Convert.ToDecimal(itmtable.Rows[j]["NetMass"]);
                wordta.No = Convert.ToInt32(itmtable.Rows[j]["No"]);
                wordta.Other = Convert.ToDecimal(itmtable.Rows[j]["Other"]);
                wordta.Packages = Convert.ToDecimal(itmtable.Rows[j]["Packages"]);
                wordta.Quantity = Convert.ToDecimal(itmtable.Rows[j]["Quantity"]);
                wordta.MainHS = itmtable.Rows[j]["MainHS"].ToString();
                wordta.Itemcode = itmtable.Rows[j]["Itemcode"].ToString();
                wordta.Cat1 = itmtable.Rows[j]["Cat1"].ToString();
                wordta.Cat2 = itmtable.Rows[j]["Cat2"].ToString();
                if (wordta.Other == 0 && wordta.FOB == 0 && wordta.Freight == 0 && wordta.Insurance == 0 && wordta.CIF == 0)
                {

                }
                else
                {
                    worlist.Add(wordta);
                }


                j++;
            }
            foreach (var mainhslist in worlist)
            {
                if (mainhslist.MainHS == mainhslist.HScode)
                {
                    mainhslist.MainHS = "1";
                }
                else
                {
                    mainhslist.MainHS = "2";
                }
            }



            decimal sumfrait = worlist.Sum(x => x.FOB);

            if (sumfrait == 0) sumfrait = 1;
            worlist = worlist.OrderBy(a => a.MainHS).ThenBy(n => n.HScode).ToList();
            var result = worlist
                .GroupBy(l => new { l.No })
    .Select(cl => new WorkingSheetData
    {
        MainHS = cl.First().MainHS.ToString(),
        HScode = cl.First().HScode,
        ItmDescription = cl.First().ItmDescription.ToString(),
        CIF = cl.Sum(c => c.CIF),
        No = cl.First().No,
        Quantity = cl.Sum(c => c.Quantity),
        Freight = cl.Sum(c => c.Freight),
        Insurance = cl.Sum(c => c.Insurance),
        Other = cl.Sum(c => c.Other),
        FOB = cl.Sum(c => c.FOB),
        NetMass = cl.Sum(c => c.NetMass),
        GrossMass = cl.Sum(c => c.GrossMass),
        Packages = Math.Round(((cl.First().Packages / sumfrait) * cl.Sum(c => c.FOB)), 2),
        Cat1 = cl.First().Cat1.ToString(),
        Cat2 = cl.First().Cat2.ToString(),
    }).ToList();

            //param table
            DataTable Param = new DataTable();
            DataRow dr;
            Param.Columns.Add("DocNo", typeof(string));
            Param.Columns.Add("Total", typeof(decimal));
            Param.Columns.Add("FileNo", typeof(string));
            Param.Columns.Add("Exporter", typeof(string));
            dr = Param.NewRow();
            decimal tot_sum = result.Sum(a => a.Packages);


            string VAL = tot_sum.ToString();

            string[] X = tot_sum.ToString().Split('.');
            if (X.Length > 0 && X[1].Length > 2)
            {
                VAL = X[0] + "." + X[1].Substring(0, 2);
            }
            tot_sum = Convert.ToDecimal(VAL);
            dr["DocNo"] = docno;
            dr["Total"] = tot_sum;
            dr["FileNo"] = fileno;
            dr["Exporter"] = exporter;
            Param.Rows.Add(dr);

            _cus_workcat.Database.Tables["Param"].SetDataSource(Param);
            _cus_workcat.Database.Tables["CustomWorkingSheet"].SetDataSource(result);

        }

        public void liabilityReport(string EntryNo, string com)
        {
            bsObj = new Services.Base();
            DataTable alldata = new DataTable();
            DataTable Param = new DataTable();

            alldata = bsObj.CHNLSVC.CustService.GetCusValuDecar(EntryNo, com);


            _cus_val_declar.Database.Tables["declar_valus"].SetDataSource(alldata);
            _cus_val_declar.Database.Tables["Param"].SetDataSource(Param);
        }
        
        public void CargoImports(string EntryNo, string com)
        {
            DataTable alldata = bsObj.CHNLSVC.CustService.GetCusValuDecar(EntryNo, com);
            DataTable cusdeccommon = bsObj.CHNLSVC.CustService.GetCusdecCommonNew("LK");
            DataTable Param = new DataTable();
            DataRow dr;
            int i = 0;
            Param.Columns.Add("cuh_consi_name", typeof(string));
            Param.Columns.Add("cuh_consi_addr", typeof(string));
            Param.Columns.Add("cuh_decl_name", typeof(string));
            Param.Columns.Add("cuh_decl_addr", typeof(string));
            Param.Columns.Add("cuh_vessel", typeof(string));
            Param.Columns.Add("cuh_voyage_dt", typeof(DateTime));
            Param.Columns.Add("cuh_doc_no", typeof(string));
            Param.Columns.Add("CUH_DT", typeof(DateTime));
            //Param.Columns.Add("WhaerAssName", typeof(string));
            //Param.Columns.Add("ChaNo", typeof(string));
            //Param.Columns.Add("IdintifyNo", typeof(string));
            dr = Param.NewRow();


            List<ImportsBLContainer> blContainers = new List<ImportsBLContainer>();
            List<ImportsBLContainer> blContainers1 = new List<ImportsBLContainer>();
            List<ImportsBLContainer> blContainers2 = new List<ImportsBLContainer>();
            // dr["DocNo"] = docno;
            // Param.Rows.Add(dr);
            if (alldata != null)
            {
                blContainers = bsObj.CHNLSVC.Financial.GetContainers(alldata.Rows[0]["cuh_sun_req_no"].ToString());
                blContainers1 = blContainers.Take(8).ToList();
                blContainers2 = blContainers.Skip(8).Take(8).ToList();
                foreach (DataRow dtRow in alldata.Rows)
                {


                    dr["cuh_consi_name"] = alldata.Rows[i].Field<string>(1);
                    dr["cuh_consi_addr"] = alldata.Rows[i].Field<string>(2);
                    dr["cuh_decl_name"] = alldata.Rows[i].Field<string>(7);
                    dr["cuh_decl_addr"] = alldata.Rows[i].Field<string>(8);
                    dr["cuh_vessel"] = alldata.Rows[i].Field<string>(21);
                    dr["cuh_voyage_dt"] = alldata.Rows[i].Field<DateTime>(22).ToString("dd/MMM/yyyy");
                    dr["cuh_doc_no"] = alldata.Rows[i]["cuh_cusdec_entry_no"].ToString();
                    dr["CUH_DT"] = alldata.Rows[i].Field<DateTime>(24).ToString("dd/MMM/yyyy");


                }
            }

            foreach (object repOp in _cargo_imp.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "containers")
                    {
                        ReportDocument subRepDoc = _cargo_imp.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["Containers"].SetDataSource(blContainers1);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "Blcontainer2")
                    {
                        ReportDocument subRepDoc = _cargo_imp.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["Containers"].SetDataSource(blContainers2);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    _cs.Dispose();
                }
            }

            Param.Rows.Add(dr);
            _cargo_imp.Database.Tables["CusdecCommon"].SetDataSource(cusdeccommon);
            _cargo_imp.Database.Tables["CargoImports"].SetDataSource(Param);
        }

        public void GoodsDecarration(string entryNo, string com)
        {
            DataTable Hdrdata = new DataTable();
            DataTable maindata = new DataTable();
            DataTable Param = new DataTable();


            DataRow dr;
            int i = 0;
            int j = 0;

            int linenumber = 0;
            int nxtlinenumber = 0;
            int maxlinenumber = 0;

            Param.Columns.Add("Company", typeof(string));
            Param.Columns.Add("User", typeof(string));
            Param.Columns.Add("Location", typeof(string));
            Param.Columns.Add("cui_itm_desc", typeof(string));
            Param.Columns.Add("cui_gross_mass", typeof(string));
            Param.Columns.Add("cui_net_mass", typeof(string));
            Param.Columns.Add("cui_bl_no", typeof(string));
            Param.Columns.Add("cui_orgin_cnty", typeof(string));
            Param.Columns.Add("cui_pkgs", typeof(string));
            Param.Columns.Add("Line", typeof(int));
            Param.Columns.Add("MaxLine", typeof(int));
            Param.Columns.Add("cui_hs_cd", typeof(string));
            Param.Columns.Add("cui_itm_price", typeof(decimal));
            Param.Columns.Add("cui_qty", typeof(string));
            Param.Columns.Add("cui_bal_qty1", typeof(string));
            Param.Columns.Add("cui_bal_qty2", typeof(string));
            Param.Columns.Add("cui_bal_qty3", typeof(string));
            Param.Columns.Add("cui_req_qty", typeof(string));


            //tax values
            DataTable taxvalues = new DataTable();
            DataRow trow;

            taxvalues.Columns.Add("Type", typeof(string));
            taxvalues.Columns.Add("Tax", typeof(decimal));
            taxvalues.Columns.Add("Rate", typeof(decimal));
            taxvalues.Columns.Add("Ammount", typeof(decimal));
            taxvalues.Columns.Add("MP", typeof(decimal));
            taxvalues.Columns.Add("Line", typeof(int));
            taxvalues.Columns.Add("Line2", typeof(int));
            taxvalues.Columns.Add("Line3", typeof(int));
            taxvalues.Columns.Add("cui_itm_desc", typeof(string));
            taxvalues.Columns.Add("cui_gross_mass", typeof(string));
            taxvalues.Columns.Add("cui_net_mass", typeof(string));
            taxvalues.Columns.Add("cui_bl_no", typeof(string));
            taxvalues.Columns.Add("cui_orgin_cnty", typeof(string));
            taxvalues.Columns.Add("cui_pkgs", typeof(string));

            DataTable firsgood = new DataTable();
            DataRow firstrow;

            firsgood.Columns.Add("cui_itm_desc", typeof(string));
            firsgood.Columns.Add("cui_gross_mass", typeof(string));
            firsgood.Columns.Add("cui_net_mass", typeof(string));
            firsgood.Columns.Add("cui_bl_no", typeof(string));
            firsgood.Columns.Add("cui_orgin_cnty", typeof(string));
            firsgood.Columns.Add("cui_pkgs", typeof(string));
            firsgood.Columns.Add("cui_hs_cd", typeof(string));
            firsgood.Columns.Add("cui_qty", typeof(string));
            firsgood.Columns.Add("cui_bal_qty2", typeof(string));
            firsgood.Columns.Add("cui_bal_qty3", typeof(string));
            firsgood.Columns.Add("cui_req_qty", typeof(string));
            firsgood.Columns.Add("cui_itm_price", typeof(decimal));

            // all goods data
            maindata = bsObj.CHNLSVC.CustService.GetGoodsDeclarationDetails(entryNo);
            if (maindata != null)
            {
                foreach (DataRow dtRow in maindata.Rows)
                {
                    linenumber = Convert.ToInt32(maindata.Rows[i]["cui_line"]);
                    trow = taxvalues.NewRow();
                    trow["Type"] = maindata.Rows[i]["cuic_ele_cd"].ToString();
                    trow["Tax"] = maindata.Rows[i]["cuic_ele_base"].ToString();
                    trow["Rate"] = maindata.Rows[i]["cuic_ele_rt"].ToString();
                    trow["Ammount"] = maindata.Rows[i]["cuic_ele_amt"].ToString();
                    trow["MP"] = maindata.Rows[i]["cuic_ele_mp"].ToString();
                    trow["Line"] = linenumber;
                    trow["Line2"] = linenumber;
                    trow["Line3"] = linenumber;
                    trow["cui_itm_desc"] = maindata.Rows[i]["cui_itm_desc"].ToString();
                    trow["cui_gross_mass"] = maindata.Rows[i]["cui_gross_mass"].ToString();
                    trow["cui_net_mass"] = maindata.Rows[i]["cui_net_mass"].ToString();
                    trow["cui_bl_no"] = maindata.Rows[i]["cui_bl_no"].ToString();
                    trow["cui_orgin_cnty"] = maindata.Rows[i]["cui_orgin_cnty"].ToString();
                    trow["cui_pkgs"] = maindata.Rows[i]["cui_pkgs"].ToString();

                    taxvalues.Rows.Add(trow);
                    maxlinenumber = linenumber;
                    i++;
                }
            }

            if (maindata != null)
            {
                foreach (DataRow dtRow in maindata.Rows)
                {
                    if (j < maindata.Rows.Count - 1)
                    {
                        linenumber = Convert.ToInt32(maindata.Rows[j]["cui_line"]);
                        nxtlinenumber = Convert.ToInt32(maindata.Rows[j + 1]["cui_line"]);

                        if (linenumber != nxtlinenumber)
                        {
                            dr = Param.NewRow();
                            dr["Company"] = "ABL";
                            dr["User"] = "ADMIN";
                            dr["Location"] = "DMP";
                            dr["cui_itm_desc"] = maindata.Rows[j]["cui_itm_desc"].ToString();
                            dr["cui_gross_mass"] = maindata.Rows[j]["cui_gross_mass"].ToString();
                            dr["cui_net_mass"] = maindata.Rows[j]["cui_net_mass"].ToString();
                            dr["cui_bl_no"] = maindata.Rows[j]["cui_bl_no"].ToString();
                            dr["cui_orgin_cnty"] = maindata.Rows[j]["cui_orgin_cnty"].ToString();
                            dr["cui_pkgs"] = maindata.Rows[j]["cui_pkgs"].ToString();
                            dr["Line"] = linenumber;
                            dr["MaxLine"] = maxlinenumber;
                            dr["cui_hs_cd"] = maindata.Rows[j]["cui_hs_cd"].ToString();
                            dr["cui_qty"] = maindata.Rows[j]["cui_qty"].ToString();
                            dr["cui_bal_qty1"] = maindata.Rows[j]["cui_bal_qty1"].ToString();
                            dr["cui_bal_qty2"] = maindata.Rows[j]["cui_bal_qty2"].ToString();
                            dr["cui_bal_qty3"] = maindata.Rows[j]["cui_bal_qty3"].ToString();
                            dr["cui_req_qty"] = maindata.Rows[j]["cui_req_qty"].ToString();
                            dr["cui_itm_price"] = Convert.ToDecimal(maindata.Rows[j]["cui_itm_price"].ToString());

                            Param.Rows.Add(dr);
                        }
                        if (j == maindata.Rows.Count - 2)
                        {
                            dr = Param.NewRow();
                            dr["Company"] = "ABL";
                            dr["User"] = "ADMIN";
                            dr["Location"] = "DMP";
                            dr["cui_itm_desc"] = maindata.Rows[j]["cui_itm_desc"].ToString();
                            dr["cui_gross_mass"] = maindata.Rows[j]["cui_gross_mass"].ToString();
                            dr["cui_net_mass"] = maindata.Rows[j]["cui_net_mass"].ToString();
                            dr["cui_bl_no"] = maindata.Rows[j]["cui_bl_no"].ToString();
                            dr["cui_orgin_cnty"] = maindata.Rows[j]["cui_orgin_cnty"].ToString();
                            dr["cui_pkgs"] = maindata.Rows[j]["cui_pkgs"].ToString();
                            dr["Line"] = linenumber;
                            dr["MaxLine"] = maxlinenumber;
                            dr["cui_hs_cd"] = maindata.Rows[j]["cui_hs_cd"].ToString();
                            dr["cui_qty"] = maindata.Rows[j]["cui_qty"].ToString();
                            dr["cui_bal_qty1"] = maindata.Rows[j]["cui_bal_qty1"].ToString();
                            dr["cui_bal_qty2"] = maindata.Rows[j]["cui_bal_qty2"].ToString();
                            dr["cui_bal_qty3"] = maindata.Rows[j]["cui_bal_qty3"].ToString();
                            dr["cui_req_qty"] = maindata.Rows[j]["cui_req_qty"].ToString();
                            dr["cui_itm_price"] = Convert.ToDecimal(maindata.Rows[j]["cui_itm_price"].ToString());
                            Param.Rows.Add(dr);
                        }
                    }
                    j++;
                }
            }
            if (maindata != null)
            {

                firstrow = firsgood.NewRow();

                firstrow["cui_itm_desc"] = maindata.Rows[0]["cui_itm_desc"].ToString();
                firstrow["cui_gross_mass"] = maindata.Rows[0]["cui_gross_mass"].ToString();
                firstrow["cui_net_mass"] = maindata.Rows[0]["cui_net_mass"].ToString();
                firstrow["cui_bl_no"] = maindata.Rows[0]["cui_bl_no"].ToString();
                firstrow["cui_orgin_cnty"] = maindata.Rows[0]["cui_orgin_cnty"].ToString();
                firstrow["cui_pkgs"] = maindata.Rows[0]["cui_pkgs"].ToString();
                firstrow["cui_hs_cd"] = maindata.Rows[0]["cui_hs_cd"].ToString();
                firstrow["cui_qty"] = maindata.Rows[0]["cui_qty"].ToString();
                firstrow["cui_bal_qty2"] = maindata.Rows[0]["cui_bal_qty2"].ToString();
                firstrow["cui_bal_qty3"] = maindata.Rows[0]["cui_bal_qty3"].ToString();
                firstrow["cui_req_qty"] = maindata.Rows[0]["cui_req_qty"].ToString();
                firstrow["cui_itm_price"] = Convert.ToDecimal(maindata.Rows[0]["cui_itm_price"].ToString());

                firsgood.Rows.Add(firstrow);
            }
            // create second page table
            DataTable secondpagedata = new DataTable();
            DataRow srow;

            secondpagedata.Columns.Add("cui_itm_desc1", typeof(string));
            secondpagedata.Columns.Add("cui_gross_mass1", typeof(string));
            secondpagedata.Columns.Add("cui_net_mass1", typeof(string));
            secondpagedata.Columns.Add("cui_bl_no1", typeof(string));
            secondpagedata.Columns.Add("cui_orgin_cnty1", typeof(string));
            secondpagedata.Columns.Add("cui_itm_desc2", typeof(string));
            secondpagedata.Columns.Add("cui_gross_mass2", typeof(string));
            secondpagedata.Columns.Add("cui_net_mass2", typeof(string));
            secondpagedata.Columns.Add("cui_bl_no2", typeof(string));
            secondpagedata.Columns.Add("cui_orgin_cnty2", typeof(string));
            secondpagedata.Columns.Add("cui_itm_desc3", typeof(string));
            secondpagedata.Columns.Add("cui_gross_mass3", typeof(string));
            secondpagedata.Columns.Add("cui_net_mass3", typeof(string));
            secondpagedata.Columns.Add("cui_bl_no3", typeof(string));
            secondpagedata.Columns.Add("cui_orgin_cnty3", typeof(string));
            secondpagedata.Columns.Add("Line1", typeof(Int16));
            secondpagedata.Columns.Add("Line2", typeof(Int16));
            secondpagedata.Columns.Add("Line3", typeof(Int16));
            secondpagedata.Columns.Add("cui_hs_cd1", typeof(string));
            secondpagedata.Columns.Add("cui_hs_cd2", typeof(string));
            secondpagedata.Columns.Add("cui_hs_cd3", typeof(string));
            secondpagedata.Columns.Add("cui_itm_price1", typeof(decimal));
            secondpagedata.Columns.Add("cui_itm_price2", typeof(decimal));
            secondpagedata.Columns.Add("cui_itm_price3", typeof(decimal));
            secondpagedata.Columns.Add("cui_bal_qty11", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty12", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty13", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty21", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty22", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty23", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty31", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty32", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty33", typeof(string));

            i = 1;
            if (Param != null)
            {
                for (int k = 0; k <= Param.Rows.Count; k = k + 3)
                {
                    srow = secondpagedata.NewRow();
                    if (i < Param.Rows.Count)
                    {

                        srow["cui_itm_desc1"] = Param.Rows[i]["cui_itm_desc"].ToString();
                        srow["cui_gross_mass1"] = Param.Rows[i]["cui_gross_mass"].ToString();
                        srow["cui_net_mass1"] = Param.Rows[i]["cui_net_mass"].ToString();
                        srow["cui_bl_no1"] = Param.Rows[i]["cui_bl_no"].ToString();
                        srow["cui_orgin_cnty1"] = Param.Rows[i]["cui_orgin_cnty"].ToString();
                        srow["Line1"] = Convert.ToInt16(Param.Rows[i]["Line"]);
                        srow["cui_hs_cd1"] = Param.Rows[i]["cui_hs_cd"].ToString();
                        srow["cui_itm_price1"] = Convert.ToDecimal(Param.Rows[i]["cui_itm_price"].ToString());
                        srow["cui_bal_qty11"] = Param.Rows[i]["cui_bal_qty1"].ToString();
                        srow["cui_bal_qty21"] = Param.Rows[i]["cui_bal_qty2"].ToString();
                        srow["cui_bal_qty31"] = Param.Rows[i]["cui_bal_qty3"].ToString();
                    }

                    if (i + 1 < Param.Rows.Count)
                    {
                        srow["cui_itm_desc2"] = Param.Rows[i + 1]["cui_itm_desc"].ToString();
                        srow["cui_gross_mass2"] = Param.Rows[i + 1]["cui_gross_mass"].ToString();
                        srow["cui_net_mass2"] = Param.Rows[i + 1]["cui_net_mass"].ToString();
                        srow["cui_bl_no2"] = Param.Rows[i + 1]["cui_bl_no"].ToString();
                        srow["cui_orgin_cnty2"] = Param.Rows[i + 1]["cui_orgin_cnty"].ToString();
                        srow["Line2"] = Convert.ToInt16(Param.Rows[i + 1]["Line"]);
                        srow["cui_hs_cd2"] = Param.Rows[i + 1]["cui_hs_cd"].ToString();
                        srow["cui_itm_price2"] = Convert.ToDecimal(Param.Rows[i + 1]["cui_itm_price"].ToString());
                        srow["cui_bal_qty12"] = Param.Rows[i + 1]["cui_bal_qty1"].ToString();
                        srow["cui_bal_qty22"] = Param.Rows[i + 1]["cui_bal_qty2"].ToString();
                        srow["cui_bal_qty32"] = Param.Rows[i + 1]["cui_bal_qty3"].ToString();
                    }

                    if (i + 2 < Param.Rows.Count)
                    {
                        srow["cui_itm_desc3"] = Param.Rows[i + 2]["cui_itm_desc"].ToString();
                        srow["cui_gross_mass3"] = Param.Rows[i + 2]["cui_gross_mass"].ToString();
                        srow["cui_net_mass3"] = Param.Rows[i + 2]["cui_net_mass"].ToString();
                        srow["cui_bl_no3"] = Param.Rows[i + 2]["cui_bl_no"].ToString();
                        srow["cui_orgin_cnty3"] = Param.Rows[i + 2]["cui_orgin_cnty"].ToString();
                        srow["Line3"] = Convert.ToInt16(Param.Rows[i + 2]["Line"]);
                        srow["cui_hs_cd3"] = Param.Rows[i + 2]["cui_hs_cd"].ToString();
                        srow["cui_itm_price3"] = Convert.ToDecimal(Param.Rows[i + 2]["cui_itm_price"].ToString());
                        srow["cui_bal_qty13"] = Param.Rows[i + 2]["cui_bal_qty1"].ToString();
                        srow["cui_bal_qty23"] = Param.Rows[i + 2]["cui_bal_qty2"].ToString();
                        srow["cui_bal_qty33"] = Param.Rows[i + 2]["cui_bal_qty3"].ToString();
                    }

                    secondpagedata.Rows.Add(srow);
                    i = i + 3;
                }
            }

            DataTable eledata = new DataTable();
            i = 0;
            string cat = "";
            decimal fob = 0;
            decimal foblkr = 0;
            decimal faight = 0;
            decimal faightlkr = 0;
            decimal insu = 0;
            decimal insulkr = 0;
            decimal other = 0;
            decimal otherlkr = 0;
            decimal total = 0;
            decimal totallkr = 0;

            eledata = bsObj.CHNLSVC.CustService.GetCusdecElementnew(entryNo);
            foreach (DataRow ele in eledata.Rows)
            {
                cat = eledata.Rows[i]["cus_ele_cd"].ToString();

                if (cat == "OTH")
                {
                    other = Convert.ToDecimal(eledata.Rows[i]["cus_amt"]);
                    otherlkr = Convert.ToDecimal(eledata.Rows[i]["cus_amt_com"]);
                }
                if (cat == "INSU")
                {
                    insu = Convert.ToDecimal(eledata.Rows[i]["cus_amt"]);
                    insulkr = Convert.ToDecimal(eledata.Rows[i]["cus_amt_com"]);
                }
                if (cat == "FRGT")
                {
                    faight = Convert.ToDecimal(eledata.Rows[i]["cus_amt"]);
                    faightlkr = Convert.ToDecimal(eledata.Rows[i]["cus_amt_com"]);

                }
                if (cat == "COST")
                {
                    fob = Convert.ToDecimal(eledata.Rows[i]["cus_amt"]);
                    foblkr = Convert.ToDecimal(eledata.Rows[i]["cus_amt_com"]);
                }

            }
            total = other + insu + faight + fob;
            totallkr = otherlkr + insulkr + faightlkr + foblkr;

            DataTable finalele = new DataTable();
            DataRow elerow;
            finalele.Columns.Add("FOB", typeof(decimal));
            finalele.Columns.Add("FREIGHT", typeof(decimal));
            finalele.Columns.Add("INSU", typeof(decimal));
            finalele.Columns.Add("OTHER", typeof(decimal));
            finalele.Columns.Add("TOTAL", typeof(decimal));
            finalele.Columns.Add("TOTALLKR", typeof(decimal));

            elerow = finalele.NewRow();
            elerow["FOB"] = fob;
            elerow["FREIGHT"] = faight;
            elerow["INSU"] = insu;
            elerow["OTHER"] = other;
            elerow["TOTAL"] = total;
            elerow["TOTALLKR"] = totallkr;

            finalele.Rows.Add(elerow);

            DataTable cusdeccommon = bsObj.CHNLSVC.CustService.GetCusdecCommonNew("LK");


            //header data
            Hdrdata = bsObj.CHNLSVC.CustService.GetCusdecHDRData(entryNo, com);

            _goods_declaration.Database.Tables["firstGooddata"].SetDataSource(firsgood);
            //  _goods_declaration.Database.Tables["TaxValues"].SetDataSource(taxvalues);
            _goods_declaration.Database.Tables["HdrData"].SetDataSource(Hdrdata);
            _goods_declaration.Database.Tables["SecondPage"].SetDataSource(secondpagedata);
            _goods_declaration.Database.Tables["EleData"].SetDataSource(finalele);
            //  _goods_declaration.Database.Tables["Param"].SetDataSource(Param);
            _goods_declaration.Database.Tables["CusdecCommon"].SetDataSource(cusdeccommon);

            foreach (object repOp in _goods_declaration.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "firstpagetax")
                    {
                        ReportDocument subRepDoc = _goods_declaration.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["TaxValues"].SetDataSource(taxvalues);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "finaltaxvalues")
                    {
                        ReportDocument subRepDoc = _goods_declaration.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["TaxValues"].SetDataSource(taxvalues);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "finalrighttax")
                    {
                        ReportDocument subRepDoc = _goods_declaration.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["TaxValues"].SetDataSource(taxvalues);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "finalbottomtax")
                    {
                        ReportDocument subRepDoc = _goods_declaration.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["TaxValues"].SetDataSource(taxvalues);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "TotalTaxsummery")
                    {
                        ReportDocument subRepDoc = _goods_declaration.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["TaxValues"].SetDataSource(taxvalues);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "secondpageitemdetails")
                    {
                        ReportDocument subRepDoc = _goods_declaration.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["Param"].SetDataSource(Param);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    _cs.Dispose();
                }
            }
        }


        public void GoodsDecarrationSheet(string entryNo, string com)
        {
            DataTable Hdrdata = new DataTable();
            DataTable maindata = new DataTable();
            DataTable Param = new DataTable();


            DataRow dr;
            int i = 0;
            int j = 0;

            int linenumber = 0;
            int nxtlinenumber = 0;
            int maxlinenumber = 0;

            Param.Columns.Add("Company", typeof(string));
            Param.Columns.Add("User", typeof(string));
            Param.Columns.Add("Location", typeof(string));
            Param.Columns.Add("cui_itm_desc", typeof(string));
            Param.Columns.Add("cui_gross_mass", typeof(string));
            Param.Columns.Add("cui_net_mass", typeof(string));
            Param.Columns.Add("cui_bl_no", typeof(string));
            Param.Columns.Add("cui_orgin_cnty", typeof(string));
            Param.Columns.Add("cui_pkgs", typeof(string));
            Param.Columns.Add("Line", typeof(int));
            Param.Columns.Add("MaxLine", typeof(int));
            Param.Columns.Add("cui_hs_cd", typeof(string));
            Param.Columns.Add("cui_itm_price", typeof(decimal));
            Param.Columns.Add("cui_qty", typeof(string));
            Param.Columns.Add("cui_bal_qty1", typeof(string));
            Param.Columns.Add("cui_bal_qty2", typeof(string));
            Param.Columns.Add("cui_bal_qty3", typeof(string));
            Param.Columns.Add("cui_req_qty", typeof(string));



            //tax values
            DataTable taxvalues = new DataTable();
            DataRow trow;

            taxvalues.Columns.Add("Type", typeof(string));
            taxvalues.Columns.Add("Tax", typeof(decimal));
            taxvalues.Columns.Add("Rate", typeof(decimal));
            taxvalues.Columns.Add("Ammount", typeof(decimal));
            taxvalues.Columns.Add("MP", typeof(decimal));
            taxvalues.Columns.Add("Line", typeof(int));
            taxvalues.Columns.Add("Line2", typeof(int));
            taxvalues.Columns.Add("Line3", typeof(int));
            taxvalues.Columns.Add("cui_itm_desc", typeof(string));
            taxvalues.Columns.Add("cui_gross_mass", typeof(string));
            taxvalues.Columns.Add("cui_net_mass", typeof(string));
            taxvalues.Columns.Add("cui_bl_no", typeof(string));
            taxvalues.Columns.Add("cui_orgin_cnty", typeof(string));
            taxvalues.Columns.Add("cui_pkgs", typeof(string));

            DataTable firsgood = new DataTable();
            DataRow firstrow;

            firsgood.Columns.Add("cui_itm_desc", typeof(string));
            firsgood.Columns.Add("cui_gross_mass", typeof(string));
            firsgood.Columns.Add("cui_net_mass", typeof(string));
            firsgood.Columns.Add("cui_bl_no", typeof(string));
            firsgood.Columns.Add("cui_orgin_cnty", typeof(string));
            firsgood.Columns.Add("cui_pkgs", typeof(string));
            firsgood.Columns.Add("cui_hs_cd", typeof(string));
            firsgood.Columns.Add("cui_qty", typeof(string));
            firsgood.Columns.Add("cui_bal_qty2", typeof(string));
            firsgood.Columns.Add("cui_bal_qty3", typeof(string));
            firsgood.Columns.Add("cui_req_qty", typeof(string));
            firsgood.Columns.Add("cui_itm_price", typeof(decimal));
            firsgood.Columns.Add("cui_line", typeof(Int32));

            //header data
            Hdrdata = bsObj.CHNLSVC.CustService.GetCusdecHDRData(entryNo, com);
            // all goods data
            maindata = bsObj.CHNLSVC.CustService.GetGoodsDeclarationDetails(entryNo);
            //DataView dv = maindata.DefaultView;
            //dv.Sort = "cui_hs_cd ,cui_model, cuic_itm_cd,cui_line";
            //maindata = dv.ToTable();

            int licount = 1;


            if (maindata.Rows.Count > 0)
            {
                foreach (DataRow dtRow in maindata.Rows)
                {
                    int nxtline;
                    linenumber = Convert.ToInt32(maindata.Rows[i]["cui_line"]);
                    if (i + 1 < maindata.Rows.Count)
                    {
                        nxtline = Convert.ToInt32(maindata.Rows[i + 1]["cui_line"]);
                    }
                    else
                    {
                        nxtline = 99999;
                    }



                    if (linenumber != nxtline)
                    {
                        trow = taxvalues.NewRow();
                        trow["Type"] = maindata.Rows[i]["cuic_ele_cd"].ToString();
                        trow["Tax"] = maindata.Rows[i]["cuic_ele_base"].ToString();
                        trow["Rate"] = maindata.Rows[i]["cuic_ele_rt"].ToString();
                        trow["Ammount"] = maindata.Rows[i]["cuic_ele_amt"].ToString();
                        trow["MP"] = maindata.Rows[i]["cuic_ele_mp"].ToString();
                        trow["Line"] = licount;
                        trow["Line2"] = licount;
                        trow["Line3"] = licount;
                        trow["cui_itm_desc"] = maindata.Rows[i]["cui_itm_desc"].ToString() + " " + maindata.Rows[i]["cui_model"].ToString();
                        trow["cui_gross_mass"] = maindata.Rows[i]["cui_gross_mass"].ToString();
                        trow["cui_net_mass"] = maindata.Rows[i]["cui_net_mass"].ToString();
                        trow["cui_bl_no"] = maindata.Rows[i]["cui_bl_no"].ToString();
                        trow["cui_orgin_cnty"] = maindata.Rows[i]["cui_orgin_cnty"].ToString();
                        trow["cui_pkgs"] = maindata.Rows[i]["cui_pkgs"].ToString();

                        taxvalues.Rows.Add(trow);
                        maxlinenumber = linenumber;
                        i++;
                        licount++;
                    }
                    else
                    {
                        trow = taxvalues.NewRow();
                        trow["Type"] = maindata.Rows[i]["cuic_ele_cd"].ToString();
                        trow["Tax"] = maindata.Rows[i]["cuic_ele_base"].ToString();
                        trow["Rate"] = maindata.Rows[i]["cuic_ele_rt"].ToString();
                        trow["Ammount"] = maindata.Rows[i]["cuic_ele_amt"].ToString();
                        trow["MP"] = maindata.Rows[i]["cuic_ele_mp"].ToString();
                        trow["Line"] = licount;
                        trow["Line2"] = licount;
                        trow["Line3"] = licount;
                        trow["cui_itm_desc"] = maindata.Rows[i]["cui_itm_desc"].ToString() + " " + maindata.Rows[i]["cui_model"].ToString();
                        trow["cui_gross_mass"] = maindata.Rows[i]["cui_gross_mass"].ToString();
                        trow["cui_net_mass"] = maindata.Rows[i]["cui_net_mass"].ToString();
                        trow["cui_bl_no"] = maindata.Rows[i]["cui_bl_no"].ToString();
                        trow["cui_orgin_cnty"] = maindata.Rows[i]["cui_orgin_cnty"].ToString();
                        trow["cui_pkgs"] = maindata.Rows[i]["cui_pkgs"].ToString();

                        taxvalues.Rows.Add(trow);
                        maxlinenumber = linenumber;
                        i++;
                    }


                }
            }


            if (Hdrdata.Rows.Count > 0)
            {
                trow = taxvalues.NewRow();
                trow["Type"] = "CE&S";
                trow["Tax"] = "1";
                trow["Rate"] = "1";
                trow["Ammount"] = Hdrdata.Rows[0]["CUH_COM_CHG"].ToString();
                trow["MP"] = "1";
                trow["Line"] = 1;
                trow["Line2"] = 1;
                trow["Line3"] = 1;
                taxvalues.Rows.Add(trow);

            }



            if (maindata != null)
            {
                foreach (DataRow dtRow in maindata.Rows)
                {
                    if (j < maindata.Rows.Count - 1)
                    {
                        linenumber = Convert.ToInt32(maindata.Rows[j]["cui_line"]);
                        nxtlinenumber = Convert.ToInt32(maindata.Rows[j + 1]["cui_line"]);

                        if (linenumber != nxtlinenumber)
                        {
                            dr = Param.NewRow();
                            dr["Company"] = "ABL";
                            dr["User"] = "ADMIN";
                            dr["Location"] = "DMP";
                            dr["cui_itm_desc"] = maindata.Rows[j]["cui_itm_desc"].ToString() + " " + maindata.Rows[j]["cui_model"].ToString();
                            dr["cui_gross_mass"] = maindata.Rows[j]["cui_gross_mass"].ToString();
                            dr["cui_net_mass"] = maindata.Rows[j]["cui_net_mass"].ToString();
                            dr["cui_bl_no"] = maindata.Rows[j]["cui_bl_no"].ToString();
                            dr["cui_orgin_cnty"] = maindata.Rows[j]["cui_orgin_cnty"].ToString();
                            dr["cui_pkgs"] = maindata.Rows[j]["cui_pkgs"].ToString();
                            dr["Line"] = linenumber;
                            dr["MaxLine"] = maxlinenumber;
                            dr["cui_hs_cd"] = maindata.Rows[j]["cui_hs_cd"].ToString();
                            dr["cui_qty"] = maindata.Rows[j]["cui_qty"].ToString();
                            dr["cui_bal_qty1"] = maindata.Rows[j]["cui_bal_qty1"].ToString();
                            dr["cui_bal_qty2"] = maindata.Rows[j]["cui_bal_qty2"].ToString();
                            dr["cui_bal_qty3"] = maindata.Rows[j]["cui_preferance"].ToString();
                            dr["cui_req_qty"] = maindata.Rows[j]["cui_req_qty"].ToString();
                            dr["cui_itm_price"] = Convert.ToDecimal(maindata.Rows[j]["cui_itm_price"].ToString());

                            Param.Rows.Add(dr);
                        }
                        if (j == maindata.Rows.Count - 2)
                        {
                            dr = Param.NewRow();
                            dr["Company"] = "ABL";
                            dr["User"] = "ADMIN";
                            dr["Location"] = "DMP";
                            dr["cui_itm_desc"] = maindata.Rows[j]["cui_itm_desc"].ToString() + " " + maindata.Rows[j]["cui_model"].ToString();
                            dr["cui_gross_mass"] = maindata.Rows[j]["cui_gross_mass"].ToString();
                            dr["cui_net_mass"] = maindata.Rows[j]["cui_net_mass"].ToString();
                            dr["cui_bl_no"] = maindata.Rows[j]["cui_bl_no"].ToString();
                            dr["cui_orgin_cnty"] = maindata.Rows[j]["cui_orgin_cnty"].ToString();
                            dr["cui_pkgs"] = maindata.Rows[j]["cui_pkgs"].ToString();
                            dr["Line"] = linenumber;
                            dr["MaxLine"] = maxlinenumber;
                            dr["cui_hs_cd"] = maindata.Rows[j]["cui_hs_cd"].ToString();
                            dr["cui_qty"] = maindata.Rows[j]["cui_qty"].ToString();
                            dr["cui_bal_qty1"] = maindata.Rows[j]["cui_bal_qty1"].ToString();
                            dr["cui_bal_qty2"] = maindata.Rows[j]["cui_bal_qty2"].ToString();
                            dr["cui_bal_qty3"] = maindata.Rows[j]["cui_preferance"].ToString();
                            dr["cui_req_qty"] = maindata.Rows[j]["cui_req_qty"].ToString();
                            dr["cui_itm_price"] = Convert.ToDecimal(maindata.Rows[j]["cui_itm_price"].ToString());
                            Param.Rows.Add(dr);
                        }
                    }
                    j++;
                }
            }
            //int p = 0;
            //int q = 1;
            //foreach (DataRow dbrow in Param.Rows)
            //{
            //    Int32 line =Convert.ToInt32( Param.Rows[p]["Line"].ToString());
            //    string itemcode = Param.Rows[p]["Line"].ToString();
            //    if()

            //    p++;
            //}


            if (maindata.Rows.Count > 0)
            {

                firstrow = firsgood.NewRow();

                firstrow["cui_itm_desc"] = maindata.Rows[0]["cui_itm_desc"].ToString() + " " + maindata.Rows[0]["cui_model"].ToString();
                firstrow["cui_gross_mass"] = maindata.Rows[0]["cui_gross_mass"].ToString();
                firstrow["cui_net_mass"] = maindata.Rows[0]["cui_net_mass"].ToString();
                firstrow["cui_bl_no"] = maindata.Rows[0]["cui_bl_no"].ToString();
                firstrow["cui_orgin_cnty"] = maindata.Rows[0]["cui_orgin_cnty"].ToString();
                firstrow["cui_pkgs"] = maindata.Rows[0]["cui_pkgs"].ToString();
                firstrow["cui_hs_cd"] = maindata.Rows[0]["cui_hs_cd"].ToString();
                firstrow["cui_qty"] = maindata.Rows[0]["cui_qty"].ToString();
                firstrow["cui_bal_qty2"] = maindata.Rows[0]["cui_bal_qty2"].ToString();
                firstrow["cui_bal_qty3"] = maindata.Rows[0]["cui_preferance"].ToString();
                firstrow["cui_req_qty"] = maindata.Rows[0]["cui_req_qty"].ToString();
                firstrow["cui_itm_price"] = Convert.ToDecimal(maindata.Rows[0]["cui_itm_price"].ToString());
                firstrow["cui_line"] = Convert.ToInt32(maindata.Rows[0]["cui_line"].ToString());
                firsgood.Rows.Add(firstrow);

                int effect = bsObj.CHNLSVC.Financial.UpdateOtherdocline(entryNo, Convert.ToInt32(maindata.Rows[0]["cui_line"].ToString()), "", 1);
            }
            // create second page table
            DataTable secondpagedata = new DataTable();
            DataRow srow;

            secondpagedata.Columns.Add("cui_itm_desc1", typeof(string));
            secondpagedata.Columns.Add("cui_gross_mass1", typeof(string));
            secondpagedata.Columns.Add("cui_net_mass1", typeof(string));
            secondpagedata.Columns.Add("cui_bl_no1", typeof(string));
            secondpagedata.Columns.Add("cui_orgin_cnty1", typeof(string));
            secondpagedata.Columns.Add("cui_itm_desc2", typeof(string));
            secondpagedata.Columns.Add("cui_gross_mass2", typeof(string));
            secondpagedata.Columns.Add("cui_net_mass2", typeof(string));
            secondpagedata.Columns.Add("cui_bl_no2", typeof(string));
            secondpagedata.Columns.Add("cui_orgin_cnty2", typeof(string));
            secondpagedata.Columns.Add("cui_itm_desc3", typeof(string));
            secondpagedata.Columns.Add("cui_gross_mass3", typeof(string));
            secondpagedata.Columns.Add("cui_net_mass3", typeof(string));
            secondpagedata.Columns.Add("cui_bl_no3", typeof(string));
            secondpagedata.Columns.Add("cui_orgin_cnty3", typeof(string));
            secondpagedata.Columns.Add("Line1", typeof(Int16));
            secondpagedata.Columns.Add("Line2", typeof(Int16));
            secondpagedata.Columns.Add("Line3", typeof(Int16));
            secondpagedata.Columns.Add("LineCount1", typeof(Int16));
            secondpagedata.Columns.Add("LineCount2", typeof(Int16));
            secondpagedata.Columns.Add("LineCount3", typeof(Int16));
            secondpagedata.Columns.Add("cui_hs_cd1", typeof(string));
            secondpagedata.Columns.Add("cui_hs_cd2", typeof(string));
            secondpagedata.Columns.Add("cui_hs_cd3", typeof(string));
            secondpagedata.Columns.Add("cui_itm_price1", typeof(decimal));
            secondpagedata.Columns.Add("cui_itm_price2", typeof(decimal));
            secondpagedata.Columns.Add("cui_itm_price3", typeof(decimal));
            secondpagedata.Columns.Add("cui_bal_qty11", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty12", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty13", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty21", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty22", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty23", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty31", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty32", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty33", typeof(string));
            int itm = 0;
            i = 1;
            int q = 2;
            if (Param.Rows.Count > 0)
            {
                for (int k = 0; k <= Param.Rows.Count; k = k + 3)
                {
                    srow = secondpagedata.NewRow();
                    if (i < Param.Rows.Count)
                    {
                        itm += 1;
                        srow["cui_itm_desc1"] = Param.Rows[i]["cui_itm_desc"].ToString();
                        srow["cui_gross_mass1"] = Param.Rows[i]["cui_gross_mass"].ToString();
                        srow["cui_net_mass1"] = Param.Rows[i]["cui_net_mass"].ToString();
                        srow["cui_bl_no1"] = Param.Rows[i]["cui_bl_no"].ToString();
                        srow["cui_orgin_cnty1"] = Param.Rows[i]["cui_orgin_cnty"].ToString();
                        srow["Line1"] = Convert.ToInt16(i + 1);
                        srow["LineCount1"] = Convert.ToInt16(i + 1);
                        srow["cui_hs_cd1"] = Param.Rows[i]["cui_hs_cd"].ToString();
                        srow["cui_itm_price1"] = Convert.ToDecimal(Param.Rows[i]["cui_itm_price"].ToString());
                        srow["cui_bal_qty11"] = Param.Rows[i]["cui_qty"].ToString();
                        srow["cui_bal_qty21"] = Param.Rows[i]["cui_bal_qty2"].ToString();
                        srow["cui_bal_qty31"] = Param.Rows[i]["cui_bal_qty3"].ToString();

                        int effect = bsObj.CHNLSVC.Financial.UpdateOtherdocline(entryNo, Convert.ToInt32(Param.Rows[i]["Line"].ToString()), "", q);
                        q++;
                    }

                    if (i + 1 < Param.Rows.Count)
                    {
                        itm += 1;
                        srow["cui_itm_desc2"] = Param.Rows[i + 1]["cui_itm_desc"].ToString();
                        srow["cui_gross_mass2"] = Param.Rows[i + 1]["cui_gross_mass"].ToString();
                        srow["cui_net_mass2"] = Param.Rows[i + 1]["cui_net_mass"].ToString();
                        srow["cui_bl_no2"] = Param.Rows[i + 1]["cui_bl_no"].ToString();
                        srow["cui_orgin_cnty2"] = Param.Rows[i + 1]["cui_orgin_cnty"].ToString();
                        srow["Line2"] = Convert.ToInt16(i + 2);
                        srow["LineCount2"] = Convert.ToInt16(i + 2);
                        srow["cui_hs_cd2"] = Param.Rows[i + 1]["cui_hs_cd"].ToString();
                        srow["cui_itm_price2"] = Convert.ToDecimal(Param.Rows[i + 1]["cui_itm_price"].ToString());
                        srow["cui_bal_qty12"] = Param.Rows[i + 1]["cui_qty"].ToString();
                        srow["cui_bal_qty22"] = Param.Rows[i + 1]["cui_bal_qty2"].ToString();
                        srow["cui_bal_qty32"] = Param.Rows[i + 1]["cui_bal_qty3"].ToString();

                        int effect = bsObj.CHNLSVC.Financial.UpdateOtherdocline(entryNo, Convert.ToInt32(Param.Rows[i + 1]["Line"].ToString()), "", q);
                        q++;
                    }

                    if (i + 2 < Param.Rows.Count)
                    {
                        itm += 1;
                        srow["cui_itm_desc3"] = Param.Rows[i + 2]["cui_itm_desc"].ToString();
                        srow["cui_gross_mass3"] = Param.Rows[i + 2]["cui_gross_mass"].ToString();
                        srow["cui_net_mass3"] = Param.Rows[i + 2]["cui_net_mass"].ToString();
                        srow["cui_bl_no3"] = Param.Rows[i + 2]["cui_bl_no"].ToString();
                        srow["cui_orgin_cnty3"] = Param.Rows[i + 2]["cui_orgin_cnty"].ToString();
                        srow["Line3"] = Convert.ToInt16(i + 3);
                        srow["LineCount3"] = Convert.ToInt16(i + 3);
                        srow["cui_hs_cd3"] = Param.Rows[i + 2]["cui_hs_cd"].ToString();
                        srow["cui_itm_price3"] = Convert.ToDecimal(Param.Rows[i + 2]["cui_itm_price"].ToString());
                        srow["cui_bal_qty13"] = Param.Rows[i + 2]["cui_qty"].ToString();
                        srow["cui_bal_qty23"] = Param.Rows[i + 2]["cui_bal_qty2"].ToString();
                        srow["cui_bal_qty33"] = Param.Rows[i + 2]["cui_bal_qty3"].ToString();

                        int effect = bsObj.CHNLSVC.Financial.UpdateOtherdocline(entryNo, Convert.ToInt32(Param.Rows[i + 2]["Line"].ToString()), "", q);
                        q++;
                    }

                    secondpagedata.Rows.Add(srow);
                    i = i + 3;
                }
            }



            DataTable eledata = new DataTable();
            i = 0;
            string cat = "";
            decimal fob = 0;
            decimal foblkr = 0;
            decimal faight = 0;
            decimal faightlkr = 0;
            decimal insu = 0;
            decimal insulkr = 0;
            decimal other = 0;
            decimal otherlkr = 0;
            decimal total = 0;
            decimal totallkr = 0;

            eledata = bsObj.CHNLSVC.CustService.GetCusdecElementnew(entryNo);
            foreach (DataRow ele in eledata.Rows)
            {
                cat = eledata.Rows[i]["cus_ele_cd"].ToString();

                if (cat == "OTH")
                {
                    other = Convert.ToDecimal(eledata.Rows[i]["cus_amt"]);
                    otherlkr = Convert.ToDecimal(eledata.Rows[i]["cus_amt_com"]);
                }
                if (cat == "INSU")
                {
                    insu = Convert.ToDecimal(eledata.Rows[i]["cus_amt"]);
                    insulkr = Convert.ToDecimal(eledata.Rows[i]["cus_amt_com"]);
                }
                if (cat == "FRGT")
                {
                    faight = Convert.ToDecimal(eledata.Rows[i]["cus_amt"]);
                    faightlkr = Convert.ToDecimal(eledata.Rows[i]["cus_amt_com"]);

                }
                if (cat == "COST")
                {
                    fob = Convert.ToDecimal(eledata.Rows[i]["cus_amt"]);
                    foblkr = Convert.ToDecimal(eledata.Rows[i]["cus_amt_com"]);
                }
                i++;

            }
            total = other + insu + faight + fob;
            totallkr = otherlkr + insulkr + faightlkr + foblkr;

            DataTable finalele = new DataTable();
            DataRow elerow;
            finalele.Columns.Add("FOB", typeof(decimal));
            finalele.Columns.Add("FREIGHT", typeof(decimal));
            finalele.Columns.Add("INSU", typeof(decimal));
            finalele.Columns.Add("OTHER", typeof(decimal));
            finalele.Columns.Add("TOTAL", typeof(decimal));
            finalele.Columns.Add("TOTALLKR", typeof(decimal));


            elerow = finalele.NewRow();
            elerow["FOB"] = fob;
            elerow["FREIGHT"] = faight;
            elerow["INSU"] = insu;
            elerow["OTHER"] = other;
            elerow["TOTAL"] = total;
            elerow["TOTALLKR"] = totallkr;


            finalele.Rows.Add(elerow);

            DataTable cusdeccommon = bsObj.CHNLSVC.CustService.GetCusdecCommonNew("LK");



            List<ImportsBLContainer> blContainers = new List<ImportsBLContainer>();
            if (Hdrdata != null)
            {
                blContainers = bsObj.CHNLSVC.Financial.GetContainers(Hdrdata.Rows[0]["cuh_sun_req_no"].ToString());
            }
            else
            {
                blContainers = null;
            }
            if (blContainers.Count > 10)
            {
                blContainers = blContainers.Skip(10).Take(10).ToList();

            }
            DataTable page = new DataTable();
            DataRow pagerow;
            page.Columns.Add("Totalitems", typeof(Int32));
            pagerow = page.NewRow();
            pagerow["Totalitems"] = itm + 1;
            page.Rows.Add(pagerow);

            _goods_declarationsheet.Database.Tables["firstGooddata"].SetDataSource(firsgood);
            //  _goods_declaration.Database.Tables["TaxValues"].SetDataSource(taxvalues);
            _goods_declarationsheet.Database.Tables["HdrData"].SetDataSource(Hdrdata);

            int r = 0;
            foreach (DataRow secondpagedatanew in secondpagedata.Rows)
            {
                if (secondpagedata.Rows[r]["Line1"].ToString() == "") secondpagedata.Rows[r]["Line1"] = 0;
                if (secondpagedata.Rows[r]["Line2"].ToString() == "") secondpagedata.Rows[r]["Line2"] = 0;
                if (secondpagedata.Rows[r]["Line3"].ToString() == "") secondpagedata.Rows[r]["Line3"] = 0;
                r++;
            }

            _goods_declarationsheet.Database.Tables["SecondPage"].SetDataSource(secondpagedata);
            _goods_declarationsheet.Database.Tables["EleData"].SetDataSource(finalele);
            //  _goods_declaration.Database.Tables["Param"].SetDataSource(Param);
            _goods_declarationsheet.Database.Tables["CusdecCommon"].SetDataSource(cusdeccommon);
            _goods_declarationsheet.Database.Tables["Page"].SetDataSource(page);

            foreach (object repOp in _goods_declarationsheet.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "firstpagetax")
                    {
                        ReportDocument subRepDoc = _goods_declarationsheet.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["TaxValues"].SetDataSource(taxvalues);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "finaltaxvalues")
                    {
                        ReportDocument subRepDoc = _goods_declarationsheet.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["TaxValues"].SetDataSource(taxvalues);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "finalrighttax")
                    {
                        ReportDocument subRepDoc = _goods_declarationsheet.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["TaxValues"].SetDataSource(taxvalues);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "finalbottomtax")
                    {
                        ReportDocument subRepDoc = _goods_declarationsheet.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["TaxValues"].SetDataSource(taxvalues);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "TotalTaxsummery")
                    {
                        ReportDocument subRepDoc = _goods_declarationsheet.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["TaxValues"].SetDataSource(taxvalues);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "secondpageitemdetails")
                    {
                        ReportDocument subRepDoc = _goods_declarationsheet.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["Param"].SetDataSource(Param);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "containers")
                    {
                        ReportDocument subRepDoc = _goods_declarationsheet.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["Containers"].SetDataSource(blContainers);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    _cs.Dispose();
                }
            }
        }
        public void GoodsDecarrationSheetother(string entryNo, string com)
        {
            DataTable Hdrdata = new DataTable();
            DataTable maindata = new DataTable();
            DataTable Param = new DataTable();


            DataRow dr;
            int i = 0;
            int j = 0;

            int linenumber = 0;
            int nxtlinenumber = 0;
            int maxlinenumber = 0;

            Param.Columns.Add("Company", typeof(string));
            Param.Columns.Add("User", typeof(string));
            Param.Columns.Add("Location", typeof(string));
            Param.Columns.Add("cui_itm_desc", typeof(string));
            Param.Columns.Add("cui_gross_mass", typeof(string));
            Param.Columns.Add("cui_net_mass", typeof(string));
            Param.Columns.Add("cui_bl_no", typeof(string));
            Param.Columns.Add("cui_orgin_cnty", typeof(string));
            Param.Columns.Add("cui_pkgs", typeof(string));
            Param.Columns.Add("Line", typeof(int));
            Param.Columns.Add("MaxLine", typeof(int));
            Param.Columns.Add("cui_hs_cd", typeof(string));
            Param.Columns.Add("cui_itm_price", typeof(decimal));
            Param.Columns.Add("cui_qty", typeof(string));
            Param.Columns.Add("cui_bal_qty1", typeof(string));
            Param.Columns.Add("cui_bal_qty2", typeof(string));
            Param.Columns.Add("cui_bal_qty3", typeof(string));
            Param.Columns.Add("cui_req_qty", typeof(string));


            //tax values
            DataTable taxvalues = new DataTable();
            DataRow trow;

            taxvalues.Columns.Add("Type", typeof(string));
            taxvalues.Columns.Add("Tax", typeof(decimal));
            taxvalues.Columns.Add("Rate", typeof(decimal));
            taxvalues.Columns.Add("Ammount", typeof(decimal));
            taxvalues.Columns.Add("MP", typeof(decimal));
            taxvalues.Columns.Add("Line", typeof(int));
            taxvalues.Columns.Add("Line2", typeof(int));
            taxvalues.Columns.Add("Line3", typeof(int));
            taxvalues.Columns.Add("cui_itm_desc", typeof(string));
            taxvalues.Columns.Add("cui_gross_mass", typeof(string));
            taxvalues.Columns.Add("cui_net_mass", typeof(string));
            taxvalues.Columns.Add("cui_bl_no", typeof(string));
            taxvalues.Columns.Add("cui_orgin_cnty", typeof(string));
            taxvalues.Columns.Add("cui_pkgs", typeof(string));

            DataTable firsgood = new DataTable();
            DataRow firstrow;

            firsgood.Columns.Add("cui_itm_desc", typeof(string));
            firsgood.Columns.Add("cui_gross_mass", typeof(string));
            firsgood.Columns.Add("cui_net_mass", typeof(string));
            firsgood.Columns.Add("cui_bl_no", typeof(string));
            firsgood.Columns.Add("cui_orgin_cnty", typeof(string));
            firsgood.Columns.Add("cui_pkgs", typeof(string));
            firsgood.Columns.Add("cui_hs_cd", typeof(string));
            firsgood.Columns.Add("cui_qty", typeof(string));
            firsgood.Columns.Add("cui_bal_qty2", typeof(string));
            firsgood.Columns.Add("cui_bal_qty3", typeof(string));
            firsgood.Columns.Add("cui_req_qty", typeof(string));
            firsgood.Columns.Add("cui_itm_price", typeof(decimal));
            firsgood.Columns.Add("cui_line", typeof(Int32));

            int licount = 1;

            //header data
            Hdrdata = bsObj.CHNLSVC.CustService.GetCusdecHDRData(entryNo, com);
            // all goods data
            maindata = bsObj.CHNLSVC.CustService.GetGoodsDeclarationDetails(entryNo);
            //DataView dv = maindata.DefaultView;
            //dv.Sort = "cui_hs_cd ,cui_model, cuic_itm_cd,cui_line";
            //maindata = dv.ToTable();
            if (maindata.Rows.Count > 0)
            {
                foreach (DataRow dtRow in maindata.Rows)
                {
                    int nxtline;
                    linenumber = Convert.ToInt32(maindata.Rows[i]["cui_line"]);
                    if (i + 1 < maindata.Rows.Count)
                    {
                        nxtline = Convert.ToInt32(maindata.Rows[i + 1]["cui_line"]);
                    }
                    else
                    {
                        nxtline = 99999;
                    }



                    if (linenumber != nxtline)
                    {
                        trow = taxvalues.NewRow();
                        trow["Type"] = maindata.Rows[i]["cuic_ele_cd"].ToString();
                        trow["Tax"] = maindata.Rows[i]["cuic_ele_base"].ToString();
                        trow["Rate"] = maindata.Rows[i]["cuic_ele_rt"].ToString();
                        trow["Ammount"] = maindata.Rows[i]["cuic_ele_amt"].ToString();
                        trow["MP"] = maindata.Rows[i]["cuic_ele_mp"].ToString();
                        trow["Line"] = licount;
                        trow["Line2"] = licount;
                        trow["Line3"] = licount;
                        trow["cui_itm_desc"] = maindata.Rows[i]["cui_itm_desc"].ToString() + " " + maindata.Rows[i]["cui_model"].ToString();
                        trow["cui_gross_mass"] = maindata.Rows[i]["cui_gross_mass"].ToString();
                        trow["cui_net_mass"] = maindata.Rows[i]["cui_net_mass"].ToString();
                        trow["cui_bl_no"] = maindata.Rows[i]["cui_bl_no"].ToString();
                        trow["cui_orgin_cnty"] = maindata.Rows[i]["cui_orgin_cnty"].ToString();
                        trow["cui_pkgs"] = maindata.Rows[i]["cui_pkgs"].ToString();

                        taxvalues.Rows.Add(trow);
                        maxlinenumber = linenumber;
                        i++;
                        licount++;
                    }
                    else
                    {
                        trow = taxvalues.NewRow();
                        trow["Type"] = maindata.Rows[i]["cuic_ele_cd"].ToString();
                        trow["Tax"] = maindata.Rows[i]["cuic_ele_base"].ToString();
                        trow["Rate"] = maindata.Rows[i]["cuic_ele_rt"].ToString();
                        trow["Ammount"] = maindata.Rows[i]["cuic_ele_amt"].ToString();
                        trow["MP"] = maindata.Rows[i]["cuic_ele_mp"].ToString();
                        trow["Line"] = licount;
                        trow["Line2"] = licount;
                        trow["Line3"] = licount;
                        trow["cui_itm_desc"] = maindata.Rows[i]["cui_itm_desc"].ToString() + " " + maindata.Rows[i]["cui_model"].ToString();
                        trow["cui_gross_mass"] = maindata.Rows[i]["cui_gross_mass"].ToString();
                        trow["cui_net_mass"] = maindata.Rows[i]["cui_net_mass"].ToString();
                        trow["cui_bl_no"] = maindata.Rows[i]["cui_bl_no"].ToString();
                        trow["cui_orgin_cnty"] = maindata.Rows[i]["cui_orgin_cnty"].ToString();
                        trow["cui_pkgs"] = maindata.Rows[i]["cui_pkgs"].ToString();

                        taxvalues.Rows.Add(trow);
                        maxlinenumber = linenumber;
                        i++;
                    }


                }
            }

            if (Hdrdata.Rows.Count > 0)
            {
                trow = taxvalues.NewRow();
                trow["Type"] = "CE&S";
                trow["Tax"] = "1";
                trow["Rate"] = "1";
                trow["Ammount"] = Hdrdata.Rows[0]["CUH_COM_CHG"].ToString();
                trow["MP"] = "1";
                trow["Line"] = 1;
                trow["Line2"] = 1;
                trow["Line3"] = 1;
                taxvalues.Rows.Add(trow);

            }
            if (maindata.Rows.Count > 0)
            {
                foreach (DataRow dtRow in maindata.Rows)
                {
                    if (j < maindata.Rows.Count - 1)
                    {
                        linenumber = Convert.ToInt32(maindata.Rows[j]["cui_line"]);
                        nxtlinenumber = Convert.ToInt32(maindata.Rows[j + 1]["cui_line"]);

                        if (linenumber != nxtlinenumber)
                        {
                            dr = Param.NewRow();
                            dr["Company"] = "ABL";
                            dr["User"] = "ADMIN";
                            dr["Location"] = "DMP";
                            dr["cui_itm_desc"] = maindata.Rows[j]["cui_itm_desc"].ToString() + " " + maindata.Rows[j]["cui_model"].ToString();
                            dr["cui_gross_mass"] = maindata.Rows[j]["cui_gross_mass"].ToString();
                            dr["cui_net_mass"] = maindata.Rows[j]["cui_net_mass"].ToString();
                            dr["cui_bl_no"] = maindata.Rows[j]["cui_bl_no"].ToString();
                            dr["cui_orgin_cnty"] = maindata.Rows[j]["cui_orgin_cnty"].ToString();
                            dr["cui_pkgs"] = maindata.Rows[j]["cui_pkgs"].ToString();
                            dr["Line"] = linenumber;
                            dr["MaxLine"] = maxlinenumber;
                            dr["cui_hs_cd"] = maindata.Rows[j]["cui_hs_cd"].ToString();
                            dr["cui_qty"] = maindata.Rows[j]["cui_qty"].ToString();
                            dr["cui_bal_qty1"] = maindata.Rows[j]["cui_bal_qty1"].ToString();
                            dr["cui_bal_qty2"] = maindata.Rows[j]["cui_bal_qty2"].ToString();
                            dr["cui_bal_qty3"] = maindata.Rows[j]["cui_preferance"].ToString();
                            dr["cui_req_qty"] = maindata.Rows[j]["cui_req_qty"].ToString();
                            dr["cui_itm_price"] = Convert.ToDecimal(maindata.Rows[j]["cui_itm_price"].ToString());

                            Param.Rows.Add(dr);
                        }
                        if (j == maindata.Rows.Count - 2)
                        {
                            dr = Param.NewRow();
                            dr["Company"] = "ABL";
                            dr["User"] = "ADMIN";
                            dr["Location"] = "DMP";
                            dr["cui_itm_desc"] = maindata.Rows[j]["cui_itm_desc"].ToString() + " " + maindata.Rows[j]["cui_model"].ToString();
                            dr["cui_gross_mass"] = maindata.Rows[j]["cui_gross_mass"].ToString();
                            dr["cui_net_mass"] = maindata.Rows[j]["cui_net_mass"].ToString();
                            dr["cui_bl_no"] = maindata.Rows[j]["cui_bl_no"].ToString();
                            dr["cui_orgin_cnty"] = maindata.Rows[j]["cui_orgin_cnty"].ToString();
                            dr["cui_pkgs"] = maindata.Rows[j]["cui_pkgs"].ToString();
                            dr["Line"] = linenumber;
                            dr["MaxLine"] = maxlinenumber;
                            dr["cui_hs_cd"] = maindata.Rows[j]["cui_hs_cd"].ToString();
                            dr["cui_qty"] = maindata.Rows[j]["cui_qty"].ToString();
                            dr["cui_bal_qty1"] = maindata.Rows[j]["cui_bal_qty1"].ToString();
                            dr["cui_bal_qty2"] = maindata.Rows[j]["cui_bal_qty2"].ToString();
                            dr["cui_bal_qty3"] = maindata.Rows[j]["cui_preferance"].ToString();
                            dr["cui_req_qty"] = maindata.Rows[j]["cui_req_qty"].ToString();
                            dr["cui_itm_price"] = Convert.ToDecimal(maindata.Rows[j]["cui_itm_price"].ToString());
                            Param.Rows.Add(dr);
                        }
                    }
                    j++;
                }
            }
            if (maindata.Rows.Count > 0)
            {

                firstrow = firsgood.NewRow();

                firstrow["cui_itm_desc"] = maindata.Rows[0]["cui_itm_desc"].ToString() + " " + maindata.Rows[0]["cui_model"].ToString();
                firstrow["cui_gross_mass"] = maindata.Rows[0]["cui_gross_mass"].ToString();
                firstrow["cui_net_mass"] = maindata.Rows[0]["cui_net_mass"].ToString();
                firstrow["cui_bl_no"] = maindata.Rows[0]["cui_bl_no"].ToString();
                firstrow["cui_orgin_cnty"] = maindata.Rows[0]["cui_orgin_cnty"].ToString();
                firstrow["cui_pkgs"] = maindata.Rows[0]["cui_pkgs"].ToString();
                firstrow["cui_hs_cd"] = maindata.Rows[0]["cui_hs_cd"].ToString();
                firstrow["cui_qty"] = maindata.Rows[0]["cui_qty"].ToString();
                firstrow["cui_bal_qty2"] = maindata.Rows[0]["cui_bal_qty2"].ToString();
                firstrow["cui_bal_qty3"] = maindata.Rows[0]["cui_preferance"].ToString();
                firstrow["cui_req_qty"] = maindata.Rows[0]["cui_req_qty"].ToString();
                firstrow["cui_itm_price"] = Convert.ToDecimal(maindata.Rows[0]["cui_itm_price"].ToString());

                //getdoc line
                string sunbondno = Hdrdata.Rows[0]["cuh_sun_bond_no"].ToString();
                List<Cusdec_Goods_decl> maindatanew1 = bsObj.CHNLSVC.CustService.GetGoodsDeclarationDetailsList(sunbondno);

                var docno = maindatanew1.Where(a => a.cui_line == Convert.ToInt32(maindata.Rows[0]["cui_line"].ToString())).First().cui_oth_doc_line;

                firstrow["cui_line"] = Convert.ToInt32(docno);
                firsgood.Rows.Add(firstrow);
            }
            // create second page table
            DataTable secondpagedata = new DataTable();
            DataRow srow;

            secondpagedata.Columns.Add("cui_itm_desc1", typeof(string));
            secondpagedata.Columns.Add("cui_gross_mass1", typeof(string));
            secondpagedata.Columns.Add("cui_net_mass1", typeof(string));
            secondpagedata.Columns.Add("cui_bl_no1", typeof(string));
            secondpagedata.Columns.Add("cui_orgin_cnty1", typeof(string));
            secondpagedata.Columns.Add("cui_itm_desc2", typeof(string));
            secondpagedata.Columns.Add("cui_gross_mass2", typeof(string));
            secondpagedata.Columns.Add("cui_net_mass2", typeof(string));
            secondpagedata.Columns.Add("cui_bl_no2", typeof(string));
            secondpagedata.Columns.Add("cui_orgin_cnty2", typeof(string));
            secondpagedata.Columns.Add("cui_itm_desc3", typeof(string));
            secondpagedata.Columns.Add("cui_gross_mass3", typeof(string));
            secondpagedata.Columns.Add("cui_net_mass3", typeof(string));
            secondpagedata.Columns.Add("cui_bl_no3", typeof(string));
            secondpagedata.Columns.Add("cui_orgin_cnty3", typeof(string));
            secondpagedata.Columns.Add("Line1", typeof(Int16));
            secondpagedata.Columns.Add("Line2", typeof(Int16));
            secondpagedata.Columns.Add("Line3", typeof(Int16));
            secondpagedata.Columns.Add("LineCount1", typeof(Int16));
            secondpagedata.Columns.Add("LineCount2", typeof(Int16));
            secondpagedata.Columns.Add("LineCount3", typeof(Int16));
            secondpagedata.Columns.Add("cui_hs_cd1", typeof(string));
            secondpagedata.Columns.Add("cui_hs_cd2", typeof(string));
            secondpagedata.Columns.Add("cui_hs_cd3", typeof(string));
            secondpagedata.Columns.Add("cui_itm_price1", typeof(decimal));
            secondpagedata.Columns.Add("cui_itm_price2", typeof(decimal));
            secondpagedata.Columns.Add("cui_itm_price3", typeof(decimal));
            secondpagedata.Columns.Add("cui_bal_qty11", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty12", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty13", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty21", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty22", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty23", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty31", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty32", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty33", typeof(string));
            secondpagedata.Columns.Add("Realline1", typeof(Int16));
            secondpagedata.Columns.Add("Realline2", typeof(Int16));
            secondpagedata.Columns.Add("Realline3", typeof(Int16));
            int itm = 0;
            i = 1;
            if (Param.Rows.Count > 0)
            {
                for (int k = 0; k <= Param.Rows.Count; k = k + 3)
                {
                    srow = secondpagedata.NewRow();
                    if (i < Param.Rows.Count)
                    {
                        itm += 1;
                        srow["cui_itm_desc1"] = Param.Rows[i]["cui_itm_desc"].ToString();
                        srow["cui_gross_mass1"] = Param.Rows[i]["cui_gross_mass"].ToString();
                        srow["cui_net_mass1"] = Param.Rows[i]["cui_net_mass"].ToString();
                        srow["cui_bl_no1"] = Param.Rows[i]["cui_bl_no"].ToString();
                        srow["cui_orgin_cnty1"] = Param.Rows[i]["cui_orgin_cnty"].ToString();
                        srow["Line1"] = Convert.ToInt16(i + 1);
                        srow["LineCount1"] = Convert.ToInt16(i + 1);
                        srow["cui_hs_cd1"] = Param.Rows[i]["cui_hs_cd"].ToString();
                        srow["cui_itm_price1"] = Convert.ToDecimal(Param.Rows[i]["cui_itm_price"].ToString());
                        srow["cui_bal_qty11"] = Param.Rows[i]["cui_qty"].ToString();
                        srow["cui_bal_qty21"] = Param.Rows[i]["cui_bal_qty2"].ToString();
                        srow["cui_bal_qty31"] = Param.Rows[i]["cui_bal_qty3"].ToString();

                        //getdoc line
                        string sunbondno = Hdrdata.Rows[0]["cuh_sun_bond_no"].ToString();
                        List<Cusdec_Goods_decl> maindatanew1 = bsObj.CHNLSVC.CustService.GetGoodsDeclarationDetailsList(sunbondno);

                        var docno = maindatanew1.Where(a => a.cui_line == Convert.ToInt16(Param.Rows[i]["Line"].ToString())).First().cui_oth_doc_line;
                        srow["Realline1"] = Convert.ToInt16(docno);
                    }

                    if (i + 1 < Param.Rows.Count)
                    {
                        itm += 1;
                        srow["cui_itm_desc2"] = Param.Rows[i + 1]["cui_itm_desc"].ToString();
                        srow["cui_gross_mass2"] = Param.Rows[i + 1]["cui_gross_mass"].ToString();
                        srow["cui_net_mass2"] = Param.Rows[i + 1]["cui_net_mass"].ToString();
                        srow["cui_bl_no2"] = Param.Rows[i + 1]["cui_bl_no"].ToString();
                        srow["cui_orgin_cnty2"] = Param.Rows[i + 1]["cui_orgin_cnty"].ToString();
                        srow["Line2"] = Convert.ToInt16(i + 2);
                        srow["LineCount2"] = Convert.ToInt16(i + 2);
                        srow["cui_hs_cd2"] = Param.Rows[i + 1]["cui_hs_cd"].ToString();
                        srow["cui_itm_price2"] = Convert.ToDecimal(Param.Rows[i + 1]["cui_itm_price"].ToString());
                        srow["cui_bal_qty12"] = Param.Rows[i + 1]["cui_qty"].ToString();
                        srow["cui_bal_qty22"] = Param.Rows[i + 1]["cui_bal_qty2"].ToString();
                        srow["cui_bal_qty32"] = Param.Rows[i + 1]["cui_bal_qty3"].ToString();

                        //getdoc line
                        string sunbondno = Hdrdata.Rows[0]["cuh_sun_bond_no"].ToString();
                        List<Cusdec_Goods_decl> maindatanew1 = bsObj.CHNLSVC.CustService.GetGoodsDeclarationDetailsList(sunbondno);

                        var docno = maindatanew1.Where(a => a.cui_line == Convert.ToInt16(Param.Rows[i + 1]["Line"].ToString())).First().cui_oth_doc_line;

                        srow["Realline2"] = Convert.ToInt16(docno);

                    }

                    if (i + 2 < Param.Rows.Count)
                    {
                        itm += 1;
                        srow["cui_itm_desc3"] = Param.Rows[i + 2]["cui_itm_desc"].ToString();
                        srow["cui_gross_mass3"] = Param.Rows[i + 2]["cui_gross_mass"].ToString();
                        srow["cui_net_mass3"] = Param.Rows[i + 2]["cui_net_mass"].ToString();
                        srow["cui_bl_no3"] = Param.Rows[i + 2]["cui_bl_no"].ToString();
                        srow["cui_orgin_cnty3"] = Param.Rows[i + 2]["cui_orgin_cnty"].ToString();
                        srow["Line3"] = Convert.ToInt16(i + 3);
                        srow["LineCount3"] = Convert.ToInt16(i + 3);
                        srow["cui_hs_cd3"] = Param.Rows[i + 2]["cui_hs_cd"].ToString();
                        srow["cui_itm_price3"] = Convert.ToDecimal(Param.Rows[i + 2]["cui_itm_price"].ToString());
                        srow["cui_bal_qty13"] = Param.Rows[i + 2]["cui_qty"].ToString();
                        srow["cui_bal_qty23"] = Param.Rows[i + 2]["cui_bal_qty2"].ToString();
                        srow["cui_bal_qty33"] = Param.Rows[i + 2]["cui_bal_qty3"].ToString();

                        //getdoc line
                        string sunbondno = Hdrdata.Rows[0]["cuh_sun_bond_no"].ToString();
                        List<Cusdec_Goods_decl> maindatanew1 = bsObj.CHNLSVC.CustService.GetGoodsDeclarationDetailsList(sunbondno);

                        var docno = maindatanew1.Where(a => a.cui_line == Convert.ToInt16(Param.Rows[i + 2]["Line"].ToString())).First().cui_oth_doc_line;


                        srow["Realline3"] = Convert.ToInt16(docno);
                    }

                    secondpagedata.Rows.Add(srow);
                    i = i + 3;
                }
            }

            DataTable eledata = new DataTable();
            i = 0;
            string cat = "";
            decimal fob = 0;
            decimal foblkr = 0;
            decimal faight = 0;
            decimal faightlkr = 0;
            decimal insu = 0;
            decimal insulkr = 0;
            decimal other = 0;
            decimal otherlkr = 0;
            decimal total = 0;
            decimal totallkr = 0;

            eledata = bsObj.CHNLSVC.CustService.GetCusdecElementnew(entryNo);
            foreach (DataRow ele in eledata.Rows)
            {
                cat = eledata.Rows[i]["cus_ele_cd"].ToString();

                if (cat == "OTH")
                {
                    other = Convert.ToDecimal(eledata.Rows[i]["cus_amt"]);
                    otherlkr = Convert.ToDecimal(eledata.Rows[i]["cus_amt_com"]);
                }
                if (cat == "INSU")
                {
                    insu = Convert.ToDecimal(eledata.Rows[i]["cus_amt"]);
                    insulkr = Convert.ToDecimal(eledata.Rows[i]["cus_amt_com"]);
                }
                if (cat == "FRGT")
                {
                    faight = Convert.ToDecimal(eledata.Rows[i]["cus_amt"]);
                    faightlkr = Convert.ToDecimal(eledata.Rows[i]["cus_amt_com"]);

                }
                if (cat == "COST")
                {
                    fob = Convert.ToDecimal(eledata.Rows[i]["cus_amt"]);
                    foblkr = Convert.ToDecimal(eledata.Rows[i]["cus_amt_com"]);
                }
                i++;

            }
            total = other + insu + faight + fob;
            totallkr = otherlkr + insulkr + faightlkr + foblkr;

            DataTable finalele = new DataTable();
            DataRow elerow;
            finalele.Columns.Add("FOB", typeof(decimal));
            finalele.Columns.Add("FREIGHT", typeof(decimal));
            finalele.Columns.Add("INSU", typeof(decimal));
            finalele.Columns.Add("OTHER", typeof(decimal));
            finalele.Columns.Add("TOTAL", typeof(decimal));
            finalele.Columns.Add("TOTALLKR", typeof(decimal));

            elerow = finalele.NewRow();
            elerow["FOB"] = fob;
            elerow["FREIGHT"] = faight;
            elerow["INSU"] = insu;
            elerow["OTHER"] = other;
            elerow["TOTAL"] = total;
            elerow["TOTALLKR"] = totallkr;

            finalele.Rows.Add(elerow);

            DataTable cusdeccommon = bsObj.CHNLSVC.CustService.GetCusdecCommonNew("LK");

            List<ImportsBLContainer> blContainers = new List<ImportsBLContainer>();
            if (Hdrdata != null)
            {
                blContainers = bsObj.CHNLSVC.Financial.GetContainers(Hdrdata.Rows[0]["cuh_sun_req_no"].ToString());
            }
            else
            {
                blContainers = null;
            }

            DataTable page = new DataTable();
            DataRow pagerow;
            page.Columns.Add("Totalitems", typeof(Int32));
            pagerow = page.NewRow();
            pagerow["Totalitems"] = itm + 1;
            page.Rows.Add(pagerow);

            _goods_declarationsheetother.Database.Tables["firstGooddata"].SetDataSource(firsgood);
            //  _goods_declaration.Database.Tables["TaxValues"].SetDataSource(taxvalues);
            _goods_declarationsheetother.Database.Tables["HdrData"].SetDataSource(Hdrdata);

            int r = 0;
            foreach (DataRow secondpagedatanew in secondpagedata.Rows)
            {
                if (secondpagedata.Rows[r]["Line1"].ToString() == "") secondpagedata.Rows[r]["Line1"] = 0;
                if (secondpagedata.Rows[r]["Line2"].ToString() == "") secondpagedata.Rows[r]["Line2"] = 0;
                if (secondpagedata.Rows[r]["Line3"].ToString() == "") secondpagedata.Rows[r]["Line3"] = 0;
                r++;
            }

            _goods_declarationsheetother.Database.Tables["SecondPage"].SetDataSource(secondpagedata);
            _goods_declarationsheetother.Database.Tables["EleData"].SetDataSource(finalele);
            //  _goods_declaration.Database.Tables["Param"].SetDataSource(Param);
            _goods_declarationsheetother.Database.Tables["CusdecCommon"].SetDataSource(cusdeccommon);
            _goods_declarationsheetother.Database.Tables["Page"].SetDataSource(page);

            foreach (object repOp in _goods_declarationsheetother.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "firstpagetax")
                    {
                        ReportDocument subRepDoc = _goods_declarationsheetother.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["TaxValues"].SetDataSource(taxvalues);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "finaltaxvalues")
                    {
                        ReportDocument subRepDoc = _goods_declarationsheetother.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["TaxValues"].SetDataSource(taxvalues);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "finalrighttax")
                    {
                        ReportDocument subRepDoc = _goods_declarationsheetother.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["TaxValues"].SetDataSource(taxvalues);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "finalbottomtax")
                    {
                        ReportDocument subRepDoc = _goods_declarationsheetother.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["TaxValues"].SetDataSource(taxvalues);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "TotalTaxsummery")
                    {
                        ReportDocument subRepDoc = _goods_declarationsheetother.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["TaxValues"].SetDataSource(taxvalues);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "secondpageitemdetails")
                    {
                        ReportDocument subRepDoc = _goods_declarationsheetother.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["Param"].SetDataSource(Param);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "containers")
                    {
                        ReportDocument subRepDoc = _goods_declarationsheetother.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["Containers"].SetDataSource(blContainers);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    _cs.Dispose();
                }
            }
        }

        public void GoodsDecarrationSheetRebond(string entryNo, string com)
        {
            DataTable Hdrdata = new DataTable();
            DataTable maindata = new DataTable();
            DataTable Param = new DataTable();


            DataRow dr;
            int i = 0;
            int j = 0;

            int linenumber = 0;
            int nxtlinenumber = 0;
            int maxlinenumber = 0;

            Param.Columns.Add("Company", typeof(string));
            Param.Columns.Add("User", typeof(string));
            Param.Columns.Add("Location", typeof(string));
            Param.Columns.Add("cui_itm_desc", typeof(string));
            Param.Columns.Add("cui_gross_mass", typeof(string));
            Param.Columns.Add("cui_net_mass", typeof(string));
            Param.Columns.Add("cui_bl_no", typeof(string));
            Param.Columns.Add("cui_orgin_cnty", typeof(string));
            Param.Columns.Add("cui_pkgs", typeof(string));
            Param.Columns.Add("Line", typeof(int));
            Param.Columns.Add("MaxLine", typeof(int));
            Param.Columns.Add("cui_hs_cd", typeof(string));
            Param.Columns.Add("cui_itm_price", typeof(decimal));
            Param.Columns.Add("cui_qty", typeof(string));
            Param.Columns.Add("cui_bal_qty1", typeof(string));
            Param.Columns.Add("cui_bal_qty2", typeof(string));
            Param.Columns.Add("cui_bal_qty3", typeof(string));
            Param.Columns.Add("cui_req_qty", typeof(string));


            //tax values
            DataTable taxvalues = new DataTable();
            DataRow trow;

            taxvalues.Columns.Add("Type", typeof(string));
            taxvalues.Columns.Add("Tax", typeof(decimal));
            taxvalues.Columns.Add("Rate", typeof(decimal));
            taxvalues.Columns.Add("Ammount", typeof(decimal));
            taxvalues.Columns.Add("MP", typeof(decimal));
            taxvalues.Columns.Add("Line", typeof(int));
            taxvalues.Columns.Add("Line2", typeof(int));
            taxvalues.Columns.Add("Line3", typeof(int));
            taxvalues.Columns.Add("cui_itm_desc", typeof(string));
            taxvalues.Columns.Add("cui_gross_mass", typeof(string));
            taxvalues.Columns.Add("cui_net_mass", typeof(string));
            taxvalues.Columns.Add("cui_bl_no", typeof(string));
            taxvalues.Columns.Add("cui_orgin_cnty", typeof(string));
            taxvalues.Columns.Add("cui_pkgs", typeof(string));

            DataTable firsgood = new DataTable();
            DataRow firstrow;

            firsgood.Columns.Add("cui_itm_desc", typeof(string));
            firsgood.Columns.Add("cui_gross_mass", typeof(string));
            firsgood.Columns.Add("cui_net_mass", typeof(string));
            firsgood.Columns.Add("cui_bl_no", typeof(string));
            firsgood.Columns.Add("cui_orgin_cnty", typeof(string));
            firsgood.Columns.Add("cui_pkgs", typeof(string));
            firsgood.Columns.Add("cui_hs_cd", typeof(string));
            firsgood.Columns.Add("cui_qty", typeof(string));
            firsgood.Columns.Add("cui_bal_qty2", typeof(string));
            firsgood.Columns.Add("cui_bal_qty3", typeof(string));
            firsgood.Columns.Add("cui_req_qty", typeof(string));
            firsgood.Columns.Add("cui_itm_price", typeof(decimal));
            firsgood.Columns.Add("cui_line", typeof(Int32));

            int licount = 1;
            // all goods data
            maindata = bsObj.CHNLSVC.CustService.GetGoodsDeclarationDetails(entryNo);
            //header data
            Hdrdata = bsObj.CHNLSVC.CustService.GetCusdecHDRData(entryNo, com);
            //DataView dv = maindata.DefaultView;
            //dv.Sort = "cui_hs_cd ,cui_model, cuic_itm_cd,cui_line";
            //maindata = dv.ToTable();
            if (maindata.Rows.Count > 0)
            {
                foreach (DataRow dtRow in maindata.Rows)
                {
                    int nxtline;
                    linenumber = Convert.ToInt32(maindata.Rows[i]["cui_line"]);
                    if (i + 1 < maindata.Rows.Count)
                    {
                        nxtline = Convert.ToInt32(maindata.Rows[i + 1]["cui_line"]);
                    }
                    else
                    {
                        nxtline = 99999;
                    }



                    if (linenumber != nxtline)
                    {
                        trow = taxvalues.NewRow();
                        trow["Type"] = maindata.Rows[i]["cuic_ele_cd"].ToString();
                        trow["Tax"] = maindata.Rows[i]["cuic_ele_base"].ToString();
                        trow["Rate"] = maindata.Rows[i]["cuic_ele_rt"].ToString();
                        trow["Ammount"] = maindata.Rows[i]["cuic_ele_amt"].ToString();
                        trow["MP"] = maindata.Rows[i]["cuic_ele_mp"].ToString();
                        trow["Line"] = licount;
                        trow["Line2"] = licount;
                        trow["Line3"] = licount;
                        trow["cui_itm_desc"] = maindata.Rows[i]["cui_itm_desc"].ToString() + " " + maindata.Rows[i]["cui_model"].ToString();
                        trow["cui_gross_mass"] = maindata.Rows[i]["cui_gross_mass"].ToString();
                        trow["cui_net_mass"] = maindata.Rows[i]["cui_net_mass"].ToString();
                        trow["cui_bl_no"] = maindata.Rows[i]["cui_bl_no"].ToString();
                        trow["cui_orgin_cnty"] = maindata.Rows[i]["cui_orgin_cnty"].ToString();
                        trow["cui_pkgs"] = maindata.Rows[i]["cui_pkgs"].ToString();

                        taxvalues.Rows.Add(trow);
                        maxlinenumber = linenumber;
                        i++;
                        licount++;
                    }
                    else
                    {
                        trow = taxvalues.NewRow();
                        trow["Type"] = maindata.Rows[i]["cuic_ele_cd"].ToString();
                        trow["Tax"] = maindata.Rows[i]["cuic_ele_base"].ToString();
                        trow["Rate"] = maindata.Rows[i]["cuic_ele_rt"].ToString();
                        trow["Ammount"] = maindata.Rows[i]["cuic_ele_amt"].ToString();
                        trow["MP"] = maindata.Rows[i]["cuic_ele_mp"].ToString();
                        trow["Line"] = licount;
                        trow["Line2"] = licount;
                        trow["Line3"] = licount;
                        trow["cui_itm_desc"] = maindata.Rows[i]["cui_itm_desc"].ToString() + " " + maindata.Rows[i]["cui_model"].ToString();
                        trow["cui_gross_mass"] = maindata.Rows[i]["cui_gross_mass"].ToString();
                        trow["cui_net_mass"] = maindata.Rows[i]["cui_net_mass"].ToString();
                        trow["cui_bl_no"] = maindata.Rows[i]["cui_bl_no"].ToString();
                        trow["cui_orgin_cnty"] = maindata.Rows[i]["cui_orgin_cnty"].ToString();
                        trow["cui_pkgs"] = maindata.Rows[i]["cui_pkgs"].ToString();

                        taxvalues.Rows.Add(trow);
                        maxlinenumber = linenumber;
                        i++;
                    }

                }
            }

            if (Hdrdata.Rows.Count > 0)
            {
                trow = taxvalues.NewRow();
                trow["Type"] = "CE&S";
                trow["Tax"] = "1";
                trow["Rate"] = "1";
                trow["Ammount"] = Hdrdata.Rows[0]["CUH_COM_CHG"].ToString();
                trow["MP"] = "1";
                trow["Line"] = 1;
                trow["Line2"] = 1;
                trow["Line3"] = 1;
                taxvalues.Rows.Add(trow);

            }
            if (maindata.Rows.Count > 0)
            {
                foreach (DataRow dtRow in maindata.Rows)
                {
                    if (j < maindata.Rows.Count - 1)
                    {
                        linenumber = Convert.ToInt32(maindata.Rows[j]["cui_line"]);
                        nxtlinenumber = Convert.ToInt32(maindata.Rows[j + 1]["cui_line"]);

                        if (linenumber != nxtlinenumber)
                        {
                            dr = Param.NewRow();
                            dr["Company"] = "ABL";
                            dr["User"] = "ADMIN";
                            dr["Location"] = "DMP";
                            dr["cui_itm_desc"] = maindata.Rows[j]["cui_itm_desc"].ToString() + " " + maindata.Rows[j]["cui_model"].ToString();
                            dr["cui_gross_mass"] = maindata.Rows[j]["cui_gross_mass"].ToString();
                            dr["cui_net_mass"] = maindata.Rows[j]["cui_net_mass"].ToString();
                            dr["cui_bl_no"] = maindata.Rows[j]["cui_bl_no"].ToString();
                            dr["cui_orgin_cnty"] = maindata.Rows[j]["cui_orgin_cnty"].ToString();
                            dr["cui_pkgs"] = maindata.Rows[j]["cui_pkgs"].ToString();
                            dr["Line"] = linenumber;
                            dr["MaxLine"] = maxlinenumber;
                            dr["cui_hs_cd"] = maindata.Rows[j]["cui_hs_cd"].ToString();
                            dr["cui_qty"] = maindata.Rows[j]["cui_qty"].ToString();
                            dr["cui_bal_qty1"] = maindata.Rows[j]["cui_bal_qty1"].ToString();
                            dr["cui_bal_qty2"] = maindata.Rows[j]["cui_bal_qty2"].ToString();
                            dr["cui_bal_qty3"] = maindata.Rows[j]["cui_bal_qty3"].ToString();
                            dr["cui_req_qty"] = maindata.Rows[j]["cui_req_qty"].ToString();
                            dr["cui_itm_price"] = Convert.ToDecimal(maindata.Rows[j]["cui_itm_price"].ToString());

                            Param.Rows.Add(dr);
                        }
                        if (j == maindata.Rows.Count - 2)
                        {
                            dr = Param.NewRow();
                            dr["Company"] = "ABL";
                            dr["User"] = "ADMIN";
                            dr["Location"] = "DMP";
                            dr["cui_itm_desc"] = maindata.Rows[j]["cui_itm_desc"].ToString() + " " + maindata.Rows[j]["cui_model"].ToString();
                            dr["cui_gross_mass"] = maindata.Rows[j]["cui_gross_mass"].ToString();
                            dr["cui_net_mass"] = maindata.Rows[j]["cui_net_mass"].ToString();
                            dr["cui_bl_no"] = maindata.Rows[j]["cui_bl_no"].ToString();
                            dr["cui_orgin_cnty"] = maindata.Rows[j]["cui_orgin_cnty"].ToString();
                            dr["cui_pkgs"] = maindata.Rows[j]["cui_pkgs"].ToString();
                            dr["Line"] = linenumber;
                            dr["MaxLine"] = maxlinenumber;
                            dr["cui_hs_cd"] = maindata.Rows[j]["cui_hs_cd"].ToString();
                            dr["cui_qty"] = maindata.Rows[j]["cui_qty"].ToString();
                            dr["cui_bal_qty1"] = maindata.Rows[j]["cui_bal_qty1"].ToString();
                            dr["cui_bal_qty2"] = maindata.Rows[j]["cui_bal_qty2"].ToString();
                            dr["cui_bal_qty3"] = maindata.Rows[j]["cui_bal_qty3"].ToString();
                            dr["cui_req_qty"] = maindata.Rows[j]["cui_req_qty"].ToString();
                            dr["cui_itm_price"] = Convert.ToDecimal(maindata.Rows[j]["cui_itm_price"].ToString());
                            Param.Rows.Add(dr);
                        }
                    }
                    j++;
                }
            }
            if (maindata.Rows.Count > 0)
            {

                firstrow = firsgood.NewRow();

                firstrow["cui_itm_desc"] = maindata.Rows[0]["cui_itm_desc"].ToString() + " " + maindata.Rows[0]["cui_model"].ToString();
                firstrow["cui_gross_mass"] = maindata.Rows[0]["cui_gross_mass"].ToString();
                firstrow["cui_net_mass"] = maindata.Rows[0]["cui_net_mass"].ToString();
                firstrow["cui_bl_no"] = maindata.Rows[0]["cui_bl_no"].ToString();
                firstrow["cui_orgin_cnty"] = maindata.Rows[0]["cui_orgin_cnty"].ToString();
                firstrow["cui_pkgs"] = maindata.Rows[0]["cui_pkgs"].ToString();
                firstrow["cui_hs_cd"] = maindata.Rows[0]["cui_hs_cd"].ToString();
                firstrow["cui_qty"] = maindata.Rows[0]["cui_qty"].ToString();
                firstrow["cui_bal_qty2"] = maindata.Rows[0]["cui_bal_qty2"].ToString();
                firstrow["cui_bal_qty3"] = maindata.Rows[0]["cui_bal_qty3"].ToString();
                firstrow["cui_req_qty"] = maindata.Rows[0]["cui_req_qty"].ToString();
                firstrow["cui_itm_price"] = Convert.ToDecimal(maindata.Rows[0]["cui_itm_price"].ToString());

                //getdoc line
                string sunbondno = Hdrdata.Rows[0]["cuh_sun_bond_no"].ToString();
                List<Cusdec_Goods_decl> maindatanew1 = bsObj.CHNLSVC.CustService.GetGoodsDeclarationDetailsList(sunbondno);

                var docno = maindatanew1.Where(a => a.cui_line == Convert.ToInt32(maindata.Rows[0]["cui_line"].ToString())).First().cui_oth_doc_line;

                firstrow["cui_line"] = Convert.ToInt32(docno);

                firsgood.Rows.Add(firstrow);
            }
            // create second page table
            DataTable secondpagedata = new DataTable();
            DataRow srow;

            secondpagedata.Columns.Add("cui_itm_desc1", typeof(string));
            secondpagedata.Columns.Add("cui_gross_mass1", typeof(string));
            secondpagedata.Columns.Add("cui_net_mass1", typeof(string));
            secondpagedata.Columns.Add("cui_bl_no1", typeof(string));
            secondpagedata.Columns.Add("cui_orgin_cnty1", typeof(string));
            secondpagedata.Columns.Add("cui_itm_desc2", typeof(string));
            secondpagedata.Columns.Add("cui_gross_mass2", typeof(string));
            secondpagedata.Columns.Add("cui_net_mass2", typeof(string));
            secondpagedata.Columns.Add("cui_bl_no2", typeof(string));
            secondpagedata.Columns.Add("cui_orgin_cnty2", typeof(string));
            secondpagedata.Columns.Add("cui_itm_desc3", typeof(string));
            secondpagedata.Columns.Add("cui_gross_mass3", typeof(string));
            secondpagedata.Columns.Add("cui_net_mass3", typeof(string));
            secondpagedata.Columns.Add("cui_bl_no3", typeof(string));
            secondpagedata.Columns.Add("cui_orgin_cnty3", typeof(string));
            secondpagedata.Columns.Add("Line1", typeof(Int16));
            secondpagedata.Columns.Add("Line2", typeof(Int16));
            secondpagedata.Columns.Add("Line3", typeof(Int16));
            secondpagedata.Columns.Add("LineCount1", typeof(Int16));
            secondpagedata.Columns.Add("LineCount2", typeof(Int16));
            secondpagedata.Columns.Add("LineCount3", typeof(Int16));
            secondpagedata.Columns.Add("cui_hs_cd1", typeof(string));
            secondpagedata.Columns.Add("cui_hs_cd2", typeof(string));
            secondpagedata.Columns.Add("cui_hs_cd3", typeof(string));
            secondpagedata.Columns.Add("cui_itm_price1", typeof(decimal));
            secondpagedata.Columns.Add("cui_itm_price2", typeof(decimal));
            secondpagedata.Columns.Add("cui_itm_price3", typeof(decimal));
            secondpagedata.Columns.Add("cui_bal_qty11", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty12", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty13", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty21", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty22", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty23", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty31", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty32", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty33", typeof(string));
            secondpagedata.Columns.Add("Realline1", typeof(Int16));
            secondpagedata.Columns.Add("Realline2", typeof(Int16));
            secondpagedata.Columns.Add("Realline3", typeof(Int16));

            int itm = 0;
            i = 1;
            if (Param.Rows.Count > 0)
            {
                for (int k = 0; k <= Param.Rows.Count; k = k + 3)
                {
                    srow = secondpagedata.NewRow();
                    if (i < Param.Rows.Count)
                    {
                        itm += 1;
                        srow["cui_itm_desc1"] = Param.Rows[i]["cui_itm_desc"].ToString();
                        srow["cui_gross_mass1"] = Param.Rows[i]["cui_gross_mass"].ToString();
                        srow["cui_net_mass1"] = Param.Rows[i]["cui_net_mass"].ToString();
                        srow["cui_bl_no1"] = Param.Rows[i]["cui_bl_no"].ToString();
                        srow["cui_orgin_cnty1"] = Param.Rows[i]["cui_orgin_cnty"].ToString();
                        srow["Line1"] = Convert.ToInt16(i + 1);
                        srow["LineCount1"] = Convert.ToInt16(i + 1);
                        srow["cui_hs_cd1"] = Param.Rows[i]["cui_hs_cd"].ToString();
                        srow["cui_itm_price1"] = Convert.ToDecimal(Param.Rows[i]["cui_itm_price"].ToString());
                        srow["cui_bal_qty11"] = Param.Rows[i]["cui_qty"].ToString();
                        srow["cui_bal_qty21"] = Param.Rows[i]["cui_bal_qty2"].ToString();
                        srow["cui_bal_qty31"] = Param.Rows[i]["cui_bal_qty3"].ToString();
                        //getdoc line
                        string sunbondno = Hdrdata.Rows[0]["cuh_sun_bond_no"].ToString();
                        List<Cusdec_Goods_decl> maindatanew1 = bsObj.CHNLSVC.CustService.GetGoodsDeclarationDetailsList(sunbondno);

                        var docno = maindatanew1.Where(a => a.cui_line == Convert.ToInt16(Param.Rows[i]["Line"].ToString())).First().cui_oth_doc_line;
                        srow["Realline1"] = Convert.ToInt16(docno);
                    }

                    if (i + 1 < Param.Rows.Count)
                    {
                        itm += 1;
                        srow["cui_itm_desc2"] = Param.Rows[i + 1]["cui_itm_desc"].ToString();
                        srow["cui_gross_mass2"] = Param.Rows[i + 1]["cui_gross_mass"].ToString();
                        srow["cui_net_mass2"] = Param.Rows[i + 1]["cui_net_mass"].ToString();
                        srow["cui_bl_no2"] = Param.Rows[i + 1]["cui_bl_no"].ToString();
                        srow["cui_orgin_cnty2"] = Param.Rows[i + 1]["cui_orgin_cnty"].ToString();
                        srow["Line2"] = Convert.ToInt16(i + 2);
                        srow["LineCount2"] = Convert.ToInt16(i + 2);
                        srow["cui_hs_cd2"] = Param.Rows[i + 1]["cui_hs_cd"].ToString();
                        srow["cui_itm_price2"] = Convert.ToDecimal(Param.Rows[i + 1]["cui_itm_price"].ToString());
                        srow["cui_bal_qty12"] = Param.Rows[i + 1]["cui_qty"].ToString();
                        srow["cui_bal_qty22"] = Param.Rows[i + 1]["cui_bal_qty2"].ToString();
                        srow["cui_bal_qty32"] = Param.Rows[i + 1]["cui_bal_qty3"].ToString();
                        //getdoc line
                        string sunbondno = Hdrdata.Rows[0]["cuh_sun_bond_no"].ToString();
                        List<Cusdec_Goods_decl> maindatanew1 = bsObj.CHNLSVC.CustService.GetGoodsDeclarationDetailsList(sunbondno);

                        var docno = maindatanew1.Where(a => a.cui_line == Convert.ToInt16(Param.Rows[i + 1]["Line"].ToString())).First().cui_oth_doc_line;

                        srow["Realline2"] = Convert.ToInt16(docno);
                    }

                    if (i + 2 < Param.Rows.Count)
                    {
                        itm += 1;
                        srow["cui_itm_desc3"] = Param.Rows[i + 2]["cui_itm_desc"].ToString();
                        srow["cui_gross_mass3"] = Param.Rows[i + 2]["cui_gross_mass"].ToString();
                        srow["cui_net_mass3"] = Param.Rows[i + 2]["cui_net_mass"].ToString();
                        srow["cui_bl_no3"] = Param.Rows[i + 2]["cui_bl_no"].ToString();
                        srow["cui_orgin_cnty3"] = Param.Rows[i + 2]["cui_orgin_cnty"].ToString();
                        srow["Line3"] = Convert.ToInt16(i + 3);
                        srow["LineCount3"] = Convert.ToInt16(i + 3);
                        srow["cui_hs_cd3"] = Param.Rows[i + 2]["cui_hs_cd"].ToString();
                        srow["cui_itm_price3"] = Convert.ToDecimal(Param.Rows[i + 2]["cui_itm_price"].ToString());
                        srow["cui_bal_qty13"] = Param.Rows[i + 2]["cui_qty"].ToString();
                        srow["cui_bal_qty23"] = Param.Rows[i + 2]["cui_bal_qty2"].ToString();
                        srow["cui_bal_qty33"] = Param.Rows[i + 2]["cui_bal_qty3"].ToString();
                        //getdoc line
                        string sunbondno = Hdrdata.Rows[0]["cuh_sun_bond_no"].ToString();
                        List<Cusdec_Goods_decl> maindatanew1 = bsObj.CHNLSVC.CustService.GetGoodsDeclarationDetailsList(sunbondno);

                        var docno = maindatanew1.Where(a => a.cui_line == Convert.ToInt16(Param.Rows[i + 2]["Line"].ToString())).First().cui_oth_doc_line;


                        srow["Realline3"] = Convert.ToInt16(docno);
                    }

                    secondpagedata.Rows.Add(srow);
                    i = i + 3;
                }
            }

            DataTable eledata = new DataTable();
            i = 0;
            string cat = "";
            decimal fob = 0;
            decimal foblkr = 0;
            decimal faight = 0;
            decimal faightlkr = 0;
            decimal insu = 0;
            decimal insulkr = 0;
            decimal other = 0;
            decimal otherlkr = 0;
            decimal total = 0;
            decimal totallkr = 0;

            eledata = bsObj.CHNLSVC.CustService.GetCusdecElementnew(entryNo);
            foreach (DataRow ele in eledata.Rows)
            {
                cat = eledata.Rows[i]["cus_ele_cd"].ToString();

                if (cat == "OTH")
                {
                    other = Convert.ToDecimal(eledata.Rows[i]["cus_amt"]);
                    otherlkr = Convert.ToDecimal(eledata.Rows[i]["cus_amt_com"]);
                }
                if (cat == "INSU")
                {
                    insu = Convert.ToDecimal(eledata.Rows[i]["cus_amt"]);
                    insulkr = Convert.ToDecimal(eledata.Rows[i]["cus_amt_com"]);
                }
                if (cat == "FRGT")
                {
                    faight = Convert.ToDecimal(eledata.Rows[i]["cus_amt"]);
                    faightlkr = Convert.ToDecimal(eledata.Rows[i]["cus_amt_com"]);

                }
                if (cat == "COST")
                {
                    fob = Convert.ToDecimal(eledata.Rows[i]["cus_amt"]);
                    foblkr = Convert.ToDecimal(eledata.Rows[i]["cus_amt_com"]);
                }
                i++;

            }
            total = other + insu + faight + fob;
            totallkr = otherlkr + insulkr + faightlkr + foblkr;

            DataTable finalele = new DataTable();
            DataRow elerow;
            finalele.Columns.Add("FOB", typeof(decimal));
            finalele.Columns.Add("FREIGHT", typeof(decimal));
            finalele.Columns.Add("INSU", typeof(decimal));
            finalele.Columns.Add("OTHER", typeof(decimal));
            finalele.Columns.Add("TOTAL", typeof(decimal));
            finalele.Columns.Add("TOTALLKR", typeof(decimal));

            elerow = finalele.NewRow();
            elerow["FOB"] = fob;
            elerow["FREIGHT"] = faight;
            elerow["INSU"] = insu;
            elerow["OTHER"] = other;
            elerow["TOTAL"] = total;
            elerow["TOTALLKR"] = totallkr;

            finalele.Rows.Add(elerow);

            DataTable cusdeccommon = bsObj.CHNLSVC.CustService.GetCusdecCommonNew("LK");



            List<ImportsBLContainer> blContainers = new List<ImportsBLContainer>();
            if (Hdrdata != null)
            {
                blContainers = bsObj.CHNLSVC.Financial.GetContainers(Hdrdata.Rows[0]["cuh_sun_req_no"].ToString());
            }
            else
            {
                blContainers = null;
            }

            DataTable page = new DataTable();
            DataRow pagerow;
            page.Columns.Add("Totalitems", typeof(Int32));
            pagerow = page.NewRow();
            pagerow["Totalitems"] = itm + 1;
            page.Rows.Add(pagerow);

            _goods_declarationsheetRebond.Database.Tables["firstGooddata"].SetDataSource(firsgood);
            //  _goods_declaration.Database.Tables["TaxValues"].SetDataSource(taxvalues);
            _goods_declarationsheetRebond.Database.Tables["HdrData"].SetDataSource(Hdrdata);

            int r = 0;
            foreach (DataRow secondpagedatanew in secondpagedata.Rows)
            {
                if (secondpagedata.Rows[r]["Line1"].ToString() == "") secondpagedata.Rows[r]["Line1"] = 0;
                if (secondpagedata.Rows[r]["Line2"].ToString() == "") secondpagedata.Rows[r]["Line2"] = 0;
                if (secondpagedata.Rows[r]["Line3"].ToString() == "") secondpagedata.Rows[r]["Line3"] = 0;
                r++;
            }

            _goods_declarationsheetRebond.Database.Tables["SecondPage"].SetDataSource(secondpagedata);
            _goods_declarationsheetRebond.Database.Tables["EleData"].SetDataSource(finalele);
            //  _goods_declaration.Database.Tables["Param"].SetDataSource(Param);
            _goods_declarationsheetRebond.Database.Tables["CusdecCommon"].SetDataSource(cusdeccommon);
            _goods_declarationsheetRebond.Database.Tables["Page"].SetDataSource(page);

            foreach (object repOp in _goods_declarationsheetRebond.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "firstpagetax")
                    {
                        ReportDocument subRepDoc = _goods_declarationsheetRebond.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["TaxValues"].SetDataSource(taxvalues);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "finaltaxvalues")
                    {
                        ReportDocument subRepDoc = _goods_declarationsheetRebond.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["TaxValues"].SetDataSource(taxvalues);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "finalrighttax")
                    {
                        ReportDocument subRepDoc = _goods_declarationsheetRebond.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["TaxValues"].SetDataSource(taxvalues);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "finalbottomtax")
                    {
                        ReportDocument subRepDoc = _goods_declarationsheetRebond.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["TaxValues"].SetDataSource(taxvalues);
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "TotalTaxsummery")
                    {
                        ReportDocument subRepDoc = _goods_declarationsheetRebond.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["TaxValues"].SetDataSource(taxvalues);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "secondpageitemdetails")
                    {
                        ReportDocument subRepDoc = _goods_declarationsheetRebond.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["Param"].SetDataSource(Param);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "containers")
                    {
                        ReportDocument subRepDoc = _goods_declarationsheetRebond.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["Containers"].SetDataSource(blContainers);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    _cs.Dispose();
                }
            }
        }

        public void GoodsDeclarationSchedule2(string entryNo, string com)
        {
            List<Cusdec_Goods_decl> maindata = bsObj.CHNLSVC.CustService.GetGoodsDeclarationDetailsList(entryNo);

            List<Cusdec_Goods_decl> result = maindata
    .GroupBy(l => new { l.cui_hs_cd, l.cuic_ele_cd })
    .Select(cl => new Cusdec_Goods_decl
    {
        cui_line = cl.First().cui_line,
        cui_itm_desc = cl.First().cui_itm_desc,
        cui_gross_mass = cl.Sum(c => c.cui_gross_mass),
        cui_net_mass = cl.Sum(c => c.cui_net_mass),
        cui_bl_no = cl.First().cui_bl_no,
        cui_orgin_cnty = cl.First().cui_orgin_cnty,
        cui_pkgs = cl.First().cui_pkgs,
        cui_hs_cd = cl.First().cui_hs_cd,
        cui_qty = cl.Sum(c => c.cui_qty),
        cui_bal_qty1 = cl.Sum(c => c.cui_bal_qty1),
        cui_bal_qty2 = cl.Sum(c => c.cui_bal_qty2),
        cui_bal_qty3 = cl.Sum(c => c.cui_bal_qty3),
        cui_req_qty = cl.Sum(c => c.cui_req_qty),
        cui_itm_price = cl.Sum(c => c.cui_itm_price),
        cuic_ele_cd = cl.First().cuic_ele_cd,
        cuic_ele_base = cl.First().cuic_ele_base,
        cuic_ele_rt = cl.First().cuic_ele_rt,
        cuic_ele_amt = cl.Sum(c => c.cuic_ele_amt),
        cuic_ele_mp = cl.First().cuic_ele_mp,
        HSDescription = cl.First().HSDescription


    }).ToList();

            Int32 linenumber = 0;
            //tax values
            DataTable taxvalues = new DataTable();
            DataRow trow;

            taxvalues.Columns.Add("Type", typeof(string));
            taxvalues.Columns.Add("Tax", typeof(decimal));
            taxvalues.Columns.Add("Rate", typeof(decimal));
            taxvalues.Columns.Add("Ammount", typeof(decimal));
            taxvalues.Columns.Add("MP", typeof(decimal));
            taxvalues.Columns.Add("Line", typeof(int));
            taxvalues.Columns.Add("Line2", typeof(int));
            taxvalues.Columns.Add("Line3", typeof(int));
            taxvalues.Columns.Add("cui_itm_desc", typeof(string));
            taxvalues.Columns.Add("cui_gross_mass", typeof(string));
            taxvalues.Columns.Add("cui_net_mass", typeof(string));
            taxvalues.Columns.Add("cui_bl_no", typeof(string));
            taxvalues.Columns.Add("cui_orgin_cnty", typeof(string));
            taxvalues.Columns.Add("cui_pkgs", typeof(string));

            foreach (var dtRow in result)
            {
                linenumber = Convert.ToInt32(dtRow.cui_line);
                trow = taxvalues.NewRow();
                trow["Type"] = dtRow.cuic_ele_cd.ToString();
                trow["Tax"] = dtRow.cuic_ele_base.ToString();
                trow["Rate"] = dtRow.cuic_ele_rt.ToString();
                trow["Ammount"] = dtRow.cuic_ele_amt.ToString();
                trow["MP"] = dtRow.cuic_ele_mp.ToString();
                trow["Line"] = linenumber;
                trow["Line2"] = linenumber;
                trow["Line3"] = linenumber;
                trow["cui_itm_desc"] = "";
                trow["cui_gross_mass"] = "";
                trow["cui_net_mass"] = "";
                trow["cui_bl_no"] = "";
                trow["cui_orgin_cnty"] = "";
                trow["cui_pkgs"] = "";
                taxvalues.Rows.Add(trow);
            }
            DataTable firsgood = new DataTable();
            DataRow firstrow;

            firsgood.Columns.Add("cui_itm_desc", typeof(string));
            firsgood.Columns.Add("cui_gross_mass", typeof(string));
            firsgood.Columns.Add("cui_net_mass", typeof(string));
            firsgood.Columns.Add("cui_bl_no", typeof(string));
            firsgood.Columns.Add("cui_orgin_cnty", typeof(string));
            firsgood.Columns.Add("cui_pkgs", typeof(string));
            firsgood.Columns.Add("cui_hs_cd", typeof(string));
            firsgood.Columns.Add("cui_qty", typeof(string));
            firsgood.Columns.Add("cui_bal_qty2", typeof(string));
            firsgood.Columns.Add("cui_bal_qty3", typeof(string));
            firsgood.Columns.Add("cui_req_qty", typeof(string));
            firsgood.Columns.Add("cui_itm_price", typeof(decimal));
            firsgood.Columns.Add("cui_line", typeof(Int32));


            var result2 = result.GroupBy(x => x.cui_hs_cd).ToList();


            if (result2 != null)
            {

                foreach (var dtRow2 in result2)
                {
                    firstrow = firsgood.NewRow();

                    firstrow["cui_itm_desc"] = dtRow2.First().HSDescription.ToString();
                    firstrow["cui_gross_mass"] = dtRow2.First().cui_gross_mass.ToString();
                    firstrow["cui_net_mass"] = dtRow2.First().cui_net_mass.ToString();
                    firstrow["cui_bl_no"] = dtRow2.First().cui_bl_no.ToString();
                    firstrow["cui_orgin_cnty"] = dtRow2.First().cui_orgin_cnty.ToString();
                    firstrow["cui_pkgs"] = dtRow2.First().cui_pkgs.ToString();
                    firstrow["cui_hs_cd"] = dtRow2.First().cui_hs_cd.ToString();
                    firstrow["cui_qty"] = "(" + dtRow2.First().cui_qty.ToString() + " U )";
                    firstrow["cui_bal_qty2"] = dtRow2.First().cui_qty.ToString();
                    firstrow["cui_bal_qty3"] = dtRow2.First().cui_bal_qty3.ToString();
                    firstrow["cui_req_qty"] = dtRow2.First().cui_req_qty.ToString();
                    firstrow["cui_itm_price"] = Convert.ToDecimal(dtRow2.First().cui_itm_price.ToString());
                    firstrow["cui_line"] = Convert.ToInt32(dtRow2.First().cui_line.ToString());
                    firsgood.Rows.Add(firstrow);

                }
            }

            DataTable eledata = new DataTable();
            int i = 0;
            string cat = "";
            decimal fob = 0;
            decimal foblkr = 0;
            decimal faight = 0;
            decimal faightlkr = 0;
            decimal insu = 0;
            decimal insulkr = 0;
            decimal other = 0;
            decimal otherlkr = 0;
            decimal total = 0;
            decimal totallkr = 0;

            eledata = bsObj.CHNLSVC.CustService.GetCusdecElementnew(entryNo);
            foreach (DataRow ele in eledata.Rows)
            {
                cat = eledata.Rows[i]["cus_ele_cd"].ToString();

                if (cat == "OTH")
                {
                    other = Convert.ToDecimal(eledata.Rows[i]["cus_amt"]);
                    otherlkr = Convert.ToDecimal(eledata.Rows[i]["cus_amt_com"]);
                }
                if (cat == "INSU")
                {
                    insu = Convert.ToDecimal(eledata.Rows[i]["cus_amt"]);
                    insulkr = Convert.ToDecimal(eledata.Rows[i]["cus_amt_com"]);
                }
                if (cat == "FRGT")
                {
                    faight = Convert.ToDecimal(eledata.Rows[i]["cus_amt"]);
                    faightlkr = Convert.ToDecimal(eledata.Rows[i]["cus_amt_com"]);

                }
                if (cat == "COST")
                {
                    fob = Convert.ToDecimal(eledata.Rows[i]["cus_amt"]);
                    foblkr = Convert.ToDecimal(eledata.Rows[i]["cus_amt_com"]);
                }

                i++;

            }
            total = other + insu + faight + fob;
            totallkr = otherlkr + insulkr + faightlkr + foblkr;

            DataTable finalele = new DataTable();
            DataRow elerow;
            finalele.Columns.Add("FOB", typeof(decimal));
            finalele.Columns.Add("FREIGHT", typeof(decimal));
            finalele.Columns.Add("INSU", typeof(decimal));
            finalele.Columns.Add("OTHER", typeof(decimal));
            finalele.Columns.Add("TOTAL", typeof(decimal));
            finalele.Columns.Add("TOTALLKR", typeof(decimal));

            elerow = finalele.NewRow();
            elerow["FOB"] = fob;
            elerow["FREIGHT"] = faight;
            elerow["INSU"] = insu;
            elerow["OTHER"] = other;
            elerow["TOTAL"] = total;
            elerow["TOTALLKR"] = totallkr;

            finalele.Rows.Add(elerow);

            DataTable cusdeccommon = bsObj.CHNLSVC.CustService.GetCusdecCommonNew("LK");
            //header data
            DataTable Hdrdata = bsObj.CHNLSVC.CustService.GetCusdecHDRData(entryNo, com);
            _goods_declarationII.Database.Tables["HdrData"].SetDataSource(Hdrdata);
            _goods_declarationII.Database.Tables["firstGooddata"].SetDataSource(firsgood);
            _goods_declarationII.Database.Tables["EleData"].SetDataSource(finalele);
            _goods_declarationII.Database.Tables["CusdecCommon"].SetDataSource(cusdeccommon);

            foreach (object repOp in _goods_declarationII.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "firstpagetax")
                    {
                        ReportDocument subRepDoc = _goods_declarationII.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["TaxValues"].SetDataSource(taxvalues);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    _cs.Dispose();
                }
            }
        }



        // Asycuda 

        public void GoodsDecarrationAsycuda(string entryNo, string com)
        {
            DataTable Hdrdata = new DataTable();
            DataTable maindata = new DataTable();
            DataTable Param = new DataTable();
            DataTable Param2 = new DataTable();


            DataRow dr;
            int i = 0;
            int j = 0;

            int linenumber = 0;
            int nxtlinenumber = 0;
            int maxlinenumber = 0;

            Param.Columns.Add("Company", typeof(string));
            Param.Columns.Add("User", typeof(string));
            Param.Columns.Add("Location", typeof(string));
            Param.Columns.Add("cui_itm_desc", typeof(string));
            Param.Columns.Add("cui_gross_mass", typeof(string));
            Param.Columns.Add("cui_net_mass", typeof(string));
            Param.Columns.Add("cui_bl_no", typeof(string));
            Param.Columns.Add("cui_orgin_cnty", typeof(string));
            Param.Columns.Add("cui_pkgs", typeof(string));
            Param.Columns.Add("Line", typeof(int));
            Param.Columns.Add("MaxLine", typeof(int));
            Param.Columns.Add("cui_hs_cd", typeof(string));
            Param.Columns.Add("cui_itm_price", typeof(decimal));
            Param.Columns.Add("cui_qty", typeof(string));
            Param.Columns.Add("cui_bal_qty1", typeof(string));
            Param.Columns.Add("cui_bal_qty2", typeof(string));
            Param.Columns.Add("cui_bal_qty3", typeof(string));
            Param.Columns.Add("cui_req_qty", typeof(string));
            Param.Columns.Add("MainHS", typeof(string));

            Param2.Columns.Add("Company", typeof(string));
            Param2.Columns.Add("User", typeof(string));
            Param2.Columns.Add("Location", typeof(string));
            Param2.Columns.Add("cui_itm_desc", typeof(string));
            Param2.Columns.Add("cui_gross_mass", typeof(string));
            Param2.Columns.Add("cui_net_mass", typeof(string));
            Param2.Columns.Add("cui_bl_no", typeof(string));
            Param2.Columns.Add("cui_orgin_cnty", typeof(string));
            Param2.Columns.Add("cui_pkgs", typeof(string));
            Param2.Columns.Add("Line", typeof(int));
            Param2.Columns.Add("MaxLine", typeof(int));
            Param2.Columns.Add("cui_hs_cd", typeof(string));
            Param2.Columns.Add("cui_itm_price", typeof(decimal));
            Param2.Columns.Add("cui_qty", typeof(string));
            Param2.Columns.Add("cui_bal_qty1", typeof(string));
            Param2.Columns.Add("cui_bal_qty2", typeof(string));
            Param2.Columns.Add("cui_bal_qty3", typeof(string));
            Param2.Columns.Add("cui_req_qty", typeof(string));
            Param2.Columns.Add("MainHS", typeof(string));

            //tax values
            DataTable taxvalues = new DataTable();
            DataRow trow;

            taxvalues.Columns.Add("Type", typeof(string));
            taxvalues.Columns.Add("Tax", typeof(decimal));
            taxvalues.Columns.Add("Rate", typeof(decimal));
            taxvalues.Columns.Add("Ammount", typeof(decimal));
            taxvalues.Columns.Add("MP", typeof(decimal));
            taxvalues.Columns.Add("Line", typeof(int));
            taxvalues.Columns.Add("Line2", typeof(int));
            taxvalues.Columns.Add("Line3", typeof(int));
            taxvalues.Columns.Add("cui_itm_desc", typeof(string));
            taxvalues.Columns.Add("cui_gross_mass", typeof(string));
            taxvalues.Columns.Add("cui_net_mass", typeof(string));
            taxvalues.Columns.Add("cui_bl_no", typeof(string));
            taxvalues.Columns.Add("cui_orgin_cnty", typeof(string));
            taxvalues.Columns.Add("cui_pkgs", typeof(string));

            DataTable firsgood = new DataTable();
            DataRow firstrow;

            firsgood.Columns.Add("cui_itm_desc", typeof(string));
            firsgood.Columns.Add("cui_gross_mass", typeof(string));
            firsgood.Columns.Add("cui_net_mass", typeof(string));
            firsgood.Columns.Add("cui_bl_no", typeof(string));
            firsgood.Columns.Add("cui_orgin_cnty", typeof(string));
            firsgood.Columns.Add("cui_pkgs", typeof(string));
            firsgood.Columns.Add("cui_hs_cd", typeof(string));
            firsgood.Columns.Add("cui_qty", typeof(string));
            firsgood.Columns.Add("cui_bal_qty2", typeof(string));
            firsgood.Columns.Add("cui_bal_qty3", typeof(string));
            firsgood.Columns.Add("cui_req_qty", typeof(string));
            firsgood.Columns.Add("cui_itm_price", typeof(decimal));
            firsgood.Columns.Add("cui_line", typeof(int));

            ////list
            List<Cusdec_Goods_decl> maindatanew = bsObj.CHNLSVC.CustService.GetGoodsDeclarationDetailsList(entryNo);

            List<Cusdec_Goods_decl> resultnew = maindatanew
         .GroupBy(l => new { l.cui_hs_cd, l.cuic_ele_cd })
         .Select(cl => new Cusdec_Goods_decl
         {
             cui_line = cl.First().cui_line,
             cui_itm_desc = cl.First().cui_itm_desc,
             cui_gross_mass = cl.Sum(c => c.cui_gross_mass),
             cui_net_mass = cl.Sum(c => c.cui_net_mass),
             cui_bl_no = cl.First().cui_bl_no,
             cui_orgin_cnty = cl.First().cui_orgin_cnty,
             cui_pkgs = cl.First().cui_pkgs,
             cui_hs_cd = cl.First().cui_hs_cd,
             cui_qty = cl.Sum(c => c.cui_qty),
             cui_bal_qty1 = cl.Sum(c => c.cui_bal_qty1),
             cui_bal_qty2 = cl.Sum(c => c.cui_bal_qty2),
             cui_bal_qty3 = cl.Sum(c => c.cui_bal_qty3),
             cui_req_qty = cl.Sum(c => c.cui_req_qty),
             cui_itm_price = cl.Sum(c => c.cui_itm_price),
             cuic_ele_cd = cl.First().cuic_ele_cd,
             cuic_ele_base = cl.Sum(c => c.cuic_ele_base),
             cuic_ele_rt = cl.First().cuic_ele_rt,
             cuic_ele_amt = cl.Sum(c => c.cuic_ele_amt),
             cuic_ele_mp = cl.First().cuic_ele_mp,
             HSDescription = cl.First().HSDescription,
             MainHS = cl.First().MainHS
         }).ToList();

            resultnew = resultnew.OrderBy(a => a.cui_hs_cd).ToList();

            int p = 0;
            string hs1 = "";
            foreach (var odlist in resultnew)
            {
                if (hs1 != odlist.cui_hs_cd)
                {
                    p++;
                    odlist.cui_line = p;
                }
                else
                {
                    odlist.cui_line = p;
                }
                hs1 = odlist.cui_hs_cd;
            }


            foreach (var ittm in resultnew)
            {
                if (entryNo.Contains("LAB"))
                {
                    string HS = ittm.cui_hs_cd;
                    List<HsCode> _hsCodes = new List<HsCode>();
                    _hsCodes = bsObj.CHNLSVC.Financial.GetHSCodeList(HS);
                    var count = _hsCodes.Where(a => a.Mhc_cost_ele == ittm.cuic_ele_cd && a.Mhc_weight_price > 0).Count();
                    if (count > 0)
                    {
                        ittm.cuic_ele_base = _hsCodes.Where(a => a.Mhc_cost_ele == ittm.cuic_ele_cd && a.Mhc_weight_price > 0).First().Mhc_weight_price;
                    }
                }
            }


            List<Cusdec_Goods_decl> resultnewW = maindatanew
 .GroupBy(l => new { l.cui_hs_cd })
 .Select(cl => new Cusdec_Goods_decl
 {
     cui_line = cl.First().cui_line,
     cui_itm_desc = cl.First().cui_itm_desc,
     cui_gross_mass = cl.Sum(c => c.cui_gross_mass),
     cui_net_mass = cl.Sum(c => c.cui_net_mass),
     cui_bl_no = cl.First().cui_bl_no,
     cui_orgin_cnty = cl.First().cui_orgin_cnty,
     cui_pkgs = cl.First().cui_pkgs,
     cui_hs_cd = cl.First().cui_hs_cd,
     cui_qty = cl.Sum(c => c.cui_qty),
     cui_bal_qty1 = cl.Sum(c => c.cui_bal_qty1),
     cui_bal_qty2 = cl.Sum(c => c.cui_bal_qty2),
     cui_bal_qty3 = cl.Sum(c => c.cui_bal_qty3),
     cui_req_qty = cl.Sum(c => c.cui_req_qty),
     cui_itm_price = cl.Sum(c => c.cui_itm_price),
     cuic_ele_cd = cl.First().cuic_ele_cd,
     cuic_ele_base = cl.Sum(c => c.cuic_ele_base),
     cuic_ele_rt = cl.First().cuic_ele_rt,
     cuic_ele_amt = cl.Sum(c => c.cuic_ele_amt),
     cuic_ele_mp = cl.First().cuic_ele_mp,
     HSDescription = cl.First().HSDescription,
     MainHS = cl.First().MainHS

 }).ToList();



            var countnew = resultnewW.Count;

            IEnumerable<Cusdec_Goods_decl> resultnew2 = resultnew;
            DataTable table = new DataTable();
            using (var reader = ObjectReader.Create(resultnew2))
            {
                table.Load(reader);
            }

            // all goods data
            //  maindata = bsObj.CHNLSVC.CustService.GetGoodsDeclarationDetails(entryNo);
            //int hscount = 0;
            //foreach (var dtRow in resultnew)
            //{

            //}


            if (resultnew != null)
            {

                foreach (var dtRow in resultnew)
                {
                    linenumber = Convert.ToInt32(dtRow.cui_line);
                    trow = taxvalues.NewRow();
                    trow["Type"] = dtRow.cuic_ele_cd.ToString();
                    trow["Tax"] = dtRow.cuic_ele_base.ToString();
                    trow["Rate"] = dtRow.cuic_ele_rt.ToString();
                    trow["Ammount"] = dtRow.cuic_ele_amt.ToString();
                    trow["MP"] = dtRow.cuic_ele_mp.ToString();
                    trow["Line"] = linenumber;
                    trow["Line2"] = linenumber;
                    trow["Line3"] = linenumber;
                    trow["cui_itm_desc"] = dtRow.HSDescription.ToString();
                    trow["cui_gross_mass"] = dtRow.cui_gross_mass.ToString();
                    trow["cui_net_mass"] = dtRow.cui_net_mass.ToString();
                    trow["cui_bl_no"] = dtRow.cui_bl_no.ToString();
                    trow["cui_orgin_cnty"] = dtRow.cui_orgin_cnty.ToString();
                    trow["cui_pkgs"] = dtRow.cui_pkgs.ToString();

                    taxvalues.Rows.Add(trow);
                    maxlinenumber = linenumber;
                    i++;
                }
            }


            if (table != null)
            {
                foreach (DataRow dtRow in table.Rows)
                {
                    if (j < table.Rows.Count - 1)
                    {
                        linenumber = Convert.ToInt32(table.Rows[j]["cui_line"]);
                        nxtlinenumber = Convert.ToInt32(table.Rows[j + 1]["cui_line"]);

                        if (linenumber != nxtlinenumber)
                        {
                            dr = Param.NewRow();
                            dr["Company"] = "ABL";
                            dr["User"] = "ADMIN";
                            dr["Location"] = "DMP";
                            dr["cui_itm_desc"] = table.Rows[j]["HSDescription"].ToString();
                            dr["cui_gross_mass"] = table.Rows[j]["cui_gross_mass"].ToString();
                            dr["cui_net_mass"] = table.Rows[j]["cui_net_mass"].ToString();
                            dr["cui_bl_no"] = table.Rows[j]["cui_bl_no"].ToString();
                            dr["cui_orgin_cnty"] = table.Rows[j]["cui_orgin_cnty"].ToString();
                            dr["cui_pkgs"] = table.Rows[j]["cui_pkgs"].ToString();
                            dr["Line"] = linenumber;
                            dr["MaxLine"] = maxlinenumber;
                            dr["cui_hs_cd"] = table.Rows[j]["cui_hs_cd"].ToString();
                            dr["cui_qty"] = "(" + table.Rows[j]["cui_qty"].ToString() + " U )";// "(" + dtRow2.First().cui_qty.ToString() + " U )";
                            dr["cui_bal_qty1"] = table.Rows[j]["cui_qty"].ToString();
                            dr["cui_bal_qty2"] = table.Rows[j]["cui_bal_qty2"].ToString();
                            dr["cui_bal_qty3"] = table.Rows[j]["cui_bal_qty3"].ToString();
                            dr["cui_req_qty"] = table.Rows[j]["cui_req_qty"].ToString();
                            dr["cui_itm_price"] = Convert.ToDecimal(table.Rows[j]["cui_itm_price"].ToString());
                            dr["MainHS"] = table.Rows[j]["MainHS"].ToString();
                            Param.Rows.Add(dr);
                        }
                        if (j == table.Rows.Count - 2)
                        {
                            dr = Param.NewRow();
                            dr["Company"] = "ABL";
                            dr["User"] = "ADMIN";
                            dr["Location"] = "DMP";
                            dr["cui_itm_desc"] = table.Rows[j]["HSDescription"].ToString();
                            dr["cui_gross_mass"] = table.Rows[j]["cui_gross_mass"].ToString();
                            dr["cui_net_mass"] = table.Rows[j]["cui_net_mass"].ToString();
                            dr["cui_bl_no"] = table.Rows[j]["cui_bl_no"].ToString();
                            dr["cui_orgin_cnty"] = table.Rows[j]["cui_orgin_cnty"].ToString();
                            dr["cui_pkgs"] = table.Rows[j]["cui_pkgs"].ToString();
                            dr["Line"] = linenumber;
                            dr["MaxLine"] = maxlinenumber;
                            dr["cui_hs_cd"] = table.Rows[j]["cui_hs_cd"].ToString();
                            dr["cui_qty"] = "(" + table.Rows[j]["cui_qty"].ToString() + " U )";// "(" + dtRow2.First().cui_qty.ToString() + " U )";
                            dr["cui_bal_qty1"] = table.Rows[j]["cui_qty"].ToString();
                            dr["cui_bal_qty2"] = table.Rows[j]["cui_bal_qty2"].ToString();
                            dr["cui_bal_qty3"] = table.Rows[j]["cui_bal_qty3"].ToString();
                            dr["cui_req_qty"] = table.Rows[j]["cui_req_qty"].ToString();
                            dr["cui_itm_price"] = Convert.ToDecimal(table.Rows[j]["cui_itm_price"].ToString());
                            dr["MainHS"] = table.Rows[j]["MainHS"].ToString();
                            Param.Rows.Add(dr);
                        }
                    }
                    j++;
                }
            }
            j = 0;
            foreach (DataRow dtRow in Param.Rows)
            {

                if (Param.Rows[j]["cui_hs_cd"].ToString() != Param.Rows[j]["MainHS"].ToString())
                {
                    dr = Param2.NewRow();
                    dr["Company"] = "ABL";
                    dr["User"] = "ADMIN";
                    dr["Location"] = "DMP";
                    dr["cui_itm_desc"] = Param.Rows[j]["cui_itm_desc"].ToString();
                    dr["cui_gross_mass"] = Param.Rows[j]["cui_gross_mass"].ToString();
                    dr["cui_net_mass"] = Param.Rows[j]["cui_net_mass"].ToString();
                    dr["cui_bl_no"] = Param.Rows[j]["cui_bl_no"].ToString();
                    dr["cui_orgin_cnty"] = Param.Rows[j]["cui_orgin_cnty"].ToString();
                    dr["cui_pkgs"] = Param.Rows[j]["cui_pkgs"].ToString();
                    dr["Line"] = Param.Rows[j]["Line"].ToString();
                    dr["MaxLine"] = Param.Rows[j]["MaxLine"].ToString();
                    dr["cui_hs_cd"] = Param.Rows[j]["cui_hs_cd"].ToString();
                    dr["cui_qty"] = Param.Rows[j]["cui_qty"].ToString();
                    dr["cui_bal_qty1"] = Param.Rows[j]["cui_bal_qty1"].ToString();
                    dr["cui_bal_qty2"] = Param.Rows[j]["cui_bal_qty2"].ToString();
                    dr["cui_bal_qty3"] = Param.Rows[j]["cui_bal_qty3"].ToString();
                    dr["cui_req_qty"] = Param.Rows[j]["cui_req_qty"].ToString();
                    dr["cui_itm_price"] = Convert.ToDecimal(Param.Rows[j]["cui_itm_price"].ToString());
                    dr["MainHS"] = Param.Rows[j]["MainHS"].ToString();
                    Param2.Rows.Add(dr);
                }
                j++;
            }
            j = 0;
            foreach (DataRow dtRow in Param.Rows)
            {

                if (Param.Rows[j]["cui_hs_cd"].ToString() != Param.Rows[j]["MainHS"].ToString())
                {
                    dr = Param2.NewRow();
                    dr["Company"] = "ABL";
                    dr["User"] = "ADMIN";
                    dr["Location"] = "DMP";
                    dr["cui_itm_desc"] = Param.Rows[j]["cui_itm_desc"].ToString();
                    dr["cui_gross_mass"] = Param.Rows[j]["cui_gross_mass"].ToString();
                    dr["cui_net_mass"] = Param.Rows[j]["cui_net_mass"].ToString();
                    dr["cui_bl_no"] = Param.Rows[j]["cui_bl_no"].ToString();
                    dr["cui_orgin_cnty"] = Param.Rows[j]["cui_orgin_cnty"].ToString();
                    dr["cui_pkgs"] = Param.Rows[j]["cui_pkgs"].ToString();
                    dr["Line"] = Param.Rows[j]["Line"].ToString();
                    dr["MaxLine"] = Param.Rows[j]["MaxLine"].ToString();
                    dr["cui_hs_cd"] = Param.Rows[j]["cui_hs_cd"].ToString();
                    dr["cui_qty"] = Param.Rows[j]["cui_qty"].ToString();
                    dr["cui_bal_qty1"] = Param.Rows[j]["cui_bal_qty1"].ToString();
                    dr["cui_bal_qty2"] = Param.Rows[j]["cui_bal_qty2"].ToString();
                    dr["cui_bal_qty3"] = Param.Rows[j]["cui_bal_qty3"].ToString();
                    dr["cui_req_qty"] = Param.Rows[j]["cui_req_qty"].ToString();
                    dr["cui_itm_price"] = Convert.ToDecimal(Param.Rows[j]["cui_itm_price"].ToString());
                    dr["MainHS"] = Param.Rows[j]["MainHS"].ToString();
                    Param2.Rows.Add(dr);
                }
                j++;
            }
            int mainhsline = 0;
            if (table != null)
            {
                j = 0;
                foreach (DataRow dtRow in table.Rows)
                {

                    if (table.Rows[j]["cui_hs_cd"].ToString() == table.Rows[j]["MainHS"].ToString())
                    {
                        if (firsgood.Rows.Count < 1)
                        {


                            firstrow = firsgood.NewRow();
                            firstrow["cui_itm_desc"] = table.Rows[j]["HSDescription"].ToString();
                            firstrow["cui_gross_mass"] = table.Rows[j]["cui_gross_mass"].ToString();
                            firstrow["cui_net_mass"] = table.Rows[j]["cui_net_mass"].ToString();
                            firstrow["cui_bl_no"] = table.Rows[j]["cui_bl_no"].ToString();
                            firstrow["cui_orgin_cnty"] = table.Rows[j]["cui_orgin_cnty"].ToString();
                            firstrow["cui_pkgs"] = table.Rows[j]["cui_pkgs"].ToString();
                            firstrow["cui_hs_cd"] = table.Rows[j]["cui_hs_cd"].ToString();
                            firstrow["cui_qty"] = "(" + table.Rows[j]["cui_qty"].ToString() + " U )";
                            firstrow["cui_bal_qty2"] = table.Rows[j]["cui_qty"].ToString();
                            firstrow["cui_bal_qty3"] = table.Rows[j]["cui_bal_qty3"].ToString();
                            firstrow["cui_req_qty"] = table.Rows[j]["cui_req_qty"].ToString();
                            firstrow["cui_itm_price"] = Convert.ToDecimal(table.Rows[j]["cui_itm_price"].ToString());
                            firstrow["cui_line"] = Convert.ToInt64(table.Rows[j]["cui_line"].ToString());
                            mainhsline = Convert.ToInt32(table.Rows[j]["cui_line"].ToString());
                            firsgood.Rows.Add(firstrow);
                        }
                    }

                    j++;
                }

                if (firsgood.Rows.Count == 0)
                {
                    j = 0;
                    foreach (DataRow dtRow in table.Rows)
                    {

                        if (firsgood.Rows.Count < 1)
                        {
                            firstrow = firsgood.NewRow();

                            firstrow["cui_itm_desc"] = table.Rows[0]["HSDescription"].ToString();
                            firstrow["cui_gross_mass"] = table.Rows[0]["cui_gross_mass"].ToString();
                            firstrow["cui_net_mass"] = table.Rows[0]["cui_net_mass"].ToString();
                            firstrow["cui_bl_no"] = table.Rows[0]["cui_bl_no"].ToString();
                            firstrow["cui_orgin_cnty"] = table.Rows[0]["cui_orgin_cnty"].ToString();
                            firstrow["cui_pkgs"] = table.Rows[0]["cui_pkgs"].ToString();
                            firstrow["cui_hs_cd"] = table.Rows[0]["cui_hs_cd"].ToString();
                            firstrow["cui_qty"] = "(" + table.Rows[0]["cui_qty"].ToString() + " U )";
                            firstrow["cui_bal_qty2"] = table.Rows[0]["cui_qty"].ToString();
                            firstrow["cui_bal_qty3"] = table.Rows[0]["cui_bal_qty3"].ToString();
                            firstrow["cui_req_qty"] = table.Rows[0]["cui_req_qty"].ToString();
                            firstrow["cui_itm_price"] = Convert.ToDecimal(table.Rows[0]["cui_itm_price"].ToString());
                            firstrow["cui_line"] = Convert.ToInt64(table.Rows[0]["cui_line"].ToString());
                            mainhsline = Convert.ToInt32(table.Rows[0]["cui_line"].ToString());
                            firsgood.Rows.Add(firstrow);
                        }


                    }
                }

            }
            // create second page table
            DataTable secondpagedata = new DataTable();
            DataRow srow;

            secondpagedata.Columns.Add("cui_itm_desc1", typeof(string));
            secondpagedata.Columns.Add("cui_gross_mass1", typeof(string));
            secondpagedata.Columns.Add("cui_net_mass1", typeof(string));
            secondpagedata.Columns.Add("cui_bl_no1", typeof(string));
            secondpagedata.Columns.Add("cui_orgin_cnty1", typeof(string));
            secondpagedata.Columns.Add("cui_itm_desc2", typeof(string));
            secondpagedata.Columns.Add("cui_gross_mass2", typeof(string));
            secondpagedata.Columns.Add("cui_net_mass2", typeof(string));
            secondpagedata.Columns.Add("cui_bl_no2", typeof(string));
            secondpagedata.Columns.Add("cui_orgin_cnty2", typeof(string));
            secondpagedata.Columns.Add("cui_itm_desc3", typeof(string));
            secondpagedata.Columns.Add("cui_gross_mass3", typeof(string));
            secondpagedata.Columns.Add("cui_net_mass3", typeof(string));
            secondpagedata.Columns.Add("cui_bl_no3", typeof(string));
            secondpagedata.Columns.Add("cui_orgin_cnty3", typeof(string));
            secondpagedata.Columns.Add("Line1", typeof(Int16));
            secondpagedata.Columns.Add("Line2", typeof(Int16));
            secondpagedata.Columns.Add("Line3", typeof(Int16));
            secondpagedata.Columns.Add("LineCount1", typeof(Int16));
            secondpagedata.Columns.Add("LineCount2", typeof(Int16));
            secondpagedata.Columns.Add("LineCount3", typeof(Int16));
            secondpagedata.Columns.Add("cui_hs_cd1", typeof(string));
            secondpagedata.Columns.Add("cui_hs_cd2", typeof(string));
            secondpagedata.Columns.Add("cui_hs_cd3", typeof(string));
            secondpagedata.Columns.Add("cui_itm_price1", typeof(decimal));
            secondpagedata.Columns.Add("cui_itm_price2", typeof(decimal));
            secondpagedata.Columns.Add("cui_itm_price3", typeof(decimal));
            secondpagedata.Columns.Add("cui_bal_qty11", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty12", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty13", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty21", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty22", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty23", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty31", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty32", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty33", typeof(string));
            secondpagedata.Columns.Add("Maxlinecount", typeof(Int16));

            //for ( i = Param.Rows.Count - 1; i >= 0; i--)
            //{
            //    DataRow drt = Param.Rows[i];
            //    if (drt["cui_hs_cd"] == drt["MainHS"])
            //        drt.Delete();
            //}
            int PCOUNT = Param.AsEnumerable()
                        .Where(r1 => r1.Field<string>("cui_hs_cd") != r1.Field<string>("MainHS")).Count();

            if (PCOUNT > 0)
            {
                Param = Param.AsEnumerable()
                       .Where(r1 => r1.Field<string>("cui_hs_cd") != r1.Field<string>("MainHS"))
                       .CopyToDataTable();
            }

            if (Param.Rows.Count > 0)
            {
                string firstgoodhs = firsgood.Rows[0]["cui_hs_cd"].ToString();
                int ncount = Param.AsEnumerable()
                         .Where(r1 => r1.Field<string>("cui_hs_cd") != firstgoodhs)
                         .Count();

                if (ncount > 0)
                {
                    Param = Param.AsEnumerable()
                        .Where(r1 => r1.Field<string>("cui_hs_cd") != firstgoodhs)
                        .CopyToDataTable();
                }
                else
                {
                    Param = null;
                }

            }
            else
            {
                Param = null;
            }


            //DataView dv = Param.DefaultView;
            //dv.Sort = "Line";
            //Param = dv.ToTable();
            i = 0;
            int maxlinecount = 0;
            if (Param != null)
            {
                for (int k = 0; k <= Param.Rows.Count; k = k + 3)
                {
                    srow = secondpagedata.NewRow();
                    if (i < Param.Rows.Count)
                    {

                        srow["cui_itm_desc1"] = Param.Rows[i]["cui_itm_desc"].ToString();
                        srow["cui_gross_mass1"] = Param.Rows[i]["cui_gross_mass"].ToString();
                        srow["cui_net_mass1"] = Param.Rows[i]["cui_net_mass"].ToString();
                        srow["cui_bl_no1"] = Param.Rows[i]["cui_bl_no"].ToString();
                        srow["cui_orgin_cnty1"] = Param.Rows[i]["cui_orgin_cnty"].ToString();
                        srow["Line1"] = Convert.ToInt16(Param.Rows[i]["Line"]);
                        srow["LineCount1"] = Convert.ToInt16(i + 2);
                        srow["cui_hs_cd1"] = Param.Rows[i]["cui_hs_cd"].ToString();
                        srow["cui_itm_price1"] = Convert.ToDecimal(Param.Rows[i]["cui_itm_price"].ToString());
                        srow["cui_bal_qty11"] = "( " + Param.Rows[i]["cui_bal_qty1"].ToString() + " U )";
                        srow["cui_bal_qty21"] = Param.Rows[i]["cui_bal_qty2"].ToString();
                        srow["cui_bal_qty31"] = Param.Rows[i]["cui_bal_qty3"].ToString();

                        if (maxlinecount < Convert.ToInt16(Param.Rows[i]["Line"]))
                        {
                            maxlinecount = Convert.ToInt16(Param.Rows[i]["Line"]);
                        }

                        srow["Maxlinecount"] = mainhsline;

                    }

                    if (i + 1 < Param.Rows.Count)
                    {


                        srow["cui_itm_desc2"] = Param.Rows[i + 1]["cui_itm_desc"].ToString();
                        srow["cui_gross_mass2"] = Param.Rows[i + 1]["cui_gross_mass"].ToString();
                        srow["cui_net_mass2"] = Param.Rows[i + 1]["cui_net_mass"].ToString();
                        srow["cui_bl_no2"] = Param.Rows[i + 1]["cui_bl_no"].ToString();
                        srow["cui_orgin_cnty2"] = Param.Rows[i + 1]["cui_orgin_cnty"].ToString();
                        srow["Line2"] = Convert.ToInt16(Param.Rows[i + 1]["Line"]);
                        srow["LineCount2"] = Convert.ToInt16(i + 3);
                        srow["cui_hs_cd2"] = Param.Rows[i + 1]["cui_hs_cd"].ToString();
                        srow["cui_itm_price2"] = Convert.ToDecimal(Param.Rows[i + 1]["cui_itm_price"].ToString());
                        srow["cui_bal_qty12"] = "( " + Param.Rows[i + 1]["cui_bal_qty1"].ToString() + " U )";
                        srow["cui_bal_qty22"] = Param.Rows[i + 1]["cui_bal_qty2"].ToString();
                        srow["cui_bal_qty32"] = Param.Rows[i + 1]["cui_bal_qty3"].ToString();
                        if (maxlinecount < Convert.ToInt16(Param.Rows[i + 1]["Line"]))
                        {
                            maxlinecount = Convert.ToInt16(Param.Rows[i + 1]["Line"]);
                        }
                        srow["Maxlinecount"] = mainhsline;

                    }

                    if (i + 2 < Param.Rows.Count)
                    {


                        srow["cui_gross_mass3"] = Param.Rows[i + 2]["cui_gross_mass"].ToString();
                        srow["cui_net_mass3"] = Param.Rows[i + 2]["cui_net_mass"].ToString();
                        srow["cui_bl_no3"] = Param.Rows[i + 2]["cui_bl_no"].ToString();
                        srow["cui_orgin_cnty3"] = Param.Rows[i + 2]["cui_orgin_cnty"].ToString();
                        srow["Line3"] = Convert.ToInt16(Param.Rows[i + 2]["Line"]);
                        srow["LineCount3"] = Convert.ToInt16(i + 4);
                        srow["cui_hs_cd3"] = Param.Rows[i + 2]["cui_hs_cd"].ToString();
                        srow["cui_itm_price3"] = Convert.ToDecimal(Param.Rows[i + 2]["cui_itm_price"].ToString());
                        srow["cui_bal_qty13"] = "( " + Param.Rows[i + 2]["cui_bal_qty1"].ToString() + " U )";
                        srow["cui_bal_qty23"] = Param.Rows[i + 2]["cui_bal_qty2"].ToString();
                        srow["cui_bal_qty33"] = Param.Rows[i + 2]["cui_bal_qty3"].ToString();
                        if (maxlinecount < Convert.ToInt16(Param.Rows[i + 2]["Line"]))
                        {
                            maxlinecount = Convert.ToInt16(Param.Rows[i + 2]["Line"]);
                        }

                        srow["Maxlinecount"] = mainhsline;
                    }

                    secondpagedata.Rows.Add(srow);
                    i = i + 3;
                }
            }

            int ll = 0;
            decimal tottax = 0;
            foreach (DataRow taxtot in taxvalues.Rows)
            {

                tottax = tottax + Convert.ToDecimal(taxvalues.Rows[ll]["Ammount"].ToString());
                ll++;
            }

            DataTable eledata = new DataTable();
            i = 0;
            string cat = "";
            decimal fob = 0;
            decimal foblkr = 0;
            decimal faight = 0;
            decimal faightlkr = 0;
            decimal insu = 0;
            decimal insulkr = 0;
            decimal other = 0;
            decimal otherlkr = 0;
            decimal total = 0;
            decimal totallkr = 0;

            eledata = bsObj.CHNLSVC.CustService.GetCusdecElementnew(entryNo);
            foreach (DataRow ele in eledata.Rows)
            {
                cat = eledata.Rows[i]["cus_ele_cd"].ToString();

                if (cat == "OTH")
                {
                    other = Convert.ToDecimal(eledata.Rows[i]["cus_amt"]);
                    otherlkr = Convert.ToDecimal(eledata.Rows[i]["cus_amt_com"]);
                }
                if (cat == "INSU")
                {
                    insu = Convert.ToDecimal(eledata.Rows[i]["cus_amt"]);
                    insulkr = Convert.ToDecimal(eledata.Rows[i]["cus_amt_com"]);
                }
                if (cat == "FRGT")
                {
                    faight = Convert.ToDecimal(eledata.Rows[i]["cus_amt"]);
                    faightlkr = Convert.ToDecimal(eledata.Rows[i]["cus_amt_com"]);

                }
                if (cat == "COST")
                {
                    fob = Convert.ToDecimal(eledata.Rows[i]["cus_amt"]);
                    foblkr = Convert.ToDecimal(eledata.Rows[i]["cus_amt_com"]);
                }
                i++;

            }
            total = other + insu + faight + fob;
            totallkr = otherlkr + insulkr + faightlkr + foblkr;

            DataTable finalele = new DataTable();
            DataRow elerow;
            finalele.Columns.Add("FOB", typeof(decimal));
            finalele.Columns.Add("FREIGHT", typeof(decimal));
            finalele.Columns.Add("INSU", typeof(decimal));
            finalele.Columns.Add("OTHER", typeof(decimal));
            finalele.Columns.Add("TOTAL", typeof(decimal));
            finalele.Columns.Add("TOTALLKR", typeof(decimal));
            finalele.Columns.Add("TOTTAX", typeof(decimal));
            finalele.Columns.Add("COUNTMAX", typeof(int));

            elerow = finalele.NewRow();
            elerow["FOB"] = fob;
            elerow["FREIGHT"] = faight;
            elerow["INSU"] = insu;
            elerow["OTHER"] = other;
            elerow["TOTAL"] = total;
            elerow["TOTALLKR"] = totallkr;
            elerow["TOTTAX"] = tottax;
            elerow["COUNTMAX"] = Convert.ToInt32(countnew);

            finalele.Rows.Add(elerow);

            DataTable cusdeccommon = bsObj.CHNLSVC.CustService.GetCusdecCommonNew("LK");


            //header data
            Hdrdata = bsObj.CHNLSVC.CustService.GetCusdecHDRData(entryNo, com);

            _goods_declarationIII.Database.Tables["firstGooddata"].SetDataSource(firsgood);
            //  _goods_declaration.Database.Tables["TaxValues"].SetDataSource(taxvalues);
            _goods_declarationIII.Database.Tables["HdrData"].SetDataSource(Hdrdata);

            int r = 0;
            foreach (DataRow secondpagedatanew in secondpagedata.Rows)
            {
                if (secondpagedata.Rows[r]["Line1"].ToString() == "") secondpagedata.Rows[r]["Line1"] = 0;
                if (secondpagedata.Rows[r]["Line2"].ToString() == "") secondpagedata.Rows[r]["Line2"] = 0;
                if (secondpagedata.Rows[r]["Line3"].ToString() == "") secondpagedata.Rows[r]["Line3"] = 0;
                r++;
            }

            _goods_declarationIII.Database.Tables["SecondPage"].SetDataSource(secondpagedata);
            _goods_declarationIII.Database.Tables["EleData"].SetDataSource(finalele);
            //  _goods_declaration.Database.Tables["Param"].SetDataSource(Param);
            _goods_declarationIII.Database.Tables["CusdecCommon"].SetDataSource(cusdeccommon);

            foreach (object repOp in _goods_declaration.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "firstpagetax")
                    {
                        ReportDocument subRepDoc = _goods_declarationIII.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["TaxValues"].SetDataSource(taxvalues);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "finaltaxvalues")
                    {
                        ReportDocument subRepDoc = _goods_declarationIII.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["TaxValues"].SetDataSource(taxvalues);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "finalrighttax")
                    {
                        ReportDocument subRepDoc = _goods_declarationIII.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["TaxValues"].SetDataSource(taxvalues);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "finalbottomtax")
                    {
                        ReportDocument subRepDoc = _goods_declarationIII.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["TaxValues"].SetDataSource(taxvalues);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "TotalTaxsummery")
                    {
                        ReportDocument subRepDoc = _goods_declarationIII.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["TaxValues"].SetDataSource(taxvalues);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "secondpageitemdetails")
                    {
                        ReportDocument subRepDoc = _goods_declarationIII.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["Param"].SetDataSource(Param);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    _cs.Dispose();
                }
            }
        }

        //Cusdec Assessments

        public void CusdecAssessment(string entryno, string loc, string com, string user)
        {
            DataTable Param = new DataTable();
            DataRow dr;
            decimal CIF = 0;
            decimal CID = 0;
            decimal EIC = 0;
            decimal PAL = 0;
            decimal VAT = 0;
            decimal XID = 0;
            decimal CES = 0;
            decimal NBT = 0;

            DateTime dt = DateTime.Now.Date;
            Int32 lineno = 0;
            decimal diffamount = 0;
            DateTime latestdate = DateTime.Now.Date;
            decimal ammount = 0;
            string fileno = "";
            string assno = "";
            decimal cif2 = 0;

            Param.Columns.Add("Company", typeof(string));
            Param.Columns.Add("User", typeof(string));
            Param.Columns.Add("Location", typeof(string));
            Param.Columns.Add("isth_doc_no", typeof(string));
            Param.Columns.Add("isth_dt", typeof(DateTime));
            Param.Columns.Add("istd_line_no", typeof(Int32));
            Param.Columns.Add("istd_entry_no", typeof(string));
            Param.Columns.Add("CIF", typeof(decimal));
            Param.Columns.Add("CID", typeof(decimal));
            Param.Columns.Add("EIC", typeof(decimal));
            Param.Columns.Add("PAL", typeof(decimal));
            Param.Columns.Add("VAT", typeof(decimal));
            Param.Columns.Add("XID", typeof(decimal));
            Param.Columns.Add("CE&S", typeof(decimal));
            Param.Columns.Add("NBT", typeof(decimal));
            Param.Columns.Add("istd_diff_amt", typeof(decimal));
            Param.Columns.Add("istd_cost_stl_amt", typeof(decimal));
            Param.Columns.Add("LatestDate", typeof(DateTime));
            Param.Columns.Add("cuh_file_no", typeof(string));
            Param.Columns.Add("istd_assess_no", typeof(string));
            Param.Columns.Add("cif2", typeof(decimal));



            DataTable maindata = bsObj.CHNLSVC.CustService.GetCusdecAssessmentData(entryno);
            int i = 0;

            if (maindata.Rows.Count > 0)
            {

                foreach (DataRow dtRow in maindata.Rows)
                {

                    string docno = maindata.Rows[i]["istd_entry_no"].ToString();
                    if (i + 1 < maindata.Rows.Count)
                    {
                        dr = Param.NewRow();
                        if (docno == maindata.Rows[i + 1]["istd_entry_no"].ToString())
                        {

                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "CIF")
                            {
                                CIF = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "CID")
                            {
                                CID = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "EIC")
                            {
                                EIC = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "PAL")
                            {
                                PAL = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "VAT")
                            {
                                VAT = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "XID")
                            {
                                XID = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "CE&S")
                            {
                                CES = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "NBT")
                            {
                                NBT = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            docno = maindata.Rows[i]["istd_entry_no"].ToString();
                            dt = Convert.ToDateTime(maindata.Rows[i]["isth_dt"].ToString());
                            lineno = Convert.ToInt32(maindata.Rows[i]["istd_line_no"].ToString());
                            diffamount = diffamount + Convert.ToDecimal(maindata.Rows[i]["istd_diff_amt"].ToString());
                            cif2 = Convert.ToDecimal(maindata.Rows[i]["cif"].ToString());

                            if (maindata.Rows[i]["LatestDate"].ToString() != "")
                                latestdate = Convert.ToDateTime(maindata.Rows[i]["LatestDate"].ToString());


                            ammount = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            fileno = maindata.Rows[i]["cuh_file_no"].ToString();
                            assno = maindata.Rows[i]["istd_assess_no"].ToString();

                            dr["Company"] = com;
                            dr["User"] = user;
                            dr["Location"] = loc;
                            dr["isth_doc_no"] = entryno;
                            dr["isth_dt"] = dt;
                            dr["istd_line_no"] = lineno;
                            dr["istd_entry_no"] = docno;
                            dr["CIF"] = CIF;
                            dr["CID"] = CID;
                            dr["EIC"] = EIC;
                            dr["PAL"] = PAL;
                            dr["VAT"] = VAT;
                            dr["XID"] = XID;
                            dr["CE&S"] = CES;
                            dr["NBT"] = NBT;
                            dr["istd_diff_amt"] = diffamount;
                            dr["istd_cost_stl_amt"] = ammount;
                            dr["LatestDate"] = latestdate;
                            dr["cuh_file_no"] = fileno;
                            dr["istd_assess_no"] = assno;
                            dr["cif2"] = cif2;


                        }
                        else
                        {
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "CIF")
                            {
                                CIF = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "CID")
                            {
                                CID = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "EIC")
                            {
                                EIC = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "PAL")
                            {
                                PAL = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "VAT")
                            {
                                VAT = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "XID")
                            {
                                XID = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "CE&S")
                            {
                                CES = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "NBT")
                            {
                                NBT = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            docno = maindata.Rows[i]["istd_entry_no"].ToString();
                            dt = Convert.ToDateTime(maindata.Rows[i]["isth_dt"].ToString());
                            lineno = Convert.ToInt32(maindata.Rows[i]["istd_line_no"].ToString());
                            diffamount = diffamount + Convert.ToDecimal(maindata.Rows[i]["istd_diff_amt"].ToString());
                            cif2 = Convert.ToDecimal(maindata.Rows[i]["cif"].ToString());

                            if (maindata.Rows[i]["LatestDate"].ToString() != "")
                                latestdate = Convert.ToDateTime(maindata.Rows[i]["LatestDate"].ToString());


                            ammount = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            fileno = maindata.Rows[i]["cuh_file_no"].ToString();
                            assno = maindata.Rows[i]["istd_assess_no"].ToString();

                            dr["Company"] = com;
                            dr["User"] = user;
                            dr["Location"] = loc;
                            dr["isth_doc_no"] = entryno;
                            dr["isth_dt"] = dt;
                            dr["istd_line_no"] = lineno;
                            dr["istd_entry_no"] = docno;
                            dr["CIF"] = CIF;
                            dr["CID"] = CID;
                            dr["EIC"] = EIC;
                            dr["PAL"] = PAL;
                            dr["VAT"] = VAT;
                            dr["XID"] = XID;
                            dr["CE&S"] = CES;
                            dr["NBT"] = NBT;
                            dr["istd_diff_amt"] = diffamount;
                            dr["istd_cost_stl_amt"] = ammount;
                            dr["LatestDate"] = latestdate;
                            dr["cuh_file_no"] = fileno;
                            dr["istd_assess_no"] = assno;
                            dr["cif2"] = cif2;

                            Param.Rows.Add(dr);
                            diffamount = 0;
                            CIF = 0;
                            CID = 0;
                            EIC = 0;
                            PAL = 0;
                            VAT = 0;
                            XID = 0;
                            CES = 0;
                            NBT = 0;
                            cif2 = 0;
                        }
                    }

                    if (i + 1 == maindata.Rows.Count)
                    {
                        dr = Param.NewRow();
                        if (maindata.Rows[i]["istd_cost_ele"].ToString() == "CIF")
                        {
                            CIF = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                        }
                        if (maindata.Rows[i]["istd_cost_ele"].ToString() == "CID")
                        {
                            CID = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                        }
                        if (maindata.Rows[i]["istd_cost_ele"].ToString() == "EIC")
                        {
                            EIC = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                        }
                        if (maindata.Rows[i]["istd_cost_ele"].ToString() == "PAL")
                        {
                            PAL = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                        }
                        if (maindata.Rows[i]["istd_cost_ele"].ToString() == "VAT")
                        {
                            VAT = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                        }
                        if (maindata.Rows[i]["istd_cost_ele"].ToString() == "XID")
                        {
                            XID = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                        }
                        if (maindata.Rows[i]["istd_cost_ele"].ToString() == "CE&S")
                        {
                            CES = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                        }
                        if (maindata.Rows[i]["istd_cost_ele"].ToString() == "NBT")
                        {
                            NBT = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                        }
                        docno = maindata.Rows[i]["istd_entry_no"].ToString();
                        dt = Convert.ToDateTime(maindata.Rows[i]["isth_dt"].ToString());
                        lineno = Convert.ToInt32(maindata.Rows[i]["istd_line_no"].ToString());
                        diffamount = diffamount + Convert.ToDecimal(maindata.Rows[i]["istd_diff_amt"].ToString());
                        cif2 = Convert.ToDecimal(maindata.Rows[i]["cif"].ToString());

                        if (maindata.Rows[i]["LatestDate"].ToString() != "")
                            latestdate = Convert.ToDateTime(maindata.Rows[i]["LatestDate"].ToString());


                        ammount = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                        fileno = maindata.Rows[i]["cuh_file_no"].ToString();
                        assno = maindata.Rows[i]["istd_assess_no"].ToString();

                        dr["Company"] = com;
                        dr["User"] = user;
                        dr["Location"] = loc;
                        dr["isth_doc_no"] = entryno;
                        dr["isth_dt"] = dt;
                        dr["istd_line_no"] = lineno;
                        dr["istd_entry_no"] = docno;
                        dr["CIF"] = CIF;
                        dr["CID"] = CID;
                        dr["EIC"] = EIC;
                        dr["PAL"] = PAL;
                        dr["VAT"] = VAT;
                        dr["XID"] = XID;
                        dr["CE&S"] = CES;
                        dr["NBT"] = NBT;
                        dr["istd_diff_amt"] = diffamount;
                        dr["istd_cost_stl_amt"] = ammount;
                        dr["LatestDate"] = latestdate;
                        dr["cuh_file_no"] = fileno;
                        dr["istd_assess_no"] = assno;
                        dr["cif2"] = cif2;

                        Param.Rows.Add(dr);
                        CIF = 0;
                        CID = 0;
                        EIC = 0;
                        PAL = 0;
                        VAT = 0;
                        XID = 0;
                        CES = 0;
                        NBT = 0;
                        cif2 = 0;
                    }


                    i++;
                }


            }

            _cusdec_assessment.Database.Tables["Param"].SetDataSource(Param);

        }

        public void CusdecAssessment2(string entryno, string loc, string com, string user)
        {
            DataTable Param = new DataTable();
            DataRow dr;
            decimal CIF = 0;
            decimal CID = 0;
            decimal EIC = 0;
            decimal PAL = 0;
            decimal VAT = 0;
            decimal XID = 0;
            decimal CES = 0;
            decimal NBT = 0;

            DateTime dt = DateTime.Now.Date;
            Int32 lineno = 0;
            decimal diffamount = 0;
            DateTime latestdate = DateTime.Now.Date;
            decimal ammount = 0;
            string fileno = "";
            string assno = "";
            decimal cif2 = 0;

            Param.Columns.Add("Company", typeof(string));
            Param.Columns.Add("User", typeof(string));
            Param.Columns.Add("Location", typeof(string));
            Param.Columns.Add("isth_doc_no", typeof(string));
            Param.Columns.Add("isth_dt", typeof(DateTime));
            Param.Columns.Add("istd_line_no", typeof(Int32));
            Param.Columns.Add("istd_entry_no", typeof(string));
            Param.Columns.Add("CIF", typeof(decimal));
            Param.Columns.Add("CID", typeof(decimal));
            Param.Columns.Add("EIC", typeof(decimal));
            Param.Columns.Add("PAL", typeof(decimal));
            Param.Columns.Add("VAT", typeof(decimal));
            Param.Columns.Add("XID", typeof(decimal));
            Param.Columns.Add("CE&S", typeof(decimal));
            Param.Columns.Add("NBT", typeof(decimal));
            Param.Columns.Add("istd_diff_amt", typeof(decimal));
            Param.Columns.Add("istd_cost_stl_amt", typeof(decimal));
            Param.Columns.Add("LatestDate", typeof(DateTime));
            Param.Columns.Add("cuh_file_no", typeof(string));
            Param.Columns.Add("istd_assess_no", typeof(string));
            Param.Columns.Add("cif2", typeof(decimal));



            DataTable maindata = bsObj.CHNLSVC.CustService.GetCusdecAssessmentData(entryno);
            int i = 0;

            if (maindata.Rows.Count > 0)
            {

                foreach (DataRow dtRow in maindata.Rows)
                {

                    string docno = maindata.Rows[i]["istd_entry_no"].ToString();
                    if (i + 1 < maindata.Rows.Count)
                    {
                        dr = Param.NewRow();
                        if (docno == maindata.Rows[i + 1]["istd_entry_no"].ToString())
                        {

                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "CIF")
                            {
                                CIF = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "CID")
                            {
                                CID = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "EIC")
                            {
                                EIC = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "PAL")
                            {
                                PAL = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "VAT")
                            {
                                VAT = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "XID")
                            {
                                XID = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "CE&S")
                            {
                                CES = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "NBT")
                            {
                                NBT = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            docno = maindata.Rows[i]["istd_entry_no"].ToString();
                            dt = Convert.ToDateTime(maindata.Rows[i]["isth_dt"].ToString());
                            lineno = Convert.ToInt32(maindata.Rows[i]["istd_line_no"].ToString());
                            diffamount = diffamount + Convert.ToDecimal(maindata.Rows[i]["istd_diff_amt"].ToString());
                            cif2 = Convert.ToDecimal(maindata.Rows[i]["cif"].ToString());

                            if (maindata.Rows[i]["LatestDate"].ToString() != "")
                                latestdate = Convert.ToDateTime(maindata.Rows[i]["LatestDate"].ToString());


                            ammount = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            fileno = maindata.Rows[i]["cuh_file_no"].ToString();
                            assno = maindata.Rows[i]["istd_assess_no"].ToString();

                            dr["Company"] = com;
                            dr["User"] = user;
                            dr["Location"] = loc;
                            dr["isth_doc_no"] = entryno;
                            dr["isth_dt"] = dt;
                            dr["istd_line_no"] = lineno;
                            dr["istd_entry_no"] = docno;
                            dr["CIF"] = CIF;
                            dr["CID"] = CID;
                            dr["EIC"] = EIC;
                            dr["PAL"] = PAL;
                            dr["VAT"] = VAT;
                            dr["XID"] = XID;
                            dr["CE&S"] = CES;
                            dr["NBT"] = NBT;
                            dr["istd_diff_amt"] = diffamount;
                            dr["istd_cost_stl_amt"] = ammount;
                            dr["LatestDate"] = latestdate;
                            dr["cuh_file_no"] = fileno;
                            dr["istd_assess_no"] = assno;
                            dr["cif2"] = cif2;


                        }
                        else
                        {
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "CIF")
                            {
                                CIF = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "CID")
                            {
                                CID = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "EIC")
                            {
                                EIC = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "PAL")
                            {
                                PAL = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "VAT")
                            {
                                VAT = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "XID")
                            {
                                XID = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "CE&S")
                            {
                                CES = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "NBT")
                            {
                                NBT = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            docno = maindata.Rows[i]["istd_entry_no"].ToString();
                            dt = Convert.ToDateTime(maindata.Rows[i]["isth_dt"].ToString());
                            lineno = Convert.ToInt32(maindata.Rows[i]["istd_line_no"].ToString());
                            diffamount = diffamount + Convert.ToDecimal(maindata.Rows[i]["istd_diff_amt"].ToString());
                            cif2 = Convert.ToDecimal(maindata.Rows[i]["cif"].ToString());

                            if (maindata.Rows[i]["LatestDate"].ToString() != "")
                                latestdate = Convert.ToDateTime(maindata.Rows[i]["LatestDate"].ToString());


                            ammount = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            fileno = maindata.Rows[i]["cuh_file_no"].ToString();
                            assno = maindata.Rows[i]["istd_assess_no"].ToString();

                            dr["Company"] = com;
                            dr["User"] = user;
                            dr["Location"] = loc;
                            dr["isth_doc_no"] = entryno;
                            dr["isth_dt"] = dt;
                            dr["istd_line_no"] = lineno;
                            dr["istd_entry_no"] = docno;
                            dr["CIF"] = CIF;
                            dr["CID"] = CID;
                            dr["EIC"] = EIC;
                            dr["PAL"] = PAL;
                            dr["VAT"] = VAT;
                            dr["XID"] = XID;
                            dr["CE&S"] = CES;
                            dr["NBT"] = NBT;
                            dr["istd_diff_amt"] = diffamount;
                            dr["istd_cost_stl_amt"] = ammount;
                            dr["LatestDate"] = latestdate;
                            dr["cuh_file_no"] = fileno;
                            dr["istd_assess_no"] = assno;
                            dr["cif2"] = cif2;

                            Param.Rows.Add(dr);
                            diffamount = 0;
                            CIF = 0;
                            CID = 0;
                            EIC = 0;
                            PAL = 0;
                            VAT = 0;
                            XID = 0;
                            CES = 0;
                            NBT = 0;
                            cif2 = 0;
                        }
                    }

                    if (i + 1 == maindata.Rows.Count)
                    {
                        dr = Param.NewRow();
                        if (maindata.Rows[i]["istd_cost_ele"].ToString() == "CIF")
                        {
                            CIF = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                        }
                        if (maindata.Rows[i]["istd_cost_ele"].ToString() == "CID")
                        {
                            CID = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                        }
                        if (maindata.Rows[i]["istd_cost_ele"].ToString() == "EIC")
                        {
                            EIC = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                        }
                        if (maindata.Rows[i]["istd_cost_ele"].ToString() == "PAL")
                        {
                            PAL = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                        }
                        if (maindata.Rows[i]["istd_cost_ele"].ToString() == "VAT")
                        {
                            VAT = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                        }
                        if (maindata.Rows[i]["istd_cost_ele"].ToString() == "XID")
                        {
                            XID = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                        }
                        if (maindata.Rows[i]["istd_cost_ele"].ToString() == "CE&S")
                        {
                            CES = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                        }
                        if (maindata.Rows[i]["istd_cost_ele"].ToString() == "NBT")
                        {
                            NBT = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                        }
                        docno = maindata.Rows[i]["istd_entry_no"].ToString();
                        dt = Convert.ToDateTime(maindata.Rows[i]["isth_dt"].ToString());
                        lineno = Convert.ToInt32(maindata.Rows[i]["istd_line_no"].ToString());
                        diffamount = diffamount + Convert.ToDecimal(maindata.Rows[i]["istd_diff_amt"].ToString());
                        cif2 = Convert.ToDecimal(maindata.Rows[i]["cif"].ToString());

                        if (maindata.Rows[i]["LatestDate"].ToString() != "")
                            latestdate = Convert.ToDateTime(maindata.Rows[i]["LatestDate"].ToString());


                        ammount = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                        fileno = maindata.Rows[i]["cuh_file_no"].ToString();
                        assno = maindata.Rows[i]["istd_assess_no"].ToString();

                        dr["Company"] = com;
                        dr["User"] = user;
                        dr["Location"] = loc;
                        dr["isth_doc_no"] = entryno;
                        dr["isth_dt"] = dt;
                        dr["istd_line_no"] = lineno;
                        dr["istd_entry_no"] = docno;
                        dr["CIF"] = CIF;
                        dr["CID"] = CID;
                        dr["EIC"] = EIC;
                        dr["PAL"] = PAL;
                        dr["VAT"] = VAT;
                        dr["XID"] = XID;
                        dr["CE&S"] = CES;
                        dr["NBT"] = NBT;
                        dr["istd_diff_amt"] = diffamount;
                        dr["istd_cost_stl_amt"] = ammount;
                        dr["LatestDate"] = latestdate;
                        dr["cuh_file_no"] = fileno;
                        dr["istd_assess_no"] = assno;
                        dr["cif2"] = cif2;

                        Param.Rows.Add(dr);
                        CIF = 0;
                        CID = 0;
                        EIC = 0;
                        PAL = 0;
                        VAT = 0;
                        XID = 0;
                        CES = 0;
                        NBT = 0;
                        cif2 = 0;
                    }


                    i++;
                }


            }

            _cusdec_assessment2.Database.Tables["Param"].SetDataSource(Param);

        }

        public void EXBOND2(string entryNo, string com)
        {
            DataTable Hdrdata = new DataTable();
            DataTable maindata = new DataTable();
            DataTable Param = new DataTable();


            DataRow dr;
            int i = 0;
            int j = 0;

            int linenumber = 0;
            int nxtlinenumber = 0;
            int maxlinenumber = 0;

            Param.Columns.Add("Company", typeof(string));
            Param.Columns.Add("User", typeof(string));
            Param.Columns.Add("Location", typeof(string));
            Param.Columns.Add("cui_itm_desc", typeof(string));
            Param.Columns.Add("cui_gross_mass", typeof(string));
            Param.Columns.Add("cui_net_mass", typeof(string));
            Param.Columns.Add("cui_bl_no", typeof(string));
            Param.Columns.Add("cui_orgin_cnty", typeof(string));
            Param.Columns.Add("cui_pkgs", typeof(string));
            Param.Columns.Add("Line", typeof(int));
            Param.Columns.Add("MaxLine", typeof(int));
            Param.Columns.Add("cui_hs_cd", typeof(string));
            Param.Columns.Add("cui_itm_price", typeof(decimal));
            Param.Columns.Add("cui_qty", typeof(string));
            Param.Columns.Add("cui_bal_qty1", typeof(string));
            Param.Columns.Add("cui_bal_qty2", typeof(string));
            Param.Columns.Add("cui_bal_qty3", typeof(string));
            Param.Columns.Add("cui_req_qty", typeof(string));


            //tax values
            DataTable taxvalues = new DataTable();
            DataRow trow;

            taxvalues.Columns.Add("Type", typeof(string));
            taxvalues.Columns.Add("Tax", typeof(decimal));
            taxvalues.Columns.Add("Rate", typeof(decimal));
            taxvalues.Columns.Add("Ammount", typeof(decimal));
            taxvalues.Columns.Add("MP", typeof(decimal));
            taxvalues.Columns.Add("Line", typeof(int));
            taxvalues.Columns.Add("Line2", typeof(int));
            taxvalues.Columns.Add("Line3", typeof(int));
            taxvalues.Columns.Add("cui_itm_desc", typeof(string));
            taxvalues.Columns.Add("cui_gross_mass", typeof(string));
            taxvalues.Columns.Add("cui_net_mass", typeof(string));
            taxvalues.Columns.Add("cui_bl_no", typeof(string));
            taxvalues.Columns.Add("cui_orgin_cnty", typeof(string));
            taxvalues.Columns.Add("cui_pkgs", typeof(string));

            DataTable firsgood = new DataTable();
            DataRow firstrow;

            firsgood.Columns.Add("cui_itm_desc", typeof(string));
            firsgood.Columns.Add("cui_gross_mass", typeof(string));
            firsgood.Columns.Add("cui_net_mass", typeof(string));
            firsgood.Columns.Add("cui_bl_no", typeof(string));
            firsgood.Columns.Add("cui_orgin_cnty", typeof(string));
            firsgood.Columns.Add("cui_pkgs", typeof(string));
            firsgood.Columns.Add("cui_hs_cd", typeof(string));
            firsgood.Columns.Add("cui_qty", typeof(string));
            firsgood.Columns.Add("cui_bal_qty2", typeof(string));
            firsgood.Columns.Add("cui_bal_qty3", typeof(string));
            firsgood.Columns.Add("cui_req_qty", typeof(string));
            firsgood.Columns.Add("cui_itm_price", typeof(decimal));
            firsgood.Columns.Add("cui_line", typeof(Int32));

            int licount = 1;

            //header data
            Hdrdata = bsObj.CHNLSVC.CustService.GetCusdecHDRData(entryNo, com);
            string sunbondno = Hdrdata.Rows[0]["cuh_sun_bond_no"].ToString();
            // all goods data
            //  maindata = bsObj.CHNLSVC.CustService.GetGoodsDeclarationDetails(entryNo);

            //maindata = maindata.AsEnumerable()
            //       .GroupBy(r2 => new {  Col1 = r2["cui_hs_cd"]})
            //       .Select(g => g.OrderBy(r2 => r2["cui_line"]).First())
            //       .CopyToDataTable();

            List<Cusdec_Goods_decl> maindatanew1 = bsObj.CHNLSVC.CustService.GetGoodsDeclarationDetailsList(sunbondno);
            //Update other doc line
            foreach (var updateotherlist in maindatanew1)
            {
                //getotherline
                List<Cusdec_Goods_decl> maindatanew11 = maindatanew1.Where(a => a.cui_line == updateotherlist.cui_line && a.cuic_itm_cd == updateotherlist.cuic_itm_cd).ToList();
                Int32 upline = maindatanew11.FirstOrDefault().cui_oth_doc_line;

                int effect = bsObj.CHNLSVC.Financial.UpdateOtherdocline(entryNo, updateotherlist.cui_line, updateotherlist.cuic_itm_cd, upline);
            }

            List<Cusdec_Goods_decl> maindatanew = bsObj.CHNLSVC.CustService.GetGoodsDeclarationDetailsList(entryNo);
            List<Cusdec_Goods_decl> resultnew = maindatanew
         .GroupBy(l => new { l.cui_oth_doc_line, l.cuic_ele_cd })
         .Select(cl => new Cusdec_Goods_decl
         {
             cui_line = cl.First().cui_line,
             cui_itm_desc = cl.First().cui_itm_desc,
             cui_gross_mass = cl.Sum(c => c.cui_gross_mass),
             cui_net_mass = cl.Sum(c => c.cui_net_mass),
             cui_bl_no = cl.First().cui_bl_no,
             cui_orgin_cnty = cl.First().cui_orgin_cnty,
             cui_pkgs = cl.First().cui_pkgs,
             cui_hs_cd = cl.First().cui_hs_cd,
             cui_qty = cl.Sum(c => c.cui_qty),
             cui_bal_qty1 = cl.Sum(c => c.cui_bal_qty1),
             cui_bal_qty2 = cl.Sum(c => c.cui_bal_qty2),
             cui_bal_qty3 = cl.Sum(c => c.cui_bal_qty3),
             cui_req_qty = cl.Sum(c => c.cui_req_qty),
             cui_itm_price = cl.Sum(c => c.cui_itm_price),
             cuic_ele_cd = cl.First().cuic_ele_cd,
             cuic_ele_base = cl.First().cuic_ele_base,
             cuic_ele_rt = cl.First().cuic_ele_rt,
             cuic_ele_amt = cl.Sum(c => c.cuic_ele_amt),
             cuic_ele_mp = cl.First().cuic_ele_mp,
             cui_model = cl.First().cui_model,
             cui_itm_price2 = cl.Sum(c => c.cui_itm_price2),
             cui_oth_doc_line = cl.First().cui_oth_doc_line


         }).ToList();

            List<Cusdec_Goods_decl> resultnewgetcount = maindatanew
        .GroupBy(l => new { l.cui_oth_doc_line })
        .Select(cl => new Cusdec_Goods_decl
        {
            cui_line = cl.First().cui_line,
            cui_itm_desc = cl.First().cui_itm_desc,
            cui_gross_mass = cl.Sum(c => c.cui_gross_mass),
            cui_net_mass = cl.Sum(c => c.cui_net_mass),
            cui_bl_no = cl.First().cui_bl_no,
            cui_orgin_cnty = cl.First().cui_orgin_cnty,
            cui_pkgs = cl.First().cui_pkgs,
            cui_hs_cd = cl.First().cui_hs_cd,
            cui_qty = cl.Sum(c => c.cui_qty),
            cui_bal_qty1 = cl.Sum(c => c.cui_bal_qty1),
            cui_bal_qty2 = cl.Sum(c => c.cui_bal_qty2),
            cui_bal_qty3 = cl.Sum(c => c.cui_bal_qty3),
            cui_req_qty = cl.Sum(c => c.cui_req_qty),
            cui_itm_price = cl.Sum(c => c.cui_itm_price),
            cuic_ele_cd = cl.First().cuic_ele_cd,
            cuic_ele_base = cl.First().cuic_ele_base,
            cuic_ele_rt = cl.First().cuic_ele_rt,
            cuic_ele_amt = cl.Sum(c => c.cuic_ele_amt),
            cuic_ele_mp = cl.First().cuic_ele_mp,
            cui_model = cl.First().cui_model,
            cui_itm_price2 = cl.Sum(c => c.cui_itm_price2),
            cui_oth_doc_line = cl.First().cui_oth_doc_line


        }).ToList();
            resultnew = resultnew.OrderBy(A => A.cui_oth_doc_line).ToList();
            var maxcount = resultnewgetcount.Count;

            foreach (var resultnewww in resultnew)
            {
                string itemsdes = "";
                string hscode = resultnewww.cui_hs_cd.ToString();
                string dutyele = resultnewww.cuic_ele_cd;
                decimal unitrateneww = resultnewww.cui_itm_price2 / resultnewww.cui_qty;

                foreach (var maindatanewww in maindatanew)
                {
                    if (maindatanewww.cui_hs_cd == hscode && maindatanewww.cui_unit_rt == unitrateneww && maindatanewww.cuic_ele_cd == dutyele)
                    {
                        itemsdes = itemsdes + "  " + maindatanewww.cui_qty + " U" + "  " + maindatanewww.cui_itm_desc + " " + maindatanewww.cui_model;
                    }
                }
                resultnewww.cui_itm_desc = itemsdes;

            }


            IEnumerable<Cusdec_Goods_decl> resultnew2 = resultnew;
            DataTable table = new DataTable();
            using (var reader = ObjectReader.Create(resultnew2))
            {
                table.Load(reader);
            }

            maindata = table;
            if (maindata.Rows.Count > 0)
            {
                foreach (DataRow dtRow in maindata.Rows)
                {
                    int nxtline;
                    linenumber = Convert.ToInt32(maindata.Rows[i]["cui_line"]);
                    if (i + 1 < maindata.Rows.Count)
                    {
                        nxtline = Convert.ToInt32(maindata.Rows[i + 1]["cui_line"]);
                    }
                    else
                    {
                        nxtline = 99999;
                    }



                    if (linenumber != nxtline)
                    {
                        trow = taxvalues.NewRow();
                        trow["Type"] = maindata.Rows[i]["cuic_ele_cd"].ToString();
                        trow["Tax"] = maindata.Rows[i]["cuic_ele_base"].ToString();
                        trow["Rate"] = maindata.Rows[i]["cuic_ele_rt"].ToString();
                        trow["Ammount"] = maindata.Rows[i]["cuic_ele_amt"].ToString();
                        trow["MP"] = maindata.Rows[i]["cuic_ele_mp"].ToString();
                        trow["Line"] = Convert.ToInt32(maindata.Rows[i]["cui_oth_doc_line"].ToString());
                        trow["Line2"] = Convert.ToInt32(maindata.Rows[i]["cui_oth_doc_line"].ToString());
                        trow["Line3"] = Convert.ToInt32(maindata.Rows[i]["cui_oth_doc_line"].ToString());
                        trow["cui_itm_desc"] = maindata.Rows[i]["cui_itm_desc"].ToString() + " ";
                        trow["cui_gross_mass"] = maindata.Rows[i]["cui_gross_mass"].ToString();
                        trow["cui_net_mass"] = maindata.Rows[i]["cui_net_mass"].ToString();
                        trow["cui_bl_no"] = maindata.Rows[i]["cui_bl_no"].ToString();
                        trow["cui_orgin_cnty"] = maindata.Rows[i]["cui_orgin_cnty"].ToString();
                        trow["cui_pkgs"] = maindata.Rows[i]["cui_pkgs"].ToString();

                        taxvalues.Rows.Add(trow);
                        maxlinenumber = linenumber;
                        i++;
                        licount++;
                    }
                    else
                    {
                        trow = taxvalues.NewRow();
                        trow["Type"] = maindata.Rows[i]["cuic_ele_cd"].ToString();
                        trow["Tax"] = maindata.Rows[i]["cuic_ele_base"].ToString();
                        trow["Rate"] = maindata.Rows[i]["cuic_ele_rt"].ToString();
                        trow["Ammount"] = maindata.Rows[i]["cuic_ele_amt"].ToString();
                        trow["MP"] = maindata.Rows[i]["cuic_ele_mp"].ToString();
                        trow["Line"] = Convert.ToInt32(maindata.Rows[i]["cui_oth_doc_line"].ToString());
                        trow["Line2"] = Convert.ToInt32(maindata.Rows[i]["cui_oth_doc_line"].ToString());
                        trow["Line3"] = Convert.ToInt32(maindata.Rows[i]["cui_oth_doc_line"].ToString());
                        trow["cui_itm_desc"] = maindata.Rows[i]["cui_itm_desc"].ToString() + " ";
                        trow["cui_gross_mass"] = maindata.Rows[i]["cui_gross_mass"].ToString();
                        trow["cui_net_mass"] = maindata.Rows[i]["cui_net_mass"].ToString();
                        trow["cui_bl_no"] = maindata.Rows[i]["cui_bl_no"].ToString();
                        trow["cui_orgin_cnty"] = maindata.Rows[i]["cui_orgin_cnty"].ToString();
                        trow["cui_pkgs"] = maindata.Rows[i]["cui_pkgs"].ToString();

                        taxvalues.Rows.Add(trow);
                        maxlinenumber = linenumber;
                        i++;
                    }


                }
            }

            if (Hdrdata.Rows.Count > 0)
            {
                trow = taxvalues.NewRow();
                trow["Type"] = "CE&S";
                trow["Tax"] = "1";
                trow["Rate"] = "1";
                trow["Ammount"] = Hdrdata.Rows[0]["CUH_COM_CHG"].ToString();
                trow["MP"] = "1";
                trow["Line"] = Convert.ToInt32(maindata.Rows[0]["cui_oth_doc_line"].ToString());
                trow["Line2"] = Convert.ToInt32(maindata.Rows[0]["cui_oth_doc_line"].ToString());
                trow["Line3"] = Convert.ToInt32(maindata.Rows[0]["cui_oth_doc_line"].ToString());
                taxvalues.Rows.Add(trow);

            }
            if (maindata.Rows.Count > 0)
            {
                foreach (DataRow dtRow in maindata.Rows)
                {
                    if (j < maindata.Rows.Count - 1)
                    {
                        linenumber = Convert.ToInt32(maindata.Rows[j]["cui_line"]);
                        nxtlinenumber = Convert.ToInt32(maindata.Rows[j + 1]["cui_line"]);

                        if (linenumber != nxtlinenumber)
                        {
                            dr = Param.NewRow();
                            dr["Company"] = "ABL";
                            dr["User"] = "ADMIN";
                            dr["Location"] = "DMP";
                            dr["cui_itm_desc"] = maindata.Rows[j]["cui_itm_desc"].ToString() + " ";
                            dr["cui_gross_mass"] = maindata.Rows[j]["cui_gross_mass"].ToString();
                            dr["cui_net_mass"] = maindata.Rows[j]["cui_net_mass"].ToString();
                            dr["cui_bl_no"] = maindata.Rows[j]["cui_bl_no"].ToString();
                            dr["cui_orgin_cnty"] = maindata.Rows[j]["cui_orgin_cnty"].ToString();
                            dr["cui_pkgs"] = maindata.Rows[j]["cui_pkgs"].ToString();
                            dr["Line"] = Convert.ToInt32(maindata.Rows[j]["cui_oth_doc_line"].ToString());
                            dr["MaxLine"] = maxlinenumber;
                            dr["cui_hs_cd"] = maindata.Rows[j]["cui_hs_cd"].ToString();
                            dr["cui_qty"] = maindata.Rows[j]["cui_qty"].ToString();
                            dr["cui_bal_qty1"] = maindata.Rows[j]["cui_bal_qty1"].ToString();
                            dr["cui_bal_qty2"] = maindata.Rows[j]["cui_bal_qty2"].ToString();
                            dr["cui_bal_qty3"] = maindata.Rows[j]["cui_bal_qty3"].ToString();
                            dr["cui_req_qty"] = maindata.Rows[j]["cui_req_qty"].ToString();
                            dr["cui_itm_price"] = Convert.ToDecimal(maindata.Rows[j]["cui_itm_price"].ToString());

                            Param.Rows.Add(dr);
                        }
                        if (j == maindata.Rows.Count - 2)
                        {
                            dr = Param.NewRow();
                            dr["Company"] = "ABL";
                            dr["User"] = "ADMIN";
                            dr["Location"] = "DMP";
                            dr["cui_itm_desc"] = maindata.Rows[j]["cui_itm_desc"].ToString() + " ";
                            dr["cui_gross_mass"] = maindata.Rows[j]["cui_gross_mass"].ToString();
                            dr["cui_net_mass"] = maindata.Rows[j]["cui_net_mass"].ToString();
                            dr["cui_bl_no"] = maindata.Rows[j]["cui_bl_no"].ToString();
                            dr["cui_orgin_cnty"] = maindata.Rows[j]["cui_orgin_cnty"].ToString();
                            dr["cui_pkgs"] = maindata.Rows[j]["cui_pkgs"].ToString();
                            dr["Line"] = Convert.ToInt32(maindata.Rows[j]["cui_oth_doc_line"].ToString());
                            dr["MaxLine"] = maxlinenumber;
                            dr["cui_hs_cd"] = maindata.Rows[j]["cui_hs_cd"].ToString();
                            dr["cui_qty"] = maindata.Rows[j]["cui_qty"].ToString();
                            dr["cui_bal_qty1"] = maindata.Rows[j]["cui_bal_qty1"].ToString();
                            dr["cui_bal_qty2"] = maindata.Rows[j]["cui_bal_qty2"].ToString();
                            dr["cui_bal_qty3"] = maindata.Rows[j]["cui_bal_qty3"].ToString();
                            dr["cui_req_qty"] = maindata.Rows[j]["cui_req_qty"].ToString();
                            dr["cui_itm_price"] = Convert.ToDecimal(maindata.Rows[j]["cui_itm_price"].ToString());
                            Param.Rows.Add(dr);
                        }
                    }
                    j++;
                }
            }
            if (maindata.Rows.Count > 0)
            {

                firstrow = firsgood.NewRow();

                firstrow["cui_itm_desc"] = maindata.Rows[0]["cui_itm_desc"].ToString() + " ";
                firstrow["cui_gross_mass"] = maindata.Rows[0]["cui_gross_mass"].ToString();
                firstrow["cui_net_mass"] = maindata.Rows[0]["cui_net_mass"].ToString();
                firstrow["cui_bl_no"] = maindata.Rows[0]["cui_bl_no"].ToString();
                firstrow["cui_orgin_cnty"] = maindata.Rows[0]["cui_orgin_cnty"].ToString();
                firstrow["cui_pkgs"] = maindata.Rows[0]["cui_pkgs"].ToString();
                firstrow["cui_hs_cd"] = maindata.Rows[0]["cui_hs_cd"].ToString();
                firstrow["cui_qty"] = maindata.Rows[0]["cui_qty"].ToString();
                firstrow["cui_bal_qty2"] = maindata.Rows[0]["cui_bal_qty2"].ToString();
                firstrow["cui_bal_qty3"] = maindata.Rows[0]["cui_bal_qty3"].ToString();
                firstrow["cui_req_qty"] = maindata.Rows[0]["cui_req_qty"].ToString();
                firstrow["cui_itm_price"] = Convert.ToDecimal(maindata.Rows[0]["cui_itm_price"].ToString());
                firstrow["cui_line"] = Convert.ToInt32(maindata.Rows[0]["cui_oth_doc_line"].ToString());
                firsgood.Rows.Add(firstrow);
            }
            // create second page table
            DataTable secondpagedata = new DataTable();
            DataRow srow;

            secondpagedata.Columns.Add("cui_itm_desc1", typeof(string));
            secondpagedata.Columns.Add("cui_gross_mass1", typeof(string));
            secondpagedata.Columns.Add("cui_net_mass1", typeof(string));
            secondpagedata.Columns.Add("cui_bl_no1", typeof(string));
            secondpagedata.Columns.Add("cui_orgin_cnty1", typeof(string));
            secondpagedata.Columns.Add("cui_itm_desc2", typeof(string));
            secondpagedata.Columns.Add("cui_gross_mass2", typeof(string));
            secondpagedata.Columns.Add("cui_net_mass2", typeof(string));
            secondpagedata.Columns.Add("cui_bl_no2", typeof(string));
            secondpagedata.Columns.Add("cui_orgin_cnty2", typeof(string));
            secondpagedata.Columns.Add("cui_itm_desc3", typeof(string));
            secondpagedata.Columns.Add("cui_gross_mass3", typeof(string));
            secondpagedata.Columns.Add("cui_net_mass3", typeof(string));
            secondpagedata.Columns.Add("cui_bl_no3", typeof(string));
            secondpagedata.Columns.Add("cui_orgin_cnty3", typeof(string));
            secondpagedata.Columns.Add("Line1", typeof(Int16));
            secondpagedata.Columns.Add("Line2", typeof(Int16));
            secondpagedata.Columns.Add("Line3", typeof(Int16));
            secondpagedata.Columns.Add("LineCount1", typeof(Int16));
            secondpagedata.Columns.Add("LineCount2", typeof(Int16));
            secondpagedata.Columns.Add("LineCount3", typeof(Int16));
            secondpagedata.Columns.Add("cui_hs_cd1", typeof(string));
            secondpagedata.Columns.Add("cui_hs_cd2", typeof(string));
            secondpagedata.Columns.Add("cui_hs_cd3", typeof(string));
            secondpagedata.Columns.Add("cui_itm_price1", typeof(decimal));
            secondpagedata.Columns.Add("cui_itm_price2", typeof(decimal));
            secondpagedata.Columns.Add("cui_itm_price3", typeof(decimal));
            secondpagedata.Columns.Add("cui_bal_qty11", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty12", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty13", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty21", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty22", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty23", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty31", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty32", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty33", typeof(string));
            int itm = 0;
            i = 1;
            if (Param.Rows.Count > 0)
            {
                for (int k = 0; k <= Param.Rows.Count; k = k + 3)
                {
                    srow = secondpagedata.NewRow();
                    if (i < Param.Rows.Count)
                    {
                        itm += 1;
                        srow["cui_itm_desc1"] = Param.Rows[i]["cui_itm_desc"].ToString();
                        srow["cui_gross_mass1"] = Param.Rows[i]["cui_gross_mass"].ToString();
                        srow["cui_net_mass1"] = Param.Rows[i]["cui_net_mass"].ToString();
                        srow["cui_bl_no1"] = Param.Rows[i]["cui_bl_no"].ToString();
                        srow["cui_orgin_cnty1"] = Param.Rows[i]["cui_orgin_cnty"].ToString();
                        srow["Line1"] = Param.Rows[i]["Line"].ToString();
                        srow["LineCount1"] = Convert.ToInt16(i + 1);
                        srow["cui_hs_cd1"] = Param.Rows[i]["cui_hs_cd"].ToString();
                        srow["cui_itm_price1"] = Convert.ToDecimal(Param.Rows[i]["cui_itm_price"].ToString());
                        srow["cui_bal_qty11"] = Param.Rows[i]["cui_qty"].ToString();
                        srow["cui_bal_qty21"] = Param.Rows[i]["cui_bal_qty2"].ToString();
                        srow["cui_bal_qty31"] = Param.Rows[i]["cui_bal_qty3"].ToString();
                    }

                    if (i + 1 < Param.Rows.Count)
                    {
                        itm += 1;
                        srow["cui_itm_desc2"] = Param.Rows[i + 1]["cui_itm_desc"].ToString();
                        srow["cui_gross_mass2"] = Param.Rows[i + 1]["cui_gross_mass"].ToString();
                        srow["cui_net_mass2"] = Param.Rows[i + 1]["cui_net_mass"].ToString();
                        srow["cui_bl_no2"] = Param.Rows[i + 1]["cui_bl_no"].ToString();
                        srow["cui_orgin_cnty2"] = Param.Rows[i + 1]["cui_orgin_cnty"].ToString();
                        srow["Line2"] = Param.Rows[i + 1]["Line"].ToString();
                        srow["LineCount2"] = Convert.ToInt16(i + 2);
                        srow["cui_hs_cd2"] = Param.Rows[i + 1]["cui_hs_cd"].ToString();
                        srow["cui_itm_price2"] = Convert.ToDecimal(Param.Rows[i + 1]["cui_itm_price"].ToString());
                        srow["cui_bal_qty12"] = Param.Rows[i + 1]["cui_qty"].ToString();
                        srow["cui_bal_qty22"] = Param.Rows[i + 1]["cui_bal_qty2"].ToString();
                        srow["cui_bal_qty32"] = Param.Rows[i + 1]["cui_bal_qty3"].ToString();
                    }

                    if (i + 2 < Param.Rows.Count)
                    {
                        itm += 1;
                        srow["cui_itm_desc3"] = Param.Rows[i + 2]["cui_itm_desc"].ToString();
                        srow["cui_gross_mass3"] = Param.Rows[i + 2]["cui_gross_mass"].ToString();
                        srow["cui_net_mass3"] = Param.Rows[i + 2]["cui_net_mass"].ToString();
                        srow["cui_bl_no3"] = Param.Rows[i + 2]["cui_bl_no"].ToString();
                        srow["cui_orgin_cnty3"] = Param.Rows[i + 2]["cui_orgin_cnty"].ToString();
                        srow["Line3"] = Param.Rows[i + 2]["Line"].ToString();
                        srow["LineCount3"] = Convert.ToInt16(i + 3);
                        srow["cui_hs_cd3"] = Param.Rows[i + 2]["cui_hs_cd"].ToString();
                        srow["cui_itm_price3"] = Convert.ToDecimal(Param.Rows[i + 2]["cui_itm_price"].ToString());
                        srow["cui_bal_qty13"] = Param.Rows[i + 2]["cui_qty"].ToString();
                        srow["cui_bal_qty23"] = Param.Rows[i + 2]["cui_bal_qty2"].ToString();
                        srow["cui_bal_qty33"] = Param.Rows[i + 2]["cui_bal_qty3"].ToString();
                    }

                    secondpagedata.Rows.Add(srow);
                    i = i + 3;
                }
            }

            DataTable eledata = new DataTable();
            i = 0;
            string cat = "";
            decimal fob = 0;
            decimal foblkr = 0;
            decimal faight = 0;
            decimal faightlkr = 0;
            decimal insu = 0;
            decimal insulkr = 0;
            decimal other = 0;
            decimal otherlkr = 0;
            decimal total = 0;
            decimal totallkr = 0;

            eledata = bsObj.CHNLSVC.CustService.GetCusdecElementnew(entryNo);
            foreach (DataRow ele in eledata.Rows)
            {
                cat = eledata.Rows[i]["cus_ele_cd"].ToString();

                if (cat == "OTH")
                {
                    other = Convert.ToDecimal(eledata.Rows[i]["cus_amt"]);
                    otherlkr = Convert.ToDecimal(eledata.Rows[i]["cus_amt_com"]);
                }
                if (cat == "INSU")
                {
                    insu = Convert.ToDecimal(eledata.Rows[i]["cus_amt"]);
                    insulkr = Convert.ToDecimal(eledata.Rows[i]["cus_amt_com"]);
                }
                if (cat == "FRGT")
                {
                    faight = Convert.ToDecimal(eledata.Rows[i]["cus_amt"]);
                    faightlkr = Convert.ToDecimal(eledata.Rows[i]["cus_amt_com"]);

                }
                if (cat == "COST")
                {
                    fob = Convert.ToDecimal(eledata.Rows[i]["cus_amt"]);
                    foblkr = Convert.ToDecimal(eledata.Rows[i]["cus_amt_com"]);
                }
                i++;

            }
            total = other + insu + faight + fob;
            totallkr = otherlkr + insulkr + faightlkr + foblkr;

            DataTable finalele = new DataTable();
            DataRow elerow;
            finalele.Columns.Add("FOB", typeof(decimal));
            finalele.Columns.Add("FREIGHT", typeof(decimal));
            finalele.Columns.Add("INSU", typeof(decimal));
            finalele.Columns.Add("OTHER", typeof(decimal));
            finalele.Columns.Add("TOTAL", typeof(decimal));
            finalele.Columns.Add("TOTALLKR", typeof(decimal));
            finalele.Columns.Add("COUNTMAX", typeof(int));

            elerow = finalele.NewRow();
            elerow["FOB"] = fob;
            elerow["FREIGHT"] = faight;
            elerow["INSU"] = insu;
            elerow["OTHER"] = other;
            elerow["TOTAL"] = total;
            elerow["TOTALLKR"] = totallkr;
            elerow["COUNTMAX"] = maxcount;

            finalele.Rows.Add(elerow);

            DataTable cusdeccommon = bsObj.CHNLSVC.CustService.GetCusdecCommonNew("LK");

            List<ImportsBLContainer> blContainers = new List<ImportsBLContainer>();
            if (Hdrdata != null)
            {
                blContainers = bsObj.CHNLSVC.Financial.GetContainers(Hdrdata.Rows[0]["cuh_sun_req_no"].ToString());
            }
            else
            {
                blContainers = null;
            }

            DataTable page = new DataTable();
            DataRow pagerow;
            page.Columns.Add("Totalitems", typeof(Int32));
            pagerow = page.NewRow();
            pagerow["Totalitems"] = itm + 1;
            page.Rows.Add(pagerow);

            _cusdec_Exbond2.Database.Tables["firstGooddata"].SetDataSource(firsgood);
            //  _goods_declaration.Database.Tables["TaxValues"].SetDataSource(taxvalues);
            _cusdec_Exbond2.Database.Tables["HdrData"].SetDataSource(Hdrdata);

            int r = 0;
            foreach (DataRow secondpagedatanew in secondpagedata.Rows)
            {
                if (secondpagedata.Rows[r]["Line1"].ToString() == "") secondpagedata.Rows[r]["Line1"] = 0;
                if (secondpagedata.Rows[r]["Line2"].ToString() == "") secondpagedata.Rows[r]["Line2"] = 0;
                if (secondpagedata.Rows[r]["Line3"].ToString() == "") secondpagedata.Rows[r]["Line3"] = 0;
                r++;
            }

            _cusdec_Exbond2.Database.Tables["SecondPage"].SetDataSource(secondpagedata);
            _cusdec_Exbond2.Database.Tables["EleData"].SetDataSource(finalele);
            //  _goods_declaration.Database.Tables["Param"].SetDataSource(Param);
            _cusdec_Exbond2.Database.Tables["CusdecCommon"].SetDataSource(cusdeccommon);
            _cusdec_Exbond2.Database.Tables["Page"].SetDataSource(page);

            foreach (object repOp in _cusdec_Exbond2.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "firstpagetax")
                    {
                        ReportDocument subRepDoc = _cusdec_Exbond2.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["TaxValues"].SetDataSource(taxvalues);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "finaltaxvalues")
                    {
                        ReportDocument subRepDoc = _cusdec_Exbond2.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["TaxValues"].SetDataSource(taxvalues);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "finalrighttax")
                    {
                        ReportDocument subRepDoc = _cusdec_Exbond2.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["TaxValues"].SetDataSource(taxvalues);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "finalbottomtax")
                    {
                        ReportDocument subRepDoc = _cusdec_Exbond2.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["TaxValues"].SetDataSource(taxvalues);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "TotalTaxsummery")
                    {
                        ReportDocument subRepDoc = _cusdec_Exbond2.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["TaxValues"].SetDataSource(taxvalues);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "secondpageitemdetails")
                    {
                        ReportDocument subRepDoc = _cusdec_Exbond2.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["Param"].SetDataSource(Param);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "containers")
                    {
                        ReportDocument subRepDoc = _cusdec_Exbond2.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["Containers"].SetDataSource(blContainers);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    _cs.Dispose();
                }
            }
        }

        public void Tobond2(string entryNo, string com)
        {
            DataTable Hdrdata = new DataTable();
            DataTable maindata = new DataTable();
            DataTable Param = new DataTable();


            DataRow dr;
            int i = 0;
            int j = 0;

            int linenumber = 0;
            int nxtlinenumber = 0;
            int maxlinenumber = 0;

            Param.Columns.Add("Company", typeof(string));
            Param.Columns.Add("User", typeof(string));
            Param.Columns.Add("Location", typeof(string));
            Param.Columns.Add("cui_itm_desc", typeof(string));
            Param.Columns.Add("cui_gross_mass", typeof(string));
            Param.Columns.Add("cui_net_mass", typeof(string));
            Param.Columns.Add("cui_bl_no", typeof(string));
            Param.Columns.Add("cui_orgin_cnty", typeof(string));
            Param.Columns.Add("cui_pkgs", typeof(string));
            Param.Columns.Add("Line", typeof(int));
            Param.Columns.Add("MaxLine", typeof(int));
            Param.Columns.Add("cui_hs_cd", typeof(string));
            Param.Columns.Add("cui_itm_price", typeof(decimal));
            Param.Columns.Add("cui_qty", typeof(string));
            Param.Columns.Add("cui_bal_qty1", typeof(string));
            Param.Columns.Add("cui_bal_qty2", typeof(string));
            Param.Columns.Add("cui_bal_qty3", typeof(string));
            Param.Columns.Add("cui_req_qty", typeof(string));


            //tax values
            DataTable taxvalues = new DataTable();
            DataRow trow;

            taxvalues.Columns.Add("Type", typeof(string));
            taxvalues.Columns.Add("Tax", typeof(decimal));
            taxvalues.Columns.Add("Rate", typeof(decimal));
            taxvalues.Columns.Add("Ammount", typeof(decimal));
            taxvalues.Columns.Add("MP", typeof(decimal));
            taxvalues.Columns.Add("Line", typeof(int));
            taxvalues.Columns.Add("Line2", typeof(int));
            taxvalues.Columns.Add("Line3", typeof(int));
            taxvalues.Columns.Add("cui_itm_desc", typeof(string));
            taxvalues.Columns.Add("cui_gross_mass", typeof(string));
            taxvalues.Columns.Add("cui_net_mass", typeof(string));
            taxvalues.Columns.Add("cui_bl_no", typeof(string));
            taxvalues.Columns.Add("cui_orgin_cnty", typeof(string));
            taxvalues.Columns.Add("cui_pkgs", typeof(string));

            DataTable firsgood = new DataTable();
            DataRow firstrow;

            firsgood.Columns.Add("cui_itm_desc", typeof(string));
            firsgood.Columns.Add("cui_gross_mass", typeof(string));
            firsgood.Columns.Add("cui_net_mass", typeof(string));
            firsgood.Columns.Add("cui_bl_no", typeof(string));
            firsgood.Columns.Add("cui_orgin_cnty", typeof(string));
            firsgood.Columns.Add("cui_pkgs", typeof(string));
            firsgood.Columns.Add("cui_hs_cd", typeof(string));
            firsgood.Columns.Add("cui_qty", typeof(string));
            firsgood.Columns.Add("cui_bal_qty2", typeof(string));
            firsgood.Columns.Add("cui_bal_qty3", typeof(string));
            firsgood.Columns.Add("cui_req_qty", typeof(string));
            firsgood.Columns.Add("cui_itm_price", typeof(decimal));
            firsgood.Columns.Add("cui_line", typeof(Int32));

            //header data
            Hdrdata = bsObj.CHNLSVC.CustService.GetCusdecHDRData(entryNo, com);
            // all goods data
            maindata = bsObj.CHNLSVC.CustService.GetGoodsDeclarationDetails(entryNo);

            maindata = maindata.AsEnumerable()
                  .GroupBy(r2 => new { Col0 = r2["cui_model"], Col1 = r2["cui_hs_cd"], Col2 = r2["cui_unit_rt"], Col3 = r2["cuic_ele_cd"] })
                  .Select(g => g.OrderBy(r2 => r2["cui_hs_cd"]).First())
                  .CopyToDataTable();

            if (maindata != null)
            {
                foreach (DataRow dtRow in maindata.Rows)
                {
                    linenumber = Convert.ToInt32(maindata.Rows[i]["cui_line"]);
                    trow = taxvalues.NewRow();
                    trow["Type"] = maindata.Rows[i]["cuic_ele_cd"].ToString();
                    trow["Tax"] = maindata.Rows[i]["cuic_ele_base"].ToString();
                    trow["Rate"] = maindata.Rows[i]["cuic_ele_rt"].ToString();
                    trow["Ammount"] = maindata.Rows[i]["cuic_ele_amt"].ToString();
                    trow["MP"] = maindata.Rows[i]["cuic_ele_mp"].ToString();
                    trow["Line"] = linenumber;
                    trow["Line2"] = linenumber;
                    trow["Line3"] = linenumber;
                    trow["cui_itm_desc"] = maindata.Rows[i]["cui_itm_desc"].ToString() + " " + maindata.Rows[i]["cui_model"].ToString();
                    trow["cui_gross_mass"] = maindata.Rows[i]["cui_gross_mass"].ToString();
                    trow["cui_net_mass"] = maindata.Rows[i]["cui_net_mass"].ToString();
                    trow["cui_bl_no"] = maindata.Rows[i]["cui_bl_no"].ToString();
                    trow["cui_orgin_cnty"] = maindata.Rows[i]["cui_orgin_cnty"].ToString();
                    trow["cui_pkgs"] = maindata.Rows[i]["cui_pkgs"].ToString();

                    taxvalues.Rows.Add(trow);
                    maxlinenumber = linenumber;
                    i++;
                }
            }
            if (Hdrdata.Rows.Count > 0)
            {
                trow = taxvalues.NewRow();
                trow["Type"] = "CE&S";
                trow["Tax"] = "1";
                trow["Rate"] = "1";
                trow["Ammount"] = Hdrdata.Rows[0]["CUH_COM_CHG"].ToString();
                trow["MP"] = "1";
                trow["Line"] = 1;
                trow["Line2"] = 1;
                trow["Line3"] = 1;
                taxvalues.Rows.Add(trow);

            }
            if (maindata != null)
            {
                foreach (DataRow dtRow in maindata.Rows)
                {
                    if (j < maindata.Rows.Count - 1)
                    {
                        linenumber = Convert.ToInt32(maindata.Rows[j]["cui_line"]);
                        nxtlinenumber = Convert.ToInt32(maindata.Rows[j + 1]["cui_line"]);

                        if (linenumber != nxtlinenumber)
                        {
                            dr = Param.NewRow();
                            dr["Company"] = "ABL";
                            dr["User"] = "ADMIN";
                            dr["Location"] = "DMP";
                            dr["cui_itm_desc"] = maindata.Rows[j]["cui_itm_desc"].ToString() + " " + maindata.Rows[j]["cui_model"].ToString();
                            dr["cui_gross_mass"] = maindata.Rows[j]["cui_gross_mass"].ToString();
                            dr["cui_net_mass"] = maindata.Rows[j]["cui_net_mass"].ToString();
                            dr["cui_bl_no"] = maindata.Rows[j]["cui_bl_no"].ToString();
                            dr["cui_orgin_cnty"] = maindata.Rows[j]["cui_orgin_cnty"].ToString();
                            dr["cui_pkgs"] = maindata.Rows[j]["cui_pkgs"].ToString();
                            dr["Line"] = linenumber;
                            dr["MaxLine"] = maxlinenumber;
                            dr["cui_hs_cd"] = maindata.Rows[j]["cui_hs_cd"].ToString();
                            dr["cui_qty"] = maindata.Rows[j]["cui_qty"].ToString();
                            dr["cui_bal_qty1"] = maindata.Rows[j]["cui_bal_qty1"].ToString();
                            dr["cui_bal_qty2"] = maindata.Rows[j]["cui_bal_qty2"].ToString();
                            dr["cui_bal_qty3"] = maindata.Rows[j]["cui_bal_qty3"].ToString();
                            dr["cui_req_qty"] = maindata.Rows[j]["cui_req_qty"].ToString();
                            dr["cui_itm_price"] = Convert.ToDecimal(maindata.Rows[j]["cui_itm_price"].ToString());

                            Param.Rows.Add(dr);
                        }
                        if (j == maindata.Rows.Count - 2)
                        {
                            dr = Param.NewRow();
                            dr["Company"] = "ABL";
                            dr["User"] = "ADMIN";
                            dr["Location"] = "DMP";
                            dr["cui_itm_desc"] = maindata.Rows[j]["cui_itm_desc"].ToString() + " " + maindata.Rows[j]["cui_model"].ToString();
                            dr["cui_gross_mass"] = maindata.Rows[j]["cui_gross_mass"].ToString();
                            dr["cui_net_mass"] = maindata.Rows[j]["cui_net_mass"].ToString();
                            dr["cui_bl_no"] = maindata.Rows[j]["cui_bl_no"].ToString();
                            dr["cui_orgin_cnty"] = maindata.Rows[j]["cui_orgin_cnty"].ToString();
                            dr["cui_pkgs"] = maindata.Rows[j]["cui_pkgs"].ToString();
                            dr["Line"] = linenumber;
                            dr["MaxLine"] = maxlinenumber;
                            dr["cui_hs_cd"] = maindata.Rows[j]["cui_hs_cd"].ToString();
                            dr["cui_qty"] = maindata.Rows[j]["cui_qty"].ToString();
                            dr["cui_bal_qty1"] = maindata.Rows[j]["cui_bal_qty1"].ToString();
                            dr["cui_bal_qty2"] = maindata.Rows[j]["cui_bal_qty2"].ToString();
                            dr["cui_bal_qty3"] = maindata.Rows[j]["cui_bal_qty3"].ToString();
                            dr["cui_req_qty"] = maindata.Rows[j]["cui_req_qty"].ToString();
                            dr["cui_itm_price"] = Convert.ToDecimal(maindata.Rows[j]["cui_itm_price"].ToString());
                            Param.Rows.Add(dr);
                        }
                    }
                    j++;
                }
            }
            if (maindata.Rows.Count > 0)
            {

                firstrow = firsgood.NewRow();

                firstrow["cui_itm_desc"] = maindata.Rows[0]["cui_itm_desc"].ToString() + " " + maindata.Rows[0]["cui_model"].ToString();
                firstrow["cui_gross_mass"] = maindata.Rows[0]["cui_gross_mass"].ToString();
                firstrow["cui_net_mass"] = maindata.Rows[0]["cui_net_mass"].ToString();
                firstrow["cui_bl_no"] = maindata.Rows[0]["cui_bl_no"].ToString();
                firstrow["cui_orgin_cnty"] = maindata.Rows[0]["cui_orgin_cnty"].ToString();
                firstrow["cui_pkgs"] = maindata.Rows[0]["cui_pkgs"].ToString();
                firstrow["cui_hs_cd"] = maindata.Rows[0]["cui_hs_cd"].ToString();
                firstrow["cui_qty"] = maindata.Rows[0]["cui_qty"].ToString();
                firstrow["cui_bal_qty2"] = maindata.Rows[0]["cui_bal_qty2"].ToString();
                firstrow["cui_bal_qty3"] = maindata.Rows[0]["cui_bal_qty3"].ToString();
                firstrow["cui_req_qty"] = maindata.Rows[0]["cui_req_qty"].ToString();
                firstrow["cui_itm_price"] = Convert.ToDecimal(maindata.Rows[0]["cui_itm_price"].ToString());
                firstrow["cui_line"] = Convert.ToInt32(maindata.Rows[0]["cui_line"].ToString());
                firsgood.Rows.Add(firstrow);
            }
            // create second page table
            DataTable secondpagedata = new DataTable();
            DataRow srow;

            secondpagedata.Columns.Add("cui_itm_desc1", typeof(string));
            secondpagedata.Columns.Add("cui_gross_mass1", typeof(string));
            secondpagedata.Columns.Add("cui_net_mass1", typeof(string));
            secondpagedata.Columns.Add("cui_bl_no1", typeof(string));
            secondpagedata.Columns.Add("cui_orgin_cnty1", typeof(string));
            secondpagedata.Columns.Add("cui_itm_desc2", typeof(string));
            secondpagedata.Columns.Add("cui_gross_mass2", typeof(string));
            secondpagedata.Columns.Add("cui_net_mass2", typeof(string));
            secondpagedata.Columns.Add("cui_bl_no2", typeof(string));
            secondpagedata.Columns.Add("cui_orgin_cnty2", typeof(string));
            secondpagedata.Columns.Add("cui_itm_desc3", typeof(string));
            secondpagedata.Columns.Add("cui_gross_mass3", typeof(string));
            secondpagedata.Columns.Add("cui_net_mass3", typeof(string));
            secondpagedata.Columns.Add("cui_bl_no3", typeof(string));
            secondpagedata.Columns.Add("cui_orgin_cnty3", typeof(string));
            secondpagedata.Columns.Add("Line1", typeof(Int16));
            secondpagedata.Columns.Add("Line2", typeof(Int16));
            secondpagedata.Columns.Add("Line3", typeof(Int16));
            secondpagedata.Columns.Add("cui_hs_cd1", typeof(string));
            secondpagedata.Columns.Add("cui_hs_cd2", typeof(string));
            secondpagedata.Columns.Add("cui_hs_cd3", typeof(string));
            secondpagedata.Columns.Add("cui_itm_price1", typeof(decimal));
            secondpagedata.Columns.Add("cui_itm_price2", typeof(decimal));
            secondpagedata.Columns.Add("cui_itm_price3", typeof(decimal));
            secondpagedata.Columns.Add("cui_bal_qty11", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty12", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty13", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty21", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty22", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty23", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty31", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty32", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty33", typeof(string));
            int itm = 0;
            i = 1;
            if (Param.Rows.Count > 0)
            {
                for (int k = 0; k <= Param.Rows.Count; k = k + 3)
                {
                    srow = secondpagedata.NewRow();
                    if (i < Param.Rows.Count)
                    {
                        itm += 1;
                        srow["cui_itm_desc1"] = Param.Rows[i]["cui_itm_desc"].ToString();
                        srow["cui_gross_mass1"] = Param.Rows[i]["cui_gross_mass"].ToString();
                        srow["cui_net_mass1"] = Param.Rows[i]["cui_net_mass"].ToString();
                        srow["cui_bl_no1"] = Param.Rows[i]["cui_bl_no"].ToString();
                        srow["cui_orgin_cnty1"] = Param.Rows[i]["cui_orgin_cnty"].ToString();
                        srow["Line1"] = Convert.ToInt16(Param.Rows[i]["Line"]);
                        srow["cui_hs_cd1"] = Param.Rows[i]["cui_hs_cd"].ToString();
                        srow["cui_itm_price1"] = Convert.ToDecimal(Param.Rows[i]["cui_itm_price"].ToString());
                        srow["cui_bal_qty11"] = Param.Rows[i]["cui_qty"].ToString();
                        srow["cui_bal_qty21"] = Param.Rows[i]["cui_bal_qty2"].ToString();
                        srow["cui_bal_qty31"] = Param.Rows[i]["cui_bal_qty3"].ToString();
                    }

                    if (i + 1 < Param.Rows.Count)
                    {
                        itm += 1;
                        srow["cui_itm_desc2"] = Param.Rows[i + 1]["cui_itm_desc"].ToString();
                        srow["cui_gross_mass2"] = Param.Rows[i + 1]["cui_gross_mass"].ToString();
                        srow["cui_net_mass2"] = Param.Rows[i + 1]["cui_net_mass"].ToString();
                        srow["cui_bl_no2"] = Param.Rows[i + 1]["cui_bl_no"].ToString();
                        srow["cui_orgin_cnty2"] = Param.Rows[i + 1]["cui_orgin_cnty"].ToString();
                        srow["Line2"] = Convert.ToInt16(Param.Rows[i + 1]["Line"]);
                        srow["cui_hs_cd2"] = Param.Rows[i + 1]["cui_hs_cd"].ToString();
                        srow["cui_itm_price2"] = Convert.ToDecimal(Param.Rows[i + 1]["cui_itm_price"].ToString());
                        srow["cui_bal_qty12"] = Param.Rows[i + 1]["cui_qty"].ToString();
                        srow["cui_bal_qty22"] = Param.Rows[i + 1]["cui_bal_qty2"].ToString();
                        srow["cui_bal_qty32"] = Param.Rows[i + 1]["cui_bal_qty3"].ToString();
                    }

                    if (i + 2 < Param.Rows.Count)
                    {
                        itm += 1;
                        srow["cui_itm_desc3"] = Param.Rows[i + 2]["cui_itm_desc"].ToString();
                        srow["cui_gross_mass3"] = Param.Rows[i + 2]["cui_gross_mass"].ToString();
                        srow["cui_net_mass3"] = Param.Rows[i + 2]["cui_net_mass"].ToString();
                        srow["cui_bl_no3"] = Param.Rows[i + 2]["cui_bl_no"].ToString();
                        srow["cui_orgin_cnty3"] = Param.Rows[i + 2]["cui_orgin_cnty"].ToString();
                        srow["Line3"] = Convert.ToInt16(Param.Rows[i + 2]["Line"]);
                        srow["cui_hs_cd3"] = Param.Rows[i + 2]["cui_hs_cd"].ToString();
                        srow["cui_itm_price3"] = Convert.ToDecimal(Param.Rows[i + 2]["cui_itm_price"].ToString());
                        srow["cui_bal_qty13"] = Param.Rows[i + 2]["cui_qty"].ToString();
                        srow["cui_bal_qty23"] = Param.Rows[i + 2]["cui_bal_qty2"].ToString();
                        srow["cui_bal_qty33"] = Param.Rows[i + 2]["cui_bal_qty3"].ToString();
                    }

                    secondpagedata.Rows.Add(srow);
                    i = i + 3;
                }
            }

            DataTable eledata = new DataTable();
            i = 0;
            string cat = "";
            decimal fob = 0;
            decimal foblkr = 0;
            decimal faight = 0;
            decimal faightlkr = 0;
            decimal insu = 0;
            decimal insulkr = 0;
            decimal other = 0;
            decimal otherlkr = 0;
            decimal total = 0;
            decimal totallkr = 0;

            eledata = bsObj.CHNLSVC.CustService.GetCusdecElementnew(entryNo);
            foreach (DataRow ele in eledata.Rows)
            {
                cat = eledata.Rows[i]["cus_ele_cd"].ToString();

                if (cat == "OTH")
                {
                    other = Convert.ToDecimal(eledata.Rows[i]["cus_amt"]);
                    otherlkr = Convert.ToDecimal(eledata.Rows[i]["cus_amt_com"]);
                }
                if (cat == "INSU")
                {
                    insu = Convert.ToDecimal(eledata.Rows[i]["cus_amt"]);
                    insulkr = Convert.ToDecimal(eledata.Rows[i]["cus_amt_com"]);
                }
                if (cat == "FRGT")
                {
                    faight = Convert.ToDecimal(eledata.Rows[i]["cus_amt"]);
                    faightlkr = Convert.ToDecimal(eledata.Rows[i]["cus_amt_com"]);

                }
                if (cat == "COST")
                {
                    fob = Convert.ToDecimal(eledata.Rows[i]["cus_amt"]);
                    foblkr = Convert.ToDecimal(eledata.Rows[i]["cus_amt_com"]);
                }
                i++;

            }
            total = other + insu + faight + fob;
            totallkr = otherlkr + insulkr + faightlkr + foblkr;

            DataTable finalele = new DataTable();
            DataRow elerow;
            finalele.Columns.Add("FOB", typeof(decimal));
            finalele.Columns.Add("FREIGHT", typeof(decimal));
            finalele.Columns.Add("INSU", typeof(decimal));
            finalele.Columns.Add("OTHER", typeof(decimal));
            finalele.Columns.Add("TOTAL", typeof(decimal));
            finalele.Columns.Add("TOTALLKR", typeof(decimal));

            elerow = finalele.NewRow();
            elerow["FOB"] = fob;
            elerow["FREIGHT"] = faight;
            elerow["INSU"] = insu;
            elerow["OTHER"] = other;
            elerow["TOTAL"] = total;
            elerow["TOTALLKR"] = totallkr;

            finalele.Rows.Add(elerow);

            DataTable cusdeccommon = bsObj.CHNLSVC.CustService.GetCusdecCommonNew("LK");



            List<ImportsBLContainer> blContainers = new List<ImportsBLContainer>();
            if (Hdrdata != null)
            {
                blContainers = bsObj.CHNLSVC.Financial.GetContainers(Hdrdata.Rows[0]["cuh_sun_req_no"].ToString());
            }
            else
            {
                blContainers = null;
            }

            DataTable page = new DataTable();
            DataRow pagerow;
            page.Columns.Add("Totalitems", typeof(Int32));
            pagerow = page.NewRow();
            pagerow["Totalitems"] = itm + 1;
            page.Rows.Add(pagerow);

            _cusdec_Tobond2.Database.Tables["firstGooddata"].SetDataSource(firsgood);
            //  _goods_declaration.Database.Tables["TaxValues"].SetDataSource(taxvalues);
            _cusdec_Tobond2.Database.Tables["HdrData"].SetDataSource(Hdrdata);

            int r = 0;
            foreach (DataRow secondpagedatanew in secondpagedata.Rows)
            {
                if (secondpagedata.Rows[r]["Line1"].ToString() == "") secondpagedata.Rows[r]["Line1"] = 0;
                if (secondpagedata.Rows[r]["Line2"].ToString() == "") secondpagedata.Rows[r]["Line2"] = 0;
                if (secondpagedata.Rows[r]["Line3"].ToString() == "") secondpagedata.Rows[r]["Line3"] = 0;
                r++;
            }

            _cusdec_Tobond2.Database.Tables["SecondPage"].SetDataSource(secondpagedata);
            _cusdec_Tobond2.Database.Tables["EleData"].SetDataSource(finalele);
            //  _goods_declaration.Database.Tables["Param"].SetDataSource(Param);
            _cusdec_Tobond2.Database.Tables["CusdecCommon"].SetDataSource(cusdeccommon);
            _cusdec_Tobond2.Database.Tables["Page"].SetDataSource(page);

            foreach (object repOp in _cusdec_Tobond2.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "firstpagetax")
                    {
                        ReportDocument subRepDoc = _cusdec_Tobond2.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["TaxValues"].SetDataSource(taxvalues);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "finaltaxvalues")
                    {
                        ReportDocument subRepDoc = _cusdec_Tobond2.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["TaxValues"].SetDataSource(taxvalues);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "finalrighttax")
                    {
                        ReportDocument subRepDoc = _cusdec_Tobond2.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["TaxValues"].SetDataSource(taxvalues);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "finalbottomtax")
                    {
                        ReportDocument subRepDoc = _cusdec_Tobond2.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["TaxValues"].SetDataSource(taxvalues);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "TotalTaxsummery")
                    {
                        ReportDocument subRepDoc = _cusdec_Tobond2.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["TaxValues"].SetDataSource(taxvalues);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "secondpageitemdetails")
                    {
                        ReportDocument subRepDoc = _cusdec_Tobond2.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["Param"].SetDataSource(Param);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "containers")
                    {
                        ReportDocument subRepDoc = _cusdec_Tobond2.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["Containers"].SetDataSource(blContainers);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    _cs.Dispose();
                }
            }
        }

        public void CusdecAssessment3(string entryno, string loc, string com, string user)
        {
            DataTable Param = new DataTable();
            DataRow dr;
            decimal CIF = 0;
            decimal CID = 0;
            decimal EIC = 0;
            decimal PAL = 0;
            decimal VAT = 0;
            decimal XID = 0;
            decimal CES = 0;
            decimal NBT = 0;

            DateTime dt = DateTime.Now.Date;
            Int32 lineno = 0;
            decimal diffamount = 0;
            DateTime latestdate = DateTime.Now.Date;
            decimal ammount = 0;
            string fileno = "";
            string assno = "";
            decimal cif2 = 0;

            Param.Columns.Add("Company", typeof(string));
            Param.Columns.Add("User", typeof(string));
            Param.Columns.Add("Location", typeof(string));
            Param.Columns.Add("isth_doc_no", typeof(string));
            Param.Columns.Add("isth_dt", typeof(DateTime));
            Param.Columns.Add("istd_line_no", typeof(Int32));
            Param.Columns.Add("istd_entry_no", typeof(string));
            Param.Columns.Add("CIF", typeof(decimal));
            Param.Columns.Add("CID", typeof(decimal));
            Param.Columns.Add("EIC", typeof(decimal));
            Param.Columns.Add("PAL", typeof(decimal));
            Param.Columns.Add("VAT", typeof(decimal));
            Param.Columns.Add("XID", typeof(decimal));
            Param.Columns.Add("CE&S", typeof(decimal));
            Param.Columns.Add("NBT", typeof(decimal));
            Param.Columns.Add("istd_diff_amt", typeof(decimal));
            Param.Columns.Add("istd_cost_stl_amt", typeof(decimal));
            Param.Columns.Add("LatestDate", typeof(DateTime));
            Param.Columns.Add("cuh_file_no", typeof(string));
            Param.Columns.Add("istd_assess_no", typeof(string));
            Param.Columns.Add("cif2", typeof(decimal));
            Param.Columns.Add("cuh_cusdec_entry_no", typeof(string));
            Param.Columns.Add("cuh_office_of_entry", typeof(string));
            Param.Columns.Add("istd_cost_claim_amt", typeof(decimal));
            Param.Columns.Add("istd_cost_unclaim_amt", typeof(decimal));
            Param.Columns.Add("ASTDOC", typeof(string));

            DataTable maindata = bsObj.CHNLSVC.CustService.GetCusdecAssessmentAccountData(entryno, com);
            int i = 0;

            if (maindata.Rows.Count > 0)
            {

                foreach (DataRow dtRow in maindata.Rows)
                {

                    string docno = maindata.Rows[i]["istd_entry_no"].ToString();
                    if (i + 1 < maindata.Rows.Count)
                    {
                        dr = Param.NewRow();
                        if (docno == maindata.Rows[i + 1]["istd_entry_no"].ToString())
                        {

                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "CIF")
                            {
                                CIF = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "CID")
                            {
                                CID = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "EIC")
                            {
                                EIC = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "PAL")
                            {
                                PAL = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "VAT")
                            {
                                VAT = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "XID")
                            {
                                XID = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "CE&S")
                            {
                                CES = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "NBT")
                            {
                                NBT = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            docno = maindata.Rows[i]["istd_entry_no"].ToString();
                            dt = Convert.ToDateTime(maindata.Rows[i]["ishd_stl_dt"].ToString());
                            lineno = Convert.ToInt32(maindata.Rows[i]["istd_line_no"].ToString());
                            diffamount = diffamount + Convert.ToDecimal(maindata.Rows[i]["istd_diff_amt"].ToString());
                            if (maindata.Rows[i]["cif"].ToString() == "")
                            {
                                cif2 = 0;
                            }
                            else
                            {
                                cif2 = Convert.ToDecimal(maindata.Rows[i]["cif"].ToString());
                            }


                            if (maindata.Rows[i]["cuh_cusdec_entry_dt"].ToString() != "")
                                latestdate = Convert.ToDateTime(maindata.Rows[i]["cuh_cusdec_entry_dt"].ToString());


                            ammount = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            fileno = maindata.Rows[i]["cuh_file_no"].ToString();
                            assno = maindata.Rows[i]["istd_assess_no"].ToString();

                            dr["Company"] = com;
                            dr["User"] = user;
                            dr["Location"] = loc;
                            dr["isth_doc_no"] = entryno;
                            dr["isth_dt"] = dt;
                            dr["istd_line_no"] = lineno;
                            dr["istd_entry_no"] = docno;
                            dr["CIF"] = CIF;
                            dr["CID"] = CID;
                            dr["EIC"] = EIC;
                            dr["PAL"] = PAL;
                            dr["VAT"] = VAT;
                            dr["XID"] = XID;
                            dr["CE&S"] = CES;
                            dr["NBT"] = NBT;
                            dr["istd_diff_amt"] = diffamount;
                            dr["istd_cost_stl_amt"] = ammount;
                            dr["LatestDate"] = latestdate;
                            dr["cuh_file_no"] = fileno;
                            dr["istd_assess_no"] = assno;
                            dr["cif2"] = cif2;
                            dr["cuh_cusdec_entry_no"] = maindata.Rows[i]["cuh_cusdec_entry_no"].ToString();
                            dr["cuh_office_of_entry"] = maindata.Rows[i]["cuh_office_of_entry"].ToString();
                            dr["istd_cost_claim_amt"] = Convert.ToDecimal(maindata.Rows[i]["istd_cost_claim_amt"].ToString());
                            dr["istd_cost_unclaim_amt"] = Convert.ToDecimal(maindata.Rows[i]["istd_cost_unclaim_amt"].ToString());
                            dr["ASTDOC"] = maindata.Rows[i]["ASTDOC"].ToString();

                        }
                        else
                        {
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "CIF")
                            {
                                CIF = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "CID")
                            {
                                CID = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "EIC")
                            {
                                EIC = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "PAL")
                            {
                                PAL = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "VAT")
                            {
                                VAT = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "XID")
                            {
                                XID = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "CE&S")
                            {
                                CES = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            if (maindata.Rows[i]["istd_cost_ele"].ToString() == "NBT")
                            {
                                NBT = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            }
                            docno = maindata.Rows[i]["istd_entry_no"].ToString();
                            dt = Convert.ToDateTime(maindata.Rows[i]["ishd_stl_dt"].ToString());
                            lineno = Convert.ToInt32(maindata.Rows[i]["istd_line_no"].ToString());
                            diffamount = diffamount + Convert.ToDecimal(maindata.Rows[i]["istd_diff_amt"].ToString());
                            if (maindata.Rows[i]["cif"].ToString() != "")
                            {
                                cif2 = Convert.ToDecimal(maindata.Rows[i]["cif"].ToString());
                            }


                            if (maindata.Rows[i]["cuh_cusdec_entry_dt"].ToString() != "")
                                latestdate = Convert.ToDateTime(maindata.Rows[i]["cuh_cusdec_entry_dt"].ToString());


                            ammount = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                            fileno = maindata.Rows[i]["cuh_file_no"].ToString();
                            assno = maindata.Rows[i]["istd_assess_no"].ToString();

                            dr["Company"] = com;
                            dr["User"] = user;
                            dr["Location"] = loc;
                            dr["isth_doc_no"] = entryno;
                            dr["isth_dt"] = dt;
                            dr["istd_line_no"] = lineno;
                            dr["istd_entry_no"] = docno;
                            dr["CIF"] = CIF;
                            dr["CID"] = CID;
                            dr["EIC"] = EIC;
                            dr["PAL"] = PAL;
                            dr["VAT"] = VAT;
                            dr["XID"] = XID;
                            dr["CE&S"] = CES;
                            dr["NBT"] = NBT;
                            dr["istd_diff_amt"] = diffamount;
                            dr["istd_cost_stl_amt"] = ammount;
                            dr["LatestDate"] = latestdate;
                            dr["cuh_file_no"] = fileno;
                            dr["istd_assess_no"] = assno;
                            dr["cif2"] = cif2;
                            dr["cuh_cusdec_entry_no"] = maindata.Rows[i]["cuh_cusdec_entry_no"].ToString();
                            dr["cuh_office_of_entry"] = maindata.Rows[i]["cuh_office_of_entry"].ToString();
                            dr["istd_cost_claim_amt"] = Convert.ToDecimal(maindata.Rows[i]["istd_cost_claim_amt"].ToString());
                            dr["istd_cost_unclaim_amt"] = Convert.ToDecimal(maindata.Rows[i]["istd_cost_unclaim_amt"].ToString());
                            dr["ASTDOC"] = maindata.Rows[i]["ASTDOC"].ToString();

                            Param.Rows.Add(dr);
                            diffamount = 0;
                            CIF = 0;
                            CID = 0;
                            EIC = 0;
                            PAL = 0;
                            VAT = 0;
                            XID = 0;
                            CES = 0;
                            NBT = 0;
                            cif2 = 0;
                        }
                    }

                    if (i + 1 == maindata.Rows.Count)
                    {
                        dr = Param.NewRow();
                        if (maindata.Rows[i]["istd_cost_ele"].ToString() == "CIF")
                        {
                            CIF = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                        }
                        if (maindata.Rows[i]["istd_cost_ele"].ToString() == "CID")
                        {
                            CID = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                        }
                        if (maindata.Rows[i]["istd_cost_ele"].ToString() == "EIC")
                        {
                            EIC = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                        }
                        if (maindata.Rows[i]["istd_cost_ele"].ToString() == "PAL")
                        {
                            PAL = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                        }
                        if (maindata.Rows[i]["istd_cost_ele"].ToString() == "VAT")
                        {
                            VAT = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                        }
                        if (maindata.Rows[i]["istd_cost_ele"].ToString() == "XID")
                        {
                            XID = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                        }
                        if (maindata.Rows[i]["istd_cost_ele"].ToString() == "CE&S")
                        {
                            CES = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                        }
                        if (maindata.Rows[i]["istd_cost_ele"].ToString() == "NBT")
                        {
                            NBT = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                        }
                        docno = maindata.Rows[i]["istd_entry_no"].ToString();
                        dt = Convert.ToDateTime(maindata.Rows[i]["ishd_stl_dt"].ToString());
                        lineno = Convert.ToInt32(maindata.Rows[i]["istd_line_no"].ToString());
                        diffamount = diffamount + Convert.ToDecimal(maindata.Rows[i]["istd_diff_amt"].ToString());
                        cif2 = Convert.ToDecimal(maindata.Rows[i]["cif"].ToString());

                        if (maindata.Rows[i]["cuh_cusdec_entry_dt"].ToString() != "")
                            latestdate = Convert.ToDateTime(maindata.Rows[i]["cuh_cusdec_entry_dt"].ToString());


                        ammount = Convert.ToDecimal(maindata.Rows[i]["istd_cost_stl_amt"].ToString());
                        fileno = maindata.Rows[i]["cuh_file_no"].ToString();
                        assno = maindata.Rows[i]["istd_assess_no"].ToString();
                        dr["Company"] = com;
                        dr["User"] = user;
                        dr["Location"] = loc;
                        dr["isth_doc_no"] = entryno;
                        dr["isth_dt"] = dt;
                        dr["istd_line_no"] = lineno;
                        dr["istd_entry_no"] = docno;
                        dr["CIF"] = CIF;
                        dr["CID"] = CID;
                        dr["EIC"] = EIC;
                        dr["PAL"] = PAL;
                        dr["VAT"] = VAT;
                        dr["XID"] = XID;
                        dr["CE&S"] = CES;
                        dr["NBT"] = NBT;
                        dr["istd_diff_amt"] = diffamount;
                        dr["istd_cost_stl_amt"] = ammount;
                        dr["LatestDate"] = latestdate;
                        dr["cuh_file_no"] = fileno;
                        dr["istd_assess_no"] = assno;
                        dr["cif2"] = cif2;
                        dr["cuh_cusdec_entry_no"] = maindata.Rows[i]["cuh_cusdec_entry_no"].ToString();
                        dr["cuh_office_of_entry"] = maindata.Rows[i]["cuh_office_of_entry"].ToString();
                        dr["istd_cost_claim_amt"] = Convert.ToDecimal(maindata.Rows[i]["istd_cost_claim_amt"].ToString());
                        dr["istd_cost_unclaim_amt"] = Convert.ToDecimal(maindata.Rows[i]["istd_cost_unclaim_amt"].ToString());
                        dr["ASTDOC"] = maindata.Rows[i]["ASTDOC"].ToString();

                        Param.Rows.Add(dr);
                        CIF = 0;
                        CID = 0;
                        EIC = 0;
                        PAL = 0;
                        VAT = 0;
                        XID = 0;
                        CES = 0;
                        NBT = 0;
                        cif2 = 0;
                    }


                    i++;
                }


            }

            _cusdec_assessment3.Database.Tables["Param"].SetDataSource(Param);

        }

        public void REBOND2(string entryNo, string com)
        {
            DataTable Hdrdata = new DataTable();
            DataTable maindata = new DataTable();
            DataTable Param = new DataTable();


            DataRow dr;
            int i = 0;
            int j = 0;

            int linenumber = 0;
            int nxtlinenumber = 0;
            int maxlinenumber = 0;

            Param.Columns.Add("Company", typeof(string));
            Param.Columns.Add("User", typeof(string));
            Param.Columns.Add("Location", typeof(string));
            Param.Columns.Add("cui_itm_desc", typeof(string));
            Param.Columns.Add("cui_gross_mass", typeof(string));
            Param.Columns.Add("cui_net_mass", typeof(string));
            Param.Columns.Add("cui_bl_no", typeof(string));
            Param.Columns.Add("cui_orgin_cnty", typeof(string));
            Param.Columns.Add("cui_pkgs", typeof(string));
            Param.Columns.Add("Line", typeof(int));
            Param.Columns.Add("MaxLine", typeof(int));
            Param.Columns.Add("cui_hs_cd", typeof(string));
            Param.Columns.Add("cui_itm_price", typeof(decimal));
            Param.Columns.Add("cui_qty", typeof(string));
            Param.Columns.Add("cui_bal_qty1", typeof(string));
            Param.Columns.Add("cui_bal_qty2", typeof(string));
            Param.Columns.Add("cui_bal_qty3", typeof(string));
            Param.Columns.Add("cui_req_qty", typeof(string));


            //tax values
            DataTable taxvalues = new DataTable();
            DataRow trow;

            taxvalues.Columns.Add("Type", typeof(string));
            taxvalues.Columns.Add("Tax", typeof(decimal));
            taxvalues.Columns.Add("Rate", typeof(decimal));
            taxvalues.Columns.Add("Ammount", typeof(decimal));
            taxvalues.Columns.Add("MP", typeof(decimal));
            taxvalues.Columns.Add("Line", typeof(int));
            taxvalues.Columns.Add("Line2", typeof(int));
            taxvalues.Columns.Add("Line3", typeof(int));
            taxvalues.Columns.Add("cui_itm_desc", typeof(string));
            taxvalues.Columns.Add("cui_gross_mass", typeof(string));
            taxvalues.Columns.Add("cui_net_mass", typeof(string));
            taxvalues.Columns.Add("cui_bl_no", typeof(string));
            taxvalues.Columns.Add("cui_orgin_cnty", typeof(string));
            taxvalues.Columns.Add("cui_pkgs", typeof(string));

            DataTable firsgood = new DataTable();
            DataRow firstrow;

            firsgood.Columns.Add("cui_itm_desc", typeof(string));
            firsgood.Columns.Add("cui_gross_mass", typeof(string));
            firsgood.Columns.Add("cui_net_mass", typeof(string));
            firsgood.Columns.Add("cui_bl_no", typeof(string));
            firsgood.Columns.Add("cui_orgin_cnty", typeof(string));
            firsgood.Columns.Add("cui_pkgs", typeof(string));
            firsgood.Columns.Add("cui_hs_cd", typeof(string));
            firsgood.Columns.Add("cui_qty", typeof(string));
            firsgood.Columns.Add("cui_bal_qty2", typeof(string));
            firsgood.Columns.Add("cui_bal_qty3", typeof(string));
            firsgood.Columns.Add("cui_req_qty", typeof(string));
            firsgood.Columns.Add("cui_itm_price", typeof(decimal));
            firsgood.Columns.Add("cui_line", typeof(Int32));

            int licount = 1;
            // all goods data
            // maindata = bsObj.CHNLSVC.CustService.GetGoodsDeclarationDetails(entryNo);
            //header data
            Hdrdata = bsObj.CHNLSVC.CustService.GetCusdecHDRData(entryNo, com);
            //DataView dv = maindata.DefaultView;
            //dv.Sort = "cui_hs_cd ,cui_model, cuic_itm_cd ,cui_line";
            //maindata = dv.ToTable();
            string sunbondno = Hdrdata.Rows[0]["cuh_sun_bond_no"].ToString();

            List<Cusdec_Goods_decl> maindatanew1 = bsObj.CHNLSVC.CustService.GetGoodsDeclarationDetailsList(sunbondno);
            //Update other doc line
            foreach (var updateotherlist in maindatanew1)
            {
                //getotherline
                List<Cusdec_Goods_decl> maindatanew11 = maindatanew1.Where(a => a.cui_line == updateotherlist.cui_line && a.cuic_itm_cd == updateotherlist.cuic_itm_cd).ToList();
                Int32 upline = maindatanew11.FirstOrDefault().cui_oth_doc_line;

                int effect = bsObj.CHNLSVC.Financial.UpdateOtherdocline(entryNo, updateotherlist.cui_line, updateotherlist.cuic_itm_cd, upline);
            }

            List<Cusdec_Goods_decl> maindatanew = bsObj.CHNLSVC.CustService.GetGoodsDeclarationDetailsList(entryNo);
            List<Cusdec_Goods_decl> resultnew = maindatanew
         .GroupBy(l => new { l.cui_oth_doc_line, l.cuic_ele_cd })
         .Select(cl => new Cusdec_Goods_decl
         {
             cui_line = cl.First().cui_line,
             cui_itm_desc = cl.First().cui_itm_desc,
             cui_gross_mass = cl.Sum(c => c.cui_gross_mass),
             cui_net_mass = cl.Sum(c => c.cui_net_mass),
             cui_bl_no = cl.First().cui_bl_no,
             cui_orgin_cnty = cl.First().cui_orgin_cnty,
             cui_pkgs = cl.First().cui_pkgs,
             cui_hs_cd = cl.First().cui_hs_cd,
             cui_qty = cl.Sum(c => c.cui_qty),
             cui_bal_qty1 = cl.Sum(c => c.cui_bal_qty1),
             cui_bal_qty2 = cl.Sum(c => c.cui_bal_qty2),
             cui_bal_qty3 = cl.Sum(c => c.cui_bal_qty3),
             cui_req_qty = cl.Sum(c => c.cui_req_qty),
             cui_itm_price = cl.Sum(c => c.cui_itm_price),
             cuic_ele_cd = cl.First().cuic_ele_cd,
             cuic_ele_base = cl.First().cuic_ele_base,
             cuic_ele_rt = cl.First().cuic_ele_rt,
             cuic_ele_amt = cl.Sum(c => c.cuic_ele_amt),
             cuic_ele_mp = cl.First().cuic_ele_mp,
             cui_model = cl.First().cui_model,
             cui_itm_price2 = cl.Sum(c => c.cui_itm_price2),
             cui_oth_doc_line = cl.First().cui_oth_doc_line


         }).ToList();

            List<Cusdec_Goods_decl> resultnewgetcount = maindatanew
        .GroupBy(l => new { l.cui_oth_doc_line })
        .Select(cl => new Cusdec_Goods_decl
        {
            cui_line = cl.First().cui_line,
            cui_itm_desc = cl.First().cui_itm_desc,
            cui_gross_mass = cl.Sum(c => c.cui_gross_mass),
            cui_net_mass = cl.Sum(c => c.cui_net_mass),
            cui_bl_no = cl.First().cui_bl_no,
            cui_orgin_cnty = cl.First().cui_orgin_cnty,
            cui_pkgs = cl.First().cui_pkgs,
            cui_hs_cd = cl.First().cui_hs_cd,
            cui_qty = cl.Sum(c => c.cui_qty),
            cui_bal_qty1 = cl.Sum(c => c.cui_bal_qty1),
            cui_bal_qty2 = cl.Sum(c => c.cui_bal_qty2),
            cui_bal_qty3 = cl.Sum(c => c.cui_bal_qty3),
            cui_req_qty = cl.Sum(c => c.cui_req_qty),
            cui_itm_price = cl.Sum(c => c.cui_itm_price),
            cuic_ele_cd = cl.First().cuic_ele_cd,
            cuic_ele_base = cl.First().cuic_ele_base,
            cuic_ele_rt = cl.First().cuic_ele_rt,
            cuic_ele_amt = cl.Sum(c => c.cuic_ele_amt),
            cuic_ele_mp = cl.First().cuic_ele_mp,
            cui_model = cl.First().cui_model,
            cui_itm_price2 = cl.Sum(c => c.cui_itm_price2),
            cui_oth_doc_line = cl.First().cui_oth_doc_line


        }).ToList();

            resultnew = resultnew.OrderBy(A => A.cui_oth_doc_line).ToList();

            var maxcount = resultnewgetcount.Count;

            foreach (var resultnewww in resultnew)
            {
                string itemsdes = "";
                string hscode = resultnewww.cui_hs_cd.ToString();
                string dutyele = resultnewww.cuic_ele_cd;
                decimal unitrateneww = resultnewww.cui_itm_price2 / resultnewww.cui_qty;

                foreach (var maindatanewww in maindatanew)
                {
                    if (maindatanewww.cui_hs_cd == hscode && maindatanewww.cui_unit_rt == unitrateneww && maindatanewww.cuic_ele_cd == dutyele)
                    {
                        itemsdes = itemsdes + "  " + maindatanewww.cui_qty + " U" + "  " + maindatanewww.cui_itm_desc + " " + maindatanewww.cui_model;
                    }
                }
                resultnewww.cui_itm_desc = itemsdes;

            }

            IEnumerable<Cusdec_Goods_decl> resultnew2 = resultnew;
            DataTable table = new DataTable();
            using (var reader = ObjectReader.Create(resultnew2))
            {
                table.Load(reader);
            }

            maindata = table;



            if (maindata.Rows.Count > 0)
            {
                foreach (DataRow dtRow in maindata.Rows)
                {
                    int nxtline;
                    linenumber = Convert.ToInt32(maindata.Rows[i]["cui_line"]);
                    if (i + 1 < maindata.Rows.Count)
                    {
                        nxtline = Convert.ToInt32(maindata.Rows[i + 1]["cui_line"]);
                    }
                    else
                    {
                        nxtline = 99999;
                    }



                    if (linenumber != nxtline)
                    {
                        trow = taxvalues.NewRow();
                        trow["Type"] = maindata.Rows[i]["cuic_ele_cd"].ToString();
                        trow["Tax"] = maindata.Rows[i]["cuic_ele_base"].ToString();
                        trow["Rate"] = maindata.Rows[i]["cuic_ele_rt"].ToString();
                        trow["Ammount"] = maindata.Rows[i]["cuic_ele_amt"].ToString();
                        trow["MP"] = maindata.Rows[i]["cuic_ele_mp"].ToString();
                        trow["Line"] = Convert.ToInt32(maindata.Rows[i]["cui_oth_doc_line"].ToString());
                        trow["Line2"] = Convert.ToInt32(maindata.Rows[i]["cui_oth_doc_line"].ToString());
                        trow["Line3"] = Convert.ToInt32(maindata.Rows[i]["cui_oth_doc_line"].ToString());
                        trow["cui_itm_desc"] = maindata.Rows[i]["cui_itm_desc"].ToString() + " ";
                        trow["cui_gross_mass"] = maindata.Rows[i]["cui_gross_mass"].ToString();
                        trow["cui_net_mass"] = maindata.Rows[i]["cui_net_mass"].ToString();
                        trow["cui_bl_no"] = maindata.Rows[i]["cui_bl_no"].ToString();
                        trow["cui_orgin_cnty"] = maindata.Rows[i]["cui_orgin_cnty"].ToString();
                        trow["cui_pkgs"] = maindata.Rows[i]["cui_pkgs"].ToString();

                        taxvalues.Rows.Add(trow);
                        maxlinenumber = linenumber;
                        i++;
                        licount++;
                    }
                    else
                    {
                        trow = taxvalues.NewRow();
                        trow["Type"] = maindata.Rows[i]["cuic_ele_cd"].ToString();
                        trow["Tax"] = maindata.Rows[i]["cuic_ele_base"].ToString();
                        trow["Rate"] = maindata.Rows[i]["cuic_ele_rt"].ToString();
                        trow["Ammount"] = maindata.Rows[i]["cuic_ele_amt"].ToString();
                        trow["MP"] = maindata.Rows[i]["cuic_ele_mp"].ToString();
                        trow["Line"] = Convert.ToInt32(maindata.Rows[i]["cui_oth_doc_line"].ToString());
                        trow["Line2"] = Convert.ToInt32(maindata.Rows[i]["cui_oth_doc_line"].ToString());
                        trow["Line3"] = Convert.ToInt32(maindata.Rows[i]["cui_oth_doc_line"].ToString());
                        trow["cui_itm_desc"] = maindata.Rows[i]["cui_itm_desc"].ToString() + " ";
                        trow["cui_gross_mass"] = maindata.Rows[i]["cui_gross_mass"].ToString();
                        trow["cui_net_mass"] = maindata.Rows[i]["cui_net_mass"].ToString();
                        trow["cui_bl_no"] = maindata.Rows[i]["cui_bl_no"].ToString();
                        trow["cui_orgin_cnty"] = maindata.Rows[i]["cui_orgin_cnty"].ToString();
                        trow["cui_pkgs"] = maindata.Rows[i]["cui_pkgs"].ToString();

                        taxvalues.Rows.Add(trow);
                        maxlinenumber = linenumber;
                        i++;
                    }

                }
            }

            if (Hdrdata.Rows.Count > 0)
            {
                trow = taxvalues.NewRow();
                trow["Type"] = "CE&S";
                trow["Tax"] = "1";
                trow["Rate"] = "1";
                trow["Ammount"] = Hdrdata.Rows[0]["CUH_COM_CHG"].ToString();
                trow["MP"] = "1";
                trow["Line"] = 1;
                trow["Line2"] = 1;
                trow["Line3"] = 1;
                taxvalues.Rows.Add(trow);

            }
            if (maindata.Rows.Count > 0)
            {
                foreach (DataRow dtRow in maindata.Rows)
                {
                    if (j < maindata.Rows.Count - 1)
                    {
                        linenumber = Convert.ToInt32(maindata.Rows[j]["cui_line"]);
                        nxtlinenumber = Convert.ToInt32(maindata.Rows[j + 1]["cui_line"]);

                        if (linenumber != nxtlinenumber)
                        {
                            dr = Param.NewRow();
                            dr["Company"] = "ABL";
                            dr["User"] = "ADMIN";
                            dr["Location"] = "DMP";
                            dr["cui_itm_desc"] = maindata.Rows[j]["cui_itm_desc"].ToString() + " ";
                            dr["cui_gross_mass"] = maindata.Rows[j]["cui_gross_mass"].ToString();
                            dr["cui_net_mass"] = maindata.Rows[j]["cui_net_mass"].ToString();
                            dr["cui_bl_no"] = maindata.Rows[j]["cui_bl_no"].ToString();
                            dr["cui_orgin_cnty"] = maindata.Rows[j]["cui_orgin_cnty"].ToString();
                            dr["cui_pkgs"] = maindata.Rows[j]["cui_pkgs"].ToString();
                            dr["Line"] = Convert.ToInt32(maindata.Rows[j]["cui_oth_doc_line"].ToString());
                            dr["MaxLine"] = maxlinenumber;
                            dr["cui_hs_cd"] = maindata.Rows[j]["cui_hs_cd"].ToString();
                            dr["cui_qty"] = maindata.Rows[j]["cui_qty"].ToString();
                            dr["cui_bal_qty1"] = maindata.Rows[j]["cui_bal_qty1"].ToString();
                            dr["cui_bal_qty2"] = maindata.Rows[j]["cui_bal_qty2"].ToString();
                            dr["cui_bal_qty3"] = maindata.Rows[j]["cui_bal_qty3"].ToString();
                            dr["cui_req_qty"] = maindata.Rows[j]["cui_req_qty"].ToString();
                            dr["cui_itm_price"] = Convert.ToDecimal(maindata.Rows[j]["cui_itm_price"].ToString());

                            Param.Rows.Add(dr);
                        }
                        if (j == maindata.Rows.Count - 2)
                        {
                            dr = Param.NewRow();
                            dr["Company"] = "ABL";
                            dr["User"] = "ADMIN";
                            dr["Location"] = "DMP";
                            dr["cui_itm_desc"] = maindata.Rows[j]["cui_itm_desc"].ToString() + " ";
                            dr["cui_gross_mass"] = maindata.Rows[j]["cui_gross_mass"].ToString();
                            dr["cui_net_mass"] = maindata.Rows[j]["cui_net_mass"].ToString();
                            dr["cui_bl_no"] = maindata.Rows[j]["cui_bl_no"].ToString();
                            dr["cui_orgin_cnty"] = maindata.Rows[j]["cui_orgin_cnty"].ToString();
                            dr["cui_pkgs"] = maindata.Rows[j]["cui_pkgs"].ToString();
                            dr["Line"] = Convert.ToInt32(maindata.Rows[j]["cui_oth_doc_line"].ToString());
                            dr["MaxLine"] = maxlinenumber;
                            dr["cui_hs_cd"] = maindata.Rows[j]["cui_hs_cd"].ToString();
                            dr["cui_qty"] = maindata.Rows[j]["cui_qty"].ToString();
                            dr["cui_bal_qty1"] = maindata.Rows[j]["cui_bal_qty1"].ToString();
                            dr["cui_bal_qty2"] = maindata.Rows[j]["cui_bal_qty2"].ToString();
                            dr["cui_bal_qty3"] = maindata.Rows[j]["cui_bal_qty3"].ToString();
                            dr["cui_req_qty"] = maindata.Rows[j]["cui_req_qty"].ToString();
                            dr["cui_itm_price"] = Convert.ToDecimal(maindata.Rows[j]["cui_itm_price"].ToString());
                            Param.Rows.Add(dr);
                        }
                    }
                    j++;
                }
            }
            int rebond2maxitm = 0;
            if (maindata.Rows.Count > 0)
            {

                firstrow = firsgood.NewRow();

                firstrow["cui_itm_desc"] = maindata.Rows[0]["cui_itm_desc"].ToString() + " ";
                firstrow["cui_gross_mass"] = maindata.Rows[0]["cui_gross_mass"].ToString();
                firstrow["cui_net_mass"] = maindata.Rows[0]["cui_net_mass"].ToString();
                firstrow["cui_bl_no"] = maindata.Rows[0]["cui_bl_no"].ToString();
                firstrow["cui_orgin_cnty"] = maindata.Rows[0]["cui_orgin_cnty"].ToString();
                firstrow["cui_pkgs"] = maindata.Rows[0]["cui_pkgs"].ToString();
                firstrow["cui_hs_cd"] = maindata.Rows[0]["cui_hs_cd"].ToString();
                firstrow["cui_qty"] = maindata.Rows[0]["cui_qty"].ToString();
                firstrow["cui_bal_qty2"] = maindata.Rows[0]["cui_bal_qty2"].ToString();
                firstrow["cui_bal_qty3"] = maindata.Rows[0]["cui_bal_qty3"].ToString();
                firstrow["cui_req_qty"] = maindata.Rows[0]["cui_req_qty"].ToString();
                firstrow["cui_itm_price"] = Convert.ToDecimal(maindata.Rows[0]["cui_itm_price"].ToString());
                firstrow["cui_line"] = Convert.ToInt32(maindata.Rows[0]["cui_oth_doc_line"].ToString());
                firsgood.Rows.Add(firstrow);
                rebond2maxitm++;
            }
            // create second page table
            DataTable secondpagedata = new DataTable();
            DataRow srow;

            secondpagedata.Columns.Add("cui_itm_desc1", typeof(string));
            secondpagedata.Columns.Add("cui_gross_mass1", typeof(string));
            secondpagedata.Columns.Add("cui_net_mass1", typeof(string));
            secondpagedata.Columns.Add("cui_bl_no1", typeof(string));
            secondpagedata.Columns.Add("cui_orgin_cnty1", typeof(string));
            secondpagedata.Columns.Add("cui_itm_desc2", typeof(string));
            secondpagedata.Columns.Add("cui_gross_mass2", typeof(string));
            secondpagedata.Columns.Add("cui_net_mass2", typeof(string));
            secondpagedata.Columns.Add("cui_bl_no2", typeof(string));
            secondpagedata.Columns.Add("cui_orgin_cnty2", typeof(string));
            secondpagedata.Columns.Add("cui_itm_desc3", typeof(string));
            secondpagedata.Columns.Add("cui_gross_mass3", typeof(string));
            secondpagedata.Columns.Add("cui_net_mass3", typeof(string));
            secondpagedata.Columns.Add("cui_bl_no3", typeof(string));
            secondpagedata.Columns.Add("cui_orgin_cnty3", typeof(string));
            secondpagedata.Columns.Add("Line1", typeof(Int16));
            secondpagedata.Columns.Add("Line2", typeof(Int16));
            secondpagedata.Columns.Add("Line3", typeof(Int16));
            secondpagedata.Columns.Add("LineCount1", typeof(Int16));
            secondpagedata.Columns.Add("LineCount2", typeof(Int16));
            secondpagedata.Columns.Add("LineCount3", typeof(Int16));
            secondpagedata.Columns.Add("cui_hs_cd1", typeof(string));
            secondpagedata.Columns.Add("cui_hs_cd2", typeof(string));
            secondpagedata.Columns.Add("cui_hs_cd3", typeof(string));
            secondpagedata.Columns.Add("cui_itm_price1", typeof(decimal));
            secondpagedata.Columns.Add("cui_itm_price2", typeof(decimal));
            secondpagedata.Columns.Add("cui_itm_price3", typeof(decimal));
            secondpagedata.Columns.Add("cui_bal_qty11", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty12", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty13", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty21", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty22", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty23", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty31", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty32", typeof(string));
            secondpagedata.Columns.Add("cui_bal_qty33", typeof(string));
            int itm = 0;
            i = 1;
            if (Param.Rows.Count > 0)
            {
                for (int k = 0; k <= Param.Rows.Count; k = k + 3)
                {
                    srow = secondpagedata.NewRow();
                    if (i < Param.Rows.Count)
                    {
                        itm += 1;
                        srow["cui_itm_desc1"] = Param.Rows[i]["cui_itm_desc"].ToString();
                        srow["cui_gross_mass1"] = Param.Rows[i]["cui_gross_mass"].ToString();
                        srow["cui_net_mass1"] = Param.Rows[i]["cui_net_mass"].ToString();
                        srow["cui_bl_no1"] = Param.Rows[i]["cui_bl_no"].ToString();
                        srow["cui_orgin_cnty1"] = Param.Rows[i]["cui_orgin_cnty"].ToString();
                        srow["Line1"] = Param.Rows[i]["Line"].ToString();
                        srow["LineCount1"] = Convert.ToInt16(i + 1);
                        srow["cui_hs_cd1"] = Param.Rows[i]["cui_hs_cd"].ToString();
                        srow["cui_itm_price1"] = Convert.ToDecimal(Param.Rows[i]["cui_itm_price"].ToString());
                        srow["cui_bal_qty11"] = Param.Rows[i]["cui_qty"].ToString();
                        srow["cui_bal_qty21"] = Param.Rows[i]["cui_bal_qty2"].ToString();
                        srow["cui_bal_qty31"] = Param.Rows[i]["cui_bal_qty3"].ToString();
                        rebond2maxitm++;
                    }

                    if (i + 1 < Param.Rows.Count)
                    {
                        itm += 1;
                        srow["cui_itm_desc2"] = Param.Rows[i + 1]["cui_itm_desc"].ToString();
                        srow["cui_gross_mass2"] = Param.Rows[i + 1]["cui_gross_mass"].ToString();
                        srow["cui_net_mass2"] = Param.Rows[i + 1]["cui_net_mass"].ToString();
                        srow["cui_bl_no2"] = Param.Rows[i + 1]["cui_bl_no"].ToString();
                        srow["cui_orgin_cnty2"] = Param.Rows[i + 1]["cui_orgin_cnty"].ToString();
                        srow["Line2"] = Param.Rows[i + 1]["Line"].ToString();
                        srow["LineCount2"] = Convert.ToInt16(i + 2);
                        srow["cui_hs_cd2"] = Param.Rows[i + 1]["cui_hs_cd"].ToString();
                        srow["cui_itm_price2"] = Convert.ToDecimal(Param.Rows[i + 1]["cui_itm_price"].ToString());
                        srow["cui_bal_qty12"] = Param.Rows[i + 1]["cui_qty"].ToString();
                        srow["cui_bal_qty22"] = Param.Rows[i + 1]["cui_bal_qty2"].ToString();
                        srow["cui_bal_qty32"] = Param.Rows[i + 1]["cui_bal_qty3"].ToString();
                        rebond2maxitm++;
                    }

                    if (i + 2 < Param.Rows.Count)
                    {
                        itm += 1;
                        srow["cui_itm_desc3"] = Param.Rows[i + 2]["cui_itm_desc"].ToString();
                        srow["cui_gross_mass3"] = Param.Rows[i + 2]["cui_gross_mass"].ToString();
                        srow["cui_net_mass3"] = Param.Rows[i + 2]["cui_net_mass"].ToString();
                        srow["cui_bl_no3"] = Param.Rows[i + 2]["cui_bl_no"].ToString();
                        srow["cui_orgin_cnty3"] = Param.Rows[i + 2]["cui_orgin_cnty"].ToString();
                        srow["Line3"] = Param.Rows[i + 2]["Line"].ToString();
                        srow["LineCount3"] = Convert.ToInt16(i + 3);
                        srow["cui_hs_cd3"] = Param.Rows[i + 2]["cui_hs_cd"].ToString();
                        srow["cui_itm_price3"] = Convert.ToDecimal(Param.Rows[i + 2]["cui_itm_price"].ToString());
                        srow["cui_bal_qty13"] = Param.Rows[i + 2]["cui_qty"].ToString();
                        srow["cui_bal_qty23"] = Param.Rows[i + 2]["cui_bal_qty2"].ToString();
                        srow["cui_bal_qty33"] = Param.Rows[i + 2]["cui_bal_qty3"].ToString();
                        rebond2maxitm++;
                    }

                    secondpagedata.Rows.Add(srow);
                    i = i + 3;
                }
            }

            DataTable eledata = new DataTable();
            i = 0;
            string cat = "";
            decimal fob = 0;
            decimal foblkr = 0;
            decimal faight = 0;
            decimal faightlkr = 0;
            decimal insu = 0;
            decimal insulkr = 0;
            decimal other = 0;
            decimal otherlkr = 0;
            decimal total = 0;
            decimal totallkr = 0;

            eledata = bsObj.CHNLSVC.CustService.GetCusdecElementnew(entryNo);
            foreach (DataRow ele in eledata.Rows)
            {
                cat = eledata.Rows[i]["cus_ele_cd"].ToString();

                if (cat == "OTH")
                {
                    other = Convert.ToDecimal(eledata.Rows[i]["cus_amt"]);
                    otherlkr = Convert.ToDecimal(eledata.Rows[i]["cus_amt_com"]);
                }
                if (cat == "INSU")
                {
                    insu = Convert.ToDecimal(eledata.Rows[i]["cus_amt"]);
                    insulkr = Convert.ToDecimal(eledata.Rows[i]["cus_amt_com"]);
                }
                if (cat == "FRGT")
                {
                    faight = Convert.ToDecimal(eledata.Rows[i]["cus_amt"]);
                    faightlkr = Convert.ToDecimal(eledata.Rows[i]["cus_amt_com"]);

                }
                if (cat == "COST")
                {
                    fob = Convert.ToDecimal(eledata.Rows[i]["cus_amt"]);
                    foblkr = Convert.ToDecimal(eledata.Rows[i]["cus_amt_com"]);
                }
                i++;

            }
            total = other + insu + faight + fob;
            totallkr = otherlkr + insulkr + faightlkr + foblkr;

            DataTable finalele = new DataTable();
            DataRow elerow;
            finalele.Columns.Add("FOB", typeof(decimal));
            finalele.Columns.Add("FREIGHT", typeof(decimal));
            finalele.Columns.Add("INSU", typeof(decimal));
            finalele.Columns.Add("OTHER", typeof(decimal));
            finalele.Columns.Add("TOTAL", typeof(decimal));
            finalele.Columns.Add("TOTALLKR", typeof(decimal));
            finalele.Columns.Add("COUNTMAX", typeof(int));

            elerow = finalele.NewRow();
            elerow["FOB"] = fob;
            elerow["FREIGHT"] = faight;
            elerow["INSU"] = insu;
            elerow["OTHER"] = other;
            elerow["TOTAL"] = total;
            elerow["TOTALLKR"] = totallkr;
            elerow["COUNTMAX"] = maxcount;


            finalele.Rows.Add(elerow);

            DataTable cusdeccommon = bsObj.CHNLSVC.CustService.GetCusdecCommonNew("LK");



            List<ImportsBLContainer> blContainers = new List<ImportsBLContainer>();
            if (Hdrdata != null)
            {
                blContainers = bsObj.CHNLSVC.Financial.GetContainers(Hdrdata.Rows[0]["cuh_sun_req_no"].ToString());
            }
            else
            {
                blContainers = null;
            }

            DataTable page = new DataTable();
            DataRow pagerow;
            page.Columns.Add("Totalitems", typeof(Int32));
            pagerow = page.NewRow();
            pagerow["Totalitems"] = itm + 1;
            page.Rows.Add(pagerow);

            _cusdec_Rebond2.Database.Tables["firstGooddata"].SetDataSource(firsgood);
            //  _goods_declaration.Database.Tables["TaxValues"].SetDataSource(taxvalues);
            _cusdec_Rebond2.Database.Tables["HdrData"].SetDataSource(Hdrdata);

            int r = 0;
            foreach (DataRow secondpagedatanew in secondpagedata.Rows)
            {
                if (secondpagedata.Rows[r]["Line1"].ToString() == "") secondpagedata.Rows[r]["Line1"] = 0;
                if (secondpagedata.Rows[r]["Line2"].ToString() == "") secondpagedata.Rows[r]["Line2"] = 0;
                if (secondpagedata.Rows[r]["Line3"].ToString() == "") secondpagedata.Rows[r]["Line3"] = 0;
                r++;
            }

            _cusdec_Rebond2.Database.Tables["SecondPage"].SetDataSource(secondpagedata);
            _cusdec_Rebond2.Database.Tables["EleData"].SetDataSource(finalele);
            //  _goods_declaration.Database.Tables["Param"].SetDataSource(Param);
            _cusdec_Rebond2.Database.Tables["CusdecCommon"].SetDataSource(cusdeccommon);
            _cusdec_Rebond2.Database.Tables["Page"].SetDataSource(page);

            foreach (object repOp in _cusdec_Rebond2.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "firstpagetax")
                    {
                        ReportDocument subRepDoc = _cusdec_Rebond2.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["TaxValues"].SetDataSource(taxvalues);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "finaltaxvalues")
                    {
                        ReportDocument subRepDoc = _cusdec_Rebond2.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["TaxValues"].SetDataSource(taxvalues);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "finalrighttax")
                    {
                        ReportDocument subRepDoc = _cusdec_Rebond2.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["TaxValues"].SetDataSource(taxvalues);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "finalbottomtax")
                    {
                        ReportDocument subRepDoc = _cusdec_Rebond2.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["TaxValues"].SetDataSource(taxvalues);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "TotalTaxsummery")
                    {
                        ReportDocument subRepDoc = _cusdec_Rebond2.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["TaxValues"].SetDataSource(taxvalues);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "secondpageitemdetails")
                    {
                        ReportDocument subRepDoc = _cusdec_Rebond2.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["Param"].SetDataSource(Param);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "containers")
                    {
                        ReportDocument subRepDoc = _cusdec_Rebond2.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["Containers"].SetDataSource(blContainers);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    _cs.Dispose();
                }
            }
        }

        //DULAJ 2018/DEC/10
        public void Shipment_Tracker(InvReportPara _objRepPara)
        {

            DataTable glbTable = new DataTable();

            DataTable param = new DataTable();
             DataRow dr;

            param.Columns.Add("User", typeof(string));
            param.Columns.Add("Company", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));

            dr = param.NewRow();
            dr["User"] = _objRepPara._GlbUserID;
            dr["Company"] = _objRepPara._GlbReportCompName;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["fromdate"] = _objRepPara._GlbReportFromDate.Date;
            dr["todate"] = _objRepPara._GlbReportToDate.Date;

            param.Rows.Add(dr);

            DataRow glbRow;

            glbTable.Columns.Add("NO CONTAINERS", typeof(string));
            glbTable.Columns.Add("CARRYING TYPE", typeof(string));
            glbTable.Columns.Add("CLEAR BY", typeof(string));
            glbTable.Columns.Add("DOC NO", typeof(string));
            glbTable.Columns.Add("AGENT", typeof(string));
            glbTable.Columns.Add("CIF", typeof(decimal));
            glbTable.Columns.Add("CTNS", typeof(string));
            glbTable.Columns.Add("DESCRIPTION", typeof(string));
            glbTable.Columns.Add("MODEL", typeof(string));
            glbTable.Columns.Add("ENTRY TYPE", typeof(string));
            glbTable.Columns.Add("FILE NO", typeof(string));
            glbTable.Columns.Add("FIN REF", typeof(string));
            glbTable.Columns.Add("VESSEL", typeof(string));
            glbTable.Columns.Add("PORT OF LOADING", typeof(string));
            glbTable.Columns.Add("CLEARING DATE", typeof(DateTime));
            glbTable.Columns.Add("HANDOVER DATE", typeof(DateTime));
            glbTable.Columns.Add("LOCATION", typeof(string));
            glbTable.Columns.Add("CUSDEC ENTRY", typeof(string));
            glbTable.Columns.Add("LOCATIONDESC", typeof(string));
            glbTable.Clear();

            DataTable tmp_Table = new DataTable();
            tmp_Table = bsObj.CHNLSVC.MsgPortal.Get_Shipment_Data(_objRepPara._GlbReportCompCode, _objRepPara._GlbReportFromDate, _objRepPara._GlbReportToDate, _objRepPara._GlbDocNo);



            List<string> docList = (tmp_Table.AsEnumerable().Select(x => x["DOC NO"].ToString()).Distinct().ToList());
            foreach (string doc in docList)
            {
                IEnumerable<DataRow> results = null;
                results = (from MyRows in tmp_Table.AsEnumerable()
                           where
                            MyRows.Field<string>("DOC NO") == doc
                           select MyRows);
                string container = "";
                if (results.Any())
                {
                    DataTable filterresult = results.CopyToDataTable();
                    glbRow = glbTable.NewRow();
                    foreach (DataRow drdocs in filterresult.Rows)
                    {
                        string con = drdocs["NO CONTAINERS"].ToString() + "x" + drdocs["CONTAINER TYPE"].ToString();
                        container = con + "," + container;
                    }
                    glbRow["DOC NO"] = doc;
                    glbRow["CARRYING TYPE"] = filterresult.Rows[0]["CARRYING TYPE"].ToString();
                    glbRow["NO CONTAINERS"] = container.Substring(0, container.Length - 1);
                    glbRow["AGENT"] = filterresult.Rows[0]["AGENT"].ToString();
                    glbRow["CLEAR BY"] = filterresult.Rows[0]["CLEAR BY"].ToString();
                    if (filterresult.Rows[0]["CIF"].ToString() != "")
                    {
                        glbRow["CIF"] = Convert.ToDecimal(filterresult.Rows[0]["CIF"].ToString());
                    }
                    else
                    {
                        glbRow["CIF"] = 0;
                    }
                    glbRow["CTNS"] = filterresult.Rows[0]["CTNS"].ToString();
                    glbRow["DESCRIPTION"] = filterresult.Rows[0]["DESCRIPTION"].ToString();
                    glbRow["MODEL"] = filterresult.Rows[0]["MODEL"].ToString();
                    glbRow["ENTRY TYPE"] = filterresult.Rows[0]["ENTRY TYPE"].ToString();
                    glbRow["FILE NO"] = filterresult.Rows[0]["FILE NO"].ToString();
                    glbRow["FIN REF"] = filterresult.Rows[0]["FIN REF"].ToString();
                    glbRow["VESSEL"] = filterresult.Rows[0]["VESSEL"].ToString();
                    glbRow["PORT OF LOADING"] = filterresult.Rows[0]["PORT OF LOADING"].ToString();
                    glbRow["LOCATIONDESC"] = filterresult.Rows[0]["LOCATIONDESC"].ToString();
                    if (filterresult.Rows[0]["CLEARING DATE"].ToString() != "")
                    {
                        glbRow["CLEARING DATE"] = Convert.ToDateTime(filterresult.Rows[0]["CLEARING DATE"].ToString());
                    }
                    else
                    {
                        glbRow["CLEARING DATE"] = DateTime.MinValue;
                    }
                    if (filterresult.Rows[0]["HANDOVER DATE"].ToString() != "")
                    {
                        glbRow["HANDOVER DATE"] = Convert.ToDateTime(filterresult.Rows[0]["HANDOVER DATE"].ToString());
                    }
                    else
                    {
                        glbRow["HANDOVER DATE"] = DateTime.MinValue;
                    }
                    glbRow["LOCATION"] = filterresult.Rows[0]["LOCATION"].ToString();
                    glbRow["CUSDEC ENTRY"] = filterresult.Rows[0]["CUSDEC ENTRY"].ToString();

                    glbTable.Rows.Add(glbRow);
                }
            }
            glbTable.TableName = "ShipmentDetail";
            param.TableName = "ParamShipment";
            _shipmentTracker.Database.Tables["ShipmentDetail"].SetDataSource(glbTable);
            _shipmentTracker.Database.Tables["ParamShipment"].SetDataSource(param);
        }
        //DULAJ 2018/DEC/10
        public void ClearingEntry(InvReportPara _objRepPara, int type)
        {
            string rptCompanies = "";
            List<string> rptComlist = _objRepPara._GlbReportCompanies.Split(',').ToList<string>();
            foreach (string cm in rptComlist)
            {
                rptCompanies = rptCompanies + "" + cm + "" + ",";
            }
            rptCompanies = rptCompanies.Substring(0, rptCompanies.Length - 1);
            if (type == 1)
            {
                DataTable glbTable = new DataTable();            

                DataTable param = new DataTable();
                DataRow dr;
                DataRow glbRow;
                param.Columns.Add("User", typeof(string));
                param.Columns.Add("Company", typeof(string));
                param.Columns.Add("heading_1", typeof(string));
                param.Columns.Add("fromdate", typeof(DateTime));
                param.Columns.Add("todate", typeof(DateTime));

                dr = param.NewRow();
                dr["User"] = _objRepPara._GlbUserID;
                dr["Company"] = _objRepPara._GlbReportCompName;
                dr["heading_1"] = _objRepPara._GlbReportHeading;
                dr["fromdate"] = _objRepPara._GlbReportFromDate.Date;
                dr["todate"] = _objRepPara._GlbReportToDate.Date;

                glbTable.Columns.Add("Type", typeof(string));
                glbTable.Columns.Add("Fourty", typeof(int));
                glbTable.Columns.Add("Twenty", typeof(int));
                glbTable.Columns.Add("LCL", typeof(int));
                glbTable.Columns.Add("AIR", typeof(int));
                // glbTable.Columns.Add("Total", typeof(string));
                glbTable.Columns.Add("Company", typeof(string));

                param.Rows.Add(dr);

                DataTable tmp_Table = bsObj.CHNLSVC.MsgPortal.Get_Clr_usr(rptCompanies, _objRepPara._GlbReportFromDate, _objRepPara._GlbReportToDate, _objRepPara._GlbDocNo);

                List<string> usrList = (tmp_Table.AsEnumerable().Select(x => x["ClearBy"].ToString()).Distinct().ToList());
                List<string> companies = (tmp_Table.AsEnumerable().Select(x => x["COMPANY"].ToString()).Distinct().ToList());
                foreach (string usr in usrList)
                {
                    foreach (string company in companies)
                    {
                        IEnumerable<DataRow> results = null;
                        results = (from MyRows in tmp_Table.AsEnumerable()
                                   where
                                    MyRows.Field<string>("ClearBy") == usr &&
                                    MyRows.Field<string>("COMPANY") == company
                                   select MyRows);

                        if (results.Any())
                        {
                            DataTable filterresult = results.CopyToDataTable();
                            glbRow = glbTable.NewRow();
                            int fourtyQty = 0;
                            int twentyQty = 0;
                            int lcl = 0;
                            int air = 0;
                            foreach (DataRow drdocs in filterresult.Rows)
                            {
                                if (drdocs["ContainerType"].ToString() == "40")
                                {
                                    fourtyQty = fourtyQty + Convert.ToInt32(drdocs["ContainerQty"].ToString());
                                }
                                if (drdocs["ContainerType"].ToString() == "AIR")
                                {
                                    air = air + Convert.ToInt32(drdocs["ContainerQty"].ToString());

                                }
                                if (drdocs["ContainerType"].ToString() == "20")
                                {

                                    twentyQty = twentyQty + Convert.ToInt32(drdocs["ContainerQty"].ToString()); ;

                                }
                                if (drdocs["ContainerType"].ToString() == "LCL")
                                {
                                    lcl = lcl + Convert.ToInt32(drdocs["ContainerQty"].ToString());

                                }
                            }
                            glbRow["Type"] = usr;
                            glbRow["Fourty"] = fourtyQty;
                            glbRow["Twenty"] = twentyQty;
                            glbRow["LCL"] = lcl;
                            glbRow["AIR"] = air;
                            glbRow["Company"] = company;
                            glbTable.Rows.Add(glbRow);
                        }
                    }
                }

                glbTable.TableName = "ClearingDetailsUser";
                param.TableName = "ParamShipment";
                _entryClearingClrUser.Database.Tables["ClearingDetailsUser"].SetDataSource(glbTable);
                _entryClearingClrUser.Database.Tables["ParamShipment"].SetDataSource(param);
            }
            if (type == 2)
            {
                DataTable glbTable = new DataTable();

                DataTable param = new DataTable();
                DataRow dr;
                DataRow glbRow;
                param.Columns.Add("User", typeof(string));
                param.Columns.Add("Company", typeof(string));
                param.Columns.Add("heading_1", typeof(string));
                param.Columns.Add("fromdate", typeof(DateTime));
                param.Columns.Add("todate", typeof(DateTime));

                dr = param.NewRow();
                dr["User"] = _objRepPara._GlbUserID;
                dr["Company"] = _objRepPara._GlbReportCompName;
                dr["heading_1"] = _objRepPara._GlbReportHeading;
                dr["fromdate"] = _objRepPara._GlbReportFromDate.Date;
                dr["todate"] = _objRepPara._GlbReportToDate.Date;

                glbTable.Columns.Add("Type", typeof(string));
                glbTable.Columns.Add("Fourty", typeof(int));
                glbTable.Columns.Add("Twenty", typeof(int));
                glbTable.Columns.Add("LCL", typeof(int));
                glbTable.Columns.Add("AIR", typeof(int));                
                glbTable.Columns.Add("Company", typeof(string));

                param.Rows.Add(dr);

                DataTable tmp_Table = bsObj.CHNLSVC.MsgPortal.Get_Clr_Com(rptCompanies, _objRepPara._GlbReportFromDate, _objRepPara._GlbReportToDate, _objRepPara._GlbDocNo);
                                
                List<string> companies = (tmp_Table.AsEnumerable().Select(x => x["COMPANY"].ToString()).Distinct().ToList());                
                    foreach (string company in companies)
                    {
                        IEnumerable<DataRow> results = null;
                        results = (from MyRows in tmp_Table.AsEnumerable()
                                   where                                    
                                   MyRows.Field<string>("COMPANY") == company
                                   select MyRows);

                        if (results.Any())
                        {
                            DataTable filterresult = results.CopyToDataTable();
                            glbRow = glbTable.NewRow();
                            int fourtyQty = 0;
                            int twentyQty = 0;
                            int lcl = 0;
                            int air = 0;
                            foreach (DataRow drdocs in filterresult.Rows)
                            {
                                if (drdocs["ContainerType"].ToString() == "40")
                                {
                                    fourtyQty = fourtyQty + Convert.ToInt32(drdocs["ContainerQty"].ToString());
                                }
                                if (drdocs["ContainerType"].ToString() == "AIR")
                                {
                                    air = air + Convert.ToInt32(drdocs["ContainerQty"].ToString());

                                }
                                if (drdocs["ContainerType"].ToString() == "20")
                                {

                                    twentyQty = twentyQty + Convert.ToInt32(drdocs["ContainerQty"].ToString()); ;

                                }
                                if (drdocs["ContainerType"].ToString() == "LCL")
                                {
                                    lcl = lcl + Convert.ToInt32(drdocs["ContainerQty"].ToString());

                                }
                            }
                            glbRow["Type"] = "";
                            glbRow["Fourty"] = fourtyQty;
                            glbRow["Twenty"] = twentyQty;
                            glbRow["LCL"] = lcl;
                            glbRow["AIR"] = air;
                            glbRow["Company"] = company;
                            glbTable.Rows.Add(glbRow);
                        }
                    }
                

                glbTable.TableName = "ClearingDetailsUser";
                param.TableName = "ParamShipment";
                _entryClearingClrcom.Database.Tables["ClearingDetailsUser"].SetDataSource(glbTable);
                _entryClearingClrcom.Database.Tables["ParamShipment"].SetDataSource(param);
            }
            if (type == 3)
            {
                DataTable glbTable = new DataTable();

                DataTable param = new DataTable();
                DataRow dr;
                DataRow glbRow;
                param.Columns.Add("User", typeof(string));
                param.Columns.Add("Company", typeof(string));
                param.Columns.Add("heading_1", typeof(string));
                param.Columns.Add("fromdate", typeof(DateTime));
                param.Columns.Add("todate", typeof(DateTime));

                dr = param.NewRow();
                dr["User"] = _objRepPara._GlbUserID;
                dr["Company"] = _objRepPara._GlbReportCompName;
                dr["heading_1"] = _objRepPara._GlbReportHeading;
                dr["fromdate"] = _objRepPara._GlbReportFromDate.Date;
                dr["todate"] = _objRepPara._GlbReportToDate.Date;

                glbTable.Columns.Add("Type", typeof(string));
                glbTable.Columns.Add("Fourty", typeof(int));
                glbTable.Columns.Add("Twenty", typeof(int));
                glbTable.Columns.Add("LCL", typeof(int));
                glbTable.Columns.Add("AIR", typeof(int));                
                glbTable.Columns.Add("Company", typeof(string));

                param.Rows.Add(dr);

                DataTable tmp_Table = bsObj.CHNLSVC.MsgPortal.Get_Clr_ShippingLine(rptCompanies, _objRepPara._GlbReportFromDate, _objRepPara._GlbReportToDate, _objRepPara._GlbDocNo);

                List<string> shpList = (tmp_Table.AsEnumerable().Select(x => x["Shipping Line"].ToString()).Distinct().ToList());
               // List<string> companies = (tmp_Table.AsEnumerable().Select(x => x["COMPANY"].ToString()).Distinct().ToList());
                foreach (string shp in shpList)
                {                   
                        IEnumerable<DataRow> results = null;
                        results = (from MyRows in tmp_Table.AsEnumerable()
                                   where
                                    MyRows.Field<string>("Shipping Line") == shp                                   
                                   select MyRows);

                        if (results.Any())
                        {
                            DataTable filterresult = results.CopyToDataTable();
                            glbRow = glbTable.NewRow();
                            int fourtyQty = 0;
                            int twentyQty = 0;
                            int lcl = 0;
                            int air = 0;
                            foreach (DataRow drdocs in filterresult.Rows)
                            {
                                if (drdocs["ContainerType"].ToString() == "40")
                                {
                                    fourtyQty = fourtyQty + Convert.ToInt32(drdocs["ContainerQty"].ToString());
                                }
                                if (drdocs["ContainerType"].ToString() == "AIR")
                                {
                                    air = air + Convert.ToInt32(drdocs["ContainerQty"].ToString());

                                }
                                if (drdocs["ContainerType"].ToString() == "20")
                                {

                                    twentyQty = twentyQty + Convert.ToInt32(drdocs["ContainerQty"].ToString()); ;

                                }
                                if (drdocs["ContainerType"].ToString() == "LCL")
                                {
                                    lcl = lcl + Convert.ToInt32(drdocs["ContainerQty"].ToString());

                                }
                            }
                            glbRow["Type"] = shp;
                            glbRow["Fourty"] = fourtyQty;
                            glbRow["Twenty"] = twentyQty;
                            glbRow["LCL"] = lcl;
                            glbRow["AIR"] = air;
                            glbRow["Company"] = "";
                            glbTable.Rows.Add(glbRow);
                        }
                    
                }

                glbTable.TableName = "ClearingDetailsUser";
                param.TableName = "ParamShipment";
                _entryClearingClrshipping.Database.Tables["ClearingDetailsUser"].SetDataSource(glbTable);
                _entryClearingClrshipping.Database.Tables["ParamShipment"].SetDataSource(param);
            }
        }
        //DULAJ 2018/DEC/24
        public void ValueDeclarationForm(string EntryNo, string com, DataTable Hdrdata)
        {
            bsObj = new Services.Base();
            DataTable Param = new DataTable();
            DataRow dr;           
            string type = "";
            Param.Columns.Add("Currency", typeof(string));
            Param.Columns.Add("CusDecNo", typeof(string));
            Param.Columns.Add("CusDecDate", typeof(DateTime));
            Param.Columns.Add("SaleType", typeof(string));


                      
         
            dr = Param.NewRow();
            dr["Currency"] = Hdrdata.Rows[0]["cuh_cur_cd"].ToString();
            dr["CusDecNo"] = Hdrdata.Rows[0]["cuh_office_of_entry"].ToString()+" " + Convert.ToDateTime(Hdrdata.Rows[0]["cuh_dt"].ToString()).Year +" "+ Hdrdata.Rows[0]["cuh_cusdec_entry_no"].ToString();
            dr["CusDecDate"] = Convert.ToDateTime(Hdrdata.Rows[0]["cuh_dt"].ToString()); 
            dr["SaleType"] = "SALES";
            Param.Rows.Add(dr);

            #region items
            //Dulaj 
            DataTable masterData = new DataTable();
          //  masterData = bsObj.CHNLSVC.CustService.GetCustomWorkingSheet(EntryNo, "TOT");
            int i = 0;
            string itmcode = "";
            string cost = "COST";
            decimal costprice = 0;
            string frgt = "FRGT";
            decimal frgtprice = 0;
            string insu = "INSU";
            decimal insuprice = 0;
            string oth = "OTH";
            decimal othprice = 0;
            decimal cif = 0;
            int linenumber = 10000000;
           

            masterData = bsObj.CHNLSVC.CustService.GetCustomWorkingSheetHS(EntryNo, "TOT");
            //itm table
            DataTable itmtable = new DataTable();
            DataRow dritm;
            itmtable.Columns.Add("No", typeof(string));
            itmtable.Columns.Add("HScode", typeof(string));
            itmtable.Columns.Add("ItmDescription", typeof(string));
            itmtable.Columns.Add("Quantity", typeof(decimal));
            itmtable.Columns.Add("FOB", typeof(decimal));
            itmtable.Columns.Add("Freight", typeof(decimal));
            itmtable.Columns.Add("Insurance", typeof(decimal));
            itmtable.Columns.Add("Other", typeof(decimal));
            itmtable.Columns.Add("CIF", typeof(decimal));
            itmtable.Columns.Add("NetMass", typeof(decimal));
            itmtable.Columns.Add("GrossMass", typeof(decimal));
            itmtable.Columns.Add("Packages", typeof(decimal));
            itmtable.Columns.Add("MainHS", typeof(string));
            itmtable.Columns.Add("Itemcode", typeof(string));
            itmtable.Columns.Add("Model", typeof(string));
            itmtable.Columns.Add("HsDescription", typeof(string));
            itmtable.Columns.Add("COUNTRY", typeof(string));
            
            if (masterData != null)
            {
                //foreach (DataRow dtRow in masterData.Rows)
                for (int k = 0; k < masterData.Rows.Count * 10; k++)
                {
                    if (i < masterData.Rows.Count)
                        if (linenumber != masterData.Rows[i].Field<Int16>(0))
                        {
                            if (i > 0)
                            {
                                //addrows
                                dritm = itmtable.NewRow();
                                dritm["No"] = Convert.ToInt32(masterData.Rows[i - 1].Field<Int16>(0));
                                dritm["HScode"] = masterData.Rows[i - 1].Field<string>(3);
                                dritm["ItmDescription"] = masterData.Rows[i - 1].Field<string>(2);
                                dritm["Quantity"] = Convert.ToDecimal(masterData.Rows[i - 1].Field<decimal>(4));
                                dritm["FOB"] = costprice;
                                dritm["Freight"] = frgtprice;
                                dritm["Insurance"] = insuprice;
                                dritm["Other"] = othprice;
                                dritm["CIF"] = costprice + frgtprice + insuprice + othprice;
                                dritm["NetMass"] = Convert.ToDecimal(masterData.Rows[i - 1].Field<decimal>(8));
                                dritm["GrossMass"] = Convert.ToDecimal(masterData.Rows[i - 1].Field<double>(7));
                                dritm["Packages"] = masterData.Rows[i - 1].Field<string>(9);
                                if (!string.IsNullOrEmpty(dritm["Packages"].ToString()))
                                {
                                    dritm["Packages"] = Convert.ToDecimal(dritm["Packages"].ToString());
                                }
                                dritm["MainHS"] = masterData.Rows[i - 1].Field<string>(10);
                                dritm["Itemcode"] = masterData.Rows[i - 1]["CUI_ITM_CD"].ToString();
                                dritm["Model"] = masterData.Rows[i - 1]["CUI_MODEL"].ToString();
                                dritm["HsDescription"] = masterData.Rows[i - 1]["HsDescription"].ToString();
                                dritm["COUNTRY"] = masterData.Rows[i - 1]["COUNTRY"].ToString();
                                itmtable.Rows.Add(dritm);
                                costprice = 0;
                                frgtprice = 0;
                                insuprice = 0;
                                othprice = 0;
                                //i++;

                            }
                            linenumber = masterData.Rows[i].Field<Int16>(0);
                        }
                        else
                        {
                            if (masterData.Rows[i].Field<string>(5) == cost)
                            {
                                costprice = masterData.Rows[i].Field<decimal>(6);

                            }
                            if (masterData.Rows[i].Field<string>(5) == frgt)
                            {
                                frgtprice = masterData.Rows[i].Field<decimal>(6);

                            }
                            if (masterData.Rows[i].Field<string>(5) == insu)
                            {
                                insuprice = masterData.Rows[i].Field<decimal>(6);

                            }
                            if (masterData.Rows[i].Field<string>(5) == oth)
                            {
                                othprice = masterData.Rows[i].Field<decimal>(6);

                            }

                            if (i == (masterData.Rows.Count - 1))
                            {
                                dritm = itmtable.NewRow();
                                dritm["No"] = Convert.ToInt32(masterData.Rows[i - 1].Field<Int16>(0));
                                dritm["HScode"] = masterData.Rows[i - 1].Field<string>(3);
                                dritm["ItmDescription"] = masterData.Rows[i - 1].Field<string>(2);
                                dritm["Quantity"] = Convert.ToDecimal(masterData.Rows[i - 1].Field<decimal>(4));
                                dritm["FOB"] = costprice;
                                dritm["Freight"] = frgtprice;
                                dritm["Insurance"] = insuprice;
                                dritm["Other"] = othprice;
                                dritm["CIF"] = costprice + frgtprice + insuprice + othprice;
                                dritm["NetMass"] = Convert.ToDecimal(masterData.Rows[i - 1].Field<decimal>(8));
                                dritm["GrossMass"] = Convert.ToDecimal(masterData.Rows[i - 1].Field<double>(7));
                                dritm["Packages"] = masterData.Rows[i - 1].Field<string>(9);
                                if (!string.IsNullOrEmpty(dritm["Packages"].ToString()))
                                {
                                    dritm["Packages"] = Convert.ToDecimal(dritm["Packages"].ToString());
                                }
                                dritm["MainHS"] = masterData.Rows[i - 1].Field<string>(10);
                                dritm["Itemcode"] = masterData.Rows[i - 1]["CUI_ITM_CD"].ToString();
                                dritm["Model"] = masterData.Rows[i - 1]["CUI_MODEL"].ToString();
                                dritm["HsDescription"] = masterData.Rows[i - 1]["HsDescription"].ToString();
                                dritm["COUNTRY"] = masterData.Rows[i - 1]["COUNTRY"].ToString();
                                itmtable.Rows.Add(dritm);
                                costprice = 0;
                                frgtprice = 0;
                                insuprice = 0;
                                othprice = 0;
                            }

                            i++;
                        }
                }
            }
            else
            {
                dritm = itmtable.NewRow();
                dritm["No"] = 1;
                dritm["HScode"] = "";
                dritm["ItmDescription"] = "";
                dritm["Quantity"] = 0;
                dritm["FOB"] = 0;
                dritm["Freight"] = 0;
                dritm["Insurance"] = 0;
                dritm["Other"] = 0;
                dritm["CIF"] = 0;
                dritm["NetMass"] = 0;
                dritm["GrossMass"] = 0;
                dritm["Packages"] = 0;
                dritm["MainHS"] = "";
                dritm["Itemcode"] = "";
                dritm["Model"] = "";
                dritm["HsDescription"] = "";
                dritm["COUNTRY"] = "";
                itmtable.Rows.Add(dritm);
                costprice = 0;
                frgtprice = 0;
                insuprice = 0;
                othprice = 0;
            }
            if (masterData != null)
            {
                dritm = itmtable.NewRow();
                dritm["No"] = Convert.ToInt32(masterData.Rows[i - 1].Field<Int16>(0));
                dritm["HScode"] = masterData.Rows[i - 1].Field<string>(3);
                dritm["ItmDescription"] = masterData.Rows[i - 1].Field<string>(2);
                dritm["Quantity"] = Convert.ToDecimal(masterData.Rows[i - 1].Field<decimal>(4));
                dritm["FOB"] = costprice;
                dritm["Freight"] = frgtprice;
                dritm["Insurance"] = insuprice;
                dritm["Other"] = othprice;
                dritm["CIF"] = costprice + frgtprice + insuprice + othprice;
                dritm["NetMass"] = Convert.ToDecimal(masterData.Rows[i - 1].Field<decimal>(8));
                dritm["GrossMass"] = Convert.ToDecimal(masterData.Rows[i - 1].Field<double>(7));
                dritm["Packages"] = masterData.Rows[i - 1].Field<string>(9);
                if (!string.IsNullOrEmpty(dritm["Packages"].ToString()))
                {
                    dritm["Packages"] = Convert.ToDecimal(dritm["Packages"].ToString());
                }
                dritm["MainHS"] = masterData.Rows[i - 1].Field<string>(10);
                dritm["Itemcode"] = masterData.Rows[i - 1]["CUI_ITM_CD"].ToString();
                dritm["Model"] = masterData.Rows[i - 1]["CUI_MODEL"].ToString();
                dritm["HsDescription"] = masterData.Rows[i - 1]["HsDescription"].ToString();
                dritm["COUNTRY"] = masterData.Rows[i - 1]["COUNTRY"].ToString();
                itmtable.Rows.Add(dritm);
                costprice = 0;
                frgtprice = 0;
                insuprice = 0;
                othprice = 0;
            }
            List<WorkingSheetData> worlist = new List<WorkingSheetData>();
            int j = 0;
            foreach (DataRow itrow in itmtable.Rows)
            {
                var wordta = new WorkingSheetData();
                wordta.CIF = Convert.ToDecimal(itmtable.Rows[j]["CIF"]);
                wordta.FOB = Convert.ToDecimal(itmtable.Rows[j]["FOB"]);
                wordta.Freight = Convert.ToDecimal(itmtable.Rows[j]["Freight"]);
                wordta.GrossMass = Convert.ToDouble(itmtable.Rows[j]["GrossMass"]);
                wordta.HScode = itmtable.Rows[j]["HScode"].ToString();
                wordta.Insurance = Convert.ToDecimal(itmtable.Rows[j]["Insurance"]);
                wordta.ItmDescription = itmtable.Rows[j]["ItmDescription"].ToString();
                wordta.NetMass = Convert.ToDecimal(itmtable.Rows[j]["NetMass"]);
                wordta.No = Convert.ToInt32(itmtable.Rows[j]["No"]);
                wordta.Other = Convert.ToDecimal(itmtable.Rows[j]["Other"]);
                wordta.Packages = Convert.ToDecimal(itmtable.Rows[j]["Packages"]);
                wordta.Quantity = Convert.ToDecimal(itmtable.Rows[j]["Quantity"]);
                wordta.MainHS = itmtable.Rows[j]["MainHS"].ToString();
                wordta.Itemcode = itmtable.Rows[j]["Itemcode"].ToString();
                wordta.Country = itmtable.Rows[j]["COUNTRY"].ToString();
                wordta.HSDescription = itmtable.Rows[j]["HsDescription"].ToString();
                // wordta.Model = itmtable.Rows[j]["Model"].HsDescription();
                if (wordta.Other == 0 && wordta.FOB == 0 && wordta.Freight == 0 && wordta.Insurance == 0 && wordta.CIF == 0)
                {

                }
                else
                {
                    worlist.Add(wordta);
                }


                j++;
            }
            foreach (var mainhslist in worlist)
            {
                if (mainhslist.MainHS == mainhslist.HScode)
                {
                    mainhslist.MainHS = "1";
                }
                else
                {
                    mainhslist.MainHS = "2";
                }
            }



            decimal sumfrait = worlist.Sum(x => x.FOB);

            if (sumfrait == 0) sumfrait = 1;
            worlist = worlist.OrderBy(a => a.MainHS).ThenBy(n => n.HScode).ToList();
            var result = worlist
                .GroupBy(l => new { l.No })
    .Select(cl => new WorkingSheetData
    {
        MainHS = cl.First().MainHS.ToString(),
        HSDescription = cl.First().HSDescription.ToString(),
        Country = cl.First().Country.ToString(),
        HScode = cl.First().HScode,
        ItmDescription = cl.First().ItmDescription.ToString(),
        CIF = cl.Sum(c => c.CIF),
        No = cl.First().No,
        Quantity = cl.Sum(c => c.Quantity),
        Freight = cl.Sum(c => c.Freight),
        Insurance = cl.Sum(c => c.Insurance),
        Other = cl.Sum(c => c.Other),
        FOB = cl.Sum(c => c.FOB),
        NetMass = cl.Sum(c => c.NetMass),
        GrossMass = cl.Sum(c => c.GrossMass),
        Packages = Math.Round(((cl.First().Packages / sumfrait) * cl.Sum(c => c.FOB)), 2)
    }).ToList();

            var resultModel = worlist
                .GroupBy(l => new { l.No })
    .Select(cl => new WorkingSheetData
    {
        MainHS = cl.First().MainHS.ToString(),
        HScode = cl.First().HScode,
        HSDescription = cl.First().HSDescription.ToString(),
        Country = cl.First().Country.ToString(),
        ItmDescription = cl.First().ItmDescription.ToString(),
        CIF = cl.Sum(c => c.CIF),
        No = cl.First().No,
        Quantity = cl.Sum(c => c.Quantity),
        Freight = cl.Sum(c => c.Freight),
        Insurance = cl.Sum(c => c.Insurance),
        Other = cl.Sum(c => c.Other),
        FOB = cl.Sum(c => c.FOB),
        NetMass = cl.Sum(c => c.NetMass),
        GrossMass = cl.Sum(c => c.GrossMass),
        Packages = Math.Round(((cl.First().Packages / sumfrait) * cl.Sum(c => c.FOB)), 2)
    }).ToList();
          
            decimal tot_sum = result.Sum(a => a.Packages);


            string VAL = tot_sum.ToString();

            string[] X = tot_sum.ToString().Split('.');
            if (X.Length > 0 && X[1].Length > 2)
            {
                VAL = X[0] + "." + X[1].Substring(0, 2);
            }        

            //_cus_workitm.Database.Tables["CustomWorkingSheet"].SetDataSource(resultModel);
          
            itmtable.Rows.RemoveAt(itmtable.Rows.Count - 1);
            //  DataTable dd = itmtable;
            List<WorkSheetHS> workSheetHsList = new List<WorkSheetHS>();
            foreach (WorkingSheetData workSheetItem in resultModel)
            {
                WorkSheetHS workSheetHS = new WorkSheetHS();
                workSheetHS.Qty = workSheetItem.Quantity;//Convert.ToDecimal(workSheetItem["Quantity"].ToString());
                workSheetHS.Description = workSheetItem.HSDescription;//workSheetItem["HsDescription"].ToString();
                workSheetHS.Country = workSheetItem.Country;//workSheetItem["Country"].ToString();
                workSheetHS.CusVal = workSheetItem.CIF; //Convert.ToDecimal(workSheetItem["CIF"].ToString());
                workSheetHS.DecVal = workSheetItem.FOB;//Convert.ToDecimal(workSheetItem["FOB"].ToString());
                workSheetHS.Addition = workSheetItem.Freight + workSheetItem.Insurance;//Convert.ToDecimal(workSheetItem["Insurance"].ToString()) + Convert.ToDecimal(workSheetItem["Freight"].ToString());
                workSheetHS.HsCode = workSheetItem.HScode;//workSheetItem["HScode"].ToString();
               // workSheetHS.MainHs = Convert.ToInt32(workSheetItem["MainHs"].ToString());
                workSheetHsList.Add(workSheetHS);
            }
            var _groupedItem = workSheetHsList.GroupBy(grp => new
            {
                grp.HsCode,
                grp.Country
               // grp.Description

            }).Select(x => new
            {
                x.Key,
                Description = x.First().Description.ToString(),                
                Qty = x.Sum(y => y.Qty),
                CusVal = x.Sum(y => y.CusVal),
                DecVal = x.Sum(y => y.DecVal),
                Addition = x.Sum(y => y.Addition)                
            }).ToList();

            List<WorkSheetHS> workSheetHsListRep = new List<WorkSheetHS>();
            int noCount = 1;
            foreach (var workingitemHs in _groupedItem)
            {
                WorkSheetHS workSheetHS = new WorkSheetHS();
                workSheetHS.Item = noCount;
                workSheetHS.Line = noCount;
                workSheetHS.Qty = workingitemHs.Qty;
                workSheetHS.Unit = "NIU";
                workSheetHS.Country = workingitemHs.Key.Country;
                workSheetHS.Description = workingitemHs.Description;
                workSheetHS.CusVal = workingitemHs.CusVal;
                workSheetHS.DecVal = workingitemHs.DecVal;
                workSheetHS.Addition = workingitemHs.Addition;
                noCount++;
                workSheetHsListRep.Add(workSheetHS);
            }
            #endregion

            _valueDeclarationReport.Database.Tables["ValueDeclarationParm"].SetDataSource(Param);
            _valueDeclarationReport.Database.Tables["ValDeclarationItm"].SetDataSource(workSheetHsListRep);
        }
    }
}