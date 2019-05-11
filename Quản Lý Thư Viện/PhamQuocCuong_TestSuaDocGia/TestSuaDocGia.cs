using System;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Extensions.Forms; //Cần để viết test
using NUnit.Framework; //Cần để viết test
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using QuanLyThuVien;
using DTO;

namespace PhamQuocCuong_TestSuaDocGia
{
    [TestFixture] //Sử dụng để thông báo chứa các Testcases ở đây
    public class Test_SuaDocGia
    {
        public int row = 1;
        public static List<Object[]> data() { //Trả về các đối tượng đọc từ file Excel
            return DocGhiFileExcel.getExcelFile(@"E:\Git\Kiem-Thu-Phan-Mem\Testcase_SuaDocGia.xlsx");
        }
        [TestCaseSource("data"), Test]
        public void Test(string madg, string tendg, string diachi, string sdt, string gt, string msg)
        {
            //Tạo đối tượng của Form
            QuanLySach qls = new QuanLySach();
            qls.Show();
            ButtonTester btnDocGia = new ButtonTester("btnDocGia");
            btnDocGia[0].Click();
            //Lấy thông tin từ MessageBox
            string actmsg = "";
            ModalFormTester msgBox = new ModalFormTester();
            msgBox.ExpectModal("Info", delegate {
                MessageBoxTester mess = new MessageBoxTester("Info");
                actmsg = mess.Text;
                mess.ClickOk(); //Click MessageBox
            });
            //Truyền giá trị vào hàm setValue() tạo bên QuanLySach.cs
            qls.setValue(madg, tendg, Convert.ToBoolean(gt.ToLower()), diachi, sdt.Trim());
            //Tạo Button Sửa độc giả rồi click
            ButtonTester btnSua = new ButtonTester("btnSuaDG");
            btnSua[0].Click();
            //So sánh kết quả và ghi vào file Excel
            try {
                if (msg.Equals(actmsg)) {
                    DocGhiFileExcel.setExcelFile(row++, 7, "PASS", @"E:\Git\Kiem-Thu-Phan-Mem\Testcase_SuaDocGia.xlsx");
                    Assert.AreEqual(actmsg, msg);
                }
                else {
                    Assert.Fail(); //Phát hiện các trường hợp Fail
                }
            } catch (Exception) {
                DocGhiFileExcel.setExcelFile(row++, 7, "PENDING", @"E:\Git\Kiem-Thu-Phan-Mem\Testcase_SuaDocGia.xlsx");
                Assert.AreEqual(actmsg, msg);
            }
        }
    }
}