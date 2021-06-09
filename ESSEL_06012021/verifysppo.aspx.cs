using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;


public partial class verifysppo : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
    SqlCommand cmd = new SqlCommand();
    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Purchase");
        lbl.Attributes.Add("class", "active");
        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx"); 
        if (!IsPostBack)
        {
            fillgrid();
            trpovalue.Visible = false;
            trpovalueprint.Visible = false;
        }
      
    }

    private void fillgrid()
    {
        try
        {
            if (Session["roles"].ToString() == "Project Manager")
                //da = new SqlDataAdapter("select pono,vendor_id,cc_code,dca_code,subdca_code,replace((isnull(po_value,0)),'.oooo','.oo')as po_value,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,remarks from SPPO where status='1'", con);
                da = new SqlDataAdapter("select pono,s.vendor_id,cc_code,dca_code,subdca_code,CAST((isnull(po_value,0)) AS Decimal(20,2))as po_value,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,LEFT(remarks, CHARINDEX('$',remarks+'$')-1) as [remarks],vendor_name from SPPO s join vendor v on s.vendor_id=v.vendor_id where s.cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "') and  s.status='1'", con);
            else if (Session["roles"].ToString() == "PurchaseManager")
                //da = new SqlDataAdapter("select pono,vendor_id,cc_code,dca_code,subdca_code,replace((isnull(po_value,0)),'.oooo','.oo')as po_value,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,remarks from SPPO where status='1'", con);
                da = new SqlDataAdapter("select pono,s.vendor_id,s.cc_code,dca_code,subdca_code,CAST((isnull(po_value,0)) AS Decimal(20,2))as po_value,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,LEFT(remarks, CHARINDEX('$',remarks+'$')-1) as [remarks],vendor_name from SPPO s join vendor v on s.vendor_id=v.vendor_id join Cost_Center cc on s.cc_code=cc.cc_code where s.status='1P' and cc.cc_type='Performing' union all select pono,s.vendor_id,s.cc_code,dca_code,subdca_code,CAST((isnull(po_value,0)) AS Decimal(20,2))as po_value,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,LEFT(remarks, CHARINDEX('$',remarks+'$')-1) as [remarks],vendor_name from SPPO s join vendor v on s.vendor_id=v.vendor_id join Cost_Center cc on s.cc_code=cc.cc_code where s.status='1NP' and cc.cc_type='Non-Performing'", con);
            else if (Session["roles"].ToString() == "Chief Material Controller")
                //da = new SqlDataAdapter("select pono,vendor_id,cc_code,dca_code,subdca_code,replace((isnull(po_value,0)),'.oooo','.oo')as po_value,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,remarks from SPPO where status='1'", con);
                da = new SqlDataAdapter("select pono,s.vendor_id,s.cc_code,dca_code,subdca_code,CAST((isnull(po_value,0)) AS Decimal(20,2))as po_value,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,LEFT(remarks, CHARINDEX('$',remarks+'$')-1) as [remarks],vendor_name from SPPO s join vendor v on s.vendor_id=v.vendor_id join Cost_Center cc on s.cc_code=cc.cc_code where s.status='1A' and cc.cc_type='Performing' union all select pono,s.vendor_id,s.cc_code,dca_code,subdca_code,CAST((isnull(po_value,0)) AS Decimal(20,2))as po_value,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,LEFT(remarks, CHARINDEX('$',remarks+'$')-1) as [remarks],vendor_name from SPPO s join vendor v on s.vendor_id=v.vendor_id join Cost_Center cc on s.cc_code=cc.cc_code where s.status='1A' and cc.cc_type='Non-Performing'", con);
            else if (Session["roles"].ToString() == "HoAdmin")
            //da = new SqlDataAdapter("select pono,vendor_id,cc_code,dca_code,subdca_code,replace((isnull(po_value,0)),'.oooo','.oo')as po_value,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,remarks from SPPO where status='1'", con);
            {
                //da = new SqlDataAdapter("select pono,s.vendor_id,cc_code,dca_code,subdca_code,CAST((isnull(po_value,0)) AS Decimal(20,2))as po_value,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,LEFT(remarks, CHARINDEX('$',remarks+'$')-1) as [remarks],vendor_name from SPPO s join vendor v on s.vendor_id=v.vendor_id where s.status='1A'", con);
                da = new SqlDataAdapter("select pono,s.vendor_id,s.cc_code,dca_code,subdca_code,CAST((isnull(po_value,0)) AS Decimal(20,2))as po_value,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,LEFT(remarks, CHARINDEX('$',remarks+'$')-1) as [remarks],vendor_name from SPPO s join vendor v on s.vendor_id=v.vendor_id join Cost_Center cc on s.cc_code=cc.cc_code where s.status='1AN' and cc.cc_type='Performing' union all select pono,s.vendor_id,s.cc_code,dca_code,subdca_code,CAST((isnull(po_value,0)) AS Decimal(20,2))as po_value,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,LEFT(remarks, CHARINDEX('$',remarks+'$')-1) as [remarks],vendor_name from SPPO s join vendor v on s.vendor_id=v.vendor_id join Cost_Center cc on s.cc_code=cc.cc_code where s.status='1AN' and cc.cc_type='Non-Performing'", con);
            }
            else if (Session["roles"].ToString() == "SuperAdmin")
                //da = new SqlDataAdapter("select pono,vendor_id,cc_code,dca_code,subdca_code,replace((isnull(po_value,0)),'.oooo','.oo')as po_value,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,remarks from SPPO where status='2'", con);
                da = new SqlDataAdapter("select pono,s.vendor_id,cc_code,dca_code,subdca_code,CAST((isnull(po_value,0)) AS Decimal(20,2))as po_value,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,LEFT(remarks, CHARINDEX('$',remarks+'$')-1) as [remarks],vendor_name from SPPO s join vendor v on s.vendor_id=v.vendor_id where s.status='2'", con);
            da.Fill(ds, "limit");
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
            pnlraisepo.Visible = false;
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (Session["roles"].ToString() == "Project Manager")
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //LinkButton lkb = e.Row.FindControl("linkDeleteCust") as LinkButton;
                //lkb.Enabled = false;
                //e.Row.Cells[11].Enabled = false;
            }
 
        }
    }
  
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        
    }
   
   
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            DateTime myDate = DateTime.Now.AddHours(11).AddMinutes(30).AddSeconds(8);
            GridViewRow gvr = (GridViewRow)GridView1.Rows[e.RowIndex];
            Label lblpovalue = (Label)gvr.FindControl("lblpovalue");
          
            Label lblpodate = (Label)gvr.FindControl("podate");
            Label lb = (Label)gvr.FindControl("lblpono");
            string cccode = GridView1.Rows[e.RowIndex].Cells[4].Text;
            string dcacode = GridView1.Rows[e.RowIndex].Cells[5].Text;

            cmd = new SqlCommand("sp_deletesppo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@cccode", cccode);
            cmd.Parameters.AddWithValue("@dcacode", dcacode);
            cmd.Parameters.AddWithValue("@podate", lblpodate.Text);
            cmd.Parameters.AddWithValue("@povalue", lblpovalue.Text);
            cmd.Parameters.AddWithValue("@User", Session["roles"].ToString());
            cmd.Parameters.AddWithValue("@rejdate", myDate);
            cmd.Parameters.AddWithValue("@PONO", lb.Text);
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            JavaScript.UPAlert(Page, msg);
            con.Close();
            fillgrid();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
      
    }


    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            trpovalue.Visible = false;
            trpovalueprint.Visible = false;
            ViewState["ponumber"] = GridView1.SelectedValue.ToString();
            pnlraisepo.Visible = true;
            poppo.Show();
            da = new SqlDataAdapter("select s.pono,s.vendor_id,v.vendor_name,v.address,REPLACE(CONVERT(VARCHAR(11),s.po_date, 106), ' ', '-')as po_date,s.remarks,s.cc_code,s.dca_code,CAST(s.po_value AS Decimal(20,2)) as po_value,REPLACE(CONVERT(VARCHAR(11),Completion_date, 106), ' ', '-')as Completion_date from SPPO s join vendor v on s.vendor_id=v.vendor_id where  s.pono='" + GridView1.SelectedValue.ToString() + "'", con);
            da.Fill(ds, "pono");
            if (ds.Tables["pono"].Rows.Count > 0)
            {
                lblvenname.Text = ds.Tables["pono"].Rows[0][2].ToString();
                lblvennameprint.Text = ds.Tables["pono"].Rows[0][2].ToString();
                lblvenaddress.Text = ds.Tables["pono"].Rows[0][3].ToString();
                lblvenaddressprint.Text = ds.Tables["pono"].Rows[0][3].ToString();
                txtpono.Text = ds.Tables["pono"].Rows[0][0].ToString();
                txtponoprint.Text = ds.Tables["pono"].Rows[0][0].ToString();
                txtpodate.Text = ds.Tables["pono"].Rows[0][4].ToString();
                txtpodateprint.Text = ds.Tables["pono"].Rows[0][4].ToString();
                txtcccode.Text = ds.Tables["pono"].Rows[0][6].ToString();
                txtcccodeprint.Text = ds.Tables["pono"].Rows[0][6].ToString();
                txtdcacode.Text = ds.Tables["pono"].Rows[0][7].ToString();
                txtdcacodeprint.Text = ds.Tables["pono"].Rows[0][7].ToString();
                txtvencode.Text = ds.Tables["pono"].Rows[0][1].ToString();
                txtvencodeprint.Text = ds.Tables["pono"].Rows[0][1].ToString();
                txtpocompdate.Text = ds.Tables["pono"].Rows[0][9].ToString();
                txtpocompdateprint.Text = ds.Tables["pono"].Rows[0][9].ToString();
                //txtremarks.Text = ds.Tables["pono"].Rows[0][5].ToString();
                //txtnewpo.Text = ds.Tables["pono"].Rows[0][8].ToString();
                ViewState["cccode"] = ds.Tables["pono"].Rows[0][6].ToString();
                ViewState["dcacode"] = ds.Tables["pono"].Rows[0][7].ToString();
                ViewState["po_value"] = ds.Tables["pono"].Rows[0][8].ToString();
                da = new SqlDataAdapter("select cc_type from cost_center where cc_code='" + ds.Tables["pono"].Rows[0][6].ToString() + "'", con);
                da.Fill(ds, "cctype");
                ViewState["cctype"] = ds.Tables["cctype"].Rows[0][0].ToString();
            }

            fillprintgrid();
        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }

    }
    protected void buttonprint_Click(object sender, EventArgs e)
    {
        string Ids = "";
        string Descs = "";
        string MDescs = "";
        try
        {
            DateTime myDate = DateTime.Now.AddHours(11).AddMinutes(30).AddSeconds(8);
            foreach (GridViewRow record in grdbill.Rows)
            {
                if ((record.FindControl("chkSelect") as CheckBox).Checked)
                {
                    TextBox txtdesc = (TextBox)record.FindControl("txtdesc");
                    string desc = txtdesc.Text;

                    if (desc != "")
                    {
                        Ids = Ids + grdbill.DataKeys[record.RowIndex]["ID"].ToString() + ",";
                        Descs = Descs + desc + ",";
                    }
                }
            }
            foreach (GridViewRow record in grdpodesc.Rows)
            {
                if ((record.FindControl("chkSelectterms") as CheckBox).Checked)
                {
                    TextBox txtterm = (TextBox)record.FindControl("txtterms");
                    string termdesc = txtterm.Text;
                    if (termdesc != "")
                    {
                        MDescs = MDescs + termdesc + "$";
                    }
                }
            }

            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_ServiceProviderPO";
            cmd.Parameters.Add(new SqlParameter("@Ids", Ids));
            cmd.Parameters.Add(new SqlParameter("@Descs", Descs));
            cmd.Parameters.Add(new SqlParameter("@Terms", MDescs));
            cmd.Parameters.Add(new SqlParameter("@cccode", ViewState["cccode"].ToString()));
            //cmd.Parameters.Add(new SqlParameter("@dcacode", ViewState["dcacode"].ToString()));
            //cmd.Parameters.Add(new SqlParameter("@oldpo", ViewState["po_value"]));
            cmd.Parameters.Add(new SqlParameter("@PONO", txtpono.Text));
            cmd.Parameters.Add(new SqlParameter("@podate", txtpodate.Text));
            //cmd.Parameters.Add(new SqlParameter("@unit", (record.FindControl("txtunit") as TextBox).Text));
            //cmd.Parameters.Add(new SqlParameter("@rate", (record.FindControl("txtrate") as TextBox).Text));
            //cmd.Parameters.Add(new SqlParameter("@quantity", (record.FindControl("txtquantity") as TextBox).Text));
            //cmd.Parameters.Add(new SqlParameter("@povalue", (record.FindControl("txtprintpovalue") as TextBox).Text));
            //cmd.Parameters.Add(new SqlParameter("@remark", (record.FindControl("txtprintremarks") as TextBox).Text));
            cmd.Parameters.Add(new SqlParameter("@User", Session["user"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@roles", Session["roles"].ToString()));
            if (Session["roles"].ToString() == "HoAdmin")
                cmd.Parameters.Add(new SqlParameter("@mdate", myDate));
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            //string msg = "";
            if (msg == "PO Updated" || msg == "PO Verified")
                JavaScript.UPAlertRedirect(Page, msg, "verifysppo.aspx");
            else
                JavaScript.UPAlert(Page, msg);
            con.Close();

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }

    }
   
    public void fillprintgrid()
    {
        da = new SqlDataAdapter("select cc_type from cost_center where cc_code='" + ViewState["cccode"].ToString() + "'", con);
        da.Fill(ds, "CCode");
        //da = new SqlDataAdapter("Select unit,CAST(rate AS Decimal(20,2)) as rate,quantity,remarks,CAST((isnull(po_value,0)) AS Decimal(20,2))as po_value from sppo where pono='" + ViewState["ponumber"].ToString() + "' ", con);
        if (Session["roles"].ToString() == "Project Manager")
        {
            da = new SqlDataAdapter("select ID,unit,CAST(rate AS Decimal(20,2)) as rate,quantity,Description,CAST((isnull(Amount,0)) AS Decimal(20,2))as Amount,CAST(ClientRate AS Decimal(20,2)) as ClientRate,CAST(PRWRate AS Decimal(20,2)) as PRWRate from sppo_items where  pono='" + ViewState["ponumber"].ToString() + "' and Type='Direct' and status='1';select CAST((isnull(po_value,0)) AS Decimal(20,2))as po_value,remarks from sppo where pono='" + ViewState["ponumber"].ToString() + "' ", con);
        }
        if (Session["roles"].ToString() == "PurchaseManager")
        {
            if (ds.Tables["CCode"].Rows[0].ItemArray[0].ToString() == "Performing")
                da = new SqlDataAdapter("select ID, unit,CAST(rate AS Decimal(20,2)) as rate,quantity,Description,CAST((isnull(Amount,0)) AS Decimal(20,2))as Amount,CAST(ClientRate AS Decimal(20,2)) as ClientRate,CAST(PRWRate AS Decimal(20,2)) as PRWRate from sppo_items where  pono='" + ViewState["ponumber"].ToString() + "' and Type='Direct' and status='1P';select CAST((isnull(po_value,0)) AS Decimal(20,2))as po_value,remarks from sppo where pono='" + ViewState["ponumber"].ToString() + "' and status='1P' ", con);
            else
                da = new SqlDataAdapter("select ID, unit,CAST(rate AS Decimal(20,2)) as rate,quantity,Description,CAST((isnull(Amount,0)) AS Decimal(20,2))as Amount,CAST(ClientRate AS Decimal(20,2)) as ClientRate,CAST(PRWRate AS Decimal(20,2)) as PRWRate from sppo_items where  pono='" + ViewState["ponumber"].ToString() + "' and Type='Direct' and status='1NP';select CAST((isnull(po_value,0)) AS Decimal(20,2))as po_value,remarks from sppo where pono='" + ViewState["ponumber"].ToString() + "' and status='1NP' ", con);
        }
        if (Session["roles"].ToString() == "Chief Material Controller")
        {
            if (ds.Tables["CCode"].Rows[0].ItemArray[0].ToString() == "Performing")
                da = new SqlDataAdapter("select ID, unit,CAST(rate AS Decimal(20,2)) as rate,quantity,Description,CAST((isnull(Amount,0)) AS Decimal(20,2))as Amount,CAST(ClientRate AS Decimal(20,2)) as ClientRate,CAST(PRWRate AS Decimal(20,2)) as PRWRate from sppo_items where  pono='" + ViewState["ponumber"].ToString() + "' and Type='Direct' and status='1A';select CAST((isnull(po_value,0)) AS Decimal(20,2))as po_value,remarks from sppo where pono='" + ViewState["ponumber"].ToString() + "' and status='1A' ", con);
            else
                da = new SqlDataAdapter("select ID, unit,CAST(rate AS Decimal(20,2)) as rate,quantity,Description,CAST((isnull(Amount,0)) AS Decimal(20,2))as Amount,CAST(ClientRate AS Decimal(20,2)) as ClientRate,CAST(PRWRate AS Decimal(20,2)) as PRWRate from sppo_items where  pono='" + ViewState["ponumber"].ToString() + "' and Type='Direct' and status='1A';select CAST((isnull(po_value,0)) AS Decimal(20,2))as po_value,remarks from sppo where pono='" + ViewState["ponumber"].ToString() + "' and status='1A' ", con);
        }
        if (Session["roles"].ToString() == "HoAdmin")
        {
            if (ds.Tables["CCode"].Rows[0].ItemArray[0].ToString() == "Performing")
                da = new SqlDataAdapter("select ID, unit,CAST(rate AS Decimal(20,2)) as rate,quantity,Description,CAST((isnull(Amount,0)) AS Decimal(20,2))as Amount,CAST(ClientRate AS Decimal(20,2)) as ClientRate,CAST(PRWRate AS Decimal(20,2)) as PRWRate from sppo_items where  pono='" + ViewState["ponumber"].ToString() + "' and Type='Direct' and status='1AN';select CAST((isnull(po_value,0)) AS Decimal(20,2))as po_value,remarks from sppo where pono='" + ViewState["ponumber"].ToString() + "' and status='1AN' ", con);
            else
                da = new SqlDataAdapter("select ID, unit,CAST(rate AS Decimal(20,2)) as rate,quantity,Description,CAST((isnull(Amount,0)) AS Decimal(20,2))as Amount,CAST(ClientRate AS Decimal(20,2)) as ClientRate,CAST(PRWRate AS Decimal(20,2)) as PRWRate from sppo_items where  pono='" + ViewState["ponumber"].ToString() + "' and Type='Direct' and status='1AN';select CAST((isnull(po_value,0)) AS Decimal(20,2))as po_value,remarks from sppo where pono='" + ViewState["ponumber"].ToString() + "' and status='1AN' ", con);
        }
        if (Session["roles"].ToString() == "SuperAdmin")
        {
            da = new SqlDataAdapter("select ID, unit,CAST(rate AS Decimal(20,2)) as rate,quantity,Description,CAST((isnull(Amount,0)) AS Decimal(20,2))as Amount,CAST(ClientRate AS Decimal(20,2)) as ClientRate,CAST(PRWRate AS Decimal(20,2)) as PRWRate from sppo_items where  pono='" + ViewState["ponumber"].ToString() + "' and Type='Direct' and status='2';select CAST((isnull(po_value,0)) AS Decimal(20,2))as po_value,remarks from sppo where pono='" + ViewState["ponumber"].ToString() + "' ", con);
        }
        da.Fill(ds, "grid");
        if (ds.Tables["grid"].Rows.Count > 0)
        {
            if (Session["roles"].ToString() == "Project Manager")
            {
                grdbill.DataSource = ds.Tables["grid"];
                grdbill.DataBind();
                grdbillprint.DataSource = ds.Tables["grid"];
                grdbillprint.DataBind();
                trpovalue.Visible = true;
                trpovalueprint.Visible = true;
                lblpovalue.Text = ds.Tables["grid1"].Rows[0].ItemArray[0].ToString();
                lblpovalueprint.Text = ds.Tables["grid1"].Rows[0].ItemArray[0].ToString();
                if (ds.Tables["grid1"].Rows.Count > 0)
                {
                    string rolesamend = ds.Tables["grid1"].Rows[0][1].ToString().Replace("'"," ");
                    da = new SqlDataAdapter("select * from dbo.fnSplitStringsppodesc('" + rolesamend + "','$')", con);
                    da.Fill(ds, "splitamend");
                    grdpodesc.DataSource = ds.Tables["splitamend"];
                    grdpodesc.DataBind();
                    grdpodescprint.DataSource = ds.Tables["splitamend"];
                    grdpodescprint.DataBind();
                    ViewState["Curtblterm"] = ds.Tables["splitamend"];
                    //BindGridviewterm();
                }
            }
            else
            {
                grdbill.DataSource = ds.Tables["grid"];
                grdbill.DataBind();
                trpovalue.Visible = true;
                trpovalueprint.Visible = true;
                lblpovalue.Text = ds.Tables["grid1"].Rows[0].ItemArray[0].ToString();
                lblpovalueprint.Text = ds.Tables["grid1"].Rows[0].ItemArray[0].ToString();
                if (ds.Tables["grid1"].Rows.Count > 0)
                {
                    string rolesamend = ds.Tables["grid1"].Rows[0][1].ToString().Replace("'", " ");
                    da = new SqlDataAdapter("select * from dbo.fnSplitStringsppodesc('" + rolesamend + "','$')", con);
                    da.Fill(ds, "splitamend");                    
                    grdpodesc.DataSource = ds.Tables["splitamend"];
                    grdpodesc.DataBind();
                    grdpodescprint.DataSource = ds.Tables["splitamend"];
                    grdpodescprint.DataBind();
                    ViewState["Curtblterm"] = ds.Tables["splitamend"];
                    //BindGridviewterm();
                    
                }
            }
            da = new SqlDataAdapter("Select Approved_Users from sppo where pono='" + ViewState["ponumber"].ToString() + "'", con);
            da.Fill(ds, "roles");
            if (ds.Tables["roles"].Rows.Count > 0)
            {
                string rolesamend = ds.Tables["roles"].Rows[0][0].ToString().Replace("'", " ");
                da = new SqlDataAdapter("select * from dbo.fnSplitStringsppodesc('" + rolesamend + "',',')", con);
                da.Fill(ds, "splitrole");
                DataTable dtra = ds.Tables["splitrole"];
                ViewState["Curtblroles"] = dtra;
                gvusers.DataSource = dtra;
                gvusers.DataBind();
                gvusersprint.DataSource = dtra;
                gvusersprint.DataBind();
            }
        }
    }
    public int j = 0;
    public int k = 1;
    protected void gvusers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (ViewState["Curtblroles"] != null)
                {
                    DataTable Objdt = ViewState["Curtblroles"] as DataTable;
                    //k = Objdt.Rows.Count;
                    if (Objdt.Rows[j][0].ToString() != "")
                    {

                        //da = new SqlDataAdapter("select Roles, (first_name+'  '+middle_name+'  '+last_name)as name ,ur.User_Name from user_roles ur join Employee_Data ed on ur.User_Name=ed.User_Name where ur.User_Name='" + Objdt.Rows[j][0].ToString() + "'", con);
                        da = new SqlDataAdapter("select (first_name+'  '+middle_name+'  '+last_name)as name,USER_NAME from Employee_Data where USER_NAME='" + Objdt.Rows[j][0].ToString() + "'", con);
                        da.Fill(ds, "userroles");                        
                        if (k == 1)
                        {
                            e.Row.Cells[1].Text = "Sr.Accountant";
                            e.Row.Cells[0].Text = "Prepared By";
                        }
                        else if (k == 2 && ViewState["cctype"].ToString() == "Performing")
                        {
                            e.Row.Cells[1].Text = "Project Manager";
                            e.Row.Cells[0].Text = "Verified By";
                        }
                        else if (k == 3 && ViewState["cctype"].ToString() == "Performing")
                        {
                            e.Row.Cells[1].Text = "PurchaseManager";
                            e.Row.Cells[0].Text = "Verified By";
                        }
                        else if (k == 4 && ViewState["cctype"].ToString() == "Performing")
                        {
                            e.Row.Cells[1].Text = "Chief Material Controller";
                            e.Row.Cells[0].Text = "Verified By";
                        }   
                        else if (k == 5 && ViewState["cctype"].ToString() == "Performing")
                        {
                            e.Row.Cells[1].Text = "HoAdmin";
                            e.Row.Cells[0].Text = "Verified By";
                        }                       
                        else if (k == 2 && (ViewState["cctype"].ToString() == "Non-Performing" || ViewState["cctype"].ToString() == "Capital"))
                        {
                            e.Row.Cells[1].Text = "PurchaseManager";
                            e.Row.Cells[0].Text = "Verified By";
                        }
                        else if (k == 3 && (ViewState["cctype"].ToString() == "Non-Performing" || ViewState["cctype"].ToString() == "Capital"))
                        {
                            e.Row.Cells[0].Text = "Verified By";
                            e.Row.Cells[1].Text = "Chief Material Controller";
                        }
                        else if (k == 4 && (ViewState["cctype"].ToString() == "Non-Performing" || ViewState["cctype"].ToString() == "Capital"))
                        {
                            e.Row.Cells[0].Text = "Verified By";
                            e.Row.Cells[1].Text = "HoAdmin";
                        }
                        e.Row.Cells[2].Text = ds.Tables["userroles"].Rows[j].ItemArray[0].ToString();                       

                    }
                }
                j = j + 1;
                k = k + 1;
            }

        }
        catch (Exception ex)
        {
            JavaScript.UPAlert(Page, Utilities.CatchException(ex));
        }

    }

    public int L = 0;
    public int M = 1;
    protected void gvusersprint_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (ViewState["Curtblroles"] != null)
                {
                    DataTable Objdt = ViewState["Curtblroles"] as DataTable;
                    //k = Objdt.Rows.Count;
                    if (Objdt.Rows[L][0].ToString() != "")
                    {

                        //da = new SqlDataAdapter("select Roles, (first_name+'  '+middle_name+'  '+last_name)as name ,ur.User_Name from user_roles ur join Employee_Data ed on ur.User_Name=ed.User_Name where ur.User_Name='" + Objdt.Rows[j][0].ToString() + "'", con);
                        da = new SqlDataAdapter("select (first_name+'  '+middle_name+'  '+last_name)as name,USER_NAME from Employee_Data where USER_NAME='" + Objdt.Rows[L][0].ToString() + "'", con);
                        da.Fill(ds, "userroles");
                        if (M == 1)
                        {
                            e.Row.Cells[1].Text = "Sr.Accountant";
                            e.Row.Cells[0].Text = "Prepared By";
                        }
                        else if (M == 2 && ViewState["cctype"].ToString() == "Performing")
                        {
                            e.Row.Cells[1].Text = "Project Manager";
                            e.Row.Cells[0].Text = "Verified By";
                        }
                        else if (M == 3 && ViewState["cctype"].ToString() == "Performing")
                        {
                            e.Row.Cells[1].Text = "PurchaseManager";
                            e.Row.Cells[0].Text = "Verified By";
                        }
                        else if (M == 4 && ViewState["cctype"].ToString() == "Performing")
                        {
                            e.Row.Cells[1].Text = "Chief Material Controller";
                            e.Row.Cells[0].Text = "Verified By";
                        }
                        else if (M == 5 && ViewState["cctype"].ToString() == "Performing")
                        {
                            e.Row.Cells[1].Text = "HoAdmin";
                            e.Row.Cells[0].Text = "Verified By";
                        }
                        else if (M == 2 && (ViewState["cctype"].ToString() == "Non-Performing" || ViewState["cctype"].ToString() == "Capital"))
                        {
                            e.Row.Cells[1].Text = "PurchaseManager";
                            e.Row.Cells[0].Text = "Verified By";
                        }
                        else if (M == 3 && (ViewState["cctype"].ToString() == "Non-Performing" || ViewState["cctype"].ToString() == "Capital"))
                        {
                            e.Row.Cells[0].Text = "Verified By";
                            e.Row.Cells[1].Text = "Chief Material Controller";
                        }
                        else if (M == 4 && (ViewState["cctype"].ToString() == "Non-Performing" || ViewState["cctype"].ToString() == "Capital"))
                        {
                            e.Row.Cells[0].Text = "Verified By";
                            e.Row.Cells[1].Text = "HoAdmin";
                        }
                        e.Row.Cells[2].Text = ds.Tables["userroles"].Rows[L].ItemArray[0].ToString();

                    }
                }
                L = L + 1;
                M = M + 1;
            }

        }
        catch (Exception ex)
        {
            JavaScript.UPAlert(Page, Utilities.CatchException(ex));
        }

    }
    protected void grdbill_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (decimal.Parse(e.Row.Cells[7].Text) > decimal.Parse(e.Row.Cells[6].Text))   //considering index 1 is the date field
            {
                e.Row.Cells[7].ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                e.Row.Cells[7].ForeColor = System.Drawing.Color.Black;
            }

        }
    }

    //public void printgrid()
    //{
    //    da = new SqlDataAdapter("Select unit,rate,quantity,remarks,replace(po_value,'.0000','.00') as po_value from sppo where pono='" + ViewState["ponumber"].ToString() + "' ", con);
    //    da.Fill(ds, "grid");
    //    GridView2.DataSource = ds.Tables["grid"];
    //    GridView2.DataBind();


    //}

    #region Terms and Conditions start
    //protected void BindGridviewterm()
    //{
    //    DataTable dtt = new DataTable();
    //    dtt.Columns.Add("chkSelectterm", typeof(string));
    //    dtt.Columns.Add("splitdata", typeof(string)); //Descriptionterm
    //    DataRow dr = dtt.NewRow();
    //    dr["chkSelectterm"] = string.Empty;
    //    //dr["Descriptionterm"] = string.Empty;
    //    dr["splitdata"] = string.Empty;
    //    dtt.Rows.Add(dr);
    //    if (ViewState["Curtblterm"] == "")
    //    {
    //        ViewState["Curtblterm"] = dtt;
    //    }        
       
    //}
    private void AddNewRowterm()
    {
        int rowIndex = 0;

        if (ViewState["Curtblterm"] != null)
        {
            DataTable dtt = (DataTable)ViewState["Curtblterm"];
            DataRow drCurrentRow = null;
            if (dtt.Rows.Count > 0)
            {
                for (int i = 1; i <= dtt.Rows.Count; i++)
                {
                    TextBox txtterms = (TextBox)grdpodesc.Rows[rowIndex].Cells[2].FindControl("txtterms");
                    drCurrentRow = dtt.NewRow();
                    dtt.Rows[i - 1]["splitdata"] = txtterms.Text;
                    rowIndex++;
                }
                dtt.Rows.Add(drCurrentRow);
                ViewState["Curtblterm"] = dtt;
                grdpodesc.DataSource = dtt;
                grdpodesc.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState Value is Null");
        }
        SetOldDataterm();
    }
    private void SetOldDataterm()
    {
        int rowIndex = 0;
        if (ViewState["Curtblterm"] != null)
        {
            DataTable dtt = (DataTable)ViewState["Curtblterm"];
            if (dtt.Rows.Count > 0)
            {
                for (int i = 0; i < dtt.Rows.Count; i++)
                {
                    CheckBox chk = (CheckBox)grdpodesc.Rows[rowIndex].Cells[0].FindControl("chkSelectterms");
                    TextBox txtterms = (TextBox)grdpodesc.Rows[rowIndex].Cells[2].FindControl("txtterms");
                    txtterms.Text = dtt.Rows[i]["splitdata"].ToString();
                    rowIndex++;
                }
            }
        }
    }
    protected void btnAddterm_Click(object sender, EventArgs e)
    {
        AddNewRowterm();
        //ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:Calculate()", true);
    }
    protected void grdterms_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (ViewState["Curtblterm"] != null)
            {
                DataTable dtt = (DataTable)ViewState["Curtblterm"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dtt.Rows.Count > 1)
                {
                    dtt.Rows.Remove(dtt.Rows[rowIndex]);
                    drCurrentRow = dtt.NewRow();
                    ViewState["Curtblterm"] = dtt;
                    grdpodesc.DataSource = dtt;
                    grdpodesc.DataBind();
                    for (int i = 0; i < grdpodesc.Rows.Count - 1; i++)
                    {
                        grdpodesc.Rows[i].Cells[1].Text = Convert.ToString(i + 1);
                    }
                    SetOldDataterm();
                }
            }
            //ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:Calculate()", true);
        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
        finally
        {
            con.Close();
        }
    }
    #endregion Terms and Conditions End
}
