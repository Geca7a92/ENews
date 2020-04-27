using System;
using System.Collections.Generic;
using System.Text;

namespace ENews.Web.ViewModels.Shared.Components.LatestHeadlines
{
    public class LatestHeadlinesViewModel
    {
        public IEnumerable<LatestHeadlineViewModel> Articles { get; set; }
    }
}
