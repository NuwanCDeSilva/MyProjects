using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayECR;
using System.Text.RegularExpressions;
using System.IO;

namespace FF.WindowsERPClient
{
    public class CCTBaseComponent
    {
        #region private variables
        string _errorLogFilePath = "C:\\SCM2_CCT_ERROR_LOG";
        string _errorFileName = "SCM2_CCTErrorLog.txt";
        string _comportId;
        ECR CCT;
        static CCTBaseComponent _cctBase;
        string _hostNumber = string.Empty;
        string _payAmount = string.Empty;
        string _invNo = string.Empty;

        //return values
        string _expireDate = string.Empty;
        string _approvelCode = string.Empty;
        string _retrivelRefNo = string.Empty;
        string _transTrace = string.Empty;
        string _batchNo = string.Empty;
        string _hostNo = string.Empty;
        string _terminalId = string.Empty;
        string _merchantId = string.Empty;
        string _aid = string.Empty;
        string _transCryptogram = string.Empty;
        string _cardHolderName = string.Empty;

        #endregion

        #region enums
        //Credit Card Transaction Mode 
        enum CCT_TansMode { Echo, DirectPay, Pay, Swipe };

        //Card Types
        enum CardTypes { VISA, MASTER, DINERS, AMEX, DEBIT, JCB, DISCOVER };
        #endregion

        #region public properties
        public string ExpireDate { get { return _expireDate; } }
        public string AppovelCode { get { return _approvelCode; } }
        public string RetrevelRefNo { get { return _retrivelRefNo; } }
        public string TransTrace { get { return _transTrace; } }
        public string BatchNo { get { return _batchNo; } }
        public string HostNo { get { return _hostNo; } }
        public string TerminalId { get { return _terminalId; } }
        public string MerchantId { get { return _merchantId; } }
        public string Aid { get { return _aid; } }
        public string TransCryptogram { get { return _transCryptogram; } }
        public string CardHolderName { get { return _cardHolderName; } }
        public static CCTBaseComponent CCTBase
        {
            get
            {
                if (_cctBase == null)
                {
                    _cctBase = new CCTBaseComponent();
                }
                return _cctBase;
            }
        }
        public string HostNumber { set { _hostNumber = value.PadLeft(2, '0'); } }
        public string PayAmount { set { _payAmount = value.ToString().PadLeft(12, '0'); } }
        public string InvoiceNo { set { _invNo = value.PadLeft(24, '0'); } }
        //public bool IsCCTOnline { get { return IsCCTAvailable(); } }
        public bool IsCCTOnline { get; set; }
        public string ComPort { set { _comportId = value; } }

        #endregion

        //Process CCT transaction
        public void SwipeCard(ref string _cardNo, ref string _cardType, ref string _error)
        {
            _error = string.Empty;
            try
            {
                string _sendCommand = CCTSendCommand(CCT_TansMode.Swipe);
                string _receiveCommand = string.Empty;

                _receiveCommand = SendToCCT(_sendCommand);
                if (IsTransactionValid(_receiveCommand, ref _error))
                {
                    _cardNo = _receiveCommand.Substring(4, 19).Trim();
                    var maskedCardNumberWithSpaces = Regex.Replace(_cardNo, ".{4}", "$0 ");
                    _cardNo = maskedCardNumberWithSpaces;

                    string _sendCardNo = _receiveCommand.Substring(4, 6).Trim();

                    //remove all non digit charters
                    Regex _digitOnlyNo = new Regex("[^0-9]+");
                    _sendCardNo = _digitOnlyNo.Replace(_sendCardNo, "").ToString();
                    _cardType = GetCardType(_sendCardNo);
                }
                else { throw new Exception(_error); }

                /*if (!string.IsNullOrEmpty(_receiveCommand))
                {
                    if (_receiveCommand.StartsWith("C910"))
                    {
                        _cardNo = _receiveCommand.Substring(4, 19).Trim();
                        var maskedCardNumberWithSpaces = Regex.Replace(_cardNo, ".{4}", "$0 ");
                        _cardNo = maskedCardNumberWithSpaces;

                        string _sendCardNo = _receiveCommand.Substring(4, 6).Trim();

                        //remove all non digit charters
                        Regex _digitOnlyNo = new Regex("[^0-9]+");
                        _sendCardNo = _digitOnlyNo.Replace(_sendCardNo, "").ToString();
                        _cardType = GetCardType(_sendCardNo);
                    }
                    else { throw new Exception("Invalid card swipe"); }
                }
                else { throw new Exception("Invalid card swipe"); }*/
            }

            catch (Exception ex)
            {
                _error = ex.Message;
                _cardNo = string.Empty;
                _cardType = string.Empty;
                WriteErrorLog("Card swipe failed " + ex.Message + " - " + ex.StackTrace);
            }
        }

