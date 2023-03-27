using CryptoAvenue.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Dal.EntityTypeConfigurations
{
    public class TradeOfferConfiguration : IEntityTypeConfiguration<TradeOffer>
    {
        public void Configure(EntityTypeBuilder<TradeOffer> builder)
        {
            //Configuration for the coins in the trade offer
            builder
                .HasOne(offer => offer.SentCoin)
                .WithMany(coin => coin.OffersSent)
                .HasForeignKey(offer => offer.SentCoinId)
                .OnDelete(DeleteBehavior.Restrict);
            builder
                .HasOne(offer => offer.ReceivedCoin)
                .WithMany(coin => coin.OffersReceived)
                .HasForeignKey(offer => offer.ReceivedCoinId)
                .OnDelete(DeleteBehavior.Restrict);

            //Configuration for the users in the trade offer
            builder
                .HasOne(offer => offer.Sender)
                .WithMany(user => user.OffersSent)
                .HasForeignKey(offer => offer.SenderId)
                .OnDelete(DeleteBehavior.Restrict);
            builder
                .HasOne(offer => offer.Recipient)
                .WithMany(user => user.OffersReceived)
                .HasForeignKey(offer => offer.RecipientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
