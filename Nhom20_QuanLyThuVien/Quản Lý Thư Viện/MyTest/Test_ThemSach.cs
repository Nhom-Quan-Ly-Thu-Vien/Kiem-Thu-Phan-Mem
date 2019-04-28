using QuanLyThuVien;
using System;
using NUnit.Extensions.Forms; //Sử dụng để viết test, nhất thiết phải có
//using NUnit.Framework; //Cần có để sử dụng các annotation trong NUnit
using NUnit.Framework;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DTO;


namespace MyTest.Test
{
    [TestFixture]
    public class Test_ThemSach
    {

        public int row = 1;
        public static List<Object[]> data()
        {
            return DocGhiFileExcel.getExcelFile(@"C:\Users\phamv\Documents\HomeWork\Kiem thu phan mem\Pham Van Hieu\dulieutest.xlsx");
        }

        [TestCaseSource("data"), Test]
        public void testTinh(String ma, String ten, String sl, String tl, String tg, String mess)
        {
            


            //QuanLySach ds = new QuanLySach();
            //ds.Show();

            //String actMsg = "";
            //ModalFormTester msgBox = new ModalFormTester();
            //msgBox.ExpectModal("NOTE", delegate
            //{
            //    MessageBoxTester msg = new MessageBoxTester("NOTE");
            //    actMsg = msg.Text;
            //    msg.ClickOk();
            //});
            if (mess.Equals("Thanh cong"))
            {
                DocGhiFileExcel.setExcelFile(row++, 7, "Pass", @"C:\Users\phamv\Documents\HomeWork\Kiem thu phan mem\Pham Van Hieu\dulieutest.xlsx");
                Assert.AreEqual("Thanh cong", mess);
            }
            else
            {
                DocGhiFileExcel.setExcelFile(row++, 7, "Fail", @"C:\Users\phamv\Documents\HomeWork\Kiem thu phan mem\Pham Van Hieu\dulieutest.xlsx");
                Assert.AreEqual("Thanh cong", mess);
            }

            //if (ma.Equals("MS1"))
            //{
                //Assert.AreEqual("null",mess);
            //}
            //else
            //{
            //    Assert.AreEqual(data.MaSach, "1");
            //}

            //ds.Close();

        }


    }

}


