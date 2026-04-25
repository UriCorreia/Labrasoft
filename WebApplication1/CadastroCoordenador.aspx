<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastroCoordenador.aspx.cs" Inherits="WebApplication1.CadastroCoordenador" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="card shadow-sm mx-auto w-100">
            <div class="card-header bg-primary text-white text-center">
                <h2 class="mb-0">📝 Cadastro de Coordenador</h2>
            </div>

            <div class="card-body p-4">
                <p class="text-muted text-center small">Preencha os campos abaixo para processar o cadastro.</p>
                <hr />

                <div id="containerMensagem" class="mt-4 text-center">
                    <asp:Label ID="lblMensagem" runat="server" CssClass="h6"></asp:Label>
                </div>

                <div class="form-group mb-3">
                    <label class="form-label font-weight-bold">Nome Completo:</label>
                    <asp:TextBox ID="txtNome" runat="server" CssClass="form-control" placeholder="Ex: João Silva"></asp:TextBox>
                </div>

                <div class="row">
                    <div class="col-md-6 form-group mb-3">
                        <label class="form-label font-weight-bold">Titulação:</label>
                        <asp:TextBox ID="txtTitulo" runat="server" CssClass="form-control" placeholder="Ex: Doutor"></asp:TextBox>
                    </div>

                    <div class="col-md-6 form-group mb-3">
                        <label class="form-label font-weight-bold">CPF:</label>
                        <asp:TextBox ID="txtCPF" runat="server" CssClass="form-control" placeholder="000.000.000-00"></asp:TextBox>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6 form-group mb-3">
                        <label class="form-label font-weight-bold">Area de Atuação:</label>
                        <asp:TextBox ID="txtAreaAtuacao" runat="server" CssClass="form-control" placeholder="Ex: Inteligência Artificial"></asp:TextBox>
                    </div>

                    <div class="col-md-6 form-group mb-3">
                        <label class="form-label font-weight-bold">Email:</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Ex: teste@gmail.com"></asp:TextBox>
                    </div>
                </div>
                
                
                <div class="row g-2 mb-4">
                    <div class="col-md-8">
                        <asp:Button ID="btnSalvar" runat="server" Text="Salvar e Processar Cadastro"
                            CssClass="btn btn-success btn-lg w-100 shadow-sm" OnClick="btnSalvar_Click" />
                    </div>
                    <div class="col-md-4">
                        <asp:Button ID="btnLimpar" runat="server" Text="Limpar Campos"
                            CssClass="btn btn-outline-secondary btn-lg w-100" OnCLick="btnLimpar_Click" />
                    </div>
                </div>

                <hr />

                <asp:Panel ID="pnlFilftros" runat="server" Visible="false" CssClass="bg-light p-3 rounded border mb-3">
                    <div class="row align-items-end g-3">

                        <div class="col-md-4">
                            <label class="form-label small font-weight-bold">Pesquisar na Lista:</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtPesquisa" runat="server" CssClass="form-control" placeholder="Nome ou Titulo"></asp:TextBox>
                                <asp:LinkButton ID="lbtnPesquisa" runat="server" CssClass="btn btn-primary" OnClick="lbtnPesquisar_Click">
                                    🔍
                                </asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-md-8 text-md-end">
                            <div class="d-flex flex-wrap gap-2 justify-content-md-end">
                                <asp:LinkButton ID="lbtnOrdenar" runat="server" CssClass="btn btn-info text-white" OnClick="lbtnOrdenar_Click">
                                    A-Z Nome
                                </asp:LinkButton>

                                <asp:LinkButton ID="lbtnLimparFiltros" runat="server" CssClass="btn btn-outline-dark" OnClick="lbtnLimparFiltros_Click">
                                    Limpar Filtros
                                </asp:LinkButton>
                            </div>
                        </div>

                    </div>
                </asp:Panel>
                <hr />
                <div class="mt-5">
                    <h3 class="text-secondary">📋 Lista de Coordenadores Cadastrados</h3>

                    <asp:GridView ID="gridBolsistas" runat="server"
                        CssClass="table table-hover table-striped border"
                        AutoGenerateColumns="true"
                        GridLines="None"
                        OnRowDataBound="gridBolsistas_RowDataBound">
                        <HeaderStyle CssClass="thead-dark" />
                    </asp:GridView>

                    <asp:Label ID="lblAvisoGrid" runat="server" Text="Nenhum Coordenador na memória."
                        CssClass="text-muted italic" Visible="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script type="text/javascript">
        function esconderMensagem() {
            $("#containerMensagem").stop(true, true).delay(3000).fadeOut(1000, function () {

                $('#<%= lblMensagem.ClientID %>').text("");

            $('#<%= lblMensagem.ClientID %>').removeClass("alert alert-success alert-danger alert-warning d-block");

            $(this).hide().css("opacity", "1");
        });
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (document.getElementById('<%= lblMensagem.ClientID %>').innerText != "") {
                esconderMensagem();
            }
        });
        }
</script>
</asp:Content>
