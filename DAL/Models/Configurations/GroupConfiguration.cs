using DAL.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> entity)
        {
            entity.ToTable("Groups");
            entity.HasKey(g => g.GroupID);

            entity.Property(g => g.GroupName)
                  .IsRequired()
                  .HasMaxLength(50);
        }
    }
}
