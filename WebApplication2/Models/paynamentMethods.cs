//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class paynamentMethods
    {
        public int userID { get; set; }
        public Nullable<int> BIK { get; set; }
        public Nullable<int> INN { get; set; }
        public Nullable<int> korr { get; set; }
        public Nullable<int> AccNum { get; set; }
    
        public virtual users users { get; set; }
    }
}
