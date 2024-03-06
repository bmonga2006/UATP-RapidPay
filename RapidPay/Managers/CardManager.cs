

using Microsoft.EntityFrameworkCore;
using RapidPay.Data;
using RapidPay.DTOs;
using RapidPay.Helpers;
using RapidPay.Interface;
using RapidPay.Models;
using System.Collections.Generic;

namespace RapidPay.Business
{
    public class CardManager : ICardManager
    {
        private readonly ApplicationDBContext dbContext;

        public CardManager(ApplicationDBContext context)
        {
            dbContext = context;
        }

        public async Task<Card> CreateCardAsync(Card card)
        {

            await dbContext.Cards.AddAsync(card);
            await dbContext.SaveChangesAsync();
            return card;
        }

        public Task<bool> CardExists(string cardNumber)
        {

            return dbContext.Cards.AnyAsync(c => c.cardNumber == cardNumber);
        }

        public async Task<Card?> GetCardFromCardNumber(string cardNumber)
        {
            return await dbContext.Cards.FirstOrDefaultAsync(c => c.cardNumber.Equals(Encryption.Encrypt(cardNumber)));
        }

        public async Task<Card?> Pay(UpdateCardDto updateCardDto, decimal transactionFee)
        {
            var existingCard = await GetCardFromCardNumber(updateCardDto.cardNumber);

            if (existingCard == null)
            {
                return null;   
            }

            existingCard.balance = existingCard.balance + updateCardDto.amount + transactionFee;

            await dbContext.SaveChangesAsync();

            return existingCard;
        }

        public async Task<Transaction> CreateTransactionAsync(Transaction tranaction)
        {

            await dbContext.Tranactions.AddAsync(tranaction);
            await dbContext.SaveChangesAsync();
            return tranaction;
        }



    }
}
