using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Domain;
using System.Configuration;
using System.Data;
using Domain.Entities;

namespace Application
{
    public class PaymentService : IPaymentService
    {
        IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public List<PaymentType> Get()
        {
            // кеш тута
            return _paymentRepository.Get();
        }

        public bool IsExists(string value)
        {
            foreach(PaymentType item in _paymentRepository.Get())
            {
                if(item.PType_id.Equals(value))
                {
                    return true;
                }
            }

            return false;
        }

    }
}
