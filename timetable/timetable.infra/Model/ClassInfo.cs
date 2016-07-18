﻿using LinqToExcel.Attributes;
using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TimeTable.Infra
{
    public class ClassInfo : BindableBase, IGridInfo
    {
        [ExcelColumn("교과목코드")] //maps the "Name" property to the "Company Title" column
        public string Code { get; set; }

        [ExcelColumn("교과목명(국)")] //maps the "State" property to the "Providence" column
        public string SubjectName { get; set; }

        [ExcelColumn("분반")] //maps the "Employees" property to the "Employee Count" column
        public string ClassNumber { get; set; }

        [ExcelColumn("교수")] //maps the "Name" property to the "Company Title" column
        public string ProfessorName { get; set; }

        [ExcelColumn("대상학과 학년 전공")] //maps the "State" property to the "Providence" column
        public string Target { get; set; }

        [ExcelColumn("학")] //maps the "Employees" property to the "Employee Count" column
        public string Grade { get; set; }

        [ExcelColumn("설계학점")] //maps the "Name" property to the "Company Title" column
        public string Design { get; set; }

        [ExcelColumn("영어강의여부")] //maps the "State" property to the "Providence" column
        public string EngClass { get; set; }

        [ExcelColumn("이러닝여부")] //maps the "Employees" property to the "Employee Count" column
        public string ElearningClass { get; set; }

        [ExcelColumn("정원")] //maps the "Employees" property to the "Employee Count" column
        public string MaxStudents { get; set; }

        [ExcelColumn("개설학과명")] //maps the "Employees" property to the "Employee Count" column
        public string Department { get; set; }

        [ExcelColumn("강")] //maps the "Employees" property to the "Employee Count" column
        public string classtime { get; set; }

        [ExcelColumn("실")] //maps the "Employees" property to the "Employee Count" column
        public string practicetime { get; set; }





        [ExcelColumn("교시1")] //maps the "Employees" property to the "Employee Count" column
        public string time01 { get; set; }

        [ExcelColumn("교시2")] //maps the "Employees" property to the "Employee Count" column
        public string time02 { get; set; }

        [ExcelColumn("교시3")] //maps the "Employees" property to the "Employee Count" column
        public string time03 { get; set; }

        [ExcelColumn("교시4")] //maps the "Employees" property to the "Employee Count" column
        public string time04 { get; set; }

        [ExcelColumn("교시5")] //maps the "Employees" property to the "Employee Count" column
        public string time05 { get; set; }

        [ExcelColumn("교시6")] //maps the "Employees" property to the "Employee Count" column
        public string time06 { get; set; }

        [ExcelColumn("교시7")] //maps the "Employees" property to the "Employee Count" column
        public string time07 { get; set; }

        [ExcelColumn("교시8")] //maps the "Employees" property to the "Employee Count" column
        public string time08 { get; set; }

        [ExcelColumn("교시9")] //maps the "Employees" property to the "Employee Count" column
        public string time09 { get; set; }

        [ExcelColumn("교시10")] //maps the "Employees" property to the "Employee Count" column
        public string time10 { get; set; }

        [ExcelColumn("교시11")] //maps the "Employees" property to the "Employee Count" column
        public string time11 { get; set; }

        [ExcelColumn("교시12")] //maps the "Employees" property to the "Employee Count" column
        public string time12 { get; set; }
        [ExcelColumn("교시13")] //maps the "Employees" property to the "Employee Count" column
        public string time13 { get; set; }
        [ExcelColumn("교시14")] //maps the "Employees" property to the "Employee Count" column
        public string time14 { get; set; }
        [ExcelColumn("교시15")] //maps the "Employees" property to the "Employee Count" column
        public string time15 { get; set; }
        [ExcelColumn("교시16")] //maps the "Employees" property to the "Employee Count" column
        public string time16 { get; set; }

        public string EtcClass { get; set; }


        public bool CompareTime(ClassInfo item)
        {
            foreach (string itemString in item.time_strings)
            {
                if (itemString == " ")
                {
                    break;
                }
                foreach (string thisString in this.time_strings)
                {
                    if (thisString == " ")
                    {
                        break;
                    }
                    if (itemString.IndexOf(thisString) > -1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        public ArrayList time_strings = new ArrayList();
        public void TimeSetting()
        {

            AddTime(time01);
            AddTime(time02);
            AddTime(time03);
            AddTime(time04);
            AddTime(time05);
            AddTime(time06);
            AddTime(time07);
            AddTime(time08);
            AddTime(time09);
            AddTime(time10);
            AddTime(time11);
            AddTime(time12);
            AddTime(time13);
            AddTime(time14);
            AddTime(time15);
            AddTime(time16);
            //time[0] = time01;
            //time[1] = time02;
            //time[2] = time03;
            //time[3] = time04;
            //time[4] = time05;
            //time[5] = time06;
            //time[6] = time07;
            //time[7] = time08;
            //time[8] = time09;
            //time[9] = time10;
            //time[10] = time11;
            //time[11] = time12;
            //time[12] = time13;
            //time[13] = time14;
            //time[14] = time15;
            //time[15] = time16;

            InitGridInfo();
        }

        public void AddTime(string str)
        {
            if (!str.Equals(" ")) time_strings.Add(str);
        }


        public void settingEtc()
        {

            if (EngClass == "Y")
            {
                EtcClass += "영";
            }
            if (ElearningClass == "Y")
            {
                EtcClass += "e";
            }
        }

        private int ParseToColumnIndex(string token)
        {
            switch (token)
            {
                case "월": return 2;
                case "화": return 3;
                case "수": return 4;
                case "목": return 5;
                case "금": return 6;
                default: return -1;
            }
        }

        private int ParseToRowIndex(string token)
        {
            switch (token)
            {
                case "01A": return 1;
                case "01B": return 2;
                case "02A": return 3;
                case "02B": return 4;
                case "03A": return 5;
                case "03B": return 6;
                case "04A": return 7;
                case "04B": return 8;
                case "05A": return 9;
                case "05B": return 10;
                case "06A": return 11;
                case "06B": return 12;
                case "07A": return 13;
                case "07B": return 14;
                case "08A": return 15;
                case "08B": return 16;
                case "09A": return 17;
                case "09B": return 18;
                default: return -1;
            }
        }
        
        
        private int row;
        public int Row
        {
            get { return row; }
            set { SetProperty(ref row, value); }
        }

        private int col;
        public int Column
        {
            get { return col; }
            set { SetProperty(ref col, value); }
        }

        private int rowspan;
        public int RowSpan
        {
            get { return rowspan; }
            set { SetProperty(ref rowspan, value); }
        }
        private void InitGridInfo()
        {
            try
            {
                //string first = time_strings[0] as string;
                //string[] tokens = first.Split('/');
                //Row = ParseToRowIndex(tokens[1]);
                //Column = ParseToColumnIndex(tokens[0]);
                //RowSpan = time_strings.Count;
                string cls;
                string practice;
                string[] tokens;

                for (int i = 0; i < 2; i++)
                {
                    if(i==0)
                    {
                        cls = time_strings[0] as string;
                        tokens = cls.Split('/');
                        Row = ParseToRowIndex(tokens[1]);
                        Column = ParseToColumnIndex(tokens[0]);
                        RowSpan = Int32.Parse(classtime) * 2;
                    }
                    else
                    {
                        practice = time_strings[RowSpan] as string;
                        tokens = practice.Split('/');
                        Row = ParseToRowIndex(tokens[1]);
                        Column = ParseToColumnIndex(tokens[0]);
                        RowSpan = Int32.Parse(practicetime) * 2;
                    }
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}
