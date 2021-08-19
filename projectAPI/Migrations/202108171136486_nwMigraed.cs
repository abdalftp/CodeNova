namespace projectAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nwMigraed : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Projects", "Employee_EmpID", "dbo.Employees");
            DropIndex("dbo.Projects", new[] { "Employee_EmpID" });
            CreateTable(
                "dbo.ProjectEmployees",
                c => new
                    {
                        Project_ID = c.Int(nullable: false),
                        Employee_EmpID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Project_ID, t.Employee_EmpID })
                .ForeignKey("dbo.Projects", t => t.Project_ID, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.Employee_EmpID, cascadeDelete: true)
                .Index(t => t.Project_ID)
                .Index(t => t.Employee_EmpID);
            
            DropColumn("dbo.Projects", "Employee_EmpID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Projects", "Employee_EmpID", c => c.Int());
            DropForeignKey("dbo.ProjectEmployees", "Employee_EmpID", "dbo.Employees");
            DropForeignKey("dbo.ProjectEmployees", "Project_ID", "dbo.Projects");
            DropIndex("dbo.ProjectEmployees", new[] { "Employee_EmpID" });
            DropIndex("dbo.ProjectEmployees", new[] { "Project_ID" });
            DropTable("dbo.ProjectEmployees");
            CreateIndex("dbo.Projects", "Employee_EmpID");
            AddForeignKey("dbo.Projects", "Employee_EmpID", "dbo.Employees", "EmpID");
        }
    }
}
