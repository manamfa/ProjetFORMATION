﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CRDLPROJET.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class facturationclientBOUEntities3 : DbContext
    {
        public facturationclientBOUEntities3()
            : base("name=facturationclientBOUEntities3")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<categorie> categories { get; set; }
        public virtual DbSet<client> clients { get; set; }
        public virtual DbSet<facturation> facturations { get; set; }
        public virtual DbSet<fournisseur> fournisseurs { get; set; }
        public virtual DbSet<historiquefacturation> historiquefacturations { get; set; }
        public virtual DbSet<historiqueprix> historiqueprixes { get; set; }
        public virtual DbSet<Payement> Payements { get; set; }
        public virtual DbSet<produit> produits { get; set; }
        public virtual DbSet<profil> profils { get; set; }
        public virtual DbSet<tableachat> tableachats { get; set; }
        public virtual DbSet<user> users { get; set; }
    }
}
