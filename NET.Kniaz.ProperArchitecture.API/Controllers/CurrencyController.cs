using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NET.Kniaz.ProperArchitecture.Application.Commands;
using NET.Kniaz.ProperArchitecture.Application.Abstractions;

namespace NET.Kniaz.ProperArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]  
    public class CurrencyController : ControllerBase
    {
        private ICommandHandler<CurrencyCommand> _currencyCommandHandler;

        public CurrencyController(ICommandHandler<CurrencyCommand> currencyCommandHandler)
        {
            _currencyCommandHandler = currencyCommandHandler;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CurrencyCommand>> GetCurrency(Guid id)
        {
            var currency = await _currencyCommandHandler.GetFullEntityAsync(id);
            return Ok(currency);
        }
        [HttpGet]
        public async Task<ActionResult<List<CurrencyCommand>>> GetAllCurrencies()
        {
            var currencies = await _currencyCommandHandler.GetEntitiesAsync();
            return Ok(currencies);
        }

        [HttpPost]
        public async Task<ActionResult<CurrencyCommand>> AddCurrency(CurrencyCommand currency)
        {
            await _currencyCommandHandler.AddEntity(currency);            
            return CreatedAtAction(nameof(GetCurrency), new { id = currency.Id }, currency);
        }

        
        [HttpPut]
        public async Task<ActionResult<CurrencyCommand>> UpdateCurrency(CurrencyCommand currency)
        {
            await _currencyCommandHandler.UpdateEntity(currency);
            return CreatedAtAction(nameof(GetCurrency), new { id = currency.Id }, currency);
        }

        [HttpDelete]
        public void DeleteCurrency(CurrencyCommand currency)
        {
            _currencyCommandHandler.RemoveEntity(currency);

        }
        

    }
}
