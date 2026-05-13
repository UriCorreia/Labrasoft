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

                <div id="containerMensagem" class="mt-4 text-center">
                    <asp:Label ID="lblMensagem" runat="server" CssClass="h6"></asp:Label>
                </div>

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
                                <asp:TextBox ID="txtPesquisa" runat="server" CssClass="form-control" placeholder="Titulo do Projeto ou Coordenador"></asp:TextBox>
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

                                <asp:LinkButton ID="lbtnLimparFiltros" runat="server" CssClass="btn btn-outline-dark" OnClick="lbtnLimparFiltro_Click">
                                    Limpar Filtros
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <hr />
                <div class="mt-5">
                    <h3 class="text-secondary">📋 Lista de Projetos Cadastrados</h3>

                    <asp:GridView ID="gridProjetos" runat="server" CssClass="table table-hover table-striped border shadow-sm"
                        AutoGenerateColumns="false" GridLines="None" DataKeyNames="id"
                        OnRowCommand="gridProjetos_RowCommand">
                        <HeaderStyle CssClass="thead-dark" />

                        <Columns>
                            <asp:BoundField DataField="titulo" HeaderText="Projeto" />
                            <asp:BoundField DataField="areaConhecimento" HeaderText="Área de Conhecimento" />
                            <asp:TemplateField HeaderText="Coordenador Responsável">
                                <ItemTemplate>
                                    <%# Eval("nomeCoordenador") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="verbaAprovada" HeaderText="Verba Aprovada" DataFormatString="{0:C}" />
                            <asp:TemplateField HeaderText="Ações">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnVerDetalhes" runat="server" CssClass="btn btn-sm btn-info text-white" CommandName="VerDetalhes" CommandArgument='<%# Eval("id") %>'                        CausesValidation="false"
                                        UseSubmitBehavior="false">
                                        🔍 Ver Detalhes
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:Label ID="lblAvisoGrid" runat="server" Text="Nenhum Projeto na memória." CssClass="text-muted font-italic" Visible="false">
                    </asp:Label>
                </div>
              
                <div class="modal fade" id="modalDetalhes" tabindex="-1" role="dialog" aria-hidden="true">
                    <div class="modal-dialog modal-lg" role="document">
                        <div class="modal-content">
                            <div class="modal-header bg-primary text-white">
                                <h5 class="modal-title">📋 Detalhes Completos do Projeto
                                </h5>
                                <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12 mb-3">
                                        <label class="font-weight-bold">
                                            Título do Projeto:
                                        </label>
                                        <asp:Label ID="lblDetTitulo" runat="server" CssClass="d-block p-2 bg-light border rounded">
                                        </asp:Label>
                                    </div>
                                    <div class="col-md-6 mb-3">
                                        <label class="font-weight-bold">
                                            Coordenador:
                                        </label>
                                        <asp:Label ID="lblDetCoordenador" runat="server" CssClass="d-block p-2 bg-light border rounded">
                                        </asp:Label>
                                    </div>
                                    <div class="col-md-6 mb-3">
                                        <label class="font-weight-bold">
                                            Área de Conhecimento:
                                        </label>
                                        <asp:Label ID="lblDetArea" runat="server" CssClass="d-block p-2 bg-light border rounded">
                                        </asp:Label>
                                    </div>
                                    <div class="col-md-6 mb-3">
                                        <label class="font-weight-bold">
                                            Verba Total:
                                        </label>
                                        <asp:Label ID="lblDetVerba" runat="server" CssClass="d-block p-2 bg-light border rounded">
                                        </asp:Label>
                                    </div>
                                    <div class="col-md-6 mb-3">
                                        <label class="font-weight-bold">
                                            Bolsa Individual:
                                        </label>
                                        <asp:Label ID="lblDetBolsa" runat="server" CssClass="d-block p-2 bg-light border rounded">
                                        </asp:Label>
                                    </div>
                                </div>
                                <hr />
                                <h6>🎓 Bolsistas Vinculados</h6>
                                <asp:BulletedList ID="bltBolsistasDet" runat="server" DisplayMode="Text" CssClass="list-group mt-2">
                                </asp:BulletedList>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" data-dismiss="modal">
                                    Fechar
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">

        function esconderMensagem() {

            $("#containerMensagem")
                .stop(true, true)
                .delay(3000)
                .fadeOut(1000, function () {

                    $('#<%= lblMensagem.ClientID %>').text("");

                $('#<%= lblMensagem.ClientID %>')
                    .removeClass("alert alert-success alert-danger alert-warning d-block");

                $(this).hide().css("opacity", "1");
            });
        }

    </script>
</asp:Content>
