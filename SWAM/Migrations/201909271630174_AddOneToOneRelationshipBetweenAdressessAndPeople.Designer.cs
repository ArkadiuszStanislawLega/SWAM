// <auto-generated />
namespace SWAM.Migrations
{
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;
    
    [GeneratedCode("EntityFramework.Migrations", "6.2.0-61023")]
    public sealed partial class AddOneToOneRelationshipBetweenAdressessAndPeople : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(AddOneToOneRelationshipBetweenAdressessAndPeople));
        
        string IMigrationMetadata.Id
        {
            get { return "201909271630174_AddOneToOneRelationshipBetweenAdressessAndPeople"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return null; }
        }
        
        string IMigrationMetadata.Target
        {
            get { return Resources.GetString("Target"); }
        }
    }
}
