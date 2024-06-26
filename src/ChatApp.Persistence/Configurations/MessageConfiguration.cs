using ChatApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Persistence.Configurations
{
    internal sealed class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasOne(y => y.Conversation)
                .WithMany(x => x.Messages)
                .HasForeignKey(x => x.ConversationId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(y => y.Sender)
                .WithMany(x => x.MessageSends)
                .HasForeignKey(x => x.SenderId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
