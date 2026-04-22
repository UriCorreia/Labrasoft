<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebApplication1.BolsistaExemplo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="jumbotron bg-primary text-white shadow">
            <h1 class="display-4"> 🎓 Sistema de Gestão Acadêmica</h1>
            <p class="lead">Plataforma centralizada para controle de projetos de pesquisa, coordenadores e bolsistas.</p>
            <hr class="my-4" style="background-color: white;"/>
            <p>Este sistema foi desenvolvido para facilitar o gerenciamento de verbas e vínculos acadêmicos.</p>
        </div>
        <div class="row mt-4">
            <div class="col-md-6 mb-4">
                <div class="card h-100 shadow-sm">
                    <div class="card-body">
                        <h5 class="card-title text-primary">Sobre o Projeto</h5>
                        <p class="card-text text-muted">
                            O objetivo deste sistema é integrar as três principais entidades de um ambiente de pesquisa:
                        </p>
                        <ul>
                            <li><strong>Bolsistas:</strong> Cadastro de alunos com controle de dados pessoais e matrícula.</li>
                            <li><strong>Coordenadores:</strong> Professores responsáveis pela gestão dos projetos e titulação acadêmica.</li>
                            <li><strong>Projetos:</strong> Unidade central que utiliza <strong>Composição</strong> para vincular um coordenador e múltiplos bolsistas, além de gerir orçamentos.</li>
                        </ul>
                    </div>
                </div>
            </div>

            <div class="col-md-6 mb-4">
                <div class="card h-100 shadow-sm border-info">
                    <div class="card-body">
                        <h5 class="card-title text-info">Implementações Técnicas</h5>
                        <p class="card-text">Destaques do desenvolvimento voltados à Orientação a Objetos:</p>
                        <div class="list-group">
                            <%--<div class="list-group-item list-group-item-action py-2">
                                <strong>Repository Pattern:</strong> Uso de listas estáticas para persistência de dados entre páginas.
                            </div>--%>
                            <div class="list-group-item list-group-item-action py-2">
                                <strong>Composição de Objetos:</strong> Relação 1:N entre Projetos e Bolsistas.
                            </div>
                            <div class="list-group-item list-group-item-action py-2">
                                <strong>Bootstrap UI:</strong> Interface responsiva e componentes interativos (GridViews, DropDowns).
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="alert alert-light border shadow-sm mt-2">
            <h5 class="alert-heading font-weight-bold">🚀 Guia Rápido de Uso</h5>
            <ol>
                <li>Comece cadastrando um <strong>Bolsista</strong> e um <strong>Coordenador</strong> nas respectivas abas do menu superior.</li>
                <li>Vá em <strong>Cadastro de Projeto</strong>, defina o título, a verba e selecione as pessoas cadastradas.</li>
                <li>Visualize e pesquise os dados processados diretamente na listagem em tempo real.</li>
            </ol>
        </div>
    </div>
</asp:Content>
