// <copyright file="TPaging.cs" company="HDSoft">
// Copyright (c) 2011 HDSoft. All Right Reserved.
// </copyright>
// <summary>Class define Object for paging list item on index page in backend. These are model item which load data from EntityObject.</summary>
// <author>Nguyen Quang Hung</author>
// <email>nqh1810@gmail.com</email>
// <date>2013-12-24</date>
// <history>
//      date        name        comments
//      ----------  --------    ---------------------------------------------------------------------------------------
//      2013.12.24  nqhung      Create
// </history>

using System;
using System.Collections.Generic;
////using System.Data.Entity;
//using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
//using System.Web;

namespace LIB.Paging
{
    public enum OrderMode
    {
        Ascending,
        Desending
    }

    public class TPaging<R> : LIB.Paging.IPaging
    where R : class
    {
        #region Construction

        public TPaging()
        {
            UrlParas = string.Empty;
        }

        public TPaging(string urlParas)
        {
            this.UrlParas = urlParas;
        }

        public TPaging(string urlParas, string pageVarName, string addParas)
        {
            this.UrlParas = urlParas + addParas;
            this.PageVarName = pageVarName;
            this.AddParas = addParas;
        }

        public TPaging(string urlParas, int type)
        {
            //type == 1 => using UrlParas
            //type != 1 => using UrlFomat
            if (type == 1)
            {
                this.UrlParas = urlParas;
            }
            else
            {
                this.UrlFomat = urlParas;
            }
        }

        public TPaging(string urlParas, string urlFomat)
        {
            this.UrlParas = urlParas;
            this.UrlFomat = urlFomat;
        }

        public TPaging(IEnumerable<R> data, int page, int rowPerPage, int pagePerView)
        {
            CalculatePaging(data, page, rowPerPage, pagePerView);
        }
        public TPaging(IQueryable<R> data, int page, int rowPerPage, int pagePerView)
        {
            CalculatePaging(data, page, rowPerPage, pagePerView);
        }

        #endregion

        #region Public Properties
        public R SearchItems { get; set; }

        public List<R> CurrentPageItems { get; set; }

        public bool IsFirstPages { get; set; }

        public bool IsLastPages { get; set; }

        public bool IsNextPages { get; set; }

        public bool IsPreviousPages { get; set; }

        public List<int> PagesNumberList { get; set; }

        public int PagesNumberView { get; set; }

        public int PageCount { get; set; }

        public int PageSize { get; set; }

        public long TotalItem { get; set; }

        public int NextPages { get; set; }

        public int PreviousPages { get; set; }

        public int CurrentPages { get; set; }

        public int NumberRowPerPages { get; set; }

        public int StartItem { get; set; }

        public int EndItem { get; set; }

        public string PageVarName { get; set; }

        public string UrlParas { get; set; }

        public string UrlFomat { get; set; }

        public string AddParas { get; set; }
        #endregion

        #region Calculate Paging
        //public void CalculatePaging<TKey>(
        //    DbSet<R> setOfItems,
        //    Expression<Func<R, bool>> whereExpress,
        //    Expression<Func<R, TKey>> orderSelecter,
        //    int currentPage,
        //    int rowPerPage,
        //    int numPageView)
        //{
        //    int totalCount;
        //    if (whereExpress != null)
        //    {
        //        totalCount = setOfItems.Where(whereExpress).Count();
        //    }
        //    else
        //    {
        //        totalCount = setOfItems.Count();
        //    }

        //    BuildPages(totalCount, currentPage, rowPerPage, numPageView);

        //    if (totalCount > 0)
        //    {
        //        if (whereExpress != null)
        //        {
        //            if (orderSelecter != null)
        //            {
        //                this.CurrentPageItems = setOfItems.AsNoTracking().Where(whereExpress).
        //                    OrderBy(orderSelecter).
        //                    Skip((this.CurrentPages - 1) * rowPerPage).
        //                    Take(rowPerPage).ToList();
        //            }
        //            else
        //            {
        //                this.CurrentPageItems = setOfItems.AsNoTracking().Where(whereExpress).
        //                    Skip((this.CurrentPages - 1) * rowPerPage).
        //                    Take(rowPerPage).ToList();
        //            }
        //        }
        //        else
        //        {
        //            if (orderSelecter != null)
        //            {
        //                this.CurrentPageItems = setOfItems.AsNoTracking().OrderBy(orderSelecter).
        //                Skip((this.CurrentPages - 1) * rowPerPage).
        //                Take(rowPerPage).ToList();
        //            }
        //            else
        //            {
        //                this.CurrentPageItems = setOfItems.AsNoTracking().Skip((this.CurrentPages - 1) * rowPerPage).
        //                    Take(rowPerPage).ToList();
        //            }
        //        }
        //    }
        //    else
        //    {
        //        this.CurrentPageItems = new List<R>();
        //    }
        //}

        //public void CalculatePaging<TKey>(
        //    DbSet<R> setOfItems,
        //    Expression<Func<R, bool>> whereExpress,
        //    Expression<Func<R, TKey>> orderSelecter,
        //    OrderMode orderMode,
        //    int currentPage,
        //    int rowPerPage,
        //    int numPageView)
        //{
        //    int totalCount;
        //    if (whereExpress != null)
        //    {
        //        totalCount = setOfItems.Where(whereExpress).Count();
        //    }
        //    else
        //    {
        //        totalCount = setOfItems.Count();
        //    }

        //    BuildPages(totalCount, currentPage, rowPerPage, numPageView);

        //    if (totalCount > 0)
        //    {
        //        if (whereExpress != null)
        //        {
        //            if (orderSelecter != null)
        //            {
        //                if (orderMode == OrderMode.Ascending)
        //                {
        //                    this.CurrentPageItems = setOfItems.AsNoTracking().Where(whereExpress).
        //                        OrderBy(orderSelecter).
        //                        Skip((this.CurrentPages - 1) * rowPerPage).
        //                        Take(rowPerPage).ToList();
        //                }
        //                else
        //                {
        //                    this.CurrentPageItems = setOfItems.AsNoTracking().Where(whereExpress).
        //                        OrderByDescending(orderSelecter).
        //                        Skip((this.CurrentPages - 1) * rowPerPage).
        //                        Take(rowPerPage).ToList();
        //                }
        //            }
        //            else
        //            {
        //                this.CurrentPageItems = setOfItems.Where(whereExpress).
        //                    Skip((this.CurrentPages - 1) * rowPerPage).
        //                    Take(rowPerPage).ToList();
        //            }
        //        }
        //        else
        //        {
        //            if (orderSelecter != null)
        //            {
        //                if (orderMode == OrderMode.Ascending)
        //                {
        //                    this.CurrentPageItems = setOfItems.AsNoTracking().OrderBy(orderSelecter).
        //                    Skip((this.CurrentPages - 1) * rowPerPage).
        //                    Take(rowPerPage).ToList();
        //                }
        //                else
        //                {
        //                    this.CurrentPageItems = setOfItems.AsNoTracking().OrderByDescending(orderSelecter).
        //                    Skip((this.CurrentPages - 1) * rowPerPage).
        //                    Take(rowPerPage).ToList();
        //                }
        //            }
        //            else
        //            {
        //                this.CurrentPageItems = setOfItems.AsNoTracking().Skip((this.CurrentPages - 1) * rowPerPage).
        //                    Take(rowPerPage).ToList();
        //            }
        //        }
        //    }
        //    else
        //    {
        //        this.CurrentPageItems = new List<R>();
        //    }
        //}

        //public void CalculatePaging(
        //    DbSet<R> setOfItems,
        //    string sqlStr,
        //    int currentPage,
        //    int rowPerPage,
        //    int numPageView)
        //{
        //    int totalCount;
        //    if (!string.IsNullOrEmpty(sqlStr))
        //    {
        //        totalCount = setOfItems.SqlQuery(sqlStr).Count();
        //    }
        //    else
        //    {
        //        totalCount = setOfItems.Count();
        //    }

        //    BuildPages(totalCount, currentPage, rowPerPage, numPageView);

        //    if (totalCount > 0)
        //    {
        //        if (!string.IsNullOrEmpty(sqlStr))
        //        {
        //            try
        //            {
        //                string sqlStrOffset = $"{sqlStr} OFFSET {(this.CurrentPages - 1) * rowPerPage} ROWS FETCH NEXT {rowPerPage} ROWS ONLY";
        //                this.CurrentPageItems = setOfItems.SqlQuery(sqlStr).ToList();
        //            }
        //            catch
        //            {
        //                this.CurrentPageItems = setOfItems.SqlQuery(sqlStr).
        //                Skip((this.CurrentPages - 1) * rowPerPage).
        //                Take(rowPerPage).ToList();
        //            }
        //        }
        //        else
        //        {
        //            this.CurrentPageItems = setOfItems.Skip((this.CurrentPages - 1) * rowPerPage).
        //            Take(rowPerPage).ToList();
        //        }
        //    }
        //    else
        //    {
        //        this.CurrentPageItems = new List<R>();
        //    }
        //}

        //public void CalculatePaging(
        //    DbContext db,
        //    string sqlStr,
        //    int currentPage,
        //    int rowPerPage,
        //    int numPageView)
        //{
        //    int totalCount;
        //    if (!string.IsNullOrEmpty(sqlStr))
        //    {
        //        totalCount = db.Database.SqlQuery<R>(sqlStr).Count();
        //    }
        //    else
        //    {
        //        throw new Exception("Parameter 'sqlStr' must have value and not empty");
        //    }

        //    BuildPages(totalCount, currentPage, rowPerPage, numPageView);

        //    if (totalCount > 0)
        //    {
        //        try
        //        {
        //            string sqlStrOffset = $"{sqlStr} OFFSET {(this.CurrentPages - 1) * rowPerPage} ROWS FETCH NEXT {rowPerPage} ROWS ONLY";
        //            this.CurrentPageItems = db.Database.SqlQuery<R>(sqlStrOffset).ToList();
        //        }
        //        catch
        //        {
        //            this.CurrentPageItems = db.Database.SqlQuery<R>(sqlStr).
        //            Skip((this.CurrentPages - 1) * rowPerPage).
        //            Take(rowPerPage).ToList();
        //        }
        //    }
        //    else
        //    {
        //        this.CurrentPageItems = new List<R>();
        //    }
        //}

        //public void CalculatePaging(
        //    DbContext db,
        //    string sqlStr,
        //    int currentPage,
        //    int rowPerPage,
        //    int numPageView,
        //    params SqlParameter[] sqlParameters)
        //{
        //    int totalCount;
        //    IEnumerable<SqlParameter> newOne()
        //    {
        //        foreach (var item in sqlParameters)
        //        {
        //            yield return new SqlParameter(item.ParameterName, item.Value);
        //        }
        //    }
        //    if (!string.IsNullOrEmpty(sqlStr))
        //    {
        //        totalCount = db.Database.SqlQuery<R>(sqlStr, newOne().ToArray()).Count();
        //    }
        //    else
        //    {
        //        throw new Exception("Parameter 'sqlStr' must have value and not empty");
        //    }

        //    BuildPages(totalCount, currentPage, rowPerPage, numPageView);

        //    if (totalCount > 0)
        //    {
        //        try
        //        {
        //            string sqlStrOffset = $"{sqlStr} OFFSET {(this.CurrentPages - 1) * rowPerPage} ROWS FETCH NEXT {rowPerPage} ROWS ONLY";
        //            this.CurrentPageItems = db.Database.SqlQuery<R>(sqlStrOffset, newOne().ToArray()).ToList();
        //        }
        //        catch
        //        {
        //            this.CurrentPageItems = db.Database.SqlQuery<R>(sqlStr, newOne().ToArray()).
        //            Skip((this.CurrentPages - 1) * rowPerPage).
        //            Take(rowPerPage).ToList();
        //        }
        //    }
        //    else
        //    {
        //        this.CurrentPageItems = new List<R>();
        //    }
        //}

        //public void CalculatePaging(
        //    DbContext db,
        //    DbSet<R> dbset,
        //    string sqlStr,
        //    int currentPage,
        //    int rowPerPage,
        //    int numPageView)
        //{
        //    int totalCount;
        //    if (!string.IsNullOrEmpty(sqlStr))
        //    {
        //        totalCount = db.Database.SqlQuery<R>(sqlStr).Count();
        //    }
        //    else
        //    {
        //        throw new Exception("Parameter 'sqlStr' must have value and not empty");
        //    }

        //    BuildPages(totalCount, currentPage, rowPerPage, numPageView);

        //    if (totalCount > 0)
        //    {
        //        if (!string.IsNullOrEmpty(sqlStr))
        //        {
        //            try
        //            {
        //                string sqlStrOffset = $"{sqlStr} OFFSET {(this.CurrentPages - 1) * rowPerPage} ROWS FETCH NEXT {rowPerPage} ROWS ONLY";
        //                this.CurrentPageItems = db.Database.SqlQuery<R>(sqlStrOffset).ToList();
        //            }
        //            catch
        //            {

        //                this.CurrentPageItems = db.Database.SqlQuery<R>(sqlStr).
        //                Skip((this.CurrentPages - 1) * rowPerPage).
        //                Take(rowPerPage).ToList();
        //            }
        //        }
        //    }
        //    else
        //    {
        //        this.CurrentPageItems = new List<R>();
        //    }


        //    foreach (var item in this.CurrentPageItems)
        //    {
        //        dbset.Attach(item);
        //        db.Entry(item).Reload();
        //    }
        //}


        /// <summary>
        /// Tính paging cho các page
        /// </summary>
        /// <param name="data"></param>
        /// <param name="currentPage"></param>
        /// <param name="rowPerPage"></param>
        /// <param name="numPageView"></param>
        public void CalculatePaging(
            IQueryable<R> data,
            int currentPage,
            int rowPerPage,
            int numPageView)
        {
            int totalCount;
            totalCount = data.Count();

            BuildPages(totalCount, currentPage, rowPerPage, numPageView);

            if (totalCount > 0)
            {
                this.CurrentPageItems = data.
                    Skip((this.CurrentPages - 1) * rowPerPage).
                    Take(rowPerPage).ToList();
            }
            else
            {
                this.CurrentPageItems = new List<R>();
            }
        }

        /// <summary>
        /// Tính paging cho các page
        /// </summary>
        /// <param name="data"></param>
        /// <param name="currentPage"></param>
        /// <param name="rowPerPage"></param>
        /// <param name="numPageView"></param>
        public void CalculatePaging(
            IEnumerable<R> data,
            int currentPage,
            int rowPerPage,
            int numPageView)
        {
            int totalCount;
            totalCount = data.Count();

            BuildPages(totalCount, currentPage, rowPerPage, numPageView);

            if (totalCount > 0)
            {
                this.CurrentPageItems = data.
                    Skip((this.CurrentPages - 1) * rowPerPage).
                    Take(rowPerPage).ToList();
            }
            else
            {
                this.CurrentPageItems = new List<R>();
            }
        }

        /// <summary>
        /// Tính paging cho các page
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="setOfItems"></param>
        /// <param name="whereExpress"></param>
        /// <param name="orderSelecter"></param>
        /// <param name="orderMode"></param>
        /// <param name="currentPage"></param>
        /// <param name="rowPerPage"></param>
        /// <param name="numPageView"></param>
        public void CalculatePaging<TKey>(
            IQueryable<R> setOfItems,
            Expression<Func<R, bool>> whereExpress,
            Expression<Func<R, TKey>> orderSelecter,
            OrderMode orderMode,
            int currentPage,
            int rowPerPage,
            int numPageView)
        {
            int totalCount;
            if (whereExpress != null)
            {
                totalCount = setOfItems.Where(whereExpress).Count();
            }
            else
            {
                totalCount = setOfItems.Count();
            }

            BuildPages(totalCount, currentPage, rowPerPage, numPageView);

            if (totalCount > 0)
            {
                if (whereExpress != null)
                {
                    if (orderSelecter != null)
                    {
                        if (orderMode == OrderMode.Ascending)
                        {
                            this.CurrentPageItems = setOfItems.Where(whereExpress).
                                OrderBy(orderSelecter).
                                Skip((this.CurrentPages - 1) * rowPerPage).
                                Take(rowPerPage).ToList();
                        }
                        else
                        {
                            this.CurrentPageItems = setOfItems.Where(whereExpress).
                                OrderByDescending(orderSelecter).
                                Skip((this.CurrentPages - 1) * rowPerPage).
                                Take(rowPerPage).ToList();
                        }
                    }
                    else
                    {
                        this.CurrentPageItems = setOfItems.Where(whereExpress).
                            Skip((this.CurrentPages - 1) * rowPerPage).
                            Take(rowPerPage).ToList();
                    }
                }
                else
                {
                    if (orderSelecter != null)
                    {
                        if (orderMode == OrderMode.Ascending)
                        {
                            this.CurrentPageItems = setOfItems.OrderBy(orderSelecter).
                            Skip((this.CurrentPages - 1) * rowPerPage).
                            Take(rowPerPage).ToList();
                        }
                        else
                        {
                            this.CurrentPageItems = setOfItems.OrderByDescending(orderSelecter).
                            Skip((this.CurrentPages - 1) * rowPerPage).
                            Take(rowPerPage).ToList();
                        }
                    }
                    else
                    {
                        this.CurrentPageItems = setOfItems.Skip((this.CurrentPages - 1) * rowPerPage).
                            Take(rowPerPage).ToList();
                    }
                }
            }
            else
            {
                this.CurrentPageItems = new List<R>();
            }
        }
        #endregion

        public void ChangeToPage(int? page)
        {
            int requestPage = page ?? 1;
            if (requestPage > PageCount)
            {
                requestPage = PageCount;
                CurrentPages = PageCount;
            }

            // CurrentPages = li_requestPage;
            if (requestPage == 1)
            {
                IsPreviousPages = false;
                IsFirstPages = false;
                PreviousPages = 1;
                if (PageCount > 1)
                {
                    NextPages = CurrentPages + 1;
                    IsNextPages = true;
                    IsLastPages = true;
                }
                else
                {

                    IsLastPages = false;
                    NextPages = 1;
                }
            }
            else if (requestPage == PageCount)
            {
                IsNextPages = false;
                IsLastPages = false;
                NextPages = CurrentPages + 1;
                if (PageCount > 1)
                {
                    IsPreviousPages = true;
                    IsFirstPages = true;
                    PreviousPages = CurrentPages - 1;
                }
                else
                {
                    IsPreviousPages = false;
                    IsFirstPages = false;
                    PreviousPages = 1;
                }
            }

            if (requestPage > 1 && requestPage < PageCount)
            {
                IsFirstPages = true;
                IsLastPages = true;
                IsPreviousPages = true;
                PreviousPages = CurrentPages - 1;
                IsNextPages = true;
                NextPages = CurrentPages + 1;
            }

            // Tính item đầu tiên và item cuối của trang
            StartItem = (CurrentPages - 1) * NumberRowPerPages + 1;
            if (CurrentPages != PageCount)
            {
                EndItem = (CurrentPages) * NumberRowPerPages;
            }
            else
            {
                EndItem = (int)TotalItem;
            }

            //GetPagesNumberList();
            GetPagesNumberListCenter();
        }

        public void FirstPages()
        {
            CurrentPages = 1;
            PreviousPages = 1;
            IsPreviousPages = false;
            IsNextPages = true;
            NextPages = CurrentPages + PagesNumberView;
            NextPages = NextPages > PageCount ? PageCount : NextPages;
            if (PageCount == 1)
            {
                IsNextPages = false;
            }

            //GetPagesNumberList();   
            GetPagesNumberListCenter();
        }

        public void LastPages()
        {
            CurrentPages = PageCount;
            NextPages = PageCount;
            IsNextPages = false;
            PreviousPages = CurrentPages - 1;
            if (PreviousPages < 1)
            {
                IsPreviousPages = false;
                PreviousPages = 1;
            }

            //GetPagesNumberList();
            GetPagesNumberListCenter();
        }

        private void BuildPages(int totalCount, int currentPage, int rowPerPage, int pageView)
        {
            //// Old
            PagesNumberView = pageView;
            CurrentPages = currentPage;
            TotalItem = totalCount;
            NumberRowPerPages = rowPerPage;
            PagesNumberList = new List<int>();
            //// 1. Calculate             
            PageCount = (int)Math.Ceiling((double)TotalItem / NumberRowPerPages);
            //// 2. list number view
            //// GetPagesNumberList();
            //// NextPages(nPage);
            ChangeToPage(currentPage);
        }

        // generator number list pages
        private void GetPagesNumberList()
        {
            int firstPages = 0;
            //// PagesNumberView = 5 of page show first pre 1 2 3 4 5 next last
            int trueView = PagesNumberView > PageCount ? PageCount : PagesNumberView;
            if (CurrentPages % PagesNumberView == 1)
            {
                firstPages = CurrentPages;
            }
            else
            {
                //// firstPages = CurrentPages - PagesNumberView + 1;
                int mod = CurrentPages % PagesNumberView == 0 ? PagesNumberView : 0;
                double divOfNumber = Math.Floor((double)CurrentPages / PagesNumberView);
                firstPages = (((int)divOfNumber) * PagesNumberView) + 1 - mod;
            }

            firstPages = firstPages < 1 ? 1 : firstPages;
            for (int i = 0; i < trueView; i++)
            {
                // pagesNumberList[i] = i;
                if (i + firstPages <= PageCount)
                {
                    PagesNumberList.Add(i + firstPages);
                }
            }
        }

        /// <summary>
        /// generator number list pages. ex: 2 3 |4| 5 7 
        /// </summary>
        private void GetPagesNumberListCenter()
        {
            int firstPages = 1;
            int lastPage = 1;

            int trueView = Math.Min(PagesNumberView, PageCount);

            int halfOfRange = (int)Math.Floor((double)PagesNumberView / 2);

            if (trueView < PagesNumberView)
            {
                firstPages = 1;
                lastPage = trueView;
            }
            else
            {
                lastPage = Math.Min(CurrentPages + halfOfRange, PageCount);
                lastPage = Math.Max(lastPage, PagesNumberView);
                firstPages = lastPage - trueView + 1;
            }

            PagesNumberList.Clear();
            for (int i = firstPages; i <= lastPage; i++)
            {
                PagesNumberList.Add(i);
            }
        }

        public delegate M GetModelFromEntityDelegate<E, M>(E entity);
    }
}