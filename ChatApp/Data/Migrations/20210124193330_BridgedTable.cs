using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatApp.Data.Migrations
{
    public partial class BridgedTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatUsers_ChatRooms_ChatRoomDtoId",
                table: "ChatUsers");

            migrationBuilder.DropIndex(
                name: "IX_ChatUsers_ChatRoomDtoId",
                table: "ChatUsers");

            migrationBuilder.DropColumn(
                name: "ChatRoomDtoId",
                table: "ChatUsers");

            migrationBuilder.CreateTable(
                name: "ChatRoomDtoChatUserDto",
                columns: table => new
                {
                    ChatRoomsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UsersName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatRoomDtoChatUserDto", x => new { x.ChatRoomsId, x.UsersName });
                    table.ForeignKey(
                        name: "FK_ChatRoomDtoChatUserDto_ChatRooms_ChatRoomsId",
                        column: x => x.ChatRoomsId,
                        principalTable: "ChatRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatRoomDtoChatUserDto_ChatUsers_UsersName",
                        column: x => x.UsersName,
                        principalTable: "ChatUsers",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatRoomDtoChatUserDto_UsersName",
                table: "ChatRoomDtoChatUserDto",
                column: "UsersName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatRoomDtoChatUserDto");

            migrationBuilder.AddColumn<string>(
                name: "ChatRoomDtoId",
                table: "ChatUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChatUsers_ChatRoomDtoId",
                table: "ChatUsers",
                column: "ChatRoomDtoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatUsers_ChatRooms_ChatRoomDtoId",
                table: "ChatUsers",
                column: "ChatRoomDtoId",
                principalTable: "ChatRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