        private string CCTSendCommand(CCT_TansMode _transactionMode)
        {
            string _command = string.Empty;

            try
            {
                switch (_transactionMode)
                {
                    case CCT_TansMode.Echo:
                        _command = "C902";
                        break;
                    case CCT_TansMode.Swipe:
                        _command = string.Format("C300{0}{1}{2}", _hostNumber, _payAmount, _invNo);
                        break;
                    case CCT_TansMode.Pay:
                        _command = string.Format("R910{0}", _payAmount);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                WriteErrorLog("Prepare CCT Send command failed " + ex.Message + " - " + ex.StackTrace);
                _command = string.Empty;
            }
            return _command;
        }

        private string SendToCCT(string _command)
        {
            string _receiveCommand = string.Empty;
            string _receiveMsg = string.Empty;
            int _receiveStatus = 0;
            int _receiveBufferSize = 1000;

            try
            {
                CCT.SendReceive(_command, ref _receiveCommand, ref _receiveStatus, ref _receiveMsg, _receiveBufferSize);
                if ((_receiveStatus != 0) && (!string.IsNullOrEmpty(_receiveMsg)))
                {
                    throw new Exception(_receiveMsg);
                }
            }
            catch (Exception ex)
            {
                WriteErrorLog("Send to CCT failed " + ex.Message + " - " + ex.StackTrace);
                _receiveCommand = string.Empty;
            }

            return _receiveCommand;
        }

        public void InitializeCCT()
        {
            CCT = new ECR(_comportId, 120000, 1000, "C:\\ECR_LOG", true, true);
        }
        private bool IsCCTAvailable()
        {
            bool _iscctAvailable = false;

            try
            {
                CCT = new ECR(_comportId, 120000, 1000, "C:\\ECR_LOG", true, true);

                string _sendCommand = CCTSendCommand(CCT_TansMode.Echo);
                string _receiveCommand = string.Empty;
                string _error = string.Empty;

                _receiveCommand = SendToCCT(_sendCommand);
                //if (!string.IsNullOrEmpty(_receiveCommand) && _receiveCommand.StartsWith("R902"))
                if (IsTransactionValid(_receiveCommand, ref _error))
                {
                    _iscctAvailable = true;
                }
                else
                {
                    throw new Exception(_error);
                }

            }
            catch (Exception ex)
            {
                WriteErrorLog("CCT is unavailable. " + ex.Message + " - " + ex.StackTrace);
                _iscctAvailable = false;
            }

            return _iscctAvailable;
        }

        private string GetCardType(string _cardNo)
        {
            string _cardType = string.Empty;

            try
            {

                if (Regex.IsMatch(_cardNo, @"^4[0-9]{5}(?:[0-9]{3})?$")) { _cardType = CardTypes.VISA.ToString(); }
                else if (Regex.IsMatch(_cardNo, @"^5[1-5][0-9]{4}$")) { _cardType = CardTypes.MASTER.ToString(); }
                else if (Regex.IsMatch(_cardNo, @"^3[0-9]{5}$")) { _cardType = CardTypes.DINERS.ToString(); }
                else if (Regex.IsMatch(_cardNo, @"^3[47][0-9]{4}$")) { _cardType = CardTypes.AMEX.ToString(); }
                else if (Regex.IsMatch(_cardNo, @"^352[89][0-9]{2}$")) { _cardType = CardTypes.JCB.ToString(); }
                else if (Regex.IsMatch(_cardNo, @"^[0-9]{6}$")) { _cardType = CardTypes.DEBIT.ToString(); }
                else if (Regex.IsMatch(_cardNo, @"^6(?:011|5[0-9]{2})[0-9]{12}$")) { _cardType = CardTypes.DISCOVER.ToString(); }

            }
            catch (Exception ex)
            {
                WriteErrorLog("Unknown card type" + ex.Message + " - " + ex.StackTrace);
                _cardType = string.Empty;
            }

            return _cardType;
        }

        public string ProcessPayment(ref int _status)
        {
            string _returnMessage = string.Empty;
            string _receiveCommand = string.Empty;

            try
            {
                string _sendCommand = CCTSendCommand(CCT_TansMode.Pay);

                _receiveCommand = SendToCCT(_sendCommand);
                if (IsTransactionValid(_receiveCommand, ref _returnMessage))
                {
                    _returnMessage = "Payment successful";
                    _status = 0;
                    IsCCTOnline = true;
                    SetReturnValues(_receiveCommand);
                }
                else { IsCCTOnline = false; throw new Exception(_returnMessage); }

                /* if (!string.IsNullOrEmpty(_receiveCommand))
                 {
                     if (_receiveCommand.StartsWith("R300"))
                     {
                         string _statusCode = _receiveCommand.Substring(27, 2);
                         if (_statusCode == "TA") { _returnMessage = "Transaction aboard"; _status = 1; }
                         else if (_statusCode == "00") { _returnMessage = "Transaction success"; _status = 0; }
                     }
                     else { throw new Exception("Invalid operation"); }
                 }
                 else { throw new Exception("Transaction failed"); }*/
            }

            catch (Exception ex)
            {
                WriteErrorLog("Payment process failed" + ex.Message + " - " + ex.StackTrace);
                _returnMessage = ex.Message;
                _status = 1;
            }

            return _returnMessage;
        }

        //Error Log
        public void WriteErrorLog(string _error)
        {
            FileStream _fileStream = null;

            try
            {
                if (File.Exists(Path.Combine( _errorLogFilePath , _errorFileName)))
                {
                    _fileStream = File.OpenWrite(Path.Combine(_errorLogFilePath, _errorFileName));
                    if (_fileStream.Length > (1024 * 1024) * 100)
                    {
                        _fileStream.Close();
                        File.Delete(Path.Combine( _errorLogFilePath , _errorFileName));
                        _fileStream = File.Create(Path.Combine(_errorLogFilePath, _errorFileName));
                    }
                }
                else
                {
                    if (!Directory.Exists(_errorLogFilePath))
                    { 
                        Directory.CreateDirectory(_errorLogFilePath);
                    }

                    _fileStream = File.Create(Path.Combine(_errorLogFilePath, _errorFileName));
                }

                _fileStream.Close();
                StreamWriter _streamWriter = new StreamWriter(Path.Combine(_errorLogFilePath, _errorFileName), true);
                _streamWriter.WriteLine(string.Format("{0} -> {1}", DateTime.Now.ToString("yyyy-MMM-dd hh:mm:ss tt"), _error));
                _streamWriter.Flush();
                _streamWriter.Close();
            }
            catch (Exception)
            {
                if (_fileStream != null) { _fileStream.Close(); }
            }
        }

        private bool IsTransactionValid(string _receiveCommand, ref string _message)
        {
            bool _isValid = true;

            try
            {
                if (string.IsNullOrEmpty(_receiveCommand))
                {
                    _message = "Invalid card swipe";
                    _isValid = false;
                }
                else if (_receiveCommand.StartsWith("R902"))
                {
                    _message = string.Empty;
                    _isValid = true;
                }
                else if (_receiveCommand.StartsWith("C910"))
                {
                    _message = string.Empty;
                    _isValid = true;
                }
                else if ((_receiveCommand.StartsWith("R300")) || (_receiveCommand.StartsWith("R200")))
                {
                    if (_receiveCommand.Length >= 29)
                    {
                        _message = GetStatusDefinition(_receiveCommand.Substring(27, 2));
                        if (string.IsNullOrEmpty(_message))
                        {
                            _isValid = true;
                        }
                        else
                        {
                            _isValid = false;
                        }
                    }
                    else
                    {
                        _message = "Invalid card swipe";
                        _isValid = false;
                    }
                }
                else
                {
                    _message = "Invalid operation";
                    _isValid = false;
                }
            }
            catch (Exception ex)
            {
                WriteErrorLog("Check CCT available failed. " + ex.Message + " - " + ex.StackTrace);
                _message = ex.Message;
                _isValid = false;
            }
            return _isValid;
        }

        private string GetStatusDefinition(string _status)
        {
            string _definition = string.Empty;

            switch (_status)
            {
                case "SE":
                    _definition = "CCT not available"; // terminal full
                    break;
                case "PE":
                    _definition = "Invalid swipe"; // Pin Entry Error
                    break;
                case "IC":
                    _definition = "Invalid swipe / Card not support";
                    break;
                case "EC":
                    _definition = "Card Expired";
                    break;
                case "ZE":
                    _definition = "Already settled"; //Zero Amount Settlement/No transaction
                    break;
                case "BU":
                    _definition = "Error - Call bank"; //Batch Not found during settlement on Host
                    break;
                case "CE":
                    _definition = "Re-try the transaction";//Comms Error or Connection Timeout
                    break;
                case "RE":
                    _definition = "Check trace"; //Record error ,Trace# not found during Void C201 or Sale Comp. C220 
                    break;
                case "HE":
                    _definition = "Check host#";//Host number Error applicable where msg includes host number field.
                    break;
                case "LE":
                    _definition = "Check phone line";//Line Error ,No phone line.
                    break;
                case "TA":
                    _definition = "Transaction aboard";
                    break;
                case "AE":
                    _definition = "Invalid pay amount";//Amount not matched .
                    break;
                default:
                    _definition = string.Empty;
                    break;
            }
            return _definition;
        }

        private void SetReturnValues(string _receiveCommand)
        {
            try
            {
                ClearReturnValues();

                if (_receiveCommand.Length >= 27)
                {
                    _expireDate = _receiveCommand.Substring(23, 4);
                }

                if (_receiveCommand.Length >= 35)
                {
                    _approvelCode = _receiveCommand.Substring(29, 6);
                }

                if (_receiveCommand.Length >= 47)
                {
                    _retrivelRefNo = _receiveCommand.Substring(35, 12);
                }

                if (_receiveCommand.Length >= 53)
                {
                    _transTrace = _receiveCommand.Substring(47, 6);
                }

                if (_receiveCommand.Length >= 59)
                {
                    _batchNo = _receiveCommand.Substring(53, 6);
                }

                if (_receiveCommand.Length >= 61)
                {
                    _hostNo = _receiveCommand.Substring(59, 2);
                }

                if (_receiveCommand.Length >= 69)
                {
                    _terminalId = _receiveCommand.Substring(61, 8);
                }

                if (_receiveCommand.Length >= 84)
                {
                    _merchantId = _receiveCommand.Substring(69, 15);
                }

                if (_receiveCommand.Length >= 98)
                {
                    _aid = _receiveCommand.Substring(84, 14);
                }

                if (_receiveCommand.Length >= 114)
                {
                    _transCryptogram = _receiveCommand.Substring(98, 16);
                }

                if (_receiveCommand.Length >= 140)
                {
                    _cardHolderName = _receiveCommand.Substring(114, 26);
                }
            }
            catch (Exception ex)
            {
                WriteErrorLog("Set return values failed " + ex.Message + " - " + ex.StackTrace);
            }
        }

        private void ClearReturnValues()
        {
            _expireDate = string.Empty;
            _approvelCode = string.Empty;
            _retrivelRefNo = string.Empty;
            _transTrace = string.Empty;
            _batchNo = string.Empty;
            _hostNo = string.Empty;
            _terminalId = string.Empty;
            _merchantId = string.Empty;
            _aid = string.Empty;
            _transCryptogram = string.Empty;
            _cardHolderName = string.Empty;
        }
    }
}
