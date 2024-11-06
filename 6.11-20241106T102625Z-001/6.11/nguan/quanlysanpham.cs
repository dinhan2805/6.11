using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static nguan.Program;

namespace nguan
{
    public partial class quanlysanpham : Form
    {
        private BindingList<Product> products;
        private Product selectedProduct; // Biến để lưu sản phẩm được chọn
        public quanlysanpham()
        {
            InitializeComponent();
            products = new BindingList<Product>();
            dataGridView1.DataSource = products;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Cài đặt chế độ chọn dòng đầy đủ
            dataGridView1.CellClick += dataGridView1_CellContentClick; // Đăng ký sự kiện CellClick
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void quanlysanpham_Load(object sender, EventArgs e)
        {
            dataGridView1.CellClick += dataGridView1_CellContentClick;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var addForm = new AddEditProductForm();
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                products.Add(addForm.Product);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Lấy sản phẩm được chọn
                var selectedProduct = (Product)dataGridView1.SelectedRows[0].DataBoundItem;

                // Mở form sửa sản phẩm và truyền đối tượng Product vào
                var editForm = new AddEditProductForm(selectedProduct);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    // Sau khi sửa xong, cập nhật lại danh sách trong BindingList
                    dataGridView1.Refresh();  // Cập nhật lại DataGridView sau khi sửa

                    // Thực tế, có thể không cần gọi Refresh() nếu BindingList tự động cập nhật
                    // nhưng nếu cần có thể thay thế bằng cách gán lại nguồn dữ liệu:
                    dataGridView1.DataSource = new BindingList<Product>(products);

                    MessageBox.Show("Sửa thành công!");
                }
                else
                {
                    MessageBox.Show("Lỗi khi sửa thông tin sản phẩm.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm để sửa.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Lấy sản phẩm được chọn
                var selectedProduct = (Product)dataGridView1.SelectedRows[0].DataBoundItem;

                // Hiển thị hộp thoại xác nhận xóa sản phẩm
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này?", "Xóa sản phẩm", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    // Xóa sản phẩm khỏi BindingList
                    products.Remove(selectedProduct);
                    MessageBox.Show("Xóa thành công!");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var searchTerm = textBox1.Text.ToLower();
            var filteredList = products.Where(p => p.Name.ToLower().Contains(searchTerm) || p.Description.ToLower().Contains(searchTerm)).ToList();
            dataGridView1.DataSource = new BindingList<Product>(filteredList);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra xem người dùng có chọn một dòng hợp lệ không
            {
                // Lấy sản phẩm đã chọn từ dòng được nhấp
                selectedProduct = (Product)dataGridView1.Rows[e.RowIndex].DataBoundItem;
                // Hiển thị thông tin sản phẩm lên các điều khiển (nếu cần thiết)
                // Ví dụ: textBox1.Text = selectedProduct.Name; (tuỳ vào giao diện)
                MessageBox.Show($"Sản phẩm đã chọn: {selectedProduct.Name}");
            }
        }
    }
}
