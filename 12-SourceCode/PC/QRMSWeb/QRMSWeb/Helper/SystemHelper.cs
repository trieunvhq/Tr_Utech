using Microsoft.AspNetCore.Components;
using QRMSWeb.Models;
using QRMSWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace QRMSWeb.Helper
{
    public class SystemHelper
    {
        static readonly HttpClient _httpClient = new HttpClient();
        public static async Task clearSearchParameter(NavigationManager _navigationManager, Blazored.LocalStorage.ILocalStorageService _localStorageService)
        {
            try
            {
                var curUrl = _navigationManager.Uri;
                var curRoutes = _navigationManager.ToAbsoluteUri(curUrl).LocalPath.Split("/");
                var function_name = ((curRoutes.Length > 1 ? curRoutes[1] : "") ?? "").ToLower().Trim();
                var lastFuntionUrl = await _localStorageService.GetItemAsStringAsync("lastAccessFunction");

                if (function_name.Equals(lastFuntionUrl))
                {
                    return;
                }
                await _localStorageService.SetItemAsStringAsync("lastAccessFunction", function_name);
                var length = await _localStorageService.LengthAsync();
                if (length > 0)
                {
                    for (int idx = length - 1; idx >= 0; idx--)
                    {
                        var key = await _localStorageService.KeyAsync(idx);
                        if (key.IndexOf("search_") == 0)
                        {
                            await _localStorageService.SetItemAsStringAsync(key, string.Empty);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var ss = ex;
            }
        }
    }
}
