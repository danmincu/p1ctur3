//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SqlProvider
{
    using System;
    using System.Collections.Generic;
    
    public partial class Preview
    {
        public System.Guid Id { get; set; }
        public byte[] Level0 { get; set; }
        public byte[] Level1 { get; set; }
        public byte[] Level2 { get; set; }
        public byte[] Level3 { get; set; }
        public string PreviewType { get; set; }
    
        public virtual Media Medium { get; set; }
    }
}