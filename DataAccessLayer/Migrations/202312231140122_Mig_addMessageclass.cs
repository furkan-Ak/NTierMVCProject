namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mig_addMessageclass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageID = c.Int(nullable: false, identity: true),
                        MessageContent = c.String(),
                        Subject = c.String(maxLength: 100),
                        MessageDate = c.DateTime(nullable: false),
                        SenderMail = c.String(maxLength: 50),
                        ReceiverMail = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.MessageID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Messages");
        }
    }
}
