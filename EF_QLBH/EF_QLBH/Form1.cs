using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EF_QLBH
{
    public partial class Frm_SP : Form
    {
        public Frm_SP()
        {
            InitializeComponent();
        }
        void LoadSanPham()
        {
            QLBHEntities db = new QLBHEntities();
            var listSP = from sp in db.SanPhams
                         select new
                         {
                             Ma = sp.MaSP,
                             Ten = sp.TenSP,
                             DVT = sp.DVTinh,
                             Gia = sp.DonGia,
                             MaLoai = sp.MaLoai,
                             Loai = sp.LoaiSanPham.TenLoai
                         };
            dataGVSanPham.DataSource = listSP.ToList();
        }
        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadSanPham();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaSP.Text = string.Empty;
            txtTenSp.Text = "";
            txtDVT.Text = string.Empty;
            txtDonGia.Text = string.Empty;
            txtMaLoaiSP.Text = string.Empty;
            txtMaSP.Enabled = true;
            btnLuu.Enabled = true;
        }

        private void Frm_SP_Load(object sender, EventArgs e)
        {
            txtMaSP.Enabled = false;
            btnLuu.Enabled = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            bool okInsert = true;

            if (txtMaSP.Text.Length == 0) //Check mã
            {
                okInsert = false;
                MessageBox.Show("Mã không được rỗng", "Lỗi Dữ Liệu Mới...", MessageBoxButtons.OK);
                txtMaSP.Focus();
            }

            if (txtTenSp.Text == "")
            {
                okInsert = false;
                MessageBox.Show("Tên không được rỗng", "Lỗi Dữ Liệu Mới...", MessageBoxButtons.OK);
                txtTenSp.Focus();
            }
            //Check các ràng buộc khác nếu cần

            if (okInsert)
            {
                try
                {
                    SanPham sp = new SanPham();
                    sp.MaSP = txtMaSP.Text;
                    sp.TenSP = txtTenSp.Text;
                    sp.DVTinh = txtDVT.Text;
                    sp.DonGia = Convert.ToInt32(txtDonGia.Text);
                    sp.MaLoai = txtMaLoaiSP.Text;

                    QLBHEntities db = new QLBHEntities();
                    db.SanPhams.Add(sp);
                    db.SaveChanges();

                    LoadSanPham();
                }
                catch
                {
                    MessageBox.Show("Có lỗi trong lúc Insert");
                }
                finally
                {
                    btnLuu.Enabled = false;
                    txtMaSP.Enabled = false;
                }

            }
        }

        private void dataGVSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaSP.Text = dataGVSanPham.Rows[dataGVSanPham.CurrentCell.RowIndex].Cells[0].Value.ToString();
            txtTenSp.Text = dataGVSanPham.Rows[dataGVSanPham.CurrentCell.RowIndex].Cells[1].Value.ToString();
            //txtDVT.Text = dataGVSanPham.SelectedCells[2].Value.ToString();
            //txtDonGia.Text = dataGVSanPham.SelectedCells[3].Value.ToString();
            //txtMaLoaiSP.Text = dataGVSanPham.SelectedCells[4].Value.ToString();
        }

        private void dataGVSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaSP.Text = dataGVSanPham.Rows[dataGVSanPham.CurrentCell.RowIndex].Cells[0].Value.ToString();
            //txtDVT.Text = dataGVSanPham.SelectedRows[0].Cells[1].Value.ToString();
            txtTenSp.Text = dataGVSanPham.Rows[dataGVSanPham.CurrentCell.RowIndex].Cells["Ten"].Value.ToString();
            txtDVT.Text = dataGVSanPham.Rows[dataGVSanPham.CurrentCell.RowIndex].Cells[2].Value.ToString();
            txtDonGia.Text = dataGVSanPham.Rows[dataGVSanPham.CurrentCell.RowIndex].Cells[3].Value.ToString();
            txtMaLoaiSP.Text = dataGVSanPham.Rows[dataGVSanPham.CurrentCell.RowIndex].Cells[4].Value.ToString();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            QLBHEntities db = new QLBHEntities();
            SanPham sp = db.SanPhams.Find(txtMaSP.Text);
            if (sp != null)
            {
                sp.TenSP = txtTenSp.Text;
                sp.DVTinh = txtDVT.Text;
                sp.DonGia = Convert.ToInt32(txtDonGia.Text);
                sp.MaLoai = txtMaLoaiSP.Text;

                try
                {
                    db.SaveChanges();
                    LoadSanPham();
                }
                catch 
                {
                    MessageBox.Show("Lỗi trong quá trình Update!!!");
                }
            }
        }
    }
}
