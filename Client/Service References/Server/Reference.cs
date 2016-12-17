﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Compete.NetCache.Server {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Server.INetCacheService")]
    internal interface INetCacheService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INetCacheService/GetValue", ReplyAction="http://tempuri.org/INetCacheService/GetValueResponse")]
        byte[] GetValue(string databaseName, string collectionName, string key);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INetCacheService/GetValue", ReplyAction="http://tempuri.org/INetCacheService/GetValueResponse")]
        System.Threading.Tasks.Task<byte[]> GetValueAsync(string databaseName, string collectionName, string key);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INetCacheService/SetValue", ReplyAction="http://tempuri.org/INetCacheService/SetValueResponse")]
        void SetValue(string databaseName, string collectionName, string key, byte[] data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INetCacheService/SetValue", ReplyAction="http://tempuri.org/INetCacheService/SetValueResponse")]
        System.Threading.Tasks.Task SetValueAsync(string databaseName, string collectionName, string key, byte[] data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INetCacheService/Remove", ReplyAction="http://tempuri.org/INetCacheService/RemoveResponse")]
        bool Remove(string databaseName, string collectionName, string key);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INetCacheService/Remove", ReplyAction="http://tempuri.org/INetCacheService/RemoveResponse")]
        System.Threading.Tasks.Task<bool> RemoveAsync(string databaseName, string collectionName, string key);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INetCacheService/CreateCollection", ReplyAction="http://tempuri.org/INetCacheService/CreateCollectionResponse")]
        bool CreateCollection(string databaseName, string collectionName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INetCacheService/CreateCollection", ReplyAction="http://tempuri.org/INetCacheService/CreateCollectionResponse")]
        System.Threading.Tasks.Task<bool> CreateCollectionAsync(string databaseName, string collectionName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INetCacheService/DropCollection", ReplyAction="http://tempuri.org/INetCacheService/DropCollectionResponse")]
        bool DropCollection(string databaseName, string collectionName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INetCacheService/DropCollection", ReplyAction="http://tempuri.org/INetCacheService/DropCollectionResponse")]
        System.Threading.Tasks.Task<bool> DropCollectionAsync(string databaseName, string collectionName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INetCacheService/CreateDatabase", ReplyAction="http://tempuri.org/INetCacheService/CreateDatabaseResponse")]
        bool CreateDatabase(string databaseName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INetCacheService/CreateDatabase", ReplyAction="http://tempuri.org/INetCacheService/CreateDatabaseResponse")]
        System.Threading.Tasks.Task<bool> CreateDatabaseAsync(string databaseName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INetCacheService/DropDatabase", ReplyAction="http://tempuri.org/INetCacheService/DropDatabaseResponse")]
        bool DropDatabase(string databaseName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INetCacheService/DropDatabase", ReplyAction="http://tempuri.org/INetCacheService/DropDatabaseResponse")]
        System.Threading.Tasks.Task<bool> DropDatabaseAsync(string databaseName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INetCacheService/Save", ReplyAction="http://tempuri.org/INetCacheService/SaveResponse")]
        void Save(string databaseName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INetCacheService/Save", ReplyAction="http://tempuri.org/INetCacheService/SaveResponse")]
        System.Threading.Tasks.Task SaveAsync(string databaseName);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    internal interface INetCacheServiceChannel : Compete.NetCache.Server.INetCacheService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    internal partial class NetCacheServiceClient : System.ServiceModel.ClientBase<Compete.NetCache.Server.INetCacheService>, Compete.NetCache.Server.INetCacheService {
        
        public NetCacheServiceClient() {
        }
        
        public NetCacheServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public NetCacheServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public NetCacheServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public NetCacheServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public byte[] GetValue(string databaseName, string collectionName, string key) {
            return base.Channel.GetValue(databaseName, collectionName, key);
        }
        
        public System.Threading.Tasks.Task<byte[]> GetValueAsync(string databaseName, string collectionName, string key) {
            return base.Channel.GetValueAsync(databaseName, collectionName, key);
        }
        
        public void SetValue(string databaseName, string collectionName, string key, byte[] data) {
            base.Channel.SetValue(databaseName, collectionName, key, data);
        }
        
        public System.Threading.Tasks.Task SetValueAsync(string databaseName, string collectionName, string key, byte[] data) {
            return base.Channel.SetValueAsync(databaseName, collectionName, key, data);
        }
        
        public bool Remove(string databaseName, string collectionName, string key) {
            return base.Channel.Remove(databaseName, collectionName, key);
        }
        
        public System.Threading.Tasks.Task<bool> RemoveAsync(string databaseName, string collectionName, string key) {
            return base.Channel.RemoveAsync(databaseName, collectionName, key);
        }
        
        public bool CreateCollection(string databaseName, string collectionName) {
            return base.Channel.CreateCollection(databaseName, collectionName);
        }
        
        public System.Threading.Tasks.Task<bool> CreateCollectionAsync(string databaseName, string collectionName) {
            return base.Channel.CreateCollectionAsync(databaseName, collectionName);
        }
        
        public bool DropCollection(string databaseName, string collectionName) {
            return base.Channel.DropCollection(databaseName, collectionName);
        }
        
        public System.Threading.Tasks.Task<bool> DropCollectionAsync(string databaseName, string collectionName) {
            return base.Channel.DropCollectionAsync(databaseName, collectionName);
        }
        
        public bool CreateDatabase(string databaseName) {
            return base.Channel.CreateDatabase(databaseName);
        }
        
        public System.Threading.Tasks.Task<bool> CreateDatabaseAsync(string databaseName) {
            return base.Channel.CreateDatabaseAsync(databaseName);
        }
        
        public bool DropDatabase(string databaseName) {
            return base.Channel.DropDatabase(databaseName);
        }
        
        public System.Threading.Tasks.Task<bool> DropDatabaseAsync(string databaseName) {
            return base.Channel.DropDatabaseAsync(databaseName);
        }
        
        public void Save(string databaseName) {
            base.Channel.Save(databaseName);
        }
        
        public System.Threading.Tasks.Task SaveAsync(string databaseName) {
            return base.Channel.SaveAsync(databaseName);
        }
    }
}
