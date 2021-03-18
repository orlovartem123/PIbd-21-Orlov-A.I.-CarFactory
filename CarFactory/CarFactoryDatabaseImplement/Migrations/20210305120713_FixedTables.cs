using Microsoft.EntityFrameworkCore.Migrations;

namespace CarFactoryDatabaseImplement.Migrations
{
    public partial class FixedTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Products_CarId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductComponents_Products_CarId",
                table: "ProductComponents");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductComponents_Components_ComponentId",
                table: "ProductComponents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductComponents",
                table: "ProductComponents");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Cars");

            migrationBuilder.RenameTable(
                name: "ProductComponents",
                newName: "CarComponents");

            migrationBuilder.RenameIndex(
                name: "IX_ProductComponents_ComponentId",
                table: "CarComponents",
                newName: "IX_CarComponents_ComponentId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductComponents_CarId",
                table: "CarComponents",
                newName: "IX_CarComponents_CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cars",
                table: "Cars",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarComponents",
                table: "CarComponents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarComponents_Cars_CarId",
                table: "CarComponents",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarComponents_Components_ComponentId",
                table: "CarComponents",
                column: "ComponentId",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Cars_CarId",
                table: "Orders",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarComponents_Cars_CarId",
                table: "CarComponents");

            migrationBuilder.DropForeignKey(
                name: "FK_CarComponents_Components_ComponentId",
                table: "CarComponents");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Cars_CarId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cars",
                table: "Cars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarComponents",
                table: "CarComponents");

            migrationBuilder.RenameTable(
                name: "Cars",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "CarComponents",
                newName: "ProductComponents");

            migrationBuilder.RenameIndex(
                name: "IX_CarComponents_ComponentId",
                table: "ProductComponents",
                newName: "IX_ProductComponents_ComponentId");

            migrationBuilder.RenameIndex(
                name: "IX_CarComponents_CarId",
                table: "ProductComponents",
                newName: "IX_ProductComponents_CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductComponents",
                table: "ProductComponents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Products_CarId",
                table: "Orders",
                column: "CarId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductComponents_Products_CarId",
                table: "ProductComponents",
                column: "CarId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductComponents_Components_ComponentId",
                table: "ProductComponents",
                column: "ComponentId",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
