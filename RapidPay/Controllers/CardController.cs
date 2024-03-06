using Microsoft.AspNetCore.Mvc;
using RapidPay.Business;
using RapidPay.Interface;
using RapidPay.DTOs;
using RapidPay.Mappers;
using RapidPay.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RapidPay.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class CardController : ControllerBase
    {
        private readonly ICardManager CardManager;

        private readonly ILoggerManager Logger;

        private readonly PaymentFeeManager PaymentFeeManager;


        public CardController(ICardManager cardManager,  PaymentFeeManager paymentFeeManager, ILoggerManager logger)
        {
            CardManager = cardManager;
            PaymentFeeManager = paymentFeeManager;
            Logger = logger;
           
        }


        [HttpGet]
        public async Task<IActionResult> GetCardBalance(string cardNumber)
        {
            Logger.LogInfo("GetCard");
            if (cardNumber.Length != 15|| !cardNumber.All(char.IsDigit))
            {
                Logger.LogError(string.Format("Input card number : {0}", cardNumber));
                return BadRequest("Invalid card number format.");
            }

            var card = await CardManager.GetCardFromCardNumber(cardNumber);
            if(card == null)
            {
                Logger.LogError("The card does not exist in the system");
                return NotFound("An error occured while processing your request");
            }

            return Ok(string.Format("The balance on this card is {0}", card.balance));
        }

        
        [HttpPost]
        public async Task<IActionResult> CreateCard([FromBody] CardDto cardDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var card = cardDto.ToCardFromCreateDTO();

            if (!await CardManager.CardExists(card.cardNumber))
            {
                
                await CardManager.CreateCardAsync(card);
                return Ok(card);
            }
            else
            {
                Logger.LogError("The card  already exists in the system");
                return BadRequest("An error occured while creating the card"); // deliberately giving a crypting error message, so that we don't indicate that card already exists
            }
        }

       
        [HttpPut()]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Pay([FromBody] UpdateCardDto updateCardDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var transactionFee = PaymentFeeManager.GetCurrentFee();
            var card = await CardManager.Pay(updateCardDto, transactionFee);

            if (card == null)
            {
                Logger.LogError("The card does not exist in the system");
                return NotFound("An error occured while processing your request");
            }
            else
            {
                var transaction = updateCardDto.ToTransactionFromUpdateDTO(transactionFee, card.Id);
                await CardManager.CreateTransactionAsync(transaction);              
                return Ok(string.Format("The new card balance is {0}", card.balance));
            }
        }

    }
}
