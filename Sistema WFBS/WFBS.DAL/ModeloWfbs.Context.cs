﻿//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WFBS.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WFBSEntities : DbContext
    {
        public WFBSEntities()
            : base("name=WFBSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<AREA> AREA { get; set; }
        public DbSet<AUDITORIA> AUDITORIA { get; set; }
        public DbSet<COMPETENCIA> COMPETENCIA { get; set; }
        public DbSet<DETALLE_EVALUACION> DETALLE_EVALUACION { get; set; }
        public DbSet<EVALUACION> EVALUACION { get; set; }
        public DbSet<HABILIDAD> HABILIDAD { get; set; }
        public DbSet<NIVEL> NIVEL { get; set; }
        public DbSet<OBSERVACION_COMPETENCIA> OBSERVACION_COMPETENCIA { get; set; }
        public DbSet<PERFIL> PERFIL { get; set; }
        public DbSet<PERFIL_DE_CARGO> PERFIL_DE_CARGO { get; set; }
        public DbSet<PERIODO_EVALUACION> PERIODO_EVALUACION { get; set; }
        public DbSet<R_C_N> R_C_N { get; set; }
        public DbSet<TIPO_EVALUACION> TIPO_EVALUACION { get; set; }
        public DbSet<USUARIO> USUARIO { get; set; }
    }
}
