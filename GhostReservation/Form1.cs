using System.Data.SqlClient;
using System.Text;

namespace GhostReservation
{
    public partial class Form1 : Form
    {
        StringBuilder errorMessages = new StringBuilder();

        public Form1()
        {
            InitializeComponent();
        }


        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            if (WithErrors())
            {
                resultBox.Text = "One or several fields are empty";
            }
            else
            {
                resultBox.Text = sqlQuery(storeIdBox.Text, articleIDBox.Text);
            }
            
        }

        private string sqlQuery(string storeID, string articleId)
        {

            string result = null;
            string connectionString = null;
            SqlConnection connection;
            SqlCommand command;
            string sql = null;
            SqlDataReader dataReader;
            connectionString = 
                "Data Source=ServerName;" +
                "Initial Catalog=DatabaseName;" +
                "User ID=UserName;" +
                "Password=Password";
            sql = $"DECLARE @StoreNo as int" +
               "DECLARE @Article as int" +
               "DECLARE @Articleno as int" +
               "DECLARE @Article as varchar (20)" +
               "set @StoreNo = " + storeID +
               "set @SupplierArticleId = " + articleId +
               "set @ArticleId = (Select ArticleID from AllArticles where SupplierArticleID = @SupplierArticleId)" +
               "set @Articleno = (Select ArticleNo from AllArticles where ArticleId = @ArticleId)" +
               "SELECT TOP 1000 CustomerOrderStatus, ArticleID, CustomerOrderLineStatus, ExternalOrderId, OrderedQty, ReceivedQty,  rex.* FROM customerorders co with(nolock) JOIN customerorderlines col with(nolock) ON co.CustomerOrderNo = col.CustomerOrderNo JOIN openquery ('MYSQL-PHARMASUITE', 'SELECT RB.bestallningsDatum,       RB.salesOrderId,ERPARTIKEL.varunummer, RBR.receptBestallningsRadStatus_id, RB.receptBestallningsStatus_id, RBR.ARTIKEL_ID FROM RECEPTBESTALLNING RB JOIN RECEPTBESTALLNINGRAD RBR ON RB.id = RBR.bestallning_id INNER JOIN ERPARTIKEL ON RBR.artikel_id = ERPARTIKEL.id where ERPARTIKEL.varunummer = " + articleId +"; ') rex ON rex.salesOrderId = co.CustomerOrderID AND rex.ARTIKEL_ID = col.ArticleID WHERE COL.ArticleID=@ArticleId AND co.StoreNo = @StoreNo and rex.ARTIKEL_ID = @ArticleId and co.CustomerOrderStatus <> 80 and CustomerOrderStatus <> 99 order by CO.CustomerOrderNo DESC" +
               "select AllArticles.ArticleID, AllArticles.SupplierArticleID, StoreArticleInfos.ArticleNo, ReservedStockQty, TotalStockQty, InStockQty, StockInOrderQty, ReservedStockInOrderQty, AvailableStockQty from StoreArticleInfos with(nolock) inner join AllArticles with(nolock) on AllArticles.ArticleNo = StoreArticleInfos.ArticleNo where StoreNo = @Storeno and StoreArticleInfos.ArticleNo = @Articleno" +
               "select top 5 * from StockAdjustments with(nolock) where StoreNo = @Storeno and ArticleNo = @Articleno order by AdjustmentDate desc";
            connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                command = new SqlCommand(sql, connection);
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
                result =errorMessages.ToString();
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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            resultBox.SelectAll();
            resultBox.Copy();
            resultBox.DeselectAll();
        }
    }
}