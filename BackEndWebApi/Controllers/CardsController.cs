using BackEndWebApi.Models;
using BackEndWebApi.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndWebApi.Controllers
{

    [ApiController]
    [Route("api/[Controller]")]
    public class CardsController : Controller
    {
        private readonly CardsDbContext _cardsDbContext;

        public CardsController(CardsDbContext cardsDbContext)
        {
            _cardsDbContext = cardsDbContext;
        }

        //Get all cards
        [HttpGet]
        public async Task<IActionResult> GetAllCards()
        {
            var Cards = await _cardsDbContext.Cards.ToListAsync();
            return Ok(Cards);
        }

        //Get Single Card
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetCard")]
        public async Task<IActionResult> GetCard([FromRoute] Guid id)
        {
            var card = await _cardsDbContext.Cards.FirstOrDefaultAsync(x => x.Id == id);

            if (card != null)
            {
                return Ok(card);
            }

            return NotFound("Card not found");

        }

        //Add Card
        [HttpPost]
        public async Task<IActionResult> AddCard([FromBody] Card card)
        {
            card.Id = Guid.NewGuid();
            await _cardsDbContext.Cards.AddAsync(card);
            await _cardsDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCard), new { id = card.Id }, card);
        }
        
        //Updating a card
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateCard([FromRoute] Guid id, [FromBody] Card card)
        {
            var existingCard = await _cardsDbContext.Cards.FirstOrDefaultAsync(x => x.Id == id);

            if (existingCard != null)
            {
                existingCard.NomeCard = card.NomeCard;
                existingCard.NumeroCard = card.NumeroCard;
                existingCard.MesDeVencimento = card.MesDeVencimento;
                existingCard.AnoDeVencimento = card.AnoDeVencimento;
                existingCard.CVC = card.CVC;
                await _cardsDbContext.SaveChangesAsync();
                return Ok(existingCard);
            }

            return NotFound("Card not Found");
        }


        //Delete a card
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteCard([FromRoute] Guid id)
        {
            var existingCard = await _cardsDbContext.Cards.FirstOrDefaultAsync(x => x.Id == id);

            if (existingCard != null)
            {
                _cardsDbContext.Remove(existingCard);
                await _cardsDbContext.SaveChangesAsync();
                return Ok(existingCard);
            }

            return NotFound("Card not Found");
        }



    }
}
