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
        


	//Property Command and EventAggregator
        public ICommand SaveCommand { get; set; }
        public ICommand LoadCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand SaveTimeTableImgCommand { get; set; }
        public ICommand ProgramInfoCommand { get; set; }
	public IEventAggregator IEvent { get; private set; }
        
	




	//ctor
	public MenuViewModel(IEventAggregator iEventAggregator)
        {

            
	    //DelegateCommand
            SaveCommand = new DelegateCommand(() => iEventAggregator.GetEvent<SaveEvent>().Publish(null));
            LoadCommand = new DelegateCommand(() => iEventAggregator.GetEvent<LoadEvent>().Publish(null));
            ExitCommand = new DelegateCommand(() => Environment.Exit(0));
            SaveTimeTableImgCommand = new DelegateCommand(() => iEventAggregator.GetEvent<SaveTimeTableImgEvent>().Publish(null));
            ProgramInfoCommand = new DelegateCommand(ProgramInfo);

	    //iEventAggregator
	    IEvent = iEventAggregator;

        }




	//Function Processing 
	//ProgramInfoCommand 
        private void ProgramInfo()
        {
            MessageBox.Show("ProgramInfo");
        }
       
    }
}
