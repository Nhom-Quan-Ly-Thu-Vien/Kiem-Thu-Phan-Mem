using System;
using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;




namespace Test
{
    //class để đọc ghi file excel
    class DocGhi
    {
        //khởi tại 1 ứng dụng Excel
        public static Excel._Application App;
        //khởi tạo bảng tính excel
        public static Excel._Workbook Workbook;
        //khởi tạo các sheets trong workbook
        public static Excel._Worksheet Worksheet;
        //quản lý các thành phần trong sheets
        public static Excel.Range Range;

        //hàm để mở file excel
        public static void open(String fileName)
        {
            //tạo đối tượng COM mở kết nối với các đối tượng cần dùng để đọc dữ liệu
            App = new Excel.Application();
            Workbook = App.Workbooks.Open(fileName);
            //chọn sheets đầu tiên
            Worksheet = Workbook.Sheets[1];
            Range = Worksheet.UsedRange;
        }

        //phương thức tạo 1 danh sách mảng đối tượng 1 
        //chiều mỗi 1 mảng là 1 dòng trong bảng tính Excel
        public static List<Object[]> getExcelFile(String fileName)
        {
            //mở file
            open(fileName);
            //đếm số dòng dữ liệu
            int rowCount = Range.Rows.Count;
            //khai báo số cột cần lấy giá trị
            int colCount = 4;
            //cột bắt đầu đọc dữ liệu từ cột 2 đến cột 4
            int j = 2;
            //khởi tạo 1 danh sách mảng đối tượng
            List<Object[]> list = new List<Object[]>();

            String ten = "";
            String sl ="";       
            String tg = "";
            String mess = "";
            //đọc từng thành phần trong excel
            for (int i = 2; i <= 13; i++)
            {

                try
                {
                    ten = Range.Cells[i, j++].Value2.ToString();
                }
                catch (Exception)
                {
                    ten = "";
                }

                
                try
                {
                    tg = Range.Cells[i, j++].Value2.ToString();
                }
                catch (Exception)
                {
                    tg = "";
                }
                try
                {
                        sl = Range.Cells[i, j++].Value2.ToString();
                }
                catch (Exception)
                {
                    sl = "";
                }
                try
                {
                    mess = Range.Cells[i, j++].Value2.ToString();
                }
                catch (Exception)
                {
                    mess = null;
                }
                if (j > colCount) { j = 2; }
                //thêm dữ liệu vào mảng ta chỉ đọc đến cột số 4 nếu lớn hơn quay lại cột bạn đầu
                Object[] obj = new Object[] { ten, tg, sl, mess };
                list.Add(obj);


            }
            //đóng kết nối
            Workbook.Close();
            return list;
        }

        //phương thức dùng để ghi dữ liệu vào file excel
        public static void setExcelFile(int i, int j, String str, String fileName)
        {
            open(fileName);
            Range.Cells[i, j].value2 = str;
            Workbook.Save();
            Workbook.Close();
        }


    }

}
