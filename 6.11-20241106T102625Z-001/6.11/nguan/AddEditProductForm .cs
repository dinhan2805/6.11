using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static nguan.Program;

namespace nguan
{
    public partial class AddEditProductForm : Form
    {
        public Product Product { get; set; }
        public AddEditProductForm()
        {
            InitializeComponent();
            Product = new Product();
        }
        public AddEditProductForm(Product product)
        {
            InitializeComponent();
            Product = product;  // Gán đối tượng sản phẩm được truyền vào
                                // Nếu sản phẩm đã có, điền thông tin vào các trường
            textBox1.Text = Product.Name;
            textBox2.Text = Product.Description;
            textBox3.Text = Product.Price.ToString();
            textBox4.Text = Product.Quantity.ToString();
        }
        private void AddEditProductForm_Load(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            // Lưu thông tin sản phẩm
            Product.Name = textBox1.Text;
            Product.Description = textBox2.Text;
            Product.Price = decimal.TryParse(textBox3.Text, out var price) ? price : 0;
            Product.Quantity = int.TryParse(textBox4.Text, out var quantity) ? quantity : 0;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
