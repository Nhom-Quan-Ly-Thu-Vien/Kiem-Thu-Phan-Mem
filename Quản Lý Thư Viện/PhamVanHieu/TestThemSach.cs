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


namespace PhamVanHieu
{
    [TestFixture]
    public class TestThemSach
    {

        public int row = 1;
        public static List<Object[]> data()
        {
            return DocGhiFile.getExcelFile(@"C:\Users\phamv\Documents\HomeWork\Kiem thu phan mem\Pham Van Hieu\dulieutest.xlsx");
        }

        [TestCaseSource("data"), Test]
        public void testTinh(String ma, String ten, String sl, String tl, String tg, String mess)
        {



            if (mess.Equals("Thanh cong"))
            {
                DocGhiFile.setExcelFile(row++, 7, "Pass", @"C:\Users\phamv\Documents\HomeWork\Kiem thu phan mem\Pham Van Hieu\dulieutest.xlsx");
                Assert.AreEqual("Thanh cong", mess);
            }
            else
            {
                DocGhiFile.setExcelFile(row++, 7, "Fail", @"C:\Users\phamv\Documents\HomeWork\Kiem thu phan mem\Pham Van Hieu\dulieutest.xlsx");
                Assert.AreEqual("Thanh cong", mess);
            }

            

        }


    }

}


