using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Company
{
    public partial class Form2 : Form
    {
        CompanyEntities1 compEnt;
        bool permissionType;

        public Form2()
        {
            InitializeComponent();
            compEnt = new CompanyEntities1();
            StoreLoad();
            permissionType = false;
        }
        private void StoreLoad()
        {
            comboBox1.Items.Clear();
            var store = from s in compEnt.Stores select s;
            foreach (var stor in store)
            {
                comboBox1.Items.Add(stor.ID.ToString());
            }
            dataGridView1.DataSource = compEnt.Stores.ToList();
            this.dataGridView1.Columns["Export_Permission"].Visible = false;
            this.dataGridView1.Columns["Supplys_Permission"].Visible = false;
            this.dataGridView1.Columns["Transactions"].Visible = false;
            this.dataGridView1.Columns["Transactions1"].Visible = false;
            this.dataGridView1.Columns["Products"].Visible = false;
        }
        private void updateStore()
        {
            
            if (comboBox1.Text == "")
                MessageBox.Show("Please Select id");
            else
            {
                int ID = int.Parse(comboBox1.Text);
                var stor = (from s in compEnt.Stores
                            where s.ID == ID
                            select s).First();
                if (stor != null)
                {
                    stor.Name = textBox1.Text;
                    stor.Address = textBox2.Text;
                    stor.Res_Name = textBox3.Text;
                    compEnt.SaveChanges();
                    MessageBox.Show("Store is Changed");
                }
            }
            
        }

        private void insertStore()
        {
            Store stor = new Store();
            if (textBox1.Text == "")
                MessageBox.Show("Please Enter The name");
            else
            {
                if (textBox2.Text == "")
                    MessageBox.Show("Please Enter The Address");
                else
                {
                    if (textBox3.Text == "")
                        MessageBox.Show("Please Enter the Resbosible Name");
                    else
                    {
                        stor.Name = textBox1.Text;
                        stor.Address = textBox2.Text;
                        stor.Res_Name = textBox3.Text;
                        compEnt.Stores.Add(stor);
                        compEnt.SaveChanges();
                        textBox1.Text = textBox2.Text = textBox3.Text = string.Empty;
                    }
                }
            }
           
        }
        private void ProductLoad()
        {
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            var product = from p in compEnt.Products 
                          join m in compEnt.Measures
                          on p.Code equals m.Prod_Code
                          select new
                          { 
                              p.Code,
                              p.Name,
                              m.Measure_Unit
                          };
            foreach (var prod in product)
            {
                comboBox3.Items.Add(prod.Code.ToString());
                comboBox2.Items.Add(prod.Measure_Unit);
            }
            dataGridView2.DataSource = product.ToList();
        }

        private void insertProduct()
        {
            Product prod = new Product();
            Measure unit = new Measure();

            if (textBox5.Text == "")
                MessageBox.Show("Please Enter The Name");
            else
            {
                if (textBox6.Text == "")
                    MessageBox.Show("Please Enter The code");
                else
                {
                    if (textBox4.Text == "")
                        MessageBox.Show("Please Enter The Unit Measure or Choose one");
                    else
                    {
                        prod.Name = textBox5.Text;
                        prod.Code = int.Parse(textBox6.Text);
                        unit.Prod_Code = int.Parse(textBox6.Text);
                        unit.Measure_Unit = textBox4.Text;
                        compEnt.Products.Add(prod);
                        compEnt.Measures.Add(unit);
                        compEnt.SaveChanges();
                        textBox5.Text = textBox6.Text = textBox4.Text = string.Empty;
                    }
                }
               
            }
           
        }
        private void updateProduct()
        {
            if (comboBox3.Text == "")
                MessageBox.Show("Select the Product code");
            else
            { 
                 int ID = int.Parse(comboBox3.Text);
                 var prod = (from p in compEnt.Products
                        where p.Code == ID
                        select p).First();

            if (prod != null)
            {
                    if (comboBox2.Text == "")
                        MessageBox.Show("Please select The unit");
                    else
                    {
                        prod.Name = textBox5.Text;
                        prod.Code = int.Parse(textBox6.Text);
                        var unit = (from m in compEnt.Measures
                                     where m.Prod_Code == ID&&
                                     m.Measure_Unit == comboBox2.Text
                                     select m).First();
                        compEnt.Measures.Remove(unit);
                        compEnt.SaveChanges();
                        Measure newone = new Measure();
                        newone.Prod_Code = ID;
                        newone.Measure_Unit = textBox4.Text;
                        compEnt.Measures.Add(newone);
                        compEnt.SaveChanges();
                        MessageBox.Show("Product is Changed");
                        comboBox2.Text = "";
                    }
            }
            }
        }
        private void CustomerLoad()
        {
            comboBox4.Items.Clear();
            var customer = from c in compEnt.Customers select c;
            foreach (var cust in customer)
            {
                comboBox4.Items.Add(cust.SSN.ToString());
            }
            dataGridView3.DataSource = compEnt.Customers.ToList();
            this.dataGridView3.Columns["Export_Permission"].Visible = false;
            this.dataGridView3.Columns["Supplys_Permission"].Visible = false;
        }
        private void insertCustomer()
        {
            Customer cust = new Customer();
            if (textBox7.Text == "")
                MessageBox.Show("Please Enter The SSN");
            else
            {
                if (textBox8.Text == "")
                    MessageBox.Show("Please Enter The Name");
                else
                {
                    if (textBox9.Text == "")
                        MessageBox.Show("Please Enter the Telephone");
                    else
                    {
                        if (comboBox7.Text=="")
                            MessageBox.Show("Please select the type");
                        else
                        {
                            cust.SSN = int.Parse(textBox7.Text);
                            cust.Name = textBox8.Text;
                            cust.Tele = int.Parse(textBox9.Text);
                            cust.Fax = int.Parse(textBox10.Text);
                            cust.Mail = textBox11.Text;
                            cust.Site = textBox12.Text;
                            cust.Type = comboBox7.Text;
                            compEnt.Customers.Add(cust);
                            compEnt.SaveChanges();
                            textBox7.Text = 
                                textBox8.Text = 
                                textBox9.Text = 
                                textBox10.Text = 
                                textBox11.Text = 
                                textBox12.Text = 
                                comboBox7.Text= string.Empty;
                        }
                        
                    }
                }
            }

        }
        private void updateCustomer()
        {

            if (comboBox4.Text == "")
                MessageBox.Show("Please Select SSN");
            else
            {
                int ID = int.Parse(comboBox4.Text);
                var cust = (from c in compEnt.Customers
                            where c.SSN == ID
                            select c).First();
                if (cust != null)
                {
                    cust.SSN = int.Parse(textBox7.Text);
                    cust.Name = textBox8.Text;
                    cust.Tele = int.Parse(textBox9.Text);
                    cust.Fax = int.Parse(textBox10.Text);
                    cust.Mail = textBox11.Text;
                    cust.Site = textBox12.Text;
                    cust.Type = comboBox7.Text;
                    compEnt.SaveChanges();
                    MessageBox.Show("Customer is Changed");
                }
            }

        }

        private void PermissionLoad()
        {
            comboBox8.Items.Clear();
            comboBox9.Items.Clear();
            comboBox10.Items.Clear();
            comboBox6.Items.Clear();
            if (permissionType == false)
            {
                var permission = from p in compEnt.Supplys_Permission
                                 join s in compEnt.Stores
                                 on p.Store_id equals s.ID
                                 join prod in compEnt.Products
                                 on p.Prod_Code equals prod.Code
                                 join c in compEnt.Customers
                                 on p.Cust_id equals c.SSN
                                 select new
                                 {
                                     p.ID,
                                     p.Quentity,
                                     p.Date,
                                     s.Name,
                                     product = prod.Name,
                                     customer = c.Name,
                                     p.Expire_Date,
                                     p.Production_Date

                                 };
                foreach (var perm in permission)
                {
                    comboBox8.Items.Add(perm.ID);
                }

                dataGridView4.DataSource = permission.ToList();
            }
            else
            {
                var permission = from p in compEnt.Export_Permission
                                 join s in compEnt.Stores
                                 on p.Store_id equals s.ID
                                 join prod in compEnt.Products
                                 on p.Prod_Code equals prod.Code
                                 join c in compEnt.Customers
                                 on p.Cust_id equals c.SSN
                                 select new
                                 {
                                     p.ID,
                                     p.Quentity,
                                     p.Date,
                                     s.Name,
                                     product = prod.Name,
                                     customer = c.Name,

                                 };
                foreach (var perm in permission)
                {
                    comboBox8.Items.Add(perm.ID);
                }

                dataGridView4.DataSource = permission.ToList();
            }
            var stor = from s in compEnt.Stores select s;
            foreach(var st in stor)
            {
                comboBox9.Items.Add(st.Name);
            }

            var pro = from p in compEnt.Products select p;
            foreach(var prd in pro)
            {
                comboBox6.Items.Add(prd.Name);
            }
            var cust = from c in compEnt.Customers select c;
            foreach(var cst in cust)
            {
                comboBox10.Items.Add(cst.Name);
            }
        }
        private void insertPermission()
        {
            if(permissionType == false)
            {
                Supplys_Permission perm = new Supplys_Permission();
                perm.ID = int.Parse(textBox13.Text);
                perm.Quentity = int.Parse(textBox16.Text);
                var stor = (from s in compEnt.Stores
                            where s.Name == comboBox9.Text
                            select s).First();
                perm.Store_id = stor.ID;
                var prod = (from s in compEnt.Products
                            where s.Name == comboBox6.Text
                            select s).First();
                perm.Prod_Code = prod.Code;
                prod.Stores.Add(stor);
                stor.Products.Add(prod);
                var cust = (from c in compEnt.Customers
                            where c.Name == comboBox10.Text
                            select c).First();
                perm.Cust_id = cust.SSN;
                DateTime dt = this.dateTimePicker3.Value.Date;
                perm.Date = dt;
                DateTime dtExpire = this.dateTimePicker2.Value.Date;
                perm.Expire_Date = dtExpire;
                DateTime dtproduction = this.dateTimePicker1.Value.Date;
                perm.Production_Date = dtproduction;
                compEnt.Supplys_Permission.Add(perm);
                compEnt.SaveChanges();
                textBox13.Text = textBox16.Text = string.Empty;
            }
            else
            {
                Export_Permission perm = new Export_Permission();
                perm.ID = int.Parse(textBox13.Text);
                perm.Quentity = int.Parse(textBox16.Text);
                var stor = (from s in compEnt.Stores
                            where s.Name == comboBox9.Text
                            select s).First();
                perm.Store_id = stor.ID;
                var prod = (from s in compEnt.Products
                            where s.Name == comboBox6.Text
                            select s).First();
                perm.Prod_Code = prod.Code;
                var cust = (from c in compEnt.Customers
                            where c.Name == comboBox10.Text
                            select c).First();
                perm.Cust_id = cust.SSN;
                DateTime dt = this.dateTimePicker3.Value.Date;
                perm.Date = dt;
                compEnt.Export_Permission.Add(perm);
                compEnt.SaveChanges();
                textBox13.Text = textBox16.Text = string.Empty;
            }
        }
        private void updatePermission()
        {
            if (permissionType == false)
            {
                if (comboBox8.Text == "")
                    MessageBox.Show("Please Select ID");
                else
                {
                    int ID = int.Parse(comboBox8.Text);
                    var perm = (from p in compEnt.Supplys_Permission
                                where p.ID == ID
                                select p).First();
                    if (perm != null)
                    {
                        perm.Quentity = int.Parse(textBox16.Text);
                        var stor = (from s in compEnt.Stores
                                    where s.Name == comboBox9.Text
                                    select s).First();
                        perm.Store_id = stor.ID;
                        var prod = (from s in compEnt.Products
                                    where s.Name == comboBox6.Text
                                    select s).First();
                        perm.Prod_Code = prod.Code;
                        prod.Stores.Add(stor);
                        stor.Products.Add(prod);
                        var cust = (from c in compEnt.Customers
                                    where c.Name == comboBox10.Text
                                    select c).First();
                        perm.Cust_id = cust.SSN;
                        DateTime dt = this.dateTimePicker3.Value.Date;
                        perm.Date = dt;
                        DateTime dtExpire = this.dateTimePicker2.Value.Date;
                        perm.Expire_Date = dtExpire;
                        DateTime dtproduction = this.dateTimePicker1.Value.Date;
                        perm.Production_Date = dtproduction;
                        compEnt.SaveChanges();
                        textBox13.Text = textBox16.Text = string.Empty;
                        compEnt.SaveChanges();
                        MessageBox.Show("Permission is Changed");
                    }
                }
            }
            else
            {

                if (comboBox8.Text == "")
                    MessageBox.Show("Please Select ID");
                else
                {
                    int ID = int.Parse(comboBox8.Text);
                    var perm = (from p in compEnt.Export_Permission
                                where p.ID == ID
                                select p).First();
                    if (perm != null)
                    {
                        perm.Quentity = int.Parse(textBox16.Text);
                        var stor = (from s in compEnt.Stores
                                    where s.Name == comboBox9.Text
                                    select s).First();
                        perm.Store_id = stor.ID;
                        var prod = (from s in compEnt.Products
                                    where s.Name == comboBox6.Text
                                    select s).First();
                        perm.Prod_Code = prod.Code;
                        var cust = (from c in compEnt.Customers
                                    where c.Name == comboBox10.Text
                                    select c).First();
                        perm.Cust_id = cust.SSN;
                        DateTime dt = this.dateTimePicker3.Value.Date;
                        perm.Date = dt;
                        compEnt.SaveChanges();
                        textBox13.Text = textBox16.Text = string.Empty;
                    }
                }
            }
        }
        private void TransactionLoad()
        {
            comboBox12.Items.Clear();
            comboBox13.Items.Clear();
            comboBox13.Items.Clear();
            comboBox14.Items.Clear();

            var transaction = from t in compEnt.Transactions
                              join prod in compEnt.Products
                              on t.Prod_Code equals prod.Code
                              join sup in compEnt.Supplys_Permission
                              on prod.Code equals sup.Prod_Code
                              join c in compEnt.Customers
                              on sup.Cust_id equals c.SSN
                             select new
                             {
                                 t.ID,
                                 Store_From = t.Store_From,
                                 Store_To = t.Store_To,
                                 prod.Name,
                                 t.Quentity,
                                 t.Expire_Date,
                                 t.Production_Date,
                                 customer = c.Name

                             };
            foreach (var trans in transaction)
            {
                comboBox11.Items.Add(trans.ID);
            }
            var stor_from = from fr in compEnt.Stores select fr;
            foreach(var stfr in stor_from)
            {
                comboBox12.Items.Add(stfr.Name);
            }
            var stor_to = from fr in compEnt.Stores select fr;
            foreach (var stto in stor_to)
            {
                comboBox13.Items.Add(stto.Name);
            }
            var custo = from cust in compEnt.Customers select cust;
            foreach (var cust in custo)
            {
                comboBox15.Items.Add(cust.Name);
            }

            dataGridView5.DataSource = transaction.ToList();
        }
        private void insertTransation()
        {
            Transaction trans = new Transaction();
            trans.Quentity = int.Parse(textBox23.Text);

            var stor = (from s in compEnt.Stores
                        where s.Name == comboBox12.Text
                        select s).First();
            trans.Store_From = stor.ID;

            var stor_to = (from s in compEnt.Stores
                        where s.Name == comboBox13.Text
                        select s).First();
            trans.Store_To = stor_to.ID;

            var prod = (from s in compEnt.Products
                        where s.Name == comboBox14.Text
                        select s).First();
            trans.Prod_Code = prod.Code;


            DateTime dtExpire = this.dateTimePicker4.Value.Date;
            trans.Expire_Date = dtExpire;

            DateTime dtproduction = this.dateTimePicker5.Value.Date;
            trans.Production_Date = dtproduction;

            prod.Stores.Add(stor_to);
            prod.Stores.Remove(stor);
            stor_to.Products.Add(prod);
            stor.Products.Add(prod);

            compEnt.Transactions.Add(trans);
            compEnt.SaveChanges();
                textBox23.Text = string.Empty;
            }
       
            private void storeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabpage1"];
        }

        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabpage2"];
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabpage3"];
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProductLoad();
            CustomerLoad();
            PermissionLoad();
            TransactionLoad();
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            insertCustomer();
            CustomerLoad();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            updateCustomer();
            CustomerLoad();
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            insertStore();
            StoreLoad();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            updateStore();
            StoreLoad();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ID = int.Parse(comboBox1.Text);
            var stor = (from s in compEnt.Stores
                        where s.ID == ID
                        select s).First();
            textBox1.Text = stor.Name;
            textBox2.Text = stor.Address;
            textBox3.Text = stor.Res_Name;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            insertProduct();
            ProductLoad();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ID = int.Parse(comboBox3.Text);
            var prod = (from p in compEnt.Products
                        where p.Code == ID
                        select p).First();
            textBox5.Text = prod.Name;
            textBox6.Text = prod.Code.ToString();
            comboBox2.Items.Clear();
            var unit = (from m in compEnt.Measures
                        where m.Prod_Code == ID
                        select m);
            foreach (var msure in unit)
            {
                comboBox2.Items.Add(msure.Measure_Unit);
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            ProductLoad();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            updateProduct();
            ProductLoad();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox4.Text = comboBox2.Text;
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ID = int.Parse(comboBox4.Text);
            var cust = (from c in compEnt.Customers
                        where c.SSN == ID
                        select c).First();
            textBox7.Text = cust.SSN.ToString();
            textBox8.Text = cust.Name;
            textBox9.Text = cust.Tele.ToString();
            textBox10.Text = cust.Fax.ToString();
            textBox11.Text = cust.Mail;
            textBox12.Text = cust.Site;
            comboBox1.SelectedItem = cust.Type;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            insertPermission();
            PermissionLoad();
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox5.Text == "Import")
            {
                permissionType = false;
                dateTimePicker1.Visible = true;
                dateTimePicker2.Visible = true;
                label26.Visible = true;
                label27.Visible = true;
            }
                
            else
            {
                permissionType = true;
                dateTimePicker1.Visible = false;
                dateTimePicker2.Visible = false;
                label26.Visible = false;
                label27.Visible = false;
            }

            PermissionLoad();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            updatePermission();
            PermissionLoad();
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void textBox21_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox12_SelectedIndexChanged(object sender, EventArgs e)
        {
            var prod = from p in compEnt.Products
                       join sup in compEnt.Supplys_Permission
                       on p.Code equals sup.Prod_Code
                       join s in compEnt.Stores
                       on sup.Store_id equals s.ID
                       where s.Name == comboBox12.Text
                       select new
                       {
                           p.Name
                       };
            foreach (var pr in prod)
            {
                comboBox14.Items.Add(pr.Name);
            }
        }

        private void label33_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 form4 = new Form4();
            form4.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form5 form5 = new Form5();
            form5.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form7 form7 = new Form7();
            form7.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form8 form8 = new Form8();
            form8.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            insertTransation();
            TransactionLoad();
        }

        private void clientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabpage3"];
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabpage5"];
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabpage5"];
        }

        private void exchangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabpage6"];
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form6 form6 = new Form6();
            form6.Show();
        }
    }
}
