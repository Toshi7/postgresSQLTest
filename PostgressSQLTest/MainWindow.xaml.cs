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

namespace PostgressSQLTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            //InitializeComponent();

            InitializeComponent();

            NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1;Port=5432;Database=test;User Id=postgres;" + "Password=admin;");
            conn.Open();


            // Define a query
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM simple_table", conn);


            // Execute a query
            NpgsqlDataReader dr = cmd.ExecuteReader();
            List<User> items = new List<User>();

            while (dr.Read())
               //    items.Add(new User() { id = (int)dr[0], tekst = (String)(dr[1])});

                   items.Add(new User() { id = Convert.ToInt32(dr[0]), tekst = Convert.ToString(dr[1]) });
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

            public string Mail { get; set; }

        }

    }
}
