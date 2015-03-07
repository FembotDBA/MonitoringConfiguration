using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MonitoringConfiguration.Properties;
using MonitoringModel;

namespace MonitoringConfiguration
{
    public partial class Config : Form
    {
        //List<MonitoredTask> monitoredTasks = new List<MonitoredTask>();
        public Config()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            List<MonitoredServer> monitoredServers = GetMonitoredServers(txtConnectionString.Text);
            FillListView(monitoredServers);

            List<MonitoredTask> monitoredTasks = GetMonitoredTasks(txtConnectionString.Text);
            lvMonitoredTasks.Objects = monitoredTasks;
            lvMonitoredTasks.AutoResizeColumns();
        }

        static List<MonitoredTask> GetMonitoredTasks(string connectionString)
        {
            
            List<MonitoredTask> MonitoredTasks = new List<MonitoredTask>();
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT MonitoredTaskId, MonitoredTaskName FROM dbo.MonitoredTask", connection))
                {

                    SqlDataReader reader = command.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            MonitoredTasks.Add(new MonitoredTask
                            {
                                MonitoredTaskId = reader["MonitoredTaskId"].ToString()
                                ,
                                MonitoredTaskName = reader["MonitoredTaskName"].ToString()
                            });
                        }
                    }
                    finally
                    {
                        reader.Close();
                    }
                }
            }

            return MonitoredTasks;
        }

        private void FillListView(List<MonitoredServer> monitoredServers)
        {

            lvServers.Columns.Add("Server Name");
            lvServers.Columns.Add("Server Id");
            lvServers.Columns.Add("Connection String");
            foreach(MonitoredServer monitoredServer in monitoredServers)
            {
                //ListViewItem listItem = new ListViewItem(monitoredServer.Id.ToString());
                ListViewItem listItem = new ListViewItem(monitoredServer.ServerName) { Tag = monitoredServer };
                listItem.SubItems.Add(monitoredServer.Id.ToString());
                listItem.SubItems.Add(monitoredServer.ConnString.ToString());
                lvServers.Items.Add(listItem);
            }
            lvServers.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }
        
        static List<MonitoredServer> GetMonitoredServers(string connectionString)
        {
            List<MonitoredServer> MonitoredServers = new List<MonitoredServer>();
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM dbo.MonitoredServer", connection))
                {

                    SqlDataReader reader = command.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            MonitoredServers.Add(new MonitoredServer
                            {
                                ServerName = reader["ServerName"].ToString()
                                ,
                                Id = Int32.Parse(reader["ServerId"].ToString())
                                ,
                                ConnString = reader["ConnString"].ToString()
                            });
                        }
                    }
                    finally
                    {
                        reader.Close();
                    }
                }
            }

            return MonitoredServers;
        }

        private void btnTask_Click(object sender, EventArgs e)
        {
            if (lvMonitoredTasks.CheckedObjects.Count > 0)
            {
                //List<MonitoredTask> checkedMonitoredTasks = (List<MonitoredTask>)lvMonitoredTasks.CheckedObjects;
                ArrayList arrCheckedMonitoredTasks = (ArrayList)lvMonitoredTasks.CheckedObjects;
                getItemInfo(arrCheckedMonitoredTasks);
            }
        }

        private void getItemInfo(ArrayList checkedMonitoredTasks)
        {
            //make sure row is selected
            if (lvServers.SelectedItems.Count == 1)
            {
                foreach (MonitoredTask monitoredTask in checkedMonitoredTasks)
                {
                    runTask(Int32.Parse(lvServers.SelectedItems[0].SubItems[1].Text), lvServers.SelectedItems[0].SubItems[2].Text, monitoredTask.MonitoredTaskName);
                }
            }
        }

        private void runTask(int id, string connString, string taskName)
        {

            string monitoredConnString = Settings.Default.MonitoredDBConnString;
            string sql = "";
            string table = "";
            string saveproc = "";

            DataTable dtResults = new DataTable();

            //read sql to execute from db
            using (SqlConnection connMonitorDB = new SqlConnection(monitoredConnString))
            {
                //using (SqlCommand command = new SqlCommand("SELECT * FROM dbo.MonitoredTask WHERE MonitoredTaskName = 'File Latency'", connMonitorDB))
                //using (SqlCommand command = new SqlCommand("SELECT * FROM dbo.MonitoredTask WHERE MonitoredTaskName = 'Error Log'", connMonitorDB))
                using (SqlCommand command = new SqlCommand("SELECT * FROM dbo.MonitoredTask WHERE MonitoredTaskName = '" + taskName + "'", connMonitorDB))
                {
                    connMonitorDB.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            sql = reader["MonitoredSQL"].ToString().Replace("%ServerId%",id.ToString());
                            table = reader["DataTable"].ToString();
                            saveproc = reader["ProcToSaveData"].ToString();
                        }
                    }
                    finally
                    {
                        reader.Close();
                    }
                }
            }
           
            //execute task sql on remote server and save results to Monitoring DB
            using (SqlConnection connRemote = new SqlConnection(connString))
            {

                
                using (SqlCommand commTask = new SqlCommand(sql, connRemote))
                {
                    try
                    {
                        connRemote.Open();


                        using (SqlDataAdapter da = new SqlDataAdapter(commTask))
                        {

                            da.Fill(dtResults);
                            connRemote.Close();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Error connecting to remote host, check connection string, access or firewall.");
                    }

                }
            }
             
            using (SqlConnection connSaveResults = new SqlConnection(monitoredConnString))
            {
                using (SqlCommand commSaveResults = connSaveResults.CreateCommand())
                {
                    //save results to table
                    //commSaveResults.CommandText = "dbo.SaveFileLatency";
                    commSaveResults.CommandText = saveproc;
                    commSaveResults.CommandType = CommandType.StoredProcedure;

                    SqlParameter parameter;
                    parameter = commSaveResults.Parameters.AddWithValue("@datatable", dtResults);
                    parameter.SqlDbType = SqlDbType.Structured;
                    //parameter.TypeName = "data.FileLatency";
                    parameter.TypeName = table;

                    connSaveResults.Open();
                    commSaveResults.ExecuteNonQuery();
                    connSaveResults.Close();

                    lblResult.Text = "Task Complete.";
                }
            }
            
            
        }

        private void txtConnectionString_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.MonitoredDBConnString = txtConnectionString.Text;
            Settings.Default.Save();
        }

        private void Config_Load(object sender, EventArgs e)
        {
            txtConnectionString.Text = Settings.Default.MonitoredDBConnString;
        }

        
    }
}
