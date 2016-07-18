﻿using Prism.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TimeTable.Infra;

namespace TimeTable.Modules.ShowTime.Views
{
    /// <summary>
    /// ShowTimetable.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ShowTimetable : UserControl
    {
        public ShowTimetable(IEventAggregator ea)
        {
            InitializeComponent();
           
        }
    }
}
