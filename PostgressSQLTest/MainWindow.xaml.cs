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


            // Execute a query
            NpgsqlDataReader dr = cmd.ExecuteReader();
            List<User> items = new List<User>();

            while (dr.Read())
               //    items.Add(new User() { id = (int)dr[0], tekst = (String)(dr[1])});

                   items.Add(new User() { id = Convert.ToInt32(dr[0]), tekst = Convert.ToString(dr[1]),
                   gender = Convert.ToString(dr[2])
                   });
                    // Read all rows and output the first column in each row
            //                                Console.Write("{0}\n", dr[0]);
            //            Console.ReadLine();


            lvUsers.ItemsSource = items;
            // Close connection
            conn.Close();

        }

        public class User
        {
            public int id { get; set; }

            public String tekst { get; set; }

            public string gender { get; set; }

        }


        private void BindGrid()
        {
            NpgsqlConnection conn;
            NpgsqlCommand cmd;
            
            //if (con.State != ConnectionState.Open)
            conn = new NpgsqlConnection("Server=127.0.0.1;Port=5432;Database=test;User Id=postgres;" + "Password=admin;");
            conn.Open();

            cmd = new NpgsqlCommand("SELECT name FROM simple_table", conn);
            // Execute the query and obtain a result set
            NpgsqlDataReader dr = cmd.ExecuteReader();
            /*if (dr.HasRows)
            {
                lblCount.Visibility = System.Windows.Visibility.Hidden;
                gvData.Visibility = System.Windows.Visibility.Visible;

            }
            else
            {
                lblCount.Visibility = System.Windows.Visibility.Visible;
                gvData.Visibility = System.Windows.Visibility.Hidden;
            }*/

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
                        cmd.CommandText = "insert into simple_table(id,name,gender) Values(" + txtEmpId.Text + ",'" + txtEmpName.Text + "','" + ddlGender.Text + "')";
                       cmd.ExecuteNonQuery();
                        BindGrid();
                        MessageBox.Show("Employee Added Successfully...");
                        //ClearAll();

                    }
                    else
                    {
                        MessageBox.Show("Please Select Gender Option....");
                    }
                }
                else
                {
                    cmd.CommandText = "update tbEmp set EmpName='" + txtEmpName.Text + "',Gender='" + ddlGender.Text + "',Contact=" + txtContact.Text + ",Address='" + txtAddress.Text + "' where EmpId=" + txtEmpId.Text;
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
            if (lvUsers.SelectedItems.Count > 0)
            {
                //                DataRowView row = (DataRowView)lvUsers.SelectedItems[0];
                ListViewItem itemx = (ListViewItem)lvUsers.SelectedItems[0];

                NpgsqlCommand cmd = new NpgsqlCommand();
                conn = new NpgsqlConnection("Server=127.0.0.1;Port=5432;Database=test;User Id=postgres;" + "Password=admin;");
                conn.Open();
                cmd.Connection = conn;
                //                int selectedId;
                //ListViewItem selectedItem = (ListViewItem)sender;
                //selectedId = (selectedItem.SubItems[0]);


                cmd.CommandText = "delete from simple_table where id=" + itemx;

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

        }

        private void lvUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
    
}
