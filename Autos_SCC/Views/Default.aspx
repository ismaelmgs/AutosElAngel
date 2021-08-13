<%@ Page Title="Página principal" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Autos_SCC._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <title>
        Autos el ángel de puebla
    </title>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="card">
        <div class="card-block" style="text-align:center;">
            <h3>Bienvenido</h3>
        </div>
    </div>
    <div class="card" style="height:65vh;">
        <br />
        <div id="carouselExampleIndicators" class="carousel slide" data-ride="carousel" style="width:80%; margin: 0 auto; box-shadow:3px 3px 3px #0000001f; border-radius:20px;">
          <ol class="carousel-indicators">
            <li data-target="#carouselExampleIndicators" data-slide-to="0" class="active"></li>
            <li data-target="#carouselExampleIndicators" data-slide-to="1"></li>
            <li data-target="#carouselExampleIndicators" data-slide-to="2"></li>
          </ol>
          <div class="carousel-inner">
            <div class="carousel-item active">
              <img class="d-block w-100" src="../images/Banner_1.jpg" alt="First slide" style="border-radius:20px;">
            </div>
            <div class="carousel-item">
              <img class="d-block w-100" src="../images/Banner_2.jpg" alt="Second slide" style="border-radius:20px;">
            </div>
            <div class="carousel-item">
              <img class="d-block w-100" src="../images/Banner_3.jpg" alt="Third slide" style="border-radius:20px;">
            </div>
          </div>
          <a class="carousel-control-prev" href="#carouselExampleIndicators" role="button" data-slide="prev">
            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
            <span class="sr-only">Previous</span>
          </a>
          <a class="carousel-control-next" href="#carouselExampleIndicators" role="button" data-slide="next">
            <span class="carousel-control-next-icon" aria-hidden="true"></span>
            <span class="sr-only">Next</span>
          </a>
        </div>
        <br />
    </div>
</asp:Content>
