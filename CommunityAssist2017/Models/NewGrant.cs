using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommunityAssist2017.Models
{
    public class NewGrant
    {
        public int GrantTypeKey { get; set; }
        public decimal GrantApplicationRequestAmount { get; set; }
        public string GrantApplicationReason { get; set; }
    }
}