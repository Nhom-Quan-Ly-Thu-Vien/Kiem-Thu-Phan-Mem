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

namespace Test_SuaDocGia
{
    [TestFixture] 
    class AnotherTest
    {
        
        public static Collection<Object[]> DataTest() {
            return DocGhiFileExcel.getExcelFile(@"E:\Kiểm thử\BTL\Copy of Testcase_SuaDocGia - Copy.xlsx");
        }

        public int row = 3;
        [TestCaseSource("DataTest")]
        public void Round1(string madg,string tendg,string diachi, string sdt, string gt,string msg) {
            try {
                
                QuanLySach qls = new QuanLySach();
                qls.Show();

                string mess = "";
                ModalFormTester mft = new ModalFormTester();
                mft.ExpectModal("Mã độc giả", delegate
                {
                    MessageBoxTester msbt = new MessageBoxTester("Mã độc giả");
                    mess = msbt.Text;
                    msbt.ClickOk();
                });

                ButtonTester btnSua = new ButtonTester("btnSuaDG");

                TextBoxTester txtMaDG = new TextBoxTester("txtMaDG");
                txtMaDG[0].Enter(DocGhiFileExcel.xlWorksheet.Cells[row,3].Value);

                btnSua.Click();
                if (msg.Equals(mess)) {
                    DocGhiFileExcel.setExcelFile(row++, 7, "PASS", @"E:\Kiểm thử\BTL\Copy of Testcase_SuaDocGia - Copy.xlsx");
                }
                else {
                    Assert.AreEqual(msg,mess);
                }
                qls.Close();
            }
            catch (Exception) {
                DocGhiFileExcel.setExcelFile(row++, 7, "FAIL", @"E:\Kiểm thử\BTL\Copy of Testcase_SuaDocGia - Copy.xlsx");
                Assert.Fail();
            }
        }

        [Test]
        public void Ressult() {
            System.Diagnostics.Process.Start(@"E:\Kiểm thử\BTL\Copy of Testcase_SuaDocGia - Copy.xlsx");
            Assert.Pass();
        }
    }
}
