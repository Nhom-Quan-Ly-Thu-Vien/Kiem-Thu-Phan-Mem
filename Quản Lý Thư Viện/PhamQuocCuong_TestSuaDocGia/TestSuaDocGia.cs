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
    //[TestFixture]
    public class Test_SuaDocGia
    {
        public int row = 1;
        public static List<Object[]> data()
        {
            return DocGhiFileExcel.getExcelFile(@"E:\Git\Kiem-Thu-Phan-Mem\Testcase_SuaDocGia.xlsx");
        }

<<<<<<< HEAD
        [TestCaseSource("data"),Test]
=======
        //[TestCaseSource("data"),Test][STAThread]
>>>>>>> 5718827fa9d36f5c17043f0da7c9c6d58c958529
        public void Test(string madg, string tendg, string diachi, string sdt, string gt, string msg)
        {
            QuanLySach qls = new QuanLySach();
            qls.Show();
            ButtonTester btnDocGia = new ButtonTester("btnDocGia");
            btnDocGia[0].Click();

            string actmsg = "";
            ModalFormTester msgBox = new ModalFormTester();
            msgBox.ExpectModal("Info", delegate
            {
                MessageBoxTester mess = new MessageBoxTester("Info");
                actmsg = mess.Text;
                mess.ClickOk();
            });

            qls.setValue(madg, tendg, Convert.ToBoolean(gt.ToLower()), diachi, sdt.TrimEnd());

            ButtonTester btnSua = new ButtonTester("btnSuaDG");
            btnSua[0].Click();

            try
            {
                if (msg.Equals(actmsg))
                {
                    DocGhiFileExcel.setExcelFile(row++, 7, "PASS", @"E:\Git\Kiem-Thu-Phan-Mem\Testcase_SuaDocGia.xlsx");
                    Assert.AreEqual(actmsg, msg);
                }
                else
                {
                    Assert.Fail();
                }
            }
            catch (Exception)
            {
                DocGhiFileExcel.setExcelFile(row++, 7, "FAIL", @"E:\Git\Kiem-Thu-Phan-Mem\Testcase_SuaDocGia.xlsx");
                Assert.AreEqual(actmsg, msg);
            }
        }
    }
}
