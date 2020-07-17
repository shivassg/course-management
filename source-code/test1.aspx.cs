﻿using log4net;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class test1 : System.Web.UI.Page
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    int userId = 15;
    string constr = ConfigurationManager.ConnectionStrings["MySql_ConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            //DataTable dt = new DataTable();
            //dt.Columns.AddRange(new DataColumn[3] { new DataColumn("Id"), new DataColumn("Name"), new DataColumn("Country") });
            //dt.Rows.Add(1, "John Hammond", "United States");
            //dt.Rows.Add(2, "Mudassar Khan", "India");
            //dt.Rows.Add(3, "Suzanne Mathews", "France");
            //dt.Rows.Add(4, "Robert Schidner", "Russia");
            //GridView1.DataSource = dt;
            BindGridView();



        }
    }

    protected void BindGridView()
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(constr))
            {
                connection.Open();
                //Select details id in tblUserMaster table
                //string strcmd = "SELECT id,uid ,amount,status ,case when approve is null then 'Pending' else 'Approved' end as approve,createdDate,modifiedDate,createdBy,updatedBy  FROM tblCustomPayment order by id desc";
                string strcmd = "SELECT B.userId, D.emailId, A.courseId, A.courseNumber, A.courseName, A.courseLevel, A.credits, C.deptName, D.fName, D.lName, case when courseStatus is False then 'Decline' else 'Approved' end as courseStatus from tblcourse as A " +
                    "LEFT OUTER JOIN tblregistration as B on B.courseId = A.courseId " +
                    "LEFT OUTER JOIN tbldepartment as C on C.deptId = A.deptId " +
                    "LEFT OUTER JOIN tbluser as D on D.userId = B.userId " +
                    "WHERE B.status = 1";

                //create a dataset object and fill it 
                MySqlDataAdapter da = new MySqlDataAdapter(strcmd, connection);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {

                    gridCourseStatus.DataSource = ds;
                    gridCourseStatus.DataBind();
                }
                else
                {
                    //div_msg.Visible = true;
                    //div_msg.InnerText = "No records found...... ";
                    gridCourseStatus.DataSource = null;
                    gridCourseStatus.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            //div_msg.Visible = true;
            //div_msg.Attributes["class"] = "alert alert-danger";
            //div_msg.InnerHtml = "Something went wrong. Please try again......";
        }
    }

    protected void gridCourseStatus_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "false")
        {
            using (MySqlConnection connection = new MySqlConnection(constr))
            {
                connection.Open();
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                string uid = commandArgs[0];
                string courseId = commandArgs[1];
                int count = 0;
                string strcmd1 = "update tblregistration set courseStatus = 1 where userId = @userId and courseId = @courseId";
                MySqlCommand cmd = new MySqlCommand(strcmd1, connection);
                cmd.Parameters.AddWithValue("@userId", uid);
                cmd.Parameters.AddWithValue("@courseId", courseId);
                count = cmd.ExecuteNonQuery();
                if (count > 0)
                {



                    BindGridView();


                }
            }


        }
        else
        {
            using (MySqlConnection connection = new MySqlConnection(constr))
            {
                connection.Open();
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                string uid = commandArgs[0];
                string courseId = commandArgs[1];
                int count = 0;
                string strcmd1 = "update tblregistration set courseStatus = 0 where userId = @userId and courseId = @courseId";
                MySqlCommand cmd = new MySqlCommand(strcmd1, connection);
                cmd.Parameters.AddWithValue("@userId", uid);
                cmd.Parameters.AddWithValue("@courseId", courseId);
                count = cmd.ExecuteNonQuery();
                if (count > 0)
                {



                    BindGridView();


                }
            }
        }
    }

    protected void gridCourseStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridCourseStatus.PageIndex = e.NewPageIndex;
        BindGridView();
    }
}