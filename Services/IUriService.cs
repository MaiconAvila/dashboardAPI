using DashboardAPI.Wrappers;
using System;

namespace DashboardAPI.Services
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationFilter filter, string route);
    }
}
