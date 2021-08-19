namespace projectAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProjectEmployees", "Project_ID", "dbo.Projects");
            DropForeignKey("dbo.ProjectEmployees", "Employee_EmpID", "dbo.Employees");
            DropIndex("dbo.ProjectEmployees", new[] { "Project_ID" });
            DropIndex("dbo.ProjectEmployees", new[] { "Employee_EmpID" });
            AddColumn("dbo.Employees", "ProjectID_ID", c => c.Int());
            AddColumn("dbo.Projects", "ProjectManagerName", c => c.String());
            CreateIndex("dbo.Employees", "ProjectID_ID");
            AddForeignKey("dbo.Employees", "ProjectID_ID", "dbo.Projects", "ID");
            DropTable("dbo.ProjectEmployees");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProjectEmployees",
                c => new
                    {
                        Project_ID = c.Int(nullable: false),
                        Employee_EmpID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Project_ID, t.Employee_EmpID });
            
            DropForeignKey("dbo.Employees", "ProjectID_ID", "dbo.Projects");
            DropIndex("dbo.Employees", new[] { "ProjectID_ID" });
            DropColumn("dbo.Projects", "ProjectManagerName");
            DropColumn("dbo.Employees", "ProjectID_ID");
            CreateIndex("dbo.ProjectEmployees", "Employee_EmpID");
            CreateIndex("dbo.ProjectEmployees", "Project_ID");
            AddForeignKey("dbo.ProjectEmployees", "Employee_EmpID", "dbo.Employees", "EmpID", cascadeDelete: true);
            AddForeignKey("dbo.ProjectEmployees", "Project_ID", "dbo.Projects", "ID", cascadeDelete: true);
        }
    }
}
