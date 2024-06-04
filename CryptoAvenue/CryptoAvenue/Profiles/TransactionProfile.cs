using AutoMapper;
using CryptoAvenue.Domain.Models;
using CryptoAvenue.Dtos.TransactionDtos;
//using System.Transactions;

namespace CryptoAvenue.Profiles
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, TransactionGetDto>()
                .ForMember(d => d.WalletId, opt => opt.MapFrom(t => t.WalletId))
                .ForMember(d => d.SourceCoinId, opt => opt.MapFrom(t => t.SourceCoinId))
                .ForMember(d => d.SourceQuantity, opt => opt.MapFrom(t => t.SourceQuantity))
                .ForMember(d => d.SourcePrice, opt => opt.MapFrom(t => t.SourcePrice))
                .ForMember(d => d.TargetCoinId, opt => opt.MapFrom(t => t.TargetCoinId))
                .ForMember(d => d.TargetQuantity, opt => opt.MapFrom(t => t.TargetQuantity))
                .ForMember(d => d.TargetPrice, opt => opt.MapFrom(t => t.TargetPrice))
                .ForMember(d => d.TransactionType, opt => opt.MapFrom(t => t.TransactionType))
                .ForMember(d => d.TransactionDate, opt => opt.MapFrom(t => DateOnly.FromDateTime(t.TransactionDate)))
                .ForMember(d => d.TransactionTime, opt => opt.MapFrom(t => TimeOnly.FromTimeSpan(t.TransactionDate.TimeOfDay)));  // Convert TimeSpan to TimeOnly

            CreateMap<TransactionGetDto, Transaction>()
                .ForMember(d => d.WalletId, opt => opt.MapFrom(t => t.WalletId))
                .ForMember(d => d.SourceCoinId, opt => opt.MapFrom(t => t.SourceCoinId))
                .ForMember(d => d.SourceQuantity, opt => opt.MapFrom(t => t.SourceQuantity))
                .ForMember(d => d.SourcePrice, opt => opt.MapFrom(t => t.SourcePrice))
                .ForMember(d => d.TargetCoinId, opt => opt.MapFrom(t => t.TargetCoinId))
                .ForMember(d => d.TargetQuantity, opt => opt.MapFrom(t => t.TargetQuantity))
                .ForMember(d => d.TargetPrice, opt => opt.MapFrom(t => t.TargetPrice))
                .ForMember(d => d.TransactionType, opt => opt.MapFrom(t => t.TransactionType))
                .ForMember(d => d.TransactionDate, opt => opt.Ignore());  // Ignore this during reverse mapping
                //.AfterMap((src, dest) => dest.TransactionDate = src.TransactionDate.ToDateTime(src.TransactionTime.ToTimeSpan())); // Convert TimeOnly back to TimeSpan
        }
    }
}
