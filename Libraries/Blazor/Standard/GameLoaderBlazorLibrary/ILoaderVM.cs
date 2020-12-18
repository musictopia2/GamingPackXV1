using BasicGameFrameworkLibrary.ChooserClasses;
using Microsoft.AspNetCore.Components;
using System;
namespace GameLoaderBlazorLibrary
{
    public interface ILoaderVM
    {
        GamePackageLoaderPickerCP? PackagePicker { get; set; }
        string GameName { get; set; }
        RenderFragment? GameRendered { get; }
        Action? StateChanged { get; set; } //so components can call into it.
        string Title { get; }
    }
}