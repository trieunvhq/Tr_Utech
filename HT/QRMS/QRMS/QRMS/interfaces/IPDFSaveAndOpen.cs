﻿ 
using System;
using System.IO;
using System.Threading.Tasks;

namespace QRMS.interfaces
{
    public interface IPDFSaveAndOpen
    {
        Task SaveAndView(string fileName, String contentType, MemoryStream stream, PDFOpenContext context);
    }

    /// <summary>
    /// Where should the PDF file open. In the app or out of the app.
    /// </summary>
    public enum PDFOpenContext
    {
        InApp,
        ChooseApp
    }
}