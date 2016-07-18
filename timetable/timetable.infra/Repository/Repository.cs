using LinqToExcel;
using Microsoft.Win32;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable.Infra.Model
{
    public class Repository : BindableBase
    {
        #region Singleton
        private static Repository _repository = new Repository();
        public static Repository Instance
        {
            get
            {
                return _repository;
            }
        }

        private Repository()
        {

        }
        #endregion


        #region Total Class Infos

        private List<ClassInfo> total_classinfos = null;
        public List<ClassInfo> TotalClassInfos
        {
            get
            {
                if (total_classinfos == null) total_classinfos = MakeTotalClassInfos();
                return total_classinfos;
            }
            set { SetProperty(ref total_classinfos, value); }
        }

        private List<ClassInfo> MakeTotalClassInfos()
        {
            string path = GetFilePath();
            if (path == null) return null;

            var excel = new ExcelQueryFactory(path);
            string sheetName = "";
            DataTable dt = new DataTable();
            var ColumnNames = excel.GetColumnNames("Sheet1");
            var data = from c in excel.WorksheetRange<ClassInfo>("A1", "BB16384", sheetName) select c;

            List<ClassInfo> info_list = data.ToList<ClassInfo>();
            foreach (ClassInfo item in info_list)
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

            return info_list;
        }

        private string GetFilePath()
        {
            //file loading
            OpenFileDialog oFileDialog = new OpenFileDialog();

            oFileDialog.Filter = "Excel (*.xlsx)|*.xlsx|Excel 97-2003 (*.xls)|*.xls";
            oFileDialog.InitialDirectory = @"C:\";
            if (oFileDialog.ShowDialog() == false)
            {
                return null;
            }
            return oFileDialog.FileName;
        }
        #endregion

        #region Selcted Class Infos
        private ObservableCollection<ClassInfo> selected_classinfos;
        public ObservableCollection<ClassInfo> SelectedClassInfos
        {
            get {
                if (selected_classinfos == null) selected_classinfos = new ObservableCollection<ClassInfo>();
                return selected_classinfos;
            }
            set { SetProperty(ref selected_classinfos, value); }
        }
        #endregion
        private ObservableCollection<ClassInfo> showrect_info;
        public ObservableCollection<ClassInfo> Showrect_info
        {
            get {
                if (showrect_info == null) showrect_info = new ObservableCollection<ClassInfo>();
                return showrect_info;
            }
            set { SetProperty(ref showrect_info, value); }
        }
        public List<ClassRect> GetClassRects()
        {
            return null;
        }
    }
}
