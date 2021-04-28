using Games.Core.Entities.Users;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Games.Core.Entities.Games
{
    public class Rate : BaseEntity
    {
        public Rate(string comment, double value, long userId, long gameId)
        {
            Comment = comment ?? throw new ArgumentNullException(nameof(comment));
            Value = value;
            UserId = userId;
            GameId = gameId;
            Date = DateTimeOffset.UtcNow;
        }

        public string Comment { get; private set; }
        public double Value { get; private set; }

        [ForeignKey(nameof(User))]
        public long UserId { get; private set; }
        public virtual User User { get; private set; }

        [ForeignKey(nameof(GameId))]
        public long GameId { get; private set; }
        public virtual Game Game { get; private set; }
        public DateTimeOffset Date { get; set; }
    }
}
