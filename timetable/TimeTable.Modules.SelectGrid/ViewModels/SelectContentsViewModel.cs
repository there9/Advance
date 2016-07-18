using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TimeTable.Infra;
using TimeTable.Modules.TotalGrid;

namespace TimeTable.Modules.SelectGrid.ViewModels
{
    public class SelectContentsViewModel : BindableBase
    {
        private string applyGradeText;
        public string ApplyGradeText
        {
            get { return applyGradeText; }
            set { SetProperty(ref applyGradeText, value); }
        }
        private List<ClassInfo> myClassInfo;
        public List<ClassInfo> MyClassInfo
        {
            get { return myClassInfo; }
            set { SetProperty(ref myClassInfo, value); }
        }

        public int TotalGrade{ get; set; }



        private ObservableCollection<object> selectlist = new ObservableCollection<object>();

        public IEventAggregator EV { get; set; }

        public ICommand DataDeleteCommand { get; set; }
        public ICommand DataInitCommand { get; set; }
        public ICommand DataInsertCommand { get; set; }
        public ICommand DataSaveCommand { get; set; }
        public ICommand DataLoadCommand { get; set; }
        public ICommand DoubleCommand { get; set; }
        public ICommand SelectItemCommand { get; set; }

        public SelectContentsViewModel(IEventAggregator ev)
        {
            EV = ev;
            ApplyGradeText = "신청학점 : "+ TotalGrade+"학점";
            DataDeleteCommand = new DelegateCommand(DataDelete);
            DataInitCommand = new DelegateCommand(DataInit);
            DataInsertCommand = new DelegateCommand<object>(DataInsertRequest);
            DataSaveCommand = new DelegateCommand(DataSave);
            DataLoadCommand = new DelegateCommand<object>(DataLoad);
            DoubleCommand = new DelegateCommand(DataDelete);
            SelectItemCommand = new DelegateCommand<object>(SelectItem);

            ev.GetEvent<InsertOneTableEvent>().Subscribe(DataInsert);
            ev.GetEvent<LoadEvent>().Subscribe(DataLoad);
            ev.GetEvent<SaveEvent>().Subscribe(DataSave);

        }

        



        List<ClassInfo> saveMyClassInfo = new List<ClassInfo>(); 
        private void DataInsert(object obj)
        {
            ClassInfo info = obj as ClassInfo;
            info.TimeSetting();
            foreach (ClassInfo item in saveMyClassInfo)
            {
                item.TimeSetting();
                if(info.CompareTime(item)==true)
                {
                    MessageBox.Show("시간 중복!");
                    return;
                }
            }
            TotalGrade += int.Parse(info.Grade);
            ApplyGradeText = "신청학점 : " + TotalGrade + "학점";
            saveMyClassInfo.Add(info);
            refresh();
        }

        ClassInfo SelectedClassInfo;
        private void SelectItem(object obj)
        {
            
                IList<object> infoclass = obj as IList<object>;
                if (isNochange == false && obj != null && infoclass.Count==1)
                {
                    SelectedClassInfo = infoclass[0] as ClassInfo;
                }

        }
        private void DataInsertRequest(object obj)
        {
            EV.GetEvent<RequestAddItemEvent>().Publish(null);

        }

        private void DataLoad(object obj)
        {
            //다이어로그만 있음 진행가능
            MessageBox.Show("불러오기");
            //string[] lines = System.IO.File.ReadAllLines(@"C:\Users\NSTL_DH\Desktop\test.txt");
            //foreach (string s in lines)
            //{
            //   MessageBox.Show(s);
            //}
        }
        
        private void DataSave()
        {
            MessageBox.Show("DataSave");
        }

        private void DataSave(object obj)
        {
            //선택리스트를 text파일로 저장한다.
            MessageBox.Show("DataSave");
        }

        private void DataInit()
        {
            saveMyClassInfo.Clear();
            TotalGrade = 0;
            ApplyGradeText = "신청학점 : " + TotalGrade + "학점";
            refresh();
            EV.GetEvent<ClearTableEvent>().Publish(null);
        }

        bool isNochange = false;
        private void DataDelete()
        {
            if(saveMyClassInfo.Count > 0 && SelectedClassInfo!=null)
            {
                isNochange = true;
                TotalGrade -= int.Parse(SelectedClassInfo.Grade);
                ApplyGradeText = "신청학점 : " + TotalGrade + "학점";
                saveMyClassInfo.Remove(SelectedClassInfo);
                refresh();
                
                EV.GetEvent<DeleteOneTableEvent>().Publish(null);
            }
            else
            {
                MessageBox.Show("시간표를 삭제할 과목이 2개이상이거나 없습니다.");
            }
            isNochange = false;
        }
        private void refresh()
        {
            MyClassInfo = null;
            MyClassInfo = saveMyClassInfo;
        }
    }
}
