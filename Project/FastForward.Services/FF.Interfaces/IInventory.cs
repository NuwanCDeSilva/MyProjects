using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using FF.BusinessObjects;
using FF.BusinessObjects.Sales;
using System.Data;
using FF.BusinessObjects.InventoryNew;
using FF.BusinessObjects.ToursNew;
using FF.BusinessObjects.General;
using FF.BusinessObjects.Security;

namespace FF.Interfaces
{
    [ServiceContract]
    public interface IInventory
    {
        //kapila
        [OperationContract]
        DataTable get_srn_items(string _refNo);
        //kapila
        [OperationContract]
        DataTable Get_serial_by_itm_stus(string company, string location, string itemCode, string _stus, string serial);
        //kapila
        [OperationContract]
        DataTable CHECKRESQTY(string COM, string LOC, string ITM, string STATUS, string USER, decimal QTY);
        //kapila
        [OperationContract]
        DataTable getMstSysParam(string _com, string _pty_tp, string _pty_cd, string _rest_tp, string _brand, string _cat);
        //kapila
        [OperationContract]
        int SaveInventoryRequestData_SR(InventoryRequest _inventoryRequest, List<InventoryRequestItem> _inventoryRequestAutoAppList, MasterAutoNumber _mastAutoNo, out string _docNo, out string _reqdNo, Boolean _isFoundSysPara = false);
        //kapila
        [OperationContract]
        DataTable GetINRSERMST_Rcc(string _company, string _loc, string _serial, string _war, string _inv, string _doc);
        //kapila
        [OperationContract]
        DataTable GetRootBalances(string _com, string _loc, string _item);
        //kapila
        [OperationContract]
        bool Is_Found_Mst_Sys_Para(string _company, string _loc, string _type);
        //kapila
        [OperationContract]
        DataTable Get_MRN_Prefer_Loc(string _company, string _Item);
        //kapila
        [OperationContract]
        DataTable Get_INTHDRByOthDoc(String Com, String Type, String OthDoc);
        //kapila
        [OperationContract]
        DataTable GetTaxStrucData(string _com, string _item);
        //kapila
        [OperationContract]
        int Delete_rept_Pick_Ser(int _seq);
        //kapila
        [OperationContract]
        DataTable GetAvailableGvPrefixes(string _itm);
        //kapila
        [OperationContract]
        DataTable Get_SCV_Area();
        //kapila
        [OperationContract]
        List<ReptPickSerials> GetAvailableSerials(string company, string location);
        //kapila
        [OperationContract]
        Int32 Import_SI(string _SI, string _loc);

        //kapila
        [OperationContract]
        DataTable getMstSysPara(string _com, string _pty_tp, string _pty_cd, string _rest_tp, string _dir_pry_cd);
        //kapila
        [OperationContract]
        DataTable getNextSerialSCM(string _com, string _loc, string _item, string _stus);

        /// <summary>
        /// getSerialSCMToMob by DULANGA 2016-2-2
        /// </summary>
        /// <param name="_com"></param>
        /// <param name="_loc"></param>
        /// <param name="_serial"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable getSerialSCMToMob(string _com, string _loc, string _serial, string item);

        //kapila
        [OperationContract]
        DataTable IsValid_SCM_Serial(string _com, string _loc, string _item, string _ser);
        //kapila
        [OperationContract]
        DataTable GetManualDocBookNo(string _Comp, string _Loc, string _DocType, Int32 _NextNo, string _prefix);

        [OperationContract]
        DataTable GetEntryDtl(string _entryno);

        //kapila
        [OperationContract]
        DataTable GetWarrantySearchAll(string _company, string _serial1, string _serial2, string _warranty, string _invoice);
        //kapila
        [OperationContract]
        Boolean IsRccSerialFoundJob(string _Item, string _Serial, string _job);
        //kapila
        [OperationContract]
        DataTable Get_TempIssued_Items(string Com, string _mloc, string _sloc, string Type);




        //kapila
        [OperationContract]
        DataTable GetAllFAMaterialRequestsList(string _com, string _loc, DateTime _from, DateTime _to, string _tp, string _reqno, string _reqby);

        //kapila
        [OperationContract]
        DataTable GetFAItem(string _item);

        //kapila
        [OperationContract]
        FA_Inventory_Req_Hdr GetInventoryFARequestData(FA_Inventory_Req_Hdr _inventoryRequest);

        //kapila
        [OperationContract]
        int SaveFARequest(FA_Inventory_Req_Hdr _reqHdr, List<FA_Inventory_Req_Itm> _reqItmList, string _moduleID, string _startChar, out string _docno);

        //kapila
        [OperationContract]
        int CancelAcInstallReq(string _com, string _loc, string _rccno, string _stus, string _user);

        //kapila
        [OperationContract]
        DataTable GetPOAlocItems(string _po);

        //kapila
        [OperationContract]
        DataTable GetDODetByInvItem(string _company, string _invoice, string _item);

        //kapila
        [OperationContract]
        int SaveACInstallRequest(RCC _RCC, List<Service_Req_Hdr> _ReqHdr, List<Service_Req_Det> _ReqDet, List<RCC_Det> _listrccdet, Boolean _isDealer, Boolean _isOnline, MasterAutoNumber _masterAutoNumber, out string _RccNo, out string _jobno, List<Service_JOB_HDR> _jobHdrlist = null, Int32 _warrstus = 0, Boolean _isExternal=false);
        //int SaveACInstallRequest(RCC _RCC, List<Service_Req_Hdr> _ReqHdr, List<Service_Req_Det> _ReqDet, List<RCC_Det> _listrccdet, Boolean _isDealer, Boolean _isOnline, MasterAutoNumber _masterAutoNumber, out string _RccNo);

        //kapila
        [OperationContract]
        Int32 CheckValidServiceAgent(string _item, string _com, string _loc);

        //kapila
        [OperationContract]
        List<InventorySerialN> GetIntSerList_new(string _docNo);

        //kapila
        [OperationContract]
        DataTable GetBatchwiseExpDates(string _com, string _loc, string _item, string _stus);

        //kapila
        [OperationContract]
        Int16 ConfirmTempGRN(List<InventoryHeader> _listInvH, out string _docNo);
        //kapila
        [OperationContract]
        DataTable GetPendingTemSavedGRN(string _com, string _loc, string _pono);

        //kapila
        [OperationContract]
        Int16 SaveTempGRN(InventoryHeader _inventoryMovementHeader, List<ReptPickSerials> _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub, MasterAutoNumber _masterAutoNumber, List<PurchaseOrderDelivery> _purchaseOrderDeliveryList, out string _docNo);

        //kapila
        [OperationContract]
        DataTable getTempPOItems(string _com, string _PO);

        //kapila
        [OperationContract]
        DataTable getItemByType(string _type);

        //kapila
        [OperationContract]
        DataTable GetMasterTypes(string _code);

        //kapila
        [OperationContract]
        Boolean IsAgentParaExist(string _com, string _code);

        //kapila
        [OperationContract]
        DataTable GetPendingRCC(string _com);

        //kapila
        [OperationContract]
        DataTable getRCCbySerialWar(string _ser, string _war);

        //kapila
        [OperationContract]
        DataTable GetSuplierByItem(string _com, string _item);

        //kapila
        [OperationContract]
        int Process_Return_StandBy(List<Service_TempIssue> oMainList, InventoryHeader _invOutHeader, InventoryHeader _invINHeader, List<ReptPickSerials> _reptPickOutSerials, List<ReptPickSerials> _reptPickINSerials, List<ReptPickSerialsSub> _reptPickOutSerialsSub, List<ReptPickSerialsSub> _reptPickINSerialsSub, MasterAutoNumber _inventoryAuto, MasterAutoNumber _inventoryAutoIN, MasterAutoNumber _ReqInsAuto, Int32 _seq, Int32 _seqLine, out string _err);

        //kapila
        [OperationContract]
        List<MasterItem> GetItemsByCate(string _mainCate, string _subCate, string _itmRange);

        //kapila
        [OperationContract]
        Int32 UpdateTempWaraCust(string _customercode, string _customername, string _customeraddressinvoce, string _customerphoneno, string _warrantyno);

        //kapila
        [OperationContract]
        Boolean checkDealerInvoice(string _inv);

        //kapila
        [OperationContract]
        int UpdateJobReqByRcc(string _rccno, string _aodno);

        //kapila
        [OperationContract]
        DataTable GetCustIncomeGroup(string _com, string _code);




        //kapila
        [OperationContract]
        List<Service_Charge> Get_RCC_Charge(string _com, string _loc, string _schnl, DateTime _date);

        //Serial Scan Common Control
        //Code By - Prabhath on 12/03/2012
        #region Scan Serials
        /// <summary>
        /// Return Company Item Status
        /// </summary>
        /// <param name="_company">Company Code</param>
        /// <returns>Item Status/Description</returns>
        [OperationContract]
        DataTable GetAllCompanyStatus(string _company);

        /// <summary>
        /// Return Location Bin
        /// </summary>
        /// <param name="_company">Company Code</param>
        /// <param name="_location">Location Code</param>
        /// <returns>Bin Code/Description</returns>
        [OperationContract]
        DataTable GetAllLocationBin(string _company, string _location);

        //Code By - Prabhath on 19/03/2012
        [OperationContract]
        MasterItem GetItem(string _company, string _item);

        //Code By - Miginda on 26/04/2012
        [OperationContract]
        List<MasterItemComponent> GetItemComponents(string _mainItemCode);

        //Code By - Prabhath on 26/03/2012
        [OperationContract]
        Boolean IsItemSerialized_1(string _item);

        //Code By - Prabhath on 26/03/2012
        [OperationContract]
        Boolean IsItemSerialized_2(string _item);

        //Code By - Prabhath on 26/03/2012
        [OperationContract]
        Boolean IsItemSerialized_3(string _item);

        //Code By - Prabhath on 26/03/2012
        [OperationContract]
        Boolean IsItemHaveSubItem(string _item);

        //Code By - Prabhath on 27/03/2012
        [OperationContract]
        Boolean IsUOMDecimalAllow(string _item);

        //Code By - Prabhath on 27/03/2012
        [OperationContract]
        List<InventorySerialRefN> GetItemDetailBySerial(string _company, string _location, string _serial);

        //Code By - Prabhath on 27/03/2012
        [OperationContract]
        DataTable GetAllScanSerials(string _company, string _location, string _user, Int32 _userseqno, string _doctype);

        //Code By - Prabhath on 27/03/2012
        [OperationContract]
        List<ReptPickSerials> GetAllScanSerialsList(string _company, string _location, string _user, Int32 _userseqno, string _doctype);
        [OperationContract]
        List<ReptPickSerials> GetAllScanSerialsListNew(string _company, string _location, string _user, Int32 _userseqno, string _doctype);

        //Code By - Prabhath on 27/03/2012
        [OperationContract]
        DataTable GetAllScanSubSerials(Int32 _userseqno, string _doctype);

        //Code By - Prabhath on 27/03/2012
        [OperationContract]
        List<ReptPickSerialsSub> GetAllScanSubSerialsList(Int32 _userseqno, string _doctype);

        // Nadeeka 09-03-2015
        [OperationContract]
        List<InventorySubSerialMaster> GetAvailablesubSerils(Int32 _serialid);

        // Nadeeka 11-11-2015
        [OperationContract]
        DataTable GetItemAllocationDet(string _item);


        //Nadeeka
        [OperationContract]
        DataTable GetJobForSerial(string _com, string _item, string _ser, Int32 _serid);

        // Nadeeka 09-03-2015
        [OperationContract]
        DataTable GetReservationDet(string _company, string _resNo, string _item, string _status);

        // Nadeeka 09-03-2015
        [OperationContract]
        List<InventorySubSerialMaster> GetAvailablesubSerilsMain(Int32 _serialid);

        // Nadeeka 09-03-2015
        [OperationContract]
        DataTable GetReplaceOriginalItems(string _serial);





        //TODO: HW
        //Code By - Prabhath on 28/03/2012
        [OperationContract]
        DataTable GetAllScanRequestItems(Int32 _userSeqNo);

        //TODO: HW
        [OperationContract]
        List<ReptPickItems> GetAllScanRequestItemsList(Int32 _userSeqNo);

        //Written By Prabhath on 29/03/2012
        [OperationContract]
        ReptPickHeader GetAllScanSerialParameters(string _company, string _user, Int32 _userseqno, string _doctype);

        //Written By Prabhath on 29/03/2012
        [OperationContract]
        Int16 SaveAllScanSerials(ReptPickSerials _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub);

        //Written By Prabhath on 05/04/2012
        //[OperationContract]
        //Int16 SaveInwardScanSerial(InventoryHeader _inventoryMovementHeader, List<ReptPickSerials> _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub, MasterAutoNumber _masterAutoNumber);

        //Written By Shani on 17/04/2012
        //method written by Chamal & Prabath
        //[OperationContract]
        //Int16 SaveOutwardScanSerial(InventoryHeader _inventoryMovementHeader, List<ReptPickSerials> _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub, MasterAutoNumber _masterAutoNumber);

        //Written By Chamal on 11/05/2012
        [OperationContract]
        Int16 GRNEntry(InventoryHeader _inventoryMovementHeader, List<ReptPickSerials> _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub, MasterAutoNumber _masterAutoNumber, out string _docNo);

        //Written By Chamal on 11/05/2012
        [OperationContract]
        Int16 ADJPlus(InventoryHeader _inventoryMovementHeader, List<ReptPickSerials> _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub, MasterAutoNumber _masterAutoNumber, out string _docNo, bool IsTemp = false, bool Isfixdb = false);

        //Written By Chamal on 11/05/2012
        [OperationContract]
        Int16 ADJMinus(InventoryHeader _inventoryMovementHeader, List<ReptPickSerials> _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub, MasterAutoNumber _masterAutoNumber, out string _docNo, bool IsTemp = false);

        //Written By Chamal on 28/05/2012
        [OperationContract]
        Int16 AODReceipt(InventoryHeader _inventoryMovementHeader, List<ReptPickSerials> _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub, MasterAutoNumber _masterAutoNumber, out string _docNo, bool IsTemp = false);

        ////Written By Chamal on 28/05/2012
        [OperationContract]
        Int16 SRN(InventoryHeader _inventoryMovementHeader, List<ReptPickSerials> _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub, MasterAutoNumber _masterAutoNumber, out string _docNo);

        //Written By Chamal on 11/05/2012
        [OperationContract]
        Int16 DeliveryOrderEntry(InventoryHeader _inventoryMovementHeader, List<ReptPickSerials> _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub, MasterAutoNumber _masterAutoNumber, out string documentNo, InventoryHeader _inventoryMovementHeaderGRN, List<ReptPickSerials> _reptPickSerialsGRN, List<ReptPickSerialsSub> _reptPickSerialsSubGRN, MasterAutoNumber _masterAutoNumberGRN, out string documentNoGRN, bool IsGRN, List<InvoiceVoucher> Voucher, List<Transport> _traList = null, List<string> _SoaList = null);

        // Nadeeka 26-03-2015
        [OperationContract]
        Int16 DeliveryOrderEntryQuotation_Based(InventoryHeader _inventoryMovementHeader, List<ReptPickSerials> _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub, MasterAutoNumber _masterAutoNumber, out string documentNo, InventoryHeader _inventoryMovementHeaderGRN, List<ReptPickSerials> _reptPickSerialsGRN, List<ReptPickSerialsSub> _reptPickSerialsSubGRN, MasterAutoNumber _masterAutoNumberGRN, out string documentNoGRN, bool IsGRN, List<InvoiceVoucher> Voucher, List<Transport> _traList = null);

        //Written By Chamal on 12/05/2012
        [OperationContract]
        Int16 ConsignmentReceipt(InventoryHeader _inventoryMovementHeader, List<ReptPickSerials> _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub, MasterAutoNumber _masterAutoNumber, List<PurchaseOrderDelivery> _purchaseOrderDeliveryList, out string _docNo, bool _ISTEMP = false);

        //Written By Chamal on 12/05/2012
        [OperationContract]
        Int16 ConsignmentReturn(InventoryHeader _inventoryMovementHeader, List<ReptPickSerials> _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub, MasterAutoNumber _masterAutoNumber, out string documentNo, bool _ISTEMP = false);

        //Written By Chamal on 07/07/2012 -
        [OperationContract]
        ReptPickSerials GetAvailableSerIDInformation(string _com, string _loc, string _itemCode, string _serial1, string _serial2, string _serialid);


        #endregion

        //----------------
        //Editor -shani
        #region Consignment return note
        //Editor -shani
        //11-5-2012
        [OperationContract]
        DataTable getDetail_on_serial_Supplier(string _company, string _location, string supplierCD, string serial_1, string serial_2);

        //9-5-2012
        [OperationContract]
        List<ReptPickSerials> GET_ser_for_ItmCD_Supplier(string company, string location, string binCode, string itemCode, string supplier);

        //Consignment return note
        #endregion


        //Editor -Shani 
        #region Status Change
        //Editor -Shani
        //9-5-2012
        [OperationContract]
        Boolean Update_sat_itm_DO_qty(string invoiceNo, Int32 item_Line, int DO_qty);

        [OperationContract]
        Int32 GET_SEQNUM_FOR_INVOICE(string doc_type, string company, string invoiceNO, int direction_);

        //Add by Chamal 13/03/2013
        [OperationContract]
        Int32 Get_Scan_SeqNo(string _company, string _location, string _doctype, string _user, int _direction, string _docNo);

        //8-5-2012
        [OperationContract]
        List<ReptPickSerials> Search_serials_for_itemCD(string company, string location, string itemCode, string pbook, string pblvl);
        //2-5-2012
        [OperationContract]
        Boolean Update_serialID_INS_AVAILABLE(string compny, string location, string itemCD, Int32 ser_ID, int availability);

        [OperationContract]
        ReptPickSerials Get_all_details_on_serialID(string company, string location, string bin, string itemCode, int serial_ID);

        //30/4/2012
        [OperationContract]
        DataTable Get_location_by_code(string com_code, string location);

        [OperationContract]
        DataTable Get_location_by_code_all(string com_code, string location, int _all);

        //28/4/2012
        [OperationContract]
        Int16 InventoryStatusChange(InventoryHeader _inventoryMovementHeaderMinus, InventoryHeader _inventoryMovementHeaderPlus, List<ReptPickSerials> _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub, MasterAutoNumber _masterAutoNumberMinus, MasterAutoNumber _masterAutoNumberPlus, out string _minusDocNo, out string _plusDocNo);

        //Code by Chamal 17/05/2013
        [OperationContract]
        string InventoryStatusChangeEntry(InventoryHeader _inventoryMovementHeaderMinus, InventoryHeader _inventoryMovementHeaderPlus, List<ReptPickSerials> _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub, MasterAutoNumber _masterAutoNumberMinus, MasterAutoNumber _masterAutoNumberPlus, List<InventoryRequestItem> _reqItems, out string _minusDocNo, out string _plusDocNo, bool isBinToBinTransfer = false, bool isDeValProcess = false,bool checkValid=false);

        //on 27/4/2012
        [OperationContract]
        string Get_item_description(string itemCode);

        [OperationContract]
        Boolean Update_TEMP_PICK_SER(string compny, string location, Int32 userseq_no, Int32 ser_id, string newstatus, string newremarks);

        //on 2-5-2012

        [OperationContract]
        Boolean Del_temp_pick_serdummy(string compny, string location, Int32 userSeqNo, Int32 ser_id, string itemCd, string bin);

        //Code by Chamal 15-03-2013
        [OperationContract]
        Boolean Del_temp_pick_itm(Int32 _seqNo, string _itemCode, string _itemStatus, Int32 _itemLine, Int32 _type);

        // on 26/4/2012
        //modify rukshan 05/oct/2015 - updated by akila 2017/09/12
        [OperationContract]
        Boolean Del_temp_pick_ser(string compny, string location, Int32 userSeqNo, Int32 ser_id, string Item, string _mainSerila, ReptPickItems _pickItem = null);
        [OperationContract]
        Boolean Update_inrser_INS_AVAILABLE(string compny, string location, string itemCD, string ser_1, int availability);

        // on 25/4/2012
        [OperationContract]
        List<ReptPickSerials> Search_by_serial(string company, string location, string itemCode, string bin, string serial_1, string serial_2);

        [OperationContract]
        List<ReptPickSerials> Get_all_serials_for_itemCD(string company, string location, string binCode, string itemCode);//searching button

        [OperationContract]
        ReptPickSerials Get_all_details_on_serial(string company, string location, string bin, string itemCode, string serial_1);
        //on 24/4/2012
        [OperationContract]
        Dictionary<string, string> Get_all_ItemSatus();

        [OperationContract]
        DataTable getDetail_on_serial1(string _company, string _location, string _serial1);

        [OperationContract]
        DataTable getMovementSerial(string _item, string _serial1);

        //on 23/4/2012
        [OperationContract]
        List<string> Get_all_Itemcodes();

        [OperationContract]
        List<string> GetAll_binCodes_for_loc(string company, string location);
        #endregion

        //Editor -Shani on 5/4/2012
        #region Stock Adjustment
        //Editor -Shani on 29/5/2012
        [OperationContract]
        string Get_Adj_SubTypes_description(string subtypeCd);
        [OperationContract]
        List<string> GetAll_Adj_SubTypes();
        //Editor -Shani on 5/4/2012
        [OperationContract]
        List<string> GetAll_Adj_SeqNums_for_user(string usrID, string doc_type, int direction_, string company_);

        //Editor -Shani on 9/4/2012
        [OperationContract]
        int Generate_new_seq_num(string usrID, string doc_type, int direction_, string company_);

        //Editor -Shani on 9/4/2012
        [OperationContract]
        Int32 SaveSeq_to_TempPickHDR(ReptPickHeader Rph);

        #endregion

        #region RCC
        [OperationContract]
        int DeleteRCCLocation(List<RCCLocations> DEL_LIST);

        [OperationContract]
        int UpdateRCCLocations(List<RCCLocations> _rccLoc_LIST);

        [OperationContract]
        DataTable GetLocsByAgent(string _com, string _agent);

        [OperationContract]
        List<RCC> GetRCC();

        [OperationContract]
        RCC GetRccByNo(string _RCCNo);

        [OperationContract]
        List<MasterRCCDef> GetRCCDef(string _Type);

        [OperationContract]
        List<MasterBusinessEntity> GetServiceAgent(string _Type);

        [OperationContract]
        List<MasterBusinessEntity> GetMobCustSearch(string _Type, string _code, string _com);


        //dualanga 2018/10/18
         [OperationContract]
         DataTable GetMobSearchItem(string type, string code);


        [OperationContract]
         int SaveRCC(RCC _RCC, List<Service_Req_Hdr> _ReqHdr, List<Service_Req_Det> _ReqDet, List<Service_Req_Def> _defList, List<ImageUploadDTO> oMainList, Boolean _isDealer, Boolean _isOnline, MasterAutoNumber _masterAutoNumber, out string _RccNo, out string _Aod);

        //kapila 23/4/2012
        [OperationContract]
        int Update_RCC_JobOpen(RCC _RCC);

        //kapila 25/4/2012
        [OperationContract]
        int Update_RCC_Repair(RCC _RCC, out string _error);

        //kapila 26/4/2012
        [OperationContract]
        int Update_RCC_complete(RCC _RCC, InventoryHeader _invHeader, List<ReptPickSerials> _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub, out string _errMsg);
        #endregion

        #region InventoryRequest(MRN)

        //Miginda - 30/03/2012
        [OperationContract]
        int SaveInventoryRequestData(InventoryRequest _inventoryRequest, MasterAutoNumber _mastAutoNo, out string _docNo);

        //Chamal - 10/07/2012
        [OperationContract]
        int SaveGRANRequestData(InventoryRequest _inventoryRequest, MasterAutoNumber _mastAutoNo, out string _docNo);

        //Miginda - 31/03/2012
        [OperationContract]
        List<InventoryRequest> GetAllInventoryRequestData(InventoryRequest _inventoryRequest);

        //Chamal 06/06/2012
        [OperationContract]
        DataTable GetAllInventoryRequestDataTable(InventoryRequest _inventoryRequest);

        //Miginda - 02/04/2012
        [OperationContract]
        InventoryRequest GetInventoryRequestData(InventoryRequest _inventoryRequest);

        //Miginda - 18/05/2012
        [OperationContract]
        int UpdateInventoryRequestStatus(InventoryRequest _inventoryRequest, List<InventoryRequestItem> _itemList = null);

        //Chamal 06/06/2012
        [OperationContract]
        DataTable GetInventoryRequestItemDataByReqNoTable(string _reqNo);

        //kapila
        [OperationContract]
        DataTable GetJobNoByReqNo(string _reqNo);

        //Chamal 06/07/2012
        [OperationContract]
        DataTable GetAllGRANSerialsTable(string _company, string _loc, string _documentType, string _docNo);

        //Chamal 06/07/2012
        [OperationContract]
        List<InventoryRequestSerials> GetAllGRANSerialsList(string _company, string _loc, string _documentType, string _docNo);

        #endregion

        //kapila
        [OperationContract]
        DataTable GetSerialDetailsBySerial(string _com, string _loc, string _itemCode, string _serial);


        //Nadeeka
        [OperationContract]
        DataTable GetSerialDetailsBySerialwithoutItem(string _com, string _loc, string _serial);


        //Nadeeka
        [OperationContract]
        DataTable GetSerialDetailsBySerialCompany(string _com, string _itemCode, string _serial);

        [OperationContract]
        DataTable GetSerialDetailsBySerial1(string _com, string _loc, string _itemCode, string _serial);


        //Written By P.Wijetunge on 9/4/2012
        [OperationContract]
        MasterAutoNumber GetAutoNumber(string _module, Int16? _direction, string _startChar, string _catType, string _catCode, DateTime? _modifyDate, Int32? _year);

        //Written By P.Wijetunge on 9/4/2012
        [OperationContract]
        Int16 UpdateAutoNumber(MasterAutoNumber _masterAutoNumber);

