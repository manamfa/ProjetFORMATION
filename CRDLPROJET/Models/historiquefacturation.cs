//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class historiquefacturation
    {
        public int historiquefacturationID { get; set; }
        public Nullable<int> produitID { get; set; }
        public Nullable<int> clientID { get; set; }
        public Nullable<int> facturationID { get; set; }
        public Nullable<int> prixunitaire { get; set; }
        public Nullable<int> Quantite { get; set; }
        public Nullable<int> Total { get; set; }
        public Nullable<System.DateTime> datehistofacturation { get; set; }
    
        public virtual client client { get; set; }
        public virtual client client1 { get; set; }
        public virtual client client2 { get; set; }
        public virtual client client3 { get; set; }
        public virtual facturation facturation { get; set; }
        public virtual facturation facturation1 { get; set; }
        public virtual facturation facturation2 { get; set; }
        public virtual facturation facturation3 { get; set; }
        public virtual produit produit { get; set; }
        public virtual produit produit1 { get; set; }
        public virtual produit produit2 { get; set; }
        public virtual produit produit3 { get; set; }
    }
}