//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SuccessPlus.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Fine
    {
        public int IdFine { get; set; }
        public int TypeFine { get; set; }
        public int IdMark { get; set; }
        public int IdStudent { get; set; }
    
        public virtual Marks Marks { get; set; }
        public virtual Student Student { get; set; }
        public virtual TypeFine TypeFine1 { get; set; }
    }
}
