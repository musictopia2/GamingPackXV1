using BasicGamingUIBlazorLibrary.GameGraphics.Base;
using ClueBoardGameCP.Data;
using ClueBoardGameCP.ViewModels;
using Microsoft.AspNetCore.Components;
using System.Drawing;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace ClueBoardGameBlazor
{
    public partial class DetectiveButton : GraphicsCommand
    {

        [CascadingParameter]
        public EnumDetectiveCategory DetectiveCategory { get; set; }
        [Parameter]
        public RectangleF Bounds { get; set; }
        private ClueBoardGameMainViewModel? _mainVM;
        private DetectiveInfo? _detective;
        protected override void OnInitialized()
        {
            if (DataContext != null)
            {
                _mainVM = (ClueBoardGameMainViewModel)DataContext;
            }
            if (CommandParameter != null)
            {
                _detective = (DetectiveInfo)CommandParameter;
            }
            base.OnInitialized();
        }
        protected override void OnParametersSet()
        {
            if (DetectiveCategory == EnumDetectiveCategory.Notebook)
            {
                MethodName = nameof(ClueBoardGameMainViewModel.FillInClue);
            }
            else if (_detective != null)
            {
                MethodName = PredictionMethod();
            }
            base.OnParametersSet();
        }
        private string PredictionMethod()
        {
            return _detective!.Category switch
            {
                EnumCardType.IsRoom => nameof(ClueBoardGameMainViewModel.CurrentRoomClick),
                EnumCardType.IsWeapon => nameof(ClueBoardGameMainViewModel.CurrentWeaponClick),
                EnumCardType.IsCharacter => nameof(ClueBoardGameMainViewModel.CurrentCharacterClick),
                _ => "",
            };
        }
        private bool WasSelected()
        {

            return _detective!.Category switch
            {
                EnumCardType.IsRoom => _detective.Name == _mainVM!.CurrentRoomName,
                EnumCardType.IsWeapon => _detective.Name == _mainVM!.CurrentWeaponName,
                EnumCardType.IsCharacter => _detective.Name == _mainVM!.CurrentCharacterName,
                _ => false,
            };
        }
        private string FillColor()
        {
            if (_detective == null || DataContext == null)
            {
                return cc.Transparent;
            }
            if (DetectiveCategory == EnumDetectiveCategory.Notebook)
            {
                if (_detective.IsChecked)
                {
                    return cc.LimeGreen;
                }
                return cc.Aqua;
            }
            if (WasSelected())
            {
                return cc.LimeGreen;
            }
            return cc.Aqua;
        }
    }
}