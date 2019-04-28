using System;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Extensions.Forms;
using NUnit.Framework;
//using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
using QuanLyThuVien;


namespace Test_SuaDocGia
{
    [TestFixture]
    class Test
    {
        QuanLySach qlsForm;
        Microsoft.Office.Interop.Excel.Application exFile;
        _Workbook exBook;
        _Worksheet exSheet;
        Range range;

        //[Test]
        //1
        private void OpenExcelFile() {
            string path = @"E:\Kiểm thử\BTL\Testcase_SuaDocGia - Copy.xlsx";
            exFile = new Microsoft.Office.Interop.Excel.Application();
            exBook = exFile.Workbooks.Open(path);
            exSheet = exBook.Worksheets["Sheet1"];
            exSheet.Activate();
            range=exSheet.Range["D6:J18"];
        }
        //2
        private void OpenForm() {
            qlsForm = new QuanLySach();
            qlsForm.Show();
        }
        //3
        private void OpenPanelQLDocGia() {
            ButtonTester btnDocGia = new ButtonTester("btnDocGia");
            btnDocGia.Click();
        }
        //4
        private bool runTestInRow(int row) {
            string actMaDG = null, actTenDG = null, actDiaChi = null, actSDT = null;

            TextBoxTester txtMaDG = new TextBoxTester("txtMaDG");
            ButtonTester btnSua = new ButtonTester("btnSuaDG");

            //Lấy thông báo của MessageBox nếu hiện lên
            ModalFormTester msbBox = new ModalFormTester();
            msbBox.ExpectModal("Mã độc giả", delegate {
                MessageBoxTester msg = new MessageBoxTester("Mã độc giả");
                actMaDG = msg.Text;
                msg.ClickOk();
            });

            txtMaDG[0].Enter(range.Cells[row,1].Value);
            btnSua[0].Click();

            if (actMaDG == range.Cells[row, 6].Value)
            {
                range.Cells[row, 7].value2 = "PASS";
                return true;
            }
            else {
                range.Cells[row, 7].value2 = "FAIL";
                return false;
            }
        }
        //5
        private void CloseForm() {
            qlsForm.Close();
        }
        //6
        private void SaveAndCloseExcelFile() {
            exBook.Save();
            exBook.Close();
            exFile.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(exBook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(exFile);
        }

        //RunTest
        [Test]
        public void RunTest() { 
            OpenExcelFile();
            OpenForm();
            int row = range.Rows.Count;
            OpenPanelQLDocGia();
            int pass = 0, fail = 0;
            for(int i=0;i<row;i++){
                if (runTestInRow(i))
                    pass++;
                else
                    fail++;
            }
            //Console.WriteLine("Pass: " + pass + "/" + row + "=> Pass " + ((float)pass / row) * 100 + "%");
            //Console.WriteLine("Fail: " + fail + "/" + row + "=> Fail " + ((float)fail / row) * 100 + "%");

            CloseForm();
            SaveAndCloseExcelFile();
        }
    }
}
