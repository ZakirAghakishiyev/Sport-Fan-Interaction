using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSelling.Application.Dtos.CardDetails;

public class CardDetailsDto
{
    public int Id { get; set; }
    public string? CardType { get; set; }
    public int? Last4Digits { get; set; }
    public string? PhoneNumber { get; set; }
}

