using AutoMapper;
using CryptoAvenue.Domain.Models;
using CryptoAvenue.Dtos.WalletDtos;

namespace CryptoAvenue.Profiles
{
    public class WalletProfile : Profile
    {
        public WalletProfile()
        {
            CreateMap<Wallet, WalletPostDto>()
                .ForMember(d => d.UserId, opt => opt.MapFrom(w => w.UserId))
                .ForMember(d => d.Quantity, opt => opt.MapFrom(w => w.Balance));
            CreateMap<WalletPostDto, Wallet>()
                .ForMember(d => d.UserId, opt => opt.MapFrom(w => w.UserId))
                .ForMember(d => d.Balance, opt => opt.MapFrom(w => w.Quantity));
        }
    }
}
