﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BCH.Comex.Data.DAL.Portal
{
    using System;
    using System.Linq;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using BCH.Comex.Core.Entities.Portal;
    using System.Data.Entity.Core.Objects;
    
    public partial class portalEntities : DbContext
    {
        public portalEntities() : base("name=portalEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Aplicacion> Aplicacions { get; set; }
        public virtual DbSet<GrupoAplicacion> GrupoAplicacions { get; set; }
        public virtual DbSet<DatosUsuario> DatosUsuarios { get; set; }
        public virtual DbSet<CodigosSucursal> tbl_datos_usuario_codigos_sucursal { get; set; }
    
        public virtual ObjectResult<string> proc_sel_TBLSceTabcomex_MS(string parametro)
        {
            var parametroParameter = parametro != null ?
                new ObjectParameter("parametro", parametro) :
                new ObjectParameter("parametro", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("proc_sel_TBLSceTabcomex_MS", parametroParameter);
        }
    }
}
