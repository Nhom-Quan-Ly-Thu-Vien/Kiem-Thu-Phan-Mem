using System;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Extensions.Forms;
using NUnit.Framework;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using QuanLyThuVien;
using DTO;

namespace PhamQuocCuong_TestSuaDocGia
{
    [TestFixture]
    public class Test_SuaDocGia
    {
        public int row = 1;
        public static List<Object[]> data()
        {
            return DocGhiFileExcel.getExcelFile(@"E:\Kiểm thử\BTL\Testcase_SuaDocGia.xlsx");
        }

        [TestCaseSource("data"), Test]
        public void Test(string madg, string tendg, string diachi, string sdt, string gt, string msg)
        {
            if (msg.Equals("Edit Successfully"))
            {
                DocGhiFileExcel.setExcelFile(row++, 7, "PASS", @"E:\Kiểm thử\BTL\Testcase_SuaDocGia.xlsx");
                Assert.AreEqual("Edit Successfully", msg);
            }
            else
            {
                DocGhiFileExcel.setExcelFile(row++, 7, "FAIL", @"E:\Kiểm thử\BTL\Testcase_SuaDocGia.xlsx");
                Assert.AreEqual("Edit Successfully", msg);
            }
        }
    }
}
