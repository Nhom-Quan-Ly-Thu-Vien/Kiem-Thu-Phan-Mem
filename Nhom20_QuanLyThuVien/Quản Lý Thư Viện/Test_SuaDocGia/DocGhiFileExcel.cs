using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections.ObjectModel;

namespace Test_SuaDocGia
{
    class DocGhiFileExcel
    {
        public static Excel.Application xlApp;
        public static Excel.Workbook xlWorkbook; //Sử dụng Workbook cho file đuôi .xlsx và HWorkbook đối với file đuôi .slx
        public static Excel._Worksheet xlWorksheet; //Quản lý các sheet trong excel
        public static Excel.Range xlRange; //Quản lý các thành phần trong 1 sheet

        public static void open(String fileName)
        {
            //Tạo đối tượng COM và mở kết nối tới các đối tượng cần dùng để đọc dữ Liệu
            xlApp = new Excel.Application();
            xlWorkbook = xlApp.Workbooks.Open(fileName);
            xlWorksheet = xlWorkbook.Sheets[1];
            xlRange = xlWorksheet.UsedRange;
        }
        public static void close()
        {
            //Đóng toàn bộ
            GC.Collect();
            GC.WaitForPendingFinalizers();

            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //Đóng và thu hồi kết nối
            xlWorkbook.Save();
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //Thoát và thu hồi kết nối
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
        }
	 //Phương thức này được dùng để tạo ra một danh sách các mảng đối tượng một Chiều, mỗi một mảng là một dòng trong bảng tính Excel.
        public static Collection<Object[]> getExcelFile(String fileName)
        {
            open(fileName);
            int rowCount = xlRange.Rows.Count;
            int colCount = 8;

            int j = 3; //Vì mình chỉ đọc dữ liệu từ cột số 3 đến cột số 8
            Object[] obj;
            Collection<Object[]> list = new Collection<object[]>();
            for (int i = 3; i <= rowCount; i++)
            {
                // Đọc từng thành phần trong từng dòng của file Excel 
                String madg = xlRange.Cells[i, j++].Value.ToString();
                String tendg = xlRange.Cells[i, j++].Value.ToString();
                String diachi = xlRange.Cells[i, j++].Value.ToString();
                String sdt = xlRange.Cells[i, j++].Value.ToString();
                String gt = xlRange.Cells[i, j++].Value.ToString();
                String msg = xlRange.Cells[i, j++].Value.ToString();
		        //Tạo đối tượng lưu các thành phần vừa đọc được vào một mảng đối tượng chiều
                obj = new Object[] {madg,tendg,diachi,sdt,gt,msg};
                list.Add(obj); //Thêm đối tượng vừa tạo được vào danh sách để có thể truyền vào TestCaseSource bên test.
                if (j > colCount)
                    j = 3;
            }
            
            close();//Đóng kết nối file Excel
            return list;
        }

	 //Phương thức này sử dụng để ghi dữ liệu vào file Excel theo từng thành phần 
        public static void setExcelFile(int i, int j, String str, String fileName)
        {
            open(fileName);
            xlRange.Cells[i, j].value = str;
            close();
        }

    }
}
