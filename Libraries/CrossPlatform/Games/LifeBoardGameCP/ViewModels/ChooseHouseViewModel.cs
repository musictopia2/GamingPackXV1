﻿using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.ViewModels;
using LifeBoardGameCP.Data;
using LifeBoardGameCP.Logic;
using System.Threading.Tasks;
namespace LifeBoardGameCP.ViewModels
{
    public class ChooseHouseViewModel : BasicSubmitViewModel
    {
        private readonly LifeBoardGameVMData _model;
        private readonly IHouseProcesses _processes;
        public ChooseHouseViewModel(CommandContainer commandContainer,
            LifeBoardGameVMData model,
            IHouseProcesses processes
            ) : base(commandContainer)
        {
            _model = model;
            _processes = processes;
        }
        public override bool CanSubmit => _model.HandList.ObjectSelected() > 0;
        public override Task SubmitAsync()
        {
            return _processes.ChoseHouseAsync(_model.HandList.ObjectSelected());
        }
    }
}