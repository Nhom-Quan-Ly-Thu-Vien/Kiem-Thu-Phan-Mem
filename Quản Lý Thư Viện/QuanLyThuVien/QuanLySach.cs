using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using BUS;
using DTO;
using System.Drawing.Printing;
using System.Drawing;

namespace QuanLyThuVien
{
    public partial class QuanLySach : Form
    {
        public QuanLySach()
        {
            InitializeComponent();
        }

     
        int row;
        private void QuanLySach_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the '_QuanLyThuVien1_0DataSet1.DocGia' table. You can move, or remove it, as needed.
            //this.docGiaTableAdapter1.Fill(this._QuanLyThuVien1_0DataSet1.DocGia);
            // TODO: This line of code loads data into the '_QuanLyThuVien1_0DataSet1.Sach' table. You can move, or remove it, as needed.
            //this.sachTableAdapter.Fill(this._QuanLyThuVien1_0DataSet1.Sach);
            // TODO: This line of code loads data into the '_QuanLyThuVien1_0DataSet1.PhieuMuon' table. You can move, or remove it, as needed.
            //this.phieuMuonTableAdapter.Fill(this._QuanLyThuVien1_0DataSet1.PhieuMuon);
            dataGridView1.DataSource = PhieuMuonBUS.Instance.LoadPhieu();
            //dataGridView1.Hide();
            dtgQuanLySach.DataSource = SachBUS.Instance.ShowSach();
            dtgDocGia.DataSource = DocGiaBUS.Instance.ShowDG();
            dgvPhieuMuon.DataSource = PhieuMuonBUS.Instance.LoadPhieuMuon();

            //đổ dữ liệu lên combobox Thể loại sách
            cmbTheLoai.DataSource = SachBUS.Instance.GetCatory();
            cmbTheLoai.DisplayMember = "TenTheLoai";
            cmbTheLoai.ValueMember = "MaTheLoai";
            cmbTheLoai.SelectedIndex = -1;

            //đổ dữ liệu lên combobox Tên sách
            cmbTenSachPhieu.DataSource = SachBUS.Instance.ShowSach();
            cmbTenSachPhieu.DisplayMember = "TenSach";
            cmbTenSachPhieu.ValueMember = "MaSach";

            //do du lieu len combobox Ten Doc Gia
            cmbTenDGPhieu.DataSource = DocGiaBUS.Instance.ShowDG();
            cmbTenDGPhieu.DisplayMember = "TenDocGia";
            cmbTenDGPhieu.ValueMember = "MaDocGia";

            //đổ dữ liệu lên combobox phiếu trả
            cboDGPT.DataSource = PhieuMuonBUS.Instance.GetMaDG();
            cboDGPT.ValueMember = "MaDocGia";


         
            //Tăng mã tự động          
            tangMa("DG",dtgDocGia,txtMaDG);
            tangMa("MS", dtgQuanLySach,txtMaSach);
            tangMa("MP", dataGridView1, txtmaPhieu);
            txtMaSach.Enabled = false;
            txtMaDG.Enabled = false;
            label28.Hide();
            btnXoaPT.Hide();
            //tô màu sach co so luong =0
            for (int i = 0; i < dtgQuanLySach.RowCount; i++)
            {
                if (int.Parse(dtgQuanLySach.Rows[i].Cells[3].Value.ToString()) == 0)
                    dtgQuanLySach.Rows[i].Cells[3].Style.BackColor = Color.Red;
            }
          
        }
        //auto tawng mã

        
        private void tangMa(string ch,DataGridView dataGridView,TextBox textBox) 
        {
            if (dataGridView.Rows.Count == 0)
                textBox.Text = ch + "00";
            else
            {
                string str = (dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[0].Value.ToString()).Remove(0, 2);
                Int32 temp = Int32.Parse(str);
                textBox.Text = ch + (temp + 1 < 10 ? "0" : "") + (temp + 1);
            }
        }