        //Written By P.Wijetunge on 10/4/2012
        [OperationContract]
        DataTable GetAllPurchaseOrder(string _company, string _adminTeam, string _status);

        //Written By P.Wijetunge on 10/4/2012
        [OperationContract]
        DataTable GetAllPurchaseOrderDetail(string _company, string _adminTeam, string _poNo);

        //Written By P.Wijetunge on 10/4/2012
        [OperationContract]
        Int16 SavePickedHeader(ReptPickHeader _scanheaderNew);

        //Written By P.Wijetunge on 10/4/2012
        [OperationContract]
        Int16 SavePickedItems(List<ReptPickItems> _scanitemNew);

        //Written By P.Wijetunge on 10/4/2012 - Composite function for save both header and the details
        [OperationContract]
        Int16 SavePickedHeaderItemDetail(ReptPickHeader _reptPickHeader, List<ReptPickItems> _reptPickItems);

        //Written By P.Wijetunge on 10/4/2012 - Composite function for save both header and the details
        [OperationContract]
        List<PurchaseOrderDetail> GetAllPurchaseOrderDetailList(string _company, string _adminTeam, string _poNo);

        //Written By P.Wijetunge on 02/5/2012 
        [OperationContract]
        DataTable GetItemComponentTable(string _mainItemCode);

        //Written By P.Wijetunge on 02/5/2012 
        [OperationContract]
        DataTable GetAvailableItemStatus(string _company, string _location, string _bin, string _item, string _serial);

        //kapila
        [OperationContract]
        DataTable GetNoOfPendingRCC(string _com, string _loc, DateTime _date);

        //kapila
        [OperationContract]
        Boolean IsValidServiceAgent(string _com, string _loc, string _agent);

        //Written By Nadeeka 01/01/2013
        [OperationContract]
        DataTable GetDeliveryOrderDetailDt(string _company, string _invoice, int _line);

        //Written By Nadeeka 01/01/2013
        [OperationContract]
        DataTable GetUserNameByUserID(string _UserID);

        //Written By Nadeeka 16-02-2015
        [OperationContract]
        int CheckSubSerialAvl(string _subser, string _subItem);


        //Written By Nadeeka 16-02-2015
        [OperationContract]
        int checkReqItemAllowCompany(string _item, string _com);

        //Written By Nadeeka 16-02-2015
        [OperationContract]
        int checkReqItemAllow(string _item);


        //Written By Nadeeka 16-02-2015
        [OperationContract]
        Boolean CheckJobInventoryBalance(string _com, string _loc, string _job, Int16 _jobline, List<ReptPickItems> _scanitemNew);

        //Written By Nadeeka 07/06/2013
        [OperationContract]
        DataTable GetCrditNoteforWarrantyClaim(string in_AdjNo);

        //Written By Nadeeka 07/06/2013
        [OperationContract]
        DataTable GetCrditNoteforWarrantyClaimScm2(string in_AdjNo);





        //written by Nadeeka 2013-04-24
        [OperationContract]
        DataTable ServicejobCard(string _jobNo);
        //written by Nadeeka 2013-04-26
        [OperationContract]
        DataTable ServicejobCardHistory(string _jobNo);

        //written by Nadeeka 2013-04-26
        [OperationContract]
        DataTable ServicejobCardDefect(string _jobNo);

        //Written By Nadeeka 23/02/2013
        [OperationContract]
        DataTable GetPODetails(string _po);

        //Written By Nadeeka 23/02/2013
        [OperationContract]
        DataTable GetPODeliveryDetails(string _po);


        //Written By Nadeeka 08/03/2013
        [OperationContract]
        DataTable DamgeGoodApproval(DateTime in_FromDate, DateTime in_ToDate, string in_Location_code, string in_Company, string in_Brand, string in_Model, string in_Itemcode, string in_Itemcat1, string in_Itemcat2, string in_Itemcat3, string in_user);


        //kapila
        [OperationContract]
        DataTable GetOutDocNoByJobNo(string _com, string _loc, string _jobno, out string _docno, Int32 _isExternal);

        //Written By Nadeeka 09/01/2013
        [OperationContract]
        DataTable GetRCCbyNoTable(string _rcc);
        //Written By Nadeeka 09/01/2013
        [OperationContract]
        DataTable GetMoveSubTypeTable(string _type);

        //Written By Chamal 12/03/2013
        [OperationContract]
        DataTable GetMoveSubTypeAllTable(string _maintype, string _subtype);

        //Written By Nadeeka 10/01/2013
        [OperationContract]
        DataTable GetAdhochdrTable(string _ref);

        //Written By Nadeeka 10/01/2013
        [OperationContract]
        DataTable GetAdhocdetTable(string _ref);

        //Written By Sanjeewa 30/10/2013
        [OperationContract]
        DataTable FixedAssetBalDetails(string _User);

        //Written By Nadeeka 10/01/2013
        [OperationContract]
        DataTable GetInventorySerialbyId(string _serId, string _loc);

        //Written By Nadeeka 04/01/2013
        [OperationContract]
        DataTable GetInventoryRequestByReqNo(string _reqNo);



        //Written By Nadeeka 26/02/2013
        [OperationContract]
        DataTable Get_all_LocationsTable(string company);

        //Written By Nadeeka 04/01/2013
        [OperationContract]
        DataTable GetInventoryRequestSerialsBySeqNo(string _seqNo);



        //Written By Nadeeka 04/01/2013
        [OperationContract]
        DataTable GetInventoryRequestItemsBySeqNo(string _seqNo);

        #region Consignment Receipt Note

        //Code By M.Geeganage on 12/5/2012 
        [OperationContract]
        List<PurchaseOrder> GetAllPendingConsignmentRequestData(PurchaseOrder _paramPurchaseOrder);

        //Code By Chamal De Silva on 26/02/2013 
        [OperationContract]
        DataTable GetAllPendingPurchaseOrderDataTable(PurchaseOrder _paramPurchaseOrder);

        //Code By Chamal De Silva on 26/02/2013 
        [OperationContract]
        DataTable GetPOItemsDataTable(string _comCode, string _poNo, Int32 _all);

        //Code By M.Geeganage on 12/5/2012 
        [OperationContract]
        PurchaseOrder GetConsignmentRequestDetails(string _companyCode, string _docNo, string _locCode);

        //Code By Chamal on 07/02/2013 
        [OperationContract]
        List<PurchaseOrderDelivery> GetConsignmentItemDetails(string _companyCode, string _docNo, string _locCode);

        //Code By Miginda on 08/05/2012
        [OperationContract]
        Int32 GetRequestUserSeqNo(string _companyCode, string _docNo);

        //Code By Miginda on 08/05/2012
        [OperationContract]
        Int32 IsExistInSerialMaster(string _companyCode, string _itemCode, string _serialNo1);

        //Code By Chamal on 06/Feb/2014
        [OperationContract]
        int IsExistInSerial1(string _com, string _itemCode, string _serialNo1);

        //Code By Chamal on 06/Feb/2014
        [OperationContract]
        int IsExistInSerial2(string _com, string _itemCode, string _serialNo2);

        //Code By Miginda on 08/05/2012
        [OperationContract]
        Int32 IsExistInTempPickSerial(string _companyCode, string _userSeqNo, string _itemCode, string _serialNo1);

        //Code By Miginda on 08/05/2012
        [OperationContract]
        Int32 GetSerialID();


        //Code By Miginda on 14/05/2012
        [OperationContract]
        List<string> GetBinCodesforInventoryInward(string company, string location);

        //Code By Miginda on 14/05/2012
        [OperationContract]
        Int16 SavePickedSerialsDecimalItems(ReptPickSerials _scanserNew);



        #endregion

        #region Purchase Order
        //Code by D.Samarathunga on 15/05/2012
        [OperationContract]
        List<MasterItemTax> GetItemTax(string _company, string _item, string _status, string _taxCode, string _taxRateCode);

        //kapila
        [OperationContract]
        DataTable GetSerialIDByJob(string _job, string _item);

        [OperationContract]
        bool IsValidCompanyItem(string _company, string _Item, Int16 _active);

        [OperationContract]
        bool IsValidSupplier(string _company, string _Supplier, Int16 _active, string _type);

        [OperationContract]
        bool IsValidItemStatus(string _itmStatus);

        [OperationContract]
        Int16 SaveNewPO(PurchaseOrder _NewPO, List<PurchaseOrderDetail> _NewPOItems, List<PurchaseOrderDelivery> _NewPODel, List<PurchaseOrderAlloc> _NewPOAloc, MasterAutoNumber _masterAutoNumber, List<PurchaseReq> _PurchaseReq, List<InventoryRequestItem> _Porequest, out string docno, bool _saveSalesOrder = false);

        [OperationContract]
        Int16 UpdateSavedPO(PurchaseOrder _UpdatePO, List<PurchaseOrderDetail> _UpdatePOItems, List<PurchaseOrderDelivery> _UpdatePODel, Int32 _SeqNo);

        //[OperationContract]
        //Int16 DeletePODel(Int32 _SeqNo);

        [OperationContract]
        Int16 UpdatePOStatus(PurchaseOrder _UpdatePO);

        [OperationContract]
        Int16 UpdatePOStatusNew(PurchaseOrder _UpdatePO, List<InventoryRequestItem> _Porequest);

        [OperationContract]
        List<QoutationDetails> GetSupplierQuotation(string _com, string _sup, string _type, string _subtype, DateTime _date, decimal _qty, string _status, string _item);

        [OperationContract]
        PurchaseOrder GetPOHeader(string _com, string _docno, string _type);

        [OperationContract]
        List<PurchaseOrderDetail> GetPOItems(PurchaseOrderDetail _paramPOItems);

        [OperationContract]
        List<PurchaseOrderDelivery> GetPODelItems(PurchaseOrderDelivery _paramPODelItems);

        #endregion

        //hw prabhath
        [OperationContract]
        List<InventoryLocation> GetItemInventoryBalance(string _company, string _location, string _item, string _status);

        [OperationContract]
        List<ReptPickSerials> GetNonSerializedItemInTopOrder(string _company, string _location, string _bin, string _item, string _status, decimal _qty);


        //14-5-2012
        [OperationContract] //to update the qty of dummy
        Boolean Update_temp_pick_serdummy(string user, string compny, string location, Int32 userSeqNo, string serial_1, string itemCd, string bin, Decimal newQty);//serial_1 is "_"
        //14-5-2012
        [OperationContract] //to get perticular scanned serial from temp_pick_ser
        List<ReptPickSerials> Get_TEMP_PICK_SER(string _company, string _location, string _user, Int32 _userseqno, Int32 ser_id, string serial_1, string itemCD, string binCD);

        [OperationContract]
        List<InventoryRequest> GetAllMaterialRequestsList(InventoryRequest _inventoryRequest);

        [OperationContract]
        DataTable GetAllMaterialRequestsTable(InventoryRequest _inventoryRequest);


        //Written By Prabhath
        [OperationContract]
        InventorySerialMaster GetSerialMasterDetailBySerialID(Int32 _serialID);

        //Written By Prabhath - updated by akila 2017/09/13
        [OperationContract]
        Int32 DeleteTempPickSerialByItem(string _company, string _location, Int32 _userSeqNo, string _item, string _status, ReptPickItems _pickItem = null);

        //Written By Prabhath
        [OperationContract]
        bool IsValidItem(string _company, string _item);

        #region Inventory Inward Entry
        //Chamal 24/05/2012
        [OperationContract]
        DataTable GetAllPendingInventoryOutwardsTable(InventoryHeader _inventoryRequest);

        [OperationContract]
        String GetDefaultBinCode(String _com, String _loc);

        [OperationContract]
        List<ReptPickSerials> GetOutwarditems(string _loc, string _defbin, ReptPickHeader _scanheaderNew, out string _unavailableitemlist);
        #endregion

        //kapila 30/5/2012
        #region Manual docs
        [OperationContract]
        DataTable GetManualDocs(string _Loc, Int16 _IsRec);

        [OperationContract]
        List<ManualDocDetail> GetManualDocDet(string _Ref);

        [OperationContract]
        Int16 SavePickedManualDocDetail(string _refNo, string _Loc, string _user, string _Status);

        [OperationContract]
        Int16 Delete_Selected_Item_Line(Int32 _BkNo, string _prefix, string _USer);

        [OperationContract]
        Int16 UpdateManualDocs(string _RefNo, string _USer);

        [OperationContract]
        Int32 UpdateTransferStatus(string _RefNo, string _User, string _TransLoc);

        [OperationContract]
        Int32 SaveManualDocReceipt(InventoryHeader _inventoryMovementHeader, List<ReptPickSerials> _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub, MasterAutoNumber _masterAutoNumber, out string _docNo);

        [OperationContract]
        int GetManualDocSerialList(string _user, string _refNo, Int32 _seqNo, string _defBIN, string _Comp, string _Loc);
        #endregion

        //Written By Prabhath on 07/06/2012
        [OperationContract]
        List<ReptPickItems> GetAllPickItem(string _company, Int32 _userSeqNo, string _docType, string _baseDoc, string _reqitem, string _reqstatus);

        //Written By Prabhath on 07/06/2012
        //Add trnsport mode 26 Apr 2016 Lakshan
        [OperationContract]
        Int32 SaveCommonOutWardEntry(string _fromCompany, string _fromProfit, string _toCompany, string _requestNo, InventoryHeader _inventoryHeader, MasterAutoNumber _inventoryAuto, InvoiceHeader _invoiceHeader, MasterAutoNumber _invoiceAuto, List<ReptPickSerials> _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub, out string _genMessage, out string _genSalesDocument, out string _genInventoryDocument, bool _isGRAN, bool _isGRNFromDIN, List<Transport> _traList = null, Boolean withcoonection = false);

        //kapila 12/6/2012
        [OperationContract]
        InventorySerialRefN GetAvailableWarranty(string _comp, string _Loc, string _Item, string _Serial);

        //kapila 13/6/2012
        [OperationContract]
        DataTable GetIssuedWarranty(Int32 _seq, Int32 _itmLine, Int32 _batchLine, Int32 _serLine);

        //kapila 14/6/2012
        [OperationContract]
        DataTable GetIssuedWarrantyOth(string _comp, string _Loc, string _Item, string _Serial);

        //shani 14/06/2012
        [OperationContract]
        string Get_default_binCD(string company, string location);

        //Prabhath 15/06/2012
        [OperationContract]
        List<ReptPickItems> IsPickQtyExceedRequestQty(Int32 _userSeq, string _reqItem, string _reqStatus);

        //kapila 18/6/2012
        [OperationContract]
        List<ReptPickSerials> Get_all_Serial_details(string _RefNo, string _USer, string _Comp, string _Loc);

        //kapila 18/6/2012
        [OperationContract]
        Int16 SaveAllScanManualDocPages(ReptPickSerials _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub);

        //kapila 19/6/2012
        [OperationContract]
        Int16 Manual_Doc_Transfer(InventoryHeader _inventoryMovementHeader, List<ReptPickSerials> _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub, MasterAutoNumber _masterAutoNumber, out string _docNo);

        //kapila 21/6/2012
        [OperationContract]
        Boolean IsValidManualDocument(string _Comp, string _Loc, string _DocType, Int32 _NextNo);
        // Nadeeka
        [OperationContract]
        Boolean IsValidManualDocument_prefix(string _Comp, string _Loc, string _DocType, Int32 _NextNo, string _prefix);

        [OperationContract]
        Int32 GetNextManualDocNo(string _Comp, string _Loc, string _DocType);

        [OperationContract]
        Int32 UpdateManualDocNo(string _Comp, string _Loc, string _DocType, Int32 _DocNo, string _TxnNo);

        //kapila 26/2/2013
        [OperationContract]
        Int32 Update_Manual_DocNo(string _Loc, string _DocType, Int32 _DocNo, string _TxnNo);

        [OperationContract]
        Boolean IsRccSerialFound(string _Item, string _Serial);

        //kapila
        [OperationContract]
        Boolean Check_Temp_coll_Man_doc_dt(string _Comp, string _User, string _Loc, string _item, string _Prefix, Int32 _RecNo, string _module);

        [OperationContract]
        int save_temp_existing_receipt_det(string _Comp, string _User, string _Loc, string _item, string _Prefix, Int32 _Recno, string _module);

        [OperationContract]
        Int32 delete_temp_current_receipt_det(string _Comp, string _User, string _module);

        [OperationContract]
        Int32 delete_temp_current_receipt(string _Comp, string _User, string _Prefix, Int32 _RecNo);

        //kapila 17/7/2012
        [OperationContract]
        int SaveManualDocPages(string _Comp, Int32 _userseqno, string _refNo);


        //Written By Prabhath 16/07/2012
        [OperationContract]
        List<InventoryBatchN> GetDeliveryOrderDetail(string _company, string _invoice, int _line);

        //sachith 2012/07/19
        [OperationContract]
        DataTable Get_all_Items();

        //sachith 2012/07/19
        [OperationContract]
        DataTable Get_all_manual_docs_by_type(string _Comp, string _loc, string _code);

        //sachith 2012/07/19
        [OperationContract]
        int UpdateGntManDocDt(string _Comp, string _loc, string _code, string _type, int _current, string _prefix, string _bookNo);

        //sachith 2012/07/19
        [OperationContract]
        int UpdateGntManDocPages(string _Comp, string _prefix, string _loc, string _code, int _current, int _last, string _user, string _rmk);

        [OperationContract]
        BusinessObjects.MasterItem GetItemByModel(string _model);
        /// <summary>
        /// added by DULANGA 2016-1-23
        /// </summary>
        /// <param name="_model"></param>
        /// <returns></returns>
        [OperationContract]
        List<BusinessObjects.MasterItem> GetAllItemByModel(string _model);

        //Written By Prabhath 21/07/2012
        [OperationContract]
        List<ReptPickSerials> GetInvoiceSerial(string _company, string _location, string _user, string _session, string _defBin, string _invoice, int _baseRefline);

        //Written By Prabhath 21/07/2012
        [OperationContract]
        List<InventoryRequestItem> GetMaterialRequestItemByRequestNoList(string _reqNo);

        //Written By Prabhath 21/07/2012
        [OperationContract]
        DataTable GetMaterialRequestItemByRequestNoTable(string _reqNo);


        //Written By Prabhath 11/08/2012
        [OperationContract]
        HpRevertHeader GetRevertHeaderfromAccountnoForRelease(string _company, string _profitcenter, string _accountno, Int16 _revertstatus);

        //Written By Prabhath 11/08/2012
        [OperationContract]
        List<ReptPickSerials> GetAdjustmentDetailFromRevertNo(string _company, string _accountno, string _revertno);

        //Written By Prabhath 11/08/2012
        [OperationContract]
        List<MasterCompanyItemStatus> GetAllCompanyStatuslist(string _company);

        //Written By Chamal de silva on 23/08/2012
        [OperationContract]
        Boolean CheckUserPermission(string _user, string _company, string _party, string _permissionCode);

        //kapila 24/8/2012
        [OperationContract]
        DataTable GetRCCSerialSearchData_Customer(string _com, string _loc, int _isSameLoc, int _isStockItem, string _searchText, string _searchCriteria);


        [OperationContract]
        DataTable GetRCCSerialSearchData_STN(string _com, string _loc, int _isSameLoc, int _isStockItem, string _searchText, string _searchCriteria, out string _err);

        //sachith 27/08/2012
        [OperationContract]
        DataTable GetLocationByType(string _initialSearchParams, string _searchCatergory, string _searchText);


        //Written By Prabhath on 01/09/2012
        [OperationContract]
        List<InventorySerialN> GetDeliveredSerialDetail(string _company, string _invoiceno);

        //Written By Prabhath on 03/09/2012
        [OperationContract]
        List<ReptPickSerials> GetInvoiceAdvanceReceiptSerial(string _company, string _invoiceno);

        #region FixAsset Or Adhoc Request And Approve

        //Written By Shani on 10/09/2012
        [OperationContract]
        Int32 Save_Adhocheader(InventoryAdhocHeader adhocHdr);


        //Written By Shani on 10/09/2012
        [OperationContract]
        Int32 Save_AdhocDetail(List<InventoryAdhocDetail> adhocdet);

        //Written By Shani on 10/09/2012
        [OperationContract]
        List<ReptPickSerials> GET_ser_FOR_STATUS(string company, string location, string itemCode, string ItmStatus);

        //Written By Shani on 10/09/2012
        [OperationContract]
        // Int32 Save_Adhoc_Request(InventoryAdhocHeader adhoc_hdr, List<InventoryAdhocDetail> adhoc_Det);
        Int32 Save_Adhoc_Request(InventoryAdhocHeader adhoc_hdr, List<InventoryAdhocDetail> adhoc_Det, List<ReptPickSerials> requestedSerialId_list, out string RefNo);

        //Written By Shani on 11/09/2012
        [OperationContract]
        // 'Int32 status' should be minus value if not going to search by status
        List<InventoryAdhocDetail> GET_adhocDET_byRefNo(string company, string location, Int32 type_, string RefNo, Int32 status, out InventoryAdhocHeader adhocHDR);

        //Written By Shani on 11/09/2012
        [OperationContract]
        Int32 Save_Adhoc_Approve(InventoryAdhocHeader adhoc_hdr, List<InventoryAdhocDetail> adhoc_Det_approved);

        //Written By Shani on 12/09/2012
        [OperationContract]
        Int32 Save_Adhoc_Confirm(InventoryAdhocHeader adhoc_hdr, List<InventoryAdhocDetail> adhoc_Det_Confirmed);

        //Written By Shani on 15/09/2012
        [OperationContract]
        Int32 Save_FGAP_confirmation(InventoryAdhocHeader adhoc_hdr, List<InventoryAdhocDetail> adhoc_Det_Confirmed, List<RecieptHeader> receiptHeaderList, List<RecieptItem> receipItemList, List<HpTransaction> transactList, MasterAutoNumber receipAuto, MasterAutoNumber tranxAuto, string loc, string pc, out string receiptNoOut);

        #endregion FixAsset Or Adhoc Request And Approve

        //Written By Prabhath on 11/09/2012
        [OperationContract]
        MasterItemBlock GetBlockedItem(string _company, string _profit, string _item);

        //[OperationContract]
        //Int16 DeliveryOrder_Auto(InventoryHeader _inventoryMovementHeader, List<ReptPickSerials> _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub, MasterAutoNumber _masterAutoNumber, out string documentNo, InventoryHeader _inventoryMovementHeaderGRN, List<ReptPickSerials> _reptPickSerialsGRN, List<ReptPickSerialsSub> _reptPickSerialsSubGRN, MasterAutoNumber _masterAutoNumberGRN, out string documentNoGRN);

        //darshana 12/09/2012
        [OperationContract]
        InventorySerialN GetDeliveredSerialForItem(string _company, string _invoiceno, string _item, string _serial);

        [OperationContract]
        DataTable GetMRNItem(string _company, string _loc, string _itm);

        //Written By Prabhath on 18/09/2012
        [OperationContract]
        Int32 CheckAvailableSerial(string _company, string _location, string _item, string _description, string _brand, string _model, string _serial, string _warranty);

        //Written By Prabhath on 18/09/2012
        [OperationContract]
        DataTable GetAvailableSerialShortDetail(string _company, string _location, string _item, string _description, string _brand, string _model, string _serial, string _warranty);

        //Written By Prabhath on 18/09/2012
        [OperationContract]
        List<ReptPickSerials> GetNonSerializedItemRandomly(string _company, string _location, string _item, string _status, decimal _qty);

        //Written By Prabhath on 18/09/2012
        [OperationContract]
        MasterItemWarrantyPeriod GetItemWarrantyDetail(string _item, string _status);



        #region stock adjesment unit cost
        //sachith
        //2012/09/21
        [OperationContract]
        DataTable GetTemSerStatus(string _com, string _loc, string _item, int _seq);

        [OperationContract]
        DataTable GetTemSerByCodeStatus(string _com, string _loc, string _item, string _stus, int _seq);

        [OperationContract]
        int UpdateUnitCost(string _com, string _loc, string _item, string _stus, string _ser, decimal _cost, int _seq);

        #endregion

        //Written By Prabhath on 24/09/2012
        //[OperationContract]
        //DataTable GetCompeleteIGetOtherShopDOtemfromAssambleItem(string _item);

        // Nadeeka 10-11-2015
        [OperationContract]
        DataTable GetItemfromAssambleItem(string _item);

        [OperationContract]
        DataTable GetCompeleteItemfromAssambleItem(string _item);




        //Written By Sanjeewa on 07/10/2013
        [OperationContract]
        DataTable GetExchangeDetails(string _DocNo);

        // Nadeeka 28-09-2015
        [OperationContract]
        Int32 CancelOutwardDoc(string _doc, string _user, string _quo, out string _error);

        //Written By Sanjeewa on 03/02/2014
        [OperationContract]
        DataTable GetFATDetails(DateTime _fromDate, DateTime _toDate, string _User, string _com, string _pc);



        //Written By Sanjeewa on 27/11/2013
        [OperationContract]
        string getItemDescription(string _itemcode);




        //Written By Sanjeewa on 29/05/2013
        [OperationContract]
        DataTable GetFIFONotFollowedDetails(string _user, string _brand, string _model, string _itemcode, string _Itemstatus,
            string _itemcat1, string _itemcat2, string _itemcat3, DateTime _asatdate, string _com, string _loc);






        //Written By Sanjeewa on 03/03/2013
        [OperationContract]
        DataTable GetSerialBalance_Asat(DateTime _asatDate);




        [OperationContract]
        DataTable GetModuleStatus(string _module);

        [OperationContract]
        DataTable GetDINList(string _com, string _loc);

        //sachith
        //2012/10/10
        [OperationContract]
        DataTable GetItemByAll(string _brand, string _cat1, string _cat2, string _cat3);

        [OperationContract]
        DataTable GetSerialByItem(string _brand, string _cat1, string _cat2, string _cat3, string _itm);


        //Shani
        //2012/11/01
        [OperationContract]
        DataTable GetLOC_from_Hierachy(string com, string channel, string subChannel, string area, string region, string zone, string pc_code);

