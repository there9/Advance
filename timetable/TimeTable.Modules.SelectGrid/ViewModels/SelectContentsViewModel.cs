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
using TimeTable.Infra.Model;
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

        private ObservableCollection<ClassInfo> selected_classinfos;
        public ObservableCollection<ClassInfo> SelectedClassInfos
        {
            get { return selected_classinfos; }
            set { SetProperty(ref selected_classinfos, value); }
        }

        public int TotalGrade { get; set; }

        private ClassInfo current_item;
        public ClassInfo CurrentItem
        {
            get { return current_item; }
            set { SetProperty(ref current_item, value); }
        }


        //PropertyCommand and EventAggregator
        public ICommand DataDeleteCommand { get; set; }
        public ICommand DataInitCommand { get; set; }
        public ICommand DataInsertCommand { get; set; }
        public ICommand DataSaveCommand { get; set; }
        public ICommand DataLoadCommand { get; set; }
        public ICommand DoubleCommand { get; set; }

        public IEventAggregator IEvent { get; set; }

        //ctor
        public SelectContentsViewModel(IEventAggregator iEventAggregator)
        {
            SelectedClassInfos = Repository.Instance.SelectedClassInfos;

            //DelegateCommand 
            DataDeleteCommand = new DelegateCommand(DataDelete);
            DataInitCommand = new DelegateCommand(DataInit);
            DataInsertCommand = new DelegateCommand<object>(DataInsertRequest);
            DataSaveCommand = new DelegateCommand<object>(DataSave);
            DataLoadCommand = new DelegateCommand<object>(DataLoad);
            DoubleCommand = new DelegateCommand(DataDelete);

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
            foreach (ClassInfo item in SelectedClassInfos)
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

            SelectedClassInfos.Add(info);
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

                //JToken jToken;
                //JObject jObject;

                //using (StreamReader file = File.OpenText(@path))
                //using (JsonTextReader reader = new JsonTextReader(file))
                //{
                //    //jObject = JObject.Load(reader);
                //    jToken = JToken.ReadFrom(reader);
                //}
                //jObject = JObject.Parse(jToken.ToString());
                // string str = jToken.ToString();
                //ClassInfo jc = JsonConvert.DeserializeObject<ClassInfo>(jToken.ToString());

                //MessageBox.Show(jc.Target.ToString());
                //SelectedClassInfo.ProfessorName = jt[0]

                //foreach(JToken info in saveMyClassInfo)
                //{

                //}
                //List<ClassInfo> classInfos = new List<ClassInfo>();

                // ClassInfo info = JsonConvert.DeserializeObject<ClassInfo>(str);
            }
            //JsonTextReader reader = new JsonTextReader(new StreamReader(path));
            //List<string> selectInfo = new List<string>();
            //IList<object> infoclass = obj as IList<object>;
            //while (reader.Read())
            //{
            //    if (reader.Value != null)
            //    {
            //        selectInfo.Add(reader.Value + "");
            //    }
            //}
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

        
        private void DataInit()
        {
            SelectedClassInfos.Clear();
            TotalGrade = 0;
            ApplyGradeText = "신청학점 : " + TotalGrade + "학점";
            IEvent.GetEvent<ClearTableEvent>().Publish(null);
        }

        private void DataDelete()
        {

            if (CurrentItem != null)
            {
                
                IEvent.GetEvent<DeleteOneTableEvent>().Publish(CurrentItem);
                SelectedClassInfos.Remove(CurrentItem);
            }
        }
    }
}
