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
    
    public partial class timetable
    {
        public int id { get; set; }
        public int userID { get; set; }
        public System.DateTime date { get; set; }
        public int workhours { get; set; }
    
        public virtual users users { get; set; }
    }
}