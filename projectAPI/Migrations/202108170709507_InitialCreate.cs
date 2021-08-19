namespace projectAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentID = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(),
                    })
                .PrimaryKey(t => t.DepartmentID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmpID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        DOJ = c.DateTime(nullable: false),
                        Department_DepartmentID = c.Int(),
                    })
                .PrimaryKey(t => t.EmpID)
                .ForeignKey("dbo.Departments", t => t.Department_DepartmentID)
                .Index(t => t.Department_DepartmentID);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProjectName = c.String(),
                        ProjectStartDate = c.DateTime(nullable: false),
                        ProjectEndDate = c.DateTime(nullable: false),
                        ProjectManagerEmail = c.String(),
                        Employee_EmpID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.Employee_EmpID)
                .Index(t => t.Employee_EmpID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "Employee_EmpID", "dbo.Employees");
            DropForeignKey("dbo.Employees", "Department_DepartmentID", "dbo.Departments");
            DropIndex("dbo.Projects", new[] { "Employee_EmpID" });
            DropIndex("dbo.Employees", new[] { "Department_DepartmentID" });
            DropTable("dbo.Projects");
            DropTable("dbo.Employees");
            DropTable("dbo.Departments");
        }
    }
}
