//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Data
{
    using System;
    using System.Collections.Generic;

    using IQMProjectEvolutionManagerWS.Data.Interfaces.OnTimeModels;

    public partial class ReleaseType : IReleaseType
    {
        public ReleaseType()
        {
            this.Releases = new HashSet<Release>();
        }
    
        public int ReleaseTypeId { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public byte[] Icon { get; set; }
    
        public virtual ICollection<Release> Releases { get; set; }
    }
}
