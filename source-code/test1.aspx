<%@ Page Title="" Language="C#" MasterPageFile="~/TestMasterPage.master" AutoEventWireup="true" CodeFile="test1.aspx.cs" Inherits="test1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="gridCourseStatus" runat="server" AutoGenerateColumns="false"
                        AllowPaging="True" CssClass="table"
                        Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="gridCourseStatus_RowCommand" OnPageIndexChanging="gridCourseStatus_PageIndexChanging">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#F7F6F3" VerticalAlign="Top" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundField DataField="courseId" HeaderText="Course Id" SortExpression="courseId" />
                            <asp:BoundField DataField="userId" HeaderText="User Id" SortExpression="userId" />
                            <asp:BoundField DataField="fName" HeaderText="First Name" SortExpression="fName" />
                            <asp:BoundField DataField="lName" HeaderText="Last Name" SortExpression="lName" />
                            <asp:BoundField DataField="emailId" HeaderText="Email Id" SortExpression="emailId" />
                            <asp:BoundField DataField="courseLevel" HeaderText="Course Level" SortExpression="courseLevel" />
                            <asp:BoundField DataField="courseNumber" HeaderText="Course Number" SortExpression="courseNumber" />
                            <asp:BoundField DataField="courseName" HeaderText="Course Name" SortExpression="courseName" />
                            <asp:BoundField DataField="courseStatus" HeaderText="Course Status" SortExpression="courseStatus" />
                            <asp:TemplateField HeaderText="Approve/Decline">
                                <ItemTemplate>
                                    <asp:Button ID="btnApprove" CssClass="btn btn-success btn-sm" Visible='<%#  (Eval("courseStatus").ToString()) == "Decline" %>'
                                        runat="server" Text="Approve" OnClientClick="if ( ! CourseApproved()) return false;"
                                        CommandArgument='<%# Eval("userId") + "," +  Eval("courseId")%>' CommandName="false" />

                                    <asp:Button ID="btnDecline" CssClass="btn btn-success btn-sm" Visible='<%#  (Eval("courseStatus").ToString()) == "Approved" %>'
                                        runat="server" Text="Decline" OnClientClick="if ( ! CourseDecline()) return false;"
                                        CommandArgument='<%# Eval("userId") + "," +  Eval("courseId")%>' CommandName="true" />
                                    <%--<asp:Label ID="lbl_approve" CssClass="" Visible='<%#  (Eval("courseStatus").ToString()) == "Approved" %>'
                                                             runat="server" Text="Approved"></asp:Label>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle BackColor="#284775" HorizontalAlign="Center" CssClass="pagination-ys" Wrap="True" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>

    <script src="js/custom.min.js"></script>
    <script type="text/javascript">
        function CourseApproved() {
            return confirm("Are you sure you want to Approved this Course?");
        }
        function CourseDecline() {
            return confirm("Are you sure you want to Decline this Course?");
        }
        function UserDeactive() {
            return confirm("Are you sure you want to Deactive this user?");
        }
    </script>
</asp:Content>

