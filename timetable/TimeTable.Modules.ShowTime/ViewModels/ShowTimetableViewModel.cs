using Microsoft.Win32;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
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

        IRegionManager _regionManager;
        public DataRowView drv { get; set; }
        public ClassInfo info { get; set; }

        List<string> Date = new List<string>();
        public ShowTimetableViewModel(IEventAggregator ea, IRegionManager rm)
        {
            _regionManager = rm;
            info = new ClassInfo();

            TextBlock txt1 = new TextBlock();
            Date.Add("월요일");
            Date.Add("화요일");
            Date.Add("수요일");
            Date.Add("목요일");
            Date.Add("금요일");
            txt1.Text = "2005 Products Shipped";
            txt1.FontSize = 20;
            txt1.FontWeight = System.Windows.FontWeights.Bold;//System.Windows와 using System.Windows.Forms이 MessageBox를 
                                                              //모두 지원하여 참조가 모호해져 한부분을 지우고 나머지 필요한 부분을 이런식으로 정리함
            txt1.Background = System.Windows.Media.Brushes.Blue;
            Grid.SetColumn(txt1, 5);
            Grid.SetRow(txt1, 5);

            ea.GetEvent<SaveTimeTableImgEvent>().Subscribe(SaveImage);
            ea.GetEvent<LoadEvent>().Subscribe(Load);
            //ea.GetEvent<InsertOneTableEvent>().Subscribe(DrawInsertTable);
            ea.GetEvent<DeleteOneTableEvent>().Subscribe(Deleteable);
            ea.GetEvent<ClearTableEvent>().Subscribe(ClearTable);

            TheList = new List<System.Windows.FrameworkElement>();
            string s1 = "";
            string s2 = "";
            AddToGrid(0, 0, s1, new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255)), 1, 2);
            for (int i = 1; i <= 9; i++)
            {
                s1 = "0" + i;
                int x = 9;
                if (x + i - 1 == 9)
                {
                    s2 = "0" + (x + (i - 1));
                }
                else
                {
                    s2 = "" + (x + (i - 1));
                }

                for (int j = 0; j < 2; j++)
                {
                    if (j == 0)
                    {
                        s1 += "A";
                        s2 += ":00";

                    }
                    else
                    {
                        s1 = "0" + i;
                        if (x + i - 1 == 9)
                        {
                            s2 = "0" + (x + (i - 1));
                        }
                        else
                        {
                            s2 = "" + (x + (i - 1));
                        }
                        s1 += "B";
                        s2 += ":30";
                    }
                    AddToGrid(2 * i - 1 + j, 0, s1, new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255)));
                    AddToGrid(2 * i - 1 + j, 1, s2, new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255)));
                    // MessageBox.Show(s);
                }
            }
            for (int i = 0; i < 5; i++)
            {
                AddToGrid(0, i + 2, Date[i], new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255)));
            }
            AddToGrid(19, 0, "이후", new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255)), 1, 2);
            for (int i = 0; i < 19; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    AddToGrid(i + 1, j + 2, "", new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255)));
                }
            }
        }

        public void AddToGrid(int x, int y, string text, SolidColorBrush brush, int rowSpan = 1, int columnspan = 1)
        {
            Label item = new Label()
            {
                Content = text,
                Background = brush
            };
            item.BorderThickness = new Thickness(0.4);
            item.BorderBrush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(00, 200, 200));
            item.HorizontalContentAlignment = HorizontalAlignment.Center;
            Grid.SetRow(item, x);
            Grid.SetColumn(item, y);
            Grid.SetRowSpan(item, rowSpan);
            Grid.SetColumnSpan(item, columnspan);

            TheList.Add(item);
        }
        private List<System.Windows.FrameworkElement> theList;
        public List<System.Windows.FrameworkElement> TheList
        {
            get { return theList; }
            set { SetProperty(ref theList, value); }
        }
        private void Load(object obj)
        {
            //불러오기
            MessageBox.Show("불러오기 성공");
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

        // 테이블 삽입 그리기
        private void DrawInsertTable(object obj)
        {
            MessageBox.Show("삽입 테이블");

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

