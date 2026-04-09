using BlazorApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Shared.Interfaces
{
    public interface IChildService
    {
        Task<List<Child>> GetChildrenByParentIdAsync(string parentId);
        Task<bool> AddChildAsync(Child child);
    }
}
