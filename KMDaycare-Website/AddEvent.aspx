﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" Theme="Theme1" AutoEventWireup="true" CodeFile="AddEvent.aspx.cs" Inherits="AddEvent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>Event Calendar</h1>
    <div class="row">
        <div class="col-5">
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
                    <div  class="form-horizontal" id="collapseExample">
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
                                        <asp:DropDownList runat="server" ID="StartTime" OnSelectedIndexChanged="StartTime_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                    <asp:Label runat="server" cssclass="form-heading">End Time</asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList runat="server" ID="EndTime">
                                           <%-- <asp:ListItem Value="7:00">7:00AM</asp:ListItem>
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
                                            <asp:ListItem Value="17:00">5:00PM</asp:ListItem>--%>
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
        <div class="col-3 pull-right">
            <h3><asp:Label runat="server" ID="TheDate"></asp:Label></h3>
            <div id="EventsForDay" runat="server" style="border:2px solid #89D1F8;"></div>
        </div>
    </div>
</asp:Content>

