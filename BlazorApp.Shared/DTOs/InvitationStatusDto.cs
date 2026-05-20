using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Shared.DTOs
{
    public class InvitationStatusDto
    {
        public string ParentEmail { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsAccepted { get; set; }
    }
}
