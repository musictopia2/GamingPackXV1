using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfacesForHelpers;
using BasicGameFrameworkLibrary.NetworkingClasses.Extensions;
using BasicGameFrameworkLibrary.NetworkingClasses.Interfaces;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using BasicGameFrameworkLibrary.ViewModels;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.MainViewModels
{
    public class BasicMultiplayerMainVM : BlazorConductorViewModel, ISimpleGame
    {
        private class PropertyLink
        {
            public PropertyInfo? OurProperty { get; set; }
            public PropertyInfo? VMProperty { get; set; }
        }
        public RestoreViewModel? RestoreScreen { get; set; }

        private string _status = "";
        [VM]
        public string Status
        {
            get { return _status; }
            set
            {
                if (SetProperty(ref _status, value)) { }
            }
        }

        private string _normalTurn = "";
        [VM]
        public string NormalTurn
        {
            get { return _normalTurn; }
            set
            {
                if (SetProperty(ref _normalTurn, value)) { }
            }
        }
        private readonly IEndTurn _mainGame;
        private readonly IViewModelData _viewModel;
        private readonly BasicData _basicData;
        private readonly TestOptions _test;
        private readonly IGamePackageResolver _resolver;
        private readonly CustomBasicList<PropertyLink> _properties = new CustomBasicList<PropertyLink>();
        private readonly INetworkMessages? _network;
        public BasicMultiplayerMainVM(CommandContainer commandContainer,
            IEndTurn mainGame,
            IViewModelData viewModel,
            BasicData basicData,
            TestOptions test,
            IGamePackageResolver resolver
            )
        {
            CommandContainer = commandContainer;
            CommandContainer.IsExecuting = true; //has to be proven false.
            CommandContainer.ManuelFinish = true;
            _mainGame = mainGame;
            _viewModel = viewModel;
            _basicData = basicData;
            _test = test;
            _resolver = resolver;
            _viewModel.PropertyChanged += ViewModelPropertyChange;
            if (_basicData.MultiPlayer)
            {
                _network = Resolve<INetworkMessages>();
            }
            Type type = GetType();
            var ourProperties = type.GetPropertiesWithAttribute<VMAttribute>().ToCustomBasicList();
            if (ourProperties.Count == 0)
            {
                throw new BasicBlankException("There has to be at least one property on the list.  Rethink");
            }
            type = _viewModel.GetType();
            var vmProperties = type.GetPropertiesWithAttribute<VMAttribute>().ToCustomBasicList();
            vmProperties.ForEach(p =>
            {
                var o = ourProperties.Where(x => x.Name == p.Name).SingleOrDefault();
                var v = p;

                if (o != null)
                {
                    PropertyLink link = new PropertyLink();
                    link.VMProperty = v;
                    link.OurProperty = o;
                    _properties.Add(link);
                    o.SetValue(this, v.GetValue(_viewModel));
                }
            });
        }
        protected override Task TryCloseAsync()
        {
            _viewModel.PropertyChanged -= ViewModelPropertyChange; //maybe this could fix the problems once and for all for the screens.
            return base.TryCloseAsync();
        }
        protected override async Task ActivateAsync()
        {
            if (_test.SaveOption == EnumTestSaveCategory.RestoreOnly)
            {
                if (_basicData.MultiPlayer == false || _basicData.Client == false)
                {
                    RestoreScreen = _resolver.Resolve<RestoreViewModel>();
                    await LoadScreenAsync(RestoreScreen);
                }
            }
            

            await base.ActivateAsync(); //now we have to do the base as well.
        }
        private void ViewModelPropertyChange(object sender, PropertyChangedEventArgs e)
        {
            //i think in this case, i have to do attributes on both ends.
            //this should allow me to not have to have overflow errors in the game logic class.
            if (e.PropertyName == "")
            {

                _properties.ForEach(l =>
                {
                    l.OurProperty!.SetValue(this, l.VMProperty!.GetValue(_viewModel));
                });

                return;
            }
            var l = _properties.Where(x => x.VMProperty!.Name == e.PropertyName).SingleOrDefault();
            if (l == null)
            {
                return;
            }
            l.OurProperty!.SetValue(this, l.VMProperty!.GetValue(_viewModel));
        }

        public virtual bool CanEnableAlways()
        {
            return true;
        }
        public virtual bool CanEnableBasics()
        {
            return true;
        }
        protected override Task CloseSpecificChildAsync(IBlazorScreen childViewModel)
        {
            CommandContainer.RemoveOldItems(childViewModel);
            return base.CloseSpecificChildAsync(childViewModel);
        }
        public CommandContainer CommandContainer { get; set; }
        public virtual bool CanEndTurn()
        {
            return true; //on default can end turn.  but there are exceptions.
        }
        [Command(EnumCommandCategory.Game)]
        public virtual async Task EndTurnAsync() //forgot needs to be virtual.  since that replaces the endturnprocess now.
        {
            if (_basicData.MultiPlayer)
            {
                await _network!.SendEndTurnAsync();
            }
            await _mainGame.EndTurnAsync();
        }
        protected virtual Task PreviewEndTurnMultiplayerAsync()
        {
            return Task.CompletedTask;
        }
    }
}