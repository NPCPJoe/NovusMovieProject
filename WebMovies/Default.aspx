<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebMovies.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>

<body>
    <form id="form1" runat="server">
    <div>
        <div><label for="DropDownListFilms" id="lblFilmsDdl" runat="server"></label><asp:DropDownList ID="DropDownListFilms" runat="server" AutoPostBack="True"></asp:DropDownList></div>
        <div><label for="DropDownListDirectors" id="lblDirectorsDdl" runat="server"></label><asp:DropDownList ID="DropDownListDirectors" runat="server" AutoPostBack="True"></asp:DropDownList></div>
        <div><label for="DropDownListActors" id="lblActorsDdl" runat="server"></label><asp:DropDownList ID="DropDownListActors" runat="server" AutoPostBack="True"></asp:DropDownList></div>
        <asp:Button ID="btnUpdate" runat="server" Text="update" Visible="False" />
    </div>
    <div>
        <asp:Button ID="btnReset" runat="server" Text="reset" ButtonParam="reset" OnClick="btnReset_Click" UseSubmitBehavior="False" />
    </div>
    </form>
</body>
</html>
