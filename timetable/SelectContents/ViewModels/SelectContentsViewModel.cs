using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace timetable.Modules.SelectGrid.ViewModels
{
    public class SelectContentsViewModel : BindableBase
    {
        private string applyGradeText;
        public string ApplyGradeText
        {
            get { return applyGradeText; }
            set { SetProperty(ref applyGradeText, value); }
        }



        private ObservableCollection<object> selectlist = new ObservableCollection<object>();



        public ICommand DataDeleteCommand { get; set; }
        public ICommand DataInitCommand { get; set; }
        public ICommand DataInsertCommand { get; set; }
        public ICommand DataSaveCommand { get; set; }
        public ICommand DataLoadCommand { get; set; }
  
        public SelectContentsViewModel()
        {
            ApplyGradeText = "신청학점 : 0 학점";
            DataDeleteCommand = new DelegateCommand(DataDelete);
            DataInitCommand = new DelegateCommand(DataInit);
            DataInsertCommand = new DelegateCommand(DataInsert);
            DataSaveCommand = new DelegateCommand(DataSave);
            DataLoadCommand = new DelegateCommand(DataLoad);

            
        }


        private void DataLoad()
        {
            MessageBox.Show("DataLoad");
        }

        private void DataSave()
        {
            MessageBox.Show("DataSave");
        }

        private void DataInsert()
        {
            MessageBox.Show("DataInsert");
        }

        private void DataInit()
        {
            MessageBox.Show("DataInit");
        }

        private void DataDelete()
        {
            MessageBox.Show("DataDelete");
        }
    }
}