        //show panel,form
        #region
        private void btnDocGia_Click(object sender, EventArgs e)
        {
            panDocGia.Show();
            panQuanLySach.Hide();
            tabQuanLyPhieu.Hide();
            panAbout.Hide();
        }

        private void btnExit1_Click(object sender, EventArgs e)
        {
            DialogResult drl = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (drl == DialogResult.Yes) Application.Exit();
        }


        private void btnQuanLySach_Click(object sender, EventArgs e)
        {
            panQuanLySach.Show();
            panDocGia.Hide();
            tabQuanLyPhieu.Hide();
            panAbout.Hide();
        }

        private void btnQuanLyPhieu_Click(object sender, EventArgs e)
        {
            panQuanLySach.Hide();
            panDocGia.Hide();
            tabQuanLyPhieu.Show();
            panAbout.Hide();

            tangMa("MP",dataGridView1,txtmaPhieu);
            tabQuanLyPhieu.SelectedTab = tabQuanLyPhieu.TabPages[0];
            cmbTenDGPhieu.Text = "";
            cmbTenSachPhieu.SelectedIndex = 0;
          
            lstSachMuon.Items.Clear();
            txtMaDGPhieu.Text = "";
            lblSLM.Hide();
            txtSLMuon.Hide();
            lblHienCo.Hide();
            //cboDGPT.SelectedIndex = -1;

            QuanLySach_Load(sender, e);
            dgvPhieuMuon.Hide();
            dgvPhieuTra.Hide();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            panQuanLySach.Hide();
            panDocGia.Hide();
            tabQuanLyPhieu.Hide();
            panAbout.Show();
        }
        #endregion
        //Code phần độc giả
        #region
        private void btnThemDG_Click(object sender, EventArgs e) //Thêm DG
        {
            try
            {
              

                if (txtTenDG.Text == "")
                    MessageBox.Show("Nhập tên độc giả.");
                else if (rdoNam.Checked == false && rdoNu.Checked == false)
                    MessageBox.Show("Chọn giới tính.");
                else if (txtDiaChi.Text == "")
                    MessageBox.Show("Nhập địa chỉ.");
                else
                {
                    String gioitinh;
                    if (rdoNam.Checked) gioitinh = "Nam";
                    else gioitinh = "Nữ";
                    Double value;
                    if (Double.TryParse(txtSDT.Text, out value) == false)
                        MessageBox.Show("Số điện thoại phải là số.");
                    else if (txtSDT.TextLength < 10 || txtSDT.TextLength > 11)
                        MessageBox.Show("Số điện thoại gồm 10 hoặc 11 số.");
                    else
                    {
                        Int32 flag = 0;
                        for (int i = 0; i < dtgDocGia.Rows.Count; i++)
                        {
                            string tenDG = dtgDocGia.Rows[i].Cells[1].Value.ToString().ToLower();
                            string sdt = dtgDocGia.Rows[i].Cells[4].Value.ToString().ToLower();
                            if (txtTenDG.Text.ToLower() == tenDG && txtSDT.Text.ToLower() == sdt)
                            {
                                MessageBox.Show("Độc giả đã có rồi.");
                                flag = 1;
                            }

                        }
                        if(flag==0)
                        {
                            DocGia docgia = new DocGia(txtMaDG.Text, txtTenDG.Text, gioitinh, txtDiaChi.Text, txtSDT.Text);
                            DocGiaBUS.Instance.ThemDG(docgia);

                            QuanLySach_Load(sender, e);
                            MessageBox.Show("Thêm thành công.");
                            tangMa("DG", dtgDocGia, txtMaDG);
                        }
                            
                    }
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Trùng mã độc giả.");
            }
        }

        public void setValue(string madg, string tendg, bool gt, string diachi, string sdt) //Function for Testing
        {
            txtMaDG.Text = madg;
            txtTenDG.Text = tendg;
            if (gt == true)
            {
                rdoNam.Checked = true;
            }
            else { rdoNu.Checked = true; }
            txtDiaChi.Text = diachi;
            txtSDT.Text = sdt;
        }

        private void btnSuaDG_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTenDG.Text == "" && rdoNam.Checked == false && rdoNu.Checked == false && txtDiaChi.Text == "" && txtSDT.Text == "")
                    MessageBox.Show("Edit Unsuccessfully", "Info");
                else
                {
                    String gioitinh;
                    if (rdoNam.Checked) gioitinh = "Nam";
                    else gioitinh = "Nữ";

                    Double value;
                    if (Double.TryParse(txtSDT.Text, out value) == false)
                        MessageBox.Show("Edit Unsuccessfully", "Info");
                    else if (txtSDT.TextLength < 10 || txtSDT.TextLength > 11)
                        MessageBox.Show("Edit Unsuccessfully", "Info");
                    else
                    {
                        DocGia docgia = new DocGia(txtMaDG.Text, txtTenDG.Text, gioitinh, txtDiaChi.Text, txtSDT.Text);
                        DocGiaBUS.Instance.SuaDG(docgia);
                        QuanLySach_Load(sender, e);

                        MessageBox.Show("Edit Successfully", "Info");
                    }
                }
            }
            catch(NullReferenceException)
            {
                MessageBox.Show("Edit Unsuccessfully", "Info");
            }
        }

        private void btnXoaDG_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dlr = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dlr == DialogResult.Yes)
                {
                    if (txtTenDG.Text == "" && rdoNam.Checked == false && rdoNu.Checked == false && txtDiaChi.Text == "" && txtSDT.Text == "")
                        MessageBox.Show("Chọn độc giả.");
                    else
                    {
                        DocGiaBUS.Instance.XoaDG(txtMaDG.Text);
                        QuanLySach_Load(sender, e);
                    }
                }
            }
            catch
            {
                return;
            }
        }

        private void btnLapPhieuMuon_Click(object sender, EventArgs e)
        {
            if (txtTenDG.Text == "")
                MessageBox.Show("Vui lòng chọn độc giả.");
            else
            {
                panQuanLySach.Hide();
                panDocGia.Hide();
                tabQuanLyPhieu.Show();

                txtMaDGPhieu.Text = txtMaDG.Text;
                cmbTenDGPhieu.SelectedValue = txtMaDGPhieu.Text;
                tabQuanLyPhieu.SelectedTab = tabQuanLyPhieu.TabPages[1];
            }
            
            
        }

        private void txtTimKiemDocGia_TextChanged(object sender, EventArgs e)
        {
            String str = txtTimKiemDocGia.Text;
            dtgDocGia.DataSource = DocGiaBUS.Instance.TimKiemDG(str);
        }

        private void dtgDocGia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                row = e.RowIndex;

                txtMaDG.Enabled = false;
                txtMaDG.Text = dtgDocGia.Rows[row].Cells[0].Value.ToString();
                txtTenDG.Text = dtgDocGia.Rows[row].Cells[1].Value.ToString();

                String gioitinh = dtgDocGia.Rows[row].Cells[2].Value.ToString(); ;
                if (gioitinh == "Nam") rdoNam.Checked = true;
                else rdoNu.Checked = true;

                txtDiaChi.Text = dtgDocGia.Rows[row].Cells[3].Value.ToString();
                txtSDT.Text = dtgDocGia.Rows[row].Cells[4].Value.ToString();
            }
            catch
            {
                return;
            }
        }

        private void panDocGia_MouseClick(object sender, MouseEventArgs e)
        {
            tangMa("DG",dtgDocGia,txtMaDG);
            txtMaDG.Enabled = false;
            txtTenDG.Text = "";
            rdoNam.Checked = false;
            rdoNu.Checked = false;
            txtDiaChi.Text = "";
            txtSDT.Text = "";
        }
        #endregion
        
        //Code Quản lý sách
        #region
        private void dtgQuanLySach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtMaSach.Enabled = false;
                row = e.RowIndex;
                txtMaSach.Text = dtgQuanLySach.Rows[row].Cells[0].Value.ToString();
                txtTenSach.Text = dtgQuanLySach.Rows[row].Cells[1].Value.ToString();
                cmbTheLoai.Text = dtgQuanLySach.Rows[row].Cells[2].Value.ToString();
                txtSoLuong.Text = dtgQuanLySach.Rows[row].Cells[3].Value.ToString();
                txtTacGia.Text = dtgQuanLySach.Rows[row].Cells[4].Value.ToString();
            }
            catch
            {
                return;
            }

        }

        private void btnThemSach_Click(object sender, EventArgs e)
        {
            
            if (txtTenSach.TextLength == 0) MessageBox.Show("Unsuccessfull", "Info");
            else if (txtSoLuong.TextLength == 0) MessageBox.Show("Unsuccessfull", "Info");
            else if (txtSoLuong.TextLength >5 ) MessageBox.Show("Unsuccessfull", "Info");
            else if (txtTacGia.TextLength == 0) MessageBox.Show("Unsuccessfull", "Info");
            else if (txtTacGia.TextLength > 30) MessageBox.Show("Unsuccessfull", "Info");
            else if (txtTenSach.TextLength > 30) MessageBox.Show("Unsuccessfull", "Info");
            else if (cmbTheLoai.SelectedItem==null) MessageBox.Show("Unsuccessfull", "Info");
            
            else
            {
                try
                {
                    
                    Int32 flag = 0;
                    for (int i = 0; i < dtgQuanLySach.Rows.Count; i++)
                    {
                        string tenSach = dtgQuanLySach.Rows[i].Cells[1].Value.ToString().ToLower();
                        string tacgia = dtgQuanLySach.Rows[i].Cells[4].Value.ToString().ToLower();
                        if (txtTenSach.Text.ToLower() == tenSach && txtTacGia.Text.ToLower() == tacgia)
                        {
                            MessageBox.Show("Unsuccessfull", "Info");
                            flag = 1;
                            break;
                        }

                    }
                    if(flag==0)
                    {
                        
                        Sach sach = new Sach(txtMaSach.Text, txtTenSach.Text, (cmbTheLoai.SelectedValue.ToString()), int.Parse(txtSoLuong.Text), txtTacGia.Text);
                        MessageBox.Show("Successfull", "Info");
                        SachBUS.Instance.AddBook(sach);
                        QuanLySach_Load(sender, e);
                        tangMa("MS", dtgQuanLySach, txtMaSach);
                        
                    }
                        
                }
                catch (SqlException)
                {
                    MessageBox.Show("Unsuccessfull", "Info");
                }
                catch(FormatException)
                {
                    MessageBox.Show("Unsuccessfull", "Info");
                }

            }
            
        }
      
        private void btnSuaSach_Click(object sender, EventArgs e)
        {
            if (txtTenSach.TextLength == 0) MessageBox.Show("Unsuccessfully", "Info");
            else if (txtSoLuong.TextLength == 0) MessageBox.Show("Unsuccessfully", "Info");
            else if (txtSoLuong.TextLength != 0)
            {
                try
                {
                    int.Parse(txtSoLuong.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Unsuccessfully", "Info");
                }
            }
            else if (txtTacGia.TextLength == 0) MessageBox.Show("Unsuccessfully", "Info");
            else
            {
                try
                {

                    Sach sach = new Sach(txtMaSach.Text, txtTenSach.Text, cmbTheLoai.SelectedValue.ToString(), int.Parse(txtSoLuong.Text), txtTacGia.Text);

                    SachBUS.Instance.UpdateBook(sach);

                    QuanLySach_Load(sender, e);
                    MessageBox.Show("Successfull", "Info");
                }
                catch (FormatException)
                {
                    MessageBox.Show("Unsuccessfully", "Info");
                }
            }          
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dlr = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if(dlr == DialogResult.Yes)
                {
                    string ma = txtMaSach.Text;
                    SachBUS.Instance.DeleteBook(ma);
                    QuanLySach_Load(sender, e);
                }
                
            }
            catch 
            {
                return;
            }           
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            
            string dk = txtTimKiem.Text;
            dtgQuanLySach.DataSource= SachBUS.Instance.LookBook(dk);
        }

        private void panQuanLySach_Click(object sender, EventArgs e)
        {
            txtMaSach.Enabled = false;
            tangMa("MS", dtgQuanLySach,txtMaSach);        
            txtTenSach.Text = "";
            txtSoLuong.Text = "";
            txtTacGia.Text = "";                
        }

        public void testDuLieu(String ten,String sl,String tl,String tg) { //code for testing
        

            txtTenSach.Text = ten;
            txtSoLuong.Text = sl;
            cmbTheLoai.Text = tl;
            txtTacGia.Text = tg;  
        }
        #endregion

        //Quản Lý Phiếu Mượn Trả
        #region
        private void txtTKPhieu_TextChanged(object sender, EventArgs e)
        {
           dgvPhieuMuon.DataSource= PhieuMuonBUS.Instance.LookPhieuMuon(txtTKPhieu.Text);

           dgvPhieuTra.DataSource= PhieuMuonBUS.Instance.LookPhieuTra(txtTKPhieu.Text);
        }
        //Xóa Bên Phiếu Mượn

        private void cmbTenSachPhieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblHienCo.Text = "Hiện Có:";
            lblSLM.Show();
            txtSLMuon.Show();
            lblHienCo.Show();
            String str = dtgQuanLySach.Rows[dtgQuanLySach.Rows.Count - 1].Cells[0].Value.ToString();
            lblHienCo.Text += SachBUS.Instance.getSLuong(cmbTenSachPhieu.SelectedValue.ToString() == "System.Data.DataRowView" ? str : cmbTenSachPhieu.SelectedValue.ToString());
            
        }

      
        //Phieu Muon
        #region
        private void btnShowPM_Click(object sender, EventArgs e)
        {
            dgvPhieuTra.Hide();
            dgvPhieuMuon.Show();
            dgvPhieuMuon.DataSource = PhieuMuonBUS.Instance.LoadPhieuMuon();
            tangMa("MP", dataGridView1, txtmaPhieu);
            label28.Show();
            label28.Text = "Phiếu Mượn";
            btnXoaPT.Hide();
        }

        private void btnThemPhieu_Click_1(object sender, EventArgs e)
        {
            
            int slco= int.Parse(SachBUS.Instance.getSLuong(cmbTenSachPhieu.SelectedValue.ToString()));
            int slmuon;
            if (txtMaDGPhieu.TextLength == 0) MessageBox.Show("Mã Độc Giả Trống.");

            else if (cmbTenSachPhieu.SelectedIndex == -1) MessageBox.Show("Vui lòng chọn sách");

            else if (txtSLMuon.TextLength == 0)
            {               
                MessageBox.Show("Vui lòng nhập số lượng.");            
            }
            else if (int.TryParse(txtSLMuon.Text, out slmuon) == false)
                MessageBox.Show("Số lượng mượn phải là số.");
            else
            {
                slmuon = int.Parse(txtSLMuon.Text);
                if (slmuon > slco)//Kiểm tra số lượng sách trong kho
                {
                   MessageBox.Show("Số lượng sách hiện không đủ");
                }
               
                else
                {
                    try
                    {
                        tangMa("MP", dataGridView1, txtmaPhieu);
                        row = lstSachMuon.Items.Count;
                        int flag = 0;
                        for (int i = 0; i < row; i++)
                        {
                            if (cmbTenSachPhieu.SelectedValue.ToString() == lstSachMuon.Items[i].SubItems[0].Text)
                            {
                                lstSachMuon.Items[i].SubItems[2].Text = (int.Parse(lstSachMuon.Items[i].SubItems[2].Text) + int.Parse(txtSLMuon.Text)).ToString();
                                flag = 1;
                            }
                        }

                        if (flag == 0)
                        {
                            lstSachMuon.Items.Add(cmbTenSachPhieu.SelectedValue.ToString());
                            lstSachMuon.Items[row].SubItems.Add(cmbTenSachPhieu.Text);
                            lstSachMuon.Items[row].SubItems.Add(txtSLMuon.Text);
                        }
                        
                        lblSLM.Hide();
                        txtSLMuon.Hide();
                        txtSLMuon.Text = "";
                    }
                    catch
                    {
                        return;
                    }
                 }
            
            }
           

        }

        private void btnXoaListSach_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lstSachMuon.Items)
            {
                if (item.Selected)
                    item.Remove();
            }
        }

        private void btnLapPhieu_Click_1(object sender, EventArgs e)
        {
            
            
                if (txtMaDGPhieu.TextLength == 0) MessageBox.Show("Mã Độc Giả Trống.");

                else if (cmbTenSachPhieu.SelectedIndex == -1) MessageBox.Show("Vui lòng chọn sách");

                else if (lstSachMuon.Items.Count == 0)
                {
                    MessageBox.Show("Vui lòng thêm sách mượn.");
                }

                else
                {
                    for (int i = 0; i < lstSachMuon.Items.Count; i++)
                    {
                        PhieuMuon phieu = new PhieuMuon(txtmaPhieu.Text, txtMaDGPhieu.Text, lstSachMuon.Items[i].SubItems[0].Text, int.Parse(lstSachMuon.Items[i].SubItems[2].Text), dpkNgayMuon.Value);
                        PhieuMuonBUS.Instance.AddPhieuMuon(phieu);
                        SachBUS.Instance.UpdateSLBook(int.Parse(SachBUS.Instance.getSLuong(lstSachMuon.Items[i].SubItems[0].Text)) - int.Parse(lstSachMuon.Items[i].SubItems[2].Text), lstSachMuon.Items[i].SubItems[0].Text);

                    }
                    
                    MessageBox.Show("Lập Thành Công.");
                    
                    MessageBox.Show("Thực hiện in phiếu mượn.");

                    PrintDialog PrintDialog = new PrintDialog();
                    PrintDocument PrintDocument = new PrintDocument();
                    PrintDialog.Document = PrintDocument; //Chuyển dữ liệu từ printDocument sang ptintPriviewDialog

                    //Nhan su kien receipt ben duoi va in ra hoa don da dc dinh dang ben duoi
                    PrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(CreateReceipt);

                    //Chọn máy in
                    DialogResult result = PrintDialog.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        PrintDocument.Print();
                    }

                    lstSachMuon.Items.Clear();
                    QuanLySach_Load(sender, e);
                }          
        }

        private void CreateReceipt(object sender, PrintPageEventArgs e)
        {
            Graphics graphic = e.Graphics;
            Font font = new Font("Courier New", 12);
            float FontHeight = font.GetHeight();
            int startX = 10;
            int startY = 10;
            int offset = 40;

            graphic.DrawString("QUẢN LÝ THƯ VIỆN", new Font("Courier New", 18), new SolidBrush(Color.Black), startX, startY);
            graphic.DrawString("PHIẾU MƯỢN", new Font("Courier New", 18), new SolidBrush(Color.Black), startX + 30, startY + 30);
            offset = offset + (int)FontHeight + 20;
            graphic.DrawString("Tên độc giả: ".PadRight(5) + cmbTenDGPhieu.Text, font, new SolidBrush(Color.Black), startX, startY + offset);
            graphic.DrawString("Mã phiếu: ".PadRight(5) + txtmaPhieu.Text, font, new SolidBrush(Color.Black), startX + 300, startY + offset);
            offset = offset + (int)FontHeight + 10;
            string top = "Tên Sách".PadRight(29) + "Số Lượng";
            graphic.DrawString(top, font, new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)FontHeight; //make the spacing consistent
            graphic.DrawString("--------------------------------------------", font, new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)FontHeight + 5; //make the spacing consistent

            for (Int32 i = 0; i < lstSachMuon.Items.Count; i++)
            {
                graphic.DrawString(lstSachMuon.Items[i].SubItems[1].Text, font, new SolidBrush(Color.Black), startX, startY + offset);
                graphic.DrawString(lstSachMuon.Items[i].SubItems[2].Text, font, new SolidBrush(Color.Black), startX + 300, startY + offset);

                offset = offset + (int)FontHeight + 5; //Tao khoang cach giua cac cot           
            }

            offset = offset + 20; //Tao khoang cach giua phan tren va duoi
            graphic.DrawString("Ngày.... tháng.... năm 20..", new Font("Courier New", 12, FontStyle.Bold), new SolidBrush(Color.Black), startX + 530, startY + offset);
            graphic.DrawString("Người thu ký", new Font("Courier New", 12, FontStyle.Bold), new SolidBrush(Color.Black), startX + 610, startY + offset + 20);
        }

      
        #endregion
        //Phiếu Trả

        #region

        private void btnShowPT_Click(object sender, EventArgs e)
        {
            dgvPhieuMuon.Hide();
            dgvPhieuTra.Show();
            dgvPhieuTra.DataSource = PhieuMuonBUS.Instance.LoadPhieuTra();
            label28.Show();
            label28.Text = "Phiếu Trả";
            btnXoaPT.Show();
        }

        private void cmbTenDGPhieu_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                txtMaDGPhieu.Text = cmbTenDGPhieu.SelectedValue.ToString();
                lstSachMuon.Items.Clear();
                txtSLMuon.Text = "";
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Mã DG trống.");
            }
            
        }

        private void cboDGPT_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvSachTra.DataSource = PhieuMuonBUS.Instance.GetSachTra(cboDGPT.Text);

            if (cboDGPT.SelectedValue.ToString() == "System.Data.DataRowView")
            {
                return;
            }
            else
            {
                txtTenDGPT.Text = DocGiaBUS.Instance.GetTenDG(cboDGPT.SelectedValue.ToString());
            } 
        }
       
        private void btnXoaSachTra_Click(object sender, EventArgs e)
        {
            try
            {
                int currentIndex = dgvSachTra.CurrentCell.RowIndex;
                dgvSachTra.Rows.RemoveAt(currentIndex);
            }
            catch 
            {
                return;
            }
            
            
        }

        private void btnLapPhieuTra_Click(object sender, EventArgs e)
        {
          
            DateTime ngaytra = dpkNgayTra.Value;
            if (cboDGPT.SelectedIndex == -1) MessageBox.Show("Chọn độc giả cần lập phiếu.");
            else
            {
                for (int i = 0; i < dgvSachTra.Rows.Count; i++)
                {
                    string sltra = dgvSachTra.Rows[i].Cells[3].Value.ToString();
                    string ms = dgvSachTra.Rows[i].Cells[1].Value.ToString();

                    PhieuMuonBUS.Instance.AddPhieuTra(ngaytra, ms, cboDGPT.Text);

                    SachBUS.Instance.UpdateSLBook(int.Parse(SachBUS.Instance.getSLuong(ms)) + int.Parse(sltra),ms);
                       
                }
                
                MessageBox.Show("Lập phiếu trả thành công.");

                MessageBox.Show("Thực hiện in phiếu trả.");

                PrintDialog PrintDialog = new PrintDialog();
                PrintDocument PrintDocument = new PrintDocument();
                PrintDialog.Document = PrintDocument; //Chuyển dữ liệu từ printDocument sang ptintPriviewDialog

                //Nhan su kien receipt ben duoi va in ra hoa don da dc dinh dang ben duoi
                PrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(CreateReceipt1);

                //Chọn máy in
                DialogResult result = PrintDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    PrintDocument.Print();
                }
                dgvSachTra.DataSource = PhieuMuonBUS.Instance.GetSachTra(cboDGPT.Text);
                QuanLySach_Load(sender, e);
            }

            
        }

        private void CreateReceipt1(object sender, PrintPageEventArgs e)
        {
            Graphics graphic = e.Graphics;
            Font font = new Font("Courier New", 12);
            float FontHeight = font.GetHeight();
            int startX = 10;
            int startY = 10;
            int offset = 40;

            graphic.DrawString("QUẢN LÝ THƯ VIỆN", new Font("Courier New", 18), new SolidBrush(Color.Black), startX, startY);
            graphic.DrawString("PHIẾU TRẢ", new Font("Courier New", 18), new SolidBrush(Color.Black), startX + 30, startY + 30);
            offset = offset + (int)FontHeight + 20;
            graphic.DrawString("Tên độc giả: ".PadRight(5) + txtTenDGPT.Text, font, new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)FontHeight + 10;
            string top = "Tên Sách".PadRight(29) + "Số Lượng";
            graphic.DrawString(top, font, new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)FontHeight; //make the spacing consistent
            graphic.DrawString("--------------------------------------------", font, new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)FontHeight + 5; //make the spacing consistent

            for (Int32 i = 0; i < dgvSachTra.Rows.Count; i++)
            {
                graphic.DrawString(dgvSachTra.Rows[i].Cells[2].Value.ToString(), font, new SolidBrush(Color.Black), startX, startY + offset);
                graphic.DrawString(dgvSachTra.Rows[i].Cells[3].Value.ToString(), font, new SolidBrush(Color.Black), startX + 300, startY + offset);

                offset = offset + (int)FontHeight + 5; //Tao khoang cach giua cac cot           
            }

            offset = offset + 20; //Tao khoang cach giua phan tren va duoi
            graphic.DrawString("Ngày.... tháng.... năm 20..", new Font("Courier New", 12, FontStyle.Bold), new SolidBrush(Color.Black), startX + 530, startY + offset);
            graphic.DrawString("Người thu ký", new Font("Courier New", 12, FontStyle.Bold), new SolidBrush(Color.Black), startX + 610, startY + offset + 20);
        }

        //Xóa Bên Phiếu Trả
        private void btnXoaPT_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dlr = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if(dlr == DialogResult.Yes)
                {
                    int currentIndex = dgvPhieuTra.CurrentCell.RowIndex;
                    string mp = dgvPhieuTra.Rows[currentIndex].Cells[0].Value.ToString();
                    string mdg2 = dgvPhieuTra.Rows[currentIndex].Cells[1].Value.ToString();
                    string ms2 = dgvPhieuTra.Rows[currentIndex].Cells[2].Value.ToString();
                    PhieuMuonBUS.Instance.DeletePhieu(mp, mdg2, ms2);
                    dgvPhieuTra.DataSource = PhieuMuonBUS.Instance.LoadPhieuTra();
                }
                
            }
            catch 
            {
                return;
            }
        }

        private void btnPhieuTra_Click(object sender, EventArgs e)
        {
            try
            {
                int currenIndex = dgvPhieuMuon.CurrentCell.RowIndex;
                cboDGPT.Text = dgvPhieuMuon.Rows[currenIndex].Cells[1].Value.ToString();
                tabQuanLyPhieu.SelectedTab = tabQuanLyPhieu.TabPages[2];
                txtMpTra.Text = dgvPhieuMuon.Rows[currenIndex].Cells[0].Value.ToString(); 
            }
            catch
            {
                return;
            }
        }

        private void dgvSachTra_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                row = e.RowIndex;
                txtMpTra.Text = dgvSachTra.Rows[row].Cells[0].Value.ToString();
            }
          catch
            {
                return;
            }
        }
        #endregion
        #endregion
    }
}
