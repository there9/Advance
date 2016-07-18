using Microsoft.Win32;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

        //Property
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
        public int TotalGrade { get; set; }



        //PropertyCommand and EventAggregator
        public ICommand DataDeleteCommand { get; set; }
        public ICommand DataInitCommand { get; set; }
        public ICommand DataInsertCommand { get; set; }
        public ICommand DataSaveCommand { get; set; }
        public ICommand DataLoadCommand { get; set; }
        public ICommand DoubleCommand { get; set; }
        public ICommand SelectItemCommand { get; set; }

        public IEventAggregator IEvent { get; set; }

        //ctor
        public SelectContentsViewModel(IEventAggregator iEventAggregator)
        {
            //DelegateCommand 
            DataDeleteCommand = new DelegateCommand(DataDelete);
            DataInitCommand = new DelegateCommand(DataInit);
            DataInsertCommand = new DelegateCommand<object>(DataInsertRequest);
            DataSaveCommand = new DelegateCommand<object>(DataSave);
            DataLoadCommand = new DelegateCommand<object>(DataLoad);
            DoubleCommand = new DelegateCommand(DataDelete);
            SelectItemCommand = new DelegateCommand<object>(SelectItem);

            //EventAggregator
            IEvent = iEventAggregator;
            iEventAggregator.GetEvent<InsertOneTableEvent>().Subscribe(DataInsert);
            iEventAggregator.GetEvent<SaveEvent>().Subscribe(DataSave);

            //init
            ApplyGradeText = "신청학점 : " + TotalGrade + "학점";
        }

        List<ClassInfo> saveMyClassInfo = new List<ClassInfo>();

        //DataInsertCommand 
        private void DataInsert(object obj)
        {
            ClassInfo info = obj as ClassInfo;
            info.TimeSetting();
            foreach (ClassInfo item in saveMyClassInfo)
            {
                item.TimeSetting();
                if (info.CompareTime(item) == true)
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
        //SelectItemCommand 
        ClassInfo SelectedClassInfo;
        private void SelectItem(object obj)
        {

            IList<object> infoclass = obj as IList<object>;
            if (isNochange == false && obj != null && infoclass.Count == 1)
            {
                SelectedClassInfo = infoclass[0] as ClassInfo;
            }

        }
        //DataInsertCommand 
        private void DataInsertRequest(object obj)
        {
            IEvent.GetEvent<RequestAddItemEvent>().Publish(null);

        }
        //DataLoadCommand 
        private void DataLoad(object obj)
        {
            //선택리스트를 text파일로 저장한다.

            OpenFileDialog oFileDialog = new OpenFileDialog();
            oFileDialog.Filter = "Json Documents (*.json) | *.json";
            oFileDialog.InitialDirectory = @"C:\";
            if (oFileDialog.ShowDialog() == false)
            {
                return;
            }
            string path = oFileDialog.FileName;

            if (File.Exists(@path))
            {
                // deserialize JSON directly from a file
                List<ClassInfo> list = new List<ClassInfo>();
                using (StreamReader file = File.OpenText(@path))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    list = (List<ClassInfo>)serializer.Deserialize(file, typeof(List<ClassInfo>));
                }
                saveMyClassInfo.Clear();
                TotalGrade = 0;
                foreach (ClassInfo info in list)
                {
                    saveMyClassInfo.Add(info);
                    TotalGrade += int.Parse(info.Grade);
                    refresh();
                }
                ApplyGradeText = "신청학점 : " + TotalGrade + "학점";
                IEvent.GetEvent<LoadImgEvent>().Publish(saveMyClassInfo);
            }
        }

        private void DataSave(object obj)
        {
            string str = JsonConvert.SerializeObject(saveMyClassInfo);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Json Documents (*.json) | *.json";
            saveFileDialog.InitialDirectory = @"C:\";
            saveFileDialog.FileName = "timetableInfo";

            if (saveFileDialog.ShowDialog() == false)
            {
                return;
            }

            string path = saveFileDialog.FileName;
            File.WriteAllText(saveFileDialog.FileName, str);
        }
        //DataInitCommand 
        private void DataInit()
        {
            saveMyClassInfo.Clear();
            TotalGrade = 0;
            ApplyGradeText = "신청학점 : " + TotalGrade + "학점";
            refresh();
            IEvent.GetEvent<ClearTableEvent>().Publish(null);
        }

        //Delete No Change Flag
        bool isNochange = false;

        //DataDeleteCommand 
        private void DataDelete()
        {
            if (saveMyClassInfo.Count > 0 && SelectedClassInfo != null)
            {
                isNochange = true;
                TotalGrade -= int.Parse(SelectedClassInfo.Grade);
                ApplyGradeText = "신청학점 : " + TotalGrade + "학점";
                saveMyClassInfo.Remove(SelectedClassInfo);
                refresh();

                IEvent.GetEvent<DeleteOneTableEvent>().Publish(null);
            }
            else
            {
                MessageBox.Show("시간표를 삭제할 과목이 2개이상이거나 없습니다.");
            }
            isNochange = false;
        }
        //DataGridRefresh
        private void refresh()
        {
            MyClassInfo = null;
            MyClassInfo = saveMyClassInfo;
        }
    }
}
