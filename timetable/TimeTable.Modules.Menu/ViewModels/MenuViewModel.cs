using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TimeTable.Infra;

namespace TimeTable.Modules.Menu.ViewModels
{
    public class MenuViewModel : BindableBase
    {
        public IEventAggregator EA { get; private set; }

        public ICommand SaveCommand { get; set; }
        public ICommand LoadCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand SaveTimeTableImgCommand { get; set; }
        public ICommand ProgramInfoCommand { get; set; }
        public MenuViewModel(IEventAggregator eventaggregator)
        {
            EA = eventaggregator;

            SaveCommand = new DelegateCommand(() => EA.GetEvent<SaveEvent>().Publish(null));
            LoadCommand = new DelegateCommand(() => EA.GetEvent<LoadEvent>().Publish(null));
            ExitCommand = new DelegateCommand(() => Environment.Exit(0));
            SaveTimeTableImgCommand = new DelegateCommand(() => EA.GetEvent<SaveTimeTableImgEvent>().Publish(null));
            ProgramInfoCommand = new DelegateCommand(ProgramInfo);





        }
        private void ProgramInfo()
        {
            MessageBox.Show("ProgramInfo");
        }
        //private void ProgramInfo()
        //{
        //    MessageBox.Show("ProgramInfo");
        //}

        //private void SaveTimeTableImg()
        //{
        //    string path = DialogHelper.SaveFileDialog();

        //    if(path != null)
        //    {
        //        EA.GetEvent<SaveTimeTableImgEvent>().Publish(path);

        //    }
        //}

        //private void Exit()
        //{
        //    Application.Current.Shutdown();
        //}

        //private void Save()
        //{
        //    MessageBox.Show("Save");
        //}
    }
}
