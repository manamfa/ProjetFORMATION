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
    
    public partial class tableachat
    {
        public int ID { get; set; }
        public Nullable<int> clientID { get; set; }
        public Nullable<int> produitID { get; set; }
        public Nullable<int> Total { get; set; }
        public Nullable<int> Quantite { get; set; }
    
        public virtual client client { get; set; }
        public virtual produit produit { get; set; }
    }
}
