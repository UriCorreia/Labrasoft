<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastroProjeto.aspx.cs" Inherits="WebApplication1.CadastroProjeto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="card shadow-sm mx-auto w-100">
            <div class="card-header bg-primary text-white text-center">
                <h2 class="mb-0">📝 Cadastro de Projeto</h2>
            </div>

            <div class="card-body p-4">
                <p class="text-muted text-center small">Preencha os campos abaixo para processar o cadastro.</p>
                <hr />

                <div class="form-group mb-3">
                    <label class="form-label font-weight-bold">Titulo do Projeto:</label>
                    <asp:TextBox ID="txtTitulo" runat="server" CssClass="form-control" placeholder="Ex: Labrasoft"></asp:TextBox>
                </div>

                <div class="form-group mb-3">
                    <label class="form-label font-weight-bold">Area de Conhecimento:</label>
                    <asp:TextBox ID="txtAreaConhecimento" runat="server" CssClass="form-control" placeholder="Ex: Inteligência Artificial"></asp:TextBox>
                </div>

                <div class="row">
                    <div class="col-md-6 form-group mb-3">
                        <label class="form-label font-weight-bold">Valor da Verba Aprovada:</label>
                        <asp:TextBox ID="txtVerbaAprovada" runat="server" CssClass="form-control" placeholder="Ex: 1200,00"></asp:TextBox>
                    </div>
                    <div class="col-md-6 form-group mb-3">
                        <label class="form-label font-weight-bold">Valor da Bolsa Individual:</label>
                        <asp:TextBox ID="txtBolsaIndividual" runat="server" CssClass="form-control" placeholder="Ex: 400,00"></asp:TextBox>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6 form-group mb-3">
                        <label class="form-label font-weight-bold">Coordenador Responsável:</label>
                        <asp:DropDownList ID="ddlCoordenadores" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="col-md-6 form-group mb-3">
                        <label class="form-label font-weight-bold">Bolsista(s):</label>
                        <div class="border p-3 rounded bg-light" style="max-height: 150px; overflow-y: auto;"">
                            <asp:CheckBoxList ID="cblBolsistas" runat="server"></asp:CheckBoxList>
                        </div>
                    </div>
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
                                <asp:TextBox ID="txtPesquisa" runat="server" CssClass="form-control" placeholder="Titulo do Projeto"></asp:TextBox>
                                <asp:LinkButton ID="lbtnPesquisa" runat="server" CssClass="btn btn-primary" OnClick="lbtnPesquisa_Click">
                                🔍
                                </asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-md-8 text-md-end">
                            <div class="d-flex flex-wrap gap-2 justify-content-md-end">
                                <asp:LinkButton ID="lbtnOrdenar" runat="server" CssClass="btn btn-info text-white" OnClick="lbtnOrdenar_Click">
                                A-Z Nome
                                </asp:LinkButton>

                                <asp:LinkButton ID="lbtnLimparFiltros" runat="server" CssClass="btn btn-outline-dark" >
                                Limpar Filtros
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <hr />
                <div class="mt-5">
                    <h3 class="text-secondary">📋 Lista de Projetos Cadastrados</h3>

                    <asp:GridView ID="gridProjetos" runat="server"
                        CssClass="table table-hover table-striped border shadow-sm"
                        AutoGenerateColumns="false" GridLines="None">
                        <HeaderStyle CssClass="thead-dark" />
                        <Columns>
                            <asp:BoundField DataField="Titulo" HeaderText="Projeto" />
                            <asp:BoundField DataField="AreaConhecimento" HeaderText="Área" />

                            <asp:TemplateField HeaderText="Coordenador">
                                <ItemTemplate>
                                    <%# Eval("CoordenadorResponsavel.Nome") %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Bolsistas">
                                <ItemTemplate>
                                    <%# string.Join(", ", ((List<WebApplication1.Models.Bolsista>)Eval("BolsistasVinculados")).Select(b => b.Nome)) %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="VerbaAprovada" HeaderText="Verba" DataFormatString="{0:C}" />
                            <asp:BoundField DataField="BolsaIndividual" HeaderText="Bolsa" DataFormatString="{0:C}" />
                        </Columns>
                    </asp:GridView>

                    <asp:Label ID="lblAvisoGrid" runat="server" Text="Nenhum Projeto na memória."
                        CssClass="text-muted italic" Visible="false"></asp:Label>
                </div>

                <div class="mt-4 text-center">
                    <asp:Label ID="lblMensagem" runat="server" CssClass="h6"></asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
