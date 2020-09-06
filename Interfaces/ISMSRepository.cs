using System;
using System.Collections.Generic;
using HolisticAccountant.Models.DTO;
using HolisticAccountant.Models.Entities;

namespace HolisticAccountant.Interfaces
{
    public interface ISMSRepository
    {
        List<TransactionDTO> PostSMSList(SMSListDTO request);
    }
}
