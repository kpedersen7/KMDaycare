<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin.master" Theme="Theme1" CodeFile="AddDailyActivity.aspx.cs" Inherits="AddDailyActivity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>Daily Activity Calendar</h1>
    <div class="row">
        <div class="col-6">
            <asp:Label runat="server" ID="messageLabel" CssClass="form-heading"></asp:Label>
            <asp:Calendar runat="server" ID="ActivityDate" OnSelectionChanged="ActivityDate_SelectionChanged" BackColor="White" BorderColor="Black" DayNameFormat="Shortest" Font-Names="Times New Roman" Font-Size="10pt" ForeColor="Black" Height="220px" NextPrevFormat="FullMonth" TitleFormat="Month" Width="400px">
                <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="10pt" ForeColor="#333333" Height="20pt"></DayHeaderStyle>
                <DayStyle Width="14%"></DayStyle>
                <NextPrevStyle Font-Size="10pt" ForeColor="White"></NextPrevStyle>
                <OtherMonthDayStyle ForeColor="#999999"></OtherMonthDayStyle>
                <SelectedDayStyle BackColor="#811F45" ForeColor="White"></SelectedDayStyle>
                <SelectorStyle BackColor="#CCCCCC" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" ForeColor="#333333" Width="1%"></SelectorStyle>
                <TitleStyle BackColor="#BEE25A" Font-Bold="True" Font-Size="13pt" ForeColor="White" Height="14pt"></TitleStyle>
                <TodayDayStyle BackColor="#CCCC99"></TodayDayStyle>
            </asp:Calendar>

            <a class="btn btn-primary" data-toggle="collapse" href="#collapseExample" role="button" aria-expanded="false" aria-controls="collapseExample">Add Activity</a>

            <div class="collapse" id="collapseExample">
                <div class="card card-body">
                    <asp:Table runat="server" ID="addactivitytable">
                        <asp:TableRow>
                            <asp:TableCell>
                                    <asp:Label runat="server" CssClass="form-heading">Activity Name</asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox runat="server" CssClass="form-textbox" ID="DescriptionofActivity" required></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                    <asp:Label runat="server" CssClass="form-heading">Start Time: </asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList runat="server" ID="StartTime">
                                    <asp:ListItem Value="7:00">7:00AM</asp:ListItem>
                                    <asp:ListItem Value="7:30">7:30AM</asp:ListItem>
                                    <asp:ListItem Value="8:00">8:00AM</asp:ListItem>
                                    <asp:ListItem Value="8:30">8:30AM</asp:ListItem>
                                    <asp:ListItem Value="9:00">9:00AM</asp:ListItem>
                                    <asp:ListItem Value="9:30">9:30AM</asp:ListItem>
                                    <asp:ListItem Value="10:00">10:00AM</asp:ListItem>
                                    <asp:ListItem Value="10:30">10:30AM</asp:ListItem>
                                    <asp:ListItem Value="11:00">11:00AM</asp:ListItem>
                                    <asp:ListItem Value="11:30">11:30AM</asp:ListItem>
                                    <asp:ListItem Value="12:00">12:00PM</asp:ListItem>
                                    <asp:ListItem Value="12:30">12:30PM</asp:ListItem>
                                    <asp:ListItem Value="13:00">1:00PM</asp:ListItem>
                                    <asp:ListItem Value="13:30">1:30PM</asp:ListItem>
                                    <asp:ListItem Value="14:00">2:00PM</asp:ListItem>
                                    <asp:ListItem Value="14:30">2:30PM</asp:ListItem>
                                    <asp:ListItem Value="15:00">3:00PM</asp:ListItem>
                                    <asp:ListItem Value="15:30">3:30PM</asp:ListItem>
                                    <asp:ListItem Value="16:00">4:00PM</asp:ListItem>
                                    <asp:ListItem Value="16:30">4:30PM</asp:ListItem>
                                    <asp:ListItem Value="17:00">5:00PM</asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                    <asp:Label runat="server" cssclass="form-heading">End Time</asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList runat="server" ID="EndTime">
                                    <asp:ListItem Value="7:00">7:00AM</asp:ListItem>
                                    <asp:ListItem Value="7:30">7:30AM</asp:ListItem>
                                    <asp:ListItem Value="8:00">8:00AM</asp:ListItem>
                                    <asp:ListItem Value="8:30">8:30AM</asp:ListItem>
                                    <asp:ListItem Value="9:00">9:00AM</asp:ListItem>
                                    <asp:ListItem Value="9:30">9:30AM</asp:ListItem>
                                    <asp:ListItem Value="10:00">10:00AM</asp:ListItem>
                                    <asp:ListItem Value="10:30">10:30AM</asp:ListItem>
                                    <asp:ListItem Value="11:00">11:00AM</asp:ListItem>
                                    <asp:ListItem Value="11:30">11:30AM</asp:ListItem>
                                    <asp:ListItem Value="12:00">12:00PM</asp:ListItem>
                                    <asp:ListItem Value="12:30">12:30PM</asp:ListItem>
                                    <asp:ListItem Value="13:00">1:00PM</asp:ListItem>
                                    <asp:ListItem Value="13:30">1:30PM</asp:ListItem>
                                    <asp:ListItem Value="14:00">2:00PM</asp:ListItem>
                                    <asp:ListItem Value="14:30">2:30PM</asp:ListItem>
                                    <asp:ListItem Value="15:00">3:00PM</asp:ListItem>
                                    <asp:ListItem Value="15:30">3:30PM</asp:ListItem>
                                    <asp:ListItem Value="16:00">4:00PM</asp:ListItem>
                                    <asp:ListItem Value="16:30">4:30PM</asp:ListItem>
                                    <asp:ListItem Value="17:00">5:00PM</asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                    <asp:Label runat="server" CssClass="form-heading" >Notes about this Activity:</asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox runat="server" CssClass="form-textbox" ID="Notes" TextMode="MultiLine" Rows="5" Style="overflow: hidden" required></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                    <asp:Label runat="server" CssClass="form-heading">ClassDescription: </asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList runat="server" ID="ClassID">
                                    <asp:ListItem Value="1">6-9 months</asp:ListItem>
                                    <asp:ListItem Value="2">Toddlers</asp:ListItem>
                                    <asp:ListItem Value="3">Preschool</asp:ListItem>
                                    <asp:ListItem Value="4">Kindergarten</asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Button runat="server" ID="submit_button" CssClass="btn btn-primary" OnClick="submit_button_Click" Text="Submit" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>
            </div>
        </div>
        <div class="col-6">
            <div id="ActivitiesForDay" runat="server"></div>
        </div>
    </div>
</asp:Content>
