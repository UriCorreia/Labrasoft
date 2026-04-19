<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastroBolsista.aspx.cs" Inherits="WebApplication1.CadastroBolsista" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="card shadow-sm mx-auto w-100">
            <div class="card-header bg-primary text-white text-center">
                <h2 class="mb-0">📝 Cadastro de Bolsista</h2>
            </div>
            
            <div class="card-body p-4">
                <p class="text-muted text-center small">Preencha os campos abaixo para processar o cadastro.</p>
                <hr />

                <div class="form-group mb-3">
                    <label class="form-label font-weight-bold">Nome Completo:</label>
                    <asp:TextBox ID="txtNome" runat="server" CssClass="form-control" placeholder="Ex: João Silva"></asp:TextBox>
                </div>

                <div class="row">
                    <div class="col-md-6 form-group mb-3">
                        <label class="form-label font-weight-bold">Matrícula:</label>
                        <asp:TextBox ID="txtMatricula" runat="server" CssClass="form-control" placeholder="2024.X.XXXX"></asp:TextBox>
                    </div>

                    <div class="col-md-6 form-group mb-3">
                        <label class="form-label font-weight-bold">CPF:</label>
                        <asp:TextBox ID="txtCPF" runat="server" CssClass="form-control" placeholder="000.000.000-00"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group mb-4">
                    <label class="form-label font-weight-bold">Data de Nascimento:</label>
                    <asp:TextBox ID="txtDataNasc" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group mb-3">
                    <label class="form-label font-weight-bold">Sexo:</label>
                    <asp:DropDownList ID="ddlSexo" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Selecione..." Value="" />
                        <asp:ListItem Text="Masculino" Value="M" />
                        <asp:ListItem Text="Feminino" Value="F" />
                        <asp:ListItem Text="Outro" Value="O" />
                    </asp:DropDownList>
                </div>


                <div class="row g-2 mb-4">
                    <div class="col-md-8">
                        <asp:Button ID="btnSalvar" runat="server" Text="Salvar e Processar Cadastro"
                            CssClass="btn btn-success btn-lg w-100 shadow-sm" OnClick="btnSalvar_Click" />
                    </div>
                    <div class="col-md-4">
                        <asp:Button ID="btnLimpar" runat="server" Text="Limpar Campos"
                            CssClass="btn btn-outline-secondary btn-lg w-100" OnClick="btnLimpar_Click" />
                    </div>
                </div>

                <hr />

                <asp:Panel ID="pnlFilftros" runat="server" Visible="false" CssClass="bg-light p-3 rounded border mb-3">
                    <div class="row align-items-end g-3">

                        <div class="col-md-4">
                            <label class="form-label small font-weight-bold">Pesquisar na Lista:</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtPesquisa" runat="server" CssClass="form-control" placeholder="Nome ou Matrícula"></asp:TextBox>
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

                                <asp:LinkButton ID="lbtnFiltrar" runat="server" CssClass="btn btn-warning text-dark" OnClick="lbtnFiltrar_Click">
                                    ♀️ Mulheres
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
                    <h3 class="text-secondary">📋 Lista de Bolsistas Cadastrados</h3>
    
                    <asp:GridView ID="gridBolsistas" runat="server" 
                        CssClass="table table-hover table-striped border" 
                        AutoGenerateColumns="true" 
                        GridLines="None"
                        OnRowDataBound="gridBolsistas_RowDataBound">
                        <HeaderStyle CssClass="thead-dark" />
                    </asp:GridView>

                    <asp:Label ID="lblAvisoGrid" runat="server" Text="Nenhum bolsista na memória." 
                        CssClass="text-muted italic" Visible="false"></asp:Label>
                </div>

                <div class="mt-4 text-center">
                    <asp:Label ID="lblMensagem" runat="server" CssClass="h6"></asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
