using Entities.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Repository.Services;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TitleDebtController : Controller
    {
        private readonly IService _service;
        public TitleDebtController(IService service)
        {
            _service = service;
        }

        [HttpGet("titles/calculated")]
        public async Task<IActionResult> GetAllTitleWithCalculatedValues()
        {
            try
            {
                var result = await _service.GetAllTitleWithCalculatedValues();
                if (result == null || result.Count == 0)
                {
                    return NotFound(new { message = "Não há títulos cadastrados" });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao processar a solicitação do servidor: {ex.Message}");
            }
        }

        [HttpPost("titles")]
        public async Task<IActionResult> InsertDebtTitle([FromBody] DebtTitleDto dto)
        {
            try
            {
                if (dto == null || dto.Installments == null || !dto.Installments.Any())
                {
                    return BadRequest(new { message = "Título e/ou parcelas inválidos" });
                }
                await _service.InsertDebtTitle(dto);
                return StatusCode(201, new {message = "Título cadastrado com sucesso"});
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao processar a solicitação do servidor: {ex.Message}");
            }
        }
    }
}
