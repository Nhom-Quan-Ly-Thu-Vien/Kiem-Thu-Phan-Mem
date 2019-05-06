using QuanLyThuVien;
using System;
using NUnit.Extensions.Forms;
using NUnit.Framework;
using System.Collections.Generic;




namespace Test
{
    [TestFixture]
    public class TestThemSach
    {
        //dòng bắt đầu ghi dữ liệu
        public int row = 2;
        //hàm lấy datatest từ file excel
        public static List<Object[]> data()
        {
            return DocGhi.getExcelFile(@"C:\Users\HP\Documents\TCs_Sua1..xlsx");
        }
        //lấy dữ liệu đẩy vào testcase
        [TestCaseSource("data"), Test]
        public void SuaRound1(String ten, String tg, String sl, String mess)
        {
            //khởi tạo đối tượng của form QuanLySach
            QuanLySach qls = new QuanLySach();
            //Hiện form
            qls.Show();
            //khởi tạo 1 biến để lấy kết quả từ messagebox 
            string actmsg = "";
            ModalFormTester msgBox = new ModalFormTester();
            //phương thức lấy giá trị của messagebox
            msgBox.ExpectModal("NOTE", delegate
            {
                MessageBoxTester ms = new MessageBoxTester("NOTE");
                actmsg = ms.Text;
                ms.ClickOk();
            });
            // set giá trị cho các trường sau khi lấy dữ liệu từ excel
            qls.dataTest(ten, tg,sl);
            //khởi tạo 1 button
            ButtonTester btnSua = new ButtonTester("btnSuaSach");
            //click vào button Sửa trên form 
            btnSua[0].Click();
            //so sách kết quả lấy từ messagebox với kết quả mong đợi
            if (mess.Equals(actmsg))
            {
                //ghi kết quả vào file excel
                DocGhi.setExcelFile(row++, 6, "Pass", @"C:\Users\HP\Documents\TCs_Sua1..xlsx");
                Assert.AreEqual(actmsg, mess);
            }
            else
            {
                DocGhi.setExcelFile(row++, 6, "Pending", @"C:\Users\HP\Documents\TCs_Sua1..xlsx");
                Assert.AreEqual(actmsg, mess);
            }
            //đóng form sau khi thực hiện xong
            qls.Close();
        }


        //lấy dữ liệu đẩy vào testcase
        [TestCaseSource("data"), Test]
        public void SuaRound2(String ten, String tg, String sl, String mess)
        {
            //khởi tạo đối tượng của form QuanLySach
            QuanLySach qls = new QuanLySach();
            //Hiện form
            qls.Show();
            //khởi tạo 1 biến để lấy kết quả từ messagebox 
            string actmsg = "";
            ModalFormTester msgBox = new ModalFormTester();
            msgBox.ExpectModal("NOTE", delegate
            {
                MessageBoxTester ms = new MessageBoxTester("NOTE");
                actmsg = ms.Text;
                ms.ClickOk();
            });
            // set giá trị cho các trường sau khi lấy dữ liệu từ excel
            qls.dataTest(ten, tg, sl);
            //khởi tạo 1 button
            ButtonTester btnSua = new ButtonTester("btnSuaSach");
            //click vào button Sửa trên form 
            btnSua[0].Click();
            //so sách kết quả lấy từ messagebox với kết quả mong đợi
            if (mess.Equals(actmsg))
            {
                //ghi kết quả vào file excel
                DocGhi.setExcelFile(row++, 7, "Pass", @"C:\Users\HP\Documents\TCs_Sua1..xlsx");
                Assert.AreEqual(actmsg, mess);
            }
            else
            {
                DocGhi.setExcelFile(row++, 7, "Pending", @"C:\Users\HP\Documents\TCs_Sua1..xlsx");
                Assert.AreEqual(actmsg, mess);
            }
            //đóng form sau khi thực hiện xong
            qls.Close();
        }
    }

}


