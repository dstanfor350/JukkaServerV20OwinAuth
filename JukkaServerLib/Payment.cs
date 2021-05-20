// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using Newtonsoft.Json;

namespace JukkaServerLib
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Payment
    {
        [JsonProperty]
        public string MachineId { get; set; }
        [JsonProperty]
        public int OrderId { get; set; }
        [JsonProperty]
        public int StoreId { get; set; }
        [JsonProperty]
        public string InvoiceNumber { get; set; }
        [JsonProperty]
        public string TransactionId { get; set; }
        [JsonProperty]
        public decimal ApprovedAmount { get; set; }
        [JsonProperty]
        public int ApprovalCode { get; set; }
        [JsonProperty]
        public string MaskedCardNumber { get; set; }
        [JsonProperty]
        public string CardType { get; set; }
        [JsonProperty]
        public string NameOnCard { get; set; }
        [JsonProperty]
        public string IssuerName { get; set; }
        [JsonProperty]
        public int ExpiryDate { get; set; }
        [JsonProperty]
        public string EntryMode { get; set; }
        [JsonProperty]
        public int ErrorCode { get; set; }
        [JsonProperty]
        public string Message { get; set; }

        Payment()
        {
        }

        Payment(string machineId, int orderId, int storeId, string invoiceNumber,
            string transactionId, decimal approvedAmount, string maskedCardNumber,
            string cardType, string nameOnCard, string issuerName, int expiryDate,
            string entryMode, int errorCode, string message)
        {
            MachineId = machineId;
            OrderId = orderId;
            StoreId = storeId;
            InvoiceNumber = invoiceNumber;
            TransactionId = transactionId;
            ApprovedAmount = approvedAmount;
            ApprovalCode = ApprovalCode;
            MaskedCardNumber = maskedCardNumber;
            CardType = cardType;
            NameOnCard = nameOnCard;
            IssuerName = issuerName;
            ExpiryDate = expiryDate;
            EntryMode = entryMode;
            ErrorCode = errorCode;
            Message = message;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
