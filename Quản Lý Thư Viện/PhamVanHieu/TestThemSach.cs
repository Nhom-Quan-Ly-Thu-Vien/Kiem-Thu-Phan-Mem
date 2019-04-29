using QuanLyThuVien;
using System;
using NUnit.Extensions.Forms; 
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

        public int row = 2;
        public static List<Object[]> data()
        {
            return DocGhiFile.getExcelFile(@"C:\Users\phamv\Documents\GitHub\Kiem-Thu-Phan-Mem\Kiem-Thu-Phan-Mem\dulieutest.xlsx");
        }

        [TestCaseSource("data"), Test]
        public void testThems(String ten, String sl, String tl, String tg, String mess)
        {

            QuanLySach qls = new QuanLySach();
            qls.Show();
            

            string actmsg = "";
            ModalFormTester msgBox = new ModalFormTester();
            msgBox.ExpectModal("Info", delegate
            {
                MessageBoxTester ms = new MessageBoxTester("Info");
                actmsg = ms.Text;
                ms.ClickOk();
            });

            qls.testDuLieu(ten,  sl,  tl,  tg);

            ButtonTester btnthem = new ButtonTester("btnThemSach");
            btnthem[0].Click();

            if (mess.Equals(actmsg))
            {
                DocGhiFile.setExcelFile(row++, 7, "Pass", @"C:\Users\phamv\Documents\GitHub\Kiem-Thu-Phan-Mem\Kiem-Thu-Phan-Mem\dulieutest.xlsx");
                Assert.AreEqual(actmsg, mess);
            }
            else
            {
                DocGhiFile.setExcelFile(row++, 7, "Fail", @"C:\Users\phamv\Documents\GitHub\Kiem-Thu-Phan-Mem\Kiem-Thu-Phan-Mem\dulieutest.xlsx");
                Assert.AreEqual(actmsg, mess);
            }

            qls.Close();

        }


    }

}


