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
            resultBox.Text = sqlQueryMain(int.Parse(storeIdBox.Text), int.Parse(SupplierArticleIDBox.Text));
        }

        private string sqlQueryMain(int storeID, int SupplierArticleId)
        {
            string result = null;
            string sql = null;
            SqlConnection connection;

            sql = " Select TOP 1000 CustomerOrderStatus,CustomerOrderLines.ArticleID,CustomerOrderLineStatus,ExternalOrderId,OrderedQty,ReceivedQty,rex.* FROM CustomerOrders INNER JOIN CustomerOrderLines ON CustomerOrders.CustomerOrderNo = CustomerOrderLines.CustomerOrderNo INNER JOIN AllArticles ON AllArticles.ArticleId = CustomerOrderLines.ArticleID FULL JOIN openquery (MYSQL, 'SELECT RECEPTBESTALLNING.salesOrderId, RECEPTBESTALLNING.id, RECEPTBESTALLNING.StoreNo, ERPARTIKEL.erpId,ERPARTIKEL.varunummer,  RECEPTBESTALLNING.receptBestallningsStatus_id, RECEPTBESTALLNINGRAD.ARTIKEL_ID FROM RECEPTBESTALLNING JOIN RECEPTBESTALLNINGRAD ON RECEPTBESTALLNING.id = RECEPTBESTALLNINGRAD.bestallning_id INNER JOIN ERPARTIKEL ON RECEPTBESTALLNINGRAD.artikel_id = ERPARTIKEL.erpId where ERPARTIKEL.varunummer = " + SupplierArticleId + ";') rex ON rex.salesOrderId = CustomerOrders.CustomerOrderId and rex.erpId = CustomerOrderLines.ArticleId WHERE AllArticles.SupplierArticleID = " + SupplierArticleId + " AND CustomerOrders.StoreNo = " + storeID + " and rex.varunummer = " + SupplierArticleId + " and CustomerOrders.CustomerOrderStatus <> 80 and CustomerOrderStatus <> 99 order by CustomerOrders.CustomerOrderNo DESC " +
                  " Select AllArticles.ArticleID, AllArticles.SupplierArticleID, StoreArticleInfos.ArticleNo, ReservedStockQty, InStockQty, ReservedStockInOrderQty from StoreArticleInfos inner join AllArticles on AllArticles.ArticleNo = StoreArticleInfos.ArticleNo  where StoreNo = " + storeID + " and AllArticles.SupplierArticleID = " + SupplierArticleId + " ";

            connection = new SqlConnection(_connectionString);
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.Text;
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
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