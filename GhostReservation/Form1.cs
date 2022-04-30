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
                articleIDBox.Text = sqlQueryArticleID(SupplierArticleIDBox.Text);
            }
            else if (WithErrors())
            {
                resultBox.Text = "One or several fields are empty";
            }
            else
            {
                resultBox.Text = sqlQueryMain(storeIdBox.Text, articleIDBox.Text);
            }
        }

        private string sqlQueryArticleID(string supplierArticleID)
        {
            string result = null;
            string connectionString = null;
            string sql = null;
            SqlConnection connection;
            SqlCommand command;
            SqlDataReader dataReader;            
            sql = "select SupplierArticleId from AllArticles where ArticleId = @SupplierArticleID";

            connection = new SqlConnection(_connectionString);
            try
            {
                connection.Open();
                command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@SupplierArticleID", supplierArticleID);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    result = dataReader["ArticleId"].ToString();
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

        private string sqlQueryMain(string storeID, string articleId)
        {
            string result = null;
            string connectionString = null;
            string sql = null;
            SqlConnection connection;
            SqlCommand command;
            SqlDataReader dataReader;            
            sql = 
               "SELECT TOP 1000 CustomerOrderStatus, ArticleID, CustomerOrderLineStatus, ExternalOrderId, OrderedQty, ReceivedQty,  rex.* FROM customerorders co with(nolock) JOIN customerorderlines col with(nolock) ON co.CustomerOrderNo = col.CustomerOrderNo JOIN openquery ('MYSQL-PHARMASUITE', 'SELECT RB.bestallningsDatum, RB.salesOrderId,ERPARTIKEL.varunummer, RBR.receptBestallningsRadStatus_id, RB.receptBestallningsStatus_id, RBR.ARTIKEL_ID FROM RECEPTBESTALLNING RB JOIN RECEPTBESTALLNINGRAD RBR ON RB.id = RBR.bestallning_id INNER JOIN ERPARTIKEL ON RBR.artikel_id = ERPARTIKEL.id where ERPARTIKEL.varunummer = @SupplierArticleId; ') rex ON rex.salesOrderId = co.CustomerOrderID AND rex.ARTIKEL_ID = col.ArticleID WHERE COL.ArticleID=@ArticleId AND co.StoreNo = @StoreNo and rex.ARTIKEL_ID = @ArticleId and co.CustomerOrderStatus <> 80 and CustomerOrderStatus <> 99 order by CO.CustomerOrderNo DESC" +
               "select AllArticles.ArticleID, AllArticles.SupplierArticleID, StoreArticleInfos.ArticleNo, ReservedStockQty, TotalStockQty, InStockQty, StockInOrderQty, ReservedStockInOrderQty, AvailableStockQty from StoreArticleInfos with(nolock) inner join AllArticles with(nolock) on AllArticles.ArticleNo = StoreArticleInfos.ArticleNo where StoreNo = @StoreNo and StoreArticleInfos.ArticleNo = @ArticleNo" +
               "select top 5 * from StockAdjustments with(nolock) where StoreNo = @StoreNo and ArticleNo = @ArticleNo order by AdjustmentDate desc";
            connection = new SqlConnection(_connectionString);
            try
            {
                connection.Open();
                command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@StoreNo", storeID);
                command.Parameters.AddWithValue("@ArticleNo", articleId);
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