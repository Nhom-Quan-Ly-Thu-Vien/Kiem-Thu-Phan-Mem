using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections.ObjectModel;
using System.Collections;
using DTO;

namespace MyTest.Test
{
    class DocGhiFileExcel
    {
        public static Excel._Application App;
        public static Excel._Workbook Workbook;
        public static Excel._Worksheet Worksheet;
        public static Excel.Range Range;
 
        public static void open(String fileName)
        {
            App = new Excel.Application();
            Workbook = App.Workbooks.Open(fileName);
            Worksheet = Workbook.Sheets[1];
            Range = Worksheet.UsedRange;
        }


        public static List<Object[]> getExcelFile(String fileName)
        {

            open(fileName);
            int rowCount = Range.Rows.Count;
            int colCount = 6;

            int j = 1;

            List<Object[]> list = new List<Object[]>();
            String ma = "1";
            String ten = "1";
            String sl ="1";
            String tl = "1";
            String tg = "1";
            String mess= "1";
            for (int i = 1; i <= rowCount; i++)
            {
                try
                {
                    ma = Range.Cells[i, j++].Value2.ToString();
                }
                catch (Exception)
                {
                    ma = "null";
                }

                try
                {
                    ten = Range.Cells[i, j++].Value2.ToString();
                }
                catch (Exception)
                {
                    ten = "null";
                }

                try
                {
                    sl = Range.Cells[i, j++].Value2.ToString();
                }
                catch (Exception)
                {
                    sl = "null";
                }

                try
                {
                    tl = Range.Cells[i, j++].Value2.ToString();
                }
                catch (Exception)
                {
                    tl = "null";
                }

                try
                {
                    tg = Range.Cells[i, j++].Value2.ToString();
                }
                catch (Exception)
                {
                    tg = "null";
                }

                try
                {
                    mess = Range.Cells[i, j++].Value2.ToString();                    
                }
                catch (Exception)
                {
                    mess = "null";
                }
                if (j > colCount) { j = 1; }

                Console.WriteLine(j);    

                Object[] obj = new Object[]{ma,ten,sl,tl,tg,mess};
                list.Add(obj);
                

            }

            Workbook.Close();
            return list;
        }


        public static void setExcelFile(int i, int j, String str, String fileName)
        {
            open(fileName);
            Range.Cells[i, j].value2 = str;
            Workbook.Save();
            Workbook.Close();
        }

        //public static void Main(string[] args)
        //{
        //    List<object[]> list = getExcelFile(@"c:\users\phamv\documents\homework\kiem thu phan mem\pham van hieu\dulieutest.xlsx");
        //    //foreach (object[] a in list)
        //    //{
        //    //    Console.WriteLine(a[0] + " " + a[1] + " " + a[2] + " " + a[3] + " " + a[4] + " " + a[5]);
        //    //}
        //    Console.Read();

        //}
    }

}
