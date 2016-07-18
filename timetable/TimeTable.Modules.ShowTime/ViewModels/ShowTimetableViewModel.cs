using Microsoft.Win32;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TimeTable.Infra;
using TimeTable.Modules.TotalGrid;

namespace TimeTable.Modules.ShowTime.ViewModels
{
    public class ShowTimetableViewModel : BindableBase
    {

        private ObservableCollection<IGridInfo> items;
        public ObservableCollection<IGridInfo> Items
        {
            get { return items; }
            set { SetProperty(ref items, value); }
        }
        private Brush bg;
        public Brush BG
        {
            get { return bg; }
            set { SetProperty(ref bg, value); }
        }
        IRegionManager _regionManager;
        public ShowTimetableViewModel(IEventAggregator ea, IRegionManager rm)
        {
            _regionManager = rm;
            TextBlock txt1 = new TextBlock();
            Items = new ObservableCollection<IGridInfo>();
            Items.Add(new LabelInfo() { Row = 1, Column = 0, Label = "01A" });
            Items.Add(new LabelInfo() { Row = 2, Column = 0, Label = "01B" });
            Items.Add(new LabelInfo() { Row = 3, Column = 0, Label = "02A" });
            Items.Add(new LabelInfo() { Row = 4, Column = 0, Label = "02B" });
            Items.Add(new LabelInfo() { Row = 5, Column = 0, Label = "03A" });
            Items.Add(new LabelInfo() { Row = 6, Column = 0, Label = "03B" });
            Items.Add(new LabelInfo() { Row = 7, Column = 0, Label = "04A" });
            Items.Add(new LabelInfo() { Row = 8, Column = 0, Label = "04B" });
            Items.Add(new LabelInfo() { Row = 9, Column = 0, Label = "05A" });
            Items.Add(new LabelInfo() { Row = 10, Column = 0, Label = "05B" });
            Items.Add(new LabelInfo() { Row = 11, Column = 0, Label = "06A" });
            Items.Add(new LabelInfo() { Row = 12, Column = 0, Label = "06B" });
            Items.Add(new LabelInfo() { Row = 13, Column = 0, Label = "07A" });
            Items.Add(new LabelInfo() { Row = 14, Column = 0, Label = "07B" });
            Items.Add(new LabelInfo() { Row = 15, Column = 0, Label = "08A" });
            Items.Add(new LabelInfo() { Row = 16, Column = 0, Label = "08B" });
            Items.Add(new LabelInfo() { Row = 17, Column = 0, Label = "09A" });
            Items.Add(new LabelInfo() { Row = 18, Column = 0, Label = "09B" });
            Items.Add(new LabelInfo() { Row = 1, Column = 1, Label = "09:00" });
            Items.Add(new LabelInfo() { Row = 2, Column = 1, Label = "09:30" });
            Items.Add(new LabelInfo() { Row = 3, Column = 1, Label = "10:00" });
            Items.Add(new LabelInfo() { Row = 4, Column = 1, Label = "10:30" });
            Items.Add(new LabelInfo() { Row = 5, Column = 1, Label = "11:00" });
            Items.Add(new LabelInfo() { Row = 6, Column = 1, Label = "11:30" });
            Items.Add(new LabelInfo() { Row = 7, Column = 1, Label = "12:00" });
            Items.Add(new LabelInfo() { Row = 8, Column = 1, Label = "12:30" });
            Items.Add(new LabelInfo() { Row = 9, Column = 1, Label = "13:00" });
            Items.Add(new LabelInfo() { Row = 10, Column = 1, Label = "13:30" });
            Items.Add(new LabelInfo() { Row = 11, Column = 1, Label = "14:00" });
            Items.Add(new LabelInfo() { Row = 12, Column = 1, Label = "14:30" });
            Items.Add(new LabelInfo() { Row = 13, Column = 1, Label = "15:00" });
            Items.Add(new LabelInfo() { Row = 14, Column = 1, Label = "15:30" });
            Items.Add(new LabelInfo() { Row = 15, Column = 1, Label = "16:00" });
            Items.Add(new LabelInfo() { Row = 16, Column = 1, Label = "16:30" });
            Items.Add(new LabelInfo() { Row = 17, Column = 1, Label = "17:00" });
            Items.Add(new LabelInfo() { Row = 18, Column = 1, Label = "17:30" });
            Items.Add(new LabelInfo() { Row = 19, Column = 0, Label = "이후" });
            Items.Add(new LabelInfo() { Row = 0, Column = 2, Label = "월" });
            Items.Add(new LabelInfo() { Row = 0, Column = 3, Label = "화" });
            Items.Add(new LabelInfo() { Row = 0, Column = 4, Label = "수" });
            Items.Add(new LabelInfo() { Row = 0, Column = 5, Label = "목" });
            Items.Add(new LabelInfo() { Row = 0, Column = 6, Label = "금" });

            txt1.Text = "2005 Products Shipped";
            txt1.FontSize = 20;
            txt1.FontWeight = System.Windows.FontWeights.Bold;//System.Windows와 using System.Windows.Forms이 MessageBox를 
                                                              //모두 지원하여 참조가 모호해져 한부분을 지우고 나머지 필요한 부분을 이런식으로 정리함
            txt1.Background = System.Windows.Media.Brushes.Blue;
            Grid.SetColumn(txt1, 5);
            Grid.SetRow(txt1, 5);

            ea.GetEvent<SaveTimeTableImgEvent>().Subscribe(SaveImage);
            ea.GetEvent<InsertOneTableEvent>().Subscribe(DrawInsertTable);
            ea.GetEvent<DeleteOneTableEvent>().Subscribe(Deleteable);
            ea.GetEvent<ClearTableEvent>().Subscribe(ClearTable);
            ea.GetEvent<LoadImgEvent>().Subscribe(LoadDrawTable);
        }

