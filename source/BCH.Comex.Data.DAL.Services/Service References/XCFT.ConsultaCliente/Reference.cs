﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BCH.Comex.Data.DAL.Services.XCFT.ConsultaCliente {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://osb.bancochile.cl/ConsultaTipoCliente/", ConfigurationName="XCFT.ConsultaCliente.ConsultaTipoCliente")]
    public interface ConsultaTipoCliente {
        
        // CODEGEN: Generating message contract since message ConsultaTipoClienteRequest has headers
        [System.ServiceModel.OperationContractAttribute(Action="http://osb.bancochile.cl//ConsultaTipoCliente/ConsultaTipoCliente", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        BCH.Comex.Data.DAL.Services.XCFT.ConsultaCliente.ConsultaTipoClienteResponse ConsultaTipoCliente(BCH.Comex.Data.DAL.Services.XCFT.ConsultaCliente.ConsultaTipoClienteRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://osb.bancochile.cl//ConsultaTipoCliente/ConsultaTipoCliente", ReplyAction="*")]
        System.Threading.Tasks.Task<BCH.Comex.Data.DAL.Services.XCFT.ConsultaCliente.ConsultaTipoClienteResponse> ConsultaTipoClienteAsync(BCH.Comex.Data.DAL.Services.XCFT.ConsultaCliente.ConsultaTipoClienteRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://osb.bancochile.cl/common/HeaderRequest")]
    public partial class datosHeaderRequest : object, System.ComponentModel.INotifyPropertyChanged {
        
        private datosConsumidor consumidorField;
        
        private datosTransaccion transaccionField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public datosConsumidor consumidor {
            get {
                return this.consumidorField;
            }
            set {
                this.consumidorField = value;
                this.RaisePropertyChanged("consumidor");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public datosTransaccion transaccion {
            get {
                return this.transaccionField;
            }
            set {
                this.transaccionField = value;
                this.RaisePropertyChanged("transaccion");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
                this.RaisePropertyChanged("AnyAttr");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://osb.bancochile.cl/common/HeaderRequest")]
    public partial class datosConsumidor : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string idAppField;
        
        private string usuarioField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string idApp {
            get {
                return this.idAppField;
            }
            set {
                this.idAppField = value;
                this.RaisePropertyChanged("idApp");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string usuario {
            get {
                return this.usuarioField;
            }
            set {
                this.usuarioField = value;
                this.RaisePropertyChanged("usuario");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="datosTransaccion", Namespace="http://osb.bancochile.cl/common/HeaderResponse")]
    public partial class datosTransaccion1 : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string internalCodeField;
        
        private string idTransaccionNegocioField;
        
        private System.Nullable<System.DateTime> fechaHoraInicioTrxField;
        
        private System.Nullable<System.DateTime> fechaHoraFinTrxField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string internalCode {
            get {
                return this.internalCodeField;
            }
            set {
                this.internalCodeField = value;
                this.RaisePropertyChanged("internalCode");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string idTransaccionNegocio {
            get {
                return this.idTransaccionNegocioField;
            }
            set {
                this.idTransaccionNegocioField = value;
                this.RaisePropertyChanged("idTransaccionNegocio");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=2)]
        public System.Nullable<System.DateTime> fechaHoraInicioTrx {
            get {
                return this.fechaHoraInicioTrxField;
            }
            set {
                this.fechaHoraInicioTrxField = value;
                this.RaisePropertyChanged("fechaHoraInicioTrx");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=3)]
        public System.Nullable<System.DateTime> fechaHoraFinTrx {
            get {
                return this.fechaHoraFinTrxField;
            }
            set {
                this.fechaHoraFinTrxField = value;
                this.RaisePropertyChanged("fechaHoraFinTrx");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://osb.bancochile.cl/common/HeaderResponse")]
    public partial class datosHeaderResponse : object, System.ComponentModel.INotifyPropertyChanged {
        
        private datosTransaccion1 transaccionField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public datosTransaccion1 transaccion {
            get {
                return this.transaccionField;
            }
            set {
                this.transaccionField = value;
                this.RaisePropertyChanged("transaccion");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://osb.bancochile.cl/common/HeaderRequest")]
    public partial class datosTransaccion : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string internalCodeField;
        
        private string idTransaccionNegocioField;
        
        private System.DateTime fechaHoraField;
        
        private string canalField;
        
        private string sucursalField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string internalCode {
            get {
                return this.internalCodeField;
            }
            set {
                this.internalCodeField = value;
                this.RaisePropertyChanged("internalCode");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string idTransaccionNegocio {
            get {
                return this.idTransaccionNegocioField;
            }
            set {
                this.idTransaccionNegocioField = value;
                this.RaisePropertyChanged("idTransaccionNegocio");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public System.DateTime fechaHora {
            get {
                return this.fechaHoraField;
            }
            set {
                this.fechaHoraField = value;
                this.RaisePropertyChanged("fechaHora");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string canal {
            get {
                return this.canalField;
            }
            set {
                this.canalField = value;
                this.RaisePropertyChanged("canal");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string sucursal {
            get {
                return this.sucursalField;
            }
            set {
                this.sucursalField = value;
                this.RaisePropertyChanged("sucursal");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://osb.bancochile.cl/ESB/ConsultaTipoCliente/OpConsultaTipoClienteResponse")]
    public partial class respConsultaTipoCliente : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string tipoClienteField;
        
        private detallePersona detallePersonaField;
        
        private detalleEmpresa detalleEmpresaField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string tipoCliente {
            get {
                return this.tipoClienteField;
            }
            set {
                this.tipoClienteField = value;
                this.RaisePropertyChanged("tipoCliente");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public detallePersona detallePersona {
            get {
                return this.detallePersonaField;
            }
            set {
                this.detallePersonaField = value;
                this.RaisePropertyChanged("detallePersona");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public detalleEmpresa detalleEmpresa {
            get {
                return this.detalleEmpresaField;
            }
            set {
                this.detalleEmpresaField = value;
                this.RaisePropertyChanged("detalleEmpresa");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://osb.bancochile.cl/ESB/ConsultaTipoCliente/OpConsultaTipoClienteResponse")]
    public partial class detallePersona : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string apellidoMaternoField;
        
        private string apellidoPaternoField;
        
        private string nombresField;
        
        private string razonSocialField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string apellidoMaterno {
            get {
                return this.apellidoMaternoField;
            }
            set {
                this.apellidoMaternoField = value;
                this.RaisePropertyChanged("apellidoMaterno");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string apellidoPaterno {
            get {
                return this.apellidoPaternoField;
            }
            set {
                this.apellidoPaternoField = value;
                this.RaisePropertyChanged("apellidoPaterno");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string nombres {
            get {
                return this.nombresField;
            }
            set {
                this.nombresField = value;
                this.RaisePropertyChanged("nombres");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string razonSocial {
            get {
                return this.razonSocialField;
            }
            set {
                this.razonSocialField = value;
                this.RaisePropertyChanged("razonSocial");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://osb.bancochile.cl/ESB/ConsultaTipoCliente/OpConsultaTipoClienteResponse")]
    public partial class detalleEmpresa : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string razonSocialField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string razonSocial {
            get {
                return this.razonSocialField;
            }
            set {
                this.razonSocialField = value;
                this.RaisePropertyChanged("razonSocial");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://osb.bancochile.cl/ESB/ConsultaTipoCliente/OpConsultaTipoClienteRequest")]
    public partial class reqConsultaTipoCliente : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string rutClienteField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string rutCliente {
            get {
                return this.rutClienteField;
            }
            set {
                this.rutClienteField = value;
                this.RaisePropertyChanged("rutCliente");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="ConsultaTipoCliente", WrapperNamespace="http://osb.bancochile.cl/ConsultaTipoCliente/", IsWrapped=true)]
    public partial class ConsultaTipoClienteRequest {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://osb.bancochile.cl/ConsultaTipoCliente/")]
        public BCH.Comex.Data.DAL.Services.XCFT.ConsultaCliente.datosHeaderRequest headerRequest;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://osb.bancochile.cl/ConsultaTipoCliente/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public BCH.Comex.Data.DAL.Services.XCFT.ConsultaCliente.reqConsultaTipoCliente reqConsultaTipoCliente;
        
        public ConsultaTipoClienteRequest() {
        }
        
        public ConsultaTipoClienteRequest(BCH.Comex.Data.DAL.Services.XCFT.ConsultaCliente.datosHeaderRequest headerRequest, BCH.Comex.Data.DAL.Services.XCFT.ConsultaCliente.reqConsultaTipoCliente reqConsultaTipoCliente) {
            this.headerRequest = headerRequest;
            this.reqConsultaTipoCliente = reqConsultaTipoCliente;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="ConsultaTipoClienteResponse", WrapperNamespace="http://osb.bancochile.cl/ConsultaTipoCliente/", IsWrapped=true)]
    public partial class ConsultaTipoClienteResponse {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://osb.bancochile.cl/ConsultaTipoCliente/")]
        public BCH.Comex.Data.DAL.Services.XCFT.ConsultaCliente.datosHeaderResponse headerResponse;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://osb.bancochile.cl/ConsultaTipoCliente/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public BCH.Comex.Data.DAL.Services.XCFT.ConsultaCliente.respConsultaTipoCliente respConsultaTipoCliente;
        
        public ConsultaTipoClienteResponse() {
        }
        
        public ConsultaTipoClienteResponse(BCH.Comex.Data.DAL.Services.XCFT.ConsultaCliente.datosHeaderResponse headerResponse, BCH.Comex.Data.DAL.Services.XCFT.ConsultaCliente.respConsultaTipoCliente respConsultaTipoCliente) {
            this.headerResponse = headerResponse;
            this.respConsultaTipoCliente = respConsultaTipoCliente;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ConsultaTipoClienteChannel : BCH.Comex.Data.DAL.Services.XCFT.ConsultaCliente.ConsultaTipoCliente, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ConsultaTipoClienteClient : System.ServiceModel.ClientBase<BCH.Comex.Data.DAL.Services.XCFT.ConsultaCliente.ConsultaTipoCliente>, BCH.Comex.Data.DAL.Services.XCFT.ConsultaCliente.ConsultaTipoCliente {
        
        public ConsultaTipoClienteClient() {
        }
        
        public ConsultaTipoClienteClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ConsultaTipoClienteClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ConsultaTipoClienteClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ConsultaTipoClienteClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        BCH.Comex.Data.DAL.Services.XCFT.ConsultaCliente.ConsultaTipoClienteResponse BCH.Comex.Data.DAL.Services.XCFT.ConsultaCliente.ConsultaTipoCliente.ConsultaTipoCliente(BCH.Comex.Data.DAL.Services.XCFT.ConsultaCliente.ConsultaTipoClienteRequest request) {
            return base.Channel.ConsultaTipoCliente(request);
        }
        
        public BCH.Comex.Data.DAL.Services.XCFT.ConsultaCliente.datosHeaderResponse ConsultaTipoCliente(BCH.Comex.Data.DAL.Services.XCFT.ConsultaCliente.datosHeaderRequest headerRequest, BCH.Comex.Data.DAL.Services.XCFT.ConsultaCliente.reqConsultaTipoCliente reqConsultaTipoCliente, out BCH.Comex.Data.DAL.Services.XCFT.ConsultaCliente.respConsultaTipoCliente respConsultaTipoCliente) {
            BCH.Comex.Data.DAL.Services.XCFT.ConsultaCliente.ConsultaTipoClienteRequest inValue = new BCH.Comex.Data.DAL.Services.XCFT.ConsultaCliente.ConsultaTipoClienteRequest();
            inValue.headerRequest = headerRequest;
            inValue.reqConsultaTipoCliente = reqConsultaTipoCliente;
            BCH.Comex.Data.DAL.Services.XCFT.ConsultaCliente.ConsultaTipoClienteResponse retVal = ((BCH.Comex.Data.DAL.Services.XCFT.ConsultaCliente.ConsultaTipoCliente)(this)).ConsultaTipoCliente(inValue);
            respConsultaTipoCliente = retVal.respConsultaTipoCliente;
            return retVal.headerResponse;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<BCH.Comex.Data.DAL.Services.XCFT.ConsultaCliente.ConsultaTipoClienteResponse> BCH.Comex.Data.DAL.Services.XCFT.ConsultaCliente.ConsultaTipoCliente.ConsultaTipoClienteAsync(BCH.Comex.Data.DAL.Services.XCFT.ConsultaCliente.ConsultaTipoClienteRequest request) {
            return base.Channel.ConsultaTipoClienteAsync(request);
        }
        
        public System.Threading.Tasks.Task<BCH.Comex.Data.DAL.Services.XCFT.ConsultaCliente.ConsultaTipoClienteResponse> ConsultaTipoClienteAsync(BCH.Comex.Data.DAL.Services.XCFT.ConsultaCliente.datosHeaderRequest headerRequest, BCH.Comex.Data.DAL.Services.XCFT.ConsultaCliente.reqConsultaTipoCliente reqConsultaTipoCliente) {
            BCH.Comex.Data.DAL.Services.XCFT.ConsultaCliente.ConsultaTipoClienteRequest inValue = new BCH.Comex.Data.DAL.Services.XCFT.ConsultaCliente.ConsultaTipoClienteRequest();
            inValue.headerRequest = headerRequest;
            inValue.reqConsultaTipoCliente = reqConsultaTipoCliente;
            return ((BCH.Comex.Data.DAL.Services.XCFT.ConsultaCliente.ConsultaTipoCliente)(this)).ConsultaTipoClienteAsync(inValue);
        }
    }
}
