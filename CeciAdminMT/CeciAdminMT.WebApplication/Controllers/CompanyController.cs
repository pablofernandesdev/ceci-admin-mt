using CeciAdminMT.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using CeciAdminMT.Domain.DTO.Commons;
using Microsoft.AspNetCore.Authorization;
using CeciAdminMT.Domain.DTO.Company;

namespace CeciAdminMT.WebApplication.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/company")]
    [ApiController]
    [Authorize(Policy = "Administrator")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyService"></param>
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        /// <summary>
        /// Add new company
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Success when creating a new company</returns>
        /// <response code="200">Returns success when creating a new item</response>
        /// <response code="400">Returns error if the request fails</response>
        /// <response code="401">Not authorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal server error</response>   
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResultResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ResultResponse))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResultResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResultResponse))]
        public async Task<ActionResult<ResultResponse>> Add([FromBody] CompanyAddDTO model)
        {
            var result = await _companyService.AddAsync(model);
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Update company
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Success when updating company</returns>
        /// <response code="200">Returns success when updating company</response>
        /// <response code="400">Returns error if the request fails</response>
        /// <response code="401">Not authorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal server error</response>   
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResultResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ResultResponse))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResultResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResultResponse))]
        public async Task<ActionResult<ResultResponse>> Update([FromBody] CompanyUpdateDTO model)
        {
            var result = await _companyService.UpdateAsync(model);
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Delete company
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Success when delete company</returns>
        /// <response code="200">Returns success when deleted company</response>
        /// <response code="400">Returns error if the request fails</response>
        /// <response code="401">Not authorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal server error</response>   
        [HttpDelete]
        [Route("{companyId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResultResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ResultResponse))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResultResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResultResponse))]
        public async Task<ActionResult<ResultResponse>> Delete([FromRoute] CompanyDeleteDTO model)
        {
            var result = await _companyService.DeleteAsync(model);
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Get all companys
        /// </summary>
        /// <returns>Success when get all companys</returns>
        /// <response code="200">Returns success when get all companys</response>
        /// <response code="401">Not authorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal server error</response>   
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDataResponse<IEnumerable<CompanyResultDTO>>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ResultResponse))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResultResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResultResponse))]
        public async Task<ActionResult<ResultDataResponse<IEnumerable<CompanyResultDTO>>>> Get([FromQuery] CompanyFilterDTO filter)
        {
            var result = await _companyService.GetAsync(filter);
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Get company by id
        /// </summary>
        /// <returns>Success when get company by id</returns>
        /// <response code="200">Returns success when get company by id</response>
        /// <response code="401">Not authorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal server error</response>   
        [HttpGet]
        [Route("{companyId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultResponse<CompanyResultDTO>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ResultResponse))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResultResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResultResponse))]
        public async Task<ActionResult<ResultResponse<CompanyResultDTO>>> GetById([FromRoute] CompanyIdentifierDTO model)
        {
            var result = await _companyService.GetByIdAsync(model.CompanyId);
            return StatusCode((int)result.StatusCode, result);
        }      
    }
}