        //Shani 12/11/2012       
        [OperationContract]
        InventoryHeader Get_Int_Hdr(string DocNo);

        //Chamal 15/03/2013       
        [OperationContract]
        List<ReptPickSerials> Get_Int_Ser(string _docNo);

        //Shani 12/11/2012       
        [OperationContract]
        DataTable Get_Int_Itm(string DocNo);

        //Shani 12/11/2012       
        [OperationContract]
        DataTable Get_SerialOfDoc(string docNo, string itemCode);

        //Shani 12/11/2012       
        [OperationContract]
        //Int32 SaveAOD_OutWardEntry(string _fromCompany, string _fromProfit, string _toCompany, InventoryHeader _inventoryHeader, MasterAutoNumber _inventoryAuto, List<ReptPickSerials> _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub, bool _isOtherLocAutoIn, out string _genMessage, out string _genInventoryDocument);
        Int32 SaveAOD_OutWardEntry(string _fromCompany, string _fromProfit, string _toCompany, InventoryHeader _inventoryHeader, MasterAutoNumber _inventoryAuto, List<ReptPickSerials> _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub, bool _isOtherLocAutoIn, out string _genMessage, out string _genInventoryDocument, out string lastOutDocNo, out string lastInDocNo);

        //Shani 15/11/2012       
        [OperationContract]
        Int32 Correct_InvalidTransaction(string IssuedLoc, string InvalidLoc, string CorrectLoc, InventoryHeader invHdr_in, List<ReptPickSerials> PickSerialsList, List<ReptPickSerialsSub> reptPickSerials_SubList, MasterAutoNumber masterAutoNum_in, MasterAutoNumber masterAutoNum_out, out string newAOD_out_docNo);

        //Written By Prabhath on 23/11/2012
        [OperationContract]
        Int32 DeleteTempPickItembyItem(Int32 _userseqno, string _item, string _status);

        //Written By Prabhath on 23/11/2012
        [OperationContract]
        Int32 UpdateTempPickItem(Int32 _userseqno, string _item, string _status, decimal _qty);


        //Written By Prabhath on 23/11/2012
        [OperationContract]
        Int32 IsExistInWarrantyMaster(string _companyCode, string _warrantyno);

        //Written By Prabhath on 23/11/2012
        [OperationContract]
        Int32 IsExistWarrantyInTempPickSerial(string _companyCode, string _warranty);

        //Written By Prabhath on 23/11/2012
        [OperationContract]
        DataTable GetMultipleItemforOneSerial(string _company, string _location, string _item, string _serial, string _warranty);

        //Written By Prabhath on 11/01/2013
        [OperationContract]
        List<InventoryLocation> GetSCMInventoryBalance(string _company, string _location, string _item);

        //Written By Prabhath on 11/01/2013
        [OperationContract]
        List<InventoryLocation> GetInventoryBalanceSCMnSCM2(string _company, string _location, string _item, string _status);
        //Written By Prabhath on 23/01/2013
        [OperationContract]
        DataTable GetItemInventoryBalanceStatus(string _company, string _location, string _item, string _status);

        //written by sachith on 30/01/2013
        [OperationContract]
        DataTable StockBalanceSearch(DateTime _from, DateTime _to, string _item, string _loc, string _com, bool isStatus);

        //Written By Shani on 29/01/2013
        [OperationContract]
        List<MasterLocation> getAllLoc_WithSubLoc(string com, string main_Loc);

        //written by sachith on 31/01/2013
        [OperationContract]
        DataTable SerialBalanceSearch(DateTime _from, DateTime _to, string _item, string _loc, string _com);

        //Written By Shani on 01/02/2013
        [OperationContract]
        DataTable Get_InrSer_NotAvailableItems(string _company, string _loc, string _itemCode);


        //written by Sanjeewa on 03/03/2016
        [OperationContract]
        DataTable get_CostUpdateDetails();


        //Written By Shani on 07/02/2013
        [OperationContract]
        List<ReptPickSerials> GetInventorySerialListById(string _serId, string _loc);

        //Written By Shani on 07/02/2013
        [OperationContract]
        Boolean CheckPreRequestAdhocSer(string company, string location, Int32 serID);

        //written by darshana on 07/02/2013
        [OperationContract]
        List<ReptPickSerials> GetRevReqSerial(string _company, string _location, string _user, string _session, string _defBin, string _invoice, string _reqNo);

        //written by darshana on 08-02-2013
        [OperationContract]
        List<ReptPickSerials> GetInvoiceSerialForReversal(string _company, string _location, string _user, string _session, string _defBin, string _invoice, int _baseRefline);

        //Written By sachith on 08/02/2013
        [OperationContract]
        ReptPickSerials Get_all_details_on_doc(string company, string location, string itemCode, string doc, string serial);


        //Written By sachith on 08/02/2013
        [OperationContract]
        Boolean UpdateSerialIDAvailable(string compny, string location, string itemCD, Int32 ser_ID, int availability, int oldAvailable);

        //Written By Prabhath on 8/2/2013
        [OperationContract]
        ReptPickSerials GetReservedByserialID(string company, string location, string bin, string itemCode, int serial_ID);

        //written by Sanjeewa 2013-05-06
        [OperationContract]
        DataTable GetLastNoSeqPageDetails(DateTime _asAtDate, string _Loc);

        //written by Sanjeewa 2013-11-22
        [OperationContract]
        DataTable GetGVPrintDetails(string _com, string _pc, int _book, int _fpage, int _tpage);

        //Written By Prabhath on 1/3/2013
        [OperationContract]
        DataTable GetSerialItem(string _serialtype, string _company, string _serial, Int16 _isWholeWord);

        //Written By Prabhath on 2/3/2013
        [OperationContract]
        DataTable GetSeriaLocation(string _serialType, string _company, string _serial, string _item);

        //Written By Prabhath on 2/3/2013
        [OperationContract]
        DataTable GetSeriaMovement(string _serialType, string _company, string _serial, string _item);

        //Written By Prabhath on 2/3/2013
        [OperationContract]
        DataTable GetSCMWarranty(string _item, string _serial, string _invoice);

        //Written By Prabhath on 8/3/2013
        [OperationContract]
        DataTable GetWarrantyClaimReqyest(string _company, string _profitcenter, string _type, int _isApproved, string _status, DateTime _fromDate, DateTime _toDate);

        //Written By Prabhath on 8/3/2013
        [OperationContract]
        List<RequestApprovalDetail> GetWarrantyClaimReqDetail(string _request);

        //Written By Prabhath on 8/3/2013
        [OperationContract]
        List<RequestApprovalDetail> GetWarrantyClaimInvoiceDuplicate(string _invoice, string _item, string _lineno);

        //Written By Prabhath on 12/3/2013
        [OperationContract]
        DataTable GetWarrantyClaimAdj(string _adjustment);

        //Written By Prabhath on 14/3/2013
        [OperationContract]
        DataTable GetMoveSubTypeMainTable(string _subType, string _mainType);

        //Written By Chamal on 14/03/2013
        [OperationContract]
        List<string> Get_User_Seq_Batch(string _user, string _docType, int _direction_, string _company, string _location);

        //Written By Shani on 20/03/2013
        [OperationContract]
        DataTable GetManualDocsGet_manual_docs_ByRef(string _Comp, string _ref);

        //Written By Shani on 28/03/2013
        [OperationContract]
        Int32 UpdateTransferStatus_NEW(string _RefNo, string _User, string _TransLoc, string userloc);

        //Written By Shani on 28/03/2013
        [OperationContract]
        Int32 Save_mandoc_request_serials(string reqNo, string document, Int32 UserPermissionLevel, string userID);

        //darshana on 28/03/2013
        [OperationContract]
        List<QuotationHeader> GetLatestValidQuotation(string _com, string _sup, string _type, string _subtype, DateTime _date, decimal _qty, string _status, string _item);

        //Chamal on 28/03/2013
        [OperationContract]
        List<MasterItemSimilar> GetSimilarItems(string _type, string _item, string _company, DateTime _date, string _docNo, string _promoCode, string _location, string _proftCenter);

        //Chamal on 28/05/2013
        [OperationContract]
        List<InventoryWarrantySubDetail> GetSubItemSerials(string _mItem, string _mSerial, int _mSerID);

        //Written By Shani on 28/03/2013
        [OperationContract]
        Int16 SavePickedManualDocDetail_TRNS(string _refNo, string _Loc, string _user, string _Status);

        //Written By Shani on 29/03/2013
        [OperationContract]
        Int32 SaveManualDocDet(List<ManualDocDetail> _mandocList);

        //Written By Shani on 29/03/2013
        [OperationContract]
        DataTable GetTempManualDocDet(string _Comp, string _User);

        //Written By Shani on 29/03/2013
        [OperationContract]
        List<ManualDocDetail> Get_manual_doc(string _Comp, string p_ref, string p_itmCd, string p_prifix, string p_bookNo);

        //Written By Shani on 29/03/2013
        [OperationContract]
        Int32 UpdateManualDocs_NEW(string _RefNo, string _USer, string loc);


        //Written By Shani on 30/03/2013
        [OperationContract]
        int UpdateRegistrationRefundSerials(string com, string invoice, string _location, string _itemCode, string _engine, string _chassis, Int32 Status);

        //Written By Chamal on 03/04/2013
        [OperationContract]
        DataTable CheckIsAodReceived(string _document);

        //Written by Chamal on 05/04/2013
        [OperationContract]
        Int16 AODCorrection(string _company, string _aodoutNo, DateTime _date, string _aodIssueLoc, string _incorrectLoc, string _correctLoc, string _manualRef, string _othRef, string _remk, string _user, string _sessionID, out string inwardNo, out string outwardNo);

        //Chamal 22/04/2013
        [OperationContract]
        bool AutoPickNonSerialItems(int _seqNo, string _company, string _location, string _bin, string _item, string _itemDesc, string _itemModel, string _itemBrand, string _itemStatus, decimal _qty, string _itemType, string _user, int _lineNo);

        //Chamal 22/04/2013
        [OperationContract]
        string AutoPickNonSerialItemsAll(int _seqNo, string _company, string _location, string _bin, string _user, List<InventoryRequestItem> _reqItems);

        //Written By Prabhath on 03/05/2013
        [OperationContract]
        List<ReptPickSerials> GetAvailableGiftVoucher(string _company, string _profitcenter, string _item);

        // Nadeeka 21-09-2015
        [OperationContract]
        DataTable GetAvailable_GV_books(Int32 _book, string _item, string _company);


        //Written By Prabhath on 03/05/2013
        [OperationContract]
        ReptPickSerials GetGiftVoucherDetail(string _company, string _profitcenter, string _item, Int32 _book, Int32 _page, string _prefix);

        //sachith 02/05/2013
        [OperationContract]
        DataTable SearchGiftVoucher(string _initialSearchParams, string _searchCatergory, string _searchText);


        //darshana 07/08/2013
        [OperationContract]
        DataTable SearchGiftVoucherByPage(string _initialSearchParams, string _searchCatergory, string _searchText);

        //sachith 02/05/2013
        [OperationContract]
        List<GiftVoucherPages> GetGiftVoucherPages(string _com, int _page);
        //sachith 03/05/2013
        [OperationContract]
        List<InventorySerialMaster> GetWarrantyDetails(string _invoice, string _acc, string _item, string _serial, string _warranty);

        //sachith 03/05/2013
        [OperationContract]
        DataTable GetInvoiceAccountNoFromItem(string _item, string _serial, string _warranty);

        //Written By Prabhath on 06/05/2013
        [OperationContract]
        DataTable GetDetailByGiftVoucher(string _company, string _profitcenter, int _page, string _type);

        //Written By Prabhath on 07/05/2013
        [OperationContract]
        DataTable GetDetailByPageNItem(string _company, string _profitcenter, int _page, string _item);


        //Written By sachith on 07/05/2013
        [OperationContract]
        List<GiftVoucherItems> GetGiftVoucherItems(string _com, int _page);


        //Written By sachith on 07/05/2013
        [OperationContract]
        Int16 GiftVoucherAdjusment(InventoryHeader _inventoryMovementHeader, List<ReptPickSerials> _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub, MasterAutoNumber _masterAutoNumber, out string _docNo, List<GiftVoucherItems> _gift);

        //Written By sachith on 08/05/2013
        [OperationContract]
        GiftVoucherPages GetGiftVoucherPage(string _com, string _pc, string _item, int _book, int _page, string _prefix);

        //darshana on 11-05-2013
        [OperationContract]
        DataTable GetAvailableGvBooks(string _com, string _pc, string _tp, string _status, string _itm, string _prefix);

        //darshana on 11-05-2013
        [OperationContract]
        List<GiftVoucherPages> GetAvailableGvPages(string _com, string _pc, string _tp, string _status, Int32 _book, string _gvCd);

        //darshana on 11-05-2013
        [OperationContract]
        List<GiftVoucherPages> GetAvailableGvPagesRange(string _com, string _pc, string _tp, string _status, Int32 _book, string _gvCd, Int32 _frmPg, Int32 _toPg);

        //darshana on 11-05-2013
        [OperationContract]
        List<GiftVoucherPages> GetGiftVoucherByOthRef(string _com, string _pc, string _refDoc);

        //darshana on 12-05-2013
        [OperationContract]
        List<GiftVoucherItems> GetGiftVoucherAllItems(string _book, string _page, string _com, string _pc);

        //Written by Prabhath on 15/05/2013
        [OperationContract]
        DataTable GetInventoryBalanceByBatch(string _company, string _location, string _item, string _status);

        //kapila 16/5/2013
        [OperationContract]
        string GetNextIncentiveNo(string _Loc, MasterAutoNumber _masterAutoNumber);

        //Written by Prabhath on 17/05/2013
        [OperationContract]
        DataTable CheckSerialAvailability(string _serialtype, string _item, string _serial);

        // Nadeeka 07-05-2015
        [OperationContract]
        DataTable CheckSerialAvailabilityscm(string _item, string _serial);

        //Written by Chamal on 20/05/2013
        [OperationContract]
        int DocDateCorrection(string _docType, string _docNo, DateTime _docDt, string _user);

        //Written by Prabhath on - 21/05/2013
        [OperationContract]
        DataTable CheckInwardDocumentUseStatus(string _company, string _location, string _document);

        //Written by Prabhath on - 21/05/2013
        [OperationContract]
        DataTable GetBuyBackInventoryDocument(string _company, string _location, string _invoiceno);

        //Written by Prabhath on 27/05/2013
        [OperationContract]
        DataTable GetConsginmentDocumentByInvoice(string _company, string _location, string _invoice);

        //Written by Prabhath on 05/06/2013
        [OperationContract]
        DataTable GetChannelDetail(string _company, string _channel);

        //Written by Prabhath on 05/06/2013
        [OperationContract]
        DataTable GetToLocationPermission(string _fromcompany, string _fromlocation, string _tocompany, string _tocategory, string _module);

        //Written by Prabhath on 07/06/2013
        [OperationContract]
        List<ReptPickSerials> GetNonSerializedItemRandomlyByDate(string _company, string _location, string _item, string _status, decimal _qty, DateTime _date);

        //Written by Prabhath on 15/06/2013
        [OperationContract]
        MasterItemBlock GetBlockedItemByPriceType(string _company, string _profit, string _item, int _pricetype);

        //sachith 2013/06/17
        [OperationContract]
        DataTable GetMRNProcessTracking(string _mrnNo, DateTime _from, DateTime _to, string _locList, string _com);

        //sachith 2013/06/18
        [OperationContract]
        DataTable GetMRNTrackingDetails(string _com, string _mrn, string _dispatch, string _warehouse, string _mov);

        //Chamal 21/06/2013
        [OperationContract]
        bool CheckSCMBondNo(string _company, string _bondNo, out string _siNo, out string _lcNo, out string _costSheetRef, out DateTime _bondDate, out string _suppCode);

        //Chamal 22/06/2013
        [OperationContract]
        bool SaveSCMBondAsPO(string _bondNo, DateTime _bondDate, string _suppCode, string _siNo, string _lcNo, string _user, string _company, string _pc, string _loc);

        //kapila
        [OperationContract]
        Int32 UpdateRCCStatus(Int32 _is_app, Int32 _is_rej, string _user, DateTime _date, string _rccno);

        //Written by Prabhath on 25/06/2013
        [OperationContract]
        DataTable GetAvailableToken(DateTime _date, string _company, string _profitcenter, int _token);

        //kapila 
        [OperationContract]
        DataTable GetDocNoByJobNo(string _com, string _loc, string _jobno, out string _docno, Int32 _isExternal);

        //Written by sachith on 27/06/2013
        [OperationContract]
        DataTable SearchGranDin(string _com, string _loc, string _type, string _from, string _to, string _stus, string _cre, string _item, string _serial);

        //kapila
        [OperationContract]
        Boolean IsExternalServiceAgent(string _com, string _code);

        //kapila
        [OperationContract]
        DataTable GetServiceLocation(string _com, string _code, out string _svcLoc);

        //kapila
        [OperationContract]
        DataTable GetVirtualLocation(string _com, string _type, out string _vLoc);

        //kapila
        [OperationContract]
        DataTable GetFixAssetLocation(string _com, string _loc, out string _fLoc);

        //shani 2013/07/08
        [OperationContract]
        DataTable Search_RCC(string _com, string _loc, string _RCCno, DateTime _from, DateTime _to, string _stus, string _item, string _serial, string _warr);

        //shani 2013/07/08
        [OperationContract]
        DataTable SEARCH_rccByDate(string _com, string _loc, string _stage, string _from, string _to, string isSR_AcceptPending);

        //shani 2013/07/09
        [OperationContract]
        DataTable Get_INT_RCC_STAGES_INFO(string _rccNo);

        //Written by Prabhath on 17/07/2013
        [OperationContract]
        bool CheckAllocationItemRagistration(string _item);

        //Written by Prabhath on 16/07/2013
        [OperationContract]
        Int16 CheckItemSerialStatus(string _item);

        //kapila
        [OperationContract]
        RCC GetRCCbySerial(string _Item, string _Serial);

        //added sachith 2013/07/25
        [OperationContract]
        InventoryRequest GetPendingRequest(string _item, int _serial, string _docNo);

        //kapila
        [OperationContract]
        Int16 UpdateIncentiveNo(MasterAutoNumber _masterAutoNumber);

        //Written by Prabhath on 05/08/2013
        [OperationContract]
        DataTable GetLocationCat3(string _company);

        //Written by Prabhath on 05/08/2013
        [OperationContract]
        DataTable GetLocationByCat3(string _cat3, string _company);

        //Written by Prabhath on 05/08/2013
        [OperationContract]
        int SaveTransactionCategory(string _fromcompany, string _fromlocation, string _tocompany, string _tocat3, string _user);

        //Written by Shani on 07-08-2013
        [OperationContract]
        Boolean CancelInventoryDocument(string _doc, string _user);

        //Written by Shani on 07-08-2013
        [OperationContract]
        bool CancelMannualDocument(DataTable TemDocTable, string _prof, string _loc, string _user, string _com, string _docType, out string _err);

        //Written by Chamal on 07-Aug-2013
        [OperationContract]
        int CancelOutwardEntry(string _doc, string _user, out string _err);

        //written by darshana on 07-08-2013
        [OperationContract]
        List<GiftVoucherPages> GetAllGvbyPages(string _com, string _pc, string _stus, string _itm, Int32 _page);

        //kapila
        [OperationContract]
        DataTable GetItemTaxData(string _com, string _cd);

        //Written by darshana on 13-08-2013
        [OperationContract]
        DataTable GetPOSAccDetFromRepDB(string _com, string _cus, string _cusTp);

        //Written by Prabhath on 14/08/2013
        [OperationContract]

        DataTable GetPickHeaderByDocument(string _company, string _document);

        //Written by Randima on 15/09/2016
        [OperationContract]
        DataTable GetPickSerByDocument(string _company, string _document);

        //Written by Prabhath on 14/08/2013
        [OperationContract]
        DataTable GetDirectUnFinishedDocument(string _company, string _location, string _documenttype);




        //kapila
        [OperationContract]
        Int16 UpdateRCCReqRaise(string _docno);
        [OperationContract]
        Int32 Account_Upload_Process(string _loc, string _acc, DateTime _dt, out string _err);

        //Written by Prabhath on 19/08/2013
        [OperationContract]
        DataTable GetDirectOutType(string _company);

        //Written by Prabhath on 19/08/2013
        [OperationContract]
        DataTable GetDirectStatus(string _company, string _type);

        //Written by Prabhath on 22/08/2013
        [OperationContract]
        DataTable GetSubLocation(string _company, string _location);

        //Written by Prabhath on 24/08/2013
        [OperationContract]
        DataTable GetLocationChannel(string _company, string _location);

        //Written by Prabhath on 24/08/2013
        [OperationContract]
        DataTable GetChannelPermission(string _maincd, string _cd, string _frmchnl, string _tochnl);

        //Written by sachith on 27/08/2013
        [OperationContract]
        DataTable GetAllChannelForInventoryTracker(string _com, List<int> _role);
        //Written by sachith on 27/08/2013
        [OperationContract]
        DataTable GetInventoryTrackerChannel(string _com, string _channel, List<int> _role);

        //sachith 11/09/2013
        [OperationContract]
        DataTable GetItemServiceSchedule(string _item);

        //Chamal 12/09/2013
        [OperationContract]
        DataTable Get_Reserved_Serials(string _company, string _location);

        //Chamal 12/09/2013
        [OperationContract]
        DataTable GetScanDocInfor(string _company, string _location, string _document, int _serid);

        //Chamal 12/09/2013
        [OperationContract]
        int RemoveReservedSerials(string _company, string _location, int _scanSeq, string _doc, int _serID, string _user, out string _err);

        //Chamal 13/09/2013
        [OperationContract]
        bool Check_Valid_Document(string _company, string _doc, string _docType);

        //Chamal 18/09/2013
        [OperationContract]
        int Check_Cons_Item_has_Quo(string _com, DateTime _date, List<ReptPickSerials> _scanSerialList, out string _err);

        //Written by Prabhath on 30/09/2013
        [OperationContract]
        Int32 SaveExchangeOut(InventoryHeader _inventoryMovementHeader, List<ReptPickSerials> _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub, List<ReptPickSerials> _doSerial, MasterAutoNumber _masterAutoNumber, RecieptHeader _recieptHeader, List<RecieptItem> _recieptItem, MasterAutoNumber _recieptAuto, out string _docNo, out string _receiptNo, string _oldwarranty, string _newwarranty, DateTime _start, Int32 _Itmwarrperiod, string _warr_type, string _customer, string _name, string _address, string _tel, string _invoice, string _shop, string _shopname, decimal _unitprice, string _status, List<RequestApprovalDetail> _AppDet);

        //darshana on 03-10-2013
        [OperationContract]
        DataTable Get_InterTrans_Req(string _company, string _user, string _reqTp, string _stus, DateTime _frmDt, DateTime _toDt, string _subTp);

        //DARSHANA ON 04-10-2013
        [OperationContract]
        int UpdateInventoryRequestStatusBulk(List<InventoryRequest> _inventoryRequest);

        //kapila
        [OperationContract]
        DataTable CheckSerialBySerial(string _company, string _location, string _item, string _serial);

        //Darshana on 09-10-2013
        [OperationContract]
        int RCCCancelProcess(string _Outdoc, string _user, string _com, string _loc, string _inDoc, string _rccNo, string _Stus, string _rccLoc, out string _err);

        //darshana on 09-10-2013
        [OperationContract]
        InventoryHeader GetRccAodOut(string _com, string _loc, string _docNo);

        //darshana on 09-10-2013
        [OperationContract]
        InventoryHeader GetRccAodIn(string _com, string _loc, string _Othdoc);

        //Written by Prabhath on 10/10/2013
        [OperationContract]
        List<ReptPickSerials> GetInvoiceSerialForExchange(string _company, string _location, string _user, string _session, string _defBin, string _invoice, int _baseRefline);

        //Written by Prabhath on 11/10/2013
        [OperationContract]
        bool IsPendingOrApprovedRequestAvailable(string _company, string _location, string _type, string _document);

        //Written by Prabhath on 11/10/2013
        [OperationContract]
        string GetExchangeInDocument(string _company, string _location, string _request);
        //Written by sachith on 17/10/2013
        [OperationContract]
        List<ReptPickSerials> GetLocationStockBalance(string _company, string _location);
        //Written by Prabhath on 18/10/2013
        [OperationContract]
        DataTable GetUserSearchDataT(string _company);

        //Written by Prabhath on 18/10/2013
        [OperationContract]
        DataSet GetUserSearchDataS(string _company);

        //Written by Prabhath on 18/10/2013
        [OperationContract]
        Int32 UpdateUserSearchData(string _company, out string _msg);
        //Written by sachith on 21/10/2013
        [OperationContract]
        int ProcessPhysicalStockVerification(string _company, string _location, out List<PhysicalStockVerificationItem> _itemList, out List<PhysicalStockVerificationSerial> _serialList, out string _errList, DateTime _date, string _creBy, MasterAutoNumber _auto, string _isCommStatus, out string _jobNo);
        //Written by sachith on 22/10/2013
        [OperationContract]
        List<AuditStatus> GetAllAuditStstus(string _com);
        //Written by sachith on 23/10/2013
        [OperationContract]
        List<AuditReportStatus> GetAllAuditReportStstus(string _com, string _mainCd);
        //Written by sachith on 23/10/2013
        [OperationContract]
        int SavePhysicalStockVerification(PhsicalStockVerificationMain _main, List<PhysicalStockVerificationItem> _itemList, DataTable _serialList, out string _error, List<AuditRemarkValue> _rmkList);
        //Written by sachith on 25/10/2013
        [OperationContract]
        List<AuditRemark> GeatAuditRemarks(string _com, string _rptType);
        //Written by sachith on 25/10/2013
        [OperationContract]
        Int16 SavePhysicalStockVerificationSerial(PhysicalStockVerificationSerial _serial, out string _err);
        //Written by sachith on 25/10/2013
        [OperationContract]
        List<PhysicalStockVerificationSerial> GetPhysicalLedgerSerials(string _com, string _loc, string _job, string _itm, string _stus, string _type);
        //Written by sachith on 25/10/2013
        [OperationContract]
        List<PhysicalStockVerificationItem> GetPhysicalVerificationItems(string _jobNo);
        //Written by sachith on 25/10/2013
        [OperationContract]
        List<PhysicalStockVerificationSerial> GetPhysicalVerificationSerials(string _jobNo);

