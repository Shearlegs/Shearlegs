using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Shearlegs.Web.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shearlegs.Web.Shared.Components
{
    public partial class Modal
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        public virtual string ModalId => nameof(Modal);

        public virtual async Task ShowAsync()
        {
            await JSRuntime.ShowModalStaticAsync(ModalId);
        }

        public virtual async Task HideAsync()
        {
            await JSRuntime.HideModalAsync(ModalId);
        }
    }
}
