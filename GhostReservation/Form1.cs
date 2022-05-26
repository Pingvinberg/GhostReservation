using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace GhostReservation
{
    public partial class Form1 : Form
    {
        StringBuilder errorMessages = new StringBuilder();

        private string _connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        DataTable dataTable1 = new DataTable();
        DataTable dataTable2 = new DataTable();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            resultBox.Text = sqlQueryMain(int.Parse(storeIdBox.Text), int.Parse(SupplierArticleIDBox.Text));
        }

        private string sqlQueryMain(int storeID, int SupplierArticleId)
        {
            string result = null;
            SqlConnection connection;

            string sql1 = " Select TOP 1000 CustomerOrderStatus,CustomerOrderLines.ArticleID,CustomerOrderLineStatus,ExternalOrderId,OrderedQty,ReceivedQty,rex.* FROM CustomerOrders INNER JOIN CustomerOrderLines ON CustomerOrders.CustomerOrderNo = CustomerOrderLines.CustomerOrderNo INNER JOIN AllArticles ON AllArticles.ArticleId = CustomerOrderLines.ArticleID FULL JOIN openquery (MYSQL, 'SELECT RECEPTBESTALLNING.salesOrderId, RECEPTBESTALLNING.id, RECEPTBESTALLNING.StoreNo, ERPARTIKEL.erpId,ERPARTIKEL.varunummer,  RECEPTBESTALLNING.receptBestallningsStatus_id, RECEPTBESTALLNINGRAD.ARTIKEL_ID FROM RECEPTBESTALLNING JOIN RECEPTBESTALLNINGRAD ON RECEPTBESTALLNING.id = RECEPTBESTALLNINGRAD.bestallning_id INNER JOIN ERPARTIKEL ON RECEPTBESTALLNINGRAD.artikel_id = ERPARTIKEL.erpId where ERPARTIKEL.varunummer = " + SupplierArticleId + ";') rex ON rex.salesOrderId = CustomerOrders.CustomerOrderId and rex.erpId = CustomerOrderLines.ArticleId WHERE AllArticles.SupplierArticleID = " + SupplierArticleId + " AND CustomerOrders.StoreNo = " + storeID + " and rex.varunummer = " + SupplierArticleId + " and CustomerOrders.CustomerOrderStatus <> 80 and CustomerOrderStatus <> 99 order by CustomerOrders.CustomerOrderNo DESC ";
            string sql2 = " Select AllArticles.ArticleID, AllArticles.SupplierArticleID, StoreArticleInfos.ArticleNo, ReservedStockQty, InStockQty, ReservedStockInOrderQty from StoreArticleInfos inner join AllArticles on AllArticles.ArticleNo = StoreArticleInfos.ArticleNo  where StoreNo = " + storeID + " and AllArticles.SupplierArticleID = " + SupplierArticleId + " ";

            connection = new SqlConnection(_connectionString);
            try
            {
                connection.Open();

                SqlCommand command1 = new SqlCommand(sql1, connection);
                command1.CommandType = CommandType.Text;
                SqlDataAdapter dataAdapter1 = new SqlDataAdapter(command1);                
                dataAdapter1.Fill(dataTable1);
                dataGridView1.DataSource = dataTable1;

                SqlCommand command2 = new SqlCommand(sql2, connection);
                command2.CommandType = CommandType.Text;
                SqlDataAdapter dataAdapter2 = new SqlDataAdapter(command2);
                dataAdapter2.Fill(dataTable2);
                dataGridView2.DataSource = dataTable2;
                
                connection.Close();
                /*
                    result = Logic for the query result and table to present to serivcedesk to just copy and paste to ticket.                        
                */
            }
            catch (SqlException ex)
            {
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                        "Message: " + ex.Errors[i].Message + "\n" +
                        "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                        "Source: " + ex.Errors[i].Source + "\n" +
                        "Procedure: " + ex.Errors[i].Procedure + "\n");
                }
                result = errorMessages.ToString();
            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }            

            return result;
        }        

        private void button1_Click(object sender, EventArgs e)
        {
            resultBox.Text = "";
            storeIdBox.Text = "";
            articleIDBox.Text = "";
            SupplierArticleIDBox.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            resultBox.SelectAll();
            resultBox.Copy();
            resultBox.DeselectAll();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(articleIDBox.Text != string.Empty)
            {
                SupplierArticleIDBox.Text = sqlQueryArticleId(articleIDBox.Text);
            }            
        }

        private string sqlQueryArticleId(string articleID)
        {
            string result = null;
            string sql = null;            
            SqlConnection connection;
            SqlDataReader dataReader;

            sql = "select SupplierArticleId from AllArticles where ArticleId = " + articleID;

            connection = new SqlConnection(_connectionString);
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.Text;
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    SupplierArticleIDBox.Text = dataReader.GetString(0);                             
                }
                dataReader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (SqlException ex)
            {
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                        "Message: " + ex.Errors[i].Message + "\n" +
                        "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                        "Source: " + ex.Errors[i].Source + "\n" +
                        "Procedure: " + ex.Errors[i].Procedure + "\n");
                }
                resultBox.Text = errorMessages.ToString();
            }
            catch (Exception ex)
            {
                resultBox.Text = ex.ToString();
            }
            return result;
        }
    }
}