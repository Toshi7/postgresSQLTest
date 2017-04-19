using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Npgsql;
using System.Data;
using System.Data.OleDb;

namespace PostgressSQLTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NpgsqlConnection conn;
        NpgsqlCommand cmd;

        public MainWindow()
        {
            //InitializeComponent();

            InitializeComponent();

            conn = new NpgsqlConnection("Server=127.0.0.1;Port=5432;Database=test;User Id=postgres;" + "Password=admin;");
            conn.Open();



            // Define a query
            cmd = new NpgsqlCommand("SELECT * FROM simple_table", conn);

            BindGrid();
            // Execute a query


        }

        public class User
        {
            public int id { get; set; }

            public String name { get; set; }

            public string gender { get; set; }
            public String contact { get; set; }
            public String address { get; set; }

        }


        private void BindGrid()
        {
            NpgsqlConnection conn;
            NpgsqlCommand cmd;
            
            //if (conn != ConnectionState.Open)
            conn = new NpgsqlConnection("Server=127.0.0.1;Port=5432;Database=test;User Id=postgres;" + "Password=admin;");
            conn.Open();
            cmd = new NpgsqlCommand("SELECT name FROM simple_table", conn);
            cmd.Connection = conn;
            cmd.CommandText = "SELECT* FROM simple_table";
           // OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataTable dt = new DataTable();
            //da.Fill(dt);


            // Execute the query and obtain a result set
            NpgsqlDataReader dr = cmd.ExecuteReader();

            List<User> items = new List<User>();
            //int count = 0;
            while (dr.Read())
                //    items.Add(new User() { id = (int)dr[0], tekst = (String)(dr[1])});

                items.Add(new User()
                {
                    id = Convert.ToInt32(dr[0]),
                    name = Convert.ToString(dr[1]),
                    gender = Convert.ToString(dr[2]),
                    contact = Convert.ToString(dr[3]),
                    address = Convert.ToString(dr[4])
                });
            // Read all rows and output the first column in each row
            //                                Console.Write("{0}\n", dr[0]);
            //            Console.ReadLine();
            gvData.ItemsSource = items;

            /*
            while (dr.Read())
            {
                count++;
            }
            
            // Close connection
            //conn.Close();




            
            if (count > 0)
            {
                lblCount.Visibility = System.Windows.Visibility.Hidden;
                gvData.Visibility = System.Windows.Visibility.Visible;

            }
            else
            {
                lblCount.Visibility = System.Windows.Visibility.Visible;
                gvData.Visibility = System.Windows.Visibility.Hidden;
            }
            */
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            conn = new NpgsqlConnection("Server=127.0.0.1;Port=5432;Database=test;User Id=postgres;" + "Password=admin;");
            conn.Open();
            cmd.Connection = conn;

            if (txtEmpId.Text != "")
            {
                if (txtEmpId.IsEnabled == true)
                {
                    if (ddlGender.Text != "Select Gender")
                    {
                        cmd.CommandText = "insert into simple_table(id,name,gender,contact,address) Values(" + txtEmpId.Text + ",'" + txtEmpName.Text + "','" + ddlGender.Text + "','" + txtContact.Text + "','" + txtAddress.Text + "')";
                       cmd.ExecuteNonQuery();
                        BindGrid();
                        MessageBox.Show("Employee Added Successfully...");
                        ClearAll();

                    }
                    else
                    {
                        MessageBox.Show("Please Select Gender Option....");
                    }
                }
                else
                {
                    

//                    MessageBox.Show("update simple_table set name='" + txtEmpName.Text + "',gender = '" + ddlGender.Text + "',contact= '" + txtContact.Text + "',address= '" + txtAddress.Text + "'where id= " + txtEmpId.Text);
                    cmd.CommandText = "update simple_table set name='" + txtEmpName.Text + "',gender = '" + ddlGender.Text + "',contact= '" + txtContact.Text + "',address= '" + txtAddress.Text + "'where id= " + txtEmpId.Text;
                    cmd.ExecuteNonQuery();
                    BindGrid();
                    MessageBox.Show("Employee Details Updated Succesffully...");
                    //ClearAll();
                }
            }
            else
            {
                MessageBox.Show("Please Add Employee Id.......");
            }
        }


        //Clear all records from controls
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
        }

        private void ClearAll()
        {
            txtEmpId.Text = "";
            txtEmpName.Text = "";
            ddlGender.SelectedIndex = 0;
            txtContact.Text = "";
            txtAddress.Text = "";
            btnAdd.Content = "Add";
            txtEmpId.IsEnabled = true;
        }


        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (gvData.SelectedItems.Count > 0)
            {
                //                DataRowView row = (DataRowView)lvUsers.SelectedItems[0];
//                ListViewItem itemx = (ListViewItem)lvUsers.SelectedItems[0];

                NpgsqlCommand cmd = new NpgsqlCommand();
                conn = new NpgsqlConnection("Server=127.0.0.1;Port=5432;Database=test;User Id=postgres;" + "Password=admin;");
                conn.Open();
                cmd.Connection = conn;
                //                int selectedId;
                //ListViewItem selectedItem = (ListViewItem)sender;
                //selectedId = (selectedItem.SubItems[0]);

                //                var a = lvUsers.SelectedIndex + 1;
                //                string myString = a.ToString();

                //                string myString = lvUsers.SelectedItems[0].ToString();
                //                MessageBox.Show(myString);

                /*
                //to find the ID user click
                var selectedStockObject = lvUsers.SelectedItems[0] as User;
                if (selectedStockObject == null)
                {
                    return;
                }
                */
                /*
                cmd.CommandText = "delete from simple_table where id=" + selectedStockObject.id;
                */

                //DataRowView row = (DataRowView)gvData.SelectedItems[0];
                //to find the ID user click
                var selectedStockObject = gvData.SelectedItems[0] as User;
                if (selectedStockObject == null)
                {
                    return;
                }
                
                
                cmd.CommandText = "delete from simple_table where id=" + selectedStockObject.id;
 //               cmd.CommandText = "delete from simple_table where id=" + row["EmpId"].ToString();
                cmd.ExecuteNonQuery();
                BindGrid();
                MessageBox.Show("Employee Deleted Successfully...");
            }
            else
            {
                MessageBox.Show("Please Select Any Employee From List...");
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (gvData.SelectedItems.Count > 0)
            {
                var selectedStockObject = gvData.SelectedItems[0] as User;
                if (selectedStockObject == null)
                {
                    return;
                }

                
                
                BindGrid();

                //                DataRowView row = (DataRowView)lvUsers.SelectedItems[0];
                 //int anInteger;
                 //anInteger = Convert.ToInt32(txtEmpId.Text);
                 // anInteger = int.Parse(txtEmpId.Text);

                txtEmpId.Text = selectedStockObject.id.ToString();
                txtEmpName.Text = selectedStockObject.name;
                ddlGender.Text = selectedStockObject.gender;
                txtContact.Text = selectedStockObject.contact;
                txtAddress.Text = selectedStockObject.address;
                txtEmpId.IsEnabled = false;
                btnAdd.Content = "Update";
            }
            else
            {
                MessageBox.Show("Please Select Any Employee From List...");
            }
        }

        

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            //Exit
           
                Application.Current.Shutdown();
           
        }

        private void gvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
    
}
