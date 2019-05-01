using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections.ObjectModel;
using DTO;
using System.IO;

namespace PhamQuocCuong_TestSuaDocGia
{
    class DocGhiFileExcel
    {
        public static Excel._Application App;
        public static Excel._Workbook Workbook; //Sử dụng Workbook cho file đuôi .xlsx và HWorkbook đối với file đuôi .slx
        public static Excel._Worksheet Worksheet; //Quản lý các sheet trong excel
        public static Excel.Range Range; //Quản lý các thành phần trong 1 sheet

        public static void open(String fileName)
        {
            //Tạo đối tượng COM và mở kết nối tới các đối tượng cần dùng để đọc dữ Liệu
            App = new Excel.Application();
            Workbook = App.Workbooks.Open(fileName);
            Worksheet = Workbook.Sheets[1];
            //Range = Worksheet.UsedRange;
            Range = Worksheet.Range["D6:I26"]; //Vùng dữ liệu cần lấy
        }
        public static void close()
        {
            //Đóng toàn bộ
            GC.Collect();
            GC.WaitForPendingFinalizers();

            Marshal.ReleaseComObject(Range);
            Marshal.ReleaseComObject(Worksheet);

            //Đóng và thu hồi kết nối
            Workbook.Save();
            Workbook.Close();
            Marshal.ReleaseComObject(Workbook);

            //Thoát và thu hồi kết nối
            App.Quit();
            Marshal.ReleaseComObject(App);
        }
        //Phương thức này được dùng để tạo ra một danh sách các mảng đối tượng một Chiều, mỗi một mảng là một dòng trong bảng tính Excel.
        public static List<Object[]> getExcelFile(String fileName)
        {
            open(fileName);
            int rowCount = Range.Rows.Count;
            int colCount = 6;
            int j = 1; //Đọc dữ liệu

            List<Object[]> list = new List<Object[]>();
            string madg = "1";
            string tendg = "1";
            string diachi = "1";
            string sdt = "1";
            string gt = "1";
            string msg = "1";
            for (int i = 1; i <= rowCount; i++)
            {
                try
                {
                    madg = Range.Cells[i, j++].Value2.ToString();
                }
                catch (Exception)
                {
                    madg = "null";
                }
                try
                {
                    tendg = Range.Cells[i, j++].Value2.ToString();
                }
                catch (Exception)
                {
                    tendg = "null";
                }
                try
                {
                    diachi = Range.Cells[i, j++].Value2.ToString();
                }
                catch (Exception)
                {
                    diachi = "null";
                }
                try
                {
                    sdt = Range.Cells[i, j++].Value2.ToString();
                }
                catch (Exception)
                {
                    sdt = "null";
                }
                try
                {
                    gt = Range.Cells[i, j++].Value2.ToString();
                }
                catch (Exception)
                {
                    gt = "null";
                }
                try
                {
                    msg = Range.Cells[i, j++].Value2.ToString();
                }
                catch (Exception)
                {
                    msg = "null";
                }
                if (j > colCount)
                {
                    j = 1;
                }

                Object[] obj = new Object[] { madg, tendg, diachi, sdt, gt, msg };
                list.Add(obj);
            }

            close();//Đóng kết nối file Excel
            return list;
        }

        //Phương thức này sử dụng để ghi dữ liệu vào file Excel theo từng thành phần 
        public static void setExcelFile(int i, int j, String str, String fileName)
        {
            open(fileName);
            Range.Cells[i, j].Value2 = str;
            close();
        }
    }
}
