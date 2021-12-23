using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDLIB.Paging
{
    public interface IPaging
    {
        #region Properties

        bool IsFirstPages { get; set; }

        bool IsLastPages { get; set; }

        bool IsNextPages { get; set; }

        bool IsPreviousPages { get; set; }

        List<int> PagesNumberList { get; set; }

        int PagesNumberView { get; set; }

        int PageCount { get; set; }

        int PageSize { get; set; }

        long TotalItem { get; set; }

        int NextPages { get; set; }

        int PreviousPages { get; set; }

        int CurrentPages { get; set; }

        int NumberRowPerPages { get; set; }

        int StartItem { get; set; }

        int EndItem { get; set; }

        string UrlParas { get; set; }

        string UrlFomat { get; set; }

        string PageVarName { get; set; }

        string AddParas { get; set; }
        #endregion
    }
}
