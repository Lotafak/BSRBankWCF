﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Client.ServiceReference1 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Account", Namespace="http://schemas.datacontract.org/2004/07/BSRBankWCF.Models")]
    [System.SerializableAttribute()]
    public partial class Account : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal AmountField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string BankAccountNumberField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Amount {
            get {
                return this.AmountField;
            }
            set {
                if ((this.AmountField.Equals(value) != true)) {
                    this.AmountField = value;
                    this.RaisePropertyChanged("Amount");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string BankAccountNumber {
            get {
                return this.BankAccountNumberField;
            }
            set {
                if ((object.ReferenceEquals(this.BankAccountNumberField, value) != true)) {
                    this.BankAccountNumberField = value;
                    this.RaisePropertyChanged("BankAccountNumber");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Message", Namespace="http://schemas.datacontract.org/2004/07/BSRBankWCF.Models")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Client.ServiceReference1.ErrorMessage))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Client.ServiceReference1.ResultMessage))]
    public partial class Message : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool IsErrorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageTextField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsError {
            get {
                return this.IsErrorField;
            }
            set {
                if ((this.IsErrorField.Equals(value) != true)) {
                    this.IsErrorField = value;
                    this.RaisePropertyChanged("IsError");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string MessageText {
            get {
                return this.MessageTextField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageTextField, value) != true)) {
                    this.MessageTextField = value;
                    this.RaisePropertyChanged("MessageText");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ErrorMessage", Namespace="http://schemas.datacontract.org/2004/07/BSRBankWCF.Models.MessageImpl")]
    [System.SerializableAttribute()]
    public partial class ErrorMessage : Client.ServiceReference1.Message {
        private ErrorMessage()
        {
        }
        public ErrorMessage( string message )
        {
            MessageText = message;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ResultMessage", Namespace="http://schemas.datacontract.org/2004/07/BSRBankWCF.Models.MessageImpl")]
    [System.SerializableAttribute()]
    public partial class ResultMessage : Client.ServiceReference1.Message {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="WithdrawDeposit", Namespace="http://schemas.datacontract.org/2004/07/BSRBankWCF.Models")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Client.ServiceReference1.Withdraw))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Client.ServiceReference1.Deposit))]
    public partial class WithdrawDeposit : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal AmountField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string BankAccountNumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CredentialsField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Amount {
            get {
                return this.AmountField;
            }
            set {
                if ((this.AmountField.Equals(value) != true)) {
                    this.AmountField = value;
                    this.RaisePropertyChanged("Amount");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string BankAccountNumber {
            get {
                return this.BankAccountNumberField;
            }
            set {
                if ((object.ReferenceEquals(this.BankAccountNumberField, value) != true)) {
                    this.BankAccountNumberField = value;
                    this.RaisePropertyChanged("BankAccountNumber");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Credentials {
            get {
                return this.CredentialsField;
            }
            set {
                if ((object.ReferenceEquals(this.CredentialsField, value) != true)) {
                    this.CredentialsField = value;
                    this.RaisePropertyChanged("Credentials");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Withdraw", Namespace="http://schemas.datacontract.org/2004/07/BSRBankWCF.Models.WithdrawDepositImpl")]
    [System.SerializableAttribute()]
    public partial class Withdraw : Client.ServiceReference1.WithdrawDeposit {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Deposit", Namespace="http://schemas.datacontract.org/2004/07/BSRBankWCF.Models.WithdrawDepositImpl")]
    [System.SerializableAttribute()]
    public partial class Deposit : Client.ServiceReference1.WithdrawDeposit {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Transfer", Namespace="http://schemas.datacontract.org/2004/07/BSRBankWCF.Models")]
    [System.SerializableAttribute()]
    public partial class Transfer : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int AmountField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FromField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TitleField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Amount {
            get {
                return this.AmountField;
            }
            set {
                if ((this.AmountField.Equals(value) != true)) {
                    this.AmountField = value;
                    this.RaisePropertyChanged("Amount");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string From {
            get {
                return this.FromField;
            }
            set {
                if ((object.ReferenceEquals(this.FromField, value) != true)) {
                    this.FromField = value;
                    this.RaisePropertyChanged("From");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Title {
            get {
                return this.TitleField;
            }
            set {
                if ((object.ReferenceEquals(this.TitleField, value) != true)) {
                    this.TitleField = value;
                    this.RaisePropertyChanged("Title");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="History", Namespace="http://schemas.datacontract.org/2004/07/BSRBankWCF.Models")]
    [System.SerializableAttribute()]
    public partial class History : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal AmountField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime DateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FromField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Client.ServiceReference1.ObjectId IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TitleField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ToField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int UserLpField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Amount {
            get {
                return this.AmountField;
            }
            set {
                if ((this.AmountField.Equals(value) != true)) {
                    this.AmountField = value;
                    this.RaisePropertyChanged("Amount");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime Date {
            get {
                return this.DateField;
            }
            set {
                if ((this.DateField.Equals(value) != true)) {
                    this.DateField = value;
                    this.RaisePropertyChanged("Date");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string From {
            get {
                return this.FromField;
            }
            set {
                if ((object.ReferenceEquals(this.FromField, value) != true)) {
                    this.FromField = value;
                    this.RaisePropertyChanged("From");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Client.ServiceReference1.ObjectId Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Title {
            get {
                return this.TitleField;
            }
            set {
                if ((object.ReferenceEquals(this.TitleField, value) != true)) {
                    this.TitleField = value;
                    this.RaisePropertyChanged("Title");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string To {
            get {
                return this.ToField;
            }
            set {
                if ((object.ReferenceEquals(this.ToField, value) != true)) {
                    this.ToField = value;
                    this.RaisePropertyChanged("To");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Type {
            get {
                return this.TypeField;
            }
            set {
                if ((object.ReferenceEquals(this.TypeField, value) != true)) {
                    this.TypeField = value;
                    this.RaisePropertyChanged("Type");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int UserLp {
            get {
                return this.UserLpField;
            }
            set {
                if ((this.UserLpField.Equals(value) != true)) {
                    this.UserLpField = value;
                    this.RaisePropertyChanged("UserLp");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ObjectId", Namespace="http://schemas.datacontract.org/2004/07/MongoDB.Bson")]
    [System.SerializableAttribute()]
    public partial struct ObjectId : System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private int _aField;
        
        private int _bField;
        
        private int _cField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int _a {
            get {
                return this._aField;
            }
            set {
                if ((this._aField.Equals(value) != true)) {
                    this._aField = value;
                    this.RaisePropertyChanged("_a");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int _b {
            get {
                return this._bField;
            }
            set {
                if ((this._bField.Equals(value) != true)) {
                    this._bField = value;
                    this.RaisePropertyChanged("_b");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int _c {
            get {
                return this._cField;
            }
            set {
                if ((this._cField.Equals(value) != true)) {
                    this._cField = value;
                    this.RaisePropertyChanged("_c");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IService1")]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetUserAccounts", ReplyAction="http://tempuri.org/IService1/GetUserAccountsResponse")]
        System.Collections.Generic.List<Client.ServiceReference1.Account> GetUserAccounts(string credentials);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetUserAccounts", ReplyAction="http://tempuri.org/IService1/GetUserAccountsResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<Client.ServiceReference1.Account>> GetUserAccountsAsync(string credentials);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/AddUser", ReplyAction="http://tempuri.org/IService1/AddUserResponse")]
        Client.ServiceReference1.Message AddUser(string login, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/AddUser", ReplyAction="http://tempuri.org/IService1/AddUserResponse")]
        System.Threading.Tasks.Task<Client.ServiceReference1.Message> AddUserAsync(string login, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/ValidateUser", ReplyAction="http://tempuri.org/IService1/ValidateUserResponse")]
        Client.ServiceReference1.Message ValidateUser(string login, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/ValidateUser", ReplyAction="http://tempuri.org/IService1/ValidateUserResponse")]
        System.Threading.Tasks.Task<Client.ServiceReference1.Message> ValidateUserAsync(string login, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/WithdrawDepositMoney", ReplyAction="http://tempuri.org/IService1/WithdrawDepositMoneyResponse")]
        Client.ServiceReference1.Message WithdrawDepositMoney(Client.ServiceReference1.WithdrawDeposit withdrawDeposit);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/WithdrawDepositMoney", ReplyAction="http://tempuri.org/IService1/WithdrawDepositMoneyResponse")]
        System.Threading.Tasks.Task<Client.ServiceReference1.Message> WithdrawDepositMoneyAsync(Client.ServiceReference1.WithdrawDeposit withdrawDeposit);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/ExecuteExternalTransfer", ReplyAction="http://tempuri.org/IService1/ExecuteExternalTransferResponse")]
        Client.ServiceReference1.Message ExecuteExternalTransfer(Client.ServiceReference1.Transfer transfer, string accountTo, string credentials);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/ExecuteExternalTransfer", ReplyAction="http://tempuri.org/IService1/ExecuteExternalTransferResponse")]
        System.Threading.Tasks.Task<Client.ServiceReference1.Message> ExecuteExternalTransferAsync(Client.ServiceReference1.Transfer transfer, string accountTo, string credentials);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/ExecuteInternalTransfer", ReplyAction="http://tempuri.org/IService1/ExecuteInternalTransferResponse")]
        Client.ServiceReference1.Message ExecuteInternalTransfer(Client.ServiceReference1.Transfer transfer, string accountTo, string credentials);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/ExecuteInternalTransfer", ReplyAction="http://tempuri.org/IService1/ExecuteInternalTransferResponse")]
        System.Threading.Tasks.Task<Client.ServiceReference1.Message> ExecuteInternalTransferAsync(Client.ServiceReference1.Transfer transfer, string accountTo, string credentials);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetUsersHistory", ReplyAction="http://tempuri.org/IService1/GetUsersHistoryResponse")]
        System.Collections.Generic.List<Client.ServiceReference1.History> GetUsersHistory(string credentials);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetUsersHistory", ReplyAction="http://tempuri.org/IService1/GetUsersHistoryResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<Client.ServiceReference1.History>> GetUsersHistoryAsync(string credentials);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/CreateBankAccount", ReplyAction="http://tempuri.org/IService1/CreateBankAccountResponse")]
        Client.ServiceReference1.Message CreateBankAccount(string credentials);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/CreateBankAccount", ReplyAction="http://tempuri.org/IService1/CreateBankAccountResponse")]
        System.Threading.Tasks.Task<Client.ServiceReference1.Message> CreateBankAccountAsync(string credentials);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : Client.ServiceReference1.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<Client.ServiceReference1.IService1>, Client.ServiceReference1.IService1 {
        
        public Service1Client() {
        }
        
        public Service1Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Service1Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Collections.Generic.List<Client.ServiceReference1.Account> GetUserAccounts(string credentials) {
            return base.Channel.GetUserAccounts(credentials);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<Client.ServiceReference1.Account>> GetUserAccountsAsync(string credentials) {
            return base.Channel.GetUserAccountsAsync(credentials);
        }
        
        public Client.ServiceReference1.Message AddUser(string login, string password) {
            return base.Channel.AddUser(login, password);
        }
        
        public System.Threading.Tasks.Task<Client.ServiceReference1.Message> AddUserAsync(string login, string password) {
            return base.Channel.AddUserAsync(login, password);
        }
        
        public Client.ServiceReference1.Message ValidateUser(string login, string password) {
            return base.Channel.ValidateUser(login, password);
        }
        
        public System.Threading.Tasks.Task<Client.ServiceReference1.Message> ValidateUserAsync(string login, string password) {
            return base.Channel.ValidateUserAsync(login, password);
        }
        
        public Client.ServiceReference1.Message WithdrawDepositMoney(Client.ServiceReference1.WithdrawDeposit withdrawDeposit) {
            return base.Channel.WithdrawDepositMoney(withdrawDeposit);
        }
        
        public System.Threading.Tasks.Task<Client.ServiceReference1.Message> WithdrawDepositMoneyAsync(Client.ServiceReference1.WithdrawDeposit withdrawDeposit) {
            return base.Channel.WithdrawDepositMoneyAsync(withdrawDeposit);
        }
        
        public Client.ServiceReference1.Message ExecuteExternalTransfer(Client.ServiceReference1.Transfer transfer, string accountTo, string credentials) {
            return base.Channel.ExecuteExternalTransfer(transfer, accountTo, credentials);
        }
        
        public System.Threading.Tasks.Task<Client.ServiceReference1.Message> ExecuteExternalTransferAsync(Client.ServiceReference1.Transfer transfer, string accountTo, string credentials) {
            return base.Channel.ExecuteExternalTransferAsync(transfer, accountTo, credentials);
        }
        
        public Client.ServiceReference1.Message ExecuteInternalTransfer(Client.ServiceReference1.Transfer transfer, string accountTo, string credentials) {
            return base.Channel.ExecuteInternalTransfer(transfer, accountTo, credentials);
        }
        
        public System.Threading.Tasks.Task<Client.ServiceReference1.Message> ExecuteInternalTransferAsync(Client.ServiceReference1.Transfer transfer, string accountTo, string credentials) {
            return base.Channel.ExecuteInternalTransferAsync(transfer, accountTo, credentials);
        }
        
        public System.Collections.Generic.List<Client.ServiceReference1.History> GetUsersHistory(string credentials) {
            return base.Channel.GetUsersHistory(credentials);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<Client.ServiceReference1.History>> GetUsersHistoryAsync(string credentials) {
            return base.Channel.GetUsersHistoryAsync(credentials);
        }
        
        public Client.ServiceReference1.Message CreateBankAccount(string credentials) {
            return base.Channel.CreateBankAccount(credentials);
        }
        
        public System.Threading.Tasks.Task<Client.ServiceReference1.Message> CreateBankAccountAsync(string credentials) {
            return base.Channel.CreateBankAccountAsync(credentials);
        }
    }
}
