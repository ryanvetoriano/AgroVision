using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgroVision.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_DRONE_GS",
                columns: table => new
                {
                    ID_DRONE = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CODIGO_IDENTIFICACAO = table.Column<string>(type: "NVARCHAR2(40)", maxLength: 40, nullable: false),
                    MODELO = table.Column<string>(type: "NVARCHAR2(60)", maxLength: 60, nullable: false),
                    FABRICANTE = table.Column<string>(type: "NVARCHAR2(60)", maxLength: 60, nullable: false),
                    AUTONOMIA_MINUTOS = table.Column<decimal>(type: "NUMBER(10,2)", nullable: false),
                    ATIVO = table.Column<bool>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_DRONE_GS", x => x.ID_DRONE);
                });

            migrationBuilder.CreateTable(
                name: "TB_LOG_ERRO_GS",
                columns: table => new
                {
                    ID_LOG = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME_PROCEDURE = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                    NOME_USER_GS = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true),
                    DATA_ERRO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    CODIGO_ERRO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    MENSAGEM_ERRO = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_LOG_ERRO_GS", x => x.ID_LOG);
                });

            migrationBuilder.CreateTable(
                name: "TB_USER_GS",
                columns: table => new
                {
                    ID_USER = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CPF_USER = table.Column<long>(type: "NUMBER(11)", nullable: false),
                    NOME_USER = table.Column<string>(type: "NVARCHAR2(60)", maxLength: 60, nullable: false),
                    SENHA_USER = table.Column<string>(type: "NVARCHAR2(18)", maxLength: 18, nullable: false),
                    NM_FAZENDA = table.Column<string>(type: "NVARCHAR2(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_USER_GS", x => x.ID_USER);
                });

            migrationBuilder.CreateTable(
                name: "TB_PLANTACOES_GS",
                columns: table => new
                {
                    ID_PLANTACAO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ID_USER = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TIPO_PLANTIO = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    CULTURA = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    AREA_PLANTIO = table.Column<decimal>(type: "NUMBER(15,2)", nullable: false),
                    PRODUTIVIDADE_ESTIMADA = table.Column<decimal>(type: "NUMBER(15,2)", nullable: false),
                    DATA_PLANTIO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    LOCAL_PLANTIO = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    STATUS_PLANTIO = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    ATIVA = table.Column<bool>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PLANTACOES_GS", x => x.ID_PLANTACAO);
                    table.ForeignKey(
                        name: "FK_TB_PLANTACOES_GS_TB_USER_GS_ID_USER",
                        column: x => x.ID_USER,
                        principalTable: "TB_USER_GS",
                        principalColumn: "ID_USER",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_INSUMO_GS",
                columns: table => new
                {
                    ID_INSUMO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ID_PLANTACAO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    NOME_INSUMO = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    UNIDADE_MEDIDA = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    QTD_ESTOQUE = table.Column<decimal>(type: "NUMBER(10,2)", nullable: false),
                    ESTOQUE_MINIMO = table.Column<decimal>(type: "NUMBER(10,2)", nullable: false),
                    DATA_ULTIMA_APLICACAO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_INSUMO_GS", x => x.ID_INSUMO);
                    table.ForeignKey(
                        name: "FK_TB_INSUMO_GS_TB_PLANTACOES_GS_ID_PLANTACAO",
                        column: x => x.ID_PLANTACAO,
                        principalTable: "TB_PLANTACOES_GS",
                        principalColumn: "ID_PLANTACAO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_MISSAO_DRONE_GS",
                columns: table => new
                {
                    ID_MISSAO_DRONE = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ID_DRONE = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ID_PLANTACAO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DATA_INICIO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    DATA_FIM = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    AREA_MAPEADA = table.Column<decimal>(type: "NUMBER(15,2)", nullable: false),
                    ALTITUDE_MEDIA = table.Column<decimal>(type: "NUMBER(10,2)", nullable: false),
                    STATUS_MISSAO = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_MISSAO_DRONE_GS", x => x.ID_MISSAO_DRONE);
                    table.ForeignKey(
                        name: "FK_TB_MISSAO_DRONE_GS_TB_DRONE_GS_ID_DRONE",
                        column: x => x.ID_DRONE,
                        principalTable: "TB_DRONE_GS",
                        principalColumn: "ID_DRONE",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_MISSAO_DRONE_GS_TB_PLANTACOES_GS_ID_PLANTACAO",
                        column: x => x.ID_PLANTACAO,
                        principalTable: "TB_PLANTACOES_GS",
                        principalColumn: "ID_PLANTACAO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_SAFRA_GS",
                columns: table => new
                {
                    ID_SAFRA = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ID_PLANTACAO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DATA_COLHEITA = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    QTD_COLHIDA = table.Column<decimal>(type: "NUMBER(15,2)", nullable: false),
                    RECEITA_ESTIMADA = table.Column<decimal>(type: "NUMBER(15,2)", nullable: false),
                    PERDA_ESTIMADA = table.Column<decimal>(type: "NUMBER(15,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_SAFRA_GS", x => x.ID_SAFRA);
                    table.ForeignKey(
                        name: "FK_TB_SAFRA_GS_TB_PLANTACOES_GS_ID_PLANTACAO",
                        column: x => x.ID_PLANTACAO,
                        principalTable: "TB_PLANTACOES_GS",
                        principalColumn: "ID_PLANTACAO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_ANALISE_DRONE_GS",
                columns: table => new
                {
                    ID_ANALISE_DRONE = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ID_PLANTACAO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ID_MISSAO_DRONE = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    DATA_ANALISE = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    INDICE_SAUDE = table.Column<decimal>(type: "NUMBER(5,2)", nullable: false),
                    UMIDADE_SOLO = table.Column<decimal>(type: "NUMBER(5,2)", nullable: false),
                    TEMPERATURA_MEDIA = table.Column<decimal>(type: "NUMBER(5,2)", nullable: false),
                    INDICE_VEGETACAO_NDVI = table.Column<decimal>(type: "NUMBER(5,2)", nullable: false),
                    AREA_AFETADA_PERCENTUAL = table.Column<decimal>(type: "NUMBER(5,2)", nullable: false),
                    PRAGAS_DETECTADAS = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    NIVEL_RISCO = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    STATUS_ANALISE = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    RECOMENDACAO = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false),
                    OBSERVACAO_IMAGEM = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ANALISE_DRONE_GS", x => x.ID_ANALISE_DRONE);
                    table.ForeignKey(
                        name: "FK_TB_ANALISE_DRONE_GS_TB_MISSAO_DRONE_GS_ID_MISSAO_DRONE",
                        column: x => x.ID_MISSAO_DRONE,
                        principalTable: "TB_MISSAO_DRONE_GS",
                        principalColumn: "ID_MISSAO_DRONE",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_TB_ANALISE_DRONE_GS_TB_PLANTACOES_GS_ID_PLANTACAO",
                        column: x => x.ID_PLANTACAO,
                        principalTable: "TB_PLANTACOES_GS",
                        principalColumn: "ID_PLANTACAO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_LEITURA_SENSOR_GS",
                columns: table => new
                {
                    ID_LEITURA_SENSOR = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ID_MISSAO_DRONE = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TIPO_SENSOR = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false),
                    VALOR = table.Column<decimal>(type: "NUMBER(15,4)", nullable: false),
                    UNIDADE_MEDIDA = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    DATA_LEITURA = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    LATITUDE = table.Column<decimal>(type: "NUMBER(10,6)", nullable: true),
                    LONGITUDE = table.Column<decimal>(type: "NUMBER(10,6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_LEITURA_SENSOR_GS", x => x.ID_LEITURA_SENSOR);
                    table.ForeignKey(
                        name: "FK_TB_LEITURA_SENSOR_GS_TB_MISSAO_DRONE_GS_ID_MISSAO_DRONE",
                        column: x => x.ID_MISSAO_DRONE,
                        principalTable: "TB_MISSAO_DRONE_GS",
                        principalColumn: "ID_MISSAO_DRONE",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_OCORRENCIA_PLANTACAO_GS",
                columns: table => new
                {
                    ID_OCORRENCIA = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ID_PLANTACAO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ID_ANALISE_DRONE = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    TIPO_OCORRENCIA = table.Column<string>(type: "NVARCHAR2(40)", maxLength: 40, nullable: false),
                    NIVEL_RISCO = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    DESCRICAO = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false),
                    DATA_OCORRENCIA = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    RESOLVIDA = table.Column<bool>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_OCORRENCIA_PLANTACAO_GS", x => x.ID_OCORRENCIA);
                    table.ForeignKey(
                        name: "FK_TB_OCORRENCIA_PLANTACAO_GS_TB_ANALISE_DRONE_GS_ID_ANALISE_DRONE",
                        column: x => x.ID_ANALISE_DRONE,
                        principalTable: "TB_ANALISE_DRONE_GS",
                        principalColumn: "ID_ANALISE_DRONE",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_TB_OCORRENCIA_PLANTACAO_GS_TB_PLANTACOES_GS_ID_PLANTACAO",
                        column: x => x.ID_PLANTACAO,
                        principalTable: "TB_PLANTACOES_GS",
                        principalColumn: "ID_PLANTACAO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_RECOMENDACAO_AGRONOMICA_GS",
                columns: table => new
                {
                    ID_RECOMENDACAO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ID_ANALISE_DRONE = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TITULO = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    DESCRICAO = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false),
                    PRIORIDADE = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    DATA_GERACAO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    CONCLUIDA = table.Column<bool>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_RECOMENDACAO_AGRONOMICA_GS", x => x.ID_RECOMENDACAO);
                    table.ForeignKey(
                        name: "FK_TB_RECOMENDACAO_AGRONOMICA_GS_TB_ANALISE_DRONE_GS_ID_ANALISE_DRONE",
                        column: x => x.ID_ANALISE_DRONE,
                        principalTable: "TB_ANALISE_DRONE_GS",
                        principalColumn: "ID_ANALISE_DRONE",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_ANALISE_DRONE_GS_ID_MISSAO_DRONE",
                table: "TB_ANALISE_DRONE_GS",
                column: "ID_MISSAO_DRONE",
                unique: true,
                filter: "\"ID_MISSAO_DRONE\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TB_ANALISE_DRONE_GS_ID_PLANTACAO",
                table: "TB_ANALISE_DRONE_GS",
                column: "ID_PLANTACAO");

            migrationBuilder.CreateIndex(
                name: "IX_TB_DRONE_GS_CODIGO_IDENTIFICACAO",
                table: "TB_DRONE_GS",
                column: "CODIGO_IDENTIFICACAO",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_INSUMO_GS_ID_PLANTACAO",
                table: "TB_INSUMO_GS",
                column: "ID_PLANTACAO");

            migrationBuilder.CreateIndex(
                name: "IX_TB_LEITURA_SENSOR_GS_ID_MISSAO_DRONE",
                table: "TB_LEITURA_SENSOR_GS",
                column: "ID_MISSAO_DRONE");

            migrationBuilder.CreateIndex(
                name: "IX_TB_MISSAO_DRONE_GS_ID_DRONE",
                table: "TB_MISSAO_DRONE_GS",
                column: "ID_DRONE");

            migrationBuilder.CreateIndex(
                name: "IX_TB_MISSAO_DRONE_GS_ID_PLANTACAO",
                table: "TB_MISSAO_DRONE_GS",
                column: "ID_PLANTACAO");

            migrationBuilder.CreateIndex(
                name: "IX_TB_OCORRENCIA_PLANTACAO_GS_ID_ANALISE_DRONE",
                table: "TB_OCORRENCIA_PLANTACAO_GS",
                column: "ID_ANALISE_DRONE");

            migrationBuilder.CreateIndex(
                name: "IX_TB_OCORRENCIA_PLANTACAO_GS_ID_PLANTACAO",
                table: "TB_OCORRENCIA_PLANTACAO_GS",
                column: "ID_PLANTACAO");

            migrationBuilder.CreateIndex(
                name: "IX_TB_PLANTACOES_GS_ID_USER",
                table: "TB_PLANTACOES_GS",
                column: "ID_USER");

            migrationBuilder.CreateIndex(
                name: "IX_TB_RECOMENDACAO_AGRONOMICA_GS_ID_ANALISE_DRONE",
                table: "TB_RECOMENDACAO_AGRONOMICA_GS",
                column: "ID_ANALISE_DRONE");

            migrationBuilder.CreateIndex(
                name: "IX_TB_SAFRA_GS_ID_PLANTACAO",
                table: "TB_SAFRA_GS",
                column: "ID_PLANTACAO");

            migrationBuilder.CreateIndex(
                name: "IX_TB_USER_GS_CPF_USER",
                table: "TB_USER_GS",
                column: "CPF_USER",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_INSUMO_GS");

            migrationBuilder.DropTable(
                name: "TB_LEITURA_SENSOR_GS");

            migrationBuilder.DropTable(
                name: "TB_LOG_ERRO_GS");

            migrationBuilder.DropTable(
                name: "TB_OCORRENCIA_PLANTACAO_GS");

            migrationBuilder.DropTable(
                name: "TB_RECOMENDACAO_AGRONOMICA_GS");

            migrationBuilder.DropTable(
                name: "TB_SAFRA_GS");

            migrationBuilder.DropTable(
                name: "TB_ANALISE_DRONE_GS");

            migrationBuilder.DropTable(
                name: "TB_MISSAO_DRONE_GS");

            migrationBuilder.DropTable(
                name: "TB_DRONE_GS");

            migrationBuilder.DropTable(
                name: "TB_PLANTACOES_GS");

            migrationBuilder.DropTable(
                name: "TB_USER_GS");
        }
    }
}
