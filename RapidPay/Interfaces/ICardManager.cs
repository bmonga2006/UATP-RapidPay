

using RapidPay.DTOs;
using RapidPay.Models;

namespace RapidPay.Interface
{
    public interface ICardManager
    {

        public  Task<Card> CreateCardAsync(Card card);

        public Task<bool> CardExists(string cardNumber);

        public Task<Card?> GetCardFromCardNumber(string cardNumber);

        public Task<Card?> Pay(UpdateCardDto updateCardDto, decimal transactionFee);

        public Task<Transaction> CreateTransactionAsync(Transaction tranaction);
    }
}
