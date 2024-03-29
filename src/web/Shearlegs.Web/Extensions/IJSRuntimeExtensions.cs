﻿using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shearlegs.Web.Extensions
{
    public static class IJSRuntimeExtensions
    {
        public static async Task ShowModalAsync(this IJSRuntime jsRuntime, string modalId)
        {
            await jsRuntime.InvokeVoidAsync("ShowModal", modalId);
        }

        public static async Task ShowModalStaticAsync(this IJSRuntime jsRuntime, string modalId)
        {
            await jsRuntime.InvokeVoidAsync("ShowModalStatic", modalId);
        }

        public static async Task HideModalAsync(this IJSRuntime jsRuntime, string modalId)
        {
            await jsRuntime.InvokeVoidAsync("HideModal", modalId);
        }

        public static async Task ChangeUrlAsync(this IJSRuntime jsRuntime, string url)
        {
            await jsRuntime.InvokeVoidAsync("ChangeUrl", url);
        }

        public static async Task<string> GetFormDataJsonAsync(this IJSRuntime jsRuntime, string formName)
        {
            return await jsRuntime.InvokeAsync<string>("GetFormDataJson", formName);
        }
    }
}
