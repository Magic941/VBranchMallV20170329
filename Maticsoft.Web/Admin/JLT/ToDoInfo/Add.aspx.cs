using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using Maticsoft.Accounts.Bus;
using Maticsoft.BLL.Members;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.JLT.ToDoInfo
{
    public partial class Add : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 679; } } //移动办公_待办管理_添加页
        private Maticsoft.BLL.JLT.ToDoInfo bll = new BLL.JLT.ToDoInfo();
        private Maticsoft.Model.JLT.ToDoInfo model = new Model.JLT.ToDoInfo();
        private User user = new User();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dropToType.Items.Add(new ListItem("本人", "0"));
                dropToType.Items.Add(new ListItem("下属", "1"));
                dropToType.Items.Add(new ListItem("所有下属", "2"));
                DataSet ds = null;
                if (CurrentUser.UserType == "AA")
                {
                    ds = user.GetUserList("");
                }
                if (CurrentUser.UserType == "UU")
                {
                    //无下级用户不绑定“指定用户”选项
                    ds = user.GetUsersByEmp(CurrentUser.UserID);
                }
                if (CurrentUser.UserType == "EE")
                {
                    ds = user.GetUsersByDepart(CurrentUser.DepartmentID, "");
                }
                if (!DataSetTools.DataSetIsNull(ds))
                {
                    dropToType.Items.Add(new ListItem("指定用户", "3"));
                    dropToUserID.DataSource = ds;
                    dropToUserID.DataTextField = "UserName";
                    dropToUserID.DataValueField = "UserId";
                    dropToUserID.DataBind();
                }

                dropToType.SelectedValue = "0";
            }
        }

        public int Status
        {
            get
            {
                int status = 0;
                string strstatus = Request.Params["status"];
                if (!string.IsNullOrWhiteSpace(strstatus))
                {
                    status = Globals.SafeInt(strstatus, 0);
                }
                return status;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            user = CurrentUser;
            int userid = user.UserID;
            model.Title = txtTitle.Text;
            model.Content = txtContent.Text;

            //本人
            model.CreatedUserId = user.UserID;
            model.UserName = user.UserName;
            model.ToType = int.Parse(dropToType.SelectedValue);
            model.Status = int.Parse(dropStatus.SelectedValue);
            model.CreatedDate = DateTime.Now;
            model.ToDoDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            int ToType = int.Parse(dropToType.SelectedValue);
            string ToUserId = hdToUserID.Value;
            //企业ID
            if (!string.IsNullOrWhiteSpace(user.DepartmentID))
            {
                model.EnterpriseID = Maticsoft.Common.Globals.SafeInt(user.DepartmentID, -1);
            }
            int tuDoInfoId = 0; //此用户创建的待办ID
            try
            {
                //创建主待办信息
                model.UserId = -1;
                tuDoInfoId = bll.Add(model);
                SaveFile(tuDoInfoId, model.UserId, bll);    //保存附件给主待办
                model.ParentId = tuDoInfoId;

                switch (ToType)
                {
                    case 0:
                        //本人 创建子待办 内容由回复填充
                        model.UserId = user.UserID;
                        model.Content = string.Empty;
                        bll.Add(model);
                        break;
                    case 3:
                        #region 指定用户发送 使用ToUserId
                        if (string.IsNullOrWhiteSpace(ToUserId))
                        {
                            MessageBox.ShowFailTip(this, "添加待办失败,发送对象不能为空！");
                            return;
                        }
                        int tmpUserId;
                        string[] toUserIdStrs = ToUserId.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                        model.ToUserId = ToUserId;

                        foreach (string userIdStr in toUserIdStrs)
                        {
                            tmpUserId = Globals.SafeInt(userIdStr, 0);
                            if (tmpUserId < 1) continue;    //忽略脏数据
                            Maticsoft.Accounts.Bus.User tmpUser = new Maticsoft.Accounts.Bus.User(tmpUserId);
                            //用户不存在
                            if (tmpUser == null || string.IsNullOrWhiteSpace(tmpUser.UserType)) continue;
                            model.UserId = tmpUser.UserID;
                            model.UserName = tmpUser.UserName;
                            model.Content = string.Empty;
                            bll.Add(model);
                        }
                        break;
                        #endregion
                    case 1:
                        {
                            #region 发送到下属  Modify 本人及其下属更正为 下属 20120105
                            #region 同部门 不使用
                            ////无下属, 终止发送到下属
                            //if (user.EmployeeID < 1) return new Result(ResultStatus.Success, tuDoInfoId);
                            //DataSet ds = user.GetUsersByEmp(user.EmployeeID); 
                            #endregion

                            DataSet ds = user.GetUsersByEmp(user.UserID);

                            //无下属员工数据, 终止发送到下属
                            if (Common.DataSetTools.DataSetIsNull(ds))
                            {
                                MessageBox.ShowFailTip(this, "添加待办失败！,无下属员工！");
                            }

                            foreach (DataRow dataRow in ds.Tables[0].Rows)
                            {
                                model.UserId = Globals.SafeInt(dataRow["UserID"].ToString(), -1);
                                model.UserName = dataRow["UserName"].ToString();
                                model.Content = string.Empty;
                                bll.Add(model);
                            }
                            break;
                            #endregion
                        }
                    case 2:
                        {
                            #region 所有下属
                            BLL.Members.UsersExp bllUsersExp = new UsersExp();
                            DataSet ds = bllUsersExp.GetAllEmpByUserId(user.UserID);

                            //无下属员工数据, 终止发送到下属
                            if (Common.DataSetTools.DataSetIsNull(ds))
                            {
                                MessageBox.ShowFailTip(this, "添加待办失败！,无下属员工！");
                            }

                            foreach (DataRow dataRow in ds.Tables[0].Rows)
                            {
                                model.UserId = Globals.SafeInt(dataRow["UserID"].ToString(), -1);
                                model.UserName = dataRow["UserName"].ToString();
                                model.Content = string.Empty;
                                bll.Add(model);
                            }
                            break;
                            #endregion
                        }
                }

                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("新增待办：【{0}】", model.Title), this);
                MessageBox.ShowSuccessTip(this, "添加待办成功", "list.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SaveFile(int id, int userId, Maticsoft.BLL.JLT.ToDoInfo bll)
        {
            StringBuilder strFileNames = new StringBuilder();
            string fileName1 = string.Empty;
            UpLoadFile(FileUpload1.PostedFile, userId, id, out fileName1);
            if (!string.IsNullOrWhiteSpace(fileName1))
            {
                strFileNames.Append(fileName1);
                strFileNames.Append("|");
            }
            string fileName2 = string.Empty;
            UpLoadFile(FileUpload2.PostedFile, userId, id, out fileName2);
            if (!string.IsNullOrWhiteSpace(fileName2))
            {
                strFileNames.Append(fileName2);
                strFileNames.Append("|");
            }
            string fileName3 = string.Empty;
            UpLoadFile(FileUpload3.PostedFile, userId, id, out fileName3);
            if (!string.IsNullOrWhiteSpace(fileName3))
            {
                strFileNames.Append(fileName3);
                strFileNames.Append("|");
            }
            string fileName4 = string.Empty;
            UpLoadFile(FileUpload4.PostedFile, userId, id, out fileName4);
            if (!string.IsNullOrWhiteSpace(fileName4))
            {
                strFileNames.Append(fileName4);
                strFileNames.Append("|");
            }
            string fileName5 = string.Empty;
            UpLoadFile(FileUpload5.PostedFile, userId, id, out fileName5);
            if (!string.IsNullOrWhiteSpace(fileName5))
            {
                strFileNames.Append(fileName5);
                strFileNames.Append("|");
            }
            string fileName6 = string.Empty;
            UpLoadFile(FileUpload6.PostedFile, userId, id, out fileName6);
            if (!string.IsNullOrWhiteSpace(fileName6))
            {
                strFileNames.Append(fileName6);
                strFileNames.Append("|");
            }
            string fileName7 = string.Empty;
            UpLoadFile(FileUpload7.PostedFile, userId, id, out fileName7);
            if (!string.IsNullOrWhiteSpace(fileName7))
            {
                strFileNames.Append(fileName7);
                strFileNames.Append("|");
            }
            string fileName8 = string.Empty;
            UpLoadFile(FileUpload8.PostedFile, userId, id, out fileName8);
            if (!string.IsNullOrWhiteSpace(fileName8))
            {
                strFileNames.Append(fileName8);
                strFileNames.Append("|");
            }
            string fileName9 = string.Empty;
            UpLoadFile(FileUpload9.PostedFile, userId, id, out fileName9);
            if (!string.IsNullOrWhiteSpace(fileName9))
            {
                strFileNames.Append(fileName9);
                strFileNames.Append("|");
            }
            string fileName10 = string.Empty;
            UpLoadFile(FileUpload10.PostedFile, userId, id, out fileName10);
            if (!string.IsNullOrWhiteSpace(fileName10))
            {
                strFileNames.Append(fileName10);
                strFileNames.Append("|");
            }

            string fileDatapath = string.Format("/Upload/JLT/ToDos/{0}/{1}_", userId, id) + "{0}";

            string filename = strFileNames.ToString().TrimEnd('|');
            if (!string.IsNullOrWhiteSpace(filename))
            {
                bll.Update(id, filename, fileDatapath);
            }
        }

        private void UpLoadFile(HttpPostedFile file, int userId, int id, out string filename)
        {
            string fileDatapath = string.Format("/Upload/JLT/ToDos/{0}/", userId);
            int filelength = file.ContentLength;
            filename = file.FileName;
            if (filelength < 1)
            {
                filename = "";
                return;
            }
            string ExtensionImage = Path.GetExtension(file.FileName).ToLower();
            if (ExtensionImage == ".exe" || ExtensionImage == ".bat")
            {
                filename = "";
                Common.MessageBox.ShowServerBusyTip(this, "文件格式不正确");
                return;
            }
            string savePath = Server.MapPath(fileDatapath);
            if (!Directory.Exists(savePath))
            {
                //不存在则自动创建文件夹
                Directory.CreateDirectory(Server.MapPath(fileDatapath));
            }
            string path = savePath + id+"_"+file.FileName;
            try
            {
                file.SaveAs(path);
            }
            catch (Exception)
            {
                filename = "";
            }
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}