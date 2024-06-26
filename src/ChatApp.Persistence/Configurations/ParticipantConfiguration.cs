using ChatApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Persistence.Configurations
{
    internal sealed class ParticipantConfiguration : IEntityTypeConfiguration<Participant>
    {
        public void Configure(EntityTypeBuilder<Participant> builder)
        {
            builder.HasOne(y => y.Conversation)
                .WithMany(x => x.Participants)
                .HasForeignKey(x => x.ConversationId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(y => y.AppUser)
                .WithMany(x => x.Participants)
                .HasForeignKey(x => x.AppUserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
