using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jobweb.Models
{
    public class ViewModel
    {
        public IEnumerable<Listing> listings { get; set; }
        public int BlogPerPage { get; set; }
        public int CurrentPage { get; set; }
        public List<Listing> Blogs { get; internal set; }

        public int PageCount()
        {
            return Convert.ToInt32(Math.Ceiling(listings.Count() / (double)BlogPerPage));
        }
        public IEnumerable<Listing> PaginatedBlogs()
        {
            int start = (CurrentPage - 1) * BlogPerPage;
            return listings.OrderBy(b => b.id).Skip(start).Take(BlogPerPage);
        }
    }
}