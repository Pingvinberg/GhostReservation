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

        public Form1()
        {
            InitializeComponent();            
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            if (SupplierArticleIDBox.Text != String.Empty && articleIDBox.Text == String.Empty)
            {
                SupplierArticleIDBox.Text = sqlQueryArticleID(articleIDBox.Text);
            }
            else if (WithErrors())
            {
                resultBox.Text = "One or several fields are empty";
            }
            else
            {
                resultBox.Text = sqlQueryMain(storeIdBox.Text, SupplierArticleIDBox.Text);
            }
        }

        private string sqlQueryArticleID(string articleID)
        {
            string result = null;            
            string sql = null;
            SqlConnection connection;
            SqlCommand command;
            SqlDataReader dataReader;            
            sql = "select SupplierArticleId from AllArticles where ArticleId = @ArticleID";

            connection = new SqlConnection(_connectionString);
            try
            {
                connection.Open();
                command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@ArticleID", articleID);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    result = dataReader["SupplierArticleId"].ToString();
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

        private string sqlQueryMain(string storeID, string SupplierArticleId)
        {
            string result = null;            
            string sql = null;
            SqlConnection connection;
            SqlCommand command;
            SqlDataReader dataReader;
            sql = "Select TOP 1000 CustomerOrderStatus,CustomerOrderLines.ArticleID,CustomerOrderLineStatus,ExternalOrderId,OrderedQty,ReceivedQty,rex.*FROM CustomerOrders with(nolock) INNER JOIN CustomerOrderLines with(nolock) ON CustomerOrders.CustomerOrderNo = CustomerOrderLines.CustomerOrderNo INNER JOIN AllArticles with(nolock) ON AllArticles.ArticleId = CustomerOrderLines.ArticleID FULL JOIN openquery (MYSQL, 'SELECT RECEPTBESTALLNING.salesOrderId,RECEPTBESTALLNING.id,RECEPTBESTALLNING.StoreNo,ERPARTIKEL.erpId,ERPARTIKEL.varunummer,RECEPTBESTALLNING.receptBestallningsStatus_id,RECEPTBESTALLNINGRAD.ARTIKEL_ID FROM RECEPTBESTALLNING JOIN RECEPTBESTALLNINGRAD ON RECEPTBESTALLNING.id = RECEPTBESTALLNINGRAD.bestallning_id      INNER JOIN ERPARTIKEL ON RECEPTBESTALLNINGRAD.artikel_id = ERPARTIKEL.erpId where ERPARTIKEL.varunummer = @SupplierArticleId;') rex ON rex.salesOrderId = CustomerOrders.CustomerOrderId and rex.erpId = CustomerOrderLines.ArticleId WHERE AllArticles.SupplierArticleID = @SupplierArticleId AND CustomerOrders.StoreNo = @StoreNo and rex.varunummer = @SupplierArticleId and CustomerOrders.CustomerOrderStatus <> 80 and CustomerOrderStatus <> 99 order by CustomerOrders.CustomerOrderNo DESC";

            connection = new SqlConnection(_connectionString);
            try
            {
                connection.Open();
                command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@StoreNo", storeID);
                command.Parameters.AddWithValue("@ArticleNo", SupplierArticleId);                

                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    result = "CustomerOrderStatus: " + dataReader["CustomerOrderStatus"].ToString() + "\n" +
                             "SupplierArticleId: " + dataReader["SupplierArticleId"].ToString() + "\n" +
                             "ArticleId: " + dataReader["ArticleId"].ToString() + "\n" +
                             "OrderId: " + dataReader["ExternalOrderId"].ToString() + "\n" +
                             "RXStatus: " + dataReader["receptBestallningsRadStatus_id"].ToString() + "\n" +
                             "+RXStatus: " + dataReader["receptBestallningsStatus_id"].ToString() + "\n" +
                             "ReservedStockQty: " + dataReader["ReservedStockQty"].ToString() + "\n" +
                             "ReservedStockInOrderQty: " + dataReader["ReservedStockInOrderQty"].ToString() + "\n";
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
                result = errorMessages.ToString();
            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }
            return result;
        }

        private bool WithErrors()
        {
            if (storeIdBox.Text.Trim() == String.Empty)
                return true;
            if (articleIDBox.Text.Trim() == String.Empty)
                return true;
            return false;
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}