        //Written by Prabhath on 01/11/2013
        [OperationContract]
        DataTable GetWarrantyDetail(string _company, string _serial1, string _serial2, string _warranty, string _invoice, string _acc);

        //Written by Prabhath on 01/11/2013
        [OperationContract]
        DataTable GetDelivery(string _company, string _item, string _serial, string _warranty, string _invoice);

        //Written by Prabhath on 02/11/2013
        [OperationContract]
        DataTable GetSCMCustomer(string _customer);

        //Written by Prabhath on 02/11/2013
        [OperationContract]
        DataTable GetSCMInvoiceDetail(string _invoice);

        // Nadeeka
        [OperationContract]
        DataTable GetSCMInvoiceDetailWithCom(string _company, string _invoice, string _item);

        //Written by Prabhath on 02/11/2013
        [OperationContract]
        DataTable GetSCMDeliveryDetail(string _invoice);


        // Nadeeka 08-05-2015
        [OperationContract]
        DataTable GetSCMDeliveryDetailItem(string _invoice, string _item);

        //Written by sachith on 06/11/2013
        [OperationContract]
        List<ReptPickSerials> GetSerialsByDocument(Int32 _seqNo, string _docNo);

        //Written by Prabhath on 06/11/2013
        [OperationContract]
        DataTable GetServiceRequest(string _company, string _warranty);

        //Written by sachith on 07/11/2013
        [OperationContract]
        int PhysicalstkUpdateItemCount(string _job, string _itm, string _stus, int _type, int _qty);

        //Written by Prabhath on 08/11/2013
        [OperationContract]
        decimal GetSCMDeliveryItemCost(string _item, string _doc, string _status);

        //Written by Prabhath on 08/11/2013
        [OperationContract]
        DataTable GetExchangeWara(string _request);

        //Written by Prabhath on 8/11/2013
        //Modify by kapila 30-07-2015
        [OperationContract]
        Int16 SaveExchangeInward(InventoryHeader _inventoryMovementHeader, List<ReptPickSerials> _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub, MasterAutoNumber _masterAutoNumber, out string _docNo, Int32 _isWarRep);

        //Written by Prabhath on 11/11/2013
        [OperationContract]
        DataTable GetGRANQtySummary(string _com, string _loc, string _type, string _from, string _to, string _stus, string _cre, string _item, string _serial);

        //Written by sachith on 9/11/2013
        [OperationContract]
        Int32 UpdateAuditStockStus(string _job, int _stus);
        //Written by sachith on 13/11/2013
        [OperationContract]
        List<InventoryCostRate> GetInventoryCostRate(string _com, string _type, string _stus, int _pd, string _oriStus);

        //Written by Prabhath on 15/11/2013
        [OperationContract]
        bool IsUserEntryExist(string _company, string _item, string _type, string _value);
        //Written by sachith on 16/11/2013
        [OperationContract]
        Int16 SavePhysicalStockVerificationItem(PhysicalStockVerificationItem _item);

        //Written by Prabhath on 22/11/2013
        [OperationContract]
        List<ReptPickSerials> GetAdjustmentDetailFromRvtintser(string _company, string _accountno, string _revertno);
        //sachith 2013/11/23
        [OperationContract]
        int SavePhysicalStockVerificationRemark(AuditRemarkValue _rmk);
        //sachith 2013/11/23
        [OperationContract]
        List<AuditRemarkValue> GetPhicalStockRemark(string _jobNo);
        //sachith 2013/11/30
        [OperationContract]
        int PhyscilStockRemoveRmk(string _job, int _serialId, int _line, out string _error);

        //Written by Prabhath on 04/12/2013
        [OperationContract]
        DataTable GetProcessUser(int _seqno, string _document, string _company);

        //Written by Prabhath on 04/12/2013
        [OperationContract]
        Int32 UpdateProcessUser(string _user, int _seqno, string _document, string _company, out string msg);

        //Written by Prabhath on 04/12/2013
        [OperationContract]
        bool CheckCompanyMulti(string _company);

        //kapila
        [OperationContract]
        DataTable FixedAsset(string _com, string _pc, DateTime _from, DateTime _to, string _user);

        //Written by Prabhath on 17/12/2013
        [OperationContract]
        decimal GetLatestCost(string _company, string _location, string _item, string _status);

        //Written by sachith on 17/12/2013
        [OperationContract]
        List<InventorySerialRefN> GetSerialByID(string _serId, string _loc);

        //Written by Prabhath on 19/12/2013
        [OperationContract]
        void GetSCMDeliveryOrder(DateTime _date, string _company, string _location, string _bin, string _party, string _dDocument, string _pDocument, string _user, out string _msg);

        //Written by Nadeeka on 27-02-2015
        [OperationContract]
        void GetSCMAOD(DateTime _date, string _company, string _location, string _bin, string _party, string _dDocument, string _pDocument, string _user, out string _msg);


        //Written by Prabhath on 31/12/2013
        [OperationContract]
        void UpdateSGLDeliveryOrder(string _company, string _party, List<ReptPickSerials> _lst, string _document);

        //Written by darshana on 09-01-2014
        //Edit by Rukshan add new parameter(IsTemp) after _masterAutoNumber
        [OperationContract]
        Int16 SavePRN(InventoryHeader _inventoryMovementHeader, List<ReptPickSerials> _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub, MasterAutoNumber _masterAutoNumber, bool IsTemp, out string _docNo);

        //Written by Chamal on 24-01-2014
        [OperationContract]
        string SampleExportExcel2007(string _com, string _user, string _itemCode, out string _err);
        //Written by sachith on 28-01-2014
        [OperationContract]
        Int32 UpdateTokenStus(string _com, string _pc, int _token, DateTime _date);

        //Written by Prabhath on 07/02/2014
        [OperationContract]
        DataTable GetOnlinePayment(string _com, string _status, string _tp, DateTime _from, DateTime _to);

        //Written by Prabhath on 07/02/2014
        [OperationContract]
        Int32 UpdateOnlinePayment(string _com, string _pc, string _ref, string _status, string _user, string _recno, out string _msg);

        //Written by Prabhath on 07/02/2014
        [OperationContract]
        DataTable GetAOACollection(string _com, string _pc, DateTime _frmDt, DateTime _toDt);

        //sachith 2014-03/06
        [OperationContract]
        int SaveAccountAcknoledge(DataTable _dt, out string _error);

        //Written by Prabhath on 14/03/2014
        [OperationContract]
        DataTable GetLocationTransaction(string _com, string _loc);

        //Written by Prabhath on 15/03/2014
        [OperationContract]
        int SaveDirectIssue(List<DirectIssueLocation> _issue, out string _error);

        //kapila
        [OperationContract]
        DataTable GetSalesDetailsMobSer(string _invNo);

        //Written by Prabhath on 17/03/2014
        [OperationContract]
        DataTable GetDocument();
        //Written by sachith on 17/03/2014
        [OperationContract]
        Int32 DeleteAccountAcknoledge(string _user);

        //written by Prabhath on 20/03/2014
        [OperationContract]
        DataTable GetAccountStatus(string _company, string _profitcenter, string _account);

        //Written by Chamal on 18/03/2014
        [OperationContract]
        int CompanyTransferAutoProcess(string _com, string _user, DateTime _trnsDate);

        //Written by Chamal on 24/03/2014
        [OperationContract]
        int CompanyTransferAutoProcessPriceCheck(string _com, string _user, DateTime _trnsDate);

        //Originally written by Miginda (SP/DAL),Expose to client by Prabhath on 26/03/2014
        [OperationContract]
        PurchaseOrder GetPurchaseOrderHeaderDetails(string _companyCode, string _docNo);

        //Written by Prabhath on 27/03/2014
        [OperationContract]
        DataTable GetSupplier(string _company, string _supplier);

        //Written by Prabhath on 01/04/2014
        [OperationContract]
        DataTable GetPOLine(string _company, string _location, string _doc, Int32 _sid);

        //Written by Prabhath on 01/04/2014
        [OperationContract]
        DataTable GetInvoiceDet(string _company, string _do);

        //Written by Chamal on 29/04/2014
        [OperationContract]
        DataTable GetSCMInvoiceDet(string _company, string _do);

        //Written by Prabhath on 1/4/2014
        [OperationContract]
        void SetOffRefDocumentSerial(List<ReptPickSerials> _lst, string _outwarddoc);

        //Written by Chamal on 30/04/2014
        [OperationContract]
        DataTable GetItemCostSerialSCM(string _doNo, string _item, string _itemstatus, string _serial, int _isSer);

        //Written by Chamal on 05/05/2014
        [OperationContract]
        DataTable GetItemStatusMaster(string _scm2status, string _scmstatus);

        //Code by Chamal on 08/05/2014
        [OperationContract]
        int CheckDuplicateSerialFound(string _company, string _location, List<ReptPickSerials> _reptPickSerials, out string _error);

        //Code by Chamal on 23/05/2014
        [OperationContract]
        int ChangeScanSerialDocDate(string _fromCom, string _toCom, string _docType, string _scanDoc, DateTime _docDate, string _user);

        //Code by Chamal on 23/05/2014
        [OperationContract]
        bool Is_Serial_Can_Remove(string _company, string _doc, string _docType, string _item, string _serial1);

        //Code by Chamal on 22/07/2014
        [OperationContract]
        int RCCCancelProcessForBackDate(string _Outdoc, string _user, string _com, string _loc, string _inDoc, string _rccNo, string _Stus, string _rccLoc, DateTime _date, string _sessionID, out string _err);

        //Code by Chamal on 21/08/2014
        [OperationContract]
        DataTable GetBOCScanSummary(string _com, string _loc);

        //Code by Chamal on 21/08/2014
        [OperationContract]
        int GetBOCSerials(string _com, string _loc, string _pc, string _invcNo, List<string> _batchList, string _user, out string _err);

        //Code by Chamal on 12/01/2015
        [OperationContract]
        int GetExcelSerials(string _com, string _loc, string _pc, string _invcNo, List<ReptPickItems> _batchList, string _user, out string _err);

        //Tharaka 2014-08-19
        [OperationContract]
        List<InventoryAdhocHeader> Get_fixedAssetTransfer_approval(string user, string com);

        //Tharaka 2014-08-19
        [OperationContract]
        List<InventoryAdhocDetail> GetAdhocdet_List(string _ref, string com, string location);

        //Tharaka 2014-08-22
        [OperationContract]
        int FiexdAssettransferApprovalSave(List<InventoryAdhocDetail> odetails, string comapany, string userID, InventoryAdhocHeader Header, int status);

        //Tharaka 2014-08-22
        [OperationContract]
        bool IsApprovedRequestAvailable(string _company, string _location, string _type, string _document);
        //Darshana 2014-12-19
        [OperationContract]
        DataTable GetAllPendingServicePurchaseOrderDataTable(PurchaseOrder _paramPurchaseOrder);
        //darshana 19-12-2014
        [OperationContract]
        DataTable GetSerPOItemsDataTable(string _comCode, string _poNo, string _loc);
        //Darshana 23/02/2015
        [OperationContract]
        DataTable GetGVAlwCom(string _comCode, string _itm, Int32 _act);

