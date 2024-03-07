using RapidPay.DTOs;
using RapidPay.Helpers;
using RapidPay.Models;

namespace RapidPay.Mappers
{
    public static class CardMappers
    {
        public static Card ToCardFromCreateDTO(this CardDto cardDto)
        {
            return new Card
            {
                cardNumber = Encryption.Encrypt(cardDto.cardNumber),
                balance = cardDto.balance,
                last4digits = cardDto.cardNumber.Substring(cardDto.cardNumber.Length - 4),
                dateCreated = DateTime.Now,
                dateUpdated = DateTime.Now
            };
        }

        public static CardDto ToCardDTOFromCreate(this CardDto card)
        {
            return new CardDto

            {
                cardNumber = card.cardNumber,
                balance = card.balance
            };
        }

        public static Transaction ToTransactionFromUpdateDTO(this UpdateCardDto updateCardDto, decimal transactionFee, int cardId)
        {
            return new Transaction
            {
                cardId = cardId,
                transactionAmount = updateCardDto.amount,
                transactionFee = transactionFee,               
                dateCreated = DateTime.Now
                
            };
        }
    }
}
