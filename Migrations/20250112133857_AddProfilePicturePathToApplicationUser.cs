using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace my_event_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddProfilePicturePathToApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
               name: "ProfilePicturePath",
               table: "AspNetUsers",
               type: "nvarchar(max)",
               nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicturePath",
                table: "AspNetUsers");
        }
    }
}
