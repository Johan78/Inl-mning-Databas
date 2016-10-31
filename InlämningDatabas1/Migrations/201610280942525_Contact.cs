namespace InlämningDatabas1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Contact : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        ContactID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        PostalCode = c.String(),
                        City = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        DOB = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.ContactID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Contacts");
        }
    }
}