        //Rukshan 18/07/2015
        [OperationContract]
        DataTable GetRequestItems(string _Com, string _ReqType, string _ProCenter, string _Supplier, DateTime _FromDate, DateTime _ToDate, string _status, string _QType, string _QSubType, string _dateSelector);
        //Rukshan 21/07/2015
        [OperationContract]
        DataTable GetItemsQuotation(string _Com, string _ReqType, string _SubType, string _Supplier, string _Item, string _status);
        //Rukshan 21/07/2015
        [OperationContract]
        DataTable GetSupplierData(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Rukshan 21/07/2015
        [OperationContract]
        DataTable GetOrderStatus(string _Type);

        [OperationContract]
        //Rukshan 18/07/2015
        DataTable GetOrderQutation(string _Cus, string _Type, string _Suppier);

        //Rukshan 2015-08-13
        [OperationContract]
        DataTable GetSuppierItem(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Rukshan 2015-08-13
        [OperationContract]
        DataTable GetBarcodeItemByDoc(string _docNo, string _com, string _Item, string _loc);

        //Rukshan 2015-08-13
        [OperationContract]
        DataTable GetPurDocNo(string _initialSearchParams, DateTime _from, DateTime _To, string _searchCatergory, string _searchText);

        //Rukshan 2015-08-13
        [OperationContract]
        DataTable GetGRNDocNo(string _initialSearchParams, DateTime _from, DateTime _To, string _searchCatergory, string _searchText);

        //Rukshan 2015-08-13
        [OperationContract]
        DataTable GetPOItem(string _docNo, string _item, string _model);

        //Rukshan 2015-08-13
        [OperationContract]
        DataTable GetGRNItem(string _docNo, string _item, string _model, string _Order);

        //Rukshan 2015-08-13
        [OperationContract]
        Int32 SaveStockAllocate(List<MasterItemAllocate> _MasterItemAllocate, DataTable _ItemAllocate);

        //Rukshan 2015-08-13
        [OperationContract]
        List<MasterItemAllocate> GetStockAllocate(string _Doc, string _ICode);
        //Rukshan 2015-08-13
        [OperationContract]
        Int32 DeleteStockAllocate(List<MasterItemAllocate> _MasterItemAllocate);
        //Rukshan 2015-08-18
        [OperationContract]
        DataTable getDocDetByDocNo(string _company, string _location, string _docno);

        //Tharaka 2015-08-13
        [OperationContract]
        DataTable GET_ITMSTATUS_BY_LOC_ITM(string _com, string _loc, string _item);

        //Tharaka 2015-08-12
        [OperationContract]
        Int32 UPDATE_ITM_STUS(string _invoiceNo, int _lineNo, string _itemCode, string _newStatus, out string _msg);

        //Tharaka 2015-05-24
        [OperationContract]
        Int32 SavePurchaseOrderNew(PurchaseOrder _NewPO, List<PurchaseOrderDetail> _NewPOItems, List<PurchaseOrderDelivery> _NewPODel, MasterAutoNumber _masterAutoNumber, out string docno, out string err);

        #region PDA Sahan

        //Sahan 25/Aug/2015
        [OperationContract]
        DataTable GetTempPickDocTypes(Int32 p_tdt_direct);

        //Sahan 25/Aug/2015
        [OperationContract]
        DataTable LoadBinCode(string p_ibn_com_cd, string p_ibn_loc_cd,string itemcode=null);

        //Sahan 25/Aug/2015
        [OperationContract]
        DataTable GetItemData(string p_mi_cd);

        //Sahan 25/Aug/2015
        [OperationContract]
        DataTable GetItemSerialAvailability(string p_ins_itm_cd, string p_ins_ser_1);

        //Sahan 25/Aug/2015
        [OperationContract]
        DataTable IsItemUOMDecimalAllow(string p_msu_cd);

        //Sahan 26/Aug/2015
        [OperationContract]
        DataTable IsDocAvailable(string p_tuh_usr_com, string p_tuh_doc_no, string p_tuh_usr_loc, string p_tuh_wh_com, string p_tuh_wh_loc, string p_tuh_load_bay);

        //Sahan 26/Aug/2015

        [OperationContract]
        Int32 SavePickedItemSerialsPDA(ReptPickSerials _scanserNew);

        //Sahan 26/Aug/2015
        [OperationContract]
        DataTable IsSavedSerialAvailable(string p_tus_itm_cd, string p_tus_ser_1, string p_tuh_wh_com, string p_tuh_wh_loc, string p_tuh_load_bay,string userid=null,string doctp=null);

        //Sahan 26/Aug/2015
        [OperationContract]
        DataTable LoadSavedSerials(string p_tus_doc_no, string p_tus_com, string p_tus_loc, string p_tus_itm_cd, string p_tus_bin, string p_tus_itm_stus, string p_tuh_wh_com, string p_tuh_wh_loc, string p_tuh_load_bay);

        //Randima 30/11/2016
        [OperationContract]
        List<ReptPickSerials> LoadSavedSerialsList(string p_tus_doc_no, string p_tus_com, string p_tus_loc, string p_tus_itm_cd, string p_tus_bin, string p_tus_itm_stus, string p_tuh_wh_com, string p_tuh_wh_loc, string p_tuh_load_bay);

        //Sahan 27/Aug/2015
        [OperationContract]
        Int32 DeleteItemsWIthSerials(ReptPickSerials _serials);

        //Sahan 27/Aug/2015
        [OperationContract]
        Int32 UpdatePickItem(ReptPickItems _items);

        //Sahan 23/Aug/2016
        [OperationContract]
        Int32 UpdatePickItemStockInOut(ReptPickItems _items);

        //Sahan 27/Aug/2015
        [OperationContract]

        DataTable LoadCurrentRowNumber(Int32 p_tui_usrseq_no, string p_tuh_wh_com, string p_tuh_wh_loc, string p_tuh_load_bay);

        //Sahan 28/Aug/2015
        [OperationContract]
        Int32 UpdatePickItemLine(ReptPickItems _itemsLines);

        //Sahan 28/Aug/2015
        [OperationContract]
        DataTable GetItemQty(Int32 p_tui_usrseq_no, string p_tus_doc_no, string p_tus_itm_cd, string p_tus_itm_stus, string p_tuh_wh_com, string p_tuh_wh_loc, string p_tuh_load_bay,string userid=null,string doctp = null);

        //Sahan 28/Aug/2015
        [OperationContract]
        Int32 UpdateQty(ReptPickItems _itemsqty);

        //Sahan 31/Aug/2015
        [OperationContract]
        DataTable GetItemTotalQty(Int32 p_tui_usrseq_no, string p_tuh_wh_com, string p_tuh_wh_loc, string p_tuh_load_bay);

        //Sahan 01/Sep/2015
        [OperationContract]
        Int32 UpdateSerializedItemsQty(ReptPickSerials _serialsQty);

        //Sahan 02/Sep/2015
        [OperationContract]
        DataTable LoadCurrentJobs(string p_tuh_usr_com, string p_tuh_usr_loc, string p_tuh_usr_id, string p_tuh_doc_tp, Int32 p_tuh_direct, string p_tuh_wh_com, string p_tuh_wh_loc, string p_tuh_load_bay);

        //Sahan 03/Sep/2015
        [OperationContract]
        DataTable CheckCurrentStockBalance(string p_inl_com, string p_inl_loc, string p_inl_itm_cd);

        //Sahan 04/Sep/2015
        [OperationContract]
        DataTable LoadDistinctBins(string _company, string _location, string _itemcode);

        //Sahan 04/Sep/2015
        [OperationContract]
        DataTable LoadItemStatusOfBins(string _company, string _location, string _itemcode, string _bin, string docno = null,string doctp=null);

        //Sahan 04/Sep/2015
        [OperationContract]
        DataTable LoadItemQtyOfBins(string _company, string _location, string _itemcode, string _bin, string _itemstatus);

        //Sahan 04/Sep/2015
        [OperationContract]
        DataTable CheckItemHasExpiryDate(string _itemcode);

        //Sahan 04/Sep/2015
        [OperationContract]
        DataTable LoadItemExpDate(string _company, string _location, string _itemcode, string _bin);

        //Sahan 04/Sep/2015
        [OperationContract]
        DataTable LoadAllSerials(string _company, string _location, string _bin, string _Itemcode, string _itemstatus, string _serial);

        //Sahan 07/Sep/2015
        [OperationContract]
        DataTable CalculateScannedQty(string _itemcode, string _itemstatus, string _company, string _location, string _bin, string p_tuh_wh_com, string p_tuh_wh_loc, string p_tuh_load_bay, string docno);

        #endregion

        //Code By Rukshan on 25/Aug/2015
        [OperationContract]
        DataTable GetAllPendingPOrder(PurchaseOrder _paramPurchaseOrder);
        //Code By Rukshan on 25/Aug/2015
        [OperationContract]
        DataTable GetPurchaseOrdersByType(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Code By Rukshan on 31/Aug/2015
        [OperationContract]
        DataTable getSubitemComponent(string _item);

        //Code By Rukshan on 31/Aug/2015
        [OperationContract]
        DataTable Get_Item_Infor(string _Item);

        //Code By Rukshan on 1/Sep/2015
        [OperationContract]
        Int32 UpdateAllScanSubSerials(List<ReptPickSerialsSub> _reptPickSerialsSub);

        //Code By Rukshan on 1/Sep/2015
        [OperationContract]
        DataTable GetSubSerials(string _ICode, int _Useq, string _MSerial);
        //Code By Rukshan on 1/Sep/2015
        [OperationContract]
        DataTable CheckitemPreFix(string _ICode);

        //Code By Rukshan on 1/Sep/2015
        [OperationContract]
        DataTable GetitemPreFix(string _item, string _Com);

        //Code By Rukshan on 2/Sep/2015
        [OperationContract]
        string Get_defaultBinCDWeb(string company, string location);

        //Code By Rukshan on 09/Sep/2015
        [OperationContract]
        DataTable GetSupplierSerial(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Pemil 11/Sep/2015        
        [OperationContract]
        DataTable GET_INR_SER(string com, string loc, string bin, string itm_cd, string itm_stus, string ser);

        //Code By Rukshan on 12/Sep/2015
        [OperationContract]
        List<ReptPickSerials> Get_Int_Ser_Temp(string _docNo);

        //Code By Rukshan on 12/Sep/2015
        [OperationContract]
        InventoryHeader Get_Int_Hdr_Temp(string DocNo);

        //Sahan 18/Sep/2015
        [OperationContract]
        DataTable GetAllPendingInventoryOutwardsWeb(InventoryHeader _inventoryRequest);

        //Sahan 19/Sep/2015

        [OperationContract]
        Int32 UpdateStockInStatus(InventoryHeader InvHdr);

        //Sahan 22 Sep 2015
        [OperationContract]
        DataTable GetTempDocHeaderData(string _docNo);

        //Sahan 22 Sep 2015
        [OperationContract]
        DataTable GetDocHeaderData(string _docNo);

        //Darshana 22 sep 2015
        [OperationContract]
        DataTable Get_Sup_Forcons(string _com, string _item, string _itemstatus);

        [OperationContract]
        //Rukshan 30 sep 2015
        List<PurchaseReq> GetPoReqLogDetails(int _PO);

        [OperationContract]
        //Rukshan 30 sep 2015
        int Update_PORequestBalanceQty(List<InventoryRequestItem> _Request);

        [OperationContract]
        //Rukshan 30 sep 2015
        DataTable GetPurchaseOrders(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate);

        [OperationContract]
        //Rukshan 02 Oct 2015
        List<InventoryBatchN> Get_Int_Batch_Temp(string _seqNo);

        [OperationContract]
        //Rukshan 02 Oct 2015
        DataTable GetPoQty(string _PONo, int _lineNo);

        [OperationContract]
        //Rukshan 05 Oct 2015
        DataTable CheckItemTo_PRN(string _Com, string _Loc, string _Item, string _status, string _Doc, string _Supplier);

        //Sahan 05/Oct/2015
        [OperationContract]
        DataTable CheckReqUseInPO(string _docNo);

        [OperationContract]
        //Rukshan 05 Oct 2015
        DataTable Get_A_F_PoReq(string _Com, string _Loc, string _Reqno, DateTime _From, DateTime _To);

        [OperationContract]
        //Written by Rukshan 08/oct/2015
        Int32 UpdatePoRequest_Iss(string _Com, string _Rno, int _ItemLine, string _icode, decimal _qty);

        //Tharaka 2015-10-13
        [OperationContract]
        // add parameter 24/Apr/2016
        List<ComboBoxObject> GET_REQ_TYPES(Int32 option, Int32 value);

        //Tharaka 2015-10-13
        [OperationContract]
        // add parameter 24/Apr/2016
        List<InventoryRequest> GET_REQUEST_FOR_DISPATCH(String Com, String Route, String MainCate, String Item, String ReqType, String Loc, String SubCate, String Model, DateTime Date, DateTime ToDate, int type);


        [OperationContract]
        List<InventoryRequest> GET_REQUEST_FOR_DISPATCHNEW(String Com, String Route, String MainCate, String Item, String ReqType, String Loc, String SubCate, String Model, DateTime Date, DateTime ToDate, int type);
        //Tharaka 2015-10-13
        [OperationContract]
        List<InventoryRequestItem> GET_INT_REQ_ITM_BY_SEQ(Int32 seq);

        //Tharaka 2015-10-13
        [OperationContract]
        List<InventoryLocation> GET_LOC_ITEMS_FOR_DISPATCH(String Com, String UserID, String Item, String Stus);
        //darshana 13-10-2015
        [OperationContract]
        Int16 DeleteTempPickObjs(Int32 _seq);

        [OperationContract]
        //Written by Rukshan 15/oct/2015
        List<InventoryBatchN> Get_Int_Batch(string _doc);

        //Sahan 16/Oct/2015

        [OperationContract]
        Int32 UpdatePRNQty(InventoryRequestItem _item);

        [OperationContract]
        //Sanjeewa 2015-10-16
        string GetInvDocType(string _DocNo);

        [OperationContract]
        //Written by Rukshan 18/oct/2015
        List<InventoryRequest> GetMRN_Req(string _IssuesFrom, string _reTo, DateTime _Fdate, DateTime _Todate, string _type, string _searchpara, string _com, string _user);

        [OperationContract]
        //Written by Rukshan 18/oct/2015
        List<InventoryRequestItem> GetMRN_Req_item(string _reqNo);

        //Tharaka 2015-10-16
        [OperationContract]
        Int32 SaveDispatchPlan(List<InventoryRequest> oHeaders, List<InventoryRequestItem> oRequesItems, String SelectedDocumnt, bool isBatchApprove, bool isPatialApprove, bool PDA, string warehousecom, string warehouseloc, string loadingbay, out string docNums, out string err, string isresno, bool isappall);

        //Darshana 2015-10-20
        [OperationContract]
        List<ReptPickSerials> GetTempSaveDet(string _com, string _loc, string _doc, string _user, string _outwardType, string _session, Int32 _usrSeq);

        //Darshana 2015-10-20
        [OperationContract]
        List<ReptPickSerials> GetSaveSerDet(string _com, string _loc, string _doc, string _user, string _outwardType, string _session, Int32 _usrSeq);

        //Sahan 21/Oct/2015
        [OperationContract]
        DataTable CheckINBIssueQty(string _company, string _loc, string _doc);

        //Sahan 21/Oct/2015
        [OperationContract]
        Int32 UpdateCRNStatus(InventoryHeader header);

        //Sahan 21/Oct/2015
        [OperationContract]
        Int32 SaveMRNRequestApprove(InventoryRequest _inventoryRequest, MasterAutoNumber _mastAutoNo, List<InventoryRequestItem> _updateRequest, DataTable _multipleshowroom,
            List<InventorySerialN> _serial, bool _cont, out string _docNo, out int _Ins, out string _docIntr, out string _printAppNo);

        //Sahan 22/Oct/2015
        [OperationContract]
        DataTable GetMRNApprov_doc(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromdate, DateTime _todate);

        //Sahan 26 Oct/2015
        [OperationContract]
        DataTable SearchCRNDoc(string _company, string _loc, string _doc, DateTime _fromdate, DateTime _todate, string _suppler);

        //Rukshan 28/ Oct/2015
        [OperationContract]
        DataTable GetBufferQty(string _company, string _showroom, string _Item, DateTime _date);

        //Rukshan 28/ Oct/2015
        [OperationContract]
        DataTable GetShopQty(string _company, string _showroom, string _Item);


        //Rukshan 28/ Oct/2015
        [OperationContract]
        DataTable GetSimilarItem(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Rukshan 28/ Oct/2015
        [OperationContract]
        DataTable GetReplaceItem(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Rukshan 29/ Oct/2015
        [OperationContract]
        decimal GetForwardsale(string _showroom, string _Item, string _com);

        //Darshana 02-11-2015
        [OperationContract]
        List<GiftVoucherPages> GetVoucherBySearch(Int32 _book, Int32 _page, string _ref);

        //Tharaka 2015-11-03
        [OperationContract]
        WarehouseBin GET_BIN_BY_CODE(string com, string loc, string binCode);

        //Rukshan 04/Nov/2015
        [OperationContract]
        int SaveCusdecEntry(InventoryRequest _inventoryRequest, MasterAutoNumber _mastAutoNo, List<ImportsBLItems> _ImportsBLItems, out string _docNo);

        //Rukshan 05/Nov/2015
        [OperationContract]
        List<InventoryRequest> GetCusdecEntryRequest(string _profit, string _Customer, DateTime _From, DateTime _To, string _type, string _status, string _loc);

        //Rukshan 07/Nov/2015
        [OperationContract]
        DataTable GET_GetTobond_BL(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Rukshan 09/Nov/2015
        [OperationContract]
        List<ImportsBLItems> GET_GetTobond_BL_Itm(string _tobond, string _bl, string _item);

        //Rukshan 09/Nov/2015
        [OperationContract]
        Int32 Update_BI_Rqty(Int32 _number, string _ItemCode, int _ItemLine, decimal _qty, string _Reno, int _type, string com,string sino, out string _error);

        //Sahan 10/Nov/2015
        [OperationContract]
        DataTable CheckWareHouseAvailability(string _company, string _loc);


        //Sahan 10/Nov/2015
        [OperationContract]

        DataTable LoadLoadingBays(string _company, string _loc, string resourse);

        //Sahan 11/Nov/2015
        [OperationContract]
        DataTable LoadUserLoadingBays(string _user, string _company, string _loc, string _warehcom, string _warehloccode);

        //Rukshan 11/Nov/2015
        [OperationContract]
        DataTable GetSearchRequest(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate);

        //Sahan 13 Nov 2015
        [OperationContract]
        DataTable IsDocNoAvailable(string p_tuh_doc_no, string p_tuh_doc_tp, Int32 p_tuh_direct, string p_tuh_usr_com);

        //Sahan 13 Nov 2015
        [OperationContract]
        Int32 UpdatePickHeader(ReptPickHeader header);

        //Sahan 13 Nov 2015
        [OperationContract]
        DataTable CheckItemsScannedStatus(Int32 p_tui_usrseq_no);

        //Sahan 17 Nov 2015
        [OperationContract]
        DataTable GetSearchInterTransferRequestWeb(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Sahan 18/Nov/2015
        [OperationContract]
        DataTable LoadProductConditionUpdateDocs();

        //Sahan 18/Nov/2015
        [OperationContract]
        DataTable LoadHeaderPCUpdateDocWareHouseUser(string type, DateTime fromdate, DateTime todate, string company, string cat1, string cat2, string cat3);

        //Sahan 18/Nov/2015
        [OperationContract]
        DataTable LoadHeaderPCUpdateDocHeadOfficeUser(string type, DateTime fromdate, DateTime todate, string company, string cat1, string cat2, string cat3);

        //Sahan 18 Nov 2015
        [OperationContract]
        DataTable LoadProductConditionPopUp(string _initialSearchParams, string _searchCatergory, string _searchText, string filter);
        [OperationContract]
        DataTable LoadProductConditionNew(string _initialSearchParams, string _searchCatergory, string _searchText, string filter);

        //Sahan 19 Nov 2015
        [OperationContract]
        DataTable LoadDocumnetItems(string docno, Int32 is_serial, string serial);

        //Sahan 20 Nov 2015
        [OperationContract]
        DataTable LoadItemConditions(string serial, string cate);

        //Sahan 20 Nov 2015
        [OperationContract]
        DataTable LoadConditionsForCategory(string company, string cate, string itemcate);

        //Sahan 20 Nov 2015
        [OperationContract]
        DataTable LoadConditionsForAllCategory(string company, string cate);

        //Sahan 20 Nov 2015

        [OperationContract]
        Int32 SaveItemConditions(ItemConditionSetup _itemconditions, Int32 its_pick, string docno, string com, string loc, string item, Int32 serial);

        //Sahan 23/Nov/2015
        [OperationContract]
        DataTable LoadDocSerials(string p_doc);

        //Sahan 23/Nov/2015
        [OperationContract]
        DataTable LoadItemConditionsPerItem(Int32 serial, string cat);

        //Sahan 23 Nov 2015
        [OperationContract]

        Int32 UpdateAnal4(InventoryHeader header);

        //Tharaka 2015-11-24
        [OperationContract]
        Int32 Save_Disposal_Job(DisposalHeader oDisposalHeader, List<DisposalLocation> oDisposalLocations, MasterAutoNumber mastAutoNo, out string err, out string DocuNumber);

        //Tharaka 2015-11-24
        [OperationContract]
        DisposalHeader GET_DISPOSAL_JOB_HEADER(string COM, string PC, string DocNum, string status);

        //Tharaka 2015-11-24
        [OperationContract]
        List<DisposalLocation> GET_DISPOSAL_LOCATIONS(Int32 Seq);

        //Tharaka 2015-11-24
        [OperationContract]
        List<DisposalHeader> GET_DISPOSAL_JOBS(DisposalHeader oFilter);

        //Tharaka 2015-11-26
        [OperationContract]
        Int32 DisposalAdjustmentWithJobSave(InventoryHeader _inventoryMovementHeader, List<ReptPickSerials> _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub, MasterAutoNumber _masterAutoNumber, out string _docNo, DisposalHeader oDisposalHeader, List<DisposalLocation> oDisposalLocations, MasterAutoNumber mastAutoNo, out string err, out string jobNumber, bool IsTemp = false);
        //Lakshan 24/11/2015
        [OperationContract]
        DataTable GetContainerBlDetails(string p_container, string p_bl, string p_date_range, string p_from, string p_to, string p_agent, string p_container_tp, string p_compmany, string _option);

        //Lakshan 24/11/2015
        [OperationContract]
        DataTable GetContainerDetails(string p_container, string p_bl, string p_date_range, string p_from, string p_to, string p_agent, string p_container_tp, string p_compmany);

        //Lakshan 24/11/2015
        [OperationContract]
        DataTable GetContainerSummary(string p_container, string p_bl, string p_date_range, string p_from, string p_to, string p_agent, string p_container_tp, string p_compmany);

        //Lakshan 24/11/2015
        [OperationContract]
        DataTable GetContainerBlList(string p_container, string p_bl, string p_date_range, string p_from, string p_to, string p_agent, string p_container_tp, string p_compmany);

        //Sahan 28 Nov 2015
        [OperationContract]
        DataTable GetSearchMRNWeb(string _initialSearchParams, string _searchCatergory, string _searchText);

        //fazan  03 dec 2015
        [OperationContract]
        DataTable GetCompany(string company);

        //fazan 2015-12-5
        [OperationContract]
        DataTable GetLocationDetails(string company, string location);
        //fazan 2015-12-07
        [OperationContract]
        DataTable LoacationType();

        //fazan 2015-12-07
        [OperationContract]
        DataTable GetCategeryType();
        //fazan 2015-12-07
        [OperationContract]
        DataTable Get_Grade_types();

        //fazan 2015-12-07
        [OperationContract]
        Int32 UpdateLocationDetails(string company, string location, string opearation, string channel, string subchanel, string reference, string address1, string address2, string tel, string mobile, string fax, string contactperson, string country, string province, string district,
            string Email, string location_N, string town, string web, string categery, string p_grade, int sublocation, string mainLocationcd, int online, string managercd,
            int forwardQty, int suspended, int act, string createdby, DateTime createdate, string modifyby, DateTime modifydate, string sessionId, string anal1, string anal2, decimal ana13, decimal anal4, string anal5, DateTime anal6, string locationtype, int allowbin, string defpc, string sev_chnl, string p_auto_ain, string p_fx_loc, DateTime p_scm2_st, int p_chk_man, DateTime p_commencedon, decimal p_approvedstockvalue, int p_bankGrantvalue, string p_wh_com, string p_wh_cd, int p_serial, int pda);


        //fazan 2015-12-09
        [OperationContract]
        Int32 UpdateLog_Details(string company, string location, string opearation, string channel, string subchanel, string reference, string address1, string address2, string tel, string mobile, string fax, string contactperson, string country, string province, string district,
            string Email, string location_N, string town, string web, string categery, string p_grade, int sublocation, string mainLocationcd, int online, string managercd,
            int forwardQty, int suspended, int act, string createdby, DateTime createdate, string modifyby, DateTime modifydate, string sessionId, string anal1, string anal2, decimal ana13, decimal anal4, string anal5, DateTime anal6, string locationtype, int allowbin, string defpc, string sev_chnl, string p_auto_ain, string p_fx_loc, DateTime p_scm2_st, int p_chk_man, DateTime p_commencedon, decimal p_approvedstockvalue, int p_bankGrantvalue, string p_wh_com, string p_wh_cd, int p_serial, string logby, DateTime logdate, string logsession);

        //fazan 2015-12-09
        [OperationContract]
        DataTable getDistrictDetails(string p_code);

        //fazan 2015-12-09
        [OperationContract]
        DataTable getTownDetails(string country, string province, string district);

        //fazan 2015-12-10
        [OperationContract]
        DataTable SortBLDetails(string BL_NO, string Doc_No, string Ref_No, string Entry_No);

        //fazan 2015-12-10
        [OperationContract]
        DataTable invoiceInfo();
        //fazan 2015-12-10
        [OperationContract]
        DataTable InvoiceWarranty_INFO(string serialno, string warrantyno, string invoice, string dono);

        //fazan 2015-12-12
        [OperationContract]
        Int32 Inventorysmst_Update(int warranty_period, string Remarks, string customer, string Details, DateTime warrentyDt, int serialid);

        //Tharaka 2015-12-10
        [OperationContract]
        List<ReptPickSerials> GetOutwarditemsNew(string _loc, string _defbin, ReptPickHeader _scanheaderNew, out string _unavailableitemlist, out string err);

        //Tharaka 2015-12-11
        [OperationContract]
        Int32 SavePickedItemSerials(ReptPickSerials _pick);

        //Sahan 1/Dec/2015
        [OperationContract]
        DataTable CheckDocIsInRepDB(string company, string request);

        //fazan 2015-12-15
        [OperationContract]
        Int32 warranty_amend_insert(string request_by, DateTime request_dt, string req_session, int req_serid, string status, string app_by, DateTime app_date, string app_session, int war_period, string war_remarks, DateTime warranty_stdt, string customer);
        //fazan 2015-12-15
        [OperationContract]
        DataTable get_warrenty_ammendData();
        //fazan 2015-12-15
        [OperationContract]
        DataTable filter_warranty_approve(string war_from, string war_to);
        //fazan 2015-12-15
        [OperationContract]
        DataTable getOperationCode();

        //fazan 2015-12-16
        [OperationContract]
        DataTable searchtowndata(string countrycd, string description, string province, string district, string towncd);


        [OperationContract]
        DataTable s_districtDetails(string districtcd, string description, string provincecd);

        //Sahan 17 Dec 2015
        [OperationContract]
        DataTable CheckIsPDALoc(string company, string loc);

        //Sahan 17 Dec 2015
        [OperationContract]
        DataTable CheckHasLoadingBay(string company, string doc, string loc);

        //Sahan 17 Dec 2015
        [OperationContract]
        Int32 DeleteRepSerials(Int32 seqno, string doc);

        //Sahan 17 Dec 2015
        [OperationContract]
        List<ReptPickSerials> GetTempPickSerialBySeqNo(Int32 seqno, string _docNo, string company, string loc);

        //Rukshan 18 Dec 2015
        [OperationContract]
        Int32 SaveMRNRequestApproveamend(List<InventoryRequestItem> _inventoryRequest, InventoryRequest reqhdr, out string _docNo);

        //Fazan 18 Dec 2015
        [OperationContract]
        DataTable getGrn_details(string doc_no);

        [OperationContract]
        DataTable warehouse_company(string comp);


        //Rukshan 22 Dec 2015
        [OperationContract]
        Int16 UpdateAllScanSerials(ReptPickSerials _reptPickSerials);

        //Tharaka 2015-12-23
        [OperationContract]
        DataTable GetItemBalanceForBIN(string _company, string _location, string _item, string _status, String BIN);



        [OperationContract]
        DataTable GetOperationCode(string com_cd, string loc_cd);

        [OperationContract]
        DataTable getchaneldescription(string company, string code);

        [OperationContract]
        DataTable getdistrictDetails(string province_cd, string districtcd);

        //fazan 2015-12-29
        [OperationContract]
        DataTable getunitCost(string itemcode, string serial);

        [OperationContract]
        DataTable getStorageDetails(string itemcode, string serial);

        [OperationContract]
        DataTable get_noofdays(string itemcode, string serial);

        [OperationContract]
        DataTable WarehouseDeatils(string towncode, string countrycode);

        [OperationContract]
        DataTable Loc_Details();

        [OperationContract]
        DataTable Total_unit_handled(string company);

        [OperationContract]
        DataTable Total_document_handled(string company);

        //Tharaka 2016-01-05
        [OperationContract]
        Int32 DeletePickSerByItemAndBaseItemLine(ReptPickSerials _scanserNew, out string err);

        //Tharaka 2016-01-05
        [OperationContract]
        Int32 DeletePickSerByItemAndItemLine(ReptPickSerials _scanserNew, out string err);

        //Tharaka 2016-01-05
        [OperationContract]
        Int32 UPDATE_QTY_ITM_STUS_NEWSTUS(decimal Qty, Int32 Seq, string item, string stus, string stusNew);
        //Written by Rukshan 24/Dec/2015
        [OperationContract]
        DataTable GETSEARCHRESERVATION(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Written by Rukshan 24/Dec/2015
        [OperationContract]
        DataTable GetItemInventoryBalancechanel(string _company, string _Item, string _status, string _DFDP);

        //Tharaka 2016-01-08
        [OperationContract]
        Int32 UPDATE_PICK_QTY(decimal Qty, Int32 Seq, string item, string stus, Int32 IsUpdatePick);

        //14/Jan/2016
        [OperationContract]
        DataTable LoadItemsBySerial(string item, string serial);

        //14/Jan/2016
        [OperationContract]
        DataTable CheckAbilityMainItemSplit(string item);

        //Sahan 18/Jan/2015
        [OperationContract]
        List<InventorySubSerialMaster> GetAvailablesubSerilsMainWeb(String _ItemCode);

        //Rukshan 20/Jan/2016
        [OperationContract]
        DataTable GETREQ_TRACKER_DATA_BY_CUSTEMER(string _customer, string _entyno, int _dateoption, DateTime _ReqDateFrom, DateTime _ReqDateTo,
            DateTime _ExpectedFrom, DateTime ExpectedTo, string _profitcenter, string _loc, string _item, string _Cat1, string _sts = null);

        //Rukshan 21/Jan/2016
        [OperationContract]
        DataTable GETREQ_TRACKER_DATA_BY_ENTRYNO_DO(string item, int IssumQty);

        [OperationContract]
        DataTable GETREQ_TRACKER_DATA_BY_ENTRYNO_DO_NEW(string item, int IssumQty, int _derction);
        //Rukshan 21/Jan/2016
        [OperationContract]
        DataTable GETREQ_TRACKER_DATA_BY_ENTRYNO(string item, int IssumQty, string _item, DateTime _ReqDateFrom, DateTime _ReqDateTo, string _cat1);

        //Rukshan 21/Jan/2016
        [OperationContract]
        decimal GET_PREVIOUS_SALES_QTY(string _com, string _loc, DateTime _fromdate, DateTime _Todate, string _item, string _doctype);

        //Sahan 25 Jan 2016
        [OperationContract]
        DataTable LoadDistinctDates(string company);

        //Sahan 25 Jan 2015
        [OperationContract]
        DataTable GetSlowMovingInventoryDetails(string company, DateTime fromdate, DateTime todate, DateTime selecteddate, string item, string model, string brand, string cat);

        //Sahan 25 Jan 2015
        [OperationContract]
        DataTable LoadAgeSlots(string company);
        //Tharaka 2016-01-21
        [OperationContract]
        DataTable GET_DISPOSAL_SERIALS(String Disposaljob, out DataTable OItems);

        //Sahan 26 Jan 2016
        [OperationContract]
        DataTable LoadInventoryByExpiryDate(string company, string item, string isordbyloc);



        //Sahan 8 Feb 2016
        [OperationContract]
        DataTable LoadBinCodeItem(string company, string location);

        //Sahan 8 Feb 2016
        [OperationContract]
        List<InventorySubSerialMaster> LoadItemCompoData(string item);

        // Sahan 9 Feb 2016
        [OperationContract]
        DataTable SearchSerialByItem(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Sahan 10 Feb 2016
        [OperationContract]

        DataTable LoadLocationAndStatusMainItem(string item, string company, string location);

        //Tharaka 2015-12-28
        [OperationContract]
        InventoryHeader GetINTHDRByOthDoc(String Com, String Type, String OthDoc);

        //Randima 2016-11-29
        [OperationContract]
        List<InventoryHeader> GetINTHDRByDispDoc(String Com, String jobNo);

        //Sahan 11 Feb 2016
        [OperationContract]
        DataTable CheckIsItemDiscontinue(string item);

        //Sanjeewa 2016-02-11
        [OperationContract]
        Int16 UpdatePhysicalStockItem(List<PhysicalStockVerificationItem> _itemList, out string _error);


        //Rukshan 11/Feb/2016
        [OperationContract]
        Int32 CANCEL_MRN(string _com, string _loc, string _reqno, string _status, string _modby, DateTime _moddate);

        //Sahan 16 Feb 2016
        [OperationContract]
        DataTable LoadSerialStatus(string company, string location, string itemcode, string serial1, string serial2, string serial3, Int32 serialstus);

        //Rukshan 15/Feb/2016
        [OperationContract]
        Int32 Update_ResHeaderStatus(string STATUS, string USER, string COM, string MRN);

        //Rukshan 15/Feb/2016
        [OperationContract]
        bool Check_MRN_Item_exceed_Ins(List<InventoryRequestItem> _Item, string _com, string _loc, DateTime _now);

        //Nuwan 2016/02/17
        [OperationContract]
        List<MST_GIFTVOUCHER_SEARCH_HEAD> getGiftVoucherSearch(string company, int item, string pgeNum, string pgeSize, string searchFld, string searchVal);

        //23 Feb 2015
        [OperationContract]
        Int32 UpdateLoadingBay(ReptPickHeader _Header);

        //Sahan 23 Feb 2016
        [OperationContract]
        DataTable LoadBLData(string doc);

        //Sahan 23 Feb 2016
        [OperationContract]
        DataTable LoadFinDataNumber(string doc);

        //Sahan 23 Feb 2016
        [OperationContract]
        DataTable LoadFinData(string doc);

        //Sahan 23 Feb 2016
        [OperationContract]
        DataTable LoadSerialId(string company, string location, string serial, Int32 type);

        //Sahan 23 Feb 2016
        [OperationContract]
        DataTable LoadSerialEnquiryData(Int32 serialid);

        //Written By rukshan on 26/02/2016
        [OperationContract]
        DataTable GetItemInventoryBalanceStatu_both_LP_IMP(string _company, string _location, string _item, string _status, bool _checkstatus, out string _name, out string _statusnew);

        //Sahan 2/Mar/2016
        [OperationContract]
        DataTable GetDocQty(Int32 seq);

        //Lakshan 2/Mar/2016
        [OperationContract]
        List<InventorySerialMaster> GetSerialMasterData(InventorySerialMaster _ser);

        //Rukshan 2016-3-4
        [OperationContract]
        Int32 SaveDispatchPlanebyAllitem(List<InventoryRequest> oHeaders, string _location, string _status1, string _status2, bool _isstatus, bool PDA, string warehousecom, string warehouseloc, string loadingbay, out string docNums, out string err);


        //Rukshan 2016-3-4
        [OperationContract]
        decimal GET_SUM_USE_BL(string _bl, string _itm);

        //Rukshan 2016-3-4
        [OperationContract]
        int SaveCUSA_amend(List<InventoryRequestItem> _inventoryRequest, List<ImportsBLItems> oImportsBLItems, InventoryRequest reqhdr, out string _docNo);

        /*Lakshan 03-Mar-2016*/
        [OperationContract]
        Int32 GetBlItmMaxSeqNo(ImportsBLItems _itm, out string err);

        /*Lakshan 03-Mar-2016*/
        [OperationContract]
        DataTable getImpPiHdrTps(string _piNo);

        //Lakshan 05-Mar-2016
        [OperationContract]
        List<ItemKitComponent> GetItemKitComponentSplit(ItemKitComponent _obj);

        //Sahan 10 Mar 2016
        [OperationContract]
        Int32 UpdateSerialMaster(SerialMasterLog SerialMaster);

        //Rukshan 12 Mar 2016
        [OperationContract]
        Int16 UpdatePicked_Hd_doc(ReptPickHeader _reptPickSerials, MasterAutoNumber _AutoNo);

        //Lakshan 2016 Mar 14
        [OperationContract]
        List<QuotationHeader> GetLatestValidQuotationData(string _com, string _sup, string _type, string _subtype, DateTime _date, decimal _qty, string _status, string _item);

        //15 Mar/2015 Sahan
        [OperationContract]
        DataTable LoadItemDataBySerial(string serial, string company, string location, string cat1, string cat2, string cat3);

        //Rukshan 15 Mar 2016
        [OperationContract]
        DataTable GET_TEMP_DOC(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _from, DateTime _To);

        //Rukshan 15 Mar 2016
        [OperationContract]
        DataTable GET_TEMP_ITM(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Nuwan 17/03/2016
        [OperationContract]
        DataTable GetItemTotalScanedQty(string docnum);
        //Nuwan 17/03/2016
        [OperationContract]
        DataTable GetItemTotalDocumentQty(string docnum);
        //Nuwan 17/03/2016
        [OperationContract]
        DataTable loadDocumentItems(string docnum);
        //Lakshan 17/03/2016
        [OperationContract]
        List<ReptPickSerials> GET_ReptPickSerials(ReptPickSerials _obj);
        //Lakshan 24/03/2016
        [OperationContract]
        ReptPickHeader GetReptPickHeader(ReptPickHeader _obj);


        //Rukshan 24/Mar/2016
        [OperationContract]
        List<InventoryRequestItem> Check_bl_GRN(List<InventoryRequestItem> _itm, string JobNo, string loc);

        //Lakshan 29/Mar/2016
        [OperationContract]
        List<InventorySerialN> Get_INR_SER_DATA(InventorySerialN _ser);
        //Lakshan 30/Mar/2016
        [OperationContract]
        List<MasterItemComponent> Get_MST_ITM_COMPONENT(MasterItemComponent _ser);
        //Lakshan 01 Apr 2016
        [OperationContract]
        List<ReptPickItems> GET_ReptPickItems(ReptPickItems _obj);
        //Lakshan 08 Apr 2016
        [OperationContract]
        List<InventorySubSerialMaster> GET_INR_SERMSTSUB(InventorySubSerialMaster _obj);

        //Lakshan 11 Apr 2016
        [OperationContract]
        List<ImpAstHeader> GET_IMP_AST_HDR(ImpAstHeader _obj);

        //Lakshan 11 Apr 2016
        [OperationContract]
        List<ImpAstDet> GET_Entry_no_Ammend(string _docNo, string _entryNo);

        //Lakshan 18 Apr 2016
        [OperationContract]
        List<ReptPickSerialsSub> GET_TEMP_PICK_SER_SUB(ReptPickSerialsSub _obj);

        //Rukshan 24 Apr 2016
        [OperationContract]
        Int32 Cancel_DispatchPlan(List<InventoryRequest> oHeaders, List<InventoryRequestItem> oRequesItems, out string err, bool isfinish);

        //Lakshan on 25 Apr 2016
        [OperationContract]
        DataTable GetSupplierItemSerial(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Rukshan 25 Apr 2016
        [OperationContract]
        Int32 SaveBufferLevel(List<MasterBufferChannel> _MasterBufferChannel, out string err);

        //Lakshan 27 Apr 2016
        [OperationContract]
        DataTable GetBLDetailsByModel(ImportsBLItems _impBlItm, MasterItem _item);

        //Lakshan 27 Apr 2016
        [OperationContract]
        Int32 saveInrLocation(List<InventoryLocation> _loc, out string err);

        //Lakshan 29 Apr 2016
        [OperationContract]
        List<InventorySerialN> Get_INT_SER_DATA(InventorySerialN _ser);

        //SUBODANA 
        [OperationContract]
        InventoryHeader GetINTHDRByDocnO(String Com, String Type, String OthDoc);

        //subodana

        [OperationContract]
        List<InventoryAllocateDetails> getAllocationDet(DateTime FRMDT, DateTime TODT);


        //Lakshan 2016 May 05
        [OperationContract]
        List<InventoryAdhocHeader> GET_INT_ADHOC_HDR(InventoryAdhocHeader _obj);

        //Rukshan 2016/May/06
        [OperationContract]
        List<MasterBufferChannel> GetBufferQty_Season(string _item, string _com, string _loc, string _ses, int option);

        //Rukshan 2016/05/14
        [OperationContract]
        Int16 SaveAllScanSerialsList(List<ReptPickSerials> _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub);

        //subodana  2016/05/17
        [OperationContract]
        DataTable getLocationDetails(string _com, string _channel, string _loc);

        //Rukshan 2016/May/17
        [OperationContract]
        Int16 UPDATEPICKSERIAL_BASEITM(List<ReptPickSerials> _reptPickSerials);

        //subodana 2016-05-19
        [OperationContract]
        DataTable GET_ITEM_PROFILE_DETAILS(string _com, string cat1, string cat2, string cat3, string cat4, string cat5, string code, string brand, string model);

        //subodana 2016-05-26
        [OperationContract]
        Int32 SaveAllItemsSerials(ReptPickHeader _inputReptPickHeader, ReptPickItems _items, ReptPickItems _itemslines, ReptPickSerials _inputReptPickSerials);

        //subodana 2016-05-31
        [OperationContract]
        DataTable getRequestItemDetails(string _com, string _reqno);

        //Lakshan 31 May 2016
        [OperationContract]
        int ProductAssemblySave(MasterAutoNumber _autonoAsbl, out string _assDoc,
        InventoryHeader _invOutHeader, List<ReptPickSerials> _reptPickSerialsOut, List<ReptPickSerialsSub> _reptPickSerialsSubOut, MasterAutoNumber _autonoMinus, out string _docMines,
        InventoryHeader _invINHeader, List<ReptPickSerials> _reptPickSerialsIn, List<ReptPickSerialsSub> _reptPickINSerialsSubIn, MasterAutoNumber _autonoPlus, out string _docPlus,
        InventoryHeader _invHdrAodIn, List<ReptPickSerials> _reptPickSerialsAodIn, out string _aodInDoc, MasterAutoNumber _autonoAodIn,
        InventoryHeader _invHdrAodOut, List<ReptPickSerials> _reptPickSerialsAodOut, out string _aodOutDoc, MasterAutoNumber _autonoAodOut,
            out string _error, bool _aod);

        //subodana 2016-06-03
        [OperationContract]
        DataTable getExistingSerial(string _serial, string _code, string docno, string status);

        //subodana 2016-06-03
        [OperationContract]
        Int32 UpdateExistingSerialRecived(string docno, Int32 serial, Int32 reserved);

        //Written by Rukshan 03/Jun/2016
        [OperationContract]
        List<INT_REQ> GETREQBY_REF(string _DOC, string _COM, string _TYPE, string _itm, int _line);

        //Lakshan 04 Jun 2016
        [OperationContract]

        List<RepConditionType> GET_REF_COND_TP(RepConditionType _obj);

        //Written by Rukshan 03/Jun/2016
        [OperationContract]
        List<ReptPickHeader> GetAllScanHdr(string _company, string _user, string _doctype, int _direc, string _location = null);
        //DArshana 06-06-2016
        [OperationContract]
        MasterItemBlock GetBlockedItmByCatTp(string _company, string _profit, string _item, int _pricetype, string _catTp);

        //subodana 2016-06-06
        [OperationContract]
        DataTable getINRSerial(string _serial, string _code);

        //subodana 2016-06-06
        [OperationContract]
        DataTable GetTepItems(Int32 seq, string _code, string status = null,string userid=null,string doctp=null);

        //Nuwan 2016.06.13
        [OperationContract]
        DataTable GetTempPickLocations(string locationCode);

        //Rukshan 2016.06.14
        [OperationContract]
        List<QuotationHeader> GetLatestAllValidQuotation(string _com, string _sup, string _type, string _subtype, DateTime _date, decimal _qty, string _status, string _item);

        //Lakshan 16 Jun 2016
        [OperationContract]
        DataTable SearchSerialsInr(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Lakshan 16 Jun 2016
        [OperationContract]
        DataTable SearchSerialsIntByItem(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Sanjeewa 2016-06-20
        [OperationContract]
        DataTable getWarrantyPrintDetails(string _warrno, int _page, string _docno);

        [OperationContract]
        DataTable getWarrantyPrintMobDetails(string _com, string _seqno, string _item, string _serial, int _page);

        //Sanjeewa 2016-06-30
        [OperationContract]
        DataTable getLocManagerDetail(string _loc);

        //Rukshan 16-06-2016

        [OperationContract]
        List<InventoryLocation> GETWH_INV_BALANCE(string _company, string _location, string _item, string _status);

        //Lakahsn 20 Jun 2016
        [OperationContract]
        List<InventoryHeader> GET_INT_HDR_DATA(InventoryHeader _obj);
        //Rukshan 16-06-2016
        [OperationContract]
        DataTable GetItemInventoryBalanceStatus_RES(string _company, string _location, string _item, string _status);
        //Lakshan 24 Jun 2016
        [OperationContract]
        List<InventoryHeader> GetIntHdrDatByDateRange(InventoryHeader obj, DateTime from, DateTime to, Int32 isDateRange);

        //Nuwan 2016.06.25
        [OperationContract]
        DataTable getCurrentDocumentDetails(string p_tuh_usr_com, string p_tuh_doc_no, string p_tuh_usr_loc, string p_tuh_wh_com, string p_tuh_wh_loc, string p_tuh_load_bay);

        //Add by Rukshan 27 Jun 2016
        [OperationContract]
        DataTable CHECK_TOBOND_GRN(string _com, string _doc, string _itm, out bool _result);

        //Add By Lakshan 2016/Jun/2016
        [OperationContract]
        DataTable SearchSplitReCall(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime dtFrom, DateTime dtTo);

        //Written By Rukshan on 05/Jul/2016
        [OperationContract]
        Int16 SaveAllScanSerials_Excel(List<ReptPickSerials> _reptPickSerials, List<ReptPickItems> _ReptPickItems, List<ReptPickSerialsSub> _reptPickSerialsSub);

        //Written By Rukshan on 07/Jul/2016
        [OperationContract]
        String GET_BOOKMAX_SERIAL(string _com, string _itm, out int _latpage);

        //Written By lakshan on 14/Jul/2016
        [OperationContract]
        List<InventoryBatchRefN> Get_Inr_Batch(InventoryBatchRefN _obj);

        //Written By Rukshan on 19/Jul/2016
        [OperationContract]
        List<PurchaseOrderDetail> GetPOItemsList(string _poNo);

        //Written By Lakshan on 20/Jul/2016
        [OperationContract]
        DataTable GetAodItem(string _docNo, string _item, string _model);

        //Written By Rukshan on 07/Jul/2016
        [OperationContract]
        String GET_TEMPBOOKMAX_SERIAL(string _com, string _itm, int _seq, out int _latpage);

        //Written By Rukshan on 25/Jul/2016
        [OperationContract]
        DataTable Get_Root_Loc(string _company, string _root, string _loc);

        //Written By Rukshan on 25/Jul/2016
        [OperationContract]
        List<InventoryLocation> Get_Root_Loc_Inv(string _com, string _Route, string _Itemcode, string _status);

        //Add By Rukshan 2016/Jul/25
        [OperationContract]
        DataTable SearchInrBatch(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _dtFrom, DateTime _dtTo);

        //Add By Rukshan 2016/Jul/25
        [OperationContract]
        DataTable SearchSerialsInr_Batchno(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Written By Rukshan on 25/Jul/2016
        [OperationContract]
        DataTable chekInr_batchno(string _docNo, string _com);

        //Written By Rukshan on 25/Jul/2016
        [OperationContract]
        DataTable GetComItem(string _item);

        //subodana 2016-08-05
        [OperationContract]
        List<AODTrackerHDRdata> Get_AOD_Trackerdata(string com, string fromloc, string toloc, DateTime fromdate, DateTime todate, string inval, string outval);

        //subodana 2016-08-05
        [OperationContract]
        DataTable GETTRANSDETAILS(string com, string doc);

        //subodana 2016-08-06
        [OperationContract]
        List<AODTrackerHDRdata> Get_AOD_TrackerdataByDoc(string com, string doc);

        //subodana 2016-08-06
        [OperationContract]
        List<AODTrackerHDRdata> Get_AOD_TrackerdataByTRNS(string com, string method, string party, string refno);

        //subodana 2016-08-06
        [OperationContract]
        DataTable GETAODITEMS(string doc);

        //subodana 2016-08-06
        [OperationContract]
        DataTable GETAODSERIAL(string com, string doc);

        //Rukshan 2016-08-10
        [OperationContract]
        List<ModelCatAndTypes> Get_Model_cat_Type(string model, string _Maincat, out List<ModelPic> _pic);

        //LAkshan 2016-08-10
        [OperationContract]
        List<InventorySerialN> GetDerWarrantyPrintSerial(string _docNo, int _rePrint);

        //LAkshan 2016-08-10
        [OperationContract]
        Int32 UpdateDerectWarantyPrint(Int32 _serId);

        //Rukshan 2016-08-15
        [OperationContract]
        Int32 SaveModelClsDef(List<ModelClsDef> _ModelClsDef, List<ModelPic> _pic, out string err);

        //Lakshan 2016 Aug 15
        [OperationContract]

        List<ReptPickHeader> GetAllScanHdrWithDateRange(ReptPickHeader _obj, Int32 _isDtRang, DateTime _dtFrom, DateTime _dtTo);

        //subodana 2016-08-18
        [OperationContract]
        List<ReservationItemsrep> GetReservationItemsDet(string com, DateTime fromdate, DateTime todate, string docno, string itemcode, string status, string dispatchloc, string custormer, string type, string adminTeam);

        //Rukshan 2016-08-18
        [OperationContract]
        List<InventorySerialN> Get_Reserved_SerialsNew(string _company, string _location);

        //subodana 2016-08-19
        [OperationContract]
        List<CatwithItems> GetItemsDetWithCat(string cat);

        //subodana 2016-08-19
        [OperationContract]
        DataTable CheckINTReqBond(string com, string refno);

        //Lakshan 2016-08-20
        [OperationContract]
        List<InventoryItem> GET_INT_ITM_DATA(InventoryItem _obj);

        //Lakshan 2016-08-20
        [OperationContract]
        List<ReptPickSerials> GET_TEMP_PICK_SER_BY_INT_SER(InventorySerialN _obj);

        //subodana 2016-08-20
        [OperationContract]
        List<BLtracker> GetBlTrackerData(string com, DateTime fromdate, DateTime todate, string blno, string itemcode, string bondno, string model);
        //Nuwan 2016.08.22
        [OperationContract]
        DataTable GetItemDataInTempPickItem(string itmCode, string docNo);
        //darshana 2016-08-23
        [OperationContract]
        List<InventoryRequest> GET_REQUEST_FOR_DISPATCH_NO(String Com, String Route, String MainCate, String Item, String ReqType, String Loc, String SubCate, String Model, DateTime Date, DateTime ToDate, int type, string ReqNo);

        //Randima 2016-08-25
        [OperationContract]
        DataTable GetItemReservationDet(string _com, string _loc, string _itm, string _status);

        //Lakshan 2016-08-29
        [OperationContract]
        List<ReptPickHeader> GetReportTempPickHdr(ReptPickHeader _obj);
        //Nuwan 2016.08.31
        [OperationContract]
        DataTable getTempPickHdrDoc(string seq);
        //Lakshan 2016 Aug 31
        [OperationContract]
        decimal GetActualRateAodIn(string _docNo, Int32 _lineNo);

        //Lakshika 2016-09-03
        [OperationContract]
        DataTable GetGRNDetailsByReqNo(string _seqNo);



        //Lakshika 2016-09-06
        [OperationContract]
        DataTable GetGRNItemsDetailsBySeqNo(string _seqNo);

        [OperationContract]
        List<AdjesmentDet> StockAdjDetails(string com, string chnal, DateTime fromdate, DateTime todate);
        //Nuwan 2016.09.08
        [OperationContract]
        decimal checkStockAvailabilityOfItem(string itemcode, string company, string location, string bincode, string itmstatus, decimal qtyforscan, out string error);
        //Nuwan 2016.09.08
        [OperationContract]
        bool deleteTempDocument(string docnum, string seqno, out string error);

        //subodana 2016-09-08
        [OperationContract]
        DataTable SP_GETENTRYREQDATA(string com, string sino);

        //subodana 2016-09-12
        [OperationContract]
        DataTable SP_GETRESQTYMRN(string com, string loc, string itmcode, string status);

        //Lakshika 2016-09-12
        [OperationContract]
        DataTable updateWarrentryIsPrint(string _docno);
        //Nuwan 2016.09.13
        [OperationContract]
        Int32 updateDocumentFinishStatus(string docno, string doctyp, Int32 status, out string error);

        //Lakshan 13 Sep 2016
        [OperationContract]
        DataTable CalculateAodExcelData(DateTime _dtFrom, DateTime _dtTo);
        //Nuwan 2016.09.13
        [OperationContract]
        DataTable chechIsAvailableSerial(string itmcode, string serial);

        //subodana 2016-09-13
        [OperationContract]
        Int32 UpdateReqUser(string com, string reqno, string user);

        //subodana 2016-09-13
        [OperationContract]

        DataTable SP_GETUPDATEUSER(string COM, string REQNO);
        //Nuwan 2016.09.15
        [OperationContract]
        DataTable LoadFinishedCurrentJobs(string p_tuh_usr_com, string p_tuh_usr_loc, string p_tuh_usr_id, string p_tuh_doc_tp, Int32 p_tuh_direct, string p_tuh_wh_com, string p_tuh_wh_loc, string p_tuh_load_bay);
        //Nuwan 2016.09.08
        [OperationContract]
        bool reopenTempDocument(string docnum, string doctp, string seqno, out string error);

        //Lakshika 2016-09-15
        [OperationContract]
        DataTable GetBalanceQtyForResItmRpt(string com, string req_no, string item_cd, int line_no);

        //Nuwan 2016.09.15
        [OperationContract]
        DataTable getExistsItemDocument(string itemcode, string company, string location, string bincode, string itmstatus);
        //Nuwan 2016.09.15
        [OperationContract]
        decimal getDocumentSerialCount(string docno);

        //subodana 2016-09-13
        [OperationContract]
        string GET_INR_BATCH_ITM(string _GRN, string _itm, string _com);
        //Nuwan 2016.09.17
        [OperationContract]
        Int32 deleteSeriallisezedSerial(string serialCheck, string docdirection, string doctyp, string _scanDocument, ReptPickSerials _inputReptPickSerials, ReptPickItems _itemsQty, string company, string location, string _item, Int32 serialid, string locserialcheck, Int32 docseq, out string error,string userid=null,string doctp=null);
        //Nuwan 2016.09.17
        [OperationContract]
        Int32 deleteNonSerialItems(ReptPickItems _itemsQty, ReptPickSerials _inputReptPickSerialsUpdate, out string error,string userid=null,string doctp=null);

        //Lakshan 2016 Sep 17
        [OperationContract]
        InventoryRequest GetInventoryRequestDataByReqNo(InventoryRequest _inputInventoryRequest);

        //Rukshan 2016 Sep 18
        [OperationContract]
        List<ReptPickSerials> GetInvoiceSerialForReversalBYITM(string _company,
            string _invoice, int _baseRefline, string _itm, int _qty);
        //Nuwan 2016.09.17
        [OperationContract]
        Int32 saveStockOutDetails(DataTable dtdoccheck, ReptPickHeader _inputReptPickHeader, string isseriaitem, DataTable temppickitems, string iscurrent, decimal qtyforscan, ReptPickItems _items, string iscreatejob, ReptPickItems _itemslines, DataTable dtsericlavailable, ReptPickSerials _inputReptPickSerials,
         string company, string location, string txtitemcode, Int32 serialid, string _scanDocument, string warecom, string wareloc, string loadingpoint, string locserialcheck, out string error);

        //Lakshan 19 Sep 2016
        [OperationContract]
        List<ReptPickSerials> GET_TEMP_PICK_SER_DATA(ReptPickSerials _repPickSer);
        //Nuwan 2016.09.19
        [OperationContract]
        Int32 saveStockInDetails(DataTable dtdoccheck, ReptPickHeader _inputReptPickHeader, string isseriaitem, DataTable temppickitems, string iscurrentjobs, decimal qtyforscan, ReptPickItems _items,
            string iscreatejob, DataTable dtsericlavailable, ReptPickItems _itemslines, string doctp, string existsdocno, Int32 existseialno, ReptPickSerials _inputReptPickSerials, string company,
            string location, string _scanDocument, string warecom, string wareloc, string loadingpoint, Int32 existserialcount, out string error,string passDocTp = null);
        //Nuwan 2016.09.20
        [OperationContract]
        DataTable getINTSerial(string serial, string itmcd, string docno);

        //Lakshan 20 Sep 2016
        [OperationContract]
        List<ReptPickItems> GET_TEMP_PICK_ITM_DATA(ReptPickItems _repItm);

        //Lakshan 20 Sep 2016
        [OperationContract]
        List<ReptPickHeader> GET_TEMP_PICK_HDR_DATA(ReptPickHeader _repHdr);

        //Lakshan 2016 Sep 22
        [OperationContract]
        Int32 UpdateTempPickSerSerVerification(ReptPickSerials _obj);

        //Lakshan 2016 Sep 23
        [OperationContract]
        List<InventoryRequest> GET_INT_REQ_DATA(InventoryRequest _obj);

        //Lakshan 2016 Sep 23
        [OperationContract]
        List<InventoryRequestItem> GET_INT_REQ_ITM_DATA(InventoryRequestItem _obj);
        //Lakshan 2016 Sep 24
        [OperationContract]
        List<INR_RES> GET_INR_RES_DATA(INR_RES _obj);

        //Rukshan 2016 Sep 25
        [OperationContract]
        List<ReptPickSerials> GetInvoiceSerialForReversalBYSerial(string _company, string _location, string _user, string _session, string _defBin,
          string _invoice, string _serial);
        //Lakshan 01 Oct 2016
        [OperationContract]
        DataTable GetItemComponentTableNew(string _mainItemCode);

        //subodana
        [OperationContract]
        DataTable GetPriceBookLvl(string _com);

        //subodana
        [OperationContract]
        DataTable GetItemPrice(string pb, string pl, string itm, DateTime dt);




        //subodana
        [OperationContract]
        List<InventoryRequestItem> GetMRN_Req_pickitm(string _reqNo);
        //Nuwan 2016.10.10
        [OperationContract]
        Int32 getItemBinScanQty(string itemCode, string bincode, string docNo);
        //Nuwan 2016.10.10
        [OperationContract]
        decimal getTotalStockQty(string itemcd, string company, string location);

        [OperationContract]
        DataTable GetReportParam(string com, string user);
        //Add by Lakshan 11 Oct 2016
        [OperationContract]
        DataTable SearchSplitReCallNew(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime dtFrom, DateTime dtTo);

        //subodana
        [OperationContract]
        List<InventoryLocation> GETWH_INV_BALANCENew(string _company, string _location, string _item, string _status);

        //Randima 13/Oct/2016
        [OperationContract]
        ReptPickSerials getSerialDet_INTSER(int _seqNo, int _itmLine, int _batchLine, int _serLine);

        //Lakshan 17/Oct/2016
        [OperationContract]
        List<InventoryRequest> GetAllMaterialRequestsListNew(InventoryRequest _inventoryRequest);

        //subodana 2016/10/18
        [OperationContract]
        DataTable SP_GETENTRYRESDATA(string sino, string Bno, string Itm);
        //Nuwan 2016.10.19
        [OperationContract]
        DataTable GetTepItemsInOtherStatus(Int32 _userSeqNo, string itmcd);

        //Rukshan 16/Oct/2016
        [OperationContract]
        DataTable GetINV_BAL_STUS_LPAND_IMP(string _company, string _location, string _item, string _status);

        //subodana 2016-10-21
        [OperationContract]
        DataTable GetEntryProgDetails(string entryno);

        //subodana 2016-10-21
        [OperationContract]
        DataTable GetEntryProgGRN(string entryno, string item, string docno);

        //Nuwan 2016.10.21
        [OperationContract]
        bool validateSRNSerialInDo(string invoiceno, string itemcode, string isseriaitem, string serial, out string errormsg);

        //Lakshan 25 Oct 2016
        [OperationContract]
        List<MasterBusinessEntity> GET_MST_BUSENTITY_DATA(string _com, string _busCd, string _tp = "");

        //Lakshan 27 Oct 2016
        [OperationContract]
        SalesOrderHeader GET_SAO_HDR_DATA(string _soNo);

        //Nuwan 28/10/2016
        [OperationContract]
        DataTable IsDocAvailableWithSeq(string p_tuh_usr_com, Int32 p_docseq, string p_tuh_usr_loc, string p_tuh_wh_com, string p_tuh_wh_loc, string p_tuh_load_bay,string doctp =null);
        //Nuwan 2016.08.22
        [OperationContract]
        DataTable GetItemDataInTempPickItemWithSeq(string itmCode, Int32 docseqNo);
        //Nuwan 2016.10.28
        [OperationContract]
        DataTable CalculateScannedQtyWithSeq(string _itemcode, string _itemstatus, string _company, string _location, string _bin, string p_tuh_wh_com, string p_tuh_wh_loc, string p_tuh_load_bay, Int32 docseq);
        //Nuwan 2016.10.28
        [OperationContract]
        DataTable getCurrentDocumentDetailsSeq(string p_tuh_usr_com, Int32 p_tuh_doc_seq, string p_tuh_usr_loc, string p_tuh_wh_com, string p_tuh_wh_loc, string p_tuh_load_bay);
        //Nuwan 2016.09.13
        [OperationContract]
        Int32 updateDocumentFinishStatusSeq(Int32 docseq, string doctyp, Int32 status, DateTime nowdt, out string error,string userid=null,string doctp=null);
        //Nuwan 17/03/2016
        [OperationContract]
        DataTable GetItemTotalScanedQtySeq(Int32 seqno);
        //Nuwan 28/10/2016
        [OperationContract]
        DataTable GetItemTotalDocumentQtySeq(Int32 docseq);
        //Nuwan 28/10/2016
        [OperationContract]
        DataTable loadDocumentItemsSeq(Int32 docseq,string userid=null,string doctp=null);
        //Nuwan 28/10/2016
        [OperationContract]
        DataTable LoadSavedSerialsSeq(Int32 p_tus_doc_seq, string p_tus_com, string p_tus_loc, string p_tus_itm_cd, string p_tus_bin, string p_tus_itm_stus, string p_tuh_wh_com, string p_tuh_wh_loc, string p_tuh_load_bay,string userid=null,string doctp =null);
        //Nuwan 2016.10.28
        [OperationContract]
        Int32 getItemBinScanQtySeq(string itemCode, string bincode, Int32 docseq);

        //Randima 31 Oct 2016
        [OperationContract]
        Int32 DisposalAdjustmentWithJobSaveNew(InventoryHeader _inventoryMovementHeader, List<ReptPickSerials> _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub, MasterAutoNumber _masterAutoNumber, out string _docNo, DisposalHeader oDisposalHeader, List<DisposalLocation> oDisposalLocations, List<DispCurrentLocation> oStockAtLocations, List<DisposalCurrStatus> oStockStatus, List<DisposalItem> oDispItems, MasterAutoNumber mastAutoNo, out string err, out string jobNumber, bool IsTemp = false);

        //Lakshan 31 Oct 2016
        [OperationContract]
        List<InventoryAllocateDetails> GET_INR_STOCK_ALOC_DATA(InventoryAllocateDetails _obj);

        //Lakshan 31 Oct 2016
        [OperationContract]
        decimal GET_INR_LOC_BAL_DATA(InventoryLocation _obj);

        [OperationContract]
        DataTable GET_EnquiryBond(string _com, string _sbu, string _tobond, string _bl, int cusdec, string _item, string _DOC, string _loc);

        //Nuwan 2016.10.31
        [OperationContract]
        DataTable getDOSerialData(string docno, string itemcd, string serial);
        //Nuwan 201610.31
        [OperationContract]
        DataTable getINTSerialdo(string docno, string item, string serial);

        //Randima 2016-10-31
        [OperationContract]
        List<DispCurrentLocation> GET_DISPOSAL_CURR_LOC_LIST(int _seqNo, string _docNo);

        //Randima 2016-10-31
        [OperationContract]
        List<DisposalCurrStatus> GET_DISPOSAL_CURR_STUS_LIST(int _seqNo, string _docNo);

        //Randima 2016-10-31
        [OperationContract]
        List<DisposalLocation> GET_DISPOSAL_LOC_LIST(int _seqNo, string _docNo);

        //Randima 2016-11-03
        [OperationContract]
        Int32 Save_Disposal_Items(List<DisposalItem> dispItm, bool isupdate);

        //Randima 2016-11-03
        [OperationContract]
        List<DisposalItem> GET_DISPOSAL_ITM_LIST(int _seqNo, string _docNo, bool istextchange);

        //Randima 2016-1104
        [OperationContract]
        int GET_DISPOSALITM_LINE(int _seqNo);

        //RUKSHAN 2016-10-23
        [OperationContract]
        int GET_SEQNUM_FOR_INVOICE_LOC(string doc_type, string company, string invoiceNO, int direction_, string _loc);

        //RUKSHAN 2016-10-23
        [OperationContract]
        int CheckSerialFoundDO(string _company, string _invoice, List<ReptPickSerials> _reptPickSerials, out string _error);

        //SUBODANA 2016-11-04
        [OperationContract]
        DataTable CusEnqEntryData(DateTime fromdate, DateTime todate);
        //SUBODANA 2016-11-04
        [OperationContract]
        DataTable CusEnqCusdecData(DateTime fromdate, DateTime todate);

        ////Lakshan modify 05 Nov 2016
        //[OperationContract]
        //int SaveInventoryRequestDataNEW(InventoryRequest _inventoryRequest, MasterAutoNumber _mastAutoNo, out string _docNo);

        //Randima 2016-11-05
        [OperationContract]
        List<INR_RES_LOG> GET_INR_RES_LOG_DATA_NEW(INR_RES_LOG _resLog);

        //Lakshan 07 Nov 2016
        [OperationContract]
        List<INR_RES_DET> GET_INR_RES_DET_DATA_NEW(INR_RES_DET _resDet);

        //Lakshan 09 Nov 2016
        [OperationContract]
        int UpdateInventoryRequestStatusWithNote(InventoryRequest _inventoryRequest, List<InventoryRequestItem> _invReqItm, out string _erro);

        //Randima 2016-11-09
        [OperationContract]
        Int32 Save_Disposal_Hdr(DisposalHeader dispHdr);

        //Randima 2016-11-09
        [OperationContract]
        Int32 DisposalAdjustmentSave(InventoryHeader _inventoryMovementHeader, List<ReptPickSerials> _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub, MasterAutoNumber _masterAutoNumber, DisposalHeader oDisposalHeader, List<DisposalItem> outDispItm, out string _docNo, out string err, bool IsTemp = false, bool check = true);

        //Lakshan 2016-11-09
        [OperationContract]
        InventoryLocation GET_INR_LOC_BALANCE(InventoryLocation _obj);

        //Lakshan 2016-11-09
        [OperationContract]
        Int32 UpdateIntReqProcessData(InventoryRequest _obj);
        //Lakshan 2016-11-09
        [OperationContract]
        List<InventoryRequest> GET_INT_REQ_DATA_NEW(InventoryRequest _obj);
        //Lakshan 12 Nov 2016
        [OperationContract]
        List<ReptPickHeader> GET_TEMP_PICK_HDR_DATA_WITH_COMPLETE_DATE(ReptPickHeader _repHdr);
        //Lakshan 12 Nov 2016
        [OperationContract]
        List<InventoryRequest> GET_REQ_FOR_DISPATCH_BYNO(String Com, String Route, String MainCate, String Item, String ReqType, String Loc, String SubCate, String Model, DateTime Date, DateTime ToDate, int type, string ReqNo);
        //Lakshan 12 Nov 2016
        [OperationContract]
        List<InventoryRequest> GET_REQUEST_FOR_DISPATCH_EXP_DT(String Com, String Route, String MainCate, String Item, String ReqType, String Loc, String SubCate, String Model, DateTime Date, DateTime ToDate, int type);

        //Rukshan 15 Nov 2016
        [OperationContract]
        List<ItemKitComponent> GetItemKitComponent_ProductPlane(ItemKitComponent _obj, string _loc);

        //Rukshan 2016/NOV-15
        [OperationContract]
        Int32 SaveProjectPlane(SatProjectHeader _BOQ, MasterAutoNumber mastAutoNo, List<ItemKitComponent> Cost, List<ProductionFinGood> _finItem, List<ProductionPlaneDetails> _ProLine, out string doc);
        //Lakshan 16 Nov 2016
        [OperationContract]
        Int32 SaveWarrantyAuthorization(List<int_war_print_auth> _obj);
        //Lakshan 16 Nov 2016
        [OperationContract]
        List<int_war_print_auth> GET_INT_WAR_PRINT_AUTH(int_war_print_auth _obj);

        //Lakshan 17 Nov 2016
        [OperationContract]
        Int32 UpdateWarrantyAuthorization(int_war_print_auth _objList);

        //Rukshan 18 Nov 2016
        [OperationContract]
        List<ProductionLine> GET_PRODUCTLINE(ProductionLine _obj);
        //Nuwan 2016.11.21
        [OperationContract]
        bool updateItemReservations(Int32 userseq, string reserve, string company, string location, string userid, string sessionid, out string error);
        //Nuwan 2016.11.21
        [OperationContract]
        DataTable getItemDetWithStatus(Int32 userseq, string status, string itmcd);
        //Lakshan 23 Nov 2016
        [OperationContract]
        decimal GET_INR_BATCH_BALANCE_BY_BATCH_NO(InventoryBatchN _obj);

        //Lakshan 06 Dec 2016
        [OperationContract]
        List<UnitConvert> GET_UNIT_CONVERTER_DATA(UnitConvert _obj);

        //Lakshan 06 Dec 2016
        [OperationContract]
        List<ReptPickSerials> GetAllScanSerialsListForAodOut(string _company, string _location, string _user, Int32 _userseqno, string _doctype);
        //Lakshan 08 Dec 2016
        [OperationContract]
        List<MasterItemSimilar> GET_MST_ITM_SIMILER_DATA(MasterItemSimilar _obj);
        //Lakshan 08 Dec 2016
        [OperationContract]
        List<MasterItemSimilar> GetSimilerItemBalanceData(MasterItemSimilar _obj, string _company);
        //Sanjeewa 2016-12-16
        [OperationContract]

        DataTable check_AOD_Recieved(string _docno);

        //subodana 2016-12-16
        [OperationContract]
        List<ReservationItemsrep> ReservationItemList(DateTime fromdate, DateTime todate, string itemcode, string cat, string dispatchloc,
            string customer, string status, string docno, string cat2, string cat3, string adminTeam, string com, string User);

        //Lakshan 16 Dec 2016
        [OperationContract]
        List<InventoryLocation> GET_LOC_ITEMS_FOR_DISPATCH_NEW(String Com, String UserID, String Item, String Stus);

        //Rukshan 16 DEC 2016
        [OperationContract]
        bool CheckAOD_AlreadyIn(string _aodno, string _com, out string loc);

        //Rukshan 16 DEC 2016
        [OperationContract]
        DataTable GetItemInventoryBalanceQty(string _company, string _location, string _item, string _status);

        //Rukshan 07/Nov/2015
        [OperationContract]
        DataTable GET_GetTobond_BL_Date(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _from, DateTime _to);

        //Lakshan 23 Dec 2016
        [OperationContract]
        List<InventoryRequestItem> GET_INT_REQ_ITM_DATA_BY_REQ_NO(string _itr_req_no);

        //RUKSHAN 30 Dec 2016
        [OperationContract]
        Int32 SaveStockVerification(PhysicalStockVerificationHdr _stockhdr, MasterAutoNumber mastAutoNo, out string doc, out string _message, List<AuditMemebers> _auditMemebers = null);

        //Rukshan 30 Dec 2016
        [OperationContract]
        DataTable SEARCH_STOCKVERF_HDR(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _from, DateTime _to);

        //Rukshan 30 Dec 2016
        [OperationContract]
        PhysicalStockVerificationHdr GET_STOCKVERF_HDR(String _doc);

        //Rukshan 30 Dec 2016
        [OperationContract]
        List<PhsicalStockVerificationMain> GET_STOCKVERF_MAIN(String _doc);

        //RUKSHAN 30 Dec 2016
        [OperationContract]
        Int32 FineshStockVerification(string _maindoc, string _com, string _loc, string _user, out string doc);

        //RUKSHAN 30 Dec 2016
        [OperationContract]
        DataTable GET_GetTobond_BL_RES(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Rukshan 30 Dec 2016
        [OperationContract]
        List<InventoryAllocateDetails> GET_STOCK_ALOC_DATA_TRCK(InventoryAllocateDetails _obj);

        //Lakshan 09 Jan 2016
        [OperationContract]
        List<ReptPickSerials> AllocationDataValidateAodOut(List<ReptPickSerials> _serList, MasterLocation _inLoc, string _outLoc);

        //Rukshan 
        [OperationContract]
        DataTable CHECK_TOBOND_GRN_status(string _com, string _doc, string _itm, string _status);

        //Lakshan 09 Jan 2017
        [OperationContract]
        List<InventoryRequestItem> AllocationValidateInterTransferApprove(InventoryRequest _invReq, List<InventoryRequestItem> _reqList);

        //by Akila 2017/01/10
        [OperationContract]
        DataTable GetItemStatusDefinition_byLocation(string comCode, string locationCode);

        //Rukshan 
        [OperationContract]
        List<ReptPickHeader> GetReportPickHdrDetails(ReptPickHeader _obj);

        //WRITTEN INTERFACE BY SUBODANA
        [OperationContract]
        MasterProfitCenter GetProfitCenter(string _company, string _profitCenter);
        [OperationContract]
        int checkInsuvaluExcel(List<InventoryRequestItem> _updateRequest, string _com, bool _cont, out string _docNo, out int _Insvalue, out string _docIntr);
        [OperationContract]
        List<ReptPickItems> GET_TEMP_PICK_ITM_GROUP(string _seq);
        //Lakshan 06 Feb 2017 
        [OperationContract]
        DataTable SearchSerialsByProductionNo(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Lakshan 06 Feb 2017 
        [OperationContract]
        decimal GET_INR_LOC_BALANCE_BY_COM(InventoryLocation _obj);

        //RUKSHAN 08 Feb 2017 
        [OperationContract]
        List<InventoryRequest> GET_INT_REQ(InventoryRequest _OBJ);

        //RUKSHAN 08 Feb 2017 
        [OperationContract]
        int ProductionIssue(InventoryHeader _HDR, List<ReptPickSerials> _adjminusserial, List<ReptPickSerials> _adjplusserial, string _fromloc, string _toloc, out string error, string _prodno);

        //Akila 2017/02/13
        [OperationContract]
        DataTable GetAuditMembers(string _comCode, string _jobNo);

        [OperationContract]
        DataTable SearchAuditMembers(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable SearchAuditJobs(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime? _from, DateTime? _to);

        [OperationContract]
        DataTable GetAuditJobDetails(string _companyCode, string _locationCode, string _jobNo);
        //Lakshan 15 Feb 2017
        [OperationContract]
        int CancelIntertransferDocument(InventoryRequest _inventoryRequest);

        //Lakshan 15 Feb 2017
        [OperationContract]
        int CancelIntertransferDocumentBulk(List<InventoryRequest> _inventoryRequest);
        //Lakshan 15 Feb 2017
        [OperationContract]
        InventoryHeader GET_INT_HDR_DATA_BY_OTH_DOC_NO(string _docTp, string _othDocNo);

        //Akila 2017/02/17
        [OperationContract]
        int SaveAuditMemebers(AuditMemebers _auditMembers);

        //Lakshan 21 Feb 21
        [OperationContract]
        List<InventoryRequest> GET_SOA_REQ_DATA_FOR_INVOICE(string _invNo);

        //By Akila 2017/02/22
        [OperationContract]
        int ProcessStockCountJob(string _company, string _location, string _currentUser, List<PhsicalStockVerificationMain> _main, out string _errorMessgae);


        //subodana 2017-02-22
        [OperationContract]
        DataTable GET_CUSDEC_ITEM(string doc, string item);

        //subodana 2017-03-08
        [OperationContract]
        List<ReptPickSerials> Search_serials_for_itemCD2(string company, string location, string itemCode, string status, string suplr);
        //Lakshan 09 Mar 2017
        [OperationContract]
        int CancelMaterialRequestNote(InventoryRequest _inventoryRequest);

        //By Akila 2017/02/27
        [OperationContract]
        void UploadPhysicallyAvailableSerials(string _company, string _location, List<string> _serialList, string _jobNo, int _seqNo, string _userId, bool _isExcessItem, string _itemCode, string _sessionId, out string _message);

        //Akila 2017/03/06
        [OperationContract]
        List<AuditJobItem> GetProcessedJobItems(string _subJobNo, Int32 _startIndex, Int32 _endIndex);

        //Akila 2017/03/06
        [OperationContract]
        List<AuditJobSerial> GetProcessedJobSerials(string _subJobNo, Int32 _startIndex, Int32 _endIndex, Int32 _status);

        //Akila 2017/03/09
        [OperationContract]
        void SaveAuditNotes(List<AuditJobSerial> _auditNotes, out string _message);

        //Akila 2017/03/10
        [OperationContract]
        Int32 UpdateAuditHeaderDetails(string _company, string _location, string _jobNo, string _userId, Int32 _jobStatus, string _sessionId, out string _message);

        //subodana
        [OperationContract]
        List<Model_Specific> Get_Model_Specific_Data(string model);
        //Lakshan 2017 Mar 2016
        [OperationContract]
        int CancelDistributionApprovedDocument(string _com, string _loc, string _reqno, string _status, string _modby, DateTime _moddate, InventoryRequest _invReq, out string _err);
        //Nuwan 2017.03.17
        [OperationContract]
        ReptPickHeader getTemporyHeaderDetails(string docno, string doctype, string company, out string error);
        //Nuwan 2017.03.17
        [OperationContract]
        List<ReptPickSerials> getScanedSerials(Int32 seqNo, out string error);
        //Nuwan 2017.03.20 
        [OperationContract]
        List<ReptPickItems> getScanedItems(Int32 seqNo, out string error);
        //Nuwan 2017.03.20
        [OperationContract]
        InventoryHeader getIntHdrData(string docno, string location, string company, out string error);

        //Akila 2017/03/21
        [OperationContract]
        DataTable GetAuditItemSummery(string _subJobNo);

        //Akila 2017/03/25
        [OperationContract]
        List<AuditJobSerial> GetAuditSerialDetails(string _subJobNo, int _searchOption, string _serialNo);

        //Lakshan 23 MAr 2017
        [OperationContract]
        List<RefPrdMt> GET_REF_PRD_MT_DATA(RefPrdMt _obj);

        //Akila 2017/03/27
        [OperationContract]
        List<AuditJobSerial> GetRemarkedSerials(string _subJobNo);

        //subodana
        [OperationContract]
        DataTable GET_AGE_DETAILS(string com, string item, DateTime fdate, DateTime tdate, Int32 mindate, Int32 maxdate, string type);
        //Udaya 30/03/2017
        [OperationContract]
        DataTable GetTechAllocationDetails(string com, string allocation, string modelNo, string itemNo, string jobNo, string chassisNo, string engineNo, string dateFrom, string dateTo, string aodNo, string locCode, string profitCenter);
        //Udaya 30/03/2017
        [OperationContract]
        Int32 saveUpdate_TechnicianAllocation(List<TechAllocation> _TechAllocation, MasterAutoNumber masterAuto, out string doc, out string errMsg);
        //Udaya 03/04/2017
        [OperationContract]
        Int16 UpdateTechAlloSeqNo(MasterAutoNumber _masterAutoNumber);
        //Udaya 03/04/2017
        [OperationContract]
        DataTable TechAlloSeqNo(string modId, string locId, string catId);

        //Akila 2017/04/06
        [OperationContract]
        DataTable GetDamageAuditItemDetails(string _company, string _Location, string _serial, string _item);

        //Akila 2017/04/07
        [OperationContract]
        DataTable GetExcessItems(string _serialNo, string _company, string _location, string _item);
        //Lakshan 07 Apr
        [OperationContract]
        List<InventoryRequest> GetAllMaterialRequestsListAodScmWeb(InventoryRequest _inventoryRequest);
        //Nuwan 2017.04.11
        [OperationContract]
        DataTable GetTepItemsBYCode(Int32 seqno, string itemcd,string userid=null,string doctp=null);
        //Add by lakshan get copy from GetSupplierSerial 12 Apr 2017
        [OperationContract]
        List<TempPopup> GetSupplierSerialForConsignmentReturn(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Add by lakshan 20 Apr 2017
        [OperationContract]
        List<ReptPickSerials> Search_serial_for_consignment(string company, string location, string itemCode, string status, string suplr, string _serial);

        //Akila 2017/04/20
        [OperationContract]
        DataTable GetRccDetails(string _company, string _location, string _serial, string _item);

        [OperationContract]
        Int16 UpdatePhysicalStockHdr(PhysicalStockVerificationHdr _hdr);

        //Akila 2017/04/24
        [OperationContract]
        DataTable GetInvNotDeliveredDetails(string _company, string _location, string _serial, string _item);

        //subodana 2017-04-25
        [OperationContract]
        DataTable GetFreqtyForLocation(string _company, string _location, string status, string _item, string document, string serialid);
        //subodana 2017-04-25
        [OperationContract]
        DataTable GetFreeoriginalqtyForLocation(string _company, string _location, string status, string _item);
        //Nuwan 2017/04/22
        [OperationContract]
        DataTable getItemExpStatus(string itemcd);
        //Nuwan 2017/04/25
        [OperationContract]
        Int32 addDocumentPrint(string printdoc, string userId, string doctype, string sessionId, string loadingpoint, out string error);

        ////ISURU 2017/04/26
        [OperationContract]
        List<PurchseOrderPrint> GetPurReqByDocnO(String Com, String Type, String OthDoc, string Loc);
        //Udaya 28/04/2017
        [OperationContract]
        List<TmpValidation> locQtyCheck(List<ReptPickSerials> _reptPickSerials, InventoryHeader _inventoryHeader, string locCode, string comCode);

        //subodana
        [OperationContract]
        DataTable GetComponetUnit(string finishitem, string comitem);

        //Akila 2017/05/18
        [OperationContract]
        int IsItemActive(string _item);

        //Udaya 19/05/2017
        [OperationContract]
        void GRNExcelUploadValidation(DataTable GetExecelTbl, int _userSeqNo, List<ReptPickSerials> _resultItemsSerialList, string _userwarrid, string poNo, string SessionID, string UserCompanyCode, string UserID, string UserDefLoca, DataTable _test, string cdDate, string PORefNo, string Bincode, string EDate, string Mdate, out bool lblView, out string msg, out int value, out List<ReptPickSerials> _excelMisMatchReptPickSerials);

        //subodana
        [OperationContract]
        List<ImportsBLItems> GET_GetTobond_BL_Itm_latest(string _tobond, string _bl, Int32 _itemline);

        //ISURU 2017/05/23
        [OperationContract]
        DataTable getpanaltystatesdetails(string invno);
        //Nuwan 2017/04/25
        [OperationContract]
        Int32 addDocumentPrintNew(string printdoc, string userId, string doctype, string sessionId, string loadingpoint, string wareloc, out string error);

        //Lakshan 26 May 2017
        [OperationContract]
        Int32 ServiceWorkingProcessComplete(MasterAutoNumber _mstAdjMines, MasterAutoNumber _mstAdjPlus, MasterAutoNumber _mstAodOut,
           InventoryHeader _invHdrAdjMines, InventoryHeader _invHdrAdjPlus, InventoryHeader _invHdrAodOut,
       List<ReptPickSerials> _rptSerialList, List<ReptPickSerialsSub> _rptSubSerialList,
            MasterAutoNumber _mstAdjMinesReq, InventoryHeader _invHdrAdjReq, string _jobNo, Int32 _jobLine,
           out string _docAdjMines, out string _docAdjPlus, out string _docAodOutNo, out string _docAdjMinesREQ, out string _error, out List<TmpValidation> _errList);
        //Tharanga 2017/06/02
        [OperationContract]
        DataTable checkIntser(string comcd, string itmCd, string intSer);
        [OperationContract]
        Int16 SaveNewPONew(PurchaseOrder _NewPO, List<PurchaseOrderDetail> _NewPOItems, List<PurchaseOrderDelivery> _NewPODel, List<PurchaseOrderAlloc> _NewPOAloc, MasterAutoNumber _masterAutoNumber, List<PurchaseReq> _PurchaseReq, List<InventoryRequestItem> _Porequest, out string docno, bool _saveSalesOrder = false);
        //Udaya 13/06/2017
        [OperationContract]
        Int32 DispatchRecordUpdate(DataTable dispatch, string comCode, string userID, string sessionId, DateTime modiDate, int chk);
        //Code By Rukshan on 09/Sep/2015 modify bu lakshan 26Jun2017
        [OperationContract]
        DataTable GetSupplierSerialWEB(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Udaya 28.06.2017
        [OperationContract]
        DataTable LoadInventoryDataByExpiryDate(string company, string item, string isordbyloc);

        //dulanga 28.6.2017
        [OperationContract]
        Int32 Is_OptionPerimitted(string userCompany, string userId, Int32 optionCode);
        //Lakshan 30Jun2017
        [OperationContract]
        List<InventoryRequestItem> GetSOAItemDataByInvoice(string _invoice);
        //Udaya 03.07.2017 Collect resavation details
        [OperationContract]
        DataTable Collect_ItemReservationDtl(string _com, string _loc, string _itm, string _status);
        //Udaya 07.07.2017 Collect Technision details
        [OperationContract]
        DataTable get_technicianAllocated_Details(string _com, string _loc, string jobNo);
        //Tharanga 2017/07/11
        [OperationContract]
        DataTable Get_Spare_parts_Movement_Report(string _com, string _item, DateTime _frmdate, DateTime _todate, string _pc);
        //Lakshan 10 Jul 2017
        [OperationContract]
        DataTable GetSearchMRNWebByJobNo(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Tharanga 2017/07/12
        [OperationContract]
        DataTable Get_Spare_parts_Movement(string _com, string _item, DateTime _frmdate, DateTime _todate, string _pc, Int32 p_permission);

        //kapila 15/jul/2017
        [OperationContract]
        List<InventoryLocation> GetItemInventoryBalance_New(string _company, string _location, string _item, string _pbook, string _pblvl);

        //Add by Lakshan 05 Aug 2017
        [OperationContract]
        List<ReptPickSerials> GetSerialDataForDisposalEntry(string _com, string _loc, string _itm, string _itmStus, string _serial, string _supplier, Int32 _serId);
        [OperationContract]
        //Add by Lakshan 10Aug2017 
        List<DisposalItem> GET_DISPOSAL_ITM_LIST_WEB(int _seqNo, string _docNo, bool istextchange);
        //Udaya 16.08.2017 item, Dept requi Report
        [OperationContract]
        DataTable RequirmentsDetails(DateTime _frmDate, DateTime _toDate, string _comCode, string _user);

        //Add by Lakshan 22Aug2017
        [OperationContract]
        decimal GetForwardsaleNew(string _showroom, string _Item, string _com, string _pc);
        //Udaya 21.08.2017 Supplier Pricess Report
        [OperationContract]
        DataTable SupplierPricess_Details(DateTime _frmDate, DateTime _toDate, string _comCode, string _user);
        [OperationContract]
        List<ImportsBLItems> GET_GetTobond_BL_Itm_new(string _tobond, string _bl, string _item, Int32 _line);

        //Add by Lakshan 29Aug2017
        [OperationContract]
        Int32 UpdateMrnItemCode(List<InventoryRequestItem> _reqList, out string _error);

        //Dulanga 2017/08/30
        [OperationContract]
        DataTable GetChanelOnPC(string _company, string _pc, string _code);
        [OperationContract]
        Int16 SaveAllPOList(List<PurchaseOrder> _allpur, MasterAutoNumber _masterAutoNumber, List<PurchaseReq> _PurchaseReq, List<InventoryRequestItem> _Porequest, out string docno, bool _saveSalesOrder = false);
        //Add by Tharanga 2017/08/25
        [OperationContract]
        Int32 Save_InventoryDoShedule(List<InventoryDoShedule> _InvoSheduleList, out string error);

        //Add by Tharanga 2017/08/26
        [OperationContract]
        Int32 GET_MAX_LINE_DO_SHEDULE(Int32 p_sid_seq_no, Int32 p_sid_itm_line, string p_sid_inv_no, string p_sid_itm_cd);
        //Add by Tharanga 2017/09/06
        [OperationContract]
        Int32 update_InventoryDoShedule(List<InventoryDoShedule> _InventoryDoSheduleList);


        //By Akila - 2017/09/12
        [OperationContract]
        Int16 UpdateScanSerailItemDetails(ReptPickSerials _pickSerials, ReptPickItems _pickItems, bool _isNewLine, out string _message);
        [OperationContract]
        bool saveProductConditionUpdate(InventoryHeader _inventoryMovementHeaderMinus, InventoryHeader _inventoryMovementHeaderPlus, List<ReptPickSerials> _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub, MasterAutoNumber _masterAutoNumberMinus, MasterAutoNumber _masterAutoNumberPlus, List<InventoryRequestItem> _reqItems, out string _minusDocNo, out string _plusDocNo, out string error, List<int> hasstuschangelist, string company, string location, string userid,
            ItemConditionSetup _itemconditions, Int32 its_pick, string docno, string itemcd, List<ItemConditionSetup> _repConSetups, Int32 serial, string addedloc,
            InventoryHeader header1, List<InventoryHeader> updateAnl4Hdr, List<int> successItemsAnal4, string newstatus,
            bool isBinToBinTransfer = false, bool isDeValProcess = false);
        [OperationContract]
        Int32 emptyTempSerialItemsansSerials(Int32 SeqNo);
        [OperationContract]
        DataTable getConditionDetails(string serialid);
        //Nuwan 2017.09.04
        [OperationContract]
        DataTable LoadHeaderPCUpdateDocHeadOfficeUserNew(string type, DateTime fromdate, DateTime todate, string location, Int32 isUpdated, string cat1, string cat2, string cat3, string document);
        //Nuwan 2017.09.04
        [OperationContract]
        DataTable LoadHeaderPCUpdateDocWareHouseUserNew(string type, DateTime fromdate, DateTime todate, string location,Int32 isUpdated, string cat1, string cat2, string cat3, string document);
        [OperationContract]
        //Nuwan 2017.09.09 for PDA SRN
        RequestApprovalHeader getApprovedRequestDetails(string invNo, out string error);
        [OperationContract]
        //Nuwan 2017.09.09 for PDA SRN
        List<RequestApprovalDetail> getApprovedRequestItemDetails(string reqno, out string error);
        //SUBODANA 2017/09/15
        [OperationContract]
        DataTable CHECK_TOBOND_GRNLTST(string _com, string _doc, string _itm, out bool _result);

        //Add by Lakshan 18Sep2017
        [OperationContract]
        DataTable GetItemInventoryBalanceStatus_RESNew(string _company, string _location, string _item, string _status);
        //Add by Lakshan 19Sep2017
        [OperationContract]
        List<InventoryBatchN> Get_Scm_Int_Batch(string _doc);

        //by akila 2017/09/20
        [OperationContract]
        DataTable GetRootSchedules(string _comCode, string locCode, DateTime _schDate);
        //Added By Udaya 21.09.2017
        [OperationContract]
        List<InventoryRequest> GET_SOA_REQ_DATA_FOR_DO(string _com, string _type, string _do);
        //Added By Udaya 23.09.2017
        [OperationContract]
        DataTable GetBOQDetails(string _boqNo, DateTime p_from_date, DateTime p_to_date, string p_proCnt, string p_customer, string p_subContractor, string p_com, bool _def);
        //Nuwan 2017/09/22
        [OperationContract]
        InvoiceHeader GetInvoiceDetailForPdaSrn(string invoiceNo, string pc, string com,out string error);
        //Nuwan 2017.09.22
        [OperationContract]
        List<InvoiceItem> GetInvoiceItemDetailForPdaSrn(string invoiceNo, string pc, string com, out string error);
        //Nuwan 2017.09.23
        [OperationContract]
        InvoiceHeader GetInvoiceDetailForPdaDO(string docno,string company, out string error);
        //Add by lakshan 25Sep2017
        [OperationContract]
        List<InventoryLocation> GetItemInventoryBalanceRes(string _company, string _location, string _item, string _status);
        //Nuwan 2017.09.26
        [OperationContract]
        DataTable getSRNItemLineno(Int32 userseq,string itmCd,string stus,out string error);
        //Added By Udaya 28.09.2017
        [OperationContract]
        DataTable Get_canibaliseData(string _docNo, string _comCode);
        [OperationContract]
        DataTable get_PickPlan_InrBatchData(string in_Company, string _loc);
        [OperationContract]
        List<InventoryRequest> GET_INT_REQPRISSUE(InventoryRequest _OBJ);
        //Add by Lakshan 09Oct2017
        [OperationContract]
        List<MasterItem> GetItemListByModel(string _model);
        [OperationContract]
        List<SatProjectDetails> GET_SAT_PRO_DET_DATA(SatProjectDetails _obj);
         [OperationContract]
        SatProjectHeader GET_SAT_PRO_HDR_DATA(string COM, string DOC);
        //Added By tHARANGA 2017/10/07
        [OperationContract]
        DataTable GetRCCbyNoTableNEW(string _rcc);
         [OperationContract]
         DataTable getDocumentDetails(string docno,string doctp=null);

        //Add by  lakshan 19Oct2017
        [OperationContract]
         List<ReptPickSerials> SearchSerialForJobItemAOD(string _com, string _loc, string _itm, string _itmSts, string _jobNo, int _jobLine);
        //Udaya Get courier details 25.Oct.2017
        [OperationContract]
        InventoryHeader GetINTHDR_Details(String Com, String Doc, String Inv);
        // add by tharanga 2017/10/26
        [OperationContract]
        decimal get_buffer_qty(string p_itm, string p_chnl, string p_grade, Int32 p_date, Int32 p_mont);// add by tharanga 2017/10/26
        //Add by Lakshan 30Oct2017
        [OperationContract]
        List<DisposalHeader> GET_DISPOSAL_JOBS_DATA(DisposalHeader oFilter);
        //Add by Lakshan 30Oct2017
        [OperationContract]
        Int32 SAVE_DISPOSAL_ITEMS_WEB(List<DisposalItem> dispItm, out string _err);
        
        // add by tharanga 2017/10/30
         [OperationContract]
        DataTable GET_shpmnet_Hdr(string p_com, DateTime p_frmdate, DateTime P_todate, string p_type, string p_stus,string doc_no);
         // add by tharanga 2017/10/30
         [OperationContract]
         DataTable GET_shpmnet_Detail(string p_doc_no);
         // add by tharanga 2017/10/31
         [OperationContract]
         decimal GET_ICE_ACTL_RT(Int32 p_seq, Int32 p_ref_line);
        //Add by lakshan 02Nov2017
         [OperationContract]
         Int32 DisposalAdjustmentWeb(InventoryHeader _inventoryMovementHeader, List<ReptPickSerials> _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub, MasterAutoNumber _masterAutoNumber, List<DisposalHeader> _dispHdrList, out string _docNo, out string err);
         //subodana
         [OperationContract]
         DataTable GetWarrRangeRmk(string com, string item, string _sup);

         //Add by Lakshan 06Nov2017
        [OperationContract]
         Int32 UpdateGrnIsInProgress(string _poNo, string _user, Int32 _isOnWp, out string _err);

        //Add by lakshan 07Nov2017
        [OperationContract]
        PurchaseOrder GET_PUR_HDR_DATA(string _com, string _doc);
        // add by tharanga 2017/11/11
        [OperationContract]
        List<GRAN_ALWSTUS> GET_GRAN_ALWSTUS(string p_com, string p_cat, string p_stus, string p_new_stus);
        // add by tharanga 2017/11/11
        [OperationContract]
        DataTable SPGETLOCTMCAT(string mli_com, string mli_loc_cd, string mli_cat_1, string mli_itm_stus, string mli_tp,string _frmstus);
        // add by tharanga 2017/11/16
        [OperationContract]
        DataTable Getser_inr_sermst(string _ser);

        //Akila 2017/11/16
        [OperationContract]
        decimal GetSerialUnitCostForExchange(string _docNo, string _itemCode);
        //subodana
        [OperationContract]
        Int32 Update_BI_RqtyNew(List<ImportsBLItems> _bllist, string com, out string _error2);
        
        //Tharindu 2017-11-24
        [OperationContract]
        Int32 SaveWarehouseItem(List<WarehseItemSetup> _lstWarehouse, out string _err);

        //Tharindu 2017-11-27
        [OperationContract]
        DataTable Get_WarehouseItem(string profitcenter, string loc, string itmcat1, string brand, string deftype);

        //Tharindu 2017-12-05
        [OperationContract]
        DataTable Get_Description(string code);

        //Tharindu 2017-12-05
        [OperationContract]
        DataTable Get_Product_refernce(string code, string cat1, string cat2, string cat3, string profcenter,int id);

        //Tharindu 2017-12-05
        [OperationContract]
        Int32 SaveProductRefernce(List<Production_ref> _lstProductionref, out string _err);
        // add by tharanga 2017/11/29
        [OperationContract]
        int SaveInventoryADJRequestData(InventoryRequest _inventoryRequest, MasterAutoNumber _mastAutoNo, out string _docNo);
        // add by tharanga 2017/11/29
        [OperationContract]
        int InventoryADJRequestDataprocess(InventoryRequest _inputInvReq,
        InventoryHeader _invOutHeader, List<ReptPickSerials> _reptPickSerialsOut, List<ReptPickSerialsSub> _reptPickSerialsSubOut, MasterAutoNumber _autonoMinus, out string _docMines,
        InventoryHeader _invINHeader, List<ReptPickSerials> _reptPickSerialsIn, List<ReptPickSerialsSub> _reptPickINSerialsSubIn, MasterAutoNumber _autonoPlus, out string _docPlus,
         out string _assDoc, out string _error);
        // add by tharanga 2017/12/06
        [OperationContract]
        DataTable Getser_int_ser(string _com, string _loc, string _itm_cd, string _itm_ser);
        // add by tharanga 2017/12/18
        [OperationContract]
        MasterItemBlock GetBlockedItmBrand(string _company, string _profit, string _item_brand, int _pricetype, string _catTp, string _mib_cat1, string _mib_cat2);
   
        //nuwan 2017.12.17
        [OperationContract]
        List<InventoryBatchRefN> getItemBalanceQty(string company, string location, string itmcode, string status);
        //nuwan 2017.12.17
        [OperationContract]
        Int32 SaveAdjItemSerials(List<ReptPickSerials> _SerialsLst, out string error);
        // add by tharanga 2018/01/04
        [OperationContract]
        List<GRAN_ALWSTUS> GET_GRAN_ALWSTUS_MORE_CON(string p_com, string p_cat, string p_stus, string p_new_stus, string p_cat1, string p_cat2, string p_cat3);
        //Add by lakshan 13Jan2018
        [OperationContract]
        List<InventoryLocation> GET_INR_LOC_BALANCE_ALL_STUS(InventoryLocation _obj);
        //Add  by Lakshan 15Jan2018
         [OperationContract]
        string CheackSerialIsAvailableInCompany(List<ReptPickSerials> _serialList);
        //subodana
         [OperationContract]
         string GetCaniMainItem(string DOC, string LOC);
         //subodana
         [OperationContract]
         string GetCaniMainSer(string DOC, string LOC);
         //tharanga 2018/01/18
         [OperationContract]
         bool check_sat_qty_inv_balance(string p_job_no, string p_com, string p_loc, string p_pc);

         //tharanga 2018/01/19
         [OperationContract]
         Int32 SaveStockVerification_EXCLE(List<PhysicalStockVerificationHdr> _stockhdr, out string doc, out string _message, List<AuditMemebers> _auditMemebers = null);
        
        //tharanga 2018/01/25
         [OperationContract]
         DataTable dtServiceAgent(string _com, string _code);
        //Add by lakshan 30Jan2018
        [OperationContract]
         List<InventoryBatchRefN> GET_INR_BATCH_BY_JOB_NO_PRO_ISSU(InventoryBatchRefN _obj);
         //tharanga 2018/01/27
         [OperationContract]
         DataTable Get_ADD_Req_by_seq(string _company, string _user, string _reqTp, string _stus, DateTime _frmDt, DateTime _toDt, string _subTp, Int32 _seq);
         //tharanga 2018/02/07
         [OperationContract]
         List<ReptPickSerials> GetSerialForExchange(string _company, string _location, string _user, string _session, string _defBin, string _invoice, int _baseRefline);
        //Nuwan 2018.02.08
         [OperationContract]
         DataTable getAllSerialDetails(string com, string loc, string ser1, string ser2, string ser3,out string error);
        //Nuwan 2018.02.12
         [OperationContract]
         DataTable getBondDocJobNo(string itmcd, string com, string stus, Int32 lineno, string req, string docno, out string error);
        //Add by lakshan 13Feb2018
         [OperationContract]
         DataTable GET_TEMP_DOC_PARTIAL(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _from, DateTime _To);
         //Add by lakshan 13Feb2018
         [OperationContract]
         List<ReptPickSerials> GetAllScanSerialsListPartial(string _company, string _location, string _user, Int32 _userseqno, string _doctype);
         //Add by lakshan 13Feb2018
         [OperationContract]
         Int32 IsExistInTempPickSerialPartial(string _companyCode, string _userSeqNo, string _itemCode, string _serialNo1);
         //Add by lakshan 13Feb2018
         [OperationContract]
         Int16 SaveAllScanSerialsPartial(ReptPickSerials _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub);
         //Add by lakshan 13Feb2018
         [OperationContract]
         List<ReptPickSerials> GET_TEMP_PICK_SER_DATA_PARTIAL(ReptPickSerials _repPickSer);
         //Add by lakshan 13Feb2018
         [OperationContract]
         List<ReptPickItems> GET_TEMP_PICK_ITM_DATA_PARTIAL(ReptPickItems _repTemItm);
         //Add by lakshan 13Feb2018
         [OperationContract]
         List<ReptPickHeader> GET_TEMP_PICK_HDR_DATA_PARTIAL(ReptPickHeader _repHdr);
         //Add by lakshan 13Feb2018
         [OperationContract]
         Int32 DocumentFinishPartially(string _com, string docno, string doctyp, Int32 status, string _user, out string error);
        //tharanga 2018/02/15
         [OperationContract]
         List<AUD_CHARGES> get_AUD_CHARGES(string _aud_com_cd, string _aud_tp, string _aud_reason, string _aud_itm_stus, string _aud_remarks, string _aud_charge_base_on, string _aud_value, Decimal _aud_charge, string _aud_price_book, string _aud_p_level, DateTime _aud_valid_from, DateTime _aud_valid_to,
                                String _aud_effect_to, Int32 _aud_charge_amend, string _aud_stock_adjuestment_type, string _aud_item_cd, string _aud_item_cat1, string _aud_item_brand);
         
         //Add by lakshan 14Feb2018
         [OperationContract]
         Int32 SaveDocumentHdrFromPartially(string docno, string doctyp, out string error);

        //Add by lakshan 17Feb2018
         [OperationContract]
         Int16 UpdateAllScanSerialsPartial(ReptPickSerials _reptPickSerials, out string error);

         //tharanga 2018/02/19
         [OperationContract]
         void updateAuditJobSerails_charhes(List<AuditJobSerial> _AuditJobSerial, out string _message);
        //nuwan 2018.02.14
         [OperationContract]
         DataTable getBinDetailsSerPic(string company, string location, string pgeNum, string pgeSize, string searchFld, string searchVal, out string error);
         //nuwan 2018.02.14
         [OperationContract]
         DataTable getSer1DetailsSerPic(string company, string location,string itemcd, string pgeNum, string pgeSize, string searchFld, string searchVal, out string error);
        
         //tharanga 2018/02/20
         [OperationContract]
         DataTable get_audit_itm_charges_sum(string _audjs_job, string _com);

         //tharanga 2018/02/21
         [OperationContract]
         List<AuditJobSerial> GetProcessedJobSerials_all(string _subJobNo, Int32 _startIndex, Int32 _endIndex, Int32 _status,string _com);
          //tharanga 2018/02/21
         [OperationContract]
        Int16 Updatei_JOB_Hdr_appby(string _AUSH_JOB, string _AUSH_COM, string _AUSH_MOD_BY, Int32 _AUSH_STUS, Int32 _ADJ_REQ, out string error);
        //subodana 2018/02/22
         [OperationContract]
         DataTable GetAllPendingPOrderBank(PurchaseOrder _paramPurchaseOrder);
         //tharanga - 2018/02/23
         [OperationContract]
         int SaveInventoryRequestData_ADUIT(List<InventoryRequest> _inventoryRequest, List<InventoryRequestItem> _InventoryRequestItem, List<InventoryRequestSerials> _InventoryRequestSerials, MasterAutoNumber _mastAutoNo);
         //tharanga - 2018/02/26
         [OperationContract]
         Decimal Get_def_price_from_pc(string _pbook, string _plevel, string _itmcd, DateTime _date);
         //Dualj 2018/Feb/22
         [OperationContract]
         Int32 CheckItemUnitsIsDecimal(string mi_cd);
        //subodana 2018-02-26
            [OperationContract]
         DataTable GetAllPendingInventoryOutwardsTable2(InventoryHeader _inventoryRequest);
            //subodana 2018-03-12
            [OperationContract]
            string GET_INR_BATCH_ITMforJob(string _job, Int32 _itmline, string _com);
        //Sanjeewa 2018-03-13
        [OperationContract]
        Int16 UpdateMigrationInvoiceDetails(string _invno, int _invline, decimal _tax, decimal _tot);

            //Nuwan 2018.03.13
            [OperationContract]
            bool reopenTempDocumentGRN(string docnum, string doctp, string seqno,string userid, out string error);
        //Nuwan 2018.03.13
            [OperationContract]
            DataTable getUserScanFinishedSerial(string seq,string userid);
            //Dulaj 2018-MAr-15
            [OperationContract]
            DataTable GetItemConditionsByDate(string _com, DateTime _from_date, DateTime _to_date, string _otherLoc, string _loc, string user);
        //Nuwan 2018.03.21
            [OperationContract]
            DataTable getSerialDetailsStatus(string ser1, string ser2, string com, out string error);
        //Dulaj 2018-MAr-21
            [OperationContract]
            Int32 CheckExessQty(string _tp, string _cd, DateTime _grnDate, string _pty_cd);
            //THARANGA 2018/03/28
            [OperationContract]
            List<AuditRemarkValue> GetPhicalStockRemark_FILTER(string p_ausv_job, Int32 p_ausv_line, Int32 p_ausv_job_seq, string p_ausv_itm, string p_ausv_itm_stus, Int32 p_ausv_ser_id, string p_ausv_rpt_stus);

        //tharanga  2018/03/30
          [OperationContract]
            DataTable Get_pending_DIN(string _in_item, string in_serial, Int32 _in_ser_id);
            [OperationContract]
            bool SendSms(string mobileNo, string message, string customerName, string sender,string company);

            //Dulaj 2018-Apr-19
            [OperationContract]
        string GetBinCodeForNonSeralizeItem(string _itemCode, string _loc, string _com, string status);
            //tharanga  2018/04/25
            [OperationContract]
            DataTable Get_alladj_Req(string _company, string _user, string _reqTp, string _stus, DateTime _frmDt, DateTime _toDt, string _subTp);
            //tharanga  2018/04/27
          [OperationContract]
            DataTable Get_Delivery_det_by_ser(string _company, string _item, string _serial, string _loc, string _doc_tp);
          //tharanga  2018/05/03
          [OperationContract]
          Int16 update_adj_req_itm_status(Int32 itri_seq_no, Int32 itri_line_no, string _itri_itm_cd, string itri_itm_stus, string tri_note, out string error);
          //DULAJ  2018/May/03
          [OperationContract]
          DataTable GetDepreciationMethods(string _company);
          //DULAJ  2018/May/03
          [OperationContract]
          Int32 SaveMasterAssetPrameter(MasterAssetParameter Msp);
          [OperationContract]
          DataTable GetAssetParameters(string _company);
          //DULAJ  2018/May/03
          [OperationContract]
          Int32 UpdateMasterAssetPrameter(MasterAssetParameter Msp);
          //tharanga  2018/05/16
          [OperationContract]
          DataTable Get_get_fixed_asset(string _com, string _loc, string _pc, string _ser, string _itm);

          //Rangika 2018-04-26
          [OperationContract]
          Int32 SaveItemStatusChangeDef(List<ItemStatus_Change_def> _ItemStatus_Change_def, out string _err);

          //Rangika 2018-05-18
          [OperationContract]
          DataTable getExelUploadResig(string userId);

          //Rangika 2018-05-25
          [OperationContract]
          Int32 Updateecxelbulk(List<User_Resign_Bulk> _User_Resign_Bulk, out string _err);

          //tharanga  2018/05/16
          [OperationContract]
          DataTable GET_TEMP_PICK_SER_BY_SER(string _ser_1, string _ser_2);

          //// tHARINDU 201-06-04
          //[OperationContract]
          //Int16 ADJPlus_FIXA(InventoryHeader _inventoryMovementHeader, List<ReptPickSerials> _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub, MasterAutoNumber _masterAutoNumber, out string _docNo, bool IsTemp = false, bool Isfixdb = false);

          //Pasindu  2018/06/12
          [OperationContract]
          DataTable getBinLocation(string p_com, string p_loc);

          //Dulaj 2018/Jun/04
          [OperationContract]
          DataTable Check_Already_savedMethods(string cat01, string cat02, string status);
          //THARANGA 2018/07/05
          [OperationContract]
          Int16 SaveAllScanSerials_NEW(ReptPickSerials _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub);
       

          // tHARINDU 201-06-04
          [OperationContract]
          Int16 ADJPlus_FIXA(InventoryHeader _inventoryMovementHeader, List<ReptPickSerials> _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub, MasterAutoNumber _masterAutoNumber, out string _docNo, bool IsTemp = false, bool Isfixdb = false);

          //[OperationContract]
          //Int16 ADJMinus_FIXA(InventoryHeader _inventoryMovementHeader, List<ReptPickSerials> _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub, MasterAutoNumber _masterAutoNumber, out string _docNo, string invdalcon, string invreportdal, bool IsTemp = false);

          [OperationContract]
          Int16 SaveAdjMinAdjplus(InventoryHeader _inventoryMovementHeader, List<ReptPickSerials> _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub, MasterAutoNumber _masterAutoNumber, InventoryHeader _invMovtHeaderFix, out string _adjno, bool IsTemp = false, bool Isfixdb = false);
        
        //THARANGA 2018/07/05
        [OperationContract]
        Int32 SAVE_REF_DATRERANGE(DateTime frm_date, DateTime todare, string user_id);
           //THARANGA 2018/07/05
        [OperationContract]
        bool MRN_VALIDATION(string _Reservation, string _Item, string _Status, string ComCode, string loc_cd, string chvl_cd, DateTime _date, decimal _Qty, string DispatchRequried, out string _MSG, out Int32 _autoAppr, out Int32 _mainAutoAppr, out bool ins, Boolean _isSubItemHavenew, out bool Check_MRN_Item_exceed, decimal currentCost_Loc, decimal Ins_value, decimal TotalGIT, out Boolean _isResvNo);

           //THARANGA 2018/07/05
        [OperationContract]
        List<InventoryBatchN> Get_Int_Batch_by_itm(string _doc, string _itmcd, string status);
        //Dulaj 2018-Jul/31
        [OperationContract]
        DataTable GetItemPricePc(string pb, string pl, string itm, DateTime dt, string pc);
        //Dulaj 2018-Aug/01
        [OperationContract]
        DataTable GetItemsByBin(string com, string itemCode, string loc, string bin, string itemStatus);
        //THARANGA 2018/08/04
        [OperationContract]
        DataTable GetItems_git_get(string com, string loc, Int32 ser_id, string itemStatus);
        //THARANGA 2018/08/04
        [OperationContract]
        DataTable getMstSysPara_new(string _com, string _pty_tp, string _pty_cd, string _rest_tp, string _dir_pry_cd);
   //tharanga 2018/08/15
         [OperationContract]
        decimal GET_INSVALUE_BYLOC(string _company, string _location);
         //tharanga 2018/08/15
         [OperationContract]
         decimal GetLatestCost_Loc(string _company, string _location);
        //tharanga 2018/08/15
         [OperationContract]
         DataTable Get_GIT_Detailsnew(DateTime _asat, string _com, string _loc, string in_Itemcode, string in_Brand, string in_Model, string in_Itemcat1,
           string in_Itemcat2, string in_Itemcat3, string in_Itemcat4, string in_Itemcat5, string _user);
         //tharanga 2018/08/24
         [OperationContract]
         Int16 UpdateInventoryLocation(InventoryLocation _invLoca, Int16 _invDirect);
         //tharanga 2018/08/24
         [OperationContract]
         Int32 UpdateLocationRes(string _company, string _location, string _item, string _stus, string _user, decimal _qty);
         //tharanga 2018/08/24
         [OperationContract]
         Int32 UpdateLocationResRevers(string _company, string _location, string _item, string _stus, string _user, decimal _qty);
         //tharanga 2018/08/24
         [OperationContract]
         DataTable Getmovsubtp(string _maintype);
         [OperationContract]
         DataTable LoadCusdecDatabyDoc(string doc);
    
         
        //Tharindu 2018-07-12
        [OperationContract]
        DataTable GetBinBalbyitm(string p_com, string p_itm_code, string p_inb_loc, string p_inb_bin);
        //Dulaj 2018/sep/17
        [OperationContract]
        Decimal GetBufferLevelInrLocation(string com, string itmCd, string loc);
        //Nuwan 2018.10.04
        [OperationContract]
        DataTable getCustomerInvoiceData(string company, string customer,string item,string model,string catgory,string invno, out string error);
        //Nuwan 2018.10.05
        [OperationContract]
        DataTable getCustomerJobHistoryData(string company, string customer, string item, string model, string catgory, string serial, string invno, out string error);
        //Nuwan 2018.10.08
        [OperationContract]
        DataTable  getSerialDoDetails(string serial, string com, out string error);
        //Nuwan 2018.10.08
        [OperationContract]
        DataTable  getInvoiceDetails(string invno, string com, out string error);
        //THARANGA 2018/07/05
  
        ////THARANGA 2018/07/05
        //[OperationContract]
        //Boolean IsExternalServiceAgent_AGENT(string _com, string _code);
       // THARANGA 2018/11/05
        [OperationContract]
        DataTable check_ser_in_by_srn(string com, string invno, string itm, string ser, string serid, out string error);
        //Dulaj 2018/Oct/12
        [OperationContract]
        DataTable GetQRMethod(string com, Int32 method);
        //THARANGA 2018/11/21
        [OperationContract]
        Mst_Movsubtp get_sub_type(string _maintype, string _subtype, out string _err);

        //Dulaj 2018/Dec/03
         [OperationContract]
        Int32 UpdateCustDecHdr(string com, string _docno,string modBy,string session);

         //Dulaj 2018/Aug/13
         [OperationContract]
         Int32 SaveSatProjectKitDetails(SatProjectKitDetails satKit);
         [OperationContract]
         List<SatProjectKitDetails> GetSatKitItems(string boqno);
         //Dulaj 2018/Aug/29
         [OperationContract]
         DataTable getItemCostByJobNo(string jobNo);
         //Dulaj 2018/Aug/29
         [OperationContract]
         Int32 DeleteKitItemsBySeq(Int32 seq);
        //Dulaj 2019/Jan/22
        [OperationContract]
         Int32 ItemPurchaseRestriction(string com, MasterItem itm, decimal qty, string itemStatus, out string _err);
        
         //Dulaj 2019/01/16
         [OperationContract]
         Int16 deletePickedManualDocDetail(string _refNo, string _Loc, string _user, string _Status);
    }
}
