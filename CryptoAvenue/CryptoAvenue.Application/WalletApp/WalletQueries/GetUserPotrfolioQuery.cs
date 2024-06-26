﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.WalletApp.WalletQueries
{
    public class GetUserPotrfolioQuery : IRequest<string>
    {
        public Guid UserId { get; set; }
    }
}
