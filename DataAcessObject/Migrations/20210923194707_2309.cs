using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAcessObject.Migrations
{
    public partial class _2309 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(70)", unicode: false, maxLength: 70, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Telefone = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    Senha = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", unicode: false, nullable: false),
                    Genero = table.Column<int>(type: "int", unicode: false, nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(70)", unicode: false, maxLength: 70, nullable: false),
                    Descricao = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraInicio = table.Column<TimeSpan>(type: "time", nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraFim = table.Column<TimeSpan>(type: "time", nullable: false),
                    Valor = table.Column<double>(type: "float", nullable: true),
                    IdadeMinima = table.Column<int>(type: "int", nullable: false),
                    PrecisaApresentaDocumento = table.Column<bool>(type: "bit", nullable: false),
                    Endereco = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    UsuarioID = table.Column<int>(type: "int", nullable: false),
                    EhPublico = table.Column<bool>(type: "bit", nullable: false),
                    Likes = table.Column<int>(type: "int", nullable: false),
                    Dislikes = table.Column<int>(type: "int", nullable: false),
                    QtdMaximaPessoas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Eventos_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comentarios",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioID = table.Column<int>(type: "int", nullable: false),
                    Texto = table.Column<string>(type: "varchar(140)", unicode: false, maxLength: 140, nullable: false),
                    Likes = table.Column<int>(type: "int", nullable: false),
                    Dislikes = table.Column<int>(type: "int", nullable: false),
                    ComentarioID = table.Column<int>(type: "int", nullable: true),
                    EventoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentarios", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Comentarios_Comentarios_ComentarioID",
                        column: x => x.ComentarioID,
                        principalTable: "Comentarios",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comentarios_Eventos_EventoID",
                        column: x => x.EventoID,
                        principalTable: "Eventos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comentarios_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "EventoTags",
                columns: table => new
                {
                    EventosID = table.Column<int>(type: "int", nullable: false),
                    TagsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventoTags", x => new { x.EventosID, x.TagsID });
                    table.ForeignKey(
                        name: "FK_EventoTags_Eventos_EventosID",
                        column: x => x.EventosID,
                        principalTable: "Eventos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventoTags_Tags_TagsID",
                        column: x => x.TagsID,
                        principalTable: "Tags",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventoUsuario",
                columns: table => new
                {
                    EventoID = table.Column<int>(type: "int", nullable: false),
                    UsuarioID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventoUsuario", x => new { x.EventoID, x.UsuarioID });
                    table.ForeignKey(
                        name: "FK_EventoUsuario_EventoID",
                        column: x => x.EventoID,
                        principalTable: "Eventos",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_EventoUsuario_ParticipanteID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_ComentarioID",
                table: "Comentarios",
                column: "ComentarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_EventoID",
                table: "Comentarios",
                column: "EventoID");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_UsuarioID",
                table: "Comentarios",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_UsuarioID",
                table: "Eventos",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_EventoTags_TagsID",
                table: "EventoTags",
                column: "TagsID");

            migrationBuilder.CreateIndex(
                name: "IX_EventoUsuario_UsuarioID",
                table: "EventoUsuario",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Nome",
                table: "Tags",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Telefone",
                table: "Usuarios",
                column: "Telefone",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comentarios");

            migrationBuilder.DropTable(
                name: "EventoTags");

            migrationBuilder.DropTable(
                name: "EventoUsuario");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Eventos");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
