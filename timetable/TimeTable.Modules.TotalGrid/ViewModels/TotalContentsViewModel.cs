using LinqToExcel;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TimeTable.Infra;

namespace TimeTable.Modules.TotalGrid.ViewModels
{
    public class TotalContentsViewModel : BindableBase
    {
        private List<ClassInfo> dataExcel;
        public List<ClassInfo> DataExcel
        {
            get { return dataExcel; }
            set { SetProperty(ref dataExcel, value); }
        }
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


        private DataView data;
        public DataView Data
        {
            get { return data; }
            set { SetProperty(ref data, value); }
        }
        public ICommand KeyBoradDownCommand { get; set; }
        public ICommand MessageCommand { get; set; }
        public ICommand SearchTextChangeCommnad { get; set; }
        public ICommand DoubleClickCommand { get; set; }
        public ICommand SelectItemCommand { get; set; }
        public ICommand DepartmentConvertCommand { get; set; }
        public IEventAggregator IEvent { get; set; }

        
        public TotalContentsViewModel(IEventAggregator ieventAggregator)
        {

            IEvent = ieventAggregator;


            ieventAggregator.GetEvent<LoadEvent>().Subscribe(LoadExcel);


            MessageCommand = new DelegateCommand(MESSAGE);
            SearchTextChangeCommnad = new DelegateCommand<object>(Search);
            DoubleClickCommand = new DelegateCommand(DataInsert);
            SelectItemCommand = new DelegateCommand<object>(SelectItem);
            DepartmentConvertCommand = new DelegateCommand<object>(DepartmentConvert);
           


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


            ieventAggregator.GetEvent<SaveEvent>().Subscribe(RefreshGridView);
            ieventAggregator.GetEvent<RequestAddItemEvent>().Subscribe(DataInsertReq);
        }

        private void LoadExcel(object obj)
        {
            try
            {
                //file loading

                OpenFileDialog oFileDialog = new OpenFileDialog();

                oFileDialog.Filter = "Excel (*.xlsx)|*.xlsx|Excel 97-2003 (*.xls)|*.xls";
                oFileDialog.InitialDirectory = @"C:\";
                if (oFileDialog.ShowDialog() == false)
                {
                    return;
                }
                string path = oFileDialog.FileName;

                //string path = "" + @"C:\Users\jinhee\Desktop\20162160616.xlsx";
                var excel = new ExcelQueryFactory(path);
                //excel.DatabaseEngine = DatabaseEngine.Ace;

                string sheetName = "";
                DataTable dt = new DataTable();
                //foreach (var a in excel.GetWorksheetNames())
                //{
                //    sheetName = a;
                //    break;
                //}

                // file reading
                var ColumnNames = excel.GetColumnNames("Sheet1");
                var data = from c in excel.WorksheetRange<ClassInfo>("A1", "BB16384", sheetName) select c;

                List<ClassInfo> list = data.ToList<ClassInfo>();
                foreach (ClassInfo item in list)
                {

                    if (item.EngClass == "Y")
                    {
                        item.EtcClass += "영";
                    }
                    if (item.ElearningClass == "Y")
                    {
                        item.EtcClass += "e";
                    }
                }
                DataExcel = list;
                saveClassInfo = list;
            }
            catch (InvalidOperationException e)
            {
                int k = 4;
            }
        }


        private List<ClassInfo> saveClassInfo = new List<ClassInfo>();

        private void Search(object obj)
        {
            TextChangedEventArgs o =  obj as TextChangedEventArgs;
            TextBox b = o.OriginalSource as TextBox;
            
            DataExcel = saveClassInfo;
            SearchText = b.Text;


            if (SearchText != "")
            {
                List<ClassInfo> TempClassInfo = new List<ClassInfo>();

                foreach (ClassInfo item in DataExcel)
                {
                    if (item.Code.Contains(SearchText))
                    {
                        TempClassInfo.Add(item);
                    }
                    else if (item.ClassNumber.Contains(SearchText))
                    {
                        TempClassInfo.Add(item);
                    }
                    else if (item.classtime.Contains(SearchText))
                    {
                        TempClassInfo.Add(item);
                    }
                    else if (item.Design.Contains(SearchText))
                    {
                        TempClassInfo.Add(item);
                    }
                    else if (item.Department.Contains(SearchText))
                    {
                        TempClassInfo.Add(item);
                    }
       
                    else if (item.EtcClass.Contains(SearchText))
                    {
                        TempClassInfo.Add(item);
                    }
                    else if (item.Grade.Contains(SearchText))
                    {
                        TempClassInfo.Add(item);
                    }
                    else if (item.MaxStudents.Contains(SearchText))
                    {
                        TempClassInfo.Add(item);
                    }
                    else if (item.ProfessorName.Contains(SearchText))
                    {
                        TempClassInfo.Add(item);
                    }
                    else if (item.practicetime.Contains(SearchText))
                    {
                        TempClassInfo.Add(item);
                    }
                    else if (item.SubjectName.Contains(SearchText))
                    {
                        TempClassInfo.Add(item);
                    }

                }
                DataExcel = TempClassInfo;
            }
            
        }



        private void DepartmentConvert(object obj)
        {
            IList<object> departmentName = obj as IList<object>;

            DataExcel = saveClassInfo;
            string ConvertString = (departmentName[0] as string);

            switch (ConvertString)
            {
                case "개설학부": ConvertString ="";
                    break;
                case "컴공": ConvertString ="컴퓨터공학부";
                    break;
                case "전전통": ConvertString = "전기ㆍ전자ㆍ통신공학부";
                    break;
                case "에신화": ConvertString ="에너지신소재화학공학부";
                    break;
                case "산경": ConvertString ="산업경영학부";
                    break;
                case "메카": ConvertString ="메카트로닉스공학부";
                    break;
                case "기계": ConvertString = "기계공학부";
                    break;
                case "문리HRD": ConvertString = "문리HRD학부";
                    break;
                case "강소기업": ConvertString ="강소기업경영학과";
                    break;
                case "기계설계": ConvertString ="기계설계공학과";
                    break;
                case "메카융합": ConvertString ="메카IT융합공학과";
                    break;
                case "기전융합": ConvertString ="기전융합공학과";
                    break;
                case "디공,건축": ConvertString = "디자인ㆍ건축공학부";
                    break;
                default:
                    ConvertString = "";
                    break;
            }
            if(ConvertString!= "")
            { 
                List<ClassInfo> TempClassInfo = new List<ClassInfo>();

                foreach (ClassInfo item in DataExcel)
                {
                    if(item.Department == ConvertString)
                    {
                        TempClassInfo.Add(item);
                    }
                }
                DataExcel = TempClassInfo;
            }
            
        }

        ClassInfo SelectedClassInfo;
        private void SelectItem(object obj)
        {
          
                IList<object> infoclass = obj as IList<object>;
                if (infoclass.Count == 1)
                {
                    SelectedClassInfo = infoclass[0] as ClassInfo;
                }
        }

       
        private void DataInsertReq(object obj)
        {
            DataInsert();
        }

        

        private void DataInsert()
        {
            //추가버튼을 눌렀을 때 publish를 보낸다. 그 전에 그 리스트를 삭제한다.
            if(SelectedClassInfo != null)
            {
                IEvent.GetEvent<InsertOneTableEvent>().Publish(SelectedClassInfo);
            }
        }

        private void RefreshGridView(object obj)
        {
            //???
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
