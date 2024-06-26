using ChatApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Persistence.Configurations
{
    internal sealed class ConversationConfiguration : IEntityTypeConfiguration<Conversation>
    {
        public void Configure(EntityTypeBuilder<Conversation> builder)
        {
            builder.HasOne(y => y.Creator)
                 .WithMany(x => x.ConversationCreateds)
                 .HasForeignKey(x => x.CreatorId)
                 .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
