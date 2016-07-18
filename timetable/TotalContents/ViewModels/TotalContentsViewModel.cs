using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace timetable.Modules.TotalGrid.ViewModels
{
    public class TotalContentsViewModel : BindableBase
    {
        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set { SetProperty(ref searchText, value); }
        }

        private List<string> departmentType = new List<string>();
        public List<string> DepartmentType
        {
            get { return departmentType; }
            set { SetProperty(ref departmentType, value); }
        }

        public ICommand SerchCommand { get; set; }
        public ICommand MessageCommand { get; set; }
        public ICommand SearchTextChangeCommnad { get; set; }
        public TotalContentsViewModel()
        {
            MessageCommand = new DelegateCommand(MESSAGE);
            SerchCommand = new DelegateCommand(SEARCH);
            SearchTextChangeCommnad = new DelegateCommand(SEARCHTEXTCHANGE);


            DepartmentType.Add("개설학부");
            DepartmentType.Add("컴공");
            DepartmentType.Add("전전통");
            DepartmentType.Add("에신화");
            DepartmentType.Add("산경");
            DepartmentType.Add("메카");
            DepartmentType.Add("디공,건축");
            DepartmentType.Add("기계");
            DepartmentType.Add("문리HRD");
            DepartmentType.Add("-------");
            DepartmentType.Add("강소기업");
            DepartmentType.Add("기계설계");
            DepartmentType.Add("메카융합");
            DepartmentType.Add("기전융합");


        }

        bool isFastSearch = false;
        private void SEARCHTEXTCHANGE()
        {
            if(isFastSearch ==true )
            { 
                MessageBox.Show("빠른 검색");
            }
        }

        private void SEARCH()
        {
            MessageBox.Show("과목 검색");
        }

        private void MESSAGE()
        {
            MessageBox.Show("도움말 출력");
        }
    }
}
