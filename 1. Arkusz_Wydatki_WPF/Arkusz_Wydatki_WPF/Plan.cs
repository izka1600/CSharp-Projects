//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Arkusz_Wydatki_WPF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Plan
    {
        public int Plan_ID { get; set; }
        public Nullable<System.DateTime> Miesiąc { get; set; }
        public Nullable<decimal> ZalozonaKwota { get; set; }
        public Nullable<decimal> FaktycznaKwota { get; set; }
        public int IdUzytkownika { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<int> Warning { get; set; }
    
        public virtual Uzytkownik Uzytkownik { get; set; }
    }
}