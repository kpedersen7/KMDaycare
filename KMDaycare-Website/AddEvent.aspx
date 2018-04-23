<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" Theme="Theme1" AutoEventWireup="true" CodeFile="AddEvent.aspx.cs" Inherits="AddEvent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>Event Calendar</h1>
    <div class="row container-fluid">
        <div class="col-6">
            <asp:Label runat="server" ID="messageLabel" CssClass="form-heading"></asp:Label>
            <asp:Calendar runat="server" ID="EventDate" OnSelectionChanged="EventDate_SelectionChanged" BackColor="White" BorderColor="Black" DayNameFormat="Shortest" Font-Names="Times New Roman" Font-Size="10pt" ForeColor="Black" Height="220px" NextPrevFormat="FullMonth" TitleFormat="Month" Width="400px">
                <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="10pt" ForeColor="#333333" Height="20pt"></DayHeaderStyle>
                <DayStyle Width="14%"></DayStyle>
                <NextPrevStyle Font-Size="10pt" ForeColor="White"></NextPrevStyle>
                <OtherMonthDayStyle ForeColor="#999999"></OtherMonthDayStyle>
                <SelectedDayStyle BackColor="#811F45" ForeColor="White"></SelectedDayStyle>
                <SelectorStyle BackColor="#CCCCCC" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" ForeColor="#333333" Width="1%"></SelectorStyle>
                <TitleStyle BackColor="#BEE25A" Font-Bold="True" Font-Size="13pt" ForeColor="White" Height="14pt"></TitleStyle>
                <TodayDayStyle BackColor="#CCCC99"></TodayDayStyle>
            </asp:Calendar>
            <asp:Panel runat="server" ID="AdminControlsPanel">
                <asp:Panel runat="server" ID="AdminControls">
                    <a class="btn btn-primary" data-toggle="collapse" href="#collapseExample" role="button" aria-expanded="false" aria-controls="collapseExample">Add Event</a>
                    <div class="form-horizontal collapse" id="collapseExample">
                        <div class="card card-body">
                            <asp:Table runat="server" ID="addtable">
                                <asp:TableRow>
                                    <asp:TableCell>
                                    <asp:Label runat="server" CssClass="form-heading">Event Name</asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox runat="server" CssClass="form-textbox" ID="Description" MaxLength="100"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                    <asp:Label runat="server" CssClass="form-heading">Start Time: </asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList runat="server" ID="StartTime">
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                    <asp:Label runat="server" cssclass="form-heading">End Time</asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList runat="server" ID="EndTime">
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                    <asp:Label runat="server" CssClass="form-heading" >Notes about this event:</asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox runat="server" CssClass="form-textbox" ID="Notes" TextMode="MultiLine" Rows="5" Style="overflow: hidden"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Button runat="server" ID="SubmitButton" CssClass="btn btn-primary" OnClick="SubmitButton_Click" Text="Submit" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </div>
                    </div>
                </asp:Panel>
            </asp:Panel>
        </div>
        <div class="col-3">
            <h3>
                <asp:Label runat="server" ID="TheDate"></asp:Label>
            </h3>
            <div id="EventsForDay" runat="server" style="border: 2px solid #89D1F8;"></div>
        </div>
    </div>
</asp:Content>

