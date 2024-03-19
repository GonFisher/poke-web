<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="PokemonLIsta.aspx.cs" Inherits="pokedex_web.PokemonLIsta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Lista De Pokemons</h1>
    <asp:GridView ID="dgvPokemons" runat="server" CssClass="table"></asp:GridView>
</asp:Content>
