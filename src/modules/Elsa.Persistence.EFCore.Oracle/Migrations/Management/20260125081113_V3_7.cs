using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elsa.Persistence.EFCore.Oracle.Migrations.Management
{
    /// <inheritdoc />
    public partial class V3_7 : Migration
    {
        private readonly Elsa.Persistence.EFCore.IElsaDbContextSchema _schema;

        /// <inheritdoc />
        public V3_7(Elsa.Persistence.EFCore.IElsaDbContextSchema schema)
        {
            _schema = schema;
        }

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InstanceId",
                schema: _schema.Schema,
                table: "WorkflowDefinitions",
                type: "NUMBER(10)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowDefinition_InstanceId",
                schema: _schema.Schema,
                table: "WorkflowDefinitions",
                column: "InstanceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WorkflowDefinition_InstanceId",
                schema: _schema.Schema,
                table: "WorkflowDefinitions");

            migrationBuilder.DropColumn(
                name: "InstanceId",
                schema: _schema.Schema,
                table: "WorkflowDefinitions");
        }
    }
}