        private void LoadDrawTable(object obj)
        {
            List<ClassInfo> infos = obj as List<ClassInfo>;
            foreach (ClassInfo info in infos)
            {
                DrawInsertTable(info);
            }
        }



        //테이블 초기화
        private void ClearTable(object obj)
        {
            MessageBox.Show("초기화 테이블");
        }

        // 테이블 삭제
        private void Deleteable(object obj)
        {
            MessageBox.Show("삭제 테이블");
        }
        List<ClassInfo> saveClass = new List<ClassInfo>();
        // 테이블 삽입 그리기
         private void DrawInsertTable(object obj)
        {
            ClassInfo drawInfo = obj as ClassInfo;
            drawInfo.TimeSetting();
            foreach (ClassInfo comparClass in saveClass)
            {
                if(comparClass.CompareTime(drawInfo)==true)
                {
                   return ;
                }
            }
            saveClass.Add(drawInfo);
            drawInfo.TimeSetting();
            ArrayList time = drawInfo.time_strings;
            int colum = 0;
            int row = 0;
            List<Point> savePoint = new List<Point>();
            for (int i=0; i<time.Count; i++)
            {
                string[] s = (time[i]).ToString().Split('/');

                row = int.Parse(s[1].Remove(2)) * 2 - 1;

                if (s[1].Contains("B"))
                {
                    row += 1;
                }
                switch (s[0])
                {
                    case "월":
                        colum = 2;
                        break;
                    case "화":
                        colum = 3;
                        break;
                    case "수":
                        colum = 4;
                        break;
                    case "목":
                        colum = 5;
                        break;
                    case "금":
                        colum = 6;
                        break;
                }
                savePoint.Add(new Point(colum, row));
            }
            Point spanPoint = savePoint[0];
            int spanCount = 0;
            foreach(Point pt in savePoint)
            {
                if (spanPoint.X != pt.X || pt.Y - spanPoint.Y != spanCount)
                {

                    Items.Add(new ClassInfo() { RowSpan = spanCount, Row = (int)(spanPoint.Y), Column = (int)(spanPoint.X), SubjectName = drawInfo.SubjectName, ProfessorName = drawInfo.ProfessorName, ClassNumber = drawInfo.ClassNumber });
                    spanPoint = new Point(pt.X, pt.Y);
                    spanCount = 1;
                }
                else
                {
                    spanCount++;

                }
            }
            Items.Add(new ClassInfo() { RowSpan = spanCount, Row = (int)(spanPoint.Y), Column = (int)(spanPoint.X), SubjectName = drawInfo.SubjectName, ProfessorName = drawInfo.ProfessorName, ClassNumber = drawInfo.ClassNumber });
        }



        void SaveUsingEncoder(FrameworkElement visual, string fileName, BitmapEncoder encoder)
        {
            RenderTargetBitmap bitmap = new RenderTargetBitmap((int)visual.ActualWidth, (int)visual.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(visual);
            BitmapFrame frame = BitmapFrame.Create(bitmap);
            encoder.Frames.Add(frame);

            using (var stream = File.Create(fileName))
            {
                encoder.Save(stream);
            }
        }

        private void SaveImage(object obj)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif|All files (*.*)|*.*";
            saveFileDialog.FileName = "timetable";
            saveFileDialog.Title = "Timetable";
            saveFileDialog.ShowDialog();

            string path = saveFileDialog.FileName;
            var encoder = new JpegBitmapEncoder();

            FrameworkElement view = _regionManager.Regions["ShowTimeTable"].Views.ElementAt(0) as FrameworkElement;

            this.SaveUsingEncoder(view, path, encoder);
        }
    }
}
