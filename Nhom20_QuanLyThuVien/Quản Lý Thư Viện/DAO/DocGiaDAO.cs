﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DTO;

namespace DAO
{
    public class DocGiaDAO
    {
        private static DocGiaDAO instance;

        public static DocGiaDAO Instance
        {
            get
            {
                if (instance == null) instance = new DocGiaDAO();
                return DocGiaDAO.instance;
            }
            set { DocGiaDAO.instance = value; }
        }

        public DocGiaDAO()
        {

        }

        public DataTable ShowDG()
        {
            string query = "Select * From DocGia";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            return data;
        }

        public DataTable TimKiemDG(String str)
        {
            String query = "select * from DocGia where MaDocGia like '%" + str + "%' or TenDocGia like N'%" + str + "%' or GioiTinh like N'%" + str + "%' or DiaChi like N'%" + str + "%' or SDT like '%" + str + "%'";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            return data;
        }

        public void ThemDG(DocGia docgia)
        {
            String query = "insert DocGia values(N'" + docgia.MaDg + "', N'"+docgia.TenDg+"', N'"+docgia.Gt+"', N'"+docgia.DiaChi+"', '"+docgia.Sdt+"')";

            DataProvider.Instance.ExecuteNonQuery(query);
        }

        public void SuaDG(DocGia docgia)
        {
            String query = "update DocGia set TenDocGia=N'" + docgia.TenDg + "', GioiTinh=N'" + docgia.Gt + "', DiaChi=N'" + docgia.DiaChi + "', SDT='" + docgia.Sdt + "' where MaDocGia=N'" + docgia.MaDg + "'";

            DataProvider.Instance.ExecuteNonQuery(query);
        }

        public void XoaDG(String str)
        {
            String query = "delete PhieuMuon where MaDocGia=N'" + str + "'";
            DataProvider.Instance.ExecuteNonQuery(query);

            String query1 = "delete DocGia where MaDocGia=N'" + str + "'";
            DataProvider.Instance.ExecuteNonQuery(query1);
        }

        public string GetTenDG(String str)
        {

            DataTable dt = new DataTable();
            String query = "SELECT TenDocGia FROM dbo.DocGia WHERE MaDocGia = '" + str + "'";
            dt = DataProvider.Instance.ExecuteQuery(query);
            return dt.Rows[0]["TenDocGia"].ToString();
        }
    }
}
