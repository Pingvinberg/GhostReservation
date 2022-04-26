using System.Data.SqlClient;

namespace GhostReservation
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }


        private void btnSearch_Click_1(object sender, EventArgs e)
        {   
            resultBox.Text = sqlQuery(storeIdBox.Text, articleIDBox.Text);
        }

        private string sqlQuery(string storeID, string articleId)
        {
            string result = null;
            string connetionString = null;
            SqlConnection connection;
            SqlCommand command;
            string sql = null;
            SqlDataReader dataReader;
            connetionString = "Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password";
            sql = "Your SQL Statement Here , like Select * from product" + storeID +"blabla" + articleId;
            connection = new SqlConnection(connetionString);
            try
            {
                connection.Open();
                command = new SqlCommand(sql, connection);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    /*MessageBox.Show(dataReader.GetValue(0) + " - " + dataReader.GetValue(1) + " - " + dataReader.GetValue(2));
                     Hantera datan som kommer i dataReader.GetValeue(0).tostring för få första värdet och hantera det.
                     dvs skapa en string här som vi skickar till resultbox.text

                    ex.
                    var data1 = dataReader.GetValue(0);
                    var data2 = dataReader.GetValue(1);
                    if (data1 > 0)
                        {
                            result = data1.toString() + " är större än 0" 
                        }   
                     */
                }
                dataReader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! ");
            }
            result = "Store ID: " + storeID + "\r\n" + "Article ID: " + articleId; //bara för att testa copy funktionen
            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            resultBox.Text = "";
            storeIdBox.Text = "";
            articleIDBox.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            resultBox.SelectAll();
            resultBox.Copy();
            resultBox.DeselectAll();
        }
    }
}