﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Domowe_Wydatki
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Arkusz_WydatkiEntities : DbContext
    {
        public Arkusz_WydatkiEntities()
            : base("name=Arkusz_WydatkiEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<eAutorzy> Autorzy { get; set; }
        public virtual DbSet<eKategorie> Kategorie { get; set; }
        public virtual DbSet<ePodkategorie> Podkategorie { get; set; }
        public virtual DbSet<eZestawienie> Zestawienie { get; set; }
    }
}
