﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34003
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Raagam.MVC.TextileManagement.UI.InventoryService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="InventoryService.IInventory")]
    public interface IInventory {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IInventory/PurReqPopulateDropDown", ReplyAction="http://tempuri.org/IInventory/PurReqPopulateDropDownResponse")]
        Raagam.TextileManagement.Model.PurchaseRequisitionHeaderModel PurReqPopulateDropDown();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IInventory/SavePurchaseRequisition", ReplyAction="http://tempuri.org/IInventory/SavePurchaseRequisitionResponse")]
        Raagam.TextileManagement.Model.PurchaseRequisitionHeaderModel SavePurchaseRequisition(Raagam.TextileManagement.Model.PurchaseRequisitionHeaderModel purchaseRequisitionHeaderModel);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IInventory/SelectPurchaseRequisition", ReplyAction="http://tempuri.org/IInventory/SelectPurchaseRequisitionResponse")]
        Raagam.TextileManagement.Model.PurchaseRequisitionHeaderModel SelectPurchaseRequisition(long purchaseRequisitionNumber);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IInventoryChannel : Raagam.MVC.TextileManagement.UI.InventoryService.IInventory, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class InventoryClient : System.ServiceModel.ClientBase<Raagam.MVC.TextileManagement.UI.InventoryService.IInventory>, Raagam.MVC.TextileManagement.UI.InventoryService.IInventory {
        
        public InventoryClient() {
        }
        
        public InventoryClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public InventoryClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public InventoryClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public InventoryClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Raagam.TextileManagement.Model.PurchaseRequisitionHeaderModel PurReqPopulateDropDown() {
            return base.Channel.PurReqPopulateDropDown();
        }
        
        public Raagam.TextileManagement.Model.PurchaseRequisitionHeaderModel SavePurchaseRequisition(Raagam.TextileManagement.Model.PurchaseRequisitionHeaderModel purchaseRequisitionHeaderModel) {
            return base.Channel.SavePurchaseRequisition(purchaseRequisitionHeaderModel);
        }
        
        public Raagam.TextileManagement.Model.PurchaseRequisitionHeaderModel SelectPurchaseRequisition(long purchaseRequisitionNumber) {
            return base.Channel.SelectPurchaseRequisition(purchaseRequisitionNumber);
        }
    }
